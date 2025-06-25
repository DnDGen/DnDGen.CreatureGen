using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tests.Integration.Tables.Helpers;
using DnDGen.Infrastructure.Helpers;
using DnDGen.TreasureGen.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Attacks
{
    public static class DamageTestData
    {
        internal static Dictionary<string, List<string>> GetCreatureAttackDamageData(
            Dictionary<string, IEnumerable<AttackDataSelection>> attackDataSelections,
            Dictionary<string, CreatureDataSelection> creatureData,
            Dictionary<string, IEnumerable<AdvancementDataSelection>> advancementData,
            DamageHelper damageHelper)
        {
            var testCases = new Dictionary<string, List<string>>();
            var damageKeys = damageHelper.GetAllCreatureDamageKeys();

            foreach (var key in damageKeys)
                testCases[key] = [];

            var attackDamages = new List<Dictionary<string, List<string>>>();
            var attackData = attackDataSelections.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value
                    .GroupBy(a => a.Name)
                    .ToDictionary(g => g.Key, g => g.ToArray()));

            var biteDamageType = $"{AttributeConstants.DamageTypes.Piercing}/{AttributeConstants.DamageTypes.Slashing}/{AttributeConstants.DamageTypes.Bludgeoning}";
            var clawDamageType = $"{AttributeConstants.DamageTypes.Piercing}/{AttributeConstants.DamageTypes.Slashing}";
            var goreDamageType = $"{AttributeConstants.DamageTypes.Piercing}";
            var slapSlamDamageType = $"{AttributeConstants.DamageTypes.Bludgeoning}";
            var stingDamageType = $"{AttributeConstants.DamageTypes.Piercing}";
            var tentacleDamageType = $"{AttributeConstants.DamageTypes.Bludgeoning}";

            Dictionary<string, List<string>> BuildData(string creature, string attackName,
                string roll, string type = "", string condition = "",
                string roll2 = null, string type2 = null, string condition2 = null,
                string roll3 = null, string type3 = null, string condition3 = null,
                string roll4 = null, string type4 = null, string condition4 = null)
            {
                return BuildDamageDataForAttack(
                    creature,
                    creatureData[creature].Size,
                    attackData[creature][attackName],
                    advancementData[creature].Select(a => a.Size),
                    [(roll, type, condition),
                    (roll2, type2, condition2),
                    (roll3, type3, condition3),
                    (roll4, type4, condition4)]);
            }

            attackDamages.Add(BuildData(CreatureConstants.Aasimar, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Aboleth, "Tentacle", "1d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Achaierai, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Achaierai, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Achaierai, "Black cloud", "2d6"));

            attackDamages.Add(BuildData(CreatureConstants.Allip, "Madness", "1d4", AbilityConstants.Wisdom));
            attackDamages.Add(BuildData(CreatureConstants.Allip, "Wisdom drain", "1d4", AbilityConstants.Wisdom));

            attackDamages.Add(BuildData(CreatureConstants.Androsphinx, "Claw", "2d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Androsphinx, "Rake", "2d4", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Angel_AstralDeva, "Slam", "1d8", slapSlamDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Angel_Planetar, "Slam", "2d8", slapSlamDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Angel_Solar, "Slam", "2d8", slapSlamDamageType));

            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Tiny, "Slam", "1d3", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Tiny_Flexible, "Slam", "1d3", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Tiny_Flexible, "Constrict", "1d3", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Tiny_MultipleLegs, "Slam", "1d3", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden, "Slam", "1d3", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Tiny_Sheetlike, "Slam", "1d3", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Tiny_Sheetlike, "Constrict", "1d3", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Tiny_TwoLegs, "Slam", "1d3", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden, "Slam", "1d3", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden, "Slam", "1d3", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Tiny_Wooden, "Slam", "1d3", slapSlamDamageType));

            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Small, "Slam", "1d4", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Small_Flexible, "Slam", "1d4", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Small_Flexible, "Constrict", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Small_MultipleLegs, "Slam", "1d4", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden, "Slam", "1d4", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Small_Sheetlike, "Slam", "1d4", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Small_Sheetlike, "Constrict", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Small_TwoLegs, "Slam", "1d4", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden, "Slam", "1d4", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Small_Wheels_Wooden, "Slam", "1d4", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Small_Wooden, "Slam", "1d4", slapSlamDamageType));

            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Medium, "Slam", "1d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Medium_Flexible, "Slam", "1d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Medium_Flexible, "Constrict", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Medium_MultipleLegs, "Slam", "1d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden, "Slam", "1d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Medium_Sheetlike, "Slam", "1d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Medium_Sheetlike, "Constrict", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Medium_TwoLegs, "Slam", "1d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden, "Slam", "1d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Medium_Wheels_Wooden, "Slam", "1d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Medium_Wooden, "Slam", "1d6", slapSlamDamageType));

            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large, "Slam", "1d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large, "Trample", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large_Flexible, "Slam", "1d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large_Flexible, "Trample", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large_Flexible, "Constrict", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large_MultipleLegs_Long, "Slam", "1d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large_MultipleLegs_Long, "Trample", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large_MultipleLegs_Long_Wooden, "Slam", "1d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large_MultipleLegs_Long_Wooden, "Trample", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large_MultipleLegs_Tall, "Slam", "1d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large_MultipleLegs_Tall, "Trample", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large_MultipleLegs_Tall_Wooden, "Slam", "1d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large_MultipleLegs_Tall_Wooden, "Trample", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large_Sheetlike, "Slam", "1d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large_Sheetlike, "Trample", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large_Sheetlike, "Constrict", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large_TwoLegs, "Slam", "1d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large_TwoLegs, "Trample", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden, "Slam", "1d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden, "Trample", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large_Wheels_Wooden, "Slam", "1d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large_Wheels_Wooden, "Trample", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large_Wooden, "Slam", "1d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large_Wooden, "Trample", "1d8", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge, "Slam", "2d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge, "Trample", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge_Flexible, "Slam", "2d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge_Flexible, "Trample", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge_Flexible, "Constrict", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge_MultipleLegs_Long, "Slam", "2d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge_MultipleLegs_Long, "Trample", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge_MultipleLegs_Long_Wooden, "Slam", "2d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge_MultipleLegs_Long_Wooden, "Trample", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge_MultipleLegs_Tall, "Slam", "2d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge_MultipleLegs_Tall, "Trample", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge_MultipleLegs_Tall_Wooden, "Slam", "2d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge_MultipleLegs_Tall_Wooden, "Trample", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge_Sheetlike, "Slam", "2d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge_Sheetlike, "Trample", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge_Sheetlike, "Constrict", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge_TwoLegs, "Slam", "2d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge_TwoLegs, "Trample", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden, "Slam", "2d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden, "Trample", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge_Wheels_Wooden, "Slam", "2d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge_Wheels_Wooden, "Trample", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge_Wooden, "Slam", "2d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge_Wooden, "Trample", "2d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan, "Slam", "2d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan, "Trample", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan_Flexible, "Slam", "2d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan_Flexible, "Trample", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan_Flexible, "Constrict", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Long, "Slam", "2d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Long, "Trample", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Long_Wooden, "Slam", "2d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Long_Wooden, "Trample", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Tall, "Slam", "2d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Tall, "Trample", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Tall_Wooden, "Slam", "2d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Tall_Wooden, "Trample", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan_Sheetlike, "Slam", "2d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan_Sheetlike, "Trample", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan_Sheetlike, "Constrict", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan_TwoLegs, "Slam", "2d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan_TwoLegs, "Trample", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden, "Slam", "2d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden, "Trample", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden, "Slam", "2d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden, "Trample", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan_Wooden, "Slam", "2d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan_Wooden, "Trample", "2d8", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal, "Slam", "4d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal, "Trample", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal_Flexible, "Slam", "4d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal_Flexible, "Trample", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal_Flexible, "Constrict", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Long, "Slam", "4d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Long, "Trample", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Long_Wooden, "Slam", "4d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Long_Wooden, "Trample", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Tall, "Slam", "4d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Tall, "Trample", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Tall_Wooden, "Slam", "4d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Tall_Wooden, "Trample", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal_Sheetlike, "Slam", "4d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal_Sheetlike, "Trample", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal_Sheetlike, "Constrict", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal_TwoLegs, "Slam", "4d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal_TwoLegs, "Trample", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden, "Slam", "4d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden, "Trample", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden, "Slam", "4d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden, "Trample", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal_Wooden, "Slam", "4d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal_Wooden, "Trample", "4d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Ankheg, "Bite", "2d6", biteDamageType, roll2: "1d4", type2: FeatConstants.Foci.Elements.Acid));
            attackDamages.Add(BuildData(CreatureConstants.Ankheg, "Spit Acid", "4d4", FeatConstants.Foci.Elements.Acid));

            attackDamages.Add(BuildData(CreatureConstants.Annis, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Annis, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Annis, "Rake", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Annis, "Rend", "2d6", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Ant_Giant_Worker, "Bite", "1d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Ant_Giant_Soldier, "Bite", "2d4", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Ant_Giant_Soldier, "Acid Sting", "1d4", stingDamageType, roll2: "1d4", type2: FeatConstants.Foci.Elements.Acid));

            attackDamages.Add(BuildData(CreatureConstants.Ant_Giant_Queen, "Bite", "2d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Ape, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Ape, "Bite", "1d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Ape_Dire, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Ape_Dire, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Ape_Dire, "Rend", "2d6", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Aranea, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Aranea, "Poison", "1d6", AbilityConstants.Strength, "Initial", "2d6", AbilityConstants.Strength, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Arrowhawk_Juvenile, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Arrowhawk_Juvenile, "Electricity ray", "2d6", FeatConstants.Foci.Elements.Electricity));

            attackDamages.Add(BuildData(CreatureConstants.Arrowhawk_Adult, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Arrowhawk_Adult, "Electricity ray", "2d8", FeatConstants.Foci.Elements.Electricity));

            attackDamages.Add(BuildData(CreatureConstants.Arrowhawk_Elder, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Arrowhawk_Elder, "Electricity ray", "2d8", FeatConstants.Foci.Elements.Electricity));

            attackDamages.Add(BuildData(CreatureConstants.AssassinVine, "Slam", "1d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AssassinVine, "Constrict", "1d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Athach, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Athach, "Rock", "2d8", AttributeConstants.DamageTypes.Bludgeoning)); //primary
            //HACK: Keeping in case there are primary/secondary attacks with same name that have different damage
            //attackDamages.Add(BuildData(CreatureConstants.Athach, "Rock", "2d8", AttributeConstants.DamageTypes.Bludgeoning)); //secondary
            attackDamages.Add(BuildData(CreatureConstants.Athach, "Poison", "1d6", AbilityConstants.Strength, "Initial", "2d6", AbilityConstants.Strength, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Avoral, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Avoral, "Wing", "2d8", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Azer, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Azer, "Heat", "1", FeatConstants.Foci.Elements.Fire));

            attackDamages.Add(BuildData(CreatureConstants.Babau, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Babau, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Babau, "Sneak Attack", "2d6"));

            attackDamages.Add(BuildData(CreatureConstants.Baboon, "Bite", "1d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Badger, "Claw", "1d2", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Badger, "Bite", "1d3", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Badger_Dire, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Badger_Dire, "Bite", "1d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Balor, "Slam", "1d10", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Balor, "Death Throes", "100"));

            attackDamages.Add(BuildData(CreatureConstants.BarbedDevil_Hamatula, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.BarbedDevil_Hamatula, "Impale", "3d8", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.Barghest, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Barghest, "Claw", "1d4", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Barghest_Greater, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Barghest_Greater, "Claw", "1d6", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Basilisk, "Bite", "1d8", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Basilisk_Greater, "Bite", "2d8", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Bat_Dire, "Bite", "1d8", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Bat_Swarm, "Swarm", "1d6"));
            attackDamages.Add(BuildData(CreatureConstants.Bat_Swarm, "Wounding", "1"));

            attackDamages.Add(BuildData(CreatureConstants.Bear_Black, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Bear_Black, "Bite", "1d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Bear_Brown, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Bear_Brown, "Bite", "2d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Bear_Dire, "Claw", "2d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Bear_Dire, "Bite", "2d8", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Bear_Polar, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Bear_Polar, "Bite", "2d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.BeardedDevil_Barbazu, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.BeardedDevil_Barbazu, "Infernal Wound", "2"));
            attackDamages.Add(BuildData(CreatureConstants.BeardedDevil_Barbazu, "Beard", "1d8"));
            attackDamages.Add(BuildData(CreatureConstants.BeardedDevil_Barbazu, "Devil Chills", "1d4", AbilityConstants.Strength, "Incubation period 1d4 days"));

            attackDamages.Add(BuildData(CreatureConstants.Bebilith, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Bebilith, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Bebilith, "Poison",
                "1d6", AbilityConstants.Constitution, "Initial",
                "2d6", AbilityConstants.Constitution, "Secondary"));
            attackDamages.Add(BuildData(CreatureConstants.Bebilith, "Rend Armor", "4d6"));

            attackDamages.Add(BuildData(CreatureConstants.Bee_Giant, "Sting", "1d4", stingDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Bee_Giant, "Poison",
                "1d4", AbilityConstants.Constitution, "Initial",
                "1d4", AbilityConstants.Constitution, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Behir, "Bite", "2d4", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Behir, "Breath Weapon", "7d6", FeatConstants.Foci.Elements.Electricity));
            attackDamages.Add(BuildData(CreatureConstants.Behir, "Constrict", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Behir, "Rake", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Behir, "Swallow Whole",
                "2d8+8", AttributeConstants.DamageTypes.Bludgeoning,
                roll2: "8", type2: FeatConstants.Foci.Elements.Acid));

            attackDamages.Add(BuildData(CreatureConstants.Beholder, "Bite", "2d4", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Beholder_Gauth, "Bite", "1d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Belker, "Wing", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Belker, "Bite", "1d4", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Belker, "Claw", "1d3", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Belker, "Smoke Claw", "3d4", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Bison, "Gore", "1d8", goreDamageType));

            attackDamages.Add(BuildData(CreatureConstants.BlackPudding, "Slam", "2d6", slapSlamDamageType, roll2: "2d6", type2: FeatConstants.Foci.Elements.Acid));
            attackDamages.Add(BuildData(CreatureConstants.BlackPudding, "Constrict",
                "2d6", AttributeConstants.DamageTypes.Bludgeoning,
                roll2: "2d6", type2: FeatConstants.Foci.Elements.Acid));

            attackDamages.Add(BuildData(CreatureConstants.BlackPudding_Elder, "Slam", "3d6", slapSlamDamageType, roll2: "3d6", type2: FeatConstants.Foci.Elements.Acid));
            attackDamages.Add(BuildData(CreatureConstants.BlackPudding_Elder, "Constrict",
                "2d8", AttributeConstants.DamageTypes.Bludgeoning,
                roll2: "2d6", type2: FeatConstants.Foci.Elements.Acid));

            attackDamages.Add(BuildData(CreatureConstants.BlinkDog, "Bite", "1d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Boar, "Gore", "1d8", goreDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Boar_Dire, "Gore", "1d8", goreDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Bodak, "Slam", "1d8", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.BombardierBeetle_Giant, "Bite", "1d4", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.BombardierBeetle_Giant, "Acid Spray", "1d4", FeatConstants.Foci.Elements.Acid));

            attackDamages.Add(BuildData(CreatureConstants.BoneDevil_Osyluth, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.BoneDevil_Osyluth, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.BoneDevil_Osyluth, "Sting", "3d4", AttributeConstants.DamageTypes.Piercing));
            attackDamages.Add(BuildData(CreatureConstants.BoneDevil_Osyluth, "Poison",
                "1d6", AbilityConstants.Strength, "Initial",
                "2d6", AbilityConstants.Strength, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Bralani, "Slam", "1d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Bralani, "Whirlwind blast", "3d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Bugbear, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Bulette, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Bulette, "Claw", "2d6", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Camel_Bactrian, "Bite", "1d4", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Camel_Dromedary, "Bite", "1d4", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.CarrionCrawler, "Tentacle", "0", tentacleDamageType));
            attackDamages.Add(BuildData(CreatureConstants.CarrionCrawler, "Bite", "1d4", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Cat, "Claw", "1d2", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Cat, "Bite", "1d3", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Centaur, "Unarmed Strike", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Centaur, "Hoof", "1d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Centipede_Monstrous_Tiny, "Bite", "1d3", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Centipede_Monstrous_Tiny, "Poison",
                "1", AbilityConstants.Dexterity, "Initial",
                "1", AbilityConstants.Dexterity, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Centipede_Monstrous_Small, "Bite", "1d4", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Centipede_Monstrous_Small, "Poison",
                "1d2", AbilityConstants.Dexterity, "Initial",
                "1d2", AbilityConstants.Dexterity, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Centipede_Monstrous_Medium, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Centipede_Monstrous_Medium, "Poison",
                "1d3", AbilityConstants.Dexterity, "Initial",
                "1d3", AbilityConstants.Dexterity, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Centipede_Monstrous_Large, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Centipede_Monstrous_Large, "Poison",
                "1d4", AbilityConstants.Dexterity, "Initial",
                "1d4", AbilityConstants.Dexterity, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Centipede_Monstrous_Huge, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Centipede_Monstrous_Huge, "Poison",
                "1d6", AbilityConstants.Dexterity, "Initial",
                "1d6", AbilityConstants.Dexterity, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Centipede_Monstrous_Gargantuan, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Centipede_Monstrous_Gargantuan, "Poison",
                "1d8", AbilityConstants.Dexterity, "Initial",
                "1d8", AbilityConstants.Dexterity, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Centipede_Monstrous_Colossal, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Centipede_Monstrous_Colossal, "Poison",
                "2d6", AbilityConstants.Dexterity, "Initial",
                "2d6", AbilityConstants.Dexterity, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Centipede_Swarm, "Swarm", "2d6"));
            attackDamages.Add(BuildData(CreatureConstants.Centipede_Swarm, "Poison",
                "1d4", AbilityConstants.Dexterity, "Initial",
                "1d4", AbilityConstants.Dexterity, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.ChainDevil_Kyton, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.ChaosBeast, "Claw", "1d3", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Cheetah, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Cheetah, "Claw", "1d2", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Chimera_Black, "Bite (Dragon)", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Chimera_Black, "Bite (Lion)", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Chimera_Black, "Gore (Goat)", "1d8", goreDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Chimera_Black, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Chimera_Black, "Breath weapon", "3d8", FeatConstants.Foci.Elements.Acid));

            attackDamages.Add(BuildData(CreatureConstants.Chimera_Blue, "Bite (Dragon)", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Chimera_Blue, "Bite (Lion)", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Chimera_Blue, "Gore (Goat)", "1d8", goreDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Chimera_Blue, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Chimera_Blue, "Breath weapon", "3d8", FeatConstants.Foci.Elements.Electricity));

            attackDamages.Add(BuildData(CreatureConstants.Chimera_Green, "Bite (Dragon)", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Chimera_Green, "Bite (Lion)", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Chimera_Green, "Gore (Goat)", "1d8", goreDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Chimera_Green, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Chimera_Green, "Breath weapon", "3d8", FeatConstants.Foci.Elements.Acid));

            attackDamages.Add(BuildData(CreatureConstants.Chimera_Red, "Bite (Dragon)", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Chimera_Red, "Bite (Lion)", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Chimera_Red, "Gore (Goat)", "1d8", goreDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Chimera_Red, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Chimera_Red, "Breath weapon", "3d8", FeatConstants.Foci.Elements.Fire));

            attackDamages.Add(BuildData(CreatureConstants.Chimera_White, "Bite (Dragon)", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Chimera_White, "Bite (Lion)", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Chimera_White, "Gore (Goat)", "1d8", goreDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Chimera_White, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Chimera_White, "Breath weapon", "3d8", FeatConstants.Foci.Elements.Cold));

            attackDamages.Add(BuildData(CreatureConstants.Choker, "Tentacle", "1d3", tentacleDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Choker, "Constrict", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Chuul, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Chuul, "Constrict", "3d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Chuul, "Paralytic Tentacles", "1d8", tentacleDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Cloaker, "Tail slap", "1d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Cloaker, "Bite", "1d4", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Cockatrice, "Bite", "1d4", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Couatl, "Bite", "1d3", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Couatl, "Poison",
                "2d4", AbilityConstants.Strength, "Initial",
                "4d4", AbilityConstants.Strength, "Secondary"));
            attackDamages.Add(BuildData(CreatureConstants.Couatl, "Constrict", "2d8", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Criosphinx, "Gore", "2d6", goreDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Criosphinx, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Criosphinx, "Rake", "1d6", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Crocodile, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Crocodile, "Tail Slap", "1d12", slapSlamDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Crocodile_Giant, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Crocodile_Giant, "Tail Slap", "1d12", slapSlamDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Cryohydra_5Heads, "Bite", "1d10", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Cryohydra_5Heads, "Breath weapon", "3d6", FeatConstants.Foci.Elements.Cold, "Per living head"));

            attackDamages.Add(BuildData(CreatureConstants.Cryohydra_6Heads, "Bite", "1d10", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Cryohydra_6Heads, "Breath weapon", "3d6", FeatConstants.Foci.Elements.Cold, "Per living head"));

            attackDamages.Add(BuildData(CreatureConstants.Cryohydra_7Heads, "Bite", "1d10", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Cryohydra_7Heads, "Breath weapon", "3d6", FeatConstants.Foci.Elements.Cold, "Per living head"));

            attackDamages.Add(BuildData(CreatureConstants.Cryohydra_8Heads, "Bite", "1d10", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Cryohydra_8Heads, "Breath weapon", "3d6", FeatConstants.Foci.Elements.Cold, "Per living head"));

            attackDamages.Add(BuildData(CreatureConstants.Cryohydra_9Heads, "Bite", "1d10", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Cryohydra_9Heads, "Breath weapon", "3d6", FeatConstants.Foci.Elements.Cold, "Per living head"));

            attackDamages.Add(BuildData(CreatureConstants.Cryohydra_10Heads, "Bite", "1d10", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Cryohydra_10Heads, "Breath weapon", "3d6", FeatConstants.Foci.Elements.Cold, "Per living head"));

            attackDamages.Add(BuildData(CreatureConstants.Cryohydra_11Heads, "Bite", "1d10", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Cryohydra_11Heads, "Breath weapon", "3d6", FeatConstants.Foci.Elements.Cold, "Per living head"));

            attackDamages.Add(BuildData(CreatureConstants.Cryohydra_12Heads, "Bite", "1d10", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Cryohydra_12Heads, "Breath weapon", "3d6", FeatConstants.Foci.Elements.Cold, "Per living head"));

            attackDamages.Add(BuildData(CreatureConstants.Darkmantle, "Slam", "1d4", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Darkmantle, "Constrict", "1d4", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Deinonychus, "Talons", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Deinonychus, "Foreclaw", "1d3", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Deinonychus, "Bite", "2d4", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Delver, "Slam", "1d6", slapSlamDamageType, roll2: "2d6", type2: FeatConstants.Foci.Elements.Acid));

            attackDamages.Add(BuildData(CreatureConstants.Derro, "Unarmed Strike", "1d2", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Derro, "Greenblood Oil",
                "1", AbilityConstants.Constitution, "Initial",
                "1d2", AbilityConstants.Constitution, "Secondary"));
            attackDamages.Add(BuildData(CreatureConstants.Derro, "Monstrous Spider Venom",
                "1d4", AbilityConstants.Strength, "Initial",
                "1d4", AbilityConstants.Strength, "Secondary"));
            attackDamages.Add(BuildData(CreatureConstants.Derro, "Sneak Attack", "1d6"));

            attackDamages.Add(BuildData(CreatureConstants.Derro_Sane, "Unarmed Strike", "1d2", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Derro_Sane, "Greenblood Oil",
                "1", AbilityConstants.Constitution, "Initial",
                "1d2", AbilityConstants.Constitution, "Secondary"));
            attackDamages.Add(BuildData(CreatureConstants.Derro_Sane, "Monstrous Spider Venom",
                "1d4", AbilityConstants.Strength, "Initial",
                "1d4", AbilityConstants.Strength, "Secondary"));
            attackDamages.Add(BuildData(CreatureConstants.Derro_Sane, "Sneak Attack", "1d6"));

            attackDamages.Add(BuildData(CreatureConstants.Destrachan, "Claw", "1d6", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Devourer, "Claw", "1d6", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Digester, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Digester, "Acid Spray (Cone)", "4d8", FeatConstants.Foci.Elements.Acid));
            attackDamages.Add(BuildData(CreatureConstants.Digester, "Acid Spray (Stream)", "8d8", FeatConstants.Foci.Elements.Acid));

            attackDamages.Add(BuildData(CreatureConstants.DisplacerBeast, "Tentacle", "1d6", tentacleDamageType));
            attackDamages.Add(BuildData(CreatureConstants.DisplacerBeast, "Bite", "1d8", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.DisplacerBeast_PackLord, "Tentacle", "1d8", tentacleDamageType));
            attackDamages.Add(BuildData(CreatureConstants.DisplacerBeast_PackLord, "Bite", "2d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Djinni, "Slam", "1d8", slapSlamDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Djinni_Noble, "Slam", "1d8", slapSlamDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Dog, "Bite", "1d4", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Dog_Riding, "Bite", "1d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Donkey, "Bite", "1d2", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Doppelganger, "Slam", "1d6", slapSlamDamageType));

            attackDamages.Add(BuildData(CreatureConstants.DragonTurtle, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.DragonTurtle, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.DragonTurtle, "Breath Weapon", "12d6", FeatConstants.Foci.Elements.Fire));

            //Tiny
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Wyrmling, "Bite", "1d4", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Wyrmling, "Claw", "1d3", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Wyrmling, "Breath Weapon", "2d4", FeatConstants.Foci.Elements.Acid));

            //small
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_VeryYoung, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_VeryYoung, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_VeryYoung, "Breath Weapon", "4d4", FeatConstants.Foci.Elements.Acid));

            //medium
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Young, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Young, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Young, "Wing", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Young, "Breath Weapon", "6d4", FeatConstants.Foci.Elements.Acid));

            //medium
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Juvenile, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Juvenile, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Juvenile, "Wing", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Juvenile, "Breath Weapon", "8d4", FeatConstants.Foci.Elements.Acid));

            //large
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_YoungAdult, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_YoungAdult, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_YoungAdult, "Wing", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_YoungAdult, "Tail Slap", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_YoungAdult, "Breath Weapon", "10d4", FeatConstants.Foci.Elements.Acid));

            //large
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Adult, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Adult, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Adult, "Wing", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Adult, "Tail Slap", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Adult, "Breath Weapon", "12d4", FeatConstants.Foci.Elements.Acid));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_MatureAdult, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_MatureAdult, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_MatureAdult, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_MatureAdult, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_MatureAdult, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_MatureAdult, "Breath Weapon", "14d4", FeatConstants.Foci.Elements.Acid));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Old, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Old, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Old, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Old, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Old, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Old, "Breath Weapon", "16d4", FeatConstants.Foci.Elements.Acid));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_VeryOld, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_VeryOld, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_VeryOld, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_VeryOld, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_VeryOld, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_VeryOld, "Breath Weapon", "18d4", FeatConstants.Foci.Elements.Acid));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Ancient, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Ancient, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Ancient, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Ancient, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Ancient, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Ancient, "Breath Weapon", "20d4", FeatConstants.Foci.Elements.Acid));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Wyrm, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Wyrm, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Wyrm, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Wyrm, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Wyrm, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Wyrm, "Tail Sweep", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_Wyrm, "Breath Weapon", "22d4", FeatConstants.Foci.Elements.Acid));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_GreatWyrm, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_GreatWyrm, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_GreatWyrm, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_GreatWyrm, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_GreatWyrm, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_GreatWyrm, "Tail Sweep", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Black_GreatWyrm, "Breath Weapon", "24d4", FeatConstants.Foci.Elements.Acid));

            //small
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Wyrmling, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Wyrmling, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Wyrmling, "Breath Weapon", "2d8", FeatConstants.Foci.Elements.Electricity));

            //medium
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_VeryYoung, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_VeryYoung, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_VeryYoung, "Wing", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_VeryYoung, "Breath Weapon", "4d8", FeatConstants.Foci.Elements.Electricity));

            //medium
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Young, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Young, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Young, "Wing", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Young, "Breath Weapon", "6d8", FeatConstants.Foci.Elements.Electricity));

            //large
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Juvenile, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Juvenile, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Juvenile, "Wing", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Juvenile, "Tail Slap", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Juvenile, "Breath Weapon", "8d8", FeatConstants.Foci.Elements.Electricity));

            //large
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_YoungAdult, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_YoungAdult, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_YoungAdult, "Wing", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_YoungAdult, "Tail Slap", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_YoungAdult, "Breath Weapon", "10d8", FeatConstants.Foci.Elements.Electricity));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Adult, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Adult, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Adult, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Adult, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Adult, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Adult, "Breath Weapon", "12d8", FeatConstants.Foci.Elements.Electricity));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_MatureAdult, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_MatureAdult, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_MatureAdult, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_MatureAdult, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_MatureAdult, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_MatureAdult, "Breath Weapon", "14d8", FeatConstants.Foci.Elements.Electricity));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Old, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Old, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Old, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Old, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Old, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Old, "Breath Weapon", "16d8", FeatConstants.Foci.Elements.Electricity));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_VeryOld, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_VeryOld, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_VeryOld, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_VeryOld, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_VeryOld, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_VeryOld, "Breath Weapon", "18d8", FeatConstants.Foci.Elements.Electricity));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Ancient, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Ancient, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Ancient, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Ancient, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Ancient, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Ancient, "Tail Sweep", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Ancient, "Breath Weapon", "20d8", FeatConstants.Foci.Elements.Electricity));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Wyrm, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Wyrm, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Wyrm, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Wyrm, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Wyrm, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Wyrm, "Tail Sweep", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_Wyrm, "Breath Weapon", "22d8", FeatConstants.Foci.Elements.Electricity));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_GreatWyrm, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_GreatWyrm, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_GreatWyrm, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_GreatWyrm, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_GreatWyrm, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_GreatWyrm, "Tail Sweep", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Blue_GreatWyrm, "Breath Weapon", "24d8", FeatConstants.Foci.Elements.Electricity));

            //small
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Wyrmling, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Wyrmling, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Wyrmling, "Breath Weapon", "2d6", FeatConstants.Foci.Elements.Acid, "Gas"));

            //medium
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_VeryYoung, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_VeryYoung, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_VeryYoung, "Wing", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_VeryYoung, "Breath Weapon", "4d6", FeatConstants.Foci.Elements.Acid, "Gas"));

            //medium
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Young, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Young, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Young, "Wing", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Young, "Breath Weapon", "6d6", FeatConstants.Foci.Elements.Acid, "Gas"));

            //large
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Juvenile, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Juvenile, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Juvenile, "Wing", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Juvenile, "Tail Slap", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Juvenile, "Breath Weapon", "8d6", FeatConstants.Foci.Elements.Acid, "Gas"));

            //large
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_YoungAdult, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_YoungAdult, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_YoungAdult, "Wing", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_YoungAdult, "Tail Slap", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_YoungAdult, "Breath Weapon", "10d6", FeatConstants.Foci.Elements.Acid, "Gas"));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Adult, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Adult, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Adult, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Adult, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Adult, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Adult, "Breath Weapon", "12d6", FeatConstants.Foci.Elements.Acid, "Gas"));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_MatureAdult, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_MatureAdult, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_MatureAdult, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_MatureAdult, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_MatureAdult, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_MatureAdult, "Breath Weapon", "14d6", FeatConstants.Foci.Elements.Acid, "Gas"));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Old, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Old, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Old, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Old, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Old, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Old, "Breath Weapon", "16d6", FeatConstants.Foci.Elements.Acid, "Gas"));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_VeryOld, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_VeryOld, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_VeryOld, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_VeryOld, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_VeryOld, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_VeryOld, "Breath Weapon", "18d6", FeatConstants.Foci.Elements.Acid, "Gas"));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Ancient, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Ancient, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Ancient, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Ancient, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Ancient, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Ancient, "Tail Sweep", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Ancient, "Breath Weapon", "20d6", FeatConstants.Foci.Elements.Acid, "Gas"));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Wyrm, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Wyrm, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Wyrm, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Wyrm, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Wyrm, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Wyrm, "Tail Sweep", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_Wyrm, "Breath Weapon", "22d6", FeatConstants.Foci.Elements.Acid, "Gas"));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_GreatWyrm, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_GreatWyrm, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_GreatWyrm, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_GreatWyrm, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_GreatWyrm, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_GreatWyrm, "Tail Sweep", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Green_GreatWyrm, "Breath Weapon", "24d6", FeatConstants.Foci.Elements.Acid, "Gas"));

            //medium
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Wyrmling, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Wyrmling, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Wyrmling, "Wing", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Wyrmling, "Breath Weapon", "2d10", FeatConstants.Foci.Elements.Fire));

            //large
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_VeryYoung, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_VeryYoung, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_VeryYoung, "Wing", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_VeryYoung, "Tail Slap", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_VeryYoung, "Breath Weapon", "4d10", FeatConstants.Foci.Elements.Fire));

            //large
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Young, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Young, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Young, "Wing", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Young, "Tail Slap", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Young, "Breath Weapon", "6d10", FeatConstants.Foci.Elements.Fire));

            //large
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Juvenile, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Juvenile, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Juvenile, "Wing", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Juvenile, "Tail Slap", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Juvenile, "Breath Weapon", "8d10", FeatConstants.Foci.Elements.Fire));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_YoungAdult, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_YoungAdult, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_YoungAdult, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_YoungAdult, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_YoungAdult, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_YoungAdult, "Breath Weapon", "10d10", FeatConstants.Foci.Elements.Fire));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Adult, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Adult, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Adult, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Adult, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Adult, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Adult, "Breath Weapon", "12d10", FeatConstants.Foci.Elements.Fire));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_MatureAdult, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_MatureAdult, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_MatureAdult, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_MatureAdult, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_MatureAdult, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_MatureAdult, "Breath Weapon", "14d10", FeatConstants.Foci.Elements.Fire));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Old, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Old, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Old, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Old, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Old, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Old, "Tail Sweep", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Old, "Breath Weapon", "16d10", FeatConstants.Foci.Elements.Fire));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_VeryOld, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_VeryOld, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_VeryOld, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_VeryOld, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_VeryOld, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_VeryOld, "Tail Sweep", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_VeryOld, "Breath Weapon", "18d10", FeatConstants.Foci.Elements.Fire));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Ancient, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Ancient, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Ancient, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Ancient, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Ancient, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Ancient, "Tail Sweep", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Ancient, "Breath Weapon", "20d10", FeatConstants.Foci.Elements.Fire));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Wyrm, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Wyrm, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Wyrm, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Wyrm, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Wyrm, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Wyrm, "Tail Sweep", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_Wyrm, "Breath Weapon", "22d10", FeatConstants.Foci.Elements.Fire));

            //colossal
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_GreatWyrm, "Bite", "4d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_GreatWyrm, "Claw", "4d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_GreatWyrm, "Wing", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_GreatWyrm, "Tail Slap", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_GreatWyrm, "Crush", "4d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_GreatWyrm, "Tail Sweep", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Red_GreatWyrm, "Breath Weapon", "24d10", FeatConstants.Foci.Elements.Fire));

            //tiny
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Wyrmling, "Bite", "1d4", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Wyrmling, "Claw", "1d3", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Wyrmling, "Breath Weapon", "1d6", FeatConstants.Foci.Elements.Cold));

            //small
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_VeryYoung, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_VeryYoung, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_VeryYoung, "Breath Weapon", "2d6", FeatConstants.Foci.Elements.Cold));

            //medium
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Young, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Young, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Young, "Wing", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Young, "Breath Weapon", "3d6", FeatConstants.Foci.Elements.Cold));

            //medium
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Juvenile, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Juvenile, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Juvenile, "Wing", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Juvenile, "Breath Weapon", "4d6", FeatConstants.Foci.Elements.Cold));

            //large
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_YoungAdult, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_YoungAdult, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_YoungAdult, "Wing", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_YoungAdult, "Tail Slap", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_YoungAdult, "Breath Weapon", "5d6", FeatConstants.Foci.Elements.Cold));

            //large
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Adult, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Adult, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Adult, "Wing", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Adult, "Tail Slap", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Adult, "Breath Weapon", "6d6", FeatConstants.Foci.Elements.Cold));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_MatureAdult, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_MatureAdult, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_MatureAdult, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_MatureAdult, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_MatureAdult, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_MatureAdult, "Breath Weapon", "7d6", FeatConstants.Foci.Elements.Cold));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Old, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Old, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Old, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Old, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Old, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Old, "Breath Weapon", "8d6", FeatConstants.Foci.Elements.Cold));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_VeryOld, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_VeryOld, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_VeryOld, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_VeryOld, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_VeryOld, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_VeryOld, "Breath Weapon", "9d6", FeatConstants.Foci.Elements.Cold));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Ancient, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Ancient, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Ancient, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Ancient, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Ancient, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Ancient, "Breath Weapon", "10d6", FeatConstants.Foci.Elements.Cold));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Wyrm, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Wyrm, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Wyrm, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Wyrm, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Wyrm, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Wyrm, "Tail Sweep", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_Wyrm, "Breath Weapon", "11d6", FeatConstants.Foci.Elements.Cold));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_GreatWyrm, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_GreatWyrm, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_GreatWyrm, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_GreatWyrm, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_GreatWyrm, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_GreatWyrm, "Tail Sweep", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_White_GreatWyrm, "Breath Weapon", "12d6", FeatConstants.Foci.Elements.Cold));

            //tiny
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Wyrmling, "Bite", "1d4", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Wyrmling, "Claw", "1d3", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Wyrmling, "Breath Weapon (fire)", "1d6", FeatConstants.Foci.Elements.Fire));

            //small
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_VeryYoung, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_VeryYoung, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_VeryYoung, "Breath Weapon (fire)", "2d6", FeatConstants.Foci.Elements.Fire));

            //medium
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Young, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Young, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Young, "Wing", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Young, "Breath Weapon (fire)", "3d6", FeatConstants.Foci.Elements.Fire));

            //medium
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Juvenile, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Juvenile, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Juvenile, "Wing", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Juvenile, "Breath Weapon (fire)", "4d6", FeatConstants.Foci.Elements.Fire));

            //large
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_YoungAdult, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_YoungAdult, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_YoungAdult, "Wing", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_YoungAdult, "Tail Slap", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_YoungAdult, "Breath Weapon (fire)", "5d6", FeatConstants.Foci.Elements.Fire));

            //large
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Adult, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Adult, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Adult, "Wing", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Adult, "Tail Slap", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Adult, "Breath Weapon (fire)", "6d6", FeatConstants.Foci.Elements.Fire));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_MatureAdult, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_MatureAdult, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_MatureAdult, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_MatureAdult, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_MatureAdult, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_MatureAdult, "Breath Weapon (fire)", "7d6", FeatConstants.Foci.Elements.Fire));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Old, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Old, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Old, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Old, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Old, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Old, "Breath Weapon (fire)", "8d6", FeatConstants.Foci.Elements.Fire));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_VeryOld, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_VeryOld, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_VeryOld, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_VeryOld, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_VeryOld, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_VeryOld, "Breath Weapon (fire)", "9d6", FeatConstants.Foci.Elements.Fire));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Ancient, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Ancient, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Ancient, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Ancient, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Ancient, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Ancient, "Breath Weapon (fire)", "10d6", FeatConstants.Foci.Elements.Fire));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Wyrm, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Wyrm, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Wyrm, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Wyrm, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Wyrm, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Wyrm, "Tail Sweep", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_Wyrm, "Breath Weapon (fire)", "11d6", FeatConstants.Foci.Elements.Fire));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_GreatWyrm, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_GreatWyrm, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_GreatWyrm, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_GreatWyrm, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_GreatWyrm, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_GreatWyrm, "Tail Sweep", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Brass_GreatWyrm, "Breath Weapon (fire)", "12d6", FeatConstants.Foci.Elements.Fire));

            //small
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrmling, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrmling, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrmling, "Breath Weapon (electricity)", "2d6", FeatConstants.Foci.Elements.Electricity));

            //medium
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_VeryYoung, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_VeryYoung, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_VeryYoung, "Wing", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_VeryYoung, "Breath Weapon (electricity)", "4d6", FeatConstants.Foci.Elements.Electricity));

            //medium
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Young, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Young, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Young, "Wing", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Young, "Breath Weapon (electricity)", "6d6", FeatConstants.Foci.Elements.Electricity));

            //large
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Juvenile, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Juvenile, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Juvenile, "Wing", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Juvenile, "Tail Slap", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Juvenile, "Breath Weapon (electricity)", "8d6", FeatConstants.Foci.Elements.Electricity));

            //large
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_YoungAdult, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_YoungAdult, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_YoungAdult, "Wing", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_YoungAdult, "Tail Slap", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_YoungAdult, "Breath Weapon (electricity)", "10d6", FeatConstants.Foci.Elements.Electricity));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Adult, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Adult, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Adult, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Adult, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Adult, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Adult, "Breath Weapon (electricity)", "12d6", FeatConstants.Foci.Elements.Electricity));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_MatureAdult, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_MatureAdult, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_MatureAdult, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_MatureAdult, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_MatureAdult, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_MatureAdult, "Breath Weapon (electricity)", "14d6", FeatConstants.Foci.Elements.Electricity));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Old, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Old, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Old, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Old, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Old, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Old, "Breath Weapon (electricity)", "16d6", FeatConstants.Foci.Elements.Electricity));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_VeryOld, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_VeryOld, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_VeryOld, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_VeryOld, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_VeryOld, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_VeryOld, "Breath Weapon (electricity)", "18d6", FeatConstants.Foci.Elements.Electricity));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Ancient, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Ancient, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Ancient, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Ancient, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Ancient, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Ancient, "Tail Sweep", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Ancient, "Breath Weapon (electricity)", "20d6", FeatConstants.Foci.Elements.Electricity));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrm, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrm, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrm, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrm, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrm, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrm, "Tail Sweep", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_Wyrm, "Breath Weapon (electricity)", "22d6", FeatConstants.Foci.Elements.Electricity));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_GreatWyrm, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_GreatWyrm, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_GreatWyrm, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_GreatWyrm, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_GreatWyrm, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_GreatWyrm, "Tail Sweep", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Bronze_GreatWyrm, "Breath Weapon (electricity)", "24d6", FeatConstants.Foci.Elements.Electricity));

            //tiny
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Wyrmling, "Bite", "1d4", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Wyrmling, "Claw", "1d3", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Wyrmling, "Breath Weapon (acid)", "2d4", FeatConstants.Foci.Elements.Acid));

            //small
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_VeryYoung, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_VeryYoung, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_VeryYoung, "Breath Weapon (acid)", "4d4", FeatConstants.Foci.Elements.Acid));

            //medium
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Young, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Young, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Young, "Wing", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Young, "Breath Weapon (acid)", "6d4", FeatConstants.Foci.Elements.Acid));

            //medium
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Juvenile, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Juvenile, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Juvenile, "Wing", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Juvenile, "Breath Weapon (acid)", "8d4", FeatConstants.Foci.Elements.Acid));

            //large
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_YoungAdult, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_YoungAdult, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_YoungAdult, "Wing", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_YoungAdult, "Tail Slap", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_YoungAdult, "Breath Weapon (acid)", "10d4", FeatConstants.Foci.Elements.Acid));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Adult, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Adult, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Adult, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Adult, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Adult, "Breath Weapon (acid)", "12d4", FeatConstants.Foci.Elements.Acid));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_MatureAdult, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_MatureAdult, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_MatureAdult, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_MatureAdult, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_MatureAdult, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_MatureAdult, "Breath Weapon (acid)", "14d4", FeatConstants.Foci.Elements.Acid));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Old, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Old, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Old, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Old, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Old, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Old, "Breath Weapon (acid)", "16d4", FeatConstants.Foci.Elements.Acid));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_VeryOld, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_VeryOld, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_VeryOld, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_VeryOld, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_VeryOld, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_VeryOld, "Breath Weapon (acid)", "18d4", FeatConstants.Foci.Elements.Acid));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Ancient, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Ancient, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Ancient, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Ancient, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Ancient, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Ancient, "Breath Weapon (acid)", "20d4", FeatConstants.Foci.Elements.Acid));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Wyrm, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Wyrm, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Wyrm, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Wyrm, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Wyrm, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Wyrm, "Tail Sweep", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_Wyrm, "Breath Weapon (acid)", "22d4", FeatConstants.Foci.Elements.Acid));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_GreatWyrm, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_GreatWyrm, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_GreatWyrm, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_GreatWyrm, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_GreatWyrm, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_GreatWyrm, "Tail Sweep", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Copper_GreatWyrm, "Breath Weapon (acid)", "24d4", FeatConstants.Foci.Elements.Acid));

            //medium
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Wyrmling, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Wyrmling, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Wyrmling, "Wing", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Wyrmling, "Breath Weapon (fire)", "2d10", FeatConstants.Foci.Elements.Fire));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Wyrmling, "Breath Weapon (weakening gas)", "1", AbilityConstants.Strength));

            //large
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_VeryYoung, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_VeryYoung, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_VeryYoung, "Wing", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_VeryYoung, "Tail Slap", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_VeryYoung, "Breath Weapon (fire)", "4d10", FeatConstants.Foci.Elements.Fire));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_VeryYoung, "Breath Weapon (weakening gas)", "2", AbilityConstants.Strength));

            //large
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Young, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Young, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Young, "Wing", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Young, "Tail Slap", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Young, "Breath Weapon (fire)", "6d10", FeatConstants.Foci.Elements.Fire));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Young, "Breath Weapon (weakening gas)", "3", AbilityConstants.Strength));

            //large
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Juvenile, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Juvenile, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Juvenile, "Wing", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Juvenile, "Tail Slap", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Juvenile, "Breath Weapon (fire)", "8d10", FeatConstants.Foci.Elements.Fire));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Juvenile, "Breath Weapon (weakening gas)", "4", AbilityConstants.Strength));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_YoungAdult, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_YoungAdult, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_YoungAdult, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_YoungAdult, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_YoungAdult, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_YoungAdult, "Breath Weapon (fire)", "10d10", FeatConstants.Foci.Elements.Fire));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_YoungAdult, "Breath Weapon (weakening gas)", "5", AbilityConstants.Strength));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Adult, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Adult, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Adult, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Adult, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Adult, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Adult, "Breath Weapon (fire)", "12d10", FeatConstants.Foci.Elements.Fire));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Adult, "Breath Weapon (weakening gas)", "6", AbilityConstants.Strength));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_MatureAdult, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_MatureAdult, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_MatureAdult, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_MatureAdult, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_MatureAdult, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_MatureAdult, "Breath Weapon (fire)", "14d10", FeatConstants.Foci.Elements.Fire));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_MatureAdult, "Breath Weapon (weakening gas)", "7", AbilityConstants.Strength));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Old, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Old, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Old, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Old, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Old, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Old, "Tail Sweep", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Old, "Breath Weapon (fire)", "16d10", FeatConstants.Foci.Elements.Fire));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Old, "Breath Weapon (weakening gas)", "8", AbilityConstants.Strength));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_VeryOld, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_VeryOld, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_VeryOld, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_VeryOld, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_VeryOld, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_VeryOld, "Tail Sweep", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_VeryOld, "Breath Weapon (fire)", "18d10", FeatConstants.Foci.Elements.Fire));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_VeryOld, "Breath Weapon (weakening gas)", "9", AbilityConstants.Strength));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Ancient, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Ancient, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Ancient, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Ancient, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Ancient, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Ancient, "Tail Sweep", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Ancient, "Breath Weapon (fire)", "20d10", FeatConstants.Foci.Elements.Fire));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Ancient, "Breath Weapon (weakening gas)", "10", AbilityConstants.Strength));

            //colossal
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Wyrm, "Bite", "4d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Wyrm, "Claw", "4d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Wyrm, "Wing", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Wyrm, "Tail Slap", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Wyrm, "Crush", "4d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Wyrm, "Tail Sweep", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Wyrm, "Breath Weapon (fire)", "22d10", FeatConstants.Foci.Elements.Fire));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_Wyrm, "Breath Weapon (weakening gas)", "11", AbilityConstants.Strength));

            //colossal
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_GreatWyrm, "Bite", "4d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_GreatWyrm, "Claw", "4d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_GreatWyrm, "Wing", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_GreatWyrm, "Tail Slap", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_GreatWyrm, "Crush", "4d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_GreatWyrm, "Tail Sweep", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_GreatWyrm, "Breath Weapon (fire)", "24d10", FeatConstants.Foci.Elements.Fire));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Gold_GreatWyrm, "Breath Weapon (weakening gas)", "12", AbilityConstants.Strength));

            //small
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Wyrmling, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Wyrmling, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Wyrmling, "Breath Weapon (cold)", "2d8", FeatConstants.Foci.Elements.Cold));

            //medium
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_VeryYoung, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_VeryYoung, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_VeryYoung, "Wing", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_VeryYoung, "Breath Weapon (cold)", "4d8", FeatConstants.Foci.Elements.Cold));

            //medium
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Young, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Young, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Young, "Wing", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Young, "Breath Weapon (cold)", "6d8", FeatConstants.Foci.Elements.Cold));

            //large
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Juvenile, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Juvenile, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Juvenile, "Wing", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Juvenile, "Tail Slap", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Juvenile, "Breath Weapon (cold)", "8d8", FeatConstants.Foci.Elements.Cold));

            //large
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_YoungAdult, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_YoungAdult, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_YoungAdult, "Wing", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_YoungAdult, "Tail Slap", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_YoungAdult, "Breath Weapon (cold)", "10d8", FeatConstants.Foci.Elements.Cold));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Adult, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Adult, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Adult, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Adult, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Adult, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Adult, "Breath Weapon (cold)", "12d8", FeatConstants.Foci.Elements.Cold));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_MatureAdult, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_MatureAdult, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_MatureAdult, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_MatureAdult, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_MatureAdult, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_MatureAdult, "Breath Weapon (cold)", "14d8", FeatConstants.Foci.Elements.Cold));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Old, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Old, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Old, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Old, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Old, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Old, "Breath Weapon (cold)", "16d8", FeatConstants.Foci.Elements.Cold));

            //huge
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_VeryOld, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_VeryOld, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_VeryOld, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_VeryOld, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_VeryOld, "Crush", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_VeryOld, "Breath Weapon (cold)", "18d8", FeatConstants.Foci.Elements.Cold));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Ancient, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Ancient, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Ancient, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Ancient, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Ancient, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Ancient, "Tail Sweep", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Ancient, "Breath Weapon (cold)", "20d8", FeatConstants.Foci.Elements.Cold));

            //gargantuan
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Wyrm, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Wyrm, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Wyrm, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Wyrm, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Wyrm, "Crush", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Wyrm, "Tail Sweep", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_Wyrm, "Breath Weapon (cold)", "22d8", FeatConstants.Foci.Elements.Cold));

            //colossal
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_GreatWyrm, "Bite", "4d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_GreatWyrm, "Claw", "4d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_GreatWyrm, "Wing", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_GreatWyrm, "Tail Slap", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_GreatWyrm, "Crush", "4d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_GreatWyrm, "Tail Sweep", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Dragon_Silver_GreatWyrm, "Breath Weapon (cold)", "24d8", FeatConstants.Foci.Elements.Cold));

            attackDamages.Add(BuildData(CreatureConstants.Dragonne, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dragonne, "Claw", "2d4", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Dretch, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Dretch, "Bite", "1d4", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Drider, "Bite", "1d4", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Drider, "Poison",
                "1d6", AbilityConstants.Strength, "Initial",
                "1d6", AbilityConstants.Strength, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Dryad, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Dwarf_Deep, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Dwarf_Duergar, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Dwarf_Hill, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Dwarf_Mountain, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Eagle, "Talons", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Eagle, "Bite", "1d4", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Eagle_Giant, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Eagle_Giant, "Bite", "1d8", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Efreeti, "Slam", "1d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Efreeti, "Heat", "1d6", FeatConstants.Foci.Elements.Fire));

            attackDamages.Add(BuildData(CreatureConstants.Elasmosaurus, "Bite", "2d8", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Elemental_Air_Small, "Slam", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Elemental_Air_Small, "Whirlwind", "1d4", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Elemental_Air_Medium, "Slam", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Elemental_Air_Medium, "Whirlwind", "1d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Elemental_Air_Large, "Slam", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Elemental_Air_Large, "Whirlwind", "2d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Elemental_Air_Huge, "Slam", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Elemental_Air_Huge, "Whirlwind", "2d8", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Elemental_Air_Greater, "Slam", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Elemental_Air_Greater, "Whirlwind", "2d8", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Elemental_Air_Elder, "Slam", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Elemental_Air_Elder, "Whirlwind", "2d8", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Elemental_Earth_Small, "Slam", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Elemental_Earth_Medium, "Slam", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Elemental_Earth_Large, "Slam", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Elemental_Earth_Huge, "Slam", "2d10", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Elemental_Earth_Greater, "Slam", "2d10", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Elemental_Earth_Elder, "Slam", "2d10", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Elemental_Fire_Small, "Slam", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Elemental_Fire_Small, "Burn", "1d4", FeatConstants.Foci.Elements.Fire));

            attackDamages.Add(BuildData(CreatureConstants.Elemental_Fire_Medium, "Slam", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Elemental_Fire_Medium, "Burn", "1d6", FeatConstants.Foci.Elements.Fire));

            attackDamages.Add(BuildData(CreatureConstants.Elemental_Fire_Large, "Slam", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Elemental_Fire_Large, "Burn", "2d6", FeatConstants.Foci.Elements.Fire));

            attackDamages.Add(BuildData(CreatureConstants.Elemental_Fire_Huge, "Slam", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Elemental_Fire_Huge, "Burn", "2d8", FeatConstants.Foci.Elements.Fire));

            attackDamages.Add(BuildData(CreatureConstants.Elemental_Fire_Greater, "Slam", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Elemental_Fire_Greater, "Burn", "2d8", FeatConstants.Foci.Elements.Fire));

            attackDamages.Add(BuildData(CreatureConstants.Elemental_Fire_Elder, "Slam", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Elemental_Fire_Elder, "Burn", "2d8", FeatConstants.Foci.Elements.Fire));

            attackDamages.Add(BuildData(CreatureConstants.Elemental_Water_Small, "Slam", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Elemental_Water_Small, "Vortex", "1d4", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Elemental_Water_Medium, "Slam", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Elemental_Water_Medium, "Vortex", "1d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Elemental_Water_Large, "Slam", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Elemental_Water_Large, "Vortex", "2d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Elemental_Water_Huge, "Slam", "2d10", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Elemental_Water_Huge, "Vortex", "2d8", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Elemental_Water_Greater, "Slam", "2d10", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Elemental_Water_Greater, "Vortex", "2d8", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Elemental_Water_Elder, "Slam", "2d10", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Elemental_Water_Elder, "Vortex", "2d8", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Elephant, "Slam", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Elephant, "Stamp", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Elephant, "Gore", "2d8", AttributeConstants.DamageTypes.Piercing));
            attackDamages.Add(BuildData(CreatureConstants.Elephant, "Trample", "2d8", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Elf_Aquatic, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Elf_Drow, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Elf_Gray, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Elf_Half, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Elf_High, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Elf_Wild, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Elf_Wood, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Erinyes, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.EtherealFilcher, "Bite", "1d4", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.EtherealMarauder, "Bite", "1d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Ettercap, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Ettercap, "Claw", "1d3", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Ettercap, "Poison",
                    "1d6", AbilityConstants.Dexterity, "Initial",
                    "2d6", AbilityConstants.Dexterity, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Ettin, "Unarmed Strike", "1d4", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.FireBeetle_Giant, "Bite", "2d4", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.FormianWorker, "Bite", "1d4", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.FormianWarrior, "Sting", "2d4", stingDamageType));
            attackDamages.Add(BuildData(CreatureConstants.FormianWarrior, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.FormianWarrior, "Bite", "1d4", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.FormianWarrior, "Poison",
                    "1d6", AbilityConstants.Strength, "Initial",
                    "1d6", AbilityConstants.Strength, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.FormianTaskmaster, "Sting", "2d4", stingDamageType));
            attackDamages.Add(BuildData(CreatureConstants.FormianTaskmaster, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.FormianTaskmaster, "Poison",
                    "1d6", AbilityConstants.Strength, "Initial",
                    "1d6", AbilityConstants.Strength, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.FormianMyrmarch, "Sting", "2d4", stingDamageType));
            attackDamages.Add(BuildData(CreatureConstants.FormianMyrmarch, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.FormianMyrmarch, "Poison",
                    "1d6", AbilityConstants.Strength, "Initial",
                    "1d6", AbilityConstants.Strength, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.FrostWorm, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.FrostWorm, "Cold", "1d8", FeatConstants.Foci.Elements.Cold));
            attackDamages.Add(BuildData(CreatureConstants.FrostWorm, "Breath weapon", "15d6", FeatConstants.Foci.Elements.Cold));

            attackDamages.Add(BuildData(CreatureConstants.Gargoyle, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Gargoyle, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Gargoyle, "Gore", "1d6", goreDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Gargoyle_Kapoacinth, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Gargoyle_Kapoacinth, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Gargoyle_Kapoacinth, "Gore", "1d6", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.GelatinousCube, "Slam",
                    "1d6", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "1d6", FeatConstants.Foci.Elements.Acid));

            attackDamages.Add(BuildData(CreatureConstants.Ghaele, "Light Ray", "2d12"));

            attackDamages.Add(BuildData(CreatureConstants.Ghoul, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Ghoul, "Claw", "1d3", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Ghoul, "Ghoul Fever",
                    "1d3", AbilityConstants.Constitution, "Incubation period 1 day",
                    "1d3", AbilityConstants.Dexterity, "Incubation period 1 day"));

            attackDamages.Add(BuildData(CreatureConstants.Ghoul_Ghast, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Ghoul_Ghast, "Claw", "1d3", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Ghoul_Ghast, "Ghoul Fever",
                    "1d3", AbilityConstants.Constitution, "Incubation period 1 day",
                    "1d3", AbilityConstants.Dexterity, "Incubation period 1 day"));

            attackDamages.Add(BuildData(CreatureConstants.Ghoul_Lacedon, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Ghoul_Lacedon, "Claw", "1d3", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Ghoul_Lacedon, "Ghoul Fever",
                    "1d3", AbilityConstants.Constitution, "Incubation period 1 day",
                    "1d3", AbilityConstants.Dexterity, "Incubation period 1 day"));

            attackDamages.Add(BuildData(CreatureConstants.Giant_Cloud, "Slam", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Giant_Cloud, "Rock", "2d8", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Giant_Fire, "Slam", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Giant_Fire, "Rock", "2d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Giant_Frost, "Slam", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Giant_Frost, "Rock", "2d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Giant_Hill, "Slam", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Giant_Hill, "Rock", "2d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Giant_Stone, "Slam", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Giant_Stone, "Rock", "2d8", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Giant_Stone_Elder, "Slam", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Giant_Stone_Elder, "Rock", "2d8", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Giant_Storm, "Slam", "1d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.GibberingMouther, "Bite", "1", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.GibberingMouther, "Spittle", "1d4", FeatConstants.Foci.Elements.Acid));
            attackDamages.Add(BuildData(CreatureConstants.GibberingMouther, "Blood Drain", "1d4", AbilityConstants.Constitution));

            attackDamages.Add(BuildData(CreatureConstants.Girallon, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Girallon, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Girallon, "Rend", "2d4", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Githyanki, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Githzerai, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Glabrezu, "Pincer", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Glabrezu, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Glabrezu, "Bite", "1d8", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Gnoll, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Gnome_Forest, "Unarmed Strike", "1d2", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Gnome_Rock, "Unarmed Strike", "1d2", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Gnome_Svirfneblin, "Unarmed Strike", "1d2", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Goblin, "Unarmed Strike", "1d2", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Golem_Clay, "Slam", "2d10", slapSlamDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Golem_Flesh, "Slam", "2d8", slapSlamDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Golem_Iron, "Slam", "2d10", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Golem_Iron, "Poisonous Gas",
                    "1d4", AbilityConstants.Constitution, "Initial",
                    "3d4", AbilityConstants.Constitution, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Golem_Stone, "Slam", "2d10", slapSlamDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Golem_Stone_Greater, "Slam", "4d8", slapSlamDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Gorgon, "Gore", "1d8", AttributeConstants.DamageTypes.Piercing));
            attackDamages.Add(BuildData(CreatureConstants.Gorgon, "Trample", "1d8", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.GrayOoze, "Slam",
                    "1d6", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "1d6", FeatConstants.Foci.Elements.Acid));
            attackDamages.Add(BuildData(CreatureConstants.GrayOoze, "Acid", "16", FeatConstants.Foci.Elements.Acid, "Wooden or Metal objects"));
            attackDamages.Add(BuildData(CreatureConstants.GrayOoze, "Constrict",
                    "1d6", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "1d6", FeatConstants.Foci.Elements.Acid));

            attackDamages.Add(BuildData(CreatureConstants.GrayRender, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.GrayRender, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.GrayRender, "Rend", "2d6", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.GreenHag, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.GreenHag, "Weakness", "2d4", AbilityConstants.Strength));

            attackDamages.Add(BuildData(CreatureConstants.Grick, "Tentacle", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Grick, "Bite", "1d3", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Griffon, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Griffon, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Griffon, "Rake", "1d6", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Grig, "Unarmed Strike", "1", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Grig_WithFiddle, "Unarmed Strike", "1", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Grimlock, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Gynosphinx, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Gynosphinx, "Rake", "1d6", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Halfling_Deep, "Unarmed Strike", "1d2", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Halfling_Lightfoot, "Unarmed Strike", "1d2", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Halfling_Tallfellow, "Unarmed Strike", "1d2", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Harpy, "Claw", "1d3", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Hawk, "Talons", "1d4", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.HellHound, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.HellHound, "Fiery Bite", "1d6", FeatConstants.Foci.Elements.Fire));
            attackDamages.Add(BuildData(CreatureConstants.HellHound, "Breath weapon", "2d6", FeatConstants.Foci.Elements.Fire));

            attackDamages.Add(BuildData(CreatureConstants.HellHound_NessianWarhound, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.HellHound_NessianWarhound, "Fiery Bite", "1d8", FeatConstants.Foci.Elements.Fire));
            attackDamages.Add(BuildData(CreatureConstants.HellHound_NessianWarhound, "Breath weapon", "3d6", FeatConstants.Foci.Elements.Fire));

            attackDamages.Add(BuildData(CreatureConstants.Hellcat_Bezekira, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Hellcat_Bezekira, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Hellcat_Bezekira, "Rake", "1d8", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Hellwasp_Swarm, "Swarm", "3d6"));
            attackDamages.Add(BuildData(CreatureConstants.Hellwasp_Swarm, "Poison",
                    "1d4", AbilityConstants.Dexterity, "Initial",
                    "1d4", AbilityConstants.Dexterity, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Hezrou, "Bite", "4d4", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Hezrou, "Claw", "1d8", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Hieracosphinx, "Bite", "1d10", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Hieracosphinx, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Hieracosphinx, "Rake", "1d6", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Hippogriff, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Hippogriff, "Bite", "1d8", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Hobgoblin, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Homunculus, "Bite", "1d4", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.HornedDevil_Cornugon, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.HornedDevil_Cornugon, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.HornedDevil_Cornugon, "Tail", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.HornedDevil_Cornugon, "Infernal Wound", "2"));

            attackDamages.Add(BuildData(CreatureConstants.Horse_Heavy, "Hoof", "1d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Horse_Heavy_War, "Hoof", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Horse_Heavy_War, "Bite", "1d4", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Horse_Light, "Hoof", "1d4", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Horse_Light_War, "Hoof", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Horse_Light_War, "Bite", "1d3", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.HoundArchon, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.HoundArchon, "Slam", "1d4", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Howler, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Howler, "Quill", "1d6", AttributeConstants.DamageTypes.Piercing));
            attackDamages.Add(BuildData(CreatureConstants.Howler, "Howl", "1", AbilityConstants.Wisdom));

            attackDamages.Add(BuildData(CreatureConstants.Human, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Hydra_5Heads, "Bite", "1d10", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Hydra_6Heads, "Bite", "1d10", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Hydra_7Heads, "Bite", "1d10", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Hydra_8Heads, "Bite", "1d10", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Hydra_9Heads, "Bite", "1d10", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Hydra_10Heads, "Bite", "1d10", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Hydra_11Heads, "Bite", "1d10", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Hydra_12Heads, "Bite", "1d10", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Hyena, "Bite", "1d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.IceDevil_Gelugon, "Claw", "1d10", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.IceDevil_Gelugon, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.IceDevil_Gelugon, "Tail", "3d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Imp, "Sting", "1d4", AttributeConstants.DamageTypes.Piercing));
            attackDamages.Add(BuildData(CreatureConstants.Imp, "Poison",
                    "1d4", AbilityConstants.Dexterity, "Initial",
                    "2d4", AbilityConstants.Dexterity, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.InvisibleStalker, "Slam", "2d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Janni, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Kobold, "Unarmed Strike", "1d2", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Kolyarut, "Slam", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Kolyarut, "Vampiric Touch", "5d6"));
            attackDamages.Add(BuildData(CreatureConstants.Kolyarut, "Enervation Ray", "1d4", "Negative Level"));

            attackDamages.Add(BuildData(CreatureConstants.Kraken, "Tentacle", "2d8", tentacleDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Kraken, "Arm", "1d6", tentacleDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Kraken, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Kraken, "Constrict (Tentacle)", "2d8", tentacleDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Kraken, "Constrict (Arm)", "1d6", tentacleDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Krenshar, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Krenshar, "Claw", "1d4", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.KuoToa, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.KuoToa, "Lightning Bolt", "1d6", FeatConstants.Foci.Elements.Electricity, "per Kuo-Toa Cleric"));

            attackDamages.Add(BuildData(CreatureConstants.Lamia, "Wisdom Drain", "1d4", AbilityConstants.Wisdom));
            attackDamages.Add(BuildData(CreatureConstants.Lamia, "Claw", "1d4", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Lammasu, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Lammasu, "Rake", "1d6", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.LanternArchon, "Light Ray", "1d6"));

            attackDamages.Add(BuildData(CreatureConstants.Lemure, "Claw", "1d4", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Leonal, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Leonal, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Leonal, "Roar", "2d6", FeatConstants.Foci.Elements.Sonic));
            attackDamages.Add(BuildData(CreatureConstants.Leonal, "Rake", "1d6", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Leopard, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Leopard, "Claw", "1d3", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Leopard, "Rake", "1d3", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Lillend, "Unarmed Strike", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Lillend, "Tail Slap", "2d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Lillend, "Constrict", "2d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Lion, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Lion, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Lion, "Rake", "1d4", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Lion_Dire, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Lion_Dire, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Lion_Dire, "Rake", "1d6", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Lizard, "Bite", "1d4", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Lizard_Monitor, "Bite", "1d8", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Lizardfolk, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Lizardfolk, "Bite", "1d4", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Locathah, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Locust_Swarm, "Swarm", "2d6"));

            attackDamages.Add(BuildData(CreatureConstants.Magmin, "Burning Touch", "1d8", FeatConstants.Foci.Elements.Fire));
            attackDamages.Add(BuildData(CreatureConstants.Magmin, "Slam", "1d3", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Magmin, "Combustion", "1d8", FeatConstants.Foci.Elements.Fire));
            attackDamages.Add(BuildData(CreatureConstants.Magmin, "Fiery Aura", "1d6", FeatConstants.Foci.Elements.Fire));

            attackDamages.Add(BuildData(CreatureConstants.MantaRay, "Ram", "1d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Manticore, "Claw", "2d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Manticore, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Manticore, "Tail Spikes", "1d8", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.Marilith, "Tail Slap", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Marilith, "Slam", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Marilith, "Constrict", "4d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Marut, "Slam", "2d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Marut, "Slam", "2d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Marut, "Fist of Thunder", "3d6", FeatConstants.Foci.Elements.Sonic));
            attackDamages.Add(BuildData(CreatureConstants.Marut, "Fist of Lightning", "3d6", FeatConstants.Foci.Elements.Electricity));

            attackDamages.Add(BuildData(CreatureConstants.Medusa, "Snakes", "1d4", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Medusa, "Poison",
                    "1d6", AbilityConstants.Strength, "Initial",
                    "2d6", AbilityConstants.Strength, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Megaraptor, "Talons", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Megaraptor, "Foreclaw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Megaraptor, "Bite", "1d8", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Mephit_Air, "Claw", "1d3", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Mephit_Air, "Breath weapon", "1d8"));

            attackDamages.Add(BuildData(CreatureConstants.Mephit_Dust, "Claw", "1d3", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Mephit_Dust, "Breath weapon", "1d4"));

            attackDamages.Add(BuildData(CreatureConstants.Mephit_Earth, "Claw", "1d3", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Mephit_Earth, "Breath weapon", "1d8"));

            attackDamages.Add(BuildData(CreatureConstants.Mephit_Fire, "Claw",
                    "1d3", clawDamageType, string.Empty,
                    "1d4", FeatConstants.Foci.Elements.Fire));
            attackDamages.Add(BuildData(CreatureConstants.Mephit_Fire, "Breath weapon", "1d8", FeatConstants.Foci.Elements.Fire));

            attackDamages.Add(BuildData(CreatureConstants.Mephit_Ice, "Claw",
                    "1d3", clawDamageType, string.Empty,
                    "1d4", FeatConstants.Foci.Elements.Cold));
            attackDamages.Add(BuildData(CreatureConstants.Mephit_Ice, "Breath weapon", "1d4", FeatConstants.Foci.Elements.Cold));

            attackDamages.Add(BuildData(CreatureConstants.Mephit_Magma, "Claw",
                    "1d3", clawDamageType, string.Empty,
                    "1d4", FeatConstants.Foci.Elements.Fire));
            attackDamages.Add(BuildData(CreatureConstants.Mephit_Magma, "Breath weapon", "1d4", FeatConstants.Foci.Elements.Fire));

            attackDamages.Add(BuildData(CreatureConstants.Mephit_Ooze, "Claw", "1d3", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Mephit_Ooze, "Breath weapon", "1d4", FeatConstants.Foci.Elements.Acid));

            attackDamages.Add(BuildData(CreatureConstants.Mephit_Salt, "Claw", "1d3", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Mephit_Salt, "Breath weapon", "1d4"));

            attackDamages.Add(BuildData(CreatureConstants.Mephit_Steam, "Claw",
                    "1d3", clawDamageType, string.Empty,
                    "1d4", FeatConstants.Foci.Elements.Fire));
            attackDamages.Add(BuildData(CreatureConstants.Mephit_Steam, "Breath weapon", "1d4", FeatConstants.Foci.Elements.Fire));

            attackDamages.Add(BuildData(CreatureConstants.Mephit_Water, "Claw", "1d3", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Mephit_Water, "Breath weapon", "1d8", FeatConstants.Foci.Elements.Acid));

            attackDamages.Add(BuildData(CreatureConstants.Merfolk, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Mimic, "Slam", "1d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Mimic, "Crush", "1d8", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.MindFlayer, "Tentacle", "1d4", tentacleDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Minotaur, "Gore", "1d8", goreDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Minotaur, "Powerful Charge", "4d6", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.Mohrg, "Slam", "1d6", slapSlamDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Monkey, "Bite", "1d3", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Mule, "Hoof", "1d4", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Mummy, "Slam", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Mummy, "Mummy Rot",
                    "1d6", AbilityConstants.Constitution, "Incubation period 1 minute",
                    "1d6", AbilityConstants.Charisma, "Incubation period 1 minute"));

            attackDamages.Add(BuildData(CreatureConstants.Naga_Dark, "Sting", "2d4", stingDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Naga_Dark, "Bite", "1d4", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Naga_Guardian, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Naga_Guardian, "Poison",
                    "1d10", AbilityConstants.Constitution, "Initial",
                    "1d10", AbilityConstants.Constitution, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Naga_Spirit, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Naga_Spirit, "Poison",
                    "1d8", AbilityConstants.Constitution, "Initial",
                    "1d8", AbilityConstants.Constitution, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Naga_Water, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Naga_Water, "Poison",
                    "1d8", AbilityConstants.Constitution, "Initial",
                    "1d8", AbilityConstants.Constitution, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Nalfeshnee, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Nalfeshnee, "Claw", "1d8", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.NightHag, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.NightHag, "Demon Fever", "1d6", AbilityConstants.Constitution, "Incubation period 1 day"));

            attackDamages.Add(BuildData(CreatureConstants.Nightcrawler, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Nightcrawler, "Sting", "2d8", AttributeConstants.DamageTypes.Piercing));
            attackDamages.Add(BuildData(CreatureConstants.Nightcrawler, "Energy Drain", "1", "Negative Level"));
            attackDamages.Add(BuildData(CreatureConstants.Nightcrawler, "Poison",
                    "2d6", AbilityConstants.Strength, "Initial",
                    "2d6", AbilityConstants.Strength, "Secondary"));
            attackDamages.Add(BuildData(CreatureConstants.Nightcrawler, "Swallow Whole",
                    "2d8+12", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "12", FeatConstants.Foci.Elements.Acid));

            attackDamages.Add(BuildData(CreatureConstants.Nightmare, "Hoof",
                    "1d8", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "1d4", FeatConstants.Foci.Elements.Fire));
            attackDamages.Add(BuildData(CreatureConstants.Nightmare, "Bite", "1d8", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Nightmare_Cauchemar, "Hoof",
                    "2d6", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "1d4", FeatConstants.Foci.Elements.Fire));
            attackDamages.Add(BuildData(CreatureConstants.Nightmare_Cauchemar, "Bite", "2d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Nightwalker, "Slam", "2d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Nightwing, "Bite", "2d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Nixie, "Unarmed Strike", "1d2", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Nymph, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.OchreJelly, "Slam",
                    "2d4", slapSlamDamageType, string.Empty,
                    "1d4", FeatConstants.Foci.Elements.Acid));
            attackDamages.Add(BuildData(CreatureConstants.OchreJelly, "Constrict",
                    "2d4", slapSlamDamageType, string.Empty,
                    "1d4", FeatConstants.Foci.Elements.Acid));

            attackDamages.Add(BuildData(CreatureConstants.Octopus, "Arms", "0", tentacleDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Octopus, "Bite", "1d3", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Octopus_Giant, "Tentacle", "1d4", tentacleDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Octopus_Giant, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Octopus_Giant, "Constrict", "2d8", tentacleDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Ogre, "Unarmed Strike", "1d4", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Ogre_Merrow, "Unarmed Strike", "1d4", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.OgreMage, "Unarmed Strike", "1d4", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Orc, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Orc_Half, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Otyugh, "Tentacle", "1d6", tentacleDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Otyugh, "Bite", "1d4", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Otyugh, "Constrict", "1d6", tentacleDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Otyugh, "Filth Fever",
                    "1d3", AbilityConstants.Dexterity, "Incubation period 1d3 days",
                    "1d3", AbilityConstants.Constitution, "Incubation period 1d3 days"));

            attackDamages.Add(BuildData(CreatureConstants.Owl, "Talons", "1d4", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Owl_Giant, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Owl_Giant, "Bite", "1d8", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Owlbear, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Owlbear, "Bite", "1d8", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Pegasus, "Hoof", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Pegasus, "Bite", "1d3", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.PhantomFungus, "Bite", "1d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.PhaseSpider, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.PhaseSpider, "Poison",
                    "1d8", AbilityConstants.Constitution, "Initial",
                    "1d8", AbilityConstants.Constitution, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Phasm, "Slam", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.PitFiend, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.PitFiend, "Wing", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.PitFiend, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.PitFiend, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.PitFiend, "Constrict", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.PitFiend, "Poison",
                "1d6", AbilityConstants.Constitution, "Initial"));
            attackDamages.Add(BuildData(CreatureConstants.PitFiend, "Devil Chills",
                "1d4", AbilityConstants.Strength, "Incubation period 1d4 days"));

            attackDamages.Add(BuildData(CreatureConstants.Pixie, "Unarmed Strike", "1d2", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Pixie_WithIrresistibleDance, "Unarmed Strike", "1d2", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Pony, "Hoof", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Pony_War, "Hoof", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Porpoise, "Slam", "2d4", slapSlamDamageType));

            attackDamages.Add(BuildData(CreatureConstants.PrayingMantis_Giant, "Claws", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.PrayingMantis_Giant, "Bite", "1d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Pseudodragon, "Sting",
                "1d3", AttributeConstants.DamageTypes.Piercing));
            attackDamages.Add(BuildData(CreatureConstants.Pseudodragon, "Bite",
                "1", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.PurpleWorm, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.PurpleWorm, "Sting", "2d6", stingDamageType));
            attackDamages.Add(BuildData(CreatureConstants.PurpleWorm, "Poison",
                    "1d6", AbilityConstants.Strength, "Initial",
                    "2d6", AbilityConstants.Strength, "Secondary"));
            attackDamages.Add(BuildData(CreatureConstants.PurpleWorm, "Swallow Whole",
                    "2d8+12", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "8", FeatConstants.Foci.Elements.Acid));

            attackDamages.Add(BuildData(CreatureConstants.Pyrohydra_5Heads, "Bite", "1d10", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Pyrohydra_5Heads, "Breath weapon", "3d6", FeatConstants.Foci.Elements.Fire, "Per living head"));

            attackDamages.Add(BuildData(CreatureConstants.Pyrohydra_6Heads, "Bite", "1d10", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Pyrohydra_6Heads, "Breath weapon", "3d6", FeatConstants.Foci.Elements.Fire, "Per living head"));

            attackDamages.Add(BuildData(CreatureConstants.Pyrohydra_7Heads, "Bite", "1d10", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Pyrohydra_7Heads, "Breath weapon", "3d6", FeatConstants.Foci.Elements.Fire, "Per living head"));

            attackDamages.Add(BuildData(CreatureConstants.Pyrohydra_8Heads, "Bite", "1d10", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Pyrohydra_8Heads, "Breath weapon", "3d6", FeatConstants.Foci.Elements.Fire, "Per living head"));

            attackDamages.Add(BuildData(CreatureConstants.Pyrohydra_9Heads, "Bite", "1d10", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Pyrohydra_9Heads, "Breath weapon", "3d6", FeatConstants.Foci.Elements.Fire, "Per living head"));

            attackDamages.Add(BuildData(CreatureConstants.Pyrohydra_10Heads, "Bite", "1d10", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Pyrohydra_10Heads, "Breath weapon", "3d6", FeatConstants.Foci.Elements.Fire, "Per living head"));

            attackDamages.Add(BuildData(CreatureConstants.Pyrohydra_11Heads, "Bite", "1d10", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Pyrohydra_11Heads, "Breath weapon", "3d6", FeatConstants.Foci.Elements.Fire, "Per living head"));

            attackDamages.Add(BuildData(CreatureConstants.Pyrohydra_12Heads, "Bite", "1d10", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Pyrohydra_12Heads, "Breath weapon", "3d6", FeatConstants.Foci.Elements.Fire, "Per living head"));

            attackDamages.Add(BuildData(CreatureConstants.Quasit, "Claw", "1d3", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Quasit, "Bite", "1d4", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Quasit, "Poison",
                    "1d4", AbilityConstants.Dexterity, "Initial",
                    "2d4", AbilityConstants.Dexterity, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Rakshasa, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Rakshasa, "Bite", "1d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Rast, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Rast, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Rast, "Blood Drain",
                "1", AbilityConstants.Constitution));

            attackDamages.Add(BuildData(CreatureConstants.Rat, "Bite", "1d3", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Rat_Dire, "Bite", "1d3", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Rat_Dire, "Filth Fever",
                    "1d3", AbilityConstants.Dexterity, "Incubation period 1d3 days",
                    "1d3", AbilityConstants.Constitution, "Incubation period 1d3 days"));

            attackDamages.Add(BuildData(CreatureConstants.Rat_Swarm, "Swarm", "1d6"));
            attackDamages.Add(BuildData(CreatureConstants.Rat_Swarm, "Filth Fever",
                    "1d3", AbilityConstants.Dexterity, "Incubation period 1d3 days",
                    "1d3", AbilityConstants.Constitution, "Incubation period 1d3 days"));

            attackDamages.Add(BuildData(CreatureConstants.Raven, "Claws", "1d2", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Ravid, "Tail Slap", "1d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Ravid, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Ravid, "Positive Energy", "2d10", "Positive energy"));

            attackDamages.Add(BuildData(CreatureConstants.RazorBoar, "Tusk Slash", "1d8", AttributeConstants.DamageTypes.Slashing));
            attackDamages.Add(BuildData(CreatureConstants.RazorBoar, "Hoof", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.RazorBoar, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.RazorBoar, "Trample", "2d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Remorhaz, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Remorhaz, "Swallow Whole",
                    "2d8+12", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "8d6", FeatConstants.Foci.Elements.Fire));

            attackDamages.Add(BuildData(CreatureConstants.Retriever, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Retriever, "Bite", "1d8", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Rhinoceras, "Gore", "2d6", goreDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Rhinoceras, "Powerful Charge", "4d6", goreDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Roc, "Talon", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Roc, "Bite", "2d8", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Roper, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Roper, "Weakness", "2d8", AbilityConstants.Strength));

            attackDamages.Add(BuildData(CreatureConstants.RustMonster, "Bite", "1d3", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Sahuagin, "Talon", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Sahuagin, "Bite", "1d4", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Sahuagin, "Rake", "1d4", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Sahuagin_Mutant, "Talon", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Sahuagin_Mutant, "Bite", "1d4", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Sahuagin_Mutant, "Rake", "1d4", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Sahuagin_Malenti, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Salamander_Flamebrother, "Tail Slap", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Salamander_Flamebrother, "Constrict", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Salamander_Flamebrother, "Heat",
                 "1d6", FeatConstants.Foci.Elements.Fire));

            attackDamages.Add(BuildData(CreatureConstants.Salamander_Average, "Tail Slap", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Salamander_Average, "Constrict", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Salamander_Average, "Heat",
                 "1d6", FeatConstants.Foci.Elements.Fire));

            attackDamages.Add(BuildData(CreatureConstants.Salamander_Noble, "Tail Slap", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Salamander_Noble, "Constrict", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Salamander_Noble, "Heat",
                 "1d8", FeatConstants.Foci.Elements.Fire));

            attackDamages.Add(BuildData(CreatureConstants.Satyr, "Head butt", "1d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Satyr_WithPipes, "Head butt", "1d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Tiny, "Claw", "1d2", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Tiny, "Sting", "1d2", AttributeConstants.DamageTypes.Piercing));
            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Tiny, "Constrict", "1d2", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Tiny, "Poison",
                    "1", AbilityConstants.Constitution, "Initial",
                    "1", AbilityConstants.Constitution, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Small, "Claw", "1d3", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Small, "Sting", "1d3", AttributeConstants.DamageTypes.Piercing));
            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Small, "Constrict", "1d3", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Small, "Poison",
                    "1d2", AbilityConstants.Constitution, "Initial",
                    "1d2", AbilityConstants.Constitution, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Medium, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Medium, "Sting", "1d4", AttributeConstants.DamageTypes.Piercing));
            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Medium, "Constrict", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Medium, "Poison",
                    "1d3", AbilityConstants.Constitution, "Initial",
                    "1d3", AbilityConstants.Constitution, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Large, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Large, "Sting", "1d6", AttributeConstants.DamageTypes.Piercing));
            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Large, "Constrict", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Large, "Poison",
                    "1d4", AbilityConstants.Constitution, "Initial",
                    "1d4", AbilityConstants.Constitution, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Huge, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Huge, "Sting", "1d8", AttributeConstants.DamageTypes.Piercing));
            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Huge, "Constrict", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Huge, "Poison",
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Gargantuan, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Gargantuan, "Sting", "2d6", AttributeConstants.DamageTypes.Piercing));
            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Gargantuan, "Constrict", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Gargantuan, "Poison",
                    "1d8", AbilityConstants.Constitution, "Initial",
                    "1d8", AbilityConstants.Constitution, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Colossal, "Claw", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Colossal, "Sting", "2d8", AttributeConstants.DamageTypes.Piercing));
            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Colossal, "Constrict", "2d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Scorpion_Monstrous_Colossal, "Poison",
                    "1d10", AbilityConstants.Constitution, "Initial",
                    "1d10", AbilityConstants.Constitution, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Scorpionfolk, "Sting", "1d8", stingDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Scorpionfolk, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Scorpionfolk, "Poison",
                    "1d4", AbilityConstants.Dexterity, "Initial",
                    "1d4", AbilityConstants.Dexterity, "Secondary"));
            attackDamages.Add(BuildData(CreatureConstants.Scorpionfolk, "Trample", "1d8", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.SeaCat, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.SeaCat, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.SeaCat, "Rend", "2d6", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.SeaHag, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.SeaHag, "Horrific Appearance", "2d6", AbilityConstants.Strength));

            attackDamages.Add(BuildData(CreatureConstants.Shadow, "Strength Damage", "1d6", AbilityConstants.Strength));

            attackDamages.Add(BuildData(CreatureConstants.Shadow_Greater, "Strength Damage", "1d8", AbilityConstants.Strength));

            attackDamages.Add(BuildData(CreatureConstants.ShadowMastiff, "Bite", "1d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.ShamblingMound, "Slam", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.ShamblingMound, "Constrict", "2d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Shark_Dire, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Shark_Dire, "Swallow Whole",
                    "2d6+6", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "1d8+4", FeatConstants.Foci.Elements.Acid));

            attackDamages.Add(BuildData(CreatureConstants.Shark_Huge, "Bite", "2d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Shark_Large, "Bite", "1d8", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Shark_Medium, "Bite", "1d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.ShieldGuardian, "Slam", "1d8", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.ShockerLizard, "Bite", "1d4", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.ShockerLizard, "Stunning Shock", "2d8", FeatConstants.Foci.Elements.Electricity));
            attackDamages.Add(BuildData(CreatureConstants.ShockerLizard, "Lethal Shock", "2d8", FeatConstants.Foci.Elements.Electricity, "per Shocker Lizard"));

            attackDamages.Add(BuildData(CreatureConstants.Skum, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Skum, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Skum, "Rake", "1d6", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Slaad_Red, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Slaad_Red, "Claw", "1d4", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Slaad_Blue, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Slaad_Blue, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Slaad_Blue, "Slaad Fever",
                    "1d3", AbilityConstants.Dexterity, "Incubation period 1 day",
                    "1d3", AbilityConstants.Charisma, "Incubation period 1 day"));

            attackDamages.Add(BuildData(CreatureConstants.Slaad_Green, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Slaad_Green, "Bite", "2d8", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Slaad_Gray, "Claw", "2d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Slaad_Gray, "Bite", "2d8", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Slaad_Death, "Claw", "3d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Slaad_Death, "Bite", "2d10", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Snake_Constrictor, "Bite", "1d3", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Snake_Constrictor, "Constrict", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Snake_Constrictor_Giant, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Snake_Constrictor_Giant, "Constrict", "1d8", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Snake_Viper_Tiny, "Bite", "1", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Snake_Viper_Tiny, "Poison",
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Snake_Viper_Small, "Bite", "1d2", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Snake_Viper_Small, "Poison",
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Snake_Viper_Medium, "Bite", "1d4", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Snake_Viper_Medium, "Poison",
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Snake_Viper_Large, "Bite", "1d4", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Snake_Viper_Large, "Poison",
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Snake_Viper_Huge, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Snake_Viper_Huge, "Poison",
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Spectre, "Incorporeal touch", "1d8"));
            attackDamages.Add(BuildData(CreatureConstants.Spectre, "Energy Drain", "2", "Negative Level"));

            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_Hunter_Tiny, "Bite", "1d3", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_Hunter_Tiny, "Poison",
                    "1d2", AbilityConstants.Strength, "Initial",
                    "1d2", AbilityConstants.Strength, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_Hunter_Small, "Bite", "1d4", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_Hunter_Small, "Poison",
                    "1d3", AbilityConstants.Strength, "Initial",
                    "1d3", AbilityConstants.Strength, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_Hunter_Medium, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_Hunter_Medium, "Poison",
                    "1d4", AbilityConstants.Strength, "Initial",
                    "1d4", AbilityConstants.Strength, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_Hunter_Large, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_Hunter_Large, "Poison",
                    "1d6", AbilityConstants.Strength, "Initial",
                    "1d6", AbilityConstants.Strength, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_Hunter_Huge, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_Hunter_Huge, "Poison",
                    "1d8", AbilityConstants.Strength, "Initial",
                    "1d8", AbilityConstants.Strength, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_Hunter_Gargantuan, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_Hunter_Gargantuan, "Poison",
                    "2d6", AbilityConstants.Strength, "Initial",
                    "2d6", AbilityConstants.Strength, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_Hunter_Colossal, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_Hunter_Colossal, "Poison",
                    "2d8", AbilityConstants.Strength, "Initial",
                    "2d8", AbilityConstants.Strength, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_WebSpinner_Tiny, "Bite", "1d3", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_WebSpinner_Tiny, "Poison",
                    "1d2", AbilityConstants.Strength, "Initial",
                    "1d2", AbilityConstants.Strength, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_WebSpinner_Small, "Bite", "1d4", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_WebSpinner_Small, "Poison",
                    "1d3", AbilityConstants.Strength, "Initial",
                    "1d3", AbilityConstants.Strength, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_WebSpinner_Medium, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_WebSpinner_Medium, "Poison",
                    "1d4", AbilityConstants.Strength, "Initial",
                    "1d4", AbilityConstants.Strength, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_WebSpinner_Large, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_WebSpinner_Large, "Poison",
                    "1d6", AbilityConstants.Strength, "Initial",
                    "1d6", AbilityConstants.Strength, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_WebSpinner_Huge, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_WebSpinner_Huge, "Poison",
                    "1d8", AbilityConstants.Strength, "Initial",
                    "1d8", AbilityConstants.Strength, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan, "Poison",
                    "2d6", AbilityConstants.Strength, "Initial",
                    "2d6", AbilityConstants.Strength, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_WebSpinner_Colossal, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Spider_Monstrous_WebSpinner_Colossal, "Poison",
                    "2d8", AbilityConstants.Strength, "Initial",
                    "2d8", AbilityConstants.Strength, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.SpiderEater, "Sting", "1d8", AttributeConstants.DamageTypes.Piercing));
            attackDamages.Add(BuildData(CreatureConstants.SpiderEater, "Bite", "1d8", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Spider_Swarm, "Swarm", "1d6"));
            attackDamages.Add(BuildData(CreatureConstants.Spider_Swarm, "Poison",
                    "1d3", AbilityConstants.Strength, "Initial",
                    "1d3", AbilityConstants.Strength, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Squid, "Arms", "0", tentacleDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Squid, "Bite", "1d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Squid_Giant, "Tentacle", "1d6", tentacleDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Squid_Giant, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Squid_Giant, "Constrict", "1d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.StagBeetle_Giant, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.StagBeetle_Giant, "Trample", "2d8", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Stirge, "Blood Drain", "1d4", AbilityConstants.Constitution));

            attackDamages.Add(BuildData(CreatureConstants.Succubus, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Succubus, "Energy Drain", "1", "Negative Level"));

            attackDamages.Add(BuildData(CreatureConstants.Tarrasque, "Bite", "4d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Tarrasque, "Horn", "1d10", AttributeConstants.DamageTypes.Piercing));
            attackDamages.Add(BuildData(CreatureConstants.Tarrasque, "Claw", "1d12", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Tarrasque, "Tail Slap", "3d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Tarrasque, "Swallow Whole",
                    "2d8+8", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "2d8+6", FeatConstants.Foci.Elements.Acid));

            attackDamages.Add(BuildData(CreatureConstants.Tendriculos, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Tendriculos, "Tendril", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Tendriculos, "Swallow Whole", "2d6", FeatConstants.Foci.Elements.Acid));

            attackDamages.Add(BuildData(CreatureConstants.Thoqqua, "Slam", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Thoqqua, "Heat", "2d6", FeatConstants.Foci.Elements.Fire));

            attackDamages.Add(BuildData(CreatureConstants.Tiefling, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Tiger, "Claw", "1d8", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Tiger, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Tiger, "Rake", "1d8", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Tiger_Dire, "Claw", "2d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Tiger_Dire, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Tiger_Dire, "Rake", "2d4", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Titan, "Slam", "1d8", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Tojanida_Juvenile, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Tojanida_Juvenile, "Claw", "1d4", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Tojanida_Adult, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Tojanida_Adult, "Claw", "1d4", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Tojanida_Elder, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Tojanida_Elder, "Claw", "1d4", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Treant, "Slam", "2d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Treant, "Trample", "2d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Triceratops, "Gore", "2d8", goreDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Triceratops, "Powerful charge", "4d8", goreDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Triceratops, "Trample", "2d12", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Triton, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Troglodyte, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Troglodyte, "Bite", "1d4", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Troll, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Troll, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Troll, "Rend", "2d6", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Troll_Scrag, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Troll_Scrag, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Troll_Scrag, "Rend", "2d6", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.TrumpetArchon, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Tyrannosaurus, "Bite", "3d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Tyrannosaurus, "Swallow Whole",
                    "2d8", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "8", FeatConstants.Foci.Elements.Acid));

            attackDamages.Add(BuildData(CreatureConstants.UmberHulk, "Claw", "2d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.UmberHulk, "Bite", "2d8", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.UmberHulk_TrulyHorrid, "Claw", "3d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.UmberHulk_TrulyHorrid, "Bite", "4d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Unicorn, "Horn", "1d8", AttributeConstants.DamageTypes.Piercing));
            attackDamages.Add(BuildData(CreatureConstants.Unicorn, "Hoof", "1d4", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.VampireSpawn, "Slam", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.VampireSpawn, "Blood Drain", "1d4", AbilityConstants.Constitution));
            attackDamages.Add(BuildData(CreatureConstants.VampireSpawn, "Energy Drain", "1", "Negative Level"));

            attackDamages.Add(BuildData(CreatureConstants.Vargouille, "Bite", "1d4", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.VioletFungus, "Tentacle", "1d6", tentacleDamageType));
            attackDamages.Add(BuildData(CreatureConstants.VioletFungus, "Poison",
                    "1d4", AbilityConstants.Strength, "Initial",
                    "1d4", AbilityConstants.Constitution, "Initial",
                    "1d4", AbilityConstants.Strength, "Secondary",
                    "1d4", AbilityConstants.Constitution, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Vrock, "Claw", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Vrock, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Vrock, "Talon", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Vrock, "Spores", "1d8"));

            attackDamages.Add(BuildData(CreatureConstants.Wasp_Giant, "Sting", "1d3", AttributeConstants.DamageTypes.Piercing));
            attackDamages.Add(BuildData(CreatureConstants.Wasp_Giant, "Poison",
                    "1d6", AbilityConstants.Dexterity, "Initial",
                    "1d6", AbilityConstants.Dexterity, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Weasel, "Bite", "1d3", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Weasel, "Attach", "1d3", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Weasel_Dire, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Weasel_Dire, "Blood Drain", "1d4", AbilityConstants.Constitution));

            attackDamages.Add(BuildData(CreatureConstants.Whale_Baleen, "Tail Slap", "1d8", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Whale_Cachalot, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Whale_Cachalot, "Tail Slap", "1d8", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Whale_Orca, "Bite", "2d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Wight, "Slam", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Wight, "Energy Drain", "1", "Negative Level"));

            attackDamages.Add(BuildData(CreatureConstants.WillOWisp, "Shock", "2d8", FeatConstants.Foci.Elements.Electricity));

            attackDamages.Add(BuildData(CreatureConstants.WinterWolf, "Bite", "1d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.WinterWolf, "Breath Weapon", "4d6", FeatConstants.Foci.Elements.Cold));
            attackDamages.Add(BuildData(CreatureConstants.WinterWolf, "Freezing Bite", "1d6", FeatConstants.Foci.Elements.Cold));

            attackDamages.Add(BuildData(CreatureConstants.Wolf, "Bite", "1d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Wolf_Dire, "Bite", "1d8", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Wolverine, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Wolverine, "Bite", "1d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Wolverine_Dire, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Wolverine_Dire, "Bite", "1d8", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Worg, "Bite", "1d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Wraith, "Incorporeal touch", "1d4"));
            attackDamages.Add(BuildData(CreatureConstants.Wraith, "Constitution Drain", "1d4", AbilityConstants.Constitution));

            attackDamages.Add(BuildData(CreatureConstants.Wraith_Dread, "Incorporeal touch", "2d6"));
            attackDamages.Add(BuildData(CreatureConstants.Wraith_Dread, "Constitution Drain", "1d8", AbilityConstants.Constitution));

            attackDamages.Add(BuildData(CreatureConstants.Wyvern, "Sting", "1d6", stingDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Wyvern, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Wyvern, "Wing", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Wyvern, "Talon", "2d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Wyvern, "Poison",
                    "2d6", AbilityConstants.Constitution, "Initial",
                    "2d6", AbilityConstants.Constitution, "Secondary"));

            attackDamages.Add(BuildData(CreatureConstants.Xill, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Xill, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Xill, "Bite", "0", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Xorn_Minor, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Xorn_Minor, "Claw", "1d3", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Xorn_Average, "Bite", "4d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Xorn_Average, "Claw", "1d4", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Xorn_Elder, "Bite", "4d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Xorn_Elder, "Claw", "1d6", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.YethHound, "Bite", "1d8", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Yrthak, "Bite", "2d8", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Yrthak, "Claw", "1d6", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Yrthak, "Sonic Lance", "6d6", FeatConstants.Foci.Elements.Sonic));
            attackDamages.Add(BuildData(CreatureConstants.Yrthak, "Explosion", "2d6", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.YuanTi_Pureblood, "Unarmed Strike", "1d3", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeHead, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeHead, "Poison",
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"));
            attackDamages.Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeHead, "Produce Acid",
                    "3d6", FeatConstants.Foci.Elements.Acid, string.Empty,
                    "2d6", FeatConstants.Foci.Elements.Acid, "Grappling or pinning foe"));

            attackDamages.Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeArms, "Bite", "1d4", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeArms, "Poison",
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"));
            attackDamages.Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeArms, "Produce Acid",
                    "3d6", FeatConstants.Foci.Elements.Acid, string.Empty,
                    "2d6", FeatConstants.Foci.Elements.Acid, "Grappling or pinning foe"));

            attackDamages.Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, "Poison",
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"));
            attackDamages.Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, "Produce Acid",
                    "3d6", FeatConstants.Foci.Elements.Acid, string.Empty,
                    "2d6", FeatConstants.Foci.Elements.Acid, "Grappling or pinning foe"));
            attackDamages.Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, "Constrict", "1d4", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTail, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTail, "Poison",
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"));
            attackDamages.Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTail, "Produce Acid",
                    "3d6", FeatConstants.Foci.Elements.Acid, string.Empty,
                    "2d6", FeatConstants.Foci.Elements.Acid, "Grappling or pinning foe"));
            attackDamages.Add(BuildData(CreatureConstants.YuanTi_Halfblood_SnakeTail, "Constrict", "1d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.YuanTi_Abomination, "Bite", "2d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.YuanTi_Abomination, "Poison",
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"));
            attackDamages.Add(BuildData(CreatureConstants.YuanTi_Abomination, "Produce Acid",
                    "3d6", FeatConstants.Foci.Elements.Acid, string.Empty,
                    "2d6", FeatConstants.Foci.Elements.Acid, "Grappling or pinning foe"));
            attackDamages.Add(BuildData(CreatureConstants.YuanTi_Abomination, "Constrict", "1d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Zelekhut, "Unarmed Strike", "1d4", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Zelekhut, "Electrified Weapon", "1d6", FeatConstants.Foci.Elements.Electricity));

            foreach (var kvp in attackDamages.SelectMany(d => d))
            {
                testCases[kvp.Key] = kvp.Value;
            }

            return testCases;
        }

        internal static Dictionary<string, List<string>> GetTemplateDamageData(
            Dictionary<string, IEnumerable<AttackDataSelection>> attackDataSelections,
            DamageHelper damageHelper)
        {
            var testCases = new Dictionary<string, List<string>>();
            var damageKeys = damageHelper.GetAllTemplateDamageKeys();

            foreach (var key in damageKeys)
                testCases[key] = [];

            var attackDamages = new List<Dictionary<string, List<string>>>();
            var templates = CreatureConstants.Templates.GetAll();

            var attackData = attackDataSelections.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value
                    .GroupBy(a => a.Name)
                    .ToDictionary(g => g.Key, g => g.ToArray()));

            var biteDamageType = $"{AttributeConstants.DamageTypes.Piercing}/{AttributeConstants.DamageTypes.Slashing}/{AttributeConstants.DamageTypes.Bludgeoning}";
            var clawDamageType = $"{AttributeConstants.DamageTypes.Piercing}/{AttributeConstants.DamageTypes.Slashing}";
            var goreDamageType = $"{AttributeConstants.DamageTypes.Piercing}";
            var slapSlamDamageType = $"{AttributeConstants.DamageTypes.Bludgeoning}";
            var stingDamageType = $"{AttributeConstants.DamageTypes.Piercing}";
            var tentacleDamageType = $"{AttributeConstants.DamageTypes.Bludgeoning}";

            Dictionary<string, List<string>> BuildData(string creature, string attackName,
                string roll, string type = "", string condition = "",
                string roll2 = null, string type2 = null, string condition2 = null,
                string roll3 = null, string type3 = null, string condition3 = null,
                string roll4 = null, string type4 = null, string condition4 = null)
            {
                return BuildDamageDataForAttack(
                    creature,
                    SizeConstants.Medium,
                    attackData[creature][attackName],
                    orderedSizes,
                    [(roll, type, condition),
                    (roll2, type2, condition2),
                    (roll3, type3, condition3),
                    (roll4, type4, condition4)]);
            }

            attackDamages.Add(BuildData(CreatureConstants.Templates.Ghost, "Corrupting Gaze",
                    "2d10", string.Empty, string.Empty,
                    "1d4", AbilityConstants.Charisma));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Ghost, "Corrupting Touch", "1d6"));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Ghost, "Draining Touch", "1d4", "Ability points (of ghost's choosing)"));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Ghost, "Horrific Appearance",
                    "1d4", AbilityConstants.Strength, string.Empty,
                    "1d4", AbilityConstants.Dexterity, string.Empty,
                    "1d4", AbilityConstants.Constitution));

            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Black, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Black, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Black, "Breath Weapon", "6d8", FeatConstants.Foci.Elements.Acid));

            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Blue, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Blue, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Blue, "Breath Weapon", "6d8", FeatConstants.Foci.Elements.Electricity));

            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Brass, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Brass, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Brass, "Breath Weapon", "6d8", FeatConstants.Foci.Elements.Fire));

            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Bronze, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Bronze, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Bronze, "Breath Weapon", "6d8", FeatConstants.Foci.Elements.Electricity));

            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Copper, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Copper, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Copper, "Breath Weapon", "6d8", FeatConstants.Foci.Elements.Acid));

            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Gold, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Gold, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Gold, "Breath Weapon", "6d8", FeatConstants.Foci.Elements.Fire));

            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Green, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Green, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Green, "Breath Weapon", "6d8", FeatConstants.Foci.Elements.Acid, "Gas"));

            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Red, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Red, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Red, "Breath Weapon", "6d8", FeatConstants.Foci.Elements.Fire));

            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Silver, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Silver, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_Silver, "Breath Weapon", "6d8", FeatConstants.Foci.Elements.Cold));

            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_White, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_White, "Bite", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfDragon_White, "Breath Weapon", "6d8", FeatConstants.Foci.Elements.Cold));

            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfFiend, "Claw", "1d4", clawDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Templates.HalfFiend, "Bite", "1d6", biteDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Lich, "Touch", "1d8+5"));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Skeleton, "Claw", "1d4", clawDamageType));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Vampire, "Slam", "1d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Vampire, "Blood Drain", "1d4", AbilityConstants.Constitution));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Vampire, "Energy Drain", "2", "Negative Level"));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural, "Claw (in Hybrid form)", "1d4", AttributeConstants.DamageTypes.Slashing));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural, "Bite (in Hybrid form)", "1d6", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Natural, "Claw (in Hybrid form)", "1d4", AttributeConstants.DamageTypes.Slashing));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Natural, "Gore (in Hybrid form)", "1d6", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural, "Claw (in Hybrid form)", "1d4", AttributeConstants.DamageTypes.Slashing));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural, "Gore (in Hybrid form)", "1d6", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Natural, "Claw (in Hybrid form)", "1d4", AttributeConstants.DamageTypes.Slashing));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Natural, "Bite (in Hybrid form)", "1d6", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural, "Claw (in Hybrid form)", "1d4", AttributeConstants.DamageTypes.Slashing));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural, "Bite (in Hybrid form)", "1d6", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Natural, "Claw (in Hybrid form)", "1d4", AttributeConstants.DamageTypes.Slashing));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Natural, "Bite (in Hybrid form)", "1d6", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural, "Claw (in Hybrid form)", "1d4", AttributeConstants.DamageTypes.Slashing));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural, "Bite (in Hybrid form)", "1d6", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Natural, "Claw (in Hybrid form)", "1d4", AttributeConstants.DamageTypes.Slashing));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Natural, "Bite (in Hybrid form)", "1d6", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural, "Claw (in Hybrid form)", "1d4", AttributeConstants.DamageTypes.Slashing));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural, "Bite (in Hybrid form)", "1d6", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted, "Claw (in Hybrid form)",
                "1d4", AttributeConstants.DamageTypes.Slashing));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted, "Bite (in Hybrid form)",
                "1d6", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Afflicted, "Claw (in Hybrid form)",
                "1d4", AttributeConstants.DamageTypes.Slashing));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Afflicted, "Gore (in Hybrid form)",
                "1d6", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted, "Claw (in Hybrid form)",
                "1d4", AttributeConstants.DamageTypes.Slashing));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted, "Gore (in Hybrid form)",
                "1d6", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Afflicted, "Claw (in Hybrid form)",
                "1d4", AttributeConstants.DamageTypes.Slashing));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Afflicted, "Bite (in Hybrid form)",
                "1d6", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted, "Claw (in Hybrid form)",
                "1d4", AttributeConstants.DamageTypes.Slashing));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted, "Bite (in Hybrid form)",
                "1d6", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted, "Claw (in Hybrid form)",
                "1d4", AttributeConstants.DamageTypes.Slashing));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted, "Bite (in Hybrid form)",
                "1d6", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted, "Claw (in Hybrid form)",
                "1d4", AttributeConstants.DamageTypes.Slashing));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted, "Bite (in Hybrid form)",
                "1d6", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted, "Claw (in Hybrid form)",
                "1d4", AttributeConstants.DamageTypes.Slashing));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted, "Bite (in Hybrid form)",
                "1d6", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted, "Claw (in Hybrid form)",
                "1d4", AttributeConstants.DamageTypes.Slashing));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted, "Bite (in Hybrid form)",
                "1d6", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Zombie, "Slam", "1d6", AttributeConstants.DamageTypes.Bludgeoning));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Black_Afflicted, "Claw (in Hybrid form)",
                "1d4", AttributeConstants.DamageTypes.Slashing));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Black_Afflicted, "Bite (in Hybrid form)",
                "1d6", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural, "Claw (in Hybrid form)",
                "1d4", AttributeConstants.DamageTypes.Slashing));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural, "Bite (in Hybrid form)",
                "1d6", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Dire_Afflicted, "Claw (in Hybrid form)",
                "1d4", AttributeConstants.DamageTypes.Slashing));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Dire_Afflicted, "Bite (in Hybrid form)",
                "1d6", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural, "Claw (in Hybrid form)",
                "1d4", AttributeConstants.DamageTypes.Slashing));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural, "Bite (in Hybrid form)",
                "1d6", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Polar_Afflicted, "Claw (in Hybrid form)",
                "1d4", AttributeConstants.DamageTypes.Slashing));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Polar_Afflicted, "Bite (in Hybrid form)",
                "1d6", AttributeConstants.DamageTypes.Piercing));

            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural, "Claw (in Hybrid form)",
                "1d4", AttributeConstants.DamageTypes.Slashing));
            attackDamages.Add(BuildData(CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural, "Bite (in Hybrid form)",
                "1d6", AttributeConstants.DamageTypes.Piercing));

            foreach (var kvp in attackDamages.SelectMany(d => d))
            {
                testCases[kvp.Key] = kvp.Value;
            }

            return testCases;
        }

        private static Dictionary<string, List<string>> BuildDamageDataForAttack(
            string creature,
            string originalSize,
            AttackDataSelection[] attacks,
            IEnumerable<string> adjustedSizes,
            (string roll, string type, string condition)[] damages)
        {
            var data = new Dictionary<string, List<string>>();
            var validDamages = damages.Where(d => d.roll != null);

            foreach (var attack in attacks)
            {
                var originalKey = attack.BuildDamageKey(creature, originalSize);
                data[originalKey] = [];

                foreach (var damage in validDamages)
                {
                    data[originalKey].Add(BuildData(damage.roll, damage.type, damage.condition));
                }

                foreach (var adjustedSize in adjustedSizes.Except([originalSize]))
                {
                    var adjustedKey = attack.BuildDamageKey(creature, adjustedSize);
                    data[adjustedKey] = [];

                    foreach (var damage in validDamages)
                    {
                        var adjustedRoll = GetAdjustedDamage(attack, damage.roll, originalSize, adjustedSize);
                        data[adjustedKey].Add(BuildData(adjustedRoll, damage.type, damage.condition));
                    }
                }
            }

            return data;
        }

        private static readonly string[] orderedSizes = SizeConstants.GetOrdered();
        private static readonly Dictionary<string, string> damageIncreases = new()
        {
            ["2d8"] = "3d8",
            ["1d10"] = "2d8",
            ["2d6"] = "3d6",
            ["1d8"] = "2d6",
            ["1d6"] = "1d8",
            ["1d4"] = "1d6",
            ["1d3"] = "1d4",
            ["1d2"] = "1d3",
        };
        private static readonly Dictionary<string, string> damageDecreases = damageIncreases.ToDictionary(kvp => kvp.Value, kvp => kvp.Key);
        private static readonly Dictionary<string, string> dieDecreases = new()
        {
            ["100"] = "20",
            ["20"] = "12",
            ["12"] = "10",
            ["10"] = "8",
        };

        internal static string GetAdjustedDamage(AttackDataSelection attack, string originalDamage, string originalSize, string adjustedSize)
        {
            if (!attack.IsNatural || attack.IsSpecial || string.IsNullOrEmpty(originalDamage))
                return originalDamage;

            var adjustedDamage = originalDamage;
            var sizeDifference = Array.IndexOf(orderedSizes, adjustedSize) - Array.IndexOf(orderedSizes, originalSize);
            var increase = sizeDifference > 0;
            sizeDifference = Math.Abs(sizeDifference);

            while (sizeDifference-- > 0)
            {
                adjustedDamage = increase ? IncreaseDamage(adjustedDamage) : DecreaseDamage(adjustedDamage);
            }

            return adjustedDamage;
        }

        private static string IncreaseDamage(string damage)
        {
            var roll = GetRoll(damage);
            if (damageIncreases.ContainsKey(roll) && damage.Contains('+'))
                return $"{damageIncreases[roll]}+{damage.Split('+')[1]}";
            else if (damageIncreases.ContainsKey(roll))
                return damageIncreases[roll];

            var sections = roll.Split('d', '+');
            var quantity = Convert.ToInt32(sections[0]) + 1;

            if (sections.Length == 1 && quantity == 2)
                return "1d2";
            else if (sections.Length == 1)
                return quantity.ToString();

            return $"{quantity}d{sections[1]}";
        }

        private static string GetRoll(string damage) => damage.Split('+')[0];

        private static string DecreaseDamage(string damage)
        {
            var roll = GetRoll(damage);
            if (damageDecreases.ContainsKey(roll) && damage.Contains('+'))
                return $"{damageDecreases[roll]}+{damage.Split('+')[1]}";
            else if (damageDecreases.ContainsKey(roll))
                return damageDecreases[roll];

            var sections = roll.Split('d', '+');
            var quantity = Convert.ToInt32(sections[0]) - 1;

            if (sections.Length == 1 && quantity <= 0)
            {
                return "0";
            }
            else if (sections.Length == 1)
            {
                return quantity.ToString();
            }
            else if (quantity <= 0)
            {
                var die = sections[1];
                if (die == "2")
                    return "1";

                return $"1d{dieDecreases[die]}";
            }

            return $"{quantity}d{sections[1]}";
        }

        internal static string BuildData(string roll, string type, string condition = "")
        {
            return DataHelper.Parse(new DamageDataSelection
            {
                Roll = roll,
                Type = type,
                Condition = condition,
            });
        }
    }
}
