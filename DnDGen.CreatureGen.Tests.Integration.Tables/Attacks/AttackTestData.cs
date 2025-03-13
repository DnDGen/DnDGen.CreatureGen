using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Magics;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.TreasureGen.Items;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Attacks
{
    public class AttackTestData
    {
        public const string None = "NONE";

        public static Dictionary<string, List<string>> GetCreatureAttackData()
        {
            var testCases = new Dictionary<string, List<string[]>>();
            var creatures = CreatureConstants.GetAll();

            var biteDamageType = $"{AttributeConstants.DamageTypes.Piercing}/{AttributeConstants.DamageTypes.Slashing}/{AttributeConstants.DamageTypes.Bludgeoning}";
            var clawDamageType = $"{AttributeConstants.DamageTypes.Piercing}/{AttributeConstants.DamageTypes.Slashing}";
            var goreDamageType = $"{AttributeConstants.DamageTypes.Piercing}";
            var slapSlamDamageType = $"{AttributeConstants.DamageTypes.Bludgeoning}";
            var stingDamageType = $"{AttributeConstants.DamageTypes.Piercing}";
            var tentacleDamageType = $"{AttributeConstants.DamageTypes.Bludgeoning}";

            foreach (var creature in creatures)
            {
                testCases[creature] = new List<string[]>();
            }

            testCases[CreatureConstants.Aasimar].Add(BuildData(AttributeConstants.Melee,
                string.Empty,
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Aasimar].Add(BuildData(AttributeConstants.Ranged,
                string.Empty,
                string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Aasimar].Add(BuildData("Unarmed Strike",
                damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Aasimar].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Aboleth].Add(BuildData("Tentacle",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                "Slime", 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Aboleth].Add(BuildData("Enslave",
                string.Empty,
                string.Empty, 0, "supernatural ability", 3, FeatConstants.Frequencies.Day, false, true, true, true,
                SaveConstants.Will, AbilityConstants.Charisma));
            testCases[CreatureConstants.Aboleth].Add(BuildData("Slime",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true,
                SaveConstants.Fortitude, AbilityConstants.Constitution));
            testCases[CreatureConstants.Aboleth].Add(BuildData(FeatConstants.SpecialQualities.Psionic,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Achaierai].Add(BuildData("Claw",
                damageHelper.BuildEntries("1d6", clawDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Achaierai].Add(BuildData("Bite",
                damageHelper.BuildEntries("4d6", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Achaierai].Add(BuildData("Black cloud",
                damageHelper.BuildEntries("2d6"),
                SpellConstants.Insanity, 0, "extraordinary ability", 3, FeatConstants.Frequencies.Day, false, true, true, true,
                SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Allip].Add(BuildData("Incorporeal touch",
                string.Empty,
                "Wisdom drain", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Allip].Add(BuildData("Babble",
                string.Empty,
                SpellConstants.Hypnotism, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true,
                SaveConstants.Will, AbilityConstants.Charisma));
            testCases[CreatureConstants.Allip].Add(BuildData("Madness",
                damageHelper.BuildEntries("1d4", AbilityConstants.Wisdom),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));
            testCases[CreatureConstants.Allip].Add(BuildData("Wisdom drain",
                damageHelper.BuildEntries("1d4", AbilityConstants.Wisdom),
                "Allip gains 5 temporary hit points", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));

            testCases[CreatureConstants.Androsphinx].Add(BuildData("Claw",
                damageHelper.BuildEntries("2d4", clawDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Androsphinx].Add(BuildData("Pounce",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.Androsphinx].Add(BuildData("Rake",
                damageHelper.BuildEntries("2d4", clawDamageType),
                string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.Androsphinx].Add(BuildData("Roar",
                string.Empty,
                string.Empty, 0, "supernatural ability", 3, FeatConstants.Frequencies.Day, false, true, true, true,
                SaveConstants.Will, AbilityConstants.Charisma));
            testCases[CreatureConstants.Androsphinx].Add(BuildData("Spells",
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData(AttributeConstants.Melee,
                string.Empty,
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d8", slapSlamDamageType),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData("Stun",
                string.Empty,
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Angel_AstralDeva].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(AttributeConstants.Melee,
                string.Empty,
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d8", slapSlamDamageType),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Angel_Planetar].Add(BuildData("Spells",
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Angel_Solar].Add(BuildData(AttributeConstants.Melee,
                string.Empty,
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(AttributeConstants.Ranged,
                string.Empty,
                string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d8", slapSlamDamageType),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Angel_Solar].Add(BuildData("Spells",
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Tiny].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.AnimatedObject_Tiny_Flexible].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Tiny_Flexible].Add(BuildData("Constrict",
                damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Tiny_MultipleLegs].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.AnimatedObject_Tiny_Sheetlike].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Tiny_Sheetlike].Add(BuildData("Blind",
                string.Empty,
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.AnimatedObject_Tiny_Sheetlike].Add(BuildData("Constrict",
                damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Tiny_TwoLegs].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.AnimatedObject_Tiny_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.AnimatedObject_Small].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.AnimatedObject_Small_Flexible].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Small_Flexible].Add(BuildData("Constrict",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Small_MultipleLegs].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.AnimatedObject_Small_Sheetlike].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Small_Sheetlike].Add(BuildData("Blind",
                string.Empty,
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.AnimatedObject_Small_Sheetlike].Add(BuildData("Constrict",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Small_TwoLegs].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.AnimatedObject_Small_Wheels_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.AnimatedObject_Small_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.AnimatedObject_Medium].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.AnimatedObject_Medium_Flexible].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Medium_Flexible].Add(BuildData("Constrict",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Medium_MultipleLegs].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.AnimatedObject_Medium_Sheetlike].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Medium_Sheetlike].Add(BuildData("Blind",
                string.Empty,
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.AnimatedObject_Medium_Sheetlike].Add(BuildData("Constrict",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Medium_TwoLegs].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.AnimatedObject_Medium_Wheels_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.AnimatedObject_Medium_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.AnimatedObject_Large].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Large].Add(BuildData("Trample",
                damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Large_Flexible].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Large_Flexible].Add(BuildData("Trample",
                damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.AnimatedObject_Large_Flexible].Add(BuildData("Constrict",
                damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Large_MultipleLegs].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Large_MultipleLegs].Add(BuildData("Trample",
                damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden].Add(BuildData("Trample",
                damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Large_Sheetlike].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Large_Sheetlike].Add(BuildData("Trample",
                damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.AnimatedObject_Large_Sheetlike].Add(BuildData("Blind",
                string.Empty,
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.AnimatedObject_Large_Sheetlike].Add(BuildData("Constrict",
                damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Large_TwoLegs].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Large_TwoLegs].Add(BuildData("Trample",
                damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden].Add(BuildData("Trample",
                damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Large_Wheels_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Large_Wheels_Wooden].Add(BuildData("Trample",
                damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Large_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Large_Wooden].Add(BuildData("Trample",
                damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Huge].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Huge].Add(BuildData("Trample",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Huge_Flexible].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Huge_Flexible].Add(BuildData("Trample",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.AnimatedObject_Huge_Flexible].Add(BuildData("Constrict",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Huge_MultipleLegs].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Huge_MultipleLegs].Add(BuildData("Trample",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden].Add(BuildData("Trample",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Huge_Sheetlike].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Huge_Sheetlike].Add(BuildData("Trample",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.AnimatedObject_Huge_Sheetlike].Add(BuildData("Blind",
                string.Empty,
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.AnimatedObject_Huge_Sheetlike].Add(BuildData("Constrict",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Huge_TwoLegs].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Huge_TwoLegs].Add(BuildData("Trample",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden].Add(BuildData("Trample",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Huge_Wheels_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Huge_Wheels_Wooden].Add(BuildData("Trample",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Huge_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Huge_Wooden].Add(BuildData("Trample",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Gargantuan].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Gargantuan].Add(BuildData("Trample",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Gargantuan_Flexible].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Gargantuan_Flexible].Add(BuildData("Trample",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.AnimatedObject_Gargantuan_Flexible].Add(BuildData("Constrict",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs].Add(BuildData("Trample",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden].Add(BuildData("Trample",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Gargantuan_Sheetlike].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Gargantuan_Sheetlike].Add(BuildData("Trample",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.AnimatedObject_Gargantuan_Sheetlike].Add(BuildData("Blind",
                string.Empty,
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.AnimatedObject_Gargantuan_Sheetlike].Add(BuildData("Constrict",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs].Add(BuildData("Trample",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden].Add(BuildData("Trample",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden].Add(BuildData("Trample",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Gargantuan_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Gargantuan_Wooden].Add(BuildData("Trample",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Colossal].Add(BuildData("Slam",
                damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Colossal].Add(BuildData("Trample",
                damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Colossal_Flexible].Add(BuildData("Slam",
                damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Colossal_Flexible].Add(BuildData("Trample",
                damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.AnimatedObject_Colossal_Flexible].Add(BuildData("Constrict",
                damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Colossal_MultipleLegs].Add(BuildData("Slam",
                damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Colossal_MultipleLegs].Add(BuildData("Trample",
                damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden].Add(BuildData("Trample",
                damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Colossal_Sheetlike].Add(BuildData("Slam",
                damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Colossal_Sheetlike].Add(BuildData("Trample",
                damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.AnimatedObject_Colossal_Sheetlike].Add(BuildData("Blind",
                string.Empty,
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.AnimatedObject_Colossal_Sheetlike].Add(BuildData("Constrict",
                damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Colossal_TwoLegs].Add(BuildData("Slam",
                damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Colossal_TwoLegs].Add(BuildData("Trample",
                damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden].Add(BuildData("Trample",
                damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden].Add(BuildData("Trample",
                damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.AnimatedObject_Colossal_Wooden].Add(BuildData("Slam",
                damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AnimatedObject_Colossal_Wooden].Add(BuildData("Trample",
                damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.Ankheg].Add(BuildData("Bite",
                damageHelper.BuildEntries(
                    "2d6", biteDamageType, string.Empty,
                    "1d4", FeatConstants.Foci.Elements.Acid),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Ankheg].Add(BuildData("Improved Grab",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Ankheg].Add(BuildData("Spit Acid",
                damageHelper.BuildEntries("4d4", FeatConstants.Foci.Elements.Acid),
                string.Empty, 0, "extraordinary ability", 1, $"6 {FeatConstants.Frequencies.Hour}", false, true, true, true));

            testCases[CreatureConstants.Annis].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Annis].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Annis].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Annis].Add(BuildData("Rake", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.Annis].Add(BuildData("Rend", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Annis].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Ant_Giant_Worker].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Ant_Giant_Worker].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Ant_Giant_Soldier].Add(BuildData("Bite",
                damageHelper.BuildEntries("2d4", biteDamageType),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Ant_Giant_Soldier].Add(BuildData("Improved Grab",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Ant_Giant_Soldier].Add(BuildData("Acid Sting",
                damageHelper.BuildEntries(
                    "1d4", stingDamageType, string.Empty,
                    "1d4", FeatConstants.Foci.Elements.Acid),
                string.Empty, 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Ant_Giant_Queen].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Ant_Giant_Queen].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Ape].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Ape].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Ape_Dire].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Ape_Dire].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Ape_Dire].Add(BuildData("Rend", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.Aranea].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Aranea].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Strength, "Initial",
                    "2d6", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            testCases[CreatureConstants.Aranea].Add(BuildData("Web", string.Empty, string.Empty, 0, "ranged, extraordinary ability", 6, FeatConstants.Frequencies.Day, false, true, true, true));
            testCases[CreatureConstants.Aranea].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Arrowhawk_Juvenile].Add(BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Arrowhawk_Juvenile].Add(BuildData("Electricity ray",
                damageHelper.BuildEntries("2d6", FeatConstants.Foci.Elements.Electricity),
                string.Empty, 0, "ranged touch, supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Arrowhawk_Adult].Add(BuildData("Bite",
                damageHelper.BuildEntries("1d8", biteDamageType),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Arrowhawk_Adult].Add(BuildData("Electricity ray",
                damageHelper.BuildEntries("2d8", FeatConstants.Foci.Elements.Electricity),
                string.Empty, 0, "ranged touch, supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Arrowhawk_Elder].Add(BuildData("Bite",
                damageHelper.BuildEntries("2d6", biteDamageType),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Arrowhawk_Elder].Add(BuildData("Electricity ray",
                damageHelper.BuildEntries("2d8", FeatConstants.Foci.Elements.Electricity),
                string.Empty, 0, "ranged touch, supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.AssassinVine].Add(BuildData("Slam", damageHelper.BuildEntries("1d6", slapSlamDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.AssassinVine].Add(BuildData("Constrict", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.AssassinVine].Add(BuildData("Entangle", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.AssassinVine].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Athach].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Athach].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, false, false, false));
            testCases[CreatureConstants.Athach].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Athach].Add(BuildData("Rock", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
            testCases[CreatureConstants.Athach].Add(BuildData("Rock", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "ranged", 2, FeatConstants.Frequencies.Round, false, true, false, false));
            testCases[CreatureConstants.Athach].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Strength, "Initial",
                    "2d6", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Avoral].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Avoral].Add(BuildData("Wing", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Avoral].Add(BuildData("Fear Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Avoral].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Azer].Add(BuildData(AttributeConstants.Melee,
                string.Empty,
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Azer].Add(BuildData(AttributeConstants.Ranged,
                string.Empty,
                string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Azer].Add(BuildData("Unarmed Strike",
                damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Azer].Add(BuildData("Heat",
                damageHelper.BuildEntries("1", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));

            testCases[CreatureConstants.Babau].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Babau].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Babau].Add(BuildData("Sneak Attack", damageHelper.BuildEntries("2d6", string.Empty), string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Babau].Add(BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
            testCases[CreatureConstants.Babau].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Baboon].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Badger].Add(BuildData("Claw", damageHelper.BuildEntries("1d2", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Badger].Add(BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Badger].Add(BuildData("Rage", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Badger_Dire].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Badger_Dire].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Badger_Dire].Add(BuildData("Rage", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Balor].Add(BuildData(AttributeConstants.Melee,
                string.Empty,
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Balor].Add(BuildData(AttributeConstants.Melee,
                string.Empty,
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, false, false));
            testCases[CreatureConstants.Balor].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d10", slapSlamDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Balor].Add(BuildData("Death Throes",
                damageHelper.BuildEntries("100", string.Empty),
                string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Life, false, true, false, true,
                SaveConstants.Reflex, AbilityConstants.Constitution));
            testCases[CreatureConstants.Balor].Add(BuildData("Entangle",
                string.Empty,
                string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, false, false, true));
            testCases[CreatureConstants.Balor].Add(BuildData("Summon Demon",
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
            testCases[CreatureConstants.Balor].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.BarbedDevil_Hamatula].Add(BuildData("Claw",
                damageHelper.BuildEntries("2d8", clawDamageType),
                "fear", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.BarbedDevil_Hamatula].Add(BuildData("Fear",
                string.Empty,
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
            testCases[CreatureConstants.BarbedDevil_Hamatula].Add(BuildData("Improved Grab",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.BarbedDevil_Hamatula].Add(BuildData("Impale",
                damageHelper.BuildEntries("3d8", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.BarbedDevil_Hamatula].Add(BuildData("Summon Devil",
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
            testCases[CreatureConstants.BarbedDevil_Hamatula].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Barghest].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Barghest].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Barghest].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Barghest].Add(BuildData("Feed", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.Barghest_Greater].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Barghest_Greater].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Barghest_Greater].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Barghest_Greater].Add(BuildData("Feed", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.Basilisk].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Basilisk].Add(BuildData("Petrifying Gaze", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));

            testCases[CreatureConstants.Basilisk_Greater].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Basilisk_Greater].Add(BuildData("Petrifying Gaze", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));

            testCases[CreatureConstants.Bat].Add(new[] { None });

            testCases[CreatureConstants.Bat_Dire].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Bat_Swarm].Add(BuildData("Swarm",
                damageHelper.BuildEntries("1d6", string.Empty),
                string.Empty, 0, "swarm", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Bat_Swarm].Add(BuildData("Distraction",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            testCases[CreatureConstants.Bat_Swarm].Add(BuildData("Wounding",
                damageHelper.BuildEntries("1", string.Empty),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));

            testCases[CreatureConstants.Bear_Black].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Bear_Black].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Bear_Brown].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Bear_Brown].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Bear_Brown].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Bear_Dire].Add(BuildData("Claw", damageHelper.BuildEntries("2d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Bear_Dire].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Bear_Dire].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Bear_Polar].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Bear_Polar].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Bear_Polar].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.BeardedDevil_Barbazu].Add(BuildData(AttributeConstants.Melee,
                string.Empty,
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.BeardedDevil_Barbazu].Add(BuildData("Claw",
                damageHelper.BuildEntries("2d8", clawDamageType),
                "Infernal Wound", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.BeardedDevil_Barbazu].Add(BuildData("Infernal Wound",
                damageHelper.BuildEntries("2", string.Empty),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, string.Empty, AbilityConstants.Constitution));
            testCases[CreatureConstants.BeardedDevil_Barbazu].Add(BuildData("Beard",
                damageHelper.BuildEntries("1d8", string.Empty),
                "Disease", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            testCases[CreatureConstants.BeardedDevil_Barbazu].Add(BuildData("Battle Frenzy",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 2, FeatConstants.Frequencies.Day, false, true, true, true));
            testCases[CreatureConstants.BeardedDevil_Barbazu].Add(BuildData("Summon Devil",
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
            testCases[CreatureConstants.BeardedDevil_Barbazu].Add(BuildData("Disease",
                string.Empty,
                "Devil Chills", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            testCases[CreatureConstants.BeardedDevil_Barbazu].Add(BuildData("Devil Chills",
                damageHelper.BuildEntries("1d4", AbilityConstants.Strength, "Incubation period 1d4 days"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            testCases[CreatureConstants.BeardedDevil_Barbazu].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Bebilith].Add(BuildData("Bite",
                damageHelper.BuildEntries("2d6", biteDamageType),
                "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Bebilith].Add(BuildData("Claw",
                damageHelper.BuildEntries("2d6", clawDamageType),
                string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Bebilith].Add(BuildData("Web",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 4, FeatConstants.Frequencies.Day, false, true, true, true, string.Empty, AbilityConstants.Constitution));
            testCases[CreatureConstants.Bebilith].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "2d6", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            testCases[CreatureConstants.Bebilith].Add(BuildData("Rend Armor",
                damageHelper.BuildEntries("4d6", string.Empty),
                string.Empty, 2, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Bebilith].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Bee_Giant].Add(BuildData("Sting", damageHelper.BuildEntries("1d4", stingDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Life, true, true, true, false));
            testCases[CreatureConstants.Bee_Giant].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Constitution, "Initial",
                    "1d4", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Behir].Add(BuildData("Bite",
                damageHelper.BuildEntries("2d4", biteDamageType),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Behir].Add(BuildData("Breath Weapon",
                damageHelper.BuildEntries("7d6", FeatConstants.Foci.Elements.Electricity),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
            testCases[CreatureConstants.Behir].Add(BuildData("Constrict",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Behir].Add(BuildData("Improved Grab",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Behir].Add(BuildData("Rake",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 0.5, "extraordinary ability", 6, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.Behir].Add(BuildData("Swallow Whole",
                damageHelper.BuildEntries(
                    "2d8+8", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "8", FeatConstants.Foci.Elements.Acid),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Beholder].Add(BuildData("Bite", damageHelper.BuildEntries("2d4", biteDamageType), string.Empty, .5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Beholder].Add(BuildData("Eye ray", string.Empty, SpellConstants.CharmMonster, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
            testCases[CreatureConstants.Beholder].Add(BuildData("Eye ray", string.Empty, SpellConstants.CharmPerson, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
            testCases[CreatureConstants.Beholder].Add(BuildData("Eye ray", string.Empty, SpellConstants.InflictModerateWounds, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
            testCases[CreatureConstants.Beholder].Add(BuildData("Eye ray", string.Empty, SpellConstants.Disintegrate, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
            testCases[CreatureConstants.Beholder].Add(BuildData("Eye ray", string.Empty, SpellConstants.Fear, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
            testCases[CreatureConstants.Beholder].Add(BuildData("Eye ray", string.Empty, SpellConstants.FingerOfDeath, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
            testCases[CreatureConstants.Beholder].Add(BuildData("Eye ray", string.Empty, SpellConstants.FleshToStone, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
            testCases[CreatureConstants.Beholder].Add(BuildData("Eye ray", string.Empty, SpellConstants.Sleep, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
            testCases[CreatureConstants.Beholder].Add(BuildData("Eye ray", string.Empty, SpellConstants.Slow, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
            testCases[CreatureConstants.Beholder].Add(BuildData("Eye ray", string.Empty, SpellConstants.Telekinesis, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));

            testCases[CreatureConstants.Beholder_Gauth].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, .5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Beholder_Gauth].Add(BuildData("Eye ray", string.Empty, SpellConstants.Sleep, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
            testCases[CreatureConstants.Beholder_Gauth].Add(BuildData("Eye ray", string.Empty, SpellConstants.InflictModerateWounds, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
            testCases[CreatureConstants.Beholder_Gauth].Add(BuildData("Eye ray", string.Empty, SpellConstants.DispelMagic, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Beholder_Gauth].Add(BuildData("Eye ray", string.Empty, SpellConstants.ScorchingRay, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Beholder_Gauth].Add(BuildData("Eye ray", string.Empty, "Paralysis", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
            testCases[CreatureConstants.Beholder_Gauth].Add(BuildData("Eye ray", string.Empty, SpellConstants.RayOfExhaustion, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Belker].Add(BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Belker].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Belker].Add(BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Belker].Add(BuildData("Smoke Claw", damageHelper.BuildEntries("3d4", clawDamageType), string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.Bison].Add(BuildData("Gore", damageHelper.BuildEntries("1d8", goreDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Bison].Add(BuildData("Stampede", string.Empty, "1d12 per 5 bison in herd", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, SaveConstants.Reflex, AbilityConstants.Strength));

            testCases[CreatureConstants.BlackPudding].Add(BuildData("Slam",
                damageHelper.BuildEntries(
                    "2d6", slapSlamDamageType, string.Empty,
                    "2d6", FeatConstants.Foci.Elements.Acid),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.BlackPudding].Add(BuildData("Acid", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
            testCases[CreatureConstants.BlackPudding].Add(BuildData("Constrict",
                damageHelper.BuildEntries(
                    "2d6", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "2d6", FeatConstants.Foci.Elements.Acid),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.BlackPudding].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.BlackPudding_Elder].Add(BuildData("Slam",
                damageHelper.BuildEntries(
                    "3d6", slapSlamDamageType, string.Empty,
                    "3d6", FeatConstants.Foci.Elements.Acid),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.BlackPudding_Elder].Add(BuildData("Acid", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
            testCases[CreatureConstants.BlackPudding_Elder].Add(BuildData("Constrict",
                damageHelper.BuildEntries(
                    "2d8", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "2d6", FeatConstants.Foci.Elements.Acid),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.BlackPudding_Elder].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.BlinkDog].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.BlinkDog].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Boar].Add(BuildData("Gore", damageHelper.BuildEntries("1d8", goreDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Boar].Add(BuildData("Ferocity", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Boar_Dire].Add(BuildData("Gore", damageHelper.BuildEntries("1d8", goreDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Boar_Dire].Add(BuildData("Ferocity", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Bodak].Add(BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Bodak].Add(BuildData("Death Gaze", string.Empty, "Death", 1.5, "melee", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));

            testCases[CreatureConstants.BombardierBeetle_Giant].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.BombardierBeetle_Giant].Add(BuildData("Acid Spray",
                damageHelper.BuildEntries("1d4", FeatConstants.Foci.Elements.Acid),
                string.Empty, 2, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, false, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.BoneDevil_Osyluth].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.BoneDevil_Osyluth].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.BoneDevil_Osyluth].Add(BuildData("Sting", damageHelper.BuildEntries("3d4", AttributeConstants.DamageTypes.Piercing), "Poison", 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.BoneDevil_Osyluth].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.BoneDevil_Osyluth].Add(BuildData("Fear Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
            testCases[CreatureConstants.BoneDevil_Osyluth].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Strength, "Initial",
                    "2d6", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            testCases[CreatureConstants.BoneDevil_Osyluth].Add(BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            testCases[CreatureConstants.Bralani].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Bralani].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Bralani].Add(BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Bralani].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Bralani].Add(BuildData("Whirlwind blast",
                damageHelper.BuildEntries("3d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Bugbear].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Bugbear].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Bugbear].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Bulette].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Bulette].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Bulette].Add(BuildData("Leap", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Camel_Bactrian].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Camel_Dromedary].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.CarrionCrawler].Add(BuildData("Tentacle",
                damageHelper.BuildEntries("0", tentacleDamageType),
                "Paralysis", 0, "melee", 8, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.CarrionCrawler].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.CarrionCrawler].Add(BuildData("Paralysis", string.Empty, "paralyzed for 2d4 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Cat].Add(BuildData("Claw", damageHelper.BuildEntries("1d2", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Cat].Add(BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Centaur].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Centaur].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Centaur].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Centaur].Add(BuildData("Hoof",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Centipede_Monstrous_Tiny].Add(BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Centipede_Monstrous_Tiny].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1", AbilityConstants.Dexterity, "Initial",
                    "1", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Centipede_Monstrous_Small].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Centipede_Monstrous_Small].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d2", AbilityConstants.Dexterity, "Initial",
                    "1d2", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Centipede_Monstrous_Medium].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Centipede_Monstrous_Medium].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d3", AbilityConstants.Dexterity, "Initial",
                    "1d3", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Centipede_Monstrous_Large].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Centipede_Monstrous_Large].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Dexterity, "Initial",
                    "1d4", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Centipede_Monstrous_Huge].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Centipede_Monstrous_Huge].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Dexterity, "Initial",
                    "1d6", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Centipede_Monstrous_Gargantuan].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Centipede_Monstrous_Gargantuan].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d8", AbilityConstants.Dexterity, "Initial",
                    "1d8", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Centipede_Monstrous_Colossal].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Centipede_Monstrous_Colossal].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "2d6", AbilityConstants.Dexterity, "Initial",
                    "2d6", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Centipede_Swarm].Add(BuildData("Swarm", damageHelper.BuildEntries("2d6"), "Poison", 0, "swarm", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Centipede_Swarm].Add(BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            testCases[CreatureConstants.Centipede_Swarm].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Dexterity, "Initial",
                    "1d4", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.ChainDevil_Kyton].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.ChainDevil_Kyton].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.ChainDevil_Kyton].Add(BuildData("Dancing Chains", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
            testCases[CreatureConstants.ChainDevil_Kyton].Add(BuildData("Unnerving Gaze", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));

            testCases[CreatureConstants.ChaosBeast].Add(BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), "Corporeal Instability", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.ChaosBeast].Add(BuildData("Corporeal Instability", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Cheetah].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Cheetah].Add(BuildData("Claw", damageHelper.BuildEntries("1d2", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Cheetah].Add(BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.Chimera_Black].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Chimera_Black].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Chimera_Black].Add(BuildData("Gore", damageHelper.BuildEntries("1d8", goreDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Chimera_Black].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Chimera_Black].Add(BuildData("Breath weapon", damageHelper.BuildEntries("3d8", FeatConstants.Foci.Elements.Acid), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Chimera_Blue].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Chimera_Blue].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Chimera_Blue].Add(BuildData("Gore", damageHelper.BuildEntries("1d8", goreDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Chimera_Blue].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Chimera_Blue].Add(BuildData("Breath weapon", damageHelper.BuildEntries("3d8", FeatConstants.Foci.Elements.Electricity), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Chimera_Green].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Chimera_Green].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Chimera_Green].Add(BuildData("Gore", damageHelper.BuildEntries("1d8", goreDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Chimera_Green].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Chimera_Green].Add(BuildData("Breath weapon", damageHelper.BuildEntries("3d8", FeatConstants.Foci.Elements.Acid), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Chimera_Red].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Chimera_Red].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Chimera_Red].Add(BuildData("Gore", damageHelper.BuildEntries("1d8", goreDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Chimera_Red].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Chimera_Red].Add(BuildData("Breath weapon", damageHelper.BuildEntries("3d8", FeatConstants.Foci.Elements.Fire), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Chimera_White].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Chimera_White].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Chimera_White].Add(BuildData("Gore", damageHelper.BuildEntries("1d8", goreDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Chimera_White].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Chimera_White].Add(BuildData("Breath weapon", damageHelper.BuildEntries("3d8", FeatConstants.Foci.Elements.Cold), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Choker].Add(BuildData("Tentacle", damageHelper.BuildEntries("1d3", tentacleDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Choker].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Choker].Add(BuildData("Constrict", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Chuul].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Chuul].Add(BuildData("Constrict", damageHelper.BuildEntries("3d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Chuul].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Chuul].Add(BuildData("Paralytic Tentacles", damageHelper.BuildEntries("1d8", tentacleDamageType), "6 round paralysis", 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Cloaker].Add(BuildData("Tail slap", damageHelper.BuildEntries("1d6", slapSlamDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Cloaker].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Cloaker].Add(BuildData("Moan", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma));
            testCases[CreatureConstants.Cloaker].Add(BuildData("Engulf", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Cockatrice].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), "Petrification", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Cockatrice].Add(BuildData("Petrification", string.Empty, string.Empty, 1, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Couatl].Add(BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Couatl].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "2d4", AbilityConstants.Strength, "Initial",
                    "4d4", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Couatl].Add(BuildData("Constrict", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Couatl].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Couatl].Add(BuildData(FeatConstants.SpecialQualities.Psionic, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Couatl].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Couatl].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Criosphinx].Add(BuildData("Gore", damageHelper.BuildEntries("2d6", goreDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Criosphinx].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Criosphinx].Add(BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.Criosphinx].Add(BuildData("Rake", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.Crocodile].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Crocodile].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d12", slapSlamDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Crocodile].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Crocodile_Giant].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Crocodile_Giant].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d12", slapSlamDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Crocodile_Giant].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Cryohydra_5Heads].Add(BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 5, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Cryohydra_5Heads].Add(BuildData("Breath weapon",
                damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Cold, "Per living head"),
                string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Cryohydra_6Heads].Add(BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 6, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Cryohydra_6Heads].Add(BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Cold, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Cryohydra_7Heads].Add(BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 7, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Cryohydra_7Heads].Add(BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Cold, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Cryohydra_8Heads].Add(BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 8, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Cryohydra_8Heads].Add(BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Cold, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Cryohydra_9Heads].Add(BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 9, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Cryohydra_9Heads].Add(BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Cold, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Cryohydra_10Heads].Add(BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 10, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Cryohydra_10Heads].Add(BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Cold, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Cryohydra_11Heads].Add(BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 11, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Cryohydra_11Heads].Add(BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Cold, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Cryohydra_12Heads].Add(BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 12, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Cryohydra_12Heads].Add(BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Cold, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Darkmantle].Add(BuildData("Slam", damageHelper.BuildEntries("1d4", slapSlamDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Darkmantle].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Darkmantle].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Darkmantle].Add(BuildData("Constrict", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Deinonychus].Add(BuildData("Talons", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Deinonychus].Add(BuildData("Foreclaw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Deinonychus].Add(BuildData("Bite", damageHelper.BuildEntries("2d4", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Deinonychus].Add(BuildData("Pounce", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.Delver].Add(BuildData("Slam",
                damageHelper.BuildEntries(
                    "1d6", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "2d6", FeatConstants.Foci.Elements.Acid),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Delver].Add(BuildData("Corrosive Slime", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Delver].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Derro].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Derro].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Derro].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Derro].Add(BuildData("Poison use", string.Empty, "Greenblood Oil or Monstrous Spider Venom", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Derro].Add(BuildData("Greenblood Oil",
                damageHelper.BuildEntries(
                    "1", AbilityConstants.Constitution, "Initial",
                    "1d2", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, false, true, true, save: SaveConstants.Fortitude, saveDcBonus: 3));
            testCases[CreatureConstants.Derro].Add(BuildData("Monstrous Spider Venom",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Strength, "Initial",
                    "1d4", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, false, true, true, save: SaveConstants.Fortitude, saveDcBonus: 2));
            testCases[CreatureConstants.Derro].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Derro].Add(BuildData("Sneak Attack", damageHelper.BuildEntries("1d6"), string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Derro_Sane].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Derro_Sane].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Derro_Sane].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Derro_Sane].Add(BuildData("Poison use", string.Empty, "Greenblood Oil or Monstrous Spider Venom", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Derro_Sane].Add(BuildData("Greenblood Oil",
                damageHelper.BuildEntries(
                    "1", AbilityConstants.Constitution, "Initial",
                    "1d2", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, false, true, true, save: SaveConstants.Fortitude, saveDcBonus: 3));
            testCases[CreatureConstants.Derro_Sane].Add(BuildData("Monstrous Spider Venom",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Strength, "Initial",
                    "1d4", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, false, true, true, save: SaveConstants.Fortitude, saveDcBonus: 2));
            testCases[CreatureConstants.Derro_Sane].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Derro_Sane].Add(BuildData("Sneak Attack", damageHelper.BuildEntries("1d6"), string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Destrachan].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Destrachan].Add(BuildData("Destructive harmonics", string.Empty, string.Empty, 1, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Charisma));

            testCases[CreatureConstants.Devourer].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Devourer].Add(BuildData("Energy Drain", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Charisma));
            testCases[CreatureConstants.Devourer].Add(BuildData("Trap Essence", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Devourer].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Digester].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Digester].Add(BuildData("Acid Spray (Cone)",
                damageHelper.BuildEntries("4d8", FeatConstants.Foci.Elements.Acid),
                string.Empty, 0, "extraordinary ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Digester].Add(BuildData("Acid Spray (Stream)",
                damageHelper.BuildEntries("8d8", FeatConstants.Foci.Elements.Acid),
                string.Empty, 0, "extraordinary ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.DisplacerBeast].Add(BuildData("Tentacle", damageHelper.BuildEntries("1d6", tentacleDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.DisplacerBeast].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.DisplacerBeast_PackLord].Add(BuildData("Tentacle", damageHelper.BuildEntries("1d8", tentacleDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.DisplacerBeast_PackLord].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Djinni].Add(BuildData("Slam", damageHelper.BuildEntries("1d8", slapSlamDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Djinni].Add(BuildData("Air mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Djinni].Add(BuildData("Whirlwind", string.Empty, string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex, saveDcBonus: 3));
            testCases[CreatureConstants.Djinni].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Djinni_Noble].Add(BuildData("Slam", damageHelper.BuildEntries("1d8", slapSlamDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Djinni_Noble].Add(BuildData("Air mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Djinni_Noble].Add(BuildData("Whirlwind", string.Empty, string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex, saveDcBonus: 3));
            testCases[CreatureConstants.Djinni_Noble].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Dog].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Dog_Riding].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Donkey].Add(BuildData("Bite", damageHelper.BuildEntries("1d2", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Doppelganger].Add(BuildData("Slam", damageHelper.BuildEntries("1d6", slapSlamDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Doppelganger].Add(BuildData("Detect Thoughts", string.Empty, string.Empty, 1, "supernatural ability", 0, FeatConstants.Frequencies.Constant, false, true, true, true));
            testCases[CreatureConstants.Doppelganger].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.DragonTurtle].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.DragonTurtle].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.DragonTurtle].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("12d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
            testCases[CreatureConstants.DragonTurtle].Add(BuildData("Capsize", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //Tiny
            testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("2d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //small
            testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("4d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //medium
            testCases[CreatureConstants.Dragon_Black_Young].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Black_Young].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_Young].Add(BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_Young].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("6d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //medium
            testCases[CreatureConstants.Dragon_Black_Juvenile].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Black_Juvenile].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_Juvenile].Add(BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_Juvenile].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("8d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Black_Juvenile].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //large
            testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("10d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //large
            testCases[CreatureConstants.Dragon_Black_Adult].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Black_Adult].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_Adult].Add(BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_Adult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_Adult].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("12d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Black_Adult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Black_Adult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Black_Adult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("14d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Black_Old].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Black_Old].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_Old].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_Old].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_Old].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Black_Old].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("16d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Black_Old].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Black_Old].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Black_Old].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Black_VeryOld].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Black_VeryOld].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_VeryOld].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_VeryOld].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_VeryOld].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Black_VeryOld].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("18d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Black_VeryOld].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Black_VeryOld].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Black_VeryOld].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Black_Ancient].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Black_Ancient].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_Ancient].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_Ancient].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_Ancient].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Black_Ancient].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("20d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Black_Ancient].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Black_Ancient].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Black_Ancient].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            testCases[CreatureConstants.Dragon_Black_Wyrm].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Black_Wyrm].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_Wyrm].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_Wyrm].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_Wyrm].Add(BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Black_Wyrm].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Black_Wyrm].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("22d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Black_Wyrm].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Black_Wyrm].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Black_Wyrm].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("24d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //small
            testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("2d8", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //medium
            testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("4d8", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //medium
            testCases[CreatureConstants.Dragon_Blue_Young].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Blue_Young].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_Young].Add(BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_Young].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //large
            testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("8d8", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //large
            testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("10d8", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Blue_Adult].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Blue_Adult].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_Adult].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_Adult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_Adult].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Blue_Adult].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("12d8", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Blue_Adult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Blue_Adult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Blue_Adult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("14d8", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Blue_Old].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Blue_Old].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_Old].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_Old].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_Old].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Blue_Old].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("16d8", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Blue_Old].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Blue_Old].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Blue_Old].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("18d8", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            testCases[CreatureConstants.Dragon_Blue_Ancient].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Blue_Ancient].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_Ancient].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_Ancient].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_Ancient].Add(BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Blue_Ancient].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Blue_Ancient].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("20d8", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Blue_Ancient].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Blue_Ancient].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Blue_Ancient].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("22d8", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("24d8", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //small
            testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("2d6", FeatConstants.Foci.Elements.Acid, "Gas"), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //medium
            testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("4d6", FeatConstants.Foci.Elements.Acid, "Gas"), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //medium
            testCases[CreatureConstants.Dragon_Green_Young].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Green_Young].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_Young].Add(BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_Young].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("6d6", FeatConstants.Foci.Elements.Acid, "Gas"), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //large
            testCases[CreatureConstants.Dragon_Green_Juvenile].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Green_Juvenile].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_Juvenile].Add(BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_Juvenile].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_Juvenile].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("8d6", FeatConstants.Foci.Elements.Acid, "Gas"), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Green_Juvenile].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //large
            testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("10d6", FeatConstants.Foci.Elements.Acid, "Gas"), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Green_Adult].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Green_Adult].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_Adult].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_Adult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_Adult].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Green_Adult].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("12d6", FeatConstants.Foci.Elements.Acid, "Gas"), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Green_Adult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Green_Adult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Green_Adult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("14d6", FeatConstants.Foci.Elements.Acid, "Gas"), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Green_Old].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Green_Old].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_Old].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_Old].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_Old].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Green_Old].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("16d6", FeatConstants.Foci.Elements.Acid, "Gas"), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Green_Old].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Green_Old].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Green_Old].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Green_VeryOld].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Green_VeryOld].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_VeryOld].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_VeryOld].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_VeryOld].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Green_VeryOld].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("18d6", FeatConstants.Foci.Elements.Acid, "Gas"), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Green_VeryOld].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Green_VeryOld].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Green_VeryOld].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            testCases[CreatureConstants.Dragon_Green_Ancient].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Green_Ancient].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_Ancient].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_Ancient].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_Ancient].Add(BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Green_Ancient].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Green_Ancient].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("20d6", FeatConstants.Foci.Elements.Acid, "Gas"), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Green_Ancient].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Green_Ancient].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Green_Ancient].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            testCases[CreatureConstants.Dragon_Green_Wyrm].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Green_Wyrm].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_Wyrm].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_Wyrm].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_Wyrm].Add(BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Green_Wyrm].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Green_Wyrm].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("22d6", FeatConstants.Foci.Elements.Acid, "Gas"), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Green_Wyrm].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Green_Wyrm].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Green_Wyrm].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("24d6", FeatConstants.Foci.Elements.Acid, "Gas"), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //medium
            testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("2d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //large
            testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("4d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //large
            testCases[CreatureConstants.Dragon_Red_Young].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Red_Young].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_Young].Add(BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_Young].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_Young].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("6d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Red_Young].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //large
            testCases[CreatureConstants.Dragon_Red_Juvenile].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Red_Juvenile].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_Juvenile].Add(BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_Juvenile].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_Juvenile].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("8d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Red_Juvenile].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Red_Juvenile].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //huge
            testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("10d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Red_Adult].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Red_Adult].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_Adult].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_Adult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_Adult].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Red_Adult].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("12d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Red_Adult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Red_Adult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Red_Adult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("14d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            testCases[CreatureConstants.Dragon_Red_Old].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Red_Old].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_Old].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_Old].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_Old].Add(BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Red_Old].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Red_Old].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("16d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Red_Old].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Red_Old].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Red_Old].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            testCases[CreatureConstants.Dragon_Red_VeryOld].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Red_VeryOld].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_VeryOld].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_VeryOld].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_VeryOld].Add(BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Red_VeryOld].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Red_VeryOld].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("18d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Red_VeryOld].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Red_VeryOld].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Red_VeryOld].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            testCases[CreatureConstants.Dragon_Red_Ancient].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Red_Ancient].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_Ancient].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_Ancient].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_Ancient].Add(BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Red_Ancient].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Red_Ancient].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("20d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Red_Ancient].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Red_Ancient].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Red_Ancient].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            testCases[CreatureConstants.Dragon_Red_Wyrm].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Red_Wyrm].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_Wyrm].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_Wyrm].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_Wyrm].Add(BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Red_Wyrm].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Red_Wyrm].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("22d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Red_Wyrm].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Red_Wyrm].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Red_Wyrm].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //colossal
            testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(BuildData("Bite", damageHelper.BuildEntries("4d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(BuildData("Claw", damageHelper.BuildEntries("4d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(BuildData("Wing", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(BuildData("Tail Slap", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(BuildData("Crush", damageHelper.BuildEntries("4d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("24d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //tiny
            testCases[CreatureConstants.Dragon_White_Wyrmling].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_White_Wyrmling].Add(BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_Wyrmling].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("1d6", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //small
            testCases[CreatureConstants.Dragon_White_VeryYoung].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_White_VeryYoung].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_VeryYoung].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("2d6", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //medium
            testCases[CreatureConstants.Dragon_White_Young].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_White_Young].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_Young].Add(BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_Young].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //medium
            testCases[CreatureConstants.Dragon_White_Juvenile].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_White_Juvenile].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_Juvenile].Add(BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_Juvenile].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("4d6", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_White_Juvenile].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //large
            testCases[CreatureConstants.Dragon_White_YoungAdult].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_White_YoungAdult].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_YoungAdult].Add(BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_YoungAdult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_YoungAdult].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("5d6", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_White_YoungAdult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_White_YoungAdult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //large
            testCases[CreatureConstants.Dragon_White_Adult].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_White_Adult].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_Adult].Add(BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_Adult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_Adult].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("6d6", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_White_Adult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_White_Adult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_White_Adult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_White_MatureAdult].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_White_MatureAdult].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_MatureAdult].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_MatureAdult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_MatureAdult].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_White_MatureAdult].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("7d6", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_White_MatureAdult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_White_MatureAdult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_White_MatureAdult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_White_Old].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_White_Old].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_Old].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_Old].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_Old].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_White_Old].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("8d6", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_White_Old].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_White_Old].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_White_Old].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_White_VeryOld].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_White_VeryOld].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_VeryOld].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_VeryOld].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_VeryOld].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_White_VeryOld].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("9d6", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_White_VeryOld].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_White_VeryOld].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_White_VeryOld].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_White_Ancient].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_White_Ancient].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_Ancient].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_Ancient].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_Ancient].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_White_Ancient].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("10d6", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_White_Ancient].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_White_Ancient].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_White_Ancient].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            testCases[CreatureConstants.Dragon_White_Wyrm].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_White_Wyrm].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_Wyrm].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_Wyrm].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_Wyrm].Add(BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_White_Wyrm].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_White_Wyrm].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("11d6", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_White_Wyrm].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_White_Wyrm].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_White_Wyrm].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(BuildData("Breath Weapon", damageHelper.BuildEntries("12d6", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //tiny
            testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("1d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+1 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //small
            testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("2d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+2 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //medium
            testCases[CreatureConstants.Dragon_Brass_Young].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Brass_Young].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_Young].Add(BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_Young].Add(BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Brass_Young].Add(BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+3 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            testCases[CreatureConstants.Dragon_Brass_Young].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Brass_Young].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //medium
            testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("4d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+4 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //large
            testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("5d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+5 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //large
            testCases[CreatureConstants.Dragon_Brass_Adult].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Brass_Adult].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_Adult].Add(BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_Adult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_Adult].Add(BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("6d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Brass_Adult].Add(BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+6 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            testCases[CreatureConstants.Dragon_Brass_Adult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Brass_Adult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Brass_Adult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("7d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+7 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Brass_Old].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Brass_Old].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_Old].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_Old].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_Old].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Brass_Old].Add(BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("8d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Brass_Old].Add(BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+8 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            testCases[CreatureConstants.Dragon_Brass_Old].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Brass_Old].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Brass_Old].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("9d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+9 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Brass_Ancient].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Brass_Ancient].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_Ancient].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_Ancient].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_Ancient].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Brass_Ancient].Add(BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("10d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Brass_Ancient].Add(BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+10 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            testCases[CreatureConstants.Dragon_Brass_Ancient].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Brass_Ancient].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Brass_Ancient].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("11d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+11 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("12d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+12 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //small
            testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(BuildData("Breath Weapon (electricity)", damageHelper.BuildEntries("2d6", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+1 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //medium
            testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(BuildData("Breath Weapon (electricity)", damageHelper.BuildEntries("4d6", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+2 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //medium
            testCases[CreatureConstants.Dragon_Bronze_Young].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Bronze_Young].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_Young].Add(BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_Young].Add(BuildData("Breath Weapon (electricity)", damageHelper.BuildEntries("6d6", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Bronze_Young].Add(BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+3 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            testCases[CreatureConstants.Dragon_Bronze_Young].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Bronze_Young].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //large
            testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(BuildData("Breath Weapon (electricity)", damageHelper.BuildEntries("8d6", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+4 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //large
            testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(BuildData("Breath Weapon (electricity)", damageHelper.BuildEntries("10d6", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+5 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Bronze_Adult].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Bronze_Adult].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_Adult].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_Adult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_Adult].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Bronze_Adult].Add(BuildData("Breath Weapon (electricity)", damageHelper.BuildEntries("12d6", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Bronze_Adult].Add(BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+6 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            testCases[CreatureConstants.Dragon_Bronze_Adult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Bronze_Adult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Bronze_Adult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(BuildData("Breath Weapon (electricity)", damageHelper.BuildEntries("14d6", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+7 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Bronze_Old].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Bronze_Old].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_Old].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_Old].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_Old].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Bronze_Old].Add(BuildData("Breath Weapon (electricity)", damageHelper.BuildEntries("16d6", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Bronze_Old].Add(BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+8 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            testCases[CreatureConstants.Dragon_Bronze_Old].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Bronze_Old].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Bronze_Old].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(BuildData("Breath Weapon (electricity)", damageHelper.BuildEntries("18d6", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+9 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(BuildData("Breath Weapon (electricity)", damageHelper.BuildEntries("20d6", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+10 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(BuildData("Breath Weapon (electricity)", damageHelper.BuildEntries("22d6", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+11 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(BuildData("Breath Weapon (electricity)", damageHelper.BuildEntries("24d6", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+12 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //tiny
            testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(BuildData("Breath Weapon (acid)", damageHelper.BuildEntries("2d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+1 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            //small
            testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(BuildData("Breath Weapon (acid)", damageHelper.BuildEntries("4d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+2 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            //medium
            testCases[CreatureConstants.Dragon_Copper_Young].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Copper_Young].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_Young].Add(BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_Young].Add(BuildData("Breath Weapon (acid)", damageHelper.BuildEntries("6d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Copper_Young].Add(BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+3 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Copper_Young].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //medium
            testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(BuildData("Breath Weapon (acid)", damageHelper.BuildEntries("8d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+4 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //large
            testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(BuildData("Breath Weapon (acid)", damageHelper.BuildEntries("10d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+5 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //large
            testCases[CreatureConstants.Dragon_Copper_Adult].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Copper_Adult].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_Adult].Add(BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_Adult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_Adult].Add(BuildData("Breath Weapon (acid)", damageHelper.BuildEntries("12d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Copper_Adult].Add(BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+6 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Copper_Adult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Copper_Adult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Copper_Adult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(BuildData("Breath Weapon (acid)", damageHelper.BuildEntries("14d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+7 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Copper_Old].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Copper_Old].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_Old].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_Old].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_Old].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Copper_Old].Add(BuildData("Breath Weapon (acid)", damageHelper.BuildEntries("16d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Copper_Old].Add(BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+8 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Copper_Old].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Copper_Old].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Copper_Old].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(BuildData("Breath Weapon (acid)", damageHelper.BuildEntries("18d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+9 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Copper_Ancient].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Copper_Ancient].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_Ancient].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_Ancient].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_Ancient].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Copper_Ancient].Add(BuildData("Breath Weapon (acid)", damageHelper.BuildEntries("20d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Copper_Ancient].Add(BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+10 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Copper_Ancient].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Copper_Ancient].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Copper_Ancient].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(BuildData("Breath Weapon (acid)", damageHelper.BuildEntries("22d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+11 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(BuildData("Breath Weapon (acid)", damageHelper.BuildEntries("24d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+12 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //medium
            testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("2d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(BuildData("Breath Weapon (weakening gas)", damageHelper.BuildEntries("1", AbilityConstants.Strength), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            //large
            testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("4d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(BuildData("Breath Weapon (weakening gas)", damageHelper.BuildEntries("2", AbilityConstants.Strength), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            //large
            testCases[CreatureConstants.Dragon_Gold_Young].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Gold_Young].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_Young].Add(BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_Young].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_Young].Add(BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("6d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Gold_Young].Add(BuildData("Breath Weapon (weakening gas)", damageHelper.BuildEntries("3", AbilityConstants.Strength), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Gold_Young].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //large
            testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("8d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(BuildData("Breath Weapon (weakening gas)", damageHelper.BuildEntries("4", AbilityConstants.Strength), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //huge
            testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("10d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(BuildData("Breath Weapon (weakening gas)", damageHelper.BuildEntries("5", AbilityConstants.Strength), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Gold_Adult].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Gold_Adult].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_Adult].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_Adult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_Adult].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Gold_Adult].Add(BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("12d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Gold_Adult].Add(BuildData("Breath Weapon (weakening gas)", damageHelper.BuildEntries("6", AbilityConstants.Strength), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Gold_Adult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Gold_Adult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Gold_Adult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("14d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(BuildData("Breath Weapon (weakening gas)", damageHelper.BuildEntries("7", AbilityConstants.Strength), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            testCases[CreatureConstants.Dragon_Gold_Old].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Gold_Old].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_Old].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_Old].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_Old].Add(BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Gold_Old].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Gold_Old].Add(BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("16d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Gold_Old].Add(BuildData("Breath Weapon (weakening gas)", damageHelper.BuildEntries("8", AbilityConstants.Strength), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Gold_Old].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Gold_Old].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Gold_Old].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("18d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(BuildData("Breath Weapon (weakening gas)", damageHelper.BuildEntries("9", AbilityConstants.Strength), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            testCases[CreatureConstants.Dragon_Gold_Ancient].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Gold_Ancient].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_Ancient].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_Ancient].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_Ancient].Add(BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Gold_Ancient].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Gold_Ancient].Add(BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("20d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Gold_Ancient].Add(BuildData("Breath Weapon (weakening gas)", damageHelper.BuildEntries("10", AbilityConstants.Strength), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Gold_Ancient].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Gold_Ancient].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Gold_Ancient].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //colossal
            testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(BuildData("Bite", damageHelper.BuildEntries("4d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(BuildData("Claw", damageHelper.BuildEntries("4d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(BuildData("Wing", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(BuildData("Tail Slap", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(BuildData("Crush", damageHelper.BuildEntries("4d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("22d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(BuildData("Breath Weapon (weakening gas)", damageHelper.BuildEntries("11", AbilityConstants.Strength), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //colossal
            testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(BuildData("Bite", damageHelper.BuildEntries("4d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(BuildData("Claw", damageHelper.BuildEntries("4d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(BuildData("Wing", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(BuildData("Tail Slap", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(BuildData("Crush", damageHelper.BuildEntries("4d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("24d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(BuildData("Breath Weapon (weakening gas)", damageHelper.BuildEntries("12", AbilityConstants.Strength), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //small
            testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(BuildData("Breath Weapon (cold)", damageHelper.BuildEntries("2d8", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+1 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            //medium
            testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(BuildData("Breath Weapon (cold)", damageHelper.BuildEntries("4d8", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+2 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            //medium
            testCases[CreatureConstants.Dragon_Silver_Young].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Silver_Young].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_Young].Add(BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_Young].Add(BuildData("Breath Weapon (cold)", damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Silver_Young].Add(BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+3 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Silver_Young].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //large
            testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(BuildData("Breath Weapon (cold)", damageHelper.BuildEntries("8d8", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+4 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //large
            testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(BuildData("Breath Weapon (cold)", damageHelper.BuildEntries("10d8", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+5 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Silver_Adult].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Silver_Adult].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_Adult].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_Adult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_Adult].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Silver_Adult].Add(BuildData("Breath Weapon (cold)", damageHelper.BuildEntries("12d8", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Silver_Adult].Add(BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+6 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Silver_Adult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Silver_Adult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Silver_Adult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(BuildData("Breath Weapon (cold)", damageHelper.BuildEntries("14d8", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+7 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Silver_Old].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Silver_Old].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_Old].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_Old].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_Old].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Silver_Old].Add(BuildData("Breath Weapon (cold)", damageHelper.BuildEntries("16d8", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Silver_Old].Add(BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+8 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Silver_Old].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Silver_Old].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Silver_Old].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(BuildData("Breath Weapon (cold)", damageHelper.BuildEntries("18d8", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+9 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            testCases[CreatureConstants.Dragon_Silver_Ancient].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Silver_Ancient].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_Ancient].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_Ancient].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_Ancient].Add(BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Silver_Ancient].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Silver_Ancient].Add(BuildData("Breath Weapon (cold)", damageHelper.BuildEntries("20d8", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Silver_Ancient].Add(BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+10 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Silver_Ancient].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Silver_Ancient].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Silver_Ancient].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(BuildData("Breath Weapon (cold)", damageHelper.BuildEntries("22d8", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+11 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //colossal
            testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(BuildData("Bite", damageHelper.BuildEntries("4d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(BuildData("Claw", damageHelper.BuildEntries("4d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(BuildData("Wing", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(BuildData("Tail Slap", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(BuildData("Crush", damageHelper.BuildEntries("4d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(BuildData("Tail Sweep", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(BuildData("Breath Weapon (cold)", damageHelper.BuildEntries("24d8", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+12 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            testCases[CreatureConstants.Dragonne].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dragonne].Add(BuildData("Claw", damageHelper.BuildEntries("2d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dragonne].Add(BuildData("Pounce", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.Dragonne].Add(BuildData("Roar", string.Empty, string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));

            testCases[CreatureConstants.Dretch].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dretch].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Dretch].Add(BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
            testCases[CreatureConstants.Dretch].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Drider].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Drider].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Drider].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Drider].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Drider].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Drider].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Strength, "Initial",
                    "1d6", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Dryad].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Dryad].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Dryad].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dryad].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Dwarf_Deep].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Dwarf_Deep].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Dwarf_Deep].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Dwarf_Duergar].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Dwarf_Duergar].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Dwarf_Duergar].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Dwarf_Duergar].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Dwarf_Hill].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Dwarf_Hill].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Dwarf_Hill].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Dwarf_Mountain].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Dwarf_Mountain].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Dwarf_Mountain].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Eagle].Add(BuildData("Talons", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Eagle].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Eagle_Giant].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Eagle_Giant].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Efreeti].Add(BuildData("Slam", damageHelper.BuildEntries("1d8", slapSlamDamageType), "Heat", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Efreeti].Add(BuildData("Change Size", string.Empty, string.Empty, 0, "spell-like ability", 2, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Efreeti].Add(BuildData("Heat",
                damageHelper.BuildEntries("1d6", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, false, true, true, true));
            testCases[CreatureConstants.Efreeti].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Elasmosaurus].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Elemental_Air_Small].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elemental_Air_Small].Add(BuildData("Air mastery",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Elemental_Air_Small].Add(BuildData("Whirlwind",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Elemental_Air_Medium].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elemental_Air_Medium].Add(BuildData("Air mastery",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Elemental_Air_Medium].Add(BuildData("Whirlwind",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Elemental_Air_Large].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elemental_Air_Large].Add(BuildData("Air mastery",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Elemental_Air_Large].Add(BuildData("Whirlwind",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Elemental_Air_Huge].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elemental_Air_Huge].Add(BuildData("Air mastery",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Elemental_Air_Huge].Add(BuildData("Whirlwind",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Elemental_Air_Greater].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elemental_Air_Greater].Add(BuildData("Air mastery",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Elemental_Air_Greater].Add(BuildData("Whirlwind",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Elemental_Air_Elder].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elemental_Air_Elder].Add(BuildData("Air mastery",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Elemental_Air_Elder].Add(BuildData("Whirlwind",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Elemental_Earth_Small].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elemental_Earth_Small].Add(BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Elemental_Earth_Small].Add(BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Elemental_Earth_Medium].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elemental_Earth_Medium].Add(BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Elemental_Earth_Medium].Add(BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Elemental_Earth_Large].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elemental_Earth_Large].Add(BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Elemental_Earth_Large].Add(BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Elemental_Earth_Huge].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d10", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elemental_Earth_Huge].Add(BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Elemental_Earth_Huge].Add(BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Elemental_Earth_Greater].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d10", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elemental_Earth_Greater].Add(BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Elemental_Earth_Greater].Add(BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Elemental_Earth_Elder].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d10", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elemental_Earth_Elder].Add(BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Elemental_Earth_Elder].Add(BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Elemental_Fire_Small].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                "Burn", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elemental_Fire_Small].Add(BuildData("Burn",
                damageHelper.BuildEntries("1d4", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Elemental_Fire_Medium].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                "Burn", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elemental_Fire_Medium].Add(BuildData("Burn",
                damageHelper.BuildEntries("1d6", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Elemental_Fire_Large].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                "Burn", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elemental_Fire_Large].Add(BuildData("Burn",
                damageHelper.BuildEntries("2d6", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Elemental_Fire_Huge].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                "Burn", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elemental_Fire_Huge].Add(BuildData("Burn",
                damageHelper.BuildEntries("2d8", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Elemental_Fire_Greater].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                "Burn", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elemental_Fire_Greater].Add(BuildData("Burn",
                damageHelper.BuildEntries("2d8", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Elemental_Fire_Elder].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                "Burn", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elemental_Fire_Elder].Add(BuildData("Burn",
                damageHelper.BuildEntries("2d8", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Elemental_Water_Small].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elemental_Water_Small].Add(BuildData("Water mastery",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Elemental_Water_Small].Add(BuildData("Drench",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Elemental_Water_Small].Add(BuildData("Vortex",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Elemental_Water_Medium].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elemental_Water_Medium].Add(BuildData("Water mastery",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Elemental_Water_Medium].Add(BuildData("Drench",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Elemental_Water_Medium].Add(BuildData("Vortex",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Elemental_Water_Large].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elemental_Water_Large].Add(BuildData("Water mastery",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Elemental_Water_Large].Add(BuildData("Drench",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Elemental_Water_Large].Add(BuildData("Vortex",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Elemental_Water_Huge].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d10", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elemental_Water_Huge].Add(BuildData("Water mastery",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Elemental_Water_Huge].Add(BuildData("Drench",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Elemental_Water_Huge].Add(BuildData("Vortex",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Elemental_Water_Greater].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d10", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elemental_Water_Greater].Add(BuildData("Water mastery",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Elemental_Water_Greater].Add(BuildData("Drench",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Elemental_Water_Greater].Add(BuildData("Vortex",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Elemental_Water_Elder].Add(BuildData("Slam",
                damageHelper.BuildEntries("2d10", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elemental_Water_Elder].Add(BuildData("Water mastery",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Elemental_Water_Elder].Add(BuildData("Drench",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Elemental_Water_Elder].Add(BuildData("Vortex",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Elephant].Add(BuildData("Slam", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elephant].Add(BuildData("Stamp", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Elephant].Add(BuildData("Gore", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elephant].Add(BuildData("Trample", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Elf_Aquatic].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Elf_Aquatic].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Elf_Aquatic].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Elf_Drow].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Elf_Drow].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Elf_Drow].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Elf_Drow].Add(BuildData("Poison",
                string.Empty,
                "1 minute unconscious (Initial), 2d4 hours unconscious (Secondary)", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, false, false, true, save: SaveConstants.Fortitude, saveDcBonus: 3));
            testCases[CreatureConstants.Elf_Drow].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Elf_Gray].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Elf_Gray].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Elf_Gray].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Elf_Half].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Elf_Half].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Elf_Half].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Elf_High].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Elf_High].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Elf_High].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Elf_Wild].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Elf_Wild].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Elf_Wild].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Elf_Wood].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Elf_Wood].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Elf_Wood].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Erinyes].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Erinyes].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Erinyes].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Erinyes].Add(BuildData("Rope", string.Empty, "Entangle", 0, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Erinyes].Add(BuildData("Entangle", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Erinyes].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Erinyes].Add(BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            testCases[CreatureConstants.EtherealFilcher].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.EtherealFilcher].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.EtherealMarauder].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.EtherealMarauder].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Ettercap].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Ettercap].Add(BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Ettercap].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Dexterity, "Initial",
                    "2d6", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 2));
            testCases[CreatureConstants.Ettercap].Add(BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, true, true, false, true, saveAbility: AbilityConstants.Constitution));

            testCases[CreatureConstants.Ettin].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 2, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Ettin].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 2, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Ettin].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.FireBeetle_Giant].Add(BuildData("Bite", damageHelper.BuildEntries("2d4", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.FormianWorker].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.FormianWorker].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.FormianWarrior].Add(BuildData("Sting", damageHelper.BuildEntries("2d4", stingDamageType), "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.FormianWarrior].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.FormianWarrior].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.FormianWarrior].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Strength, "Initial",
                    "1d6", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.FormianTaskmaster].Add(BuildData("Sting", damageHelper.BuildEntries("2d4", stingDamageType), "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.FormianTaskmaster].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.FormianTaskmaster].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Strength, "Initial",
                    "1d6", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.FormianTaskmaster].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.FormianTaskmaster].Add(BuildData("Dominated creature", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.FormianMyrmarch].Add(BuildData("Sting",
                damageHelper.BuildEntries("2d4", stingDamageType),
                "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.FormianMyrmarch].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.FormianMyrmarch].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.FormianMyrmarch].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Strength, "Initial",
                    "1d6", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.FormianMyrmarch].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.FormianQueen].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.FormianQueen].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.FrostWorm].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), "Cold", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.FrostWorm].Add(BuildData("Trill", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
            testCases[CreatureConstants.FrostWorm].Add(BuildData("Cold",
                damageHelper.BuildEntries("1d8", FeatConstants.Foci.Elements.Cold),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            testCases[CreatureConstants.FrostWorm].Add(BuildData("Breath weapon",
                damageHelper.BuildEntries("15d6", FeatConstants.Foci.Elements.Cold),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Gargoyle].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Gargoyle].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Gargoyle].Add(BuildData("Gore", damageHelper.BuildEntries("1d6", goreDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Gargoyle_Kapoacinth].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Gargoyle_Kapoacinth].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Gargoyle_Kapoacinth].Add(BuildData("Gore", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.GelatinousCube].Add(BuildData("Slam",
                damageHelper.BuildEntries(
                    "1d6", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "1d6", FeatConstants.Foci.Elements.Acid),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.GelatinousCube].Add(BuildData("Acid", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.GelatinousCube].Add(BuildData("Engulf", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, SaveConstants.Reflex, AbilityConstants.Strength, 1));
            testCases[CreatureConstants.GelatinousCube].Add(BuildData("Paralysis", string.Empty, "3d6 rounds of paralysis", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Ghaele].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Ghaele].Add(BuildData("Light Ray",
                damageHelper.BuildEntries("2d12"),
                string.Empty, 0, "ranged touch", 2, FeatConstants.Frequencies.Round, false, true, true, false));
            testCases[CreatureConstants.Ghaele].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Ghaele].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Ghaele].Add(BuildData("Gaze", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            testCases[CreatureConstants.Ghoul].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), "Disease, Paralysis", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Ghoul].Add(BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), "Paralysis", 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Ghoul].Add(BuildData("Disease", string.Empty, "Ghoul Fever", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            testCases[CreatureConstants.Ghoul].Add(BuildData("Ghoul Fever",
                damageHelper.BuildEntries(
                    "1d3", AbilityConstants.Constitution, "Incubation period 1 day",
                    "1d3", AbilityConstants.Dexterity, "Incubation period 1 day"),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Ghoul].Add(BuildData("Paralysis", string.Empty, "1d4+1 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Ghoul_Ghast].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), "Disease, Paralysis", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Ghoul_Ghast].Add(BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), "Paralysis", 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Ghoul_Ghast].Add(BuildData("Disease", string.Empty, "Ghoul Fever", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            testCases[CreatureConstants.Ghoul_Ghast].Add(BuildData("Ghoul Fever",
                damageHelper.BuildEntries(
                    "1d3", AbilityConstants.Constitution, "Incubation period 1 day",
                    "1d3", AbilityConstants.Dexterity, "Incubation period 1 day"),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Ghoul_Ghast].Add(BuildData("Paralysis", string.Empty, "1d4+1 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Ghoul_Ghast].Add(BuildData("Stench", string.Empty, "1d6+4 rounds sickened", 0, "melee", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Ghoul_Lacedon].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), "Disease, Paralysis", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Ghoul_Lacedon].Add(BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), "Paralysis", 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Ghoul_Lacedon].Add(BuildData("Disease", string.Empty, "Ghoul Fever", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            testCases[CreatureConstants.Ghoul_Lacedon].Add(BuildData("Ghoul Fever",
                damageHelper.BuildEntries(
                    "1d3", AbilityConstants.Constitution, "Incubation period 1 day",
                    "1d3", AbilityConstants.Dexterity, "Incubation period 1 day"),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Ghoul_Lacedon].Add(BuildData("Paralysis", string.Empty, "1d4+1 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Giant_Cloud].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Giant_Cloud].Add(BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Giant_Cloud].Add(BuildData("Rock", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
            testCases[CreatureConstants.Giant_Cloud].Add(BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Giant_Cloud].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Giant_Fire].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Giant_Fire].Add(BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Giant_Fire].Add(BuildData("Rock", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
            testCases[CreatureConstants.Giant_Fire].Add(BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Giant_Frost].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Giant_Frost].Add(BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Giant_Frost].Add(BuildData("Rock", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
            testCases[CreatureConstants.Giant_Frost].Add(BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Giant_Hill].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Giant_Hill].Add(BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Giant_Hill].Add(BuildData("Rock", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
            testCases[CreatureConstants.Giant_Hill].Add(BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Giant_Stone].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Giant_Stone].Add(BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Giant_Stone].Add(BuildData("Rock", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
            testCases[CreatureConstants.Giant_Stone].Add(BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Giant_Stone_Elder].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Giant_Stone_Elder].Add(BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Giant_Stone_Elder].Add(BuildData("Rock", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
            testCases[CreatureConstants.Giant_Stone_Elder].Add(BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Giant_Stone_Elder].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Giant_Storm].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Giant_Storm].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Giant_Storm].Add(BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Giant_Storm].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.GibberingMouther].Add(BuildData("Bite",
                damageHelper.BuildEntries("1", biteDamageType),
                string.Empty, 1, "melee", 6, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.GibberingMouther].Add(BuildData("Spittle",
                damageHelper.BuildEntries("1d4", FeatConstants.Foci.Elements.Acid),
                "Blindness", 0, "ranged touch", 1, FeatConstants.Frequencies.Round, false, true, true, false));
            testCases[CreatureConstants.GibberingMouther].Add(BuildData("Blindness",
                string.Empty,
                "1d4 rounds blinded", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.GibberingMouther].Add(BuildData("Gibbering",
                string.Empty,
                "1d2 rounds Confusion", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
            testCases[CreatureConstants.GibberingMouther].Add(BuildData("Improved Grab",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.GibberingMouther].Add(BuildData("Swallow Whole",
                string.Empty,
                "Blood Drain", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.GibberingMouther].Add(BuildData("Blood Drain",
                damageHelper.BuildEntries("1d4", AbilityConstants.Constitution),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.GibberingMouther].Add(BuildData("Ground Manipulation",
                string.Empty,
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Girallon].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Girallon].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Girallon].Add(BuildData("Rend", damageHelper.BuildEntries("2d4", clawDamageType), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Githyanki].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Githyanki].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Githyanki].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Githyanki].Add(BuildData(FeatConstants.SpecialQualities.Psionic, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Githzerai].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Githzerai].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Githzerai].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Githzerai].Add(BuildData(FeatConstants.SpecialQualities.Psionic, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Glabrezu].Add(BuildData("Pincer", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Glabrezu].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Glabrezu].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Glabrezu].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Glabrezu].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Glabrezu].Add(BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            testCases[CreatureConstants.Gnoll].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Gnoll].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Gnoll].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Gnome_Forest].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Gnome_Forest].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Gnome_Forest].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Gnome_Forest].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Gnome_Rock].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Gnome_Rock].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Gnome_Rock].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Gnome_Rock].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Gnome_Svirfneblin].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Gnome_Svirfneblin].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Gnome_Svirfneblin].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Gnome_Svirfneblin].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Goblin].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Goblin].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Goblin].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Golem_Clay].Add(BuildData("Slam", damageHelper.BuildEntries("2d10", slapSlamDamageType), "Cursed Wound", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Golem_Clay].Add(BuildData("Berserk", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.Golem_Clay].Add(BuildData("Cursed Wound", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
            testCases[CreatureConstants.Golem_Clay].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Golem_Flesh].Add(BuildData("Slam", damageHelper.BuildEntries("2d8", slapSlamDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Golem_Flesh].Add(BuildData("Berserk", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.Golem_Iron].Add(BuildData("Slam", damageHelper.BuildEntries("2d10", slapSlamDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Golem_Iron].Add(BuildData("Breath weapon", string.Empty, "Poisonous Gas", 0, "supernatural ability", 1, $"1d4+1 {FeatConstants.Frequencies.Round}", false, true, true, true));
            testCases[CreatureConstants.Golem_Iron].Add(BuildData("Poisonous Gas",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Constitution, "Initial",
                    "3d4", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "melee", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Golem_Stone].Add(BuildData("Slam", damageHelper.BuildEntries("2d10", slapSlamDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Golem_Stone].Add(BuildData("Slow", string.Empty, string.Empty, 0, "supernatural ability", 1, $"2 {FeatConstants.Frequencies.Round}", true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));

            testCases[CreatureConstants.Golem_Stone_Greater].Add(BuildData("Slam", damageHelper.BuildEntries("4d8", slapSlamDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Golem_Stone_Greater].Add(BuildData("Slow", string.Empty, string.Empty, 0, "supernatural ability", 1, $"2 {FeatConstants.Frequencies.Round}", true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));

            testCases[CreatureConstants.Gorgon].Add(BuildData("Gore", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Gorgon].Add(BuildData("Breath weapon", string.Empty, "Turn to stone", 0, "supernatural ability", 5, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Gorgon].Add(BuildData("Trample", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.GrayOoze].Add(BuildData("Slam",
                damageHelper.BuildEntries(
                    "1d6", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "1d6", FeatConstants.Foci.Elements.Acid),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.GrayOoze].Add(BuildData("Acid",
                damageHelper.BuildEntries("16", FeatConstants.Foci.Elements.Acid, "Wooden or Metal objects"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
            testCases[CreatureConstants.GrayOoze].Add(BuildData("Constrict",
                damageHelper.BuildEntries(
                    "1d6", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "1d6", FeatConstants.Foci.Elements.Acid),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.GrayOoze].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.GrayRender].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.GrayRender].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.GrayRender].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.GrayRender].Add(BuildData("Rend", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.GreenHag].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.GreenHag].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.GreenHag].Add(BuildData("Weakness",
                damageHelper.BuildEntries("2d4", AbilityConstants.Strength),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
            testCases[CreatureConstants.GreenHag].Add(BuildData("Mimicry", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Grick].Add(BuildData("Tentacle", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Grick].Add(BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Griffon].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Griffon].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Griffon].Add(BuildData("Pounce", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.Griffon].Add(BuildData("Rake", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Grig].Add(BuildData(AttributeConstants.Melee,
                string.Empty,
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Grig].Add(BuildData(AttributeConstants.Ranged,
                string.Empty,
                string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Grig].Add(BuildData("Unarmed Strike",
                damageHelper.BuildEntries("1", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Grig].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Grig_WithFiddle].Add(BuildData(AttributeConstants.Melee,
                string.Empty,
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Grig_WithFiddle].Add(BuildData(AttributeConstants.Ranged,
                string.Empty,
                string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Grig_WithFiddle].Add(BuildData("Unarmed Strike",
                damageHelper.BuildEntries("1", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Grig_WithFiddle].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Grig_WithFiddle].Add(BuildData("Fiddle",
                string.Empty,
                SpellConstants.IrresistibleDance, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            testCases[CreatureConstants.Grimlock].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Grimlock].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Gynosphinx].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Gynosphinx].Add(BuildData("Pounce", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.Gynosphinx].Add(BuildData("Rake", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Gynosphinx].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Halfling_Deep].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Halfling_Deep].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Halfling_Deep].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Halfling_Lightfoot].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Halfling_Lightfoot].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Halfling_Lightfoot].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Halfling_Tallfellow].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Halfling_Tallfellow].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Halfling_Tallfellow].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Harpy].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Harpy].Add(BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Harpy].Add(BuildData("Captivating Song", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            testCases[CreatureConstants.Hawk].Add(BuildData("Talons", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.HellHound].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), "Fiery Bite", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.HellHound].Add(BuildData("Fiery Bite", damageHelper.BuildEntries("1d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.HellHound].Add(BuildData("Breath weapon", damageHelper.BuildEntries("2d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"2d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.HellHound_NessianWarhound].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), "Fiery Bite", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.HellHound_NessianWarhound].Add(BuildData("Fiery Bite", damageHelper.BuildEntries("1d8", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.HellHound_NessianWarhound].Add(BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"2d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Hellcat_Bezekira].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Hellcat_Bezekira].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Hellcat_Bezekira].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Hellcat_Bezekira].Add(BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.Hellcat_Bezekira].Add(BuildData("Rake", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Hellwasp_Swarm].Add(BuildData("Swarm", damageHelper.BuildEntries("3d6"), "Poison", 0, "swarm", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Hellwasp_Swarm].Add(BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            testCases[CreatureConstants.Hellwasp_Swarm].Add(BuildData("Inhabit", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, true, true, true, true));
            testCases[CreatureConstants.Hellwasp_Swarm].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Dexterity, "Initial",
                    "1d4", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Hezrou].Add(BuildData("Bite", damageHelper.BuildEntries("4d4", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Hezrou].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Hezrou].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Hezrou].Add(BuildData("Stench", string.Empty, "Nauseated while in range + 1d4 rounds afterwards", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            testCases[CreatureConstants.Hezrou].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Hezrou].Add(BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            testCases[CreatureConstants.Hieracosphinx].Add(BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Hieracosphinx].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Hieracosphinx].Add(BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.Hieracosphinx].Add(BuildData("Rake", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Hippogriff].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Hippogriff].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Hobgoblin].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Hobgoblin].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Hobgoblin].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Homunculus].Add(BuildData("Bite",
                damageHelper.BuildEntries("1d4", biteDamageType),
                "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Homunculus].Add(BuildData("Poison",
                string.Empty,
                "Initial damage sleep for 1 minute, secondary damage sleep for another 5d6 minutes", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 2));

            testCases[CreatureConstants.HornedDevil_Cornugon].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.HornedDevil_Cornugon].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.HornedDevil_Cornugon].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.HornedDevil_Cornugon].Add(BuildData("Tail",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                "Infernal Wound", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.HornedDevil_Cornugon].Add(BuildData("Fear Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Will, AbilityConstants.Charisma));
            testCases[CreatureConstants.HornedDevil_Cornugon].Add(BuildData("Infernal Wound",
                damageHelper.BuildEntries("2"),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, string.Empty, AbilityConstants.Constitution));
            testCases[CreatureConstants.HornedDevil_Cornugon].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.HornedDevil_Cornugon].Add(BuildData("Stun", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Strength));
            testCases[CreatureConstants.HornedDevil_Cornugon].Add(BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            testCases[CreatureConstants.Horse_Heavy].Add(BuildData("Hoof",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Horse_Heavy_War].Add(BuildData("Hoof",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Horse_Heavy_War].Add(BuildData("Bite",
                damageHelper.BuildEntries("1d4", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Horse_Light].Add(BuildData("Hoof",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Horse_Light_War].Add(BuildData("Hoof",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Horse_Light_War].Add(BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.HoundArchon].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.HoundArchon].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.HoundArchon].Add(BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.HoundArchon].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Howler].Add(BuildData("Bite",
                damageHelper.BuildEntries("2d8", biteDamageType),
                "1d4 Quills", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Howler].Add(BuildData("Quill",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Dexterity, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Howler].Add(BuildData("Howl",
                damageHelper.BuildEntries("1", AbilityConstants.Wisdom),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hour, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            testCases[CreatureConstants.Human].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Human].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Human].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Hydra_5Heads].Add(BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 5, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Hydra_6Heads].Add(BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 6, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Hydra_7Heads].Add(BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 7, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Hydra_8Heads].Add(BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 8, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Hydra_9Heads].Add(BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 9, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Hydra_10Heads].Add(BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 10, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Hydra_11Heads].Add(BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 11, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Hydra_12Heads].Add(BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 12, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Hyena].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Hyena].Add(BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData("Claw", damageHelper.BuildEntries("1d10", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData("Tail", damageHelper.BuildEntries("3d6", AttributeConstants.DamageTypes.Bludgeoning), "slow", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData("Fear Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Will, AbilityConstants.Charisma));
            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData("Slow", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            testCases[CreatureConstants.IceDevil_Gelugon].Add(BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            testCases[CreatureConstants.Imp].Add(BuildData("Sting", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), "poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Imp].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Dexterity, "Initial",
                    "2d4", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution, 2));
            testCases[CreatureConstants.Imp].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.InvisibleStalker].Add(BuildData("Slam", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Janni].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Janni].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Janni].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Janni].Add(BuildData("Change Size", string.Empty, string.Empty, 0, "spell-like ability", 2, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Janni].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Kobold].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Kobold].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Kobold].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Kolyarut].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Kolyarut].Add(BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Kolyarut].Add(BuildData("Vampiric Touch",
                damageHelper.BuildEntries("5d6"),
                "Gain temporary hit points equal to damage dealt", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.Kolyarut].Add(BuildData("Enervation Ray",
                damageHelper.BuildEntries("1d4", "Negative Level"),
                string.Empty, 0, "ranged touch", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Kolyarut].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Kraken].Add(BuildData("Tentacle",
                damageHelper.BuildEntries("2d8", tentacleDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Kraken].Add(BuildData("Arm",
                damageHelper.BuildEntries("1d6", tentacleDamageType),
                string.Empty, 0.5, "melee", 6, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Kraken].Add(BuildData("Bite",
                damageHelper.BuildEntries("4d6", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Kraken].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Kraken].Add(BuildData("Constrict (Tentacle)",
                damageHelper.BuildEntries("2d8", tentacleDamageType),
                string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Kraken].Add(BuildData("Constrict (Arm)",
                damageHelper.BuildEntries("1d6", tentacleDamageType),
                string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Kraken].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Krenshar].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Krenshar].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Krenshar].Add(BuildData("Scare", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Krenshar].Add(BuildData("Scare with Screech", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            testCases[CreatureConstants.KuoToa].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.KuoToa].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.KuoToa].Add(BuildData("Lightning Bolt",
                damageHelper.BuildEntries("1d6", FeatConstants.Foci.Elements.Electricity, "per Kuo-Toa Cleric"),
                string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", true, true, true, false, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Lamia].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Lamia].Add(BuildData("Touch", string.Empty, "Wisdom Drain", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Lamia].Add(BuildData("Wisdom Drain",
                damageHelper.BuildEntries("1d4", AbilityConstants.Wisdom),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Lamia].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Lamia].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Lammasu].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Lammasu].Add(BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.Lammasu].Add(BuildData("Rake", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.Lammasu].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Lammasu].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.LanternArchon].Add(BuildData("Light Ray", damageHelper.BuildEntries("1d6"), string.Empty, 0, "ranged touch", 2, FeatConstants.Frequencies.Round, false, true, true, false));
            testCases[CreatureConstants.LanternArchon].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Lemure].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Leonal].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Leonal].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Leonal].Add(BuildData("Roar", damageHelper.BuildEntries("2d6", FeatConstants.Foci.Elements.Sonic), string.Empty, 0, "supernatural ability", 3, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
            testCases[CreatureConstants.Leonal].Add(BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.Leonal].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Leonal].Add(BuildData("Rake", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.Leonal].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Leopard].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Leopard].Add(BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Leopard].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Leopard].Add(BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.Leopard].Add(BuildData("Rake", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.Lillend].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Lillend].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Lillend].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", slapSlamDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Lillend].Add(BuildData("Constrict", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Lillend].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Lillend].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Lillend].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Lion].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Lion].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Lion].Add(BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.Lion].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Lion].Add(BuildData("Rake", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.Lion_Dire].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Lion_Dire].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Lion_Dire].Add(BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.Lion_Dire].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Lion_Dire].Add(BuildData("Rake", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.Lizard].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Lizard_Monitor].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Lizardfolk].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Lizardfolk].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Lizardfolk].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Lizardfolk].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Locathah].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Locathah].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Locathah].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Locust_Swarm].Add(BuildData("Swarm", damageHelper.BuildEntries("2d6"), string.Empty, 0, "swarm", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Locust_Swarm].Add(BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Magmin].Add(BuildData("Burning Touch", damageHelper.BuildEntries("1d8", FeatConstants.Foci.Elements.Fire), "Combustion", 0, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Magmin].Add(BuildData("Slam", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), "Combustion", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Magmin].Add(BuildData("Combustion", damageHelper.BuildEntries("1d8", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.Magmin].Add(BuildData("Fiery Aura", damageHelper.BuildEntries("1d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.MantaRay].Add(BuildData("Ram", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Manticore].Add(BuildData("Claw", damageHelper.BuildEntries("2d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Manticore].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Manticore].Add(BuildData("Spikes", string.Empty, "Tail Spikes", 0, "ranged", 6, FeatConstants.Frequencies.Round, false, true, true, false));
            testCases[CreatureConstants.Manticore].Add(BuildData("Tail Spikes", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "extraordinary ability", 24, FeatConstants.Frequencies.Day, false, true, true, true));

            testCases[CreatureConstants.Marilith].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Marilith].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 5, FeatConstants.Frequencies.Round, true, false, false, false));
            testCases[CreatureConstants.Marilith].Add(BuildData("Tail Slap", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Marilith].Add(BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 6, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Marilith].Add(BuildData("Constrict", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Strength));
            testCases[CreatureConstants.Marilith].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Marilith].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Marilith].Add(BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            testCases[CreatureConstants.Marut].Add(BuildData("Slam", damageHelper.BuildEntries("2d6", slapSlamDamageType), "Fist of Thunder", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Marut].Add(BuildData("Slam", damageHelper.BuildEntries("2d6", slapSlamDamageType), "Fist of Lightning", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Marut].Add(BuildData("Fist of Thunder",
                damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Sonic),
                "deafened 2d6 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Marut].Add(BuildData("Fist of Lightning",
                damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Electricity),
                "blinded 2d6 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Marut].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Medusa].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Medusa].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Medusa].Add(BuildData("Snakes",
                damageHelper.BuildEntries("1d4", biteDamageType),
                "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Medusa].Add(BuildData("Petrifying Gaze", string.Empty, "Permanent petrification", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
            testCases[CreatureConstants.Medusa].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Strength, "Initial",
                    "2d6", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Megaraptor].Add(BuildData("Talons",
                damageHelper.BuildEntries("2d6", clawDamageType),
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Megaraptor].Add(BuildData("Foreclaw",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Megaraptor].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Megaraptor].Add(BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.Mephit_Air].Add(BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Mephit_Air].Add(BuildData("Breath weapon",
                damageHelper.BuildEntries("1d8"),
                string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
            testCases[CreatureConstants.Mephit_Air].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Mephit_Air].Add(BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            testCases[CreatureConstants.Mephit_Dust].Add(BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Mephit_Dust].Add(BuildData("Breath weapon",
                damageHelper.BuildEntries("1d4"),
                "Itching Skin and Burning Eyes (-4 AC, -2 attack rolls for 3 rounds)", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
            testCases[CreatureConstants.Mephit_Dust].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Mephit_Dust].Add(BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            testCases[CreatureConstants.Mephit_Earth].Add(BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Mephit_Earth].Add(BuildData("Breath weapon",
                damageHelper.BuildEntries("1d8"),
                string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
            testCases[CreatureConstants.Mephit_Earth].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Mephit_Earth].Add(BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            testCases[CreatureConstants.Mephit_Fire].Add(BuildData("Claw",
                damageHelper.BuildEntries(
                    "1d3", clawDamageType, string.Empty,
                    "1d4", FeatConstants.Foci.Elements.Fire),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Mephit_Fire].Add(BuildData("Breath weapon",
                damageHelper.BuildEntries("1d8", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
            testCases[CreatureConstants.Mephit_Fire].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Mephit_Fire].Add(BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            testCases[CreatureConstants.Mephit_Ice].Add(BuildData("Claw",
                damageHelper.BuildEntries(
                    "1d3", clawDamageType, string.Empty,
                    "1d4", FeatConstants.Foci.Elements.Cold),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Mephit_Ice].Add(BuildData("Breath weapon",
                damageHelper.BuildEntries("1d4", FeatConstants.Foci.Elements.Cold),
                "Frostbitten Skin and Frozen Eyes (-4 AC, -2 attack rolls for 3 rounds)", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
            testCases[CreatureConstants.Mephit_Ice].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Mephit_Ice].Add(BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            testCases[CreatureConstants.Mephit_Magma].Add(BuildData("Claw",
                damageHelper.BuildEntries(
                    "1d3", clawDamageType, string.Empty,
                    "1d4", FeatConstants.Foci.Elements.Fire),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Mephit_Magma].Add(BuildData("Breath weapon",
                damageHelper.BuildEntries("1d4", FeatConstants.Foci.Elements.Fire),
                "Burning Skin and Seared Eyes (-4 AC, -2 attack rolls for 3 rounds)", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
            testCases[CreatureConstants.Mephit_Magma].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Mephit_Magma].Add(BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            testCases[CreatureConstants.Mephit_Ooze].Add(BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Mephit_Ooze].Add(BuildData("Breath weapon",
                damageHelper.BuildEntries("1d4", FeatConstants.Foci.Elements.Acid),
                "Itching Skin and Burning Eyes (-4 AC, -2 attack rolls for 3 rounds)", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
            testCases[CreatureConstants.Mephit_Ooze].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Mephit_Ooze].Add(BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            testCases[CreatureConstants.Mephit_Salt].Add(BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Mephit_Salt].Add(BuildData("Breath weapon",
                damageHelper.BuildEntries("1d4"),
                "Itching Skin and Burning Eyes (-4 AC, -2 attack rolls for 3 rounds)", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
            testCases[CreatureConstants.Mephit_Salt].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Mephit_Salt].Add(BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            testCases[CreatureConstants.Mephit_Steam].Add(BuildData("Claw",
                damageHelper.BuildEntries(
                    "1d3", clawDamageType, string.Empty,
                    "1d4", FeatConstants.Foci.Elements.Fire),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Mephit_Steam].Add(BuildData("Breath weapon",
                damageHelper.BuildEntries("1d4", FeatConstants.Foci.Elements.Fire),
                "Burning Skin and Seared Eyes (-4 AC, -2 attack rolls for 3 rounds)", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
            testCases[CreatureConstants.Mephit_Steam].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Mephit_Steam].Add(BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            testCases[CreatureConstants.Mephit_Water].Add(BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Mephit_Water].Add(BuildData("Breath weapon",
                damageHelper.BuildEntries("1d8", FeatConstants.Foci.Elements.Acid),
                string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
            testCases[CreatureConstants.Mephit_Water].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Mephit_Water].Add(BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            testCases[CreatureConstants.Merfolk].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Merfolk].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Merfolk].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Mimic].Add(BuildData("Slam", damageHelper.BuildEntries("1d8", slapSlamDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Mimic].Add(BuildData("Adhesive", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Mimic].Add(BuildData("Crush", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.MindFlayer].Add(BuildData("Tentacle", damageHelper.BuildEntries("1d4", tentacleDamageType), string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.MindFlayer].Add(BuildData("Mind Blast", string.Empty, "3d4 rounds stunned", 1, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
            testCases[CreatureConstants.MindFlayer].Add(BuildData(FeatConstants.SpecialQualities.Psionic, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.MindFlayer].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.MindFlayer].Add(BuildData("Extract", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Minotaur].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Minotaur].Add(BuildData("Gore", damageHelper.BuildEntries("1d8", goreDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Minotaur].Add(BuildData("Powerful Charge", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.Mohrg].Add(BuildData("Slam", damageHelper.BuildEntries("1d6", slapSlamDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Mohrg].Add(BuildData("Tongue", string.Empty, "Paralyzing Touch", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Mohrg].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Mohrg].Add(BuildData("Paralyzing Touch", string.Empty, "1d4 minutes paralyzed", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Mohrg].Add(BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));

            testCases[CreatureConstants.Monkey].Add(BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Mule].Add(BuildData("Hoof", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Mummy].Add(BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), "Disease", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Mummy].Add(BuildData("Despair", string.Empty, "1d4 rounds fear", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
            testCases[CreatureConstants.Mummy].Add(BuildData("Disease", string.Empty, "Mummy Rot", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Mummy].Add(BuildData("Mummy Rot",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Constitution, "Incubation period 1 minute",
                    "1d6", AbilityConstants.Charisma, "Incubation period 1 minute"),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Naga_Dark].Add(BuildData("Sting", damageHelper.BuildEntries("2d4", stingDamageType), "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Naga_Dark].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Naga_Dark].Add(BuildData("Poison", string.Empty, "lapse into a nightmare-haunted sleep for 2d4 minutes", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Naga_Dark].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Naga_Guardian].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Naga_Guardian].Add(BuildData("Spit", string.Empty, "Poison", 0, "ranged touch", 1, FeatConstants.Frequencies.Round, false, true, false, false));
            testCases[CreatureConstants.Naga_Guardian].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d10", AbilityConstants.Constitution, "Initial",
                    "1d10", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Naga_Guardian].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Naga_Spirit].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Naga_Spirit].Add(BuildData("Charming Gaze", string.Empty, SpellConstants.CharmPerson, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
            testCases[CreatureConstants.Naga_Spirit].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d8", AbilityConstants.Constitution, "Initial",
                    "1d8", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Naga_Spirit].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Naga_Water].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Naga_Water].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d8", AbilityConstants.Constitution, "Initial",
                    "1d8", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Naga_Water].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Nalfeshnee].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Nalfeshnee].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Nalfeshnee].Add(BuildData("Smite", string.Empty, string.Empty, 1, "supernatural ability", 3, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
            testCases[CreatureConstants.Nalfeshnee].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Nalfeshnee].Add(BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            testCases[CreatureConstants.NightHag].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), "Disease", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.NightHag].Add(BuildData("Disease",
                damageHelper.BuildEntries("1d6", AbilityConstants.Constitution, "Incubation period 1 day"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.NightHag].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.NightHag].Add(BuildData("Dream Haunting", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            testCases[CreatureConstants.Nightcrawler].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData("Sting", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData("Desecrating Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData("Energy Drain",
                damageHelper.BuildEntries("1", "Negative Level"),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "2d6", AbilityConstants.Strength, "Initial",
                    "2d6", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData("Summon Undead", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
            testCases[CreatureConstants.Nightcrawler].Add(BuildData("Swallow Whole",
                damageHelper.BuildEntries(
                    "2d8+12", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "12", FeatConstants.Foci.Elements.Acid),
                "Energy Drain", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Nightmare].Add(BuildData("Hoof",
                damageHelper.BuildEntries(
                    "1d8", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "1d4", FeatConstants.Foci.Elements.Fire),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Nightmare].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Nightmare].Add(BuildData("Flaming Hooves", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            testCases[CreatureConstants.Nightmare].Add(BuildData("Smoke", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Nightmare].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Nightmare_Cauchemar].Add(BuildData("Hoof",
                damageHelper.BuildEntries(
                    "2d6", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "1d4", FeatConstants.Foci.Elements.Fire),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Nightmare_Cauchemar].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Nightmare_Cauchemar].Add(BuildData("Flaming Hooves", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            testCases[CreatureConstants.Nightmare_Cauchemar].Add(BuildData("Smoke", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Nightmare_Cauchemar].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Nightwalker].Add(BuildData("Slam", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Nightwalker].Add(BuildData("Crush Item", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Nightwalker].Add(BuildData("Desecrating Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));
            testCases[CreatureConstants.Nightwalker].Add(BuildData("Evil Gaze", string.Empty, "1d8 rounds paralyzed with fear", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
            testCases[CreatureConstants.Nightwalker].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Nightwalker].Add(BuildData("Summon Undead", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            testCases[CreatureConstants.Nightwing].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), "Magic Drain", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Nightwing].Add(BuildData("Desecrating Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));
            testCases[CreatureConstants.Nightwing].Add(BuildData("Magic Drain", string.Empty, "1 point enhancement bonus", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Nightwing].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Nightwing].Add(BuildData("Summon Undead", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            testCases[CreatureConstants.Nixie].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Nixie].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Nixie].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Nixie].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Nymph].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Nymph].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Nymph].Add(BuildData("Blinding Beauty", string.Empty, "Blinded permanently", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Nymph].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Nymph].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Nymph].Add(BuildData("Stunning Glance", string.Empty, "stunned 2d4 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.OchreJelly].Add(BuildData("Slam",
                damageHelper.BuildEntries(
                    "2d4", slapSlamDamageType, string.Empty,
                    "1d4", FeatConstants.Foci.Elements.Acid),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.OchreJelly].Add(BuildData("Acid", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
            testCases[CreatureConstants.OchreJelly].Add(BuildData("Constrict",
                damageHelper.BuildEntries(
                    "2d4", slapSlamDamageType, string.Empty,
                    "1d4", FeatConstants.Foci.Elements.Acid),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.OchreJelly].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Octopus].Add(BuildData("Arms", damageHelper.BuildEntries("0", tentacleDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Octopus].Add(BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Octopus].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Octopus_Giant].Add(BuildData("Tentacle", damageHelper.BuildEntries("1d4", tentacleDamageType), string.Empty, 1, "melee", 8, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Octopus_Giant].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Octopus_Giant].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Octopus_Giant].Add(BuildData("Constrict", damageHelper.BuildEntries("2d8", tentacleDamageType), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Ogre].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Ogre].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Ogre].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Ogre_Merrow].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Ogre_Merrow].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Ogre_Merrow].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.OgreMage].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.OgreMage].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.OgreMage].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.OgreMage].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Orc].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Orc].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Orc].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Orc_Half].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Orc_Half].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Orc_Half].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Otyugh].Add(BuildData("Tentacle", damageHelper.BuildEntries("1d6", tentacleDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Otyugh].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), "Disease", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Otyugh].Add(BuildData("Constrict", damageHelper.BuildEntries("1d6", tentacleDamageType), string.Empty, 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Otyugh].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Otyugh].Add(BuildData("Disease", string.Empty, "Filth Fever", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            testCases[CreatureConstants.Otyugh].Add(BuildData("Filth Fever",
                damageHelper.BuildEntries(
                    "1d3", AbilityConstants.Dexterity, "Incubation period 1d3 days",
                    "1d3", AbilityConstants.Constitution, "Incubation period 1d3 days"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Owl].Add(BuildData("Talons", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Owl_Giant].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Owl_Giant].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Owlbear].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Owlbear].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Owlbear].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Pegasus].Add(BuildData("Hoof", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Pegasus].Add(BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Pegasus].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.PhantomFungus].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.PhaseSpider].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.PhaseSpider].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d8", AbilityConstants.Constitution, "Initial",
                    "1d8", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.PhaseSpider].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Phasm].Add(BuildData("Slam", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.PitFiend].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.PitFiend].Add(BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.PitFiend].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), "poison, disease", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.PitFiend].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.PitFiend].Add(BuildData("Constrict", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 2, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.PitFiend].Add(BuildData("Fear Aura", string.Empty, string.Empty, 0, "supernatural ability", 2, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Will, AbilityConstants.Charisma));
            testCases[CreatureConstants.PitFiend].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.PitFiend].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.PitFiend].Add(BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
            testCases[CreatureConstants.PitFiend].Add(BuildData("Poison",
                damageHelper.BuildEntries("1d6", AbilityConstants.Constitution, "Initial"),
                "Death (Secondary)", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            testCases[CreatureConstants.PitFiend].Add(BuildData("Disease", string.Empty, "Devil Chills", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            testCases[CreatureConstants.PitFiend].Add(BuildData("Devil Chills",
                damageHelper.BuildEntries("1d4", AbilityConstants.Strength, "Incubation period 1d4 days"),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Pixie].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Pixie].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Pixie].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Pixie].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Pixie].Add(BuildData("Special Arrow (Memory Loss)", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, false, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveDcBonus: 2));
            testCases[CreatureConstants.Pixie].Add(BuildData("Special Arrow (Sleep)", string.Empty, SpellConstants.Sleep, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, false, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveDcBonus: 2));

            testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(BuildData("Special Arrow (Memory Loss)", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, false, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveDcBonus: 2));
            testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(BuildData("Special Arrow (Sleep)", string.Empty, SpellConstants.Sleep, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, false, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveDcBonus: 2));

            testCases[CreatureConstants.Pony].Add(BuildData("Hoof", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Pony_War].Add(BuildData("Hoof", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Porpoise].Add(BuildData("Slam", damageHelper.BuildEntries("2d4", slapSlamDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.PrayingMantis_Giant].Add(BuildData("Claws", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.PrayingMantis_Giant].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.PrayingMantis_Giant].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Pseudodragon].Add(BuildData("Sting",
                damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Piercing),
                "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Pseudodragon].Add(BuildData("Bite",
                damageHelper.BuildEntries("1", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Pseudodragon].Add(BuildData("Poison",
                string.Empty,
                "initial damage sleep for 1 minute, secondary damage sleep for 1d3 hours", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 2));

            testCases[CreatureConstants.PurpleWorm].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.PurpleWorm].Add(BuildData("Sting", damageHelper.BuildEntries("2d6", stingDamageType), "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.PurpleWorm].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.PurpleWorm].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Strength, "Initial",
                    "2d6", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.PurpleWorm].Add(BuildData("Swallow Whole",
                damageHelper.BuildEntries(
                    "2d8+12", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "8", FeatConstants.Foci.Elements.Acid),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Pyrohydra_5Heads].Add(BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 5, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Pyrohydra_5Heads].Add(BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Fire, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Pyrohydra_6Heads].Add(BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 6, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Pyrohydra_6Heads].Add(BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Fire, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Pyrohydra_7Heads].Add(BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 7, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Pyrohydra_7Heads].Add(BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Fire, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Pyrohydra_8Heads].Add(BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 8, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Pyrohydra_8Heads].Add(BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Fire, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Pyrohydra_9Heads].Add(BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 9, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Pyrohydra_9Heads].Add(BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Fire, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Pyrohydra_10Heads].Add(BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 10, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Pyrohydra_10Heads].Add(BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Fire, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Pyrohydra_11Heads].Add(BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 11, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Pyrohydra_11Heads].Add(BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Fire, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Pyrohydra_12Heads].Add(BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 12, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Pyrohydra_12Heads].Add(BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Fire, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Quasit].Add(BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), "poison", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Quasit].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Quasit].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Dexterity, "Initial",
                    "2d4", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution, 2));
            testCases[CreatureConstants.Quasit].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Rakshasa].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Rakshasa].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Rakshasa].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Rast].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Rast].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Rast].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Rast].Add(BuildData("Paralyzing Gaze", string.Empty, "Paralysis for 1d6 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Rast].Add(BuildData("Blood Drain",
                damageHelper.BuildEntries("1", AbilityConstants.Constitution),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Rat].Add(BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Rat_Dire].Add(BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), "Disease", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Rat_Dire].Add(BuildData("Disease", string.Empty, "Filth Fever", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
            testCases[CreatureConstants.Rat_Dire].Add(BuildData("Filth Fever",
                damageHelper.BuildEntries(
                    "1d3", AbilityConstants.Dexterity, "Incubation period 1d3 days",
                    "1d3", AbilityConstants.Constitution, "Incubation period 1d3 days"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Rat_Swarm].Add(BuildData("Swarm", damageHelper.BuildEntries("1d6"), "Disease", 0, "swarm", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Rat_Swarm].Add(BuildData("Disease", string.Empty, "Filth Fever", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
            testCases[CreatureConstants.Rat_Swarm].Add(BuildData("Filth Fever",
                damageHelper.BuildEntries(
                    "1d3", AbilityConstants.Dexterity, "Incubation period 1d3 days",
                    "1d3", AbilityConstants.Constitution, "Incubation period 1d3 days"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            testCases[CreatureConstants.Rat_Swarm].Add(BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Raven].Add(BuildData("Claws", damageHelper.BuildEntries("1d2", clawDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Ravid].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d6", slapSlamDamageType), "Positive Energy", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Ravid].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), "Positive Energy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Ravid].Add(BuildData("Tail Touch", string.Empty, "Positive Energy", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Ravid].Add(BuildData("Claw Touch", string.Empty, "Positive Energy", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Ravid].Add(BuildData("Positive Energy",
                damageHelper.BuildEntries("2d10", "Positive energy"),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Ravid].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.RazorBoar].Add(BuildData("Tusk Slash", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), "Vorpal Tusk", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.RazorBoar].Add(BuildData("Hoof", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.RazorBoar].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.RazorBoar].Add(BuildData("Vorpal Tusk", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.RazorBoar].Add(BuildData("Trample", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Remorhaz].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Remorhaz].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Remorhaz].Add(BuildData("Swallow Whole",
                damageHelper.BuildEntries(
                    "2d8+12", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "8d6", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Retriever].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Retriever].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Retriever].Add(BuildData("Eye Ray", string.Empty, string.Empty, 0, "ranged touch", 1, FeatConstants.Frequencies.Round, false, true, false, true, string.Empty, AbilityConstants.Dexterity));
            testCases[CreatureConstants.Retriever].Add(BuildData("Find Target", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));
            testCases[CreatureConstants.Retriever].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Rhinoceras].Add(BuildData("Gore", damageHelper.BuildEntries("2d6", goreDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Rhinoceras].Add(BuildData("Powerful Charge", damageHelper.BuildEntries("4d6", goreDamageType), string.Empty, 3, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.Roc].Add(BuildData("Talon", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Roc].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Roper].Add(BuildData("Strand", string.Empty, "Drag", 0, "ranged touch", 6, FeatConstants.Frequencies.Round, false, true, false, false));
            testCases[CreatureConstants.Roper].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Roper].Add(BuildData("Drag", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Strength));
            testCases[CreatureConstants.Roper].Add(BuildData("Weakness",
                damageHelper.BuildEntries("2d8", AbilityConstants.Strength),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.RustMonster].Add(BuildData("Antennae", string.Empty, "Rust", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.RustMonster].Add(BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.RustMonster].Add(BuildData("Rust", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 4));

            testCases[CreatureConstants.Sahuagin].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Sahuagin].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Sahuagin].Add(BuildData("Talon", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Sahuagin].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Sahuagin].Add(BuildData("Blood Frenzy", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
            testCases[CreatureConstants.Sahuagin].Add(BuildData("Rake", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Sahuagin_Mutant].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Sahuagin_Mutant].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 2, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Sahuagin_Mutant].Add(BuildData("Talon", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Sahuagin_Mutant].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Sahuagin_Mutant].Add(BuildData("Blood Frenzy", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
            testCases[CreatureConstants.Sahuagin_Mutant].Add(BuildData("Rake", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Sahuagin_Malenti].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Sahuagin_Malenti].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Sahuagin_Malenti].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Sahuagin_Malenti].Add(BuildData("Blood Frenzy", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            testCases[CreatureConstants.Salamander_Flamebrother].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Salamander_Flamebrother].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), "Heat", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Salamander_Flamebrother].Add(BuildData("Constrict", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), "Heat", 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Salamander_Flamebrother].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Salamander_Flamebrother].Add(BuildData("Heat",
                 damageHelper.BuildEntries("1d6", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));

            testCases[CreatureConstants.Salamander_Average].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Salamander_Average].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), "Heat", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Salamander_Average].Add(BuildData("Constrict", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), "Heat", 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Salamander_Average].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Salamander_Average].Add(BuildData("Heat",
                 damageHelper.BuildEntries("1d6", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));

            testCases[CreatureConstants.Salamander_Noble].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Salamander_Noble].Add(BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), "Heat", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Salamander_Noble].Add(BuildData("Constrict", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), "Heat", 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Salamander_Noble].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Salamander_Noble].Add(BuildData("Heat",
                 damageHelper.BuildEntries("1d8", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            testCases[CreatureConstants.Salamander_Noble].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Satyr].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, false, false));
            testCases[CreatureConstants.Satyr].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 0, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Satyr].Add(BuildData("Head butt", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Satyr_WithPipes].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, false, false));
            testCases[CreatureConstants.Satyr_WithPipes].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 0, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Satyr_WithPipes].Add(BuildData("Head butt", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Satyr_WithPipes].Add(BuildData("Pipes", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            testCases[CreatureConstants.Scorpion_Monstrous_Tiny].Add(BuildData("Claw", damageHelper.BuildEntries("1d2", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Scorpion_Monstrous_Tiny].Add(BuildData("Sting", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Piercing), "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Scorpion_Monstrous_Tiny].Add(BuildData("Constrict", damageHelper.BuildEntries("1d2", clawDamageType), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Scorpion_Monstrous_Tiny].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Scorpion_Monstrous_Tiny].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1", AbilityConstants.Constitution, "Initial",
                    "1", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Scorpion_Monstrous_Small].Add(BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Scorpion_Monstrous_Small].Add(BuildData("Sting", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Piercing), "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Scorpion_Monstrous_Small].Add(BuildData("Constrict", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Scorpion_Monstrous_Small].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Scorpion_Monstrous_Small].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d2", AbilityConstants.Constitution, "Initial",
                    "1d2", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Scorpion_Monstrous_Medium].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Scorpion_Monstrous_Medium].Add(BuildData("Sting", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Scorpion_Monstrous_Medium].Add(BuildData("Constrict", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Scorpion_Monstrous_Medium].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Scorpion_Monstrous_Medium].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d3", AbilityConstants.Constitution, "Initial",
                    "1d3", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Scorpion_Monstrous_Large].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Scorpion_Monstrous_Large].Add(BuildData("Sting", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Scorpion_Monstrous_Large].Add(BuildData("Constrict", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Scorpion_Monstrous_Large].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Scorpion_Monstrous_Large].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Constitution, "Initial",
                    "1d4", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Scorpion_Monstrous_Huge].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Scorpion_Monstrous_Huge].Add(BuildData("Sting", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Scorpion_Monstrous_Huge].Add(BuildData("Constrict", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Scorpion_Monstrous_Huge].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Scorpion_Monstrous_Huge].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan].Add(BuildData("Sting", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan].Add(BuildData("Constrict", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d8", AbilityConstants.Constitution, "Initial",
                    "1d8", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Scorpion_Monstrous_Colossal].Add(BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Scorpion_Monstrous_Colossal].Add(BuildData("Sting", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Scorpion_Monstrous_Colossal].Add(BuildData("Constrict", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Scorpion_Monstrous_Colossal].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Scorpion_Monstrous_Colossal].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d10", AbilityConstants.Constitution, "Initial",
                    "1d10", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Scorpionfolk].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Scorpionfolk].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 0, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Scorpionfolk].Add(BuildData("Sting", damageHelper.BuildEntries("1d8", stingDamageType), "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Scorpionfolk].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Scorpionfolk].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Dexterity, "Initial",
                    "1d4", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Scorpionfolk].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Scorpionfolk].Add(BuildData("Trample", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            testCases[CreatureConstants.SeaCat].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.SeaCat].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.SeaCat].Add(BuildData("Rend", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.SeaHag].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.SeaHag].Add(BuildData("Horrific Appearance",
                damageHelper.BuildEntries("2d6", AbilityConstants.Strength),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
            testCases[CreatureConstants.SeaHag].Add(BuildData("Evil Eye", string.Empty, string.Empty, 0, "supernatural ability", 3, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));

            testCases[CreatureConstants.Shadow].Add(BuildData("Incorporeal touch", string.Empty, "Strength Damage", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Shadow].Add(BuildData("Strength Damage",
                damageHelper.BuildEntries("1d6", AbilityConstants.Strength),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            testCases[CreatureConstants.Shadow].Add(BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Shadow_Greater].Add(BuildData("Incorporeal touch", string.Empty, "Strength Damage", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Shadow_Greater].Add(BuildData("Strength Damage",
                damageHelper.BuildEntries("1d8", AbilityConstants.Strength),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            testCases[CreatureConstants.Shadow_Greater].Add(BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.ShadowMastiff].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.ShadowMastiff].Add(BuildData("Bay", string.Empty, "panicked 2d4 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
            testCases[CreatureConstants.ShadowMastiff].Add(BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.ShamblingMound].Add(BuildData("Slam", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.ShamblingMound].Add(BuildData("Constrict", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.ShamblingMound].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Shark_Dire].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Shark_Dire].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Shark_Dire].Add(BuildData("Swallow Whole",
                damageHelper.BuildEntries(
                    "2d6+6", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "1d8+4", FeatConstants.Foci.Elements.Acid),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Shark_Huge].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Shark_Large].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Shark_Medium].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.ShieldGuardian].Add(BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.ShieldGuardian].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.ShockerLizard].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.ShockerLizard].Add(BuildData("Stunning Shock",
                damageHelper.BuildEntries("2d8", FeatConstants.Foci.Elements.Electricity),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            testCases[CreatureConstants.ShockerLizard].Add(BuildData("Lethal Shock",
                damageHelper.BuildEntries("2d8", FeatConstants.Foci.Elements.Electricity, "per Shocker Lizard"),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, false, true, true, true, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Shrieker].Add(BuildData("Shriek", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Skum].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Skum].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Skum].Add(BuildData("Rake", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.Slaad_Red].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Slaad_Red].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), "Implant", 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Slaad_Red].Add(BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.Slaad_Red].Add(BuildData("Implant", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Slaad_Red].Add(BuildData("Stunning Croak", string.Empty, "Stunned 1d3 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Slaad_Red].Add(BuildData("Summon Slaad", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            testCases[CreatureConstants.Slaad_Blue].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Slaad_Blue].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), "Disease", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Slaad_Blue].Add(BuildData("Disease", string.Empty, "Slaad Fever", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.Slaad_Blue].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Slaad_Blue].Add(BuildData("Slaad Fever",
                damageHelper.BuildEntries(
                    "1d3", AbilityConstants.Dexterity, "Incubation period 1 day",
                    "1d3", AbilityConstants.Charisma, "Incubation period 1 day"),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Slaad_Blue].Add(BuildData("Summon Slaad", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            testCases[CreatureConstants.Slaad_Green].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Slaad_Green].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Slaad_Green].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Slaad_Green].Add(BuildData("Summon Slaad", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            testCases[CreatureConstants.Slaad_Gray].Add(BuildData("Claw", damageHelper.BuildEntries("2d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Slaad_Gray].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Slaad_Gray].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Slaad_Gray].Add(BuildData("Summon Slaad", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            testCases[CreatureConstants.Slaad_Death].Add(BuildData("Claw", damageHelper.BuildEntries("3d6", clawDamageType), "Stun", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData("Bite", damageHelper.BuildEntries("2d10", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData("Stun", string.Empty, "Stunned 1 round", 0, "extraordinary ability", 3, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Wisdom, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Slaad_Death].Add(BuildData("Summon Slaad", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            testCases[CreatureConstants.Snake_Constrictor].Add(BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Snake_Constrictor].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Snake_Constrictor].Add(BuildData("Constrict", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Snake_Constrictor_Giant].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Snake_Constrictor_Giant].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Snake_Constrictor_Giant].Add(BuildData("Constrict", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Snake_Viper_Tiny].Add(BuildData("Bite",
                damageHelper.BuildEntries("1", biteDamageType),
                "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Snake_Viper_Tiny].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Snake_Viper_Small].Add(BuildData("Bite", damageHelper.BuildEntries("1d2", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Snake_Viper_Small].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Snake_Viper_Medium].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Snake_Viper_Medium].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Snake_Viper_Large].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Snake_Viper_Large].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Snake_Viper_Huge].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Snake_Viper_Huge].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Spectre].Add(BuildData("Incorporeal touch", damageHelper.BuildEntries("1d8"), "Energy Drain", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Spectre].Add(BuildData("Energy Drain",
                damageHelper.BuildEntries("2", "Negative Level"),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Spectre].Add(BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny].Add(BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d2", AbilityConstants.Strength, "Initial",
                    "1d2", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Spider_Monstrous_Hunter_Small].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Spider_Monstrous_Hunter_Small].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d3", AbilityConstants.Strength, "Initial",
                    "1d3", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Strength, "Initial",
                    "1d4", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Spider_Monstrous_Hunter_Large].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Spider_Monstrous_Hunter_Large].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Strength, "Initial",
                    "1d6", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d8", AbilityConstants.Strength, "Initial",
                    "1d8", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "2d6", AbilityConstants.Strength, "Initial",
                    "2d6", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "2d8", AbilityConstants.Strength, "Initial",
                    "2d8", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny].Add(BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d2", AbilityConstants.Strength, "Initial",
                    "1d2", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny].Add(BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Constitution));

            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d3", AbilityConstants.Strength, "Initial",
                    "1d3", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small].Add(BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Constitution));

            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Strength, "Initial",
                    "1d4", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium].Add(BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Constitution));

            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Strength, "Initial",
                    "1d6", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large].Add(BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Constitution));

            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d8", AbilityConstants.Strength, "Initial",
                    "1d8", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge].Add(BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Constitution));

            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "2d6", AbilityConstants.Strength, "Initial",
                    "2d6", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan].Add(BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Constitution));

            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "2d8", AbilityConstants.Strength, "Initial",
                    "2d8", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal].Add(BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Constitution));

            testCases[CreatureConstants.SpiderEater].Add(BuildData("Sting", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.SpiderEater].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.SpiderEater].Add(BuildData("Poison",
                string.Empty,
                "Paralysis for 1d8+5 weeks (Secondary)", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            testCases[CreatureConstants.SpiderEater].Add(BuildData("Implant", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, requiredGender: GenderConstants.Female));

            testCases[CreatureConstants.Spider_Swarm].Add(BuildData("Swarm", damageHelper.BuildEntries("1d6"), "poison", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Spider_Swarm].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d3", AbilityConstants.Strength, "Initial",
                    "1d3", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            testCases[CreatureConstants.Spider_Swarm].Add(BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Squid].Add(BuildData("Arms",
                damageHelper.BuildEntries("0", tentacleDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Squid].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Squid].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Squid_Giant].Add(BuildData("Tentacle", damageHelper.BuildEntries("1d6", tentacleDamageType), string.Empty, 1, "melee", 10, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Squid_Giant].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Squid_Giant].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Squid_Giant].Add(BuildData("Constrict", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.StagBeetle_Giant].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.StagBeetle_Giant].Add(BuildData("Trample", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Stirge].Add(BuildData("Touch", string.Empty, "Attach", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Stirge].Add(BuildData("Attach", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Stirge].Add(BuildData("Blood Drain",
                damageHelper.BuildEntries("1d4", AbilityConstants.Constitution),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.Succubus].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Succubus].Add(BuildData("Energy Drain",
                damageHelper.BuildEntries("1", "Negative Level"),
                SpellConstants.Suggestion, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, string.Empty, AbilityConstants.Charisma));
            testCases[CreatureConstants.Succubus].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Succubus].Add(BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            testCases[CreatureConstants.Tarrasque].Add(BuildData("Bite", damageHelper.BuildEntries("4d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Tarrasque].Add(BuildData("Horn",
                damageHelper.BuildEntries("1d10", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Tarrasque].Add(BuildData("Claw",
                damageHelper.BuildEntries("1d12", clawDamageType),
                string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Tarrasque].Add(BuildData("Tail Slap",
                damageHelper.BuildEntries("3d8", slapSlamDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Tarrasque].Add(BuildData("Augmented Critical", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Tarrasque].Add(BuildData("Frightful Presence", string.Empty, "Shaken", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
            testCases[CreatureConstants.Tarrasque].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Tarrasque].Add(BuildData("Rush", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true));
            testCases[CreatureConstants.Tarrasque].Add(BuildData("Swallow Whole",
                damageHelper.BuildEntries(
                    "2d8+8", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "2d8+6", FeatConstants.Foci.Elements.Acid),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Tendriculos].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Tendriculos].Add(BuildData("Tendril",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Tendriculos].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Tendriculos].Add(BuildData("Swallow Whole",
                damageHelper.BuildEntries("2d6", FeatConstants.Foci.Elements.Acid),
                "Paralysis", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Tendriculos].Add(BuildData("Paralysis", string.Empty, "paralyzed for 3d6 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Thoqqua].Add(BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), "Heat, Burn", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Thoqqua].Add(BuildData("Heat", damageHelper.BuildEntries("2d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Thoqqua].Add(BuildData("Burn", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Tiefling].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Tiefling].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Tiefling].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Tiefling].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Tiger].Add(BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Tiger].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Tiger].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Tiger].Add(BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.Tiger].Add(BuildData("Rake", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.Tiger_Dire].Add(BuildData("Claw", damageHelper.BuildEntries("2d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Tiger_Dire].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Tiger_Dire].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Tiger_Dire].Add(BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.Tiger_Dire].Add(BuildData("Rake", damageHelper.BuildEntries("2d4", clawDamageType), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.Titan].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Titan].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Titan].Add(BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Titan].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Toad].Add(new[] { None });

            testCases[CreatureConstants.Tojanida_Juvenile].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Tojanida_Juvenile].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Tojanida_Juvenile].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Tojanida_Juvenile].Add(BuildData("Ink Cloud", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Tojanida_Adult].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Tojanida_Adult].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Tojanida_Adult].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Tojanida_Adult].Add(BuildData("Ink Cloud", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Tojanida_Elder].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Tojanida_Elder].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Tojanida_Elder].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Tojanida_Elder].Add(BuildData("Ink Cloud", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Treant].Add(BuildData("Slam", damageHelper.BuildEntries("2d6", slapSlamDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Treant].Add(BuildData("Animate Trees", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Treant].Add(BuildData("Double Damage Against Objects", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Treant].Add(BuildData("Trample", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Triceratops].Add(BuildData("Gore",
                damageHelper.BuildEntries("2d8", goreDamageType),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Triceratops].Add(BuildData("Powerful charge",
                damageHelper.BuildEntries("4d8", goreDamageType),
                string.Empty, 2, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Triceratops].Add(BuildData("Trample",
                damageHelper.BuildEntries("2d12", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            testCases[CreatureConstants.Triton].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Triton].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Triton].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Triton].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Troglodyte].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Troglodyte].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Troglodyte].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Troglodyte].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Troglodyte].Add(BuildData("Stench", string.Empty, "Sickened 10 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Troll].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Troll].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Troll].Add(BuildData("Rend", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Troll_Scrag].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Troll_Scrag].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Troll_Scrag].Add(BuildData("Rend", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.TrumpetArchon].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.TrumpetArchon].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.TrumpetArchon].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.TrumpetArchon].Add(BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.TrumpetArchon].Add(BuildData("Trumpet", string.Empty, "1d4 rounds paralyzed", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Tyrannosaurus].Add(BuildData("Bite",
                damageHelper.BuildEntries("3d6", biteDamageType),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Tyrannosaurus].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Tyrannosaurus].Add(BuildData("Swallow Whole",
                damageHelper.BuildEntries(
                    "2d8", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "8", FeatConstants.Foci.Elements.Acid),
                string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.UmberHulk].Add(BuildData("Claw", damageHelper.BuildEntries("2d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.UmberHulk].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.UmberHulk].Add(BuildData("Confusing Gaze", string.Empty, SpellConstants.Confusion, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            testCases[CreatureConstants.UmberHulk_TrulyHorrid].Add(BuildData("Claw", damageHelper.BuildEntries("3d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.UmberHulk_TrulyHorrid].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.UmberHulk_TrulyHorrid].Add(BuildData("Confusing Gaze", string.Empty, SpellConstants.Confusion, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            testCases[CreatureConstants.Unicorn].Add(BuildData("Horn",
                damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Unicorn].Add(BuildData("Hoof",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Unicorn].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.VampireSpawn].Add(BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), "Energy Drain", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.VampireSpawn].Add(BuildData("Blood Drain",
                damageHelper.BuildEntries("1d4", AbilityConstants.Constitution),
                "Vampire Spawn gains 5 temporary hit points", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.VampireSpawn].Add(BuildData("Domination", string.Empty, SpellConstants.DominatePerson, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
            testCases[CreatureConstants.VampireSpawn].Add(BuildData("Energy Drain",
                damageHelper.BuildEntries("1", "Negative Level"),
                "Vampire Spawn gains 5 temporary hit points", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.VampireSpawn].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Vargouille].Add(BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Vargouille].Add(BuildData("Shriek", string.Empty, "paralyzed 2d4 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 1));
            testCases[CreatureConstants.Vargouille].Add(BuildData("Kiss", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 4));
            testCases[CreatureConstants.Vargouille].Add(BuildData("Poison", string.Empty, "unable to heal the vargouille’s bite damage naturally or magically", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 1));

            testCases[CreatureConstants.VioletFungus].Add(BuildData("Tentacle", damageHelper.BuildEntries("1d6", tentacleDamageType), "Poison", 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.VioletFungus].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Strength, "Initial",
                    "1d4", AbilityConstants.Constitution, "Initial",
                    "1d4", AbilityConstants.Strength, "Secondary",
                    "1d4", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Vrock].Add(BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Vrock].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Vrock].Add(BuildData("Talon", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Vrock].Add(BuildData("Dance of Ruin", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true, SaveConstants.Reflex, AbilityConstants.Charisma));
            testCases[CreatureConstants.Vrock].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Vrock].Add(BuildData("Spores",
                damageHelper.BuildEntries("1d8"),
                string.Empty, 0, "melee", 1, $"3 {FeatConstants.Frequencies.Round}", false, true, true, true));
            testCases[CreatureConstants.Vrock].Add(BuildData("Stunning Screech", string.Empty, string.Empty, 0, "melee", 1, FeatConstants.Frequencies.Hour, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            testCases[CreatureConstants.Vrock].Add(BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            testCases[CreatureConstants.Wasp_Giant].Add(BuildData("Sting", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Wasp_Giant].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Dexterity, "Initial",
                    "1d6", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Weasel].Add(BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), "Attach", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Weasel].Add(BuildData("Attach",
                damageHelper.BuildEntries("1d3", biteDamageType),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Weasel_Dire].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), "Attach", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Weasel_Dire].Add(BuildData("Attach",
                string.Empty,
                "Blood Drain", 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Weasel_Dire].Add(BuildData("Blood Drain",
                damageHelper.BuildEntries("1d4", AbilityConstants.Constitution),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Whale_Baleen].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Whale_Cachalot].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Whale_Cachalot].Add(BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Whale_Orca].Add(BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Wight].Add(BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), "Energy Drain", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Wight].Add(BuildData("Energy Drain", damageHelper.BuildEntries("1", "Negative Level"), "Wight gains 5 temporary hit points", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Wight].Add(BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.WillOWisp].Add(BuildData("Shock",
                damageHelper.BuildEntries("2d8", FeatConstants.Foci.Elements.Electricity),
                string.Empty, 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.WinterWolf].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), "Freezing Bite", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.WinterWolf].Add(BuildData("Breath Weapon",
                damageHelper.BuildEntries("4d6", FeatConstants.Foci.Elements.Cold),
                string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
            testCases[CreatureConstants.WinterWolf].Add(BuildData("Freezing Bite",
                damageHelper.BuildEntries("1d6", FeatConstants.Foci.Elements.Cold),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
            testCases[CreatureConstants.WinterWolf].Add(BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.Wolf].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Wolf].Add(BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.Wolf_Dire].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Wolf_Dire].Add(BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.Wolverine].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Wolverine].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Wolverine].Add(BuildData("Rage", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));

            testCases[CreatureConstants.Wolverine_Dire].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Wolverine_Dire].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Wolverine_Dire].Add(BuildData("Rage", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));

            testCases[CreatureConstants.Worg].Add(BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Worg].Add(BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.Wraith].Add(BuildData("Incorporeal touch",
                damageHelper.BuildEntries("1d4"),
                $"Constitution Drain", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Wraith].Add(BuildData("Constitution Drain",
                damageHelper.BuildEntries("1d4", AbilityConstants.Constitution),
                "Wraith gains 5 temporary hit points", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Wraith].Add(BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Wraith_Dread].Add(BuildData("Incorporeal touch",
                damageHelper.BuildEntries("2d6"),
                $"Constitution Drain", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Wraith_Dread].Add(BuildData("Constitution Drain",
                damageHelper.BuildEntries("1d8", AbilityConstants.Constitution),
                "Dread Wraith gains 5 temporary hit points", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Wraith_Dread].Add(BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Wyvern].Add(BuildData("Sting", damageHelper.BuildEntries("1d6", stingDamageType), "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Wyvern].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Wyvern].Add(BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Wyvern].Add(BuildData("Talon", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Wyvern].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "2d6", AbilityConstants.Constitution, "Initial",
                    "2d6", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.Wyvern].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Xill].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 2, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Xill].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 2, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.Xill].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Xill].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Xill].Add(BuildData("Bite",
                damageHelper.BuildEntries("0", biteDamageType),
                "Paralysis", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Xill].Add(BuildData("Implant", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Xill].Add(BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Xill].Add(BuildData("Paralysis", string.Empty, "paralyzed for 1d4 hours", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.Xorn_Minor].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Xorn_Minor].Add(BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 0.5, "melee", 3, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Xorn_Average].Add(BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Xorn_Average].Add(BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 3, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Xorn_Elder].Add(BuildData("Bite", damageHelper.BuildEntries("4d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Xorn_Elder].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 3, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.YethHound].Add(BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.YethHound].Add(BuildData("Bay", string.Empty, "panicked 2d4 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
            testCases[CreatureConstants.YethHound].Add(BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            testCases[CreatureConstants.Yrthak].Add(BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Yrthak].Add(BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Yrthak].Add(BuildData("Sonic Lance",
                damageHelper.BuildEntries("6d6", FeatConstants.Foci.Elements.Sonic),
                string.Empty, 0, "ranged touch", 1, $"2 {FeatConstants.Frequencies.Round}", false, true, true, false));
            testCases[CreatureConstants.Yrthak].Add(BuildData("Explosion",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0, "supernatural ability", 1, $"2 {FeatConstants.Frequencies.Round}", true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            testCases[CreatureConstants.YuanTi_Pureblood].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.YuanTi_Pureblood].Add(BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.YuanTi_Pureblood].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.YuanTi_Pureblood].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(BuildData(AttributeConstants.Melee,
                string.Empty,
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(BuildData(AttributeConstants.Ranged,
                string.Empty,
                string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(BuildData("Produce Acid",
                damageHelper.BuildEntries(
                    "3d6", FeatConstants.Foci.Elements.Acid, string.Empty,
                    "2d6", FeatConstants.Foci.Elements.Acid, "Grappling or pinning foe"),
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(BuildData("Bite",
                damageHelper.BuildEntries("1d4", biteDamageType),
                "Poison", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(BuildData("Produce Acid",
                damageHelper.BuildEntries(
                    "3d6", FeatConstants.Foci.Elements.Acid, string.Empty,
                    "2d6", FeatConstants.Foci.Elements.Acid, "Grappling or pinning foe"),
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(BuildData(AttributeConstants.Melee,
                string.Empty,
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(BuildData(AttributeConstants.Ranged,
                string.Empty,
                string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(BuildData("Produce Acid",
                damageHelper.BuildEntries(
                    "3d6", FeatConstants.Foci.Elements.Acid, string.Empty,
                    "2d6", FeatConstants.Foci.Elements.Acid, "Grappling or pinning foe"),
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(BuildData("Constrict",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(BuildData(AttributeConstants.Melee,
                string.Empty,
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(BuildData(AttributeConstants.Ranged,
                string.Empty,
                string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(BuildData("Produce Acid",
                damageHelper.BuildEntries(
                    "3d6", FeatConstants.Foci.Elements.Acid, string.Empty,
                    "2d6", FeatConstants.Foci.Elements.Acid, "Grappling or pinning foe"),
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(BuildData("Constrict",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.YuanTi_Abomination].Add(BuildData(AttributeConstants.Melee,
                string.Empty,
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.YuanTi_Abomination].Add(BuildData(AttributeConstants.Ranged,
                string.Empty,
                string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            testCases[CreatureConstants.YuanTi_Abomination].Add(BuildData("Bite",
                damageHelper.BuildEntries("2d6", biteDamageType),
                "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.YuanTi_Abomination].Add(BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.YuanTi_Abomination].Add(BuildData("Aversion",
                string.Empty,
                "aversion 10 minutes", 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
            testCases[CreatureConstants.YuanTi_Abomination].Add(BuildData("Produce Acid",
                damageHelper.BuildEntries(
                    "3d6", FeatConstants.Foci.Elements.Acid, string.Empty,
                    "2d6", FeatConstants.Foci.Elements.Acid, "Grappling or pinning foe"),
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            testCases[CreatureConstants.YuanTi_Abomination].Add(BuildData("Constrict",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            testCases[CreatureConstants.YuanTi_Abomination].Add(BuildData("Improved Grab",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.YuanTi_Abomination].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            testCases[CreatureConstants.Zelekhut].Add(BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, false, true, false));
            testCases[CreatureConstants.Zelekhut].Add(BuildData("Unarmed Strike", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Zelekhut].Add(BuildData("Electrified Weapon",
                damageHelper.BuildEntries("1d6", FeatConstants.Foci.Elements.Electricity),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            testCases[CreatureConstants.Zelekhut].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            return testCases;
        }

        public static Dictionary<string, List<string>> GetTemplateAttackData()
        {
            var testCases = new Dictionary<string, List<string>>();
            var templates = CreatureConstants.Templates.GetAll();

            var biteDamageType = $"{AttributeConstants.DamageTypes.Piercing}/{AttributeConstants.DamageTypes.Slashing}/{AttributeConstants.DamageTypes.Bludgeoning}";
            var clawDamageType = $"{AttributeConstants.DamageTypes.Piercing}/{AttributeConstants.DamageTypes.Slashing}";
            var goreDamageType = $"{AttributeConstants.DamageTypes.Piercing}";
            var slapSlamDamageType = $"{AttributeConstants.DamageTypes.Bludgeoning}";
            var stingDamageType = $"{AttributeConstants.DamageTypes.Piercing}";
            var tentacleDamageType = $"{AttributeConstants.DamageTypes.Bludgeoning}";

            foreach (var template in templates)
            {
                testCases[template] = [];
            }

            testCases[CreatureConstants.Templates.CelestialCreature].Add(BuildData("Smite Evil",
                string.Empty,
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            testCases[CreatureConstants.Templates.FiendishCreature].Add(BuildData("Smite Good",
                string.Empty,
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            testCases[CreatureConstants.Templates.Ghost].Add(BuildData("Corrupting Gaze",
                damageHelper.BuildEntries(
                    "2d10", string.Empty, string.Empty,
                    "1d4", AbilityConstants.Charisma),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true,
                SaveConstants.Fortitude, AbilityConstants.Charisma));
            testCases[CreatureConstants.Templates.Ghost].Add(BuildData("Corrupting Touch",
                damageHelper.BuildEntries("1d6"),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            testCases[CreatureConstants.Templates.Ghost].Add(BuildData("Draining Touch",
                damageHelper.BuildEntries("1d4", "Ability points (of ghost's choosing)"),
                "Ghost heals 5 points of damage", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            testCases[CreatureConstants.Templates.Ghost].Add(BuildData("Frightful Moan",
                string.Empty,
                "Panic for 2d4 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true,
                SaveConstants.Will, AbilityConstants.Charisma));
            testCases[CreatureConstants.Templates.Ghost].Add(BuildData("Horrific Appearance",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Strength, string.Empty,
                    "1d4", AbilityConstants.Dexterity, string.Empty,
                    "1d4", AbilityConstants.Constitution),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true,
                SaveConstants.Fortitude, AbilityConstants.Charisma));
            testCases[CreatureConstants.Templates.Ghost].Add(BuildData("Malevolence",
                string.Empty,
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true,
                SaveConstants.Will, AbilityConstants.Charisma, 5));
            testCases[CreatureConstants.Templates.Ghost].Add(BuildData("Manifestation",
                string.Empty,
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Templates.Ghost].Add(BuildData(SpellConstants.Telekinesis,
                string.Empty,
                string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true,
                SaveConstants.Will, AbilityConstants.Charisma));

            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Templates.HalfCelestial].Add(BuildData("Smite Evil",
                string.Empty,
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            testCases[CreatureConstants.Templates.HalfDragon_Black].Add(BuildData("Claw",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.HalfDragon_Black].Add(BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.HalfDragon_Black].Add(BuildData("Breath Weapon",
                damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Acid),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                SaveConstants.Reflex, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.HalfDragon_Blue].Add(BuildData("Claw",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.HalfDragon_Blue].Add(BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.HalfDragon_Blue].Add(BuildData("Breath Weapon",
                damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Electricity),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                SaveConstants.Reflex, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.HalfDragon_Brass].Add(BuildData("Claw",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.HalfDragon_Brass].Add(BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.HalfDragon_Brass].Add(BuildData("Breath Weapon",
                damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                SaveConstants.Reflex, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.HalfDragon_Bronze].Add(BuildData("Claw",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.HalfDragon_Bronze].Add(BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.HalfDragon_Bronze].Add(BuildData("Breath Weapon",
                damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Electricity),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                SaveConstants.Reflex, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.HalfDragon_Copper].Add(BuildData("Claw",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.HalfDragon_Copper].Add(BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.HalfDragon_Copper].Add(BuildData("Breath Weapon",
                damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Acid),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                SaveConstants.Reflex, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.HalfDragon_Gold].Add(BuildData("Claw",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.HalfDragon_Gold].Add(BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.HalfDragon_Gold].Add(BuildData("Breath Weapon",
                damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                SaveConstants.Reflex, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.HalfDragon_Green].Add(BuildData("Claw",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.HalfDragon_Green].Add(BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.HalfDragon_Green].Add(BuildData("Breath Weapon",
                damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Acid, "Gas"),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                SaveConstants.Reflex, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.HalfDragon_Red].Add(BuildData("Claw",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.HalfDragon_Red].Add(BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.HalfDragon_Red].Add(BuildData("Breath Weapon",
                damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                SaveConstants.Reflex, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.HalfDragon_Silver].Add(BuildData("Claw",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.HalfDragon_Silver].Add(BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.HalfDragon_Silver].Add(BuildData("Breath Weapon",
                damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Cold),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                SaveConstants.Reflex, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.HalfDragon_White].Add(BuildData("Claw",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.HalfDragon_White].Add(BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.HalfDragon_White].Add(BuildData("Breath Weapon",
                damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Cold),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                SaveConstants.Reflex, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData("Claw",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Templates.HalfFiend].Add(BuildData("Smite Good",
                string.Empty,
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            testCases[CreatureConstants.Templates.Lich].Add(BuildData("Touch",
                damageHelper.BuildEntries("1d8+5"),
                "Paralyzing Touch", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false, SaveConstants.Will, AbilityConstants.Charisma));
            testCases[CreatureConstants.Templates.Lich].Add(BuildData("Fear Aura",
                string.Empty,
                "Fear", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Will, AbilityConstants.Charisma));
            testCases[CreatureConstants.Templates.Lich].Add(BuildData("Paralyzing Touch",
                string.Empty,
                "Paralyzed", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Charisma));

            testCases[CreatureConstants.Templates.None].Add(new[] { None });

            testCases[CreatureConstants.Templates.Skeleton].Add(BuildData("Claw",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Templates.Vampire].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Vampire].Add(BuildData("Blood Drain",
                damageHelper.BuildEntries("1d4", AbilityConstants.Constitution),
                "Vampire gains 5 temporary hit points", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Templates.Vampire].Add(BuildData("Children of the Night",
                string.Empty,
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Templates.Vampire].Add(BuildData("Dominate",
                string.Empty,
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
            testCases[CreatureConstants.Templates.Vampire].Add(BuildData("Create Spawn",
                string.Empty,
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Templates.Vampire].Add(BuildData("Energy Drain",
                damageHelper.BuildEntries("2", "Negative Level"),
                "Vampire gains 5 temporary hit points", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural].Add(BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural].Add(BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural].Add(BuildData("Curse of Lycanthropy",
                string.Empty,
                "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true,
                SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Natural].Add(BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Natural].Add(BuildData("Gore (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Natural].Add(BuildData("Curse of Lycanthropy",
                string.Empty,
                "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural].Add(BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural].Add(BuildData("Gore (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural].Add(BuildData("Curse of Lycanthropy",
                string.Empty,
                "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Natural].Add(BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Natural].Add(BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Natural].Add(BuildData("Curse of Lycanthropy",
                string.Empty,
                "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural].Add(BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural].Add(BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural].Add(BuildData("Curse of Lycanthropy",
                string.Empty,
                "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Natural].Add(BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Natural].Add(BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Natural].Add(BuildData("Curse of Lycanthropy",
                string.Empty,
                "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural].Add(BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural].Add(BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural].Add(BuildData("Curse of Lycanthropy",
                string.Empty,
                "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Natural].Add(BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Natural].Add(BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Natural].Add(BuildData("Curse of Lycanthropy",
                string.Empty,
                "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural].Add(BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural].Add(BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural].Add(BuildData("Curse of Lycanthropy",
                string.Empty,
                "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted].Add(BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted].Add(BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Afflicted].Add(BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Afflicted].Add(BuildData("Gore (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted].Add(BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted].Add(BuildData("Gore (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Afflicted].Add(BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Afflicted].Add(BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted].Add(BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted].Add(BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted].Add(BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted].Add(BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted].Add(BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted].Add(BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted].Add(BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted].Add(BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted].Add(BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted].Add(BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Templates.Zombie].Add(BuildData("Slam",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Black_Afflicted].Add(BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Black_Afflicted].Add(BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural].Add(BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural].Add(BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural].Add(BuildData("Curse of Lycanthropy",
                string.Empty,
                "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true,
                SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Dire_Afflicted].Add(BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Dire_Afflicted].Add(BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural].Add(BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural].Add(BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural].Add(BuildData("Curse of Lycanthropy",
                string.Empty,
                "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true,
                SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Polar_Afflicted].Add(BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Polar_Afflicted].Add(BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural].Add(BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural].Add(BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural].Add(BuildData("Curse of Lycanthropy",
                string.Empty,
                "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true,
                SaveConstants.Fortitude, AbilityConstants.Constitution));

            return testCases;
        }

        private static string[] BuildData(string name, string empty1, string empty2, int v2, string v3, int v4, string day, bool v5, bool v6, bool v7, bool v8)
        {
            return Infrastructure.Helpers.DataHelper.Parse(new AttackDataSelection
            {

            });
        }
    }
}
