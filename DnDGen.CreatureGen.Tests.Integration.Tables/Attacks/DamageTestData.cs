using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Magics;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tests.Integration.Tables.Creatures;
using DnDGen.Infrastructure.Helpers;
using DnDGen.TreasureGen.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Attacks
{
    public static class DamageTestData
    {
        public static Dictionary<string, List<string>> GetCreatureAttackDamageData()
        {
            var testCases = new Dictionary<string, List<string>>();
            var attackDamages = new List<Dictionary<string, List<string>>>();
            var attackData = AttackTestData.GetCreatureAttackData().ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value
                    .Select(DataHelper.Parse<AttackDataSelection>)
                    .GroupBy(a => a.Name)
                    .ToDictionary(g => g.Key, g => g.ToArray()));
            var creatureData = CreatureDataTests.GetCreatureTestData().ToDictionary(kvp => kvp.Key, kvp => DataHelper.Parse<CreatureDataSelection>(kvp.Value));
            var advancementData = AdvancementsTests.GetAdvancementsTestData().ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Select(DataHelper.Parse<AdvancementDataSelection>));

            var biteDamageType = $"{AttributeConstants.DamageTypes.Piercing}/{AttributeConstants.DamageTypes.Slashing}/{AttributeConstants.DamageTypes.Bludgeoning}";
            var clawDamageType = $"{AttributeConstants.DamageTypes.Piercing}/{AttributeConstants.DamageTypes.Slashing}";
            var goreDamageType = $"{AttributeConstants.DamageTypes.Piercing}";
            var slapSlamDamageType = $"{AttributeConstants.DamageTypes.Bludgeoning}";
            var stingDamageType = $"{AttributeConstants.DamageTypes.Piercing}";
            var tentacleDamageType = $"{AttributeConstants.DamageTypes.Bludgeoning}";

            var damageKeys = AttackTestData.GetDamageKeys();
            foreach (var key in damageKeys)
            {
                testCases[key] = [];
            }

            Dictionary<string, List<string>> BuildData(string creature, string attackName,
                string roll, string type = "", string condition = "",
                string roll2 = null, string type2 = null, string condition2 = null)
            {
                return BuildDamageDataForAttack(
                    creature,
                    creatureData[creature],
                    attackData[creature][attackName],
                    advancementData[creature],
                    roll, type, condition, roll2, type2, condition2);
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
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large_MultipleLegs, "Slam", "1d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large_MultipleLegs, "Trample", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden, "Slam", "1d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden, "Trample", "1d8", AttributeConstants.DamageTypes.Bludgeoning));
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
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge_MultipleLegs, "Slam", "2d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge_MultipleLegs, "Trample", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden, "Slam", "2d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden, "Trample", "2d6", AttributeConstants.DamageTypes.Bludgeoning));
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
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs, "Slam", "2d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs, "Trample", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden, "Slam", "2d8", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden, "Trample", "2d8", AttributeConstants.DamageTypes.Bludgeoning));
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
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal_MultipleLegs, "Slam", "4d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal_MultipleLegs, "Trample", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden, "Slam", "4d6", slapSlamDamageType));
            attackDamages.Add(BuildData(CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden, "Trample", "4d6", AttributeConstants.DamageTypes.Bludgeoning));
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

            attackDamages.Add(BuildData(CreatureConstants.Ant_Giant_Worker, "Bite", "2d4", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Ant_Giant_Worker, "Acid Sting", "1d4", stingDamageType, roll2: "1d4", type2: FeatConstants.Foci.Elements.Acid));

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

            attackDamages.Add(BuildData(CreatureConstants.Bat_Swarm, "Swarm", "1d6", biteDamageType));
            attackDamages.Add(BuildData(CreatureConstants.Bat_Swarm, "Wounding", "1", biteDamageType));

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

            attackDamages[CreatureConstants.Cat] = BuildData("Claw", damageHelper.BuildEntries("1d2", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Cat] = BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            attackDamages[CreatureConstants.Centaur] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Centaur] = BuildData("Hoof",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));

            attackDamages[CreatureConstants.Centipede_Monstrous_Tiny] = BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Centipede_Monstrous_Tiny] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1", AbilityConstants.Dexterity, "Initial",
                    "1", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Centipede_Monstrous_Small] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Centipede_Monstrous_Small] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d2", AbilityConstants.Dexterity, "Initial",
                    "1d2", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Centipede_Monstrous_Medium] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Centipede_Monstrous_Medium] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d3", AbilityConstants.Dexterity, "Initial",
                    "1d3", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Centipede_Monstrous_Large] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Centipede_Monstrous_Large] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Dexterity, "Initial",
                    "1d4", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Centipede_Monstrous_Huge] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Centipede_Monstrous_Huge] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Dexterity, "Initial",
                    "1d6", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Centipede_Monstrous_Gargantuan] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Centipede_Monstrous_Gargantuan] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d8", AbilityConstants.Dexterity, "Initial",
                    "1d8", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Centipede_Monstrous_Colossal] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Centipede_Monstrous_Colossal] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "2d6", AbilityConstants.Dexterity, "Initial",
                    "2d6", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Centipede_Swarm] = BuildData("Swarm", damageHelper.BuildEntries("2d6"), "Poison", 0, "swarm", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Centipede_Swarm] = BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            attackDamages[CreatureConstants.Centipede_Swarm] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Dexterity, "Initial",
                    "1d4", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            attackDamages[CreatureConstants.ChainDevil_Kyton] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.ChainDevil_Kyton] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.ChainDevil_Kyton] = BuildData("Dancing Chains", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
            attackDamages[CreatureConstants.ChainDevil_Kyton] = BuildData("Unnerving Gaze", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));

            attackDamages[CreatureConstants.ChaosBeast] = BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), "Corporeal Instability", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.ChaosBeast] = BuildData("Corporeal Instability", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Cheetah] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Cheetah] = BuildData("Claw", damageHelper.BuildEntries("1d2", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Cheetah] = BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            attackDamages[CreatureConstants.Chimera_Black] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Chimera_Black] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Chimera_Black] = BuildData("Gore", damageHelper.BuildEntries("1d8", goreDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Chimera_Black] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Chimera_Black] = BuildData("Breath weapon", damageHelper.BuildEntries("3d8", FeatConstants.Foci.Elements.Acid), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Chimera_Blue] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Chimera_Blue] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Chimera_Blue] = BuildData("Gore", damageHelper.BuildEntries("1d8", goreDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Chimera_Blue] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Chimera_Blue] = BuildData("Breath weapon", damageHelper.BuildEntries("3d8", FeatConstants.Foci.Elements.Electricity), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Chimera_Green] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Chimera_Green] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Chimera_Green] = BuildData("Gore", damageHelper.BuildEntries("1d8", goreDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Chimera_Green] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Chimera_Green] = BuildData("Breath weapon", damageHelper.BuildEntries("3d8", FeatConstants.Foci.Elements.Acid), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Chimera_Red] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Chimera_Red] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Chimera_Red] = BuildData("Gore", damageHelper.BuildEntries("1d8", goreDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Chimera_Red] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Chimera_Red] = BuildData("Breath weapon", damageHelper.BuildEntries("3d8", FeatConstants.Foci.Elements.Fire), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Chimera_White] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Chimera_White] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Chimera_White] = BuildData("Gore", damageHelper.BuildEntries("1d8", goreDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Chimera_White] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Chimera_White] = BuildData("Breath weapon", damageHelper.BuildEntries("3d8", FeatConstants.Foci.Elements.Cold), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Choker] = BuildData("Tentacle", damageHelper.BuildEntries("1d3", tentacleDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Choker] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Choker] = BuildData("Constrict", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Chuul] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Chuul] = BuildData("Constrict", damageHelper.BuildEntries("3d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Chuul] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Chuul] = BuildData("Paralytic Tentacles", damageHelper.BuildEntries("1d8", tentacleDamageType), "6 round paralysis", 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Cloaker] = BuildData("Tail slap", damageHelper.BuildEntries("1d6", slapSlamDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Cloaker] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Cloaker] = BuildData("Moan", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma));
            attackDamages[CreatureConstants.Cloaker] = BuildData("Engulf", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Cockatrice] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), "Petrification", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Cockatrice] = BuildData("Petrification", string.Empty, string.Empty, 1, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Couatl] = BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Couatl] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "2d4", AbilityConstants.Strength, "Initial",
                    "4d4", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Couatl] = BuildData("Constrict", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Couatl] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Couatl] = BuildData(FeatConstants.SpecialQualities.Psionic, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Couatl] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Couatl] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Criosphinx] = BuildData("Gore", damageHelper.BuildEntries("2d6", goreDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Criosphinx] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Criosphinx] = BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            attackDamages[CreatureConstants.Criosphinx] = BuildData("Rake", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            attackDamages[CreatureConstants.Crocodile] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Crocodile] = BuildData("Tail Slap", damageHelper.BuildEntries("1d12", slapSlamDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Crocodile] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Crocodile_Giant] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Crocodile_Giant] = BuildData("Tail Slap", damageHelper.BuildEntries("1d12", slapSlamDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Crocodile_Giant] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Cryohydra_5Heads] = BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 5, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Cryohydra_5Heads] = BuildData("Breath weapon",
                damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Cold, "Per living head"),
                string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Cryohydra_6Heads] = BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 6, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Cryohydra_6Heads] = BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Cold, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Cryohydra_7Heads] = BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 7, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Cryohydra_7Heads] = BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Cold, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Cryohydra_8Heads] = BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 8, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Cryohydra_8Heads] = BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Cold, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Cryohydra_9Heads] = BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 9, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Cryohydra_9Heads] = BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Cold, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Cryohydra_10Heads] = BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 10, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Cryohydra_10Heads] = BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Cold, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Cryohydra_11Heads] = BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 11, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Cryohydra_11Heads] = BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Cold, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Cryohydra_12Heads] = BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 12, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Cryohydra_12Heads] = BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Cold, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Darkmantle] = BuildData("Slam", damageHelper.BuildEntries("1d4", slapSlamDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Darkmantle] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Darkmantle] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Darkmantle] = BuildData("Constrict", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Deinonychus] = BuildData("Talons", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Deinonychus] = BuildData("Foreclaw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Deinonychus] = BuildData("Bite", damageHelper.BuildEntries("2d4", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Deinonychus] = BuildData("Pounce", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            attackDamages[CreatureConstants.Delver] = BuildData("Slam",
                damageHelper.BuildEntries(
                    "1d6", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "2d6", FeatConstants.Foci.Elements.Acid),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Delver] = BuildData("Corrosive Slime", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Delver] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Derro] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Derro] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Derro] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Derro] = BuildData("Poison use", string.Empty, "Greenblood Oil or Monstrous Spider Venom", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Derro] = BuildData("Greenblood Oil",
                damageHelper.BuildEntries(
                    "1", AbilityConstants.Constitution, "Initial",
                    "1d2", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, false, true, true, save: SaveConstants.Fortitude, saveDcBonus: 3));
            attackDamages[CreatureConstants.Derro] = BuildData("Monstrous Spider Venom",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Strength, "Initial",
                    "1d4", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, false, true, true, save: SaveConstants.Fortitude, saveDcBonus: 2));
            attackDamages[CreatureConstants.Derro] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Derro] = BuildData("Sneak Attack", damageHelper.BuildEntries("1d6"), string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Derro_Sane] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Derro_Sane] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Derro_Sane] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Derro_Sane] = BuildData("Poison use", string.Empty, "Greenblood Oil or Monstrous Spider Venom", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Derro_Sane] = BuildData("Greenblood Oil",
                damageHelper.BuildEntries(
                    "1", AbilityConstants.Constitution, "Initial",
                    "1d2", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, false, true, true, save: SaveConstants.Fortitude, saveDcBonus: 3));
            attackDamages[CreatureConstants.Derro_Sane] = BuildData("Monstrous Spider Venom",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Strength, "Initial",
                    "1d4", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, false, true, true, save: SaveConstants.Fortitude, saveDcBonus: 2));
            attackDamages[CreatureConstants.Derro_Sane] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Derro_Sane] = BuildData("Sneak Attack", damageHelper.BuildEntries("1d6"), string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Destrachan] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Destrachan] = BuildData("Destructive harmonics", string.Empty, string.Empty, 1, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Charisma));

            attackDamages[CreatureConstants.Devourer] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Devourer] = BuildData("Energy Drain", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Charisma));
            attackDamages[CreatureConstants.Devourer] = BuildData("Trap Essence", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Devourer] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Digester] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Digester] = BuildData("Acid Spray (Cone)",
                damageHelper.BuildEntries("4d8", FeatConstants.Foci.Elements.Acid),
                string.Empty, 0, "extraordinary ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Digester] = BuildData("Acid Spray (Stream)",
                damageHelper.BuildEntries("8d8", FeatConstants.Foci.Elements.Acid),
                string.Empty, 0, "extraordinary ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.DisplacerBeast] = BuildData("Tentacle", damageHelper.BuildEntries("1d6", tentacleDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.DisplacerBeast] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            attackDamages[CreatureConstants.DisplacerBeast_PackLord] = BuildData("Tentacle", damageHelper.BuildEntries("1d8", tentacleDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.DisplacerBeast_PackLord] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            attackDamages[CreatureConstants.Djinni] = BuildData("Slam", damageHelper.BuildEntries("1d8", slapSlamDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Djinni] = BuildData("Air mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Djinni] = BuildData("Whirlwind", string.Empty, string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex, saveDcBonus: 3));
            attackDamages[CreatureConstants.Djinni] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Djinni_Noble] = BuildData("Slam", damageHelper.BuildEntries("1d8", slapSlamDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Djinni_Noble] = BuildData("Air mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Djinni_Noble] = BuildData("Whirlwind", string.Empty, string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex, saveDcBonus: 3));
            attackDamages[CreatureConstants.Djinni_Noble] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Dog] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Dog_Riding] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Donkey] = BuildData("Bite", damageHelper.BuildEntries("1d2", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Doppelganger] = BuildData("Slam", damageHelper.BuildEntries("1d6", slapSlamDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Doppelganger] = BuildData("Detect Thoughts", string.Empty, string.Empty, 1, "supernatural ability", 0, FeatConstants.Frequencies.Constant, false, true, true, true));
            attackDamages[CreatureConstants.Doppelganger] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.DragonTurtle] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.DragonTurtle] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.DragonTurtle] = BuildData("Breath Weapon", damageHelper.BuildEntries("12d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
            attackDamages[CreatureConstants.DragonTurtle] = BuildData("Capsize", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //Tiny
            attackDamages[CreatureConstants.Dragon_Black_Wyrmling] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Black_Wyrmling] = BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_Wyrmling] = BuildData("Breath Weapon", damageHelper.BuildEntries("2d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //small
            attackDamages[CreatureConstants.Dragon_Black_VeryYoung] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Black_VeryYoung] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_VeryYoung] = BuildData("Breath Weapon", damageHelper.BuildEntries("4d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //medium
            attackDamages[CreatureConstants.Dragon_Black_Young] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Black_Young] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_Young] = BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_Young] = BuildData("Breath Weapon", damageHelper.BuildEntries("6d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //medium
            attackDamages[CreatureConstants.Dragon_Black_Juvenile] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Black_Juvenile] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_Juvenile] = BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_Juvenile] = BuildData("Breath Weapon", damageHelper.BuildEntries("8d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Black_Juvenile] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //large
            attackDamages[CreatureConstants.Dragon_Black_YoungAdult] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Black_YoungAdult] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_YoungAdult] = BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_YoungAdult] = BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_YoungAdult] = BuildData("Breath Weapon", damageHelper.BuildEntries("10d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Black_YoungAdult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Black_YoungAdult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Black_YoungAdult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //large
            attackDamages[CreatureConstants.Dragon_Black_Adult] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Black_Adult] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_Adult] = BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_Adult] = BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_Adult] = BuildData("Breath Weapon", damageHelper.BuildEntries("12d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Black_Adult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Black_Adult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Black_Adult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Black_MatureAdult] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Black_MatureAdult] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_MatureAdult] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_MatureAdult] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_MatureAdult] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Black_MatureAdult] = BuildData("Breath Weapon", damageHelper.BuildEntries("14d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Black_MatureAdult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Black_MatureAdult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Black_MatureAdult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Black_Old] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Black_Old] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_Old] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_Old] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_Old] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Black_Old] = BuildData("Breath Weapon", damageHelper.BuildEntries("16d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Black_Old] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Black_Old] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Black_Old] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Black_VeryOld] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Black_VeryOld] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_VeryOld] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_VeryOld] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_VeryOld] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Black_VeryOld] = BuildData("Breath Weapon", damageHelper.BuildEntries("18d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Black_VeryOld] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Black_VeryOld] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Black_VeryOld] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Black_Ancient] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Black_Ancient] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_Ancient] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_Ancient] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_Ancient] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Black_Ancient] = BuildData("Breath Weapon", damageHelper.BuildEntries("20d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Black_Ancient] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Black_Ancient] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Black_Ancient] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            attackDamages[CreatureConstants.Dragon_Black_Wyrm] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Black_Wyrm] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_Wyrm] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_Wyrm] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_Wyrm] = BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Black_Wyrm] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Black_Wyrm] = BuildData("Breath Weapon", damageHelper.BuildEntries("22d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Black_Wyrm] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Black_Wyrm] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Black_Wyrm] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            attackDamages[CreatureConstants.Dragon_Black_GreatWyrm] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Black_GreatWyrm] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_GreatWyrm] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_GreatWyrm] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Black_GreatWyrm] = BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Black_GreatWyrm] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Black_GreatWyrm] = BuildData("Breath Weapon", damageHelper.BuildEntries("24d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Black_GreatWyrm] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Black_GreatWyrm] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Black_GreatWyrm] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //small
            attackDamages[CreatureConstants.Dragon_Blue_Wyrmling] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Blue_Wyrmling] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_Wyrmling] = BuildData("Breath Weapon", damageHelper.BuildEntries("2d8", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //medium
            attackDamages[CreatureConstants.Dragon_Blue_VeryYoung] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Blue_VeryYoung] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_VeryYoung] = BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_VeryYoung] = BuildData("Breath Weapon", damageHelper.BuildEntries("4d8", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //medium
            attackDamages[CreatureConstants.Dragon_Blue_Young] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Blue_Young] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_Young] = BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_Young] = BuildData("Breath Weapon", damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //large
            attackDamages[CreatureConstants.Dragon_Blue_Juvenile] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Blue_Juvenile] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_Juvenile] = BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_Juvenile] = BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_Juvenile] = BuildData("Breath Weapon", damageHelper.BuildEntries("8d8", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Blue_Juvenile] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //large
            attackDamages[CreatureConstants.Dragon_Blue_YoungAdult] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Blue_YoungAdult] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_YoungAdult] = BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_YoungAdult] = BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_YoungAdult] = BuildData("Breath Weapon", damageHelper.BuildEntries("10d8", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Blue_YoungAdult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Blue_YoungAdult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Blue_Adult] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Blue_Adult] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_Adult] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_Adult] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_Adult] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Blue_Adult] = BuildData("Breath Weapon", damageHelper.BuildEntries("12d8", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Blue_Adult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Blue_Adult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Blue_Adult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Blue_MatureAdult] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Blue_MatureAdult] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_MatureAdult] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_MatureAdult] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_MatureAdult] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Blue_MatureAdult] = BuildData("Breath Weapon", damageHelper.BuildEntries("14d8", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Blue_MatureAdult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Blue_MatureAdult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Blue_MatureAdult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Blue_Old] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Blue_Old] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_Old] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_Old] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_Old] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Blue_Old] = BuildData("Breath Weapon", damageHelper.BuildEntries("16d8", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Blue_Old] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Blue_Old] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Blue_Old] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Blue_VeryOld] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Blue_VeryOld] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_VeryOld] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_VeryOld] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_VeryOld] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Blue_VeryOld] = BuildData("Breath Weapon", damageHelper.BuildEntries("18d8", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Blue_VeryOld] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Blue_VeryOld] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Blue_VeryOld] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            attackDamages[CreatureConstants.Dragon_Blue_Ancient] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Blue_Ancient] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_Ancient] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_Ancient] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_Ancient] = BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Blue_Ancient] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Blue_Ancient] = BuildData("Breath Weapon", damageHelper.BuildEntries("20d8", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Blue_Ancient] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Blue_Ancient] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Blue_Ancient] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            attackDamages[CreatureConstants.Dragon_Blue_Wyrm] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Blue_Wyrm] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_Wyrm] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_Wyrm] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_Wyrm] = BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Blue_Wyrm] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Blue_Wyrm] = BuildData("Breath Weapon", damageHelper.BuildEntries("22d8", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Blue_Wyrm] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Blue_Wyrm] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Blue_Wyrm] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            attackDamages[CreatureConstants.Dragon_Blue_GreatWyrm] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Blue_GreatWyrm] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_GreatWyrm] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_GreatWyrm] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Blue_GreatWyrm] = BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Blue_GreatWyrm] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Blue_GreatWyrm] = BuildData("Breath Weapon", damageHelper.BuildEntries("24d8", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Blue_GreatWyrm] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Blue_GreatWyrm] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Blue_GreatWyrm] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //small
            attackDamages[CreatureConstants.Dragon_Green_Wyrmling] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Green_Wyrmling] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_Wyrmling] = BuildData("Breath Weapon", damageHelper.BuildEntries("2d6", FeatConstants.Foci.Elements.Acid, "Gas"), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //medium
            attackDamages[CreatureConstants.Dragon_Green_VeryYoung] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Green_VeryYoung] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_VeryYoung] = BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_VeryYoung] = BuildData("Breath Weapon", damageHelper.BuildEntries("4d6", FeatConstants.Foci.Elements.Acid, "Gas"), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //medium
            attackDamages[CreatureConstants.Dragon_Green_Young] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Green_Young] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_Young] = BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_Young] = BuildData("Breath Weapon", damageHelper.BuildEntries("6d6", FeatConstants.Foci.Elements.Acid, "Gas"), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //large
            attackDamages[CreatureConstants.Dragon_Green_Juvenile] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Green_Juvenile] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_Juvenile] = BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_Juvenile] = BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_Juvenile] = BuildData("Breath Weapon", damageHelper.BuildEntries("8d6", FeatConstants.Foci.Elements.Acid, "Gas"), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Green_Juvenile] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //large
            attackDamages[CreatureConstants.Dragon_Green_YoungAdult] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Green_YoungAdult] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_YoungAdult] = BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_YoungAdult] = BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_YoungAdult] = BuildData("Breath Weapon", damageHelper.BuildEntries("10d6", FeatConstants.Foci.Elements.Acid, "Gas"), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Green_YoungAdult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Green_YoungAdult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Green_Adult] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Green_Adult] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_Adult] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_Adult] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_Adult] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Green_Adult] = BuildData("Breath Weapon", damageHelper.BuildEntries("12d6", FeatConstants.Foci.Elements.Acid, "Gas"), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Green_Adult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Green_Adult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Green_Adult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Green_MatureAdult] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Green_MatureAdult] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_MatureAdult] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_MatureAdult] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_MatureAdult] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Green_MatureAdult] = BuildData("Breath Weapon", damageHelper.BuildEntries("14d6", FeatConstants.Foci.Elements.Acid, "Gas"), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Green_MatureAdult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Green_MatureAdult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Green_MatureAdult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Green_Old] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Green_Old] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_Old] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_Old] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_Old] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Green_Old] = BuildData("Breath Weapon", damageHelper.BuildEntries("16d6", FeatConstants.Foci.Elements.Acid, "Gas"), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Green_Old] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Green_Old] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Green_Old] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Green_VeryOld] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Green_VeryOld] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_VeryOld] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_VeryOld] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_VeryOld] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Green_VeryOld] = BuildData("Breath Weapon", damageHelper.BuildEntries("18d6", FeatConstants.Foci.Elements.Acid, "Gas"), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Green_VeryOld] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Green_VeryOld] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Green_VeryOld] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            attackDamages[CreatureConstants.Dragon_Green_Ancient] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Green_Ancient] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_Ancient] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_Ancient] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_Ancient] = BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Green_Ancient] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Green_Ancient] = BuildData("Breath Weapon", damageHelper.BuildEntries("20d6", FeatConstants.Foci.Elements.Acid, "Gas"), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Green_Ancient] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Green_Ancient] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Green_Ancient] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            attackDamages[CreatureConstants.Dragon_Green_Wyrm] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Green_Wyrm] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_Wyrm] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_Wyrm] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_Wyrm] = BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Green_Wyrm] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Green_Wyrm] = BuildData("Breath Weapon", damageHelper.BuildEntries("22d6", FeatConstants.Foci.Elements.Acid, "Gas"), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Green_Wyrm] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Green_Wyrm] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Green_Wyrm] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            attackDamages[CreatureConstants.Dragon_Green_GreatWyrm] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Green_GreatWyrm] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_GreatWyrm] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_GreatWyrm] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Green_GreatWyrm] = BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Green_GreatWyrm] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Green_GreatWyrm] = BuildData("Breath Weapon", damageHelper.BuildEntries("24d6", FeatConstants.Foci.Elements.Acid, "Gas"), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Green_GreatWyrm] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Green_GreatWyrm] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Green_GreatWyrm] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //medium
            attackDamages[CreatureConstants.Dragon_Red_Wyrmling] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Red_Wyrmling] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_Wyrmling] = BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_Wyrmling] = BuildData("Breath Weapon", damageHelper.BuildEntries("2d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //large
            attackDamages[CreatureConstants.Dragon_Red_VeryYoung] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Red_VeryYoung] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_VeryYoung] = BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_VeryYoung] = BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_VeryYoung] = BuildData("Breath Weapon", damageHelper.BuildEntries("4d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //large
            attackDamages[CreatureConstants.Dragon_Red_Young] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Red_Young] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_Young] = BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_Young] = BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_Young] = BuildData("Breath Weapon", damageHelper.BuildEntries("6d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Red_Young] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //large
            attackDamages[CreatureConstants.Dragon_Red_Juvenile] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Red_Juvenile] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_Juvenile] = BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_Juvenile] = BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_Juvenile] = BuildData("Breath Weapon", damageHelper.BuildEntries("8d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Red_Juvenile] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Red_Juvenile] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //huge
            attackDamages[CreatureConstants.Dragon_Red_YoungAdult] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Red_YoungAdult] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_YoungAdult] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_YoungAdult] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_YoungAdult] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Red_YoungAdult] = BuildData("Breath Weapon", damageHelper.BuildEntries("10d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Red_YoungAdult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Red_YoungAdult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Red_YoungAdult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Red_Adult] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Red_Adult] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_Adult] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_Adult] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_Adult] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Red_Adult] = BuildData("Breath Weapon", damageHelper.BuildEntries("12d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Red_Adult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Red_Adult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Red_Adult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Red_MatureAdult] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Red_MatureAdult] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_MatureAdult] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_MatureAdult] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_MatureAdult] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Red_MatureAdult] = BuildData("Breath Weapon", damageHelper.BuildEntries("14d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Red_MatureAdult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Red_MatureAdult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Red_MatureAdult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            attackDamages[CreatureConstants.Dragon_Red_Old] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Red_Old] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_Old] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_Old] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_Old] = BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Red_Old] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Red_Old] = BuildData("Breath Weapon", damageHelper.BuildEntries("16d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Red_Old] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Red_Old] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Red_Old] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            attackDamages[CreatureConstants.Dragon_Red_VeryOld] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Red_VeryOld] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_VeryOld] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_VeryOld] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_VeryOld] = BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Red_VeryOld] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Red_VeryOld] = BuildData("Breath Weapon", damageHelper.BuildEntries("18d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Red_VeryOld] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Red_VeryOld] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Red_VeryOld] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            attackDamages[CreatureConstants.Dragon_Red_Ancient] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Red_Ancient] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_Ancient] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_Ancient] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_Ancient] = BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Red_Ancient] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Red_Ancient] = BuildData("Breath Weapon", damageHelper.BuildEntries("20d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Red_Ancient] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Red_Ancient] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Red_Ancient] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            attackDamages[CreatureConstants.Dragon_Red_Wyrm] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Red_Wyrm] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_Wyrm] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_Wyrm] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_Wyrm] = BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Red_Wyrm] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Red_Wyrm] = BuildData("Breath Weapon", damageHelper.BuildEntries("22d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Red_Wyrm] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Red_Wyrm] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Red_Wyrm] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //colossal
            attackDamages[CreatureConstants.Dragon_Red_GreatWyrm] = BuildData("Bite", damageHelper.BuildEntries("4d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Red_GreatWyrm] = BuildData("Claw", damageHelper.BuildEntries("4d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_GreatWyrm] = BuildData("Wing", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_GreatWyrm] = BuildData("Tail Slap", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Red_GreatWyrm] = BuildData("Crush", damageHelper.BuildEntries("4d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Red_GreatWyrm] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Red_GreatWyrm] = BuildData("Breath Weapon", damageHelper.BuildEntries("24d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Red_GreatWyrm] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Red_GreatWyrm] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Red_GreatWyrm] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //tiny
            attackDamages[CreatureConstants.Dragon_White_Wyrmling] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_White_Wyrmling] = BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_Wyrmling] = BuildData("Breath Weapon", damageHelper.BuildEntries("1d6", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //small
            attackDamages[CreatureConstants.Dragon_White_VeryYoung] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_White_VeryYoung] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_VeryYoung] = BuildData("Breath Weapon", damageHelper.BuildEntries("2d6", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //medium
            attackDamages[CreatureConstants.Dragon_White_Young] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_White_Young] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_Young] = BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_Young] = BuildData("Breath Weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            //medium
            attackDamages[CreatureConstants.Dragon_White_Juvenile] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_White_Juvenile] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_Juvenile] = BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_Juvenile] = BuildData("Breath Weapon", damageHelper.BuildEntries("4d6", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_White_Juvenile] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //large
            attackDamages[CreatureConstants.Dragon_White_YoungAdult] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_White_YoungAdult] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_YoungAdult] = BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_YoungAdult] = BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_YoungAdult] = BuildData("Breath Weapon", damageHelper.BuildEntries("5d6", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_White_YoungAdult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_White_YoungAdult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //large
            attackDamages[CreatureConstants.Dragon_White_Adult] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_White_Adult] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_Adult] = BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_Adult] = BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_Adult] = BuildData("Breath Weapon", damageHelper.BuildEntries("6d6", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_White_Adult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_White_Adult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_White_Adult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_White_MatureAdult] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_White_MatureAdult] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_MatureAdult] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_MatureAdult] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_MatureAdult] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_White_MatureAdult] = BuildData("Breath Weapon", damageHelper.BuildEntries("7d6", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_White_MatureAdult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_White_MatureAdult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_White_MatureAdult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_White_Old] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_White_Old] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_Old] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_Old] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_Old] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_White_Old] = BuildData("Breath Weapon", damageHelper.BuildEntries("8d6", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_White_Old] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_White_Old] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_White_Old] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_White_VeryOld] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_White_VeryOld] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_VeryOld] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_VeryOld] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_VeryOld] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_White_VeryOld] = BuildData("Breath Weapon", damageHelper.BuildEntries("9d6", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_White_VeryOld] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_White_VeryOld] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_White_VeryOld] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_White_Ancient] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_White_Ancient] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_Ancient] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_Ancient] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_Ancient] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_White_Ancient] = BuildData("Breath Weapon", damageHelper.BuildEntries("10d6", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_White_Ancient] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_White_Ancient] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_White_Ancient] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            attackDamages[CreatureConstants.Dragon_White_Wyrm] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_White_Wyrm] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_Wyrm] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_Wyrm] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_Wyrm] = BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_White_Wyrm] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_White_Wyrm] = BuildData("Breath Weapon", damageHelper.BuildEntries("11d6", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_White_Wyrm] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_White_Wyrm] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_White_Wyrm] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            attackDamages[CreatureConstants.Dragon_White_GreatWyrm] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_White_GreatWyrm] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_GreatWyrm] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_GreatWyrm] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_White_GreatWyrm] = BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_White_GreatWyrm] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_White_GreatWyrm] = BuildData("Breath Weapon", damageHelper.BuildEntries("12d6", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_White_GreatWyrm] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_White_GreatWyrm] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_White_GreatWyrm] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //tiny
            attackDamages[CreatureConstants.Dragon_Brass_Wyrmling] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Brass_Wyrmling] = BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_Wyrmling] = BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("1d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Brass_Wyrmling] = BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+1 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Dragon_Brass_Wyrmling] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //small
            attackDamages[CreatureConstants.Dragon_Brass_VeryYoung] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Brass_VeryYoung] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_VeryYoung] = BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("2d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Brass_VeryYoung] = BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+2 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Dragon_Brass_VeryYoung] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //medium
            attackDamages[CreatureConstants.Dragon_Brass_Young] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Brass_Young] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_Young] = BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_Young] = BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Brass_Young] = BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+3 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Dragon_Brass_Young] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Brass_Young] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //medium
            attackDamages[CreatureConstants.Dragon_Brass_Juvenile] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Brass_Juvenile] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_Juvenile] = BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_Juvenile] = BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("4d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Brass_Juvenile] = BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+4 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Dragon_Brass_Juvenile] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Brass_Juvenile] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //large
            attackDamages[CreatureConstants.Dragon_Brass_YoungAdult] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Brass_YoungAdult] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_YoungAdult] = BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_YoungAdult] = BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_YoungAdult] = BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("5d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Brass_YoungAdult] = BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+5 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Dragon_Brass_YoungAdult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Brass_YoungAdult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Brass_YoungAdult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //large
            attackDamages[CreatureConstants.Dragon_Brass_Adult] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Brass_Adult] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_Adult] = BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_Adult] = BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_Adult] = BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("6d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Brass_Adult] = BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+6 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Dragon_Brass_Adult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Brass_Adult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Brass_Adult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Brass_MatureAdult] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Brass_MatureAdult] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_MatureAdult] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_MatureAdult] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_MatureAdult] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Brass_MatureAdult] = BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("7d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Brass_MatureAdult] = BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+7 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Dragon_Brass_MatureAdult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Brass_MatureAdult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Brass_MatureAdult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Brass_Old] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Brass_Old] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_Old] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_Old] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_Old] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Brass_Old] = BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("8d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Brass_Old] = BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+8 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Dragon_Brass_Old] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Brass_Old] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Brass_Old] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Brass_VeryOld] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Brass_VeryOld] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_VeryOld] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_VeryOld] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_VeryOld] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Brass_VeryOld] = BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("9d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Brass_VeryOld] = BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+9 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Dragon_Brass_VeryOld] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Brass_VeryOld] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Brass_VeryOld] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Brass_Ancient] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Brass_Ancient] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_Ancient] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_Ancient] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_Ancient] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Brass_Ancient] = BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("10d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Brass_Ancient] = BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+10 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Dragon_Brass_Ancient] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Brass_Ancient] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Brass_Ancient] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            attackDamages[CreatureConstants.Dragon_Brass_Wyrm] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Brass_Wyrm] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_Wyrm] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_Wyrm] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_Wyrm] = BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Brass_Wyrm] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Brass_Wyrm] = BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("11d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Brass_Wyrm] = BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+11 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Dragon_Brass_Wyrm] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Brass_Wyrm] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Brass_Wyrm] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            attackDamages[CreatureConstants.Dragon_Brass_GreatWyrm] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Brass_GreatWyrm] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_GreatWyrm] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_GreatWyrm] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Brass_GreatWyrm] = BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Brass_GreatWyrm] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Brass_GreatWyrm] = BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("12d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Brass_GreatWyrm] = BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+12 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Dragon_Brass_GreatWyrm] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Brass_GreatWyrm] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Brass_GreatWyrm] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //small
            attackDamages[CreatureConstants.Dragon_Bronze_Wyrmling] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Bronze_Wyrmling] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_Wyrmling] = BuildData("Breath Weapon (electricity)", damageHelper.BuildEntries("2d6", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Bronze_Wyrmling] = BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+1 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Dragon_Bronze_Wyrmling] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //medium
            attackDamages[CreatureConstants.Dragon_Bronze_VeryYoung] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Bronze_VeryYoung] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_VeryYoung] = BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_VeryYoung] = BuildData("Breath Weapon (electricity)", damageHelper.BuildEntries("4d6", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Bronze_VeryYoung] = BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+2 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Dragon_Bronze_VeryYoung] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //medium
            attackDamages[CreatureConstants.Dragon_Bronze_Young] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Bronze_Young] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_Young] = BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_Young] = BuildData("Breath Weapon (electricity)", damageHelper.BuildEntries("6d6", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Bronze_Young] = BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+3 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Dragon_Bronze_Young] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Bronze_Young] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //large
            attackDamages[CreatureConstants.Dragon_Bronze_Juvenile] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Bronze_Juvenile] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_Juvenile] = BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_Juvenile] = BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_Juvenile] = BuildData("Breath Weapon (electricity)", damageHelper.BuildEntries("8d6", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Bronze_Juvenile] = BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+4 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Dragon_Bronze_Juvenile] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Bronze_Juvenile] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //large
            attackDamages[CreatureConstants.Dragon_Bronze_YoungAdult] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Bronze_YoungAdult] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_YoungAdult] = BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_YoungAdult] = BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_YoungAdult] = BuildData("Breath Weapon (electricity)", damageHelper.BuildEntries("10d6", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Bronze_YoungAdult] = BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+5 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Dragon_Bronze_YoungAdult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Bronze_YoungAdult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Bronze_YoungAdult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Bronze_Adult] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Bronze_Adult] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_Adult] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_Adult] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_Adult] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Bronze_Adult] = BuildData("Breath Weapon (electricity)", damageHelper.BuildEntries("12d6", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Bronze_Adult] = BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+6 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Dragon_Bronze_Adult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Bronze_Adult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Bronze_Adult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Bronze_MatureAdult] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Bronze_MatureAdult] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_MatureAdult] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_MatureAdult] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_MatureAdult] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Bronze_MatureAdult] = BuildData("Breath Weapon (electricity)", damageHelper.BuildEntries("14d6", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Bronze_MatureAdult] = BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+7 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Dragon_Bronze_MatureAdult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Bronze_MatureAdult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Bronze_MatureAdult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Bronze_Old] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Bronze_Old] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_Old] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_Old] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_Old] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Bronze_Old] = BuildData("Breath Weapon (electricity)", damageHelper.BuildEntries("16d6", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Bronze_Old] = BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+8 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Dragon_Bronze_Old] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Bronze_Old] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Bronze_Old] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Bronze_VeryOld] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Bronze_VeryOld] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_VeryOld] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_VeryOld] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_VeryOld] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Bronze_VeryOld] = BuildData("Breath Weapon (electricity)", damageHelper.BuildEntries("18d6", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Bronze_VeryOld] = BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+9 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Dragon_Bronze_VeryOld] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Bronze_VeryOld] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Bronze_VeryOld] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            attackDamages[CreatureConstants.Dragon_Bronze_Ancient] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Bronze_Ancient] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_Ancient] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_Ancient] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_Ancient] = BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Bronze_Ancient] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Bronze_Ancient] = BuildData("Breath Weapon (electricity)", damageHelper.BuildEntries("20d6", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Bronze_Ancient] = BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+10 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Dragon_Bronze_Ancient] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Bronze_Ancient] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Bronze_Ancient] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            attackDamages[CreatureConstants.Dragon_Bronze_Wyrm] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Bronze_Wyrm] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_Wyrm] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_Wyrm] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_Wyrm] = BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Bronze_Wyrm] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Bronze_Wyrm] = BuildData("Breath Weapon (electricity)", damageHelper.BuildEntries("22d6", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Bronze_Wyrm] = BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+11 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Dragon_Bronze_Wyrm] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Bronze_Wyrm] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Bronze_Wyrm] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            attackDamages[CreatureConstants.Dragon_Bronze_GreatWyrm] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Bronze_GreatWyrm] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_GreatWyrm] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_GreatWyrm] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Bronze_GreatWyrm] = BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Bronze_GreatWyrm] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Bronze_GreatWyrm] = BuildData("Breath Weapon (electricity)", damageHelper.BuildEntries("24d6", FeatConstants.Foci.Elements.Electricity), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Bronze_GreatWyrm] = BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+12 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Dragon_Bronze_GreatWyrm] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Bronze_GreatWyrm] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Bronze_GreatWyrm] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //tiny
            attackDamages[CreatureConstants.Dragon_Copper_Wyrmling] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Copper_Wyrmling] = BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_Wyrmling] = BuildData("Breath Weapon (acid)", damageHelper.BuildEntries("2d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Copper_Wyrmling] = BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+1 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            //small
            attackDamages[CreatureConstants.Dragon_Copper_VeryYoung] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Copper_VeryYoung] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_VeryYoung] = BuildData("Breath Weapon (acid)", damageHelper.BuildEntries("4d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Copper_VeryYoung] = BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+2 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            //medium
            attackDamages[CreatureConstants.Dragon_Copper_Young] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Copper_Young] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_Young] = BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_Young] = BuildData("Breath Weapon (acid)", damageHelper.BuildEntries("6d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Copper_Young] = BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+3 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Copper_Young] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //medium
            attackDamages[CreatureConstants.Dragon_Copper_Juvenile] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Copper_Juvenile] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_Juvenile] = BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_Juvenile] = BuildData("Breath Weapon (acid)", damageHelper.BuildEntries("8d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Copper_Juvenile] = BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+4 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Copper_Juvenile] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //large
            attackDamages[CreatureConstants.Dragon_Copper_YoungAdult] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Copper_YoungAdult] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_YoungAdult] = BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_YoungAdult] = BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_YoungAdult] = BuildData("Breath Weapon (acid)", damageHelper.BuildEntries("10d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Copper_YoungAdult] = BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+5 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Copper_YoungAdult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //large
            attackDamages[CreatureConstants.Dragon_Copper_Adult] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Copper_Adult] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_Adult] = BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_Adult] = BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_Adult] = BuildData("Breath Weapon (acid)", damageHelper.BuildEntries("12d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Copper_Adult] = BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+6 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Copper_Adult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Copper_Adult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Copper_Adult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Copper_MatureAdult] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Copper_MatureAdult] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_MatureAdult] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_MatureAdult] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_MatureAdult] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Copper_MatureAdult] = BuildData("Breath Weapon (acid)", damageHelper.BuildEntries("14d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Copper_MatureAdult] = BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+7 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Copper_MatureAdult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Copper_MatureAdult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Copper_MatureAdult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Copper_Old] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Copper_Old] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_Old] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_Old] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_Old] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Copper_Old] = BuildData("Breath Weapon (acid)", damageHelper.BuildEntries("16d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Copper_Old] = BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+8 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Copper_Old] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Copper_Old] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Copper_Old] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Copper_VeryOld] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Copper_VeryOld] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_VeryOld] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_VeryOld] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_VeryOld] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Copper_VeryOld] = BuildData("Breath Weapon (acid)", damageHelper.BuildEntries("18d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Copper_VeryOld] = BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+9 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Copper_VeryOld] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Copper_VeryOld] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Copper_VeryOld] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Copper_Ancient] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Copper_Ancient] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_Ancient] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_Ancient] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_Ancient] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Copper_Ancient] = BuildData("Breath Weapon (acid)", damageHelper.BuildEntries("20d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Copper_Ancient] = BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+10 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Copper_Ancient] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Copper_Ancient] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Copper_Ancient] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            attackDamages[CreatureConstants.Dragon_Copper_Wyrm] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Copper_Wyrm] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_Wyrm] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_Wyrm] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_Wyrm] = BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Copper_Wyrm] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Copper_Wyrm] = BuildData("Breath Weapon (acid)", damageHelper.BuildEntries("22d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Copper_Wyrm] = BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+11 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Copper_Wyrm] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Copper_Wyrm] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Copper_Wyrm] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            attackDamages[CreatureConstants.Dragon_Copper_GreatWyrm] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Copper_GreatWyrm] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_GreatWyrm] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_GreatWyrm] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Copper_GreatWyrm] = BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Copper_GreatWyrm] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Copper_GreatWyrm] = BuildData("Breath Weapon (acid)", damageHelper.BuildEntries("24d4", FeatConstants.Foci.Elements.Acid), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Copper_GreatWyrm] = BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+12 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Copper_GreatWyrm] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Copper_GreatWyrm] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Copper_GreatWyrm] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //medium
            attackDamages[CreatureConstants.Dragon_Gold_Wyrmling] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Gold_Wyrmling] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_Wyrmling] = BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_Wyrmling] = BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("2d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Gold_Wyrmling] = BuildData("Breath Weapon (weakening gas)", damageHelper.BuildEntries("1", AbilityConstants.Strength), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            //large
            attackDamages[CreatureConstants.Dragon_Gold_VeryYoung] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Gold_VeryYoung] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_VeryYoung] = BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_VeryYoung] = BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_VeryYoung] = BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("4d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Gold_VeryYoung] = BuildData("Breath Weapon (weakening gas)", damageHelper.BuildEntries("2", AbilityConstants.Strength), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            //large
            attackDamages[CreatureConstants.Dragon_Gold_Young] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Gold_Young] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_Young] = BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_Young] = BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_Young] = BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("6d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Gold_Young] = BuildData("Breath Weapon (weakening gas)", damageHelper.BuildEntries("3", AbilityConstants.Strength), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Gold_Young] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //large
            attackDamages[CreatureConstants.Dragon_Gold_Juvenile] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Gold_Juvenile] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_Juvenile] = BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_Juvenile] = BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_Juvenile] = BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("8d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Gold_Juvenile] = BuildData("Breath Weapon (weakening gas)", damageHelper.BuildEntries("4", AbilityConstants.Strength), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Gold_Juvenile] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Gold_Juvenile] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //huge
            attackDamages[CreatureConstants.Dragon_Gold_YoungAdult] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Gold_YoungAdult] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_YoungAdult] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_YoungAdult] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_YoungAdult] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Gold_YoungAdult] = BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("10d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Gold_YoungAdult] = BuildData("Breath Weapon (weakening gas)", damageHelper.BuildEntries("5", AbilityConstants.Strength), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Gold_YoungAdult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Gold_YoungAdult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Gold_YoungAdult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Gold_Adult] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Gold_Adult] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_Adult] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_Adult] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_Adult] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Gold_Adult] = BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("12d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Gold_Adult] = BuildData("Breath Weapon (weakening gas)", damageHelper.BuildEntries("6", AbilityConstants.Strength), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Gold_Adult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Gold_Adult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Gold_Adult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Gold_MatureAdult] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Gold_MatureAdult] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_MatureAdult] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_MatureAdult] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_MatureAdult] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Gold_MatureAdult] = BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("14d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Gold_MatureAdult] = BuildData("Breath Weapon (weakening gas)", damageHelper.BuildEntries("7", AbilityConstants.Strength), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Gold_MatureAdult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Gold_MatureAdult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Gold_MatureAdult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            attackDamages[CreatureConstants.Dragon_Gold_Old] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Gold_Old] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_Old] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_Old] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_Old] = BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Gold_Old] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Gold_Old] = BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("16d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Gold_Old] = BuildData("Breath Weapon (weakening gas)", damageHelper.BuildEntries("8", AbilityConstants.Strength), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Gold_Old] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Gold_Old] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Gold_Old] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            attackDamages[CreatureConstants.Dragon_Gold_VeryOld] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Gold_VeryOld] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_VeryOld] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_VeryOld] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_VeryOld] = BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Gold_VeryOld] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Gold_VeryOld] = BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("18d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Gold_VeryOld] = BuildData("Breath Weapon (weakening gas)", damageHelper.BuildEntries("9", AbilityConstants.Strength), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Gold_VeryOld] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Gold_VeryOld] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Gold_VeryOld] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            attackDamages[CreatureConstants.Dragon_Gold_Ancient] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Gold_Ancient] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_Ancient] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_Ancient] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_Ancient] = BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Gold_Ancient] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Gold_Ancient] = BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("20d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Gold_Ancient] = BuildData("Breath Weapon (weakening gas)", damageHelper.BuildEntries("10", AbilityConstants.Strength), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Gold_Ancient] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Gold_Ancient] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Gold_Ancient] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //colossal
            attackDamages[CreatureConstants.Dragon_Gold_Wyrm] = BuildData("Bite", damageHelper.BuildEntries("4d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Gold_Wyrm] = BuildData("Claw", damageHelper.BuildEntries("4d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_Wyrm] = BuildData("Wing", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_Wyrm] = BuildData("Tail Slap", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_Wyrm] = BuildData("Crush", damageHelper.BuildEntries("4d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Gold_Wyrm] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Gold_Wyrm] = BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("22d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Gold_Wyrm] = BuildData("Breath Weapon (weakening gas)", damageHelper.BuildEntries("11", AbilityConstants.Strength), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Gold_Wyrm] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Gold_Wyrm] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Gold_Wyrm] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //colossal
            attackDamages[CreatureConstants.Dragon_Gold_GreatWyrm] = BuildData("Bite", damageHelper.BuildEntries("4d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Gold_GreatWyrm] = BuildData("Claw", damageHelper.BuildEntries("4d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_GreatWyrm] = BuildData("Wing", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_GreatWyrm] = BuildData("Tail Slap", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Gold_GreatWyrm] = BuildData("Crush", damageHelper.BuildEntries("4d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Gold_GreatWyrm] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Gold_GreatWyrm] = BuildData("Breath Weapon (fire)", damageHelper.BuildEntries("24d10", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Gold_GreatWyrm] = BuildData("Breath Weapon (weakening gas)", damageHelper.BuildEntries("12", AbilityConstants.Strength), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Gold_GreatWyrm] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Gold_GreatWyrm] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Gold_GreatWyrm] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //small
            attackDamages[CreatureConstants.Dragon_Silver_Wyrmling] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Silver_Wyrmling] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_Wyrmling] = BuildData("Breath Weapon (cold)", damageHelper.BuildEntries("2d8", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Silver_Wyrmling] = BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+1 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            //medium
            attackDamages[CreatureConstants.Dragon_Silver_VeryYoung] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Silver_VeryYoung] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_VeryYoung] = BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_VeryYoung] = BuildData("Breath Weapon (cold)", damageHelper.BuildEntries("4d8", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Silver_VeryYoung] = BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+2 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            //medium
            attackDamages[CreatureConstants.Dragon_Silver_Young] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Silver_Young] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_Young] = BuildData("Wing", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_Young] = BuildData("Breath Weapon (cold)", damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Silver_Young] = BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+3 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Silver_Young] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //large
            attackDamages[CreatureConstants.Dragon_Silver_Juvenile] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Silver_Juvenile] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_Juvenile] = BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_Juvenile] = BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_Juvenile] = BuildData("Breath Weapon (cold)", damageHelper.BuildEntries("8d8", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Silver_Juvenile] = BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+4 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Silver_Juvenile] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Silver_Juvenile] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            //large
            attackDamages[CreatureConstants.Dragon_Silver_YoungAdult] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Silver_YoungAdult] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_YoungAdult] = BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_YoungAdult] = BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_YoungAdult] = BuildData("Breath Weapon (cold)", damageHelper.BuildEntries("10d8", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Silver_YoungAdult] = BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+5 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Silver_YoungAdult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Silver_YoungAdult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Silver_YoungAdult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Silver_Adult] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Silver_Adult] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_Adult] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_Adult] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_Adult] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Silver_Adult] = BuildData("Breath Weapon (cold)", damageHelper.BuildEntries("12d8", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Silver_Adult] = BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+6 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Silver_Adult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Silver_Adult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Silver_Adult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Silver_MatureAdult] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Silver_MatureAdult] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_MatureAdult] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_MatureAdult] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_MatureAdult] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Silver_MatureAdult] = BuildData("Breath Weapon (cold)", damageHelper.BuildEntries("14d8", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Silver_MatureAdult] = BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+7 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Silver_MatureAdult] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Silver_MatureAdult] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Silver_MatureAdult] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Silver_Old] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Silver_Old] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_Old] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_Old] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_Old] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Silver_Old] = BuildData("Breath Weapon (cold)", damageHelper.BuildEntries("16d8", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Silver_Old] = BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+8 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Silver_Old] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Silver_Old] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Silver_Old] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //huge
            attackDamages[CreatureConstants.Dragon_Silver_VeryOld] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Silver_VeryOld] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_VeryOld] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_VeryOld] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_VeryOld] = BuildData("Crush", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Silver_VeryOld] = BuildData("Breath Weapon (cold)", damageHelper.BuildEntries("18d8", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Silver_VeryOld] = BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+9 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Silver_VeryOld] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Silver_VeryOld] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Silver_VeryOld] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            attackDamages[CreatureConstants.Dragon_Silver_Ancient] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Silver_Ancient] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_Ancient] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_Ancient] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_Ancient] = BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Silver_Ancient] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Silver_Ancient] = BuildData("Breath Weapon (cold)", damageHelper.BuildEntries("20d8", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Silver_Ancient] = BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+10 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Silver_Ancient] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Silver_Ancient] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Silver_Ancient] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //gargantuan
            attackDamages[CreatureConstants.Dragon_Silver_Wyrm] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Silver_Wyrm] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_Wyrm] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_Wyrm] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_Wyrm] = BuildData("Crush", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Silver_Wyrm] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Silver_Wyrm] = BuildData("Breath Weapon (cold)", damageHelper.BuildEntries("22d8", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Silver_Wyrm] = BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+11 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Silver_Wyrm] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Silver_Wyrm] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Silver_Wyrm] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            //colossal
            attackDamages[CreatureConstants.Dragon_Silver_GreatWyrm] = BuildData("Bite", damageHelper.BuildEntries("4d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragon_Silver_GreatWyrm] = BuildData("Claw", damageHelper.BuildEntries("4d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_GreatWyrm] = BuildData("Wing", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_GreatWyrm] = BuildData("Tail Slap", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragon_Silver_GreatWyrm] = BuildData("Crush", damageHelper.BuildEntries("4d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Silver_GreatWyrm] = BuildData("Tail Sweep", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Silver_GreatWyrm] = BuildData("Breath Weapon (cold)", damageHelper.BuildEntries("24d8", FeatConstants.Foci.Elements.Cold), string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Dragon_Silver_GreatWyrm] = BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+12 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Dragon_Silver_GreatWyrm] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Silver_GreatWyrm] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Dragon_Silver_GreatWyrm] = BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            attackDamages[CreatureConstants.Dragonne] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dragonne] = BuildData("Claw", damageHelper.BuildEntries("2d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dragonne] = BuildData("Pounce", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            attackDamages[CreatureConstants.Dragonne] = BuildData("Roar", string.Empty, string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));

            attackDamages[CreatureConstants.Dretch] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dretch] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Dretch] = BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
            attackDamages[CreatureConstants.Dretch] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Drider] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Drider] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Drider] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Drider] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Drider] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Drider] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Strength, "Initial",
                    "1d6", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Dryad] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Dryad] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Dryad] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dryad] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Dwarf_Deep] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Dwarf_Deep] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Dwarf_Deep] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Dwarf_Duergar] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Dwarf_Duergar] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Dwarf_Duergar] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Dwarf_Duergar] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Dwarf_Hill] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Dwarf_Hill] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Dwarf_Hill] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Dwarf_Mountain] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Dwarf_Mountain] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Dwarf_Mountain] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Eagle] = BuildData("Talons", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Eagle] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            attackDamages[CreatureConstants.Eagle_Giant] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Eagle_Giant] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            attackDamages[CreatureConstants.Efreeti] = BuildData("Slam", damageHelper.BuildEntries("1d8", slapSlamDamageType), "Heat", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Efreeti] = BuildData("Change Size", string.Empty, string.Empty, 0, "spell-like ability", 2, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Efreeti] = BuildData("Heat",
                damageHelper.BuildEntries("1d6", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, false, true, true, true));
            attackDamages[CreatureConstants.Efreeti] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Elasmosaurus] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Elemental_Air_Small] = BuildData("Slam",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elemental_Air_Small] = BuildData("Air mastery",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Elemental_Air_Small] = BuildData("Whirlwind",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Elemental_Air_Medium] = BuildData("Slam",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elemental_Air_Medium] = BuildData("Air mastery",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Elemental_Air_Medium] = BuildData("Whirlwind",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Elemental_Air_Large] = BuildData("Slam",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elemental_Air_Large] = BuildData("Air mastery",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Elemental_Air_Large] = BuildData("Whirlwind",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Elemental_Air_Huge] = BuildData("Slam",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elemental_Air_Huge] = BuildData("Air mastery",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Elemental_Air_Huge] = BuildData("Whirlwind",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Elemental_Air_Greater] = BuildData("Slam",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elemental_Air_Greater] = BuildData("Air mastery",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Elemental_Air_Greater] = BuildData("Whirlwind",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Elemental_Air_Elder] = BuildData("Slam",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elemental_Air_Elder] = BuildData("Air mastery",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Elemental_Air_Elder] = BuildData("Whirlwind",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Elemental_Earth_Small] = BuildData("Slam",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elemental_Earth_Small] = BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Elemental_Earth_Small] = BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Elemental_Earth_Medium] = BuildData("Slam",
                damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elemental_Earth_Medium] = BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Elemental_Earth_Medium] = BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Elemental_Earth_Large] = BuildData("Slam",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elemental_Earth_Large] = BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Elemental_Earth_Large] = BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Elemental_Earth_Huge] = BuildData("Slam",
                damageHelper.BuildEntries("2d10", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elemental_Earth_Huge] = BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Elemental_Earth_Huge] = BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Elemental_Earth_Greater] = BuildData("Slam",
                damageHelper.BuildEntries("2d10", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elemental_Earth_Greater] = BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Elemental_Earth_Greater] = BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Elemental_Earth_Elder] = BuildData("Slam",
                damageHelper.BuildEntries("2d10", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elemental_Earth_Elder] = BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Elemental_Earth_Elder] = BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Elemental_Fire_Small] = BuildData("Slam",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                "Burn", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elemental_Fire_Small] = BuildData("Burn",
                damageHelper.BuildEntries("1d4", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Elemental_Fire_Medium] = BuildData("Slam",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                "Burn", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elemental_Fire_Medium] = BuildData("Burn",
                damageHelper.BuildEntries("1d6", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Elemental_Fire_Large] = BuildData("Slam",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                "Burn", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elemental_Fire_Large] = BuildData("Burn",
                damageHelper.BuildEntries("2d6", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Elemental_Fire_Huge] = BuildData("Slam",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                "Burn", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elemental_Fire_Huge] = BuildData("Burn",
                damageHelper.BuildEntries("2d8", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Elemental_Fire_Greater] = BuildData("Slam",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                "Burn", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elemental_Fire_Greater] = BuildData("Burn",
                damageHelper.BuildEntries("2d8", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Elemental_Fire_Elder] = BuildData("Slam",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                "Burn", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elemental_Fire_Elder] = BuildData("Burn",
                damageHelper.BuildEntries("2d8", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Elemental_Water_Small] = BuildData("Slam",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elemental_Water_Small] = BuildData("Water mastery",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Elemental_Water_Small] = BuildData("Drench",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Elemental_Water_Small] = BuildData("Vortex",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Elemental_Water_Medium] = BuildData("Slam",
                damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elemental_Water_Medium] = BuildData("Water mastery",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Elemental_Water_Medium] = BuildData("Drench",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Elemental_Water_Medium] = BuildData("Vortex",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Elemental_Water_Large] = BuildData("Slam",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elemental_Water_Large] = BuildData("Water mastery",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Elemental_Water_Large] = BuildData("Drench",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Elemental_Water_Large] = BuildData("Vortex",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Elemental_Water_Huge] = BuildData("Slam",
                damageHelper.BuildEntries("2d10", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elemental_Water_Huge] = BuildData("Water mastery",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Elemental_Water_Huge] = BuildData("Drench",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Elemental_Water_Huge] = BuildData("Vortex",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Elemental_Water_Greater] = BuildData("Slam",
                damageHelper.BuildEntries("2d10", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elemental_Water_Greater] = BuildData("Water mastery",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Elemental_Water_Greater] = BuildData("Drench",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Elemental_Water_Greater] = BuildData("Vortex",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Elemental_Water_Elder] = BuildData("Slam",
                damageHelper.BuildEntries("2d10", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elemental_Water_Elder] = BuildData("Water mastery",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Elemental_Water_Elder] = BuildData("Drench",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Elemental_Water_Elder] = BuildData("Vortex",
                damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Elephant] = BuildData("Slam", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elephant] = BuildData("Stamp", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Elephant] = BuildData("Gore", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elephant] = BuildData("Trample", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Elf_Aquatic] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Elf_Aquatic] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Elf_Aquatic] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Elf_Drow] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Elf_Drow] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Elf_Drow] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Elf_Drow] = BuildData("Poison",
                string.Empty,
                "1 minute unconscious (Initial), 2d4 hours unconscious (Secondary)", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, false, false, true, save: SaveConstants.Fortitude, saveDcBonus: 3));
            attackDamages[CreatureConstants.Elf_Drow] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Elf_Gray] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Elf_Gray] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Elf_Gray] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Elf_Half] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Elf_Half] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Elf_Half] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Elf_High] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Elf_High] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Elf_High] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Elf_Wild] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Elf_Wild] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Elf_Wild] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Elf_Wood] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Elf_Wood] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Elf_Wood] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Erinyes] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Erinyes] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Erinyes] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Erinyes] = BuildData("Rope", string.Empty, "Entangle", 0, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Erinyes] = BuildData("Entangle", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Erinyes] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Erinyes] = BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            attackDamages[CreatureConstants.EtherealFilcher] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.EtherealFilcher] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.EtherealMarauder] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.EtherealMarauder] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Ettercap] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Ettercap] = BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Ettercap] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Dexterity, "Initial",
                    "2d6", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 2));
            attackDamages[CreatureConstants.Ettercap] = BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, true, true, false, true, saveAbility: AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Ettin] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 2, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Ettin] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 2, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Ettin] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.FireBeetle_Giant] = BuildData("Bite", damageHelper.BuildEntries("2d4", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.FormianWorker] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.FormianWorker] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.FormianWarrior] = BuildData("Sting", damageHelper.BuildEntries("2d4", stingDamageType), "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.FormianWarrior] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.FormianWarrior] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.FormianWarrior] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Strength, "Initial",
                    "1d6", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.FormianTaskmaster] = BuildData("Sting", damageHelper.BuildEntries("2d4", stingDamageType), "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.FormianTaskmaster] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.FormianTaskmaster] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Strength, "Initial",
                    "1d6", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.FormianTaskmaster] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.FormianTaskmaster] = BuildData("Dominated creature", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.FormianMyrmarch] = BuildData("Sting",
                damageHelper.BuildEntries("2d4", stingDamageType),
                "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.FormianMyrmarch] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.FormianMyrmarch] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.FormianMyrmarch] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Strength, "Initial",
                    "1d6", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.FormianMyrmarch] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.FormianQueen] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.FormianQueen] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.FrostWorm] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), "Cold", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.FrostWorm] = BuildData("Trill", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
            attackDamages[CreatureConstants.FrostWorm] = BuildData("Cold",
                damageHelper.BuildEntries("1d8", FeatConstants.Foci.Elements.Cold),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            attackDamages[CreatureConstants.FrostWorm] = BuildData("Breath weapon",
                damageHelper.BuildEntries("15d6", FeatConstants.Foci.Elements.Cold),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Gargoyle] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Gargoyle] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Gargoyle] = BuildData("Gore", damageHelper.BuildEntries("1d6", goreDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            attackDamages[CreatureConstants.Gargoyle_Kapoacinth] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Gargoyle_Kapoacinth] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Gargoyle_Kapoacinth] = BuildData("Gore", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            attackDamages[CreatureConstants.GelatinousCube] = BuildData("Slam",
                damageHelper.BuildEntries(
                    "1d6", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "1d6", FeatConstants.Foci.Elements.Acid),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.GelatinousCube] = BuildData("Acid", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            attackDamages[CreatureConstants.GelatinousCube] = BuildData("Engulf", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, SaveConstants.Reflex, AbilityConstants.Strength, 1));
            attackDamages[CreatureConstants.GelatinousCube] = BuildData("Paralysis", string.Empty, "3d6 rounds of paralysis", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Ghaele] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Ghaele] = BuildData("Light Ray",
                damageHelper.BuildEntries("2d12"),
                string.Empty, 0, "ranged touch", 2, FeatConstants.Frequencies.Round, false, true, true, false));
            attackDamages[CreatureConstants.Ghaele] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Ghaele] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Ghaele] = BuildData("Gaze", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            attackDamages[CreatureConstants.Ghoul] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), "Disease, Paralysis", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Ghoul] = BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), "Paralysis", 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Ghoul] = BuildData("Disease", string.Empty, "Ghoul Fever", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            attackDamages[CreatureConstants.Ghoul] = BuildData("Ghoul Fever",
                damageHelper.BuildEntries(
                    "1d3", AbilityConstants.Constitution, "Incubation period 1 day",
                    "1d3", AbilityConstants.Dexterity, "Incubation period 1 day"),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Ghoul] = BuildData("Paralysis", string.Empty, "1d4+1 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Ghoul_Ghast] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), "Disease, Paralysis", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Ghoul_Ghast] = BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), "Paralysis", 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Ghoul_Ghast] = BuildData("Disease", string.Empty, "Ghoul Fever", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            attackDamages[CreatureConstants.Ghoul_Ghast] = BuildData("Ghoul Fever",
                damageHelper.BuildEntries(
                    "1d3", AbilityConstants.Constitution, "Incubation period 1 day",
                    "1d3", AbilityConstants.Dexterity, "Incubation period 1 day"),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Ghoul_Ghast] = BuildData("Paralysis", string.Empty, "1d4+1 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Ghoul_Ghast] = BuildData("Stench", string.Empty, "1d6+4 rounds sickened", 0, "melee", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Ghoul_Lacedon] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), "Disease, Paralysis", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Ghoul_Lacedon] = BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), "Paralysis", 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Ghoul_Lacedon] = BuildData("Disease", string.Empty, "Ghoul Fever", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            attackDamages[CreatureConstants.Ghoul_Lacedon] = BuildData("Ghoul Fever",
                damageHelper.BuildEntries(
                    "1d3", AbilityConstants.Constitution, "Incubation period 1 day",
                    "1d3", AbilityConstants.Dexterity, "Incubation period 1 day"),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Ghoul_Lacedon] = BuildData("Paralysis", string.Empty, "1d4+1 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Giant_Cloud] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Giant_Cloud] = BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Giant_Cloud] = BuildData("Rock", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
            attackDamages[CreatureConstants.Giant_Cloud] = BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Giant_Cloud] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Giant_Fire] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Giant_Fire] = BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Giant_Fire] = BuildData("Rock", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
            attackDamages[CreatureConstants.Giant_Fire] = BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Giant_Frost] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Giant_Frost] = BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Giant_Frost] = BuildData("Rock", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
            attackDamages[CreatureConstants.Giant_Frost] = BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Giant_Hill] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Giant_Hill] = BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Giant_Hill] = BuildData("Rock", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
            attackDamages[CreatureConstants.Giant_Hill] = BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Giant_Stone] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Giant_Stone] = BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Giant_Stone] = BuildData("Rock", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
            attackDamages[CreatureConstants.Giant_Stone] = BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Giant_Stone_Elder] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Giant_Stone_Elder] = BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Giant_Stone_Elder] = BuildData("Rock", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
            attackDamages[CreatureConstants.Giant_Stone_Elder] = BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Giant_Stone_Elder] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Giant_Storm] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Giant_Storm] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Giant_Storm] = BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Giant_Storm] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.GibberingMouther] = BuildData("Bite",
                damageHelper.BuildEntries("1", biteDamageType),
                string.Empty, 1, "melee", 6, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.GibberingMouther] = BuildData("Spittle",
                damageHelper.BuildEntries("1d4", FeatConstants.Foci.Elements.Acid),
                "Blindness", 0, "ranged touch", 1, FeatConstants.Frequencies.Round, false, true, true, false));
            attackDamages[CreatureConstants.GibberingMouther] = BuildData("Blindness",
                string.Empty,
                "1d4 rounds blinded", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.GibberingMouther] = BuildData("Gibbering",
                string.Empty,
                "1d2 rounds Confusion", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
            attackDamages[CreatureConstants.GibberingMouther] = BuildData("Improved Grab",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.GibberingMouther] = BuildData("Swallow Whole",
                string.Empty,
                "Blood Drain", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.GibberingMouther] = BuildData("Blood Drain",
                damageHelper.BuildEntries("1d4", AbilityConstants.Constitution),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.GibberingMouther] = BuildData("Ground Manipulation",
                string.Empty,
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Girallon] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Girallon] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Girallon] = BuildData("Rend", damageHelper.BuildEntries("2d4", clawDamageType), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Githyanki] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Githyanki] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Githyanki] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Githyanki] = BuildData(FeatConstants.SpecialQualities.Psionic, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Githzerai] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Githzerai] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Githzerai] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Githzerai] = BuildData(FeatConstants.SpecialQualities.Psionic, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Glabrezu] = BuildData("Pincer", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Glabrezu] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Glabrezu] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Glabrezu] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Glabrezu] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Glabrezu] = BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            attackDamages[CreatureConstants.Gnoll] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Gnoll] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Gnoll] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Gnome_Forest] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Gnome_Forest] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Gnome_Forest] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Gnome_Forest] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Gnome_Rock] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Gnome_Rock] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Gnome_Rock] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Gnome_Rock] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Gnome_Svirfneblin] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Gnome_Svirfneblin] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Gnome_Svirfneblin] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Gnome_Svirfneblin] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Goblin] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Goblin] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Goblin] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Golem_Clay] = BuildData("Slam", damageHelper.BuildEntries("2d10", slapSlamDamageType), "Cursed Wound", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Golem_Clay] = BuildData("Berserk", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            attackDamages[CreatureConstants.Golem_Clay] = BuildData("Cursed Wound", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
            attackDamages[CreatureConstants.Golem_Clay] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Golem_Flesh] = BuildData("Slam", damageHelper.BuildEntries("2d8", slapSlamDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Golem_Flesh] = BuildData("Berserk", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            attackDamages[CreatureConstants.Golem_Iron] = BuildData("Slam", damageHelper.BuildEntries("2d10", slapSlamDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Golem_Iron] = BuildData("Breath weapon", string.Empty, "Poisonous Gas", 0, "supernatural ability", 1, $"1d4+1 {FeatConstants.Frequencies.Round}", false, true, true, true));
            attackDamages[CreatureConstants.Golem_Iron] = BuildData("Poisonous Gas",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Constitution, "Initial",
                    "3d4", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "melee", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Golem_Stone] = BuildData("Slam", damageHelper.BuildEntries("2d10", slapSlamDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Golem_Stone] = BuildData("Slow", string.Empty, string.Empty, 0, "supernatural ability", 1, $"2 {FeatConstants.Frequencies.Round}", true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));

            attackDamages[CreatureConstants.Golem_Stone_Greater] = BuildData("Slam", damageHelper.BuildEntries("4d8", slapSlamDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Golem_Stone_Greater] = BuildData("Slow", string.Empty, string.Empty, 0, "supernatural ability", 1, $"2 {FeatConstants.Frequencies.Round}", true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));

            attackDamages[CreatureConstants.Gorgon] = BuildData("Gore", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Gorgon] = BuildData("Breath weapon", string.Empty, "Turn to stone", 0, "supernatural ability", 5, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Gorgon] = BuildData("Trample", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            attackDamages[CreatureConstants.GrayOoze] = BuildData("Slam",
                damageHelper.BuildEntries(
                    "1d6", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "1d6", FeatConstants.Foci.Elements.Acid),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.GrayOoze] = BuildData("Acid",
                damageHelper.BuildEntries("16", FeatConstants.Foci.Elements.Acid, "Wooden or Metal objects"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
            attackDamages[CreatureConstants.GrayOoze] = BuildData("Constrict",
                damageHelper.BuildEntries(
                    "1d6", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "1d6", FeatConstants.Foci.Elements.Acid),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.GrayOoze] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.GrayRender] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.GrayRender] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.GrayRender] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.GrayRender] = BuildData("Rend", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.GreenHag] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.GreenHag] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.GreenHag] = BuildData("Weakness",
                damageHelper.BuildEntries("2d4", AbilityConstants.Strength),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
            attackDamages[CreatureConstants.GreenHag] = BuildData("Mimicry", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Grick] = BuildData("Tentacle", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Grick] = BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            attackDamages[CreatureConstants.Griffon] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Griffon] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Griffon] = BuildData("Pounce", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            attackDamages[CreatureConstants.Griffon] = BuildData("Rake", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Grig] = BuildData(AttributeConstants.Melee,
                string.Empty,
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Grig] = BuildData(AttributeConstants.Ranged,
                string.Empty,
                string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Grig] = BuildData("Unarmed Strike",
                damageHelper.BuildEntries("1", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Grig] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Grig_WithFiddle] = BuildData(AttributeConstants.Melee,
                string.Empty,
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Grig_WithFiddle] = BuildData(AttributeConstants.Ranged,
                string.Empty,
                string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Grig_WithFiddle] = BuildData("Unarmed Strike",
                damageHelper.BuildEntries("1", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Grig_WithFiddle] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Grig_WithFiddle] = BuildData("Fiddle",
                string.Empty,
                SpellConstants.IrresistibleDance, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            attackDamages[CreatureConstants.Grimlock] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Grimlock] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Gynosphinx] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Gynosphinx] = BuildData("Pounce", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            attackDamages[CreatureConstants.Gynosphinx] = BuildData("Rake", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Gynosphinx] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Halfling_Deep] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Halfling_Deep] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Halfling_Deep] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Halfling_Lightfoot] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Halfling_Lightfoot] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Halfling_Lightfoot] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Halfling_Tallfellow] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Halfling_Tallfellow] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Halfling_Tallfellow] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Harpy] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Harpy] = BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Harpy] = BuildData("Captivating Song", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            attackDamages[CreatureConstants.Hawk] = BuildData("Talons", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.HellHound] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), "Fiery Bite", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.HellHound] = BuildData("Fiery Bite", damageHelper.BuildEntries("1d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.HellHound] = BuildData("Breath weapon", damageHelper.BuildEntries("2d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"2d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.HellHound_NessianWarhound] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), "Fiery Bite", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.HellHound_NessianWarhound] = BuildData("Fiery Bite", damageHelper.BuildEntries("1d8", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.HellHound_NessianWarhound] = BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "supernatural ability", 1, $"2d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Hellcat_Bezekira] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Hellcat_Bezekira] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Hellcat_Bezekira] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Hellcat_Bezekira] = BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            attackDamages[CreatureConstants.Hellcat_Bezekira] = BuildData("Rake", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Hellwasp_Swarm] = BuildData("Swarm", damageHelper.BuildEntries("3d6"), "Poison", 0, "swarm", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Hellwasp_Swarm] = BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            attackDamages[CreatureConstants.Hellwasp_Swarm] = BuildData("Inhabit", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, true, true, true, true));
            attackDamages[CreatureConstants.Hellwasp_Swarm] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Dexterity, "Initial",
                    "1d4", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Hezrou] = BuildData("Bite", damageHelper.BuildEntries("4d4", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Hezrou] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Hezrou] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Hezrou] = BuildData("Stench", string.Empty, "Nauseated while in range + 1d4 rounds afterwards", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            attackDamages[CreatureConstants.Hezrou] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Hezrou] = BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            attackDamages[CreatureConstants.Hieracosphinx] = BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Hieracosphinx] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Hieracosphinx] = BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            attackDamages[CreatureConstants.Hieracosphinx] = BuildData("Rake", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Hippogriff] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Hippogriff] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Hobgoblin] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Hobgoblin] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Hobgoblin] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Homunculus] = BuildData("Bite",
                damageHelper.BuildEntries("1d4", biteDamageType),
                "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Homunculus] = BuildData("Poison",
                string.Empty,
                "Initial damage sleep for 1 minute, secondary damage sleep for another 5d6 minutes", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 2));

            attackDamages[CreatureConstants.HornedDevil_Cornugon] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.HornedDevil_Cornugon] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.HornedDevil_Cornugon] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.HornedDevil_Cornugon] = BuildData("Tail",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                "Infernal Wound", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.HornedDevil_Cornugon] = BuildData("Fear Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Will, AbilityConstants.Charisma));
            attackDamages[CreatureConstants.HornedDevil_Cornugon] = BuildData("Infernal Wound",
                damageHelper.BuildEntries("2"),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, string.Empty, AbilityConstants.Constitution));
            attackDamages[CreatureConstants.HornedDevil_Cornugon] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.HornedDevil_Cornugon] = BuildData("Stun", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Strength));
            attackDamages[CreatureConstants.HornedDevil_Cornugon] = BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            attackDamages[CreatureConstants.Horse_Heavy] = BuildData("Hoof",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));

            attackDamages[CreatureConstants.Horse_Heavy_War] = BuildData("Hoof",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Horse_Heavy_War] = BuildData("Bite",
                damageHelper.BuildEntries("1d4", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            attackDamages[CreatureConstants.Horse_Light] = BuildData("Hoof",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));

            attackDamages[CreatureConstants.Horse_Light_War] = BuildData("Hoof",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Horse_Light_War] = BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            attackDamages[CreatureConstants.HoundArchon] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.HoundArchon] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.HoundArchon] = BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.HoundArchon] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Howler] = BuildData("Bite",
                damageHelper.BuildEntries("2d8", biteDamageType),
                "1d4 Quills", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Howler] = BuildData("Quill",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Dexterity, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Howler] = BuildData("Howl",
                damageHelper.BuildEntries("1", AbilityConstants.Wisdom),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hour, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            attackDamages[CreatureConstants.Human] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Human] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Human] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Hydra_5Heads] = BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 5, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Hydra_6Heads] = BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 6, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Hydra_7Heads] = BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 7, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Hydra_8Heads] = BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 8, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Hydra_9Heads] = BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 9, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Hydra_10Heads] = BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 10, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Hydra_11Heads] = BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 11, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Hydra_12Heads] = BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 12, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Hyena] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Hyena] = BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            attackDamages[CreatureConstants.IceDevil_Gelugon] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.IceDevil_Gelugon] = BuildData("Claw", damageHelper.BuildEntries("1d10", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.IceDevil_Gelugon] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.IceDevil_Gelugon] = BuildData("Tail", damageHelper.BuildEntries("3d6", AttributeConstants.DamageTypes.Bludgeoning), "slow", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.IceDevil_Gelugon] = BuildData("Fear Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Will, AbilityConstants.Charisma));
            attackDamages[CreatureConstants.IceDevil_Gelugon] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.IceDevil_Gelugon] = BuildData("Slow", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            attackDamages[CreatureConstants.IceDevil_Gelugon] = BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            attackDamages[CreatureConstants.Imp] = BuildData("Sting", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), "poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Imp] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Dexterity, "Initial",
                    "2d4", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution, 2));
            attackDamages[CreatureConstants.Imp] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.InvisibleStalker] = BuildData("Slam", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Janni] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Janni] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Janni] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Janni] = BuildData("Change Size", string.Empty, string.Empty, 0, "spell-like ability", 2, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Janni] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Kobold] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Kobold] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Kobold] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Kolyarut] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Kolyarut] = BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Kolyarut] = BuildData("Vampiric Touch",
                damageHelper.BuildEntries("5d6"),
                "Gain temporary hit points equal to damage dealt", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            attackDamages[CreatureConstants.Kolyarut] = BuildData("Enervation Ray",
                damageHelper.BuildEntries("1d4", "Negative Level"),
                string.Empty, 0, "ranged touch", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Kolyarut] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Kraken] = BuildData("Tentacle",
                damageHelper.BuildEntries("2d8", tentacleDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Kraken] = BuildData("Arm",
                damageHelper.BuildEntries("1d6", tentacleDamageType),
                string.Empty, 0.5, "melee", 6, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Kraken] = BuildData("Bite",
                damageHelper.BuildEntries("4d6", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Kraken] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Kraken] = BuildData("Constrict (Tentacle)",
                damageHelper.BuildEntries("2d8", tentacleDamageType),
                string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Kraken] = BuildData("Constrict (Arm)",
                damageHelper.BuildEntries("1d6", tentacleDamageType),
                string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Kraken] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Krenshar] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Krenshar] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Krenshar] = BuildData("Scare", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Krenshar] = BuildData("Scare with Screech", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            attackDamages[CreatureConstants.KuoToa] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.KuoToa] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.KuoToa] = BuildData("Lightning Bolt",
                damageHelper.BuildEntries("1d6", FeatConstants.Foci.Elements.Electricity, "per Kuo-Toa Cleric"),
                string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", true, true, true, false, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Lamia] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Lamia] = BuildData("Touch", string.Empty, "Wisdom Drain", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Lamia] = BuildData("Wisdom Drain",
                damageHelper.BuildEntries("1d4", AbilityConstants.Wisdom),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Lamia] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Lamia] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Lammasu] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Lammasu] = BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            attackDamages[CreatureConstants.Lammasu] = BuildData("Rake", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));
            attackDamages[CreatureConstants.Lammasu] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Lammasu] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.LanternArchon] = BuildData("Light Ray", damageHelper.BuildEntries("1d6"), string.Empty, 0, "ranged touch", 2, FeatConstants.Frequencies.Round, false, true, true, false));
            attackDamages[CreatureConstants.LanternArchon] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Lemure] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Leonal] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Leonal] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Leonal] = BuildData("Roar", damageHelper.BuildEntries("2d6", FeatConstants.Foci.Elements.Sonic), string.Empty, 0, "supernatural ability", 3, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
            attackDamages[CreatureConstants.Leonal] = BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            attackDamages[CreatureConstants.Leonal] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Leonal] = BuildData("Rake", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));
            attackDamages[CreatureConstants.Leonal] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Leopard] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Leopard] = BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Leopard] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Leopard] = BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            attackDamages[CreatureConstants.Leopard] = BuildData("Rake", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

            attackDamages[CreatureConstants.Lillend] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Lillend] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Lillend] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", slapSlamDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Lillend] = BuildData("Constrict", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Lillend] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Lillend] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Lillend] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Lion] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Lion] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Lion] = BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            attackDamages[CreatureConstants.Lion] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Lion] = BuildData("Rake", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

            attackDamages[CreatureConstants.Lion_Dire] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Lion_Dire] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Lion_Dire] = BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            attackDamages[CreatureConstants.Lion_Dire] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Lion_Dire] = BuildData("Rake", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

            attackDamages[CreatureConstants.Lizard] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Lizard_Monitor] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Lizardfolk] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Lizardfolk] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Lizardfolk] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Lizardfolk] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            attackDamages[CreatureConstants.Locathah] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Locathah] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Locathah] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Locust_Swarm] = BuildData("Swarm", damageHelper.BuildEntries("2d6"), string.Empty, 0, "swarm", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Locust_Swarm] = BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Magmin] = BuildData("Burning Touch", damageHelper.BuildEntries("1d8", FeatConstants.Foci.Elements.Fire), "Combustion", 0, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Magmin] = BuildData("Slam", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), "Combustion", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Magmin] = BuildData("Combustion", damageHelper.BuildEntries("1d8", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.Magmin] = BuildData("Fiery Aura", damageHelper.BuildEntries("1d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.MantaRay] = BuildData("Ram", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            attackDamages[CreatureConstants.Manticore] = BuildData("Claw", damageHelper.BuildEntries("2d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Manticore] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Manticore] = BuildData("Spikes", string.Empty, "Tail Spikes", 0, "ranged", 6, FeatConstants.Frequencies.Round, false, true, true, false));
            attackDamages[CreatureConstants.Manticore] = BuildData("Tail Spikes", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "extraordinary ability", 24, FeatConstants.Frequencies.Day, false, true, true, true));

            attackDamages[CreatureConstants.Marilith] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Marilith] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 5, FeatConstants.Frequencies.Round, true, false, false, false));
            attackDamages[CreatureConstants.Marilith] = BuildData("Tail Slap", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Marilith] = BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 6, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Marilith] = BuildData("Constrict", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Strength));
            attackDamages[CreatureConstants.Marilith] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Marilith] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Marilith] = BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            attackDamages[CreatureConstants.Marut] = BuildData("Slam", damageHelper.BuildEntries("2d6", slapSlamDamageType), "Fist of Thunder", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Marut] = BuildData("Slam", damageHelper.BuildEntries("2d6", slapSlamDamageType), "Fist of Lightning", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Marut] = BuildData("Fist of Thunder",
                damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Sonic),
                "deafened 2d6 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Marut] = BuildData("Fist of Lightning",
                damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Electricity),
                "blinded 2d6 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Marut] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Medusa] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Medusa] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Medusa] = BuildData("Snakes",
                damageHelper.BuildEntries("1d4", biteDamageType),
                "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Medusa] = BuildData("Petrifying Gaze", string.Empty, "Permanent petrification", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
            attackDamages[CreatureConstants.Medusa] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Strength, "Initial",
                    "2d6", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Megaraptor] = BuildData("Talons",
                damageHelper.BuildEntries("2d6", clawDamageType),
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Megaraptor] = BuildData("Foreclaw",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Megaraptor] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Megaraptor] = BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            attackDamages[CreatureConstants.Mephit_Air] = BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Mephit_Air] = BuildData("Breath weapon",
                damageHelper.BuildEntries("1d8"),
                string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
            attackDamages[CreatureConstants.Mephit_Air] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Mephit_Air] = BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            attackDamages[CreatureConstants.Mephit_Dust] = BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Mephit_Dust] = BuildData("Breath weapon",
                damageHelper.BuildEntries("1d4"),
                "Itching Skin and Burning Eyes (-4 AC, -2 attack rolls for 3 rounds)", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
            attackDamages[CreatureConstants.Mephit_Dust] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Mephit_Dust] = BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            attackDamages[CreatureConstants.Mephit_Earth] = BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Mephit_Earth] = BuildData("Breath weapon",
                damageHelper.BuildEntries("1d8"),
                string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
            attackDamages[CreatureConstants.Mephit_Earth] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Mephit_Earth] = BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            attackDamages[CreatureConstants.Mephit_Fire] = BuildData("Claw",
                damageHelper.BuildEntries(
                    "1d3", clawDamageType, string.Empty,
                    "1d4", FeatConstants.Foci.Elements.Fire),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Mephit_Fire] = BuildData("Breath weapon",
                damageHelper.BuildEntries("1d8", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
            attackDamages[CreatureConstants.Mephit_Fire] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Mephit_Fire] = BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            attackDamages[CreatureConstants.Mephit_Ice] = BuildData("Claw",
                damageHelper.BuildEntries(
                    "1d3", clawDamageType, string.Empty,
                    "1d4", FeatConstants.Foci.Elements.Cold),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Mephit_Ice] = BuildData("Breath weapon",
                damageHelper.BuildEntries("1d4", FeatConstants.Foci.Elements.Cold),
                "Frostbitten Skin and Frozen Eyes (-4 AC, -2 attack rolls for 3 rounds)", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
            attackDamages[CreatureConstants.Mephit_Ice] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Mephit_Ice] = BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            attackDamages[CreatureConstants.Mephit_Magma] = BuildData("Claw",
                damageHelper.BuildEntries(
                    "1d3", clawDamageType, string.Empty,
                    "1d4", FeatConstants.Foci.Elements.Fire),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Mephit_Magma] = BuildData("Breath weapon",
                damageHelper.BuildEntries("1d4", FeatConstants.Foci.Elements.Fire),
                "Burning Skin and Seared Eyes (-4 AC, -2 attack rolls for 3 rounds)", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
            attackDamages[CreatureConstants.Mephit_Magma] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Mephit_Magma] = BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            attackDamages[CreatureConstants.Mephit_Ooze] = BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Mephit_Ooze] = BuildData("Breath weapon",
                damageHelper.BuildEntries("1d4", FeatConstants.Foci.Elements.Acid),
                "Itching Skin and Burning Eyes (-4 AC, -2 attack rolls for 3 rounds)", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
            attackDamages[CreatureConstants.Mephit_Ooze] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Mephit_Ooze] = BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            attackDamages[CreatureConstants.Mephit_Salt] = BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Mephit_Salt] = BuildData("Breath weapon",
                damageHelper.BuildEntries("1d4"),
                "Itching Skin and Burning Eyes (-4 AC, -2 attack rolls for 3 rounds)", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
            attackDamages[CreatureConstants.Mephit_Salt] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Mephit_Salt] = BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            attackDamages[CreatureConstants.Mephit_Steam] = BuildData("Claw",
                damageHelper.BuildEntries(
                    "1d3", clawDamageType, string.Empty,
                    "1d4", FeatConstants.Foci.Elements.Fire),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Mephit_Steam] = BuildData("Breath weapon",
                damageHelper.BuildEntries("1d4", FeatConstants.Foci.Elements.Fire),
                "Burning Skin and Seared Eyes (-4 AC, -2 attack rolls for 3 rounds)", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
            attackDamages[CreatureConstants.Mephit_Steam] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Mephit_Steam] = BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            attackDamages[CreatureConstants.Mephit_Water] = BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Mephit_Water] = BuildData("Breath weapon",
                damageHelper.BuildEntries("1d8", FeatConstants.Foci.Elements.Acid),
                string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
            attackDamages[CreatureConstants.Mephit_Water] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Mephit_Water] = BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            attackDamages[CreatureConstants.Merfolk] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Merfolk] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Merfolk] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Mimic] = BuildData("Slam", damageHelper.BuildEntries("1d8", slapSlamDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Mimic] = BuildData("Adhesive", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Mimic] = BuildData("Crush", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.MindFlayer] = BuildData("Tentacle", damageHelper.BuildEntries("1d4", tentacleDamageType), string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.MindFlayer] = BuildData("Mind Blast", string.Empty, "3d4 rounds stunned", 1, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
            attackDamages[CreatureConstants.MindFlayer] = BuildData(FeatConstants.SpecialQualities.Psionic, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.MindFlayer] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.MindFlayer] = BuildData("Extract", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Minotaur] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Minotaur] = BuildData("Gore", damageHelper.BuildEntries("1d8", goreDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Minotaur] = BuildData("Powerful Charge", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            attackDamages[CreatureConstants.Mohrg] = BuildData("Slam", damageHelper.BuildEntries("1d6", slapSlamDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Mohrg] = BuildData("Tongue", string.Empty, "Paralyzing Touch", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Mohrg] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Mohrg] = BuildData("Paralyzing Touch", string.Empty, "1d4 minutes paralyzed", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Mohrg] = BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));

            attackDamages[CreatureConstants.Monkey] = BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Mule] = BuildData("Hoof", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Mummy] = BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), "Disease", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Mummy] = BuildData("Despair", string.Empty, "1d4 rounds fear", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Mummy] = BuildData("Disease", string.Empty, "Mummy Rot", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Mummy] = BuildData("Mummy Rot",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Constitution, "Incubation period 1 minute",
                    "1d6", AbilityConstants.Charisma, "Incubation period 1 minute"),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Naga_Dark] = BuildData("Sting", damageHelper.BuildEntries("2d4", stingDamageType), "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Naga_Dark] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Naga_Dark] = BuildData("Poison", string.Empty, "lapse into a nightmare-haunted sleep for 2d4 minutes", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Naga_Dark] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Naga_Guardian] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Naga_Guardian] = BuildData("Spit", string.Empty, "Poison", 0, "ranged touch", 1, FeatConstants.Frequencies.Round, false, true, false, false));
            attackDamages[CreatureConstants.Naga_Guardian] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d10", AbilityConstants.Constitution, "Initial",
                    "1d10", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Naga_Guardian] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Naga_Spirit] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Naga_Spirit] = BuildData("Charming Gaze", string.Empty, SpellConstants.CharmPerson, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Naga_Spirit] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d8", AbilityConstants.Constitution, "Initial",
                    "1d8", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Naga_Spirit] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Naga_Water] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Naga_Water] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d8", AbilityConstants.Constitution, "Initial",
                    "1d8", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Naga_Water] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Nalfeshnee] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Nalfeshnee] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Nalfeshnee] = BuildData("Smite", string.Empty, string.Empty, 1, "supernatural ability", 3, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
            attackDamages[CreatureConstants.Nalfeshnee] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Nalfeshnee] = BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            attackDamages[CreatureConstants.NightHag] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), "Disease", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.NightHag] = BuildData("Disease",
                damageHelper.BuildEntries("1d6", AbilityConstants.Constitution, "Incubation period 1 day"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.NightHag] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.NightHag] = BuildData("Dream Haunting", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            attackDamages[CreatureConstants.Nightcrawler] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Nightcrawler] = BuildData("Sting", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Nightcrawler] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Nightcrawler] = BuildData("Desecrating Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));
            attackDamages[CreatureConstants.Nightcrawler] = BuildData("Energy Drain",
                damageHelper.BuildEntries("1", "Negative Level"),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Nightcrawler] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Nightcrawler] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "2d6", AbilityConstants.Strength, "Initial",
                    "2d6", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Nightcrawler] = BuildData("Summon Undead", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
            attackDamages[CreatureConstants.Nightcrawler] = BuildData("Swallow Whole",
                damageHelper.BuildEntries(
                    "2d8+12", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "12", FeatConstants.Foci.Elements.Acid),
                "Energy Drain", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Nightmare] = BuildData("Hoof",
                damageHelper.BuildEntries(
                    "1d8", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "1d4", FeatConstants.Foci.Elements.Fire),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Nightmare] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Nightmare] = BuildData("Flaming Hooves", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            attackDamages[CreatureConstants.Nightmare] = BuildData("Smoke", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Nightmare] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Nightmare_Cauchemar] = BuildData("Hoof",
                damageHelper.BuildEntries(
                    "2d6", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "1d4", FeatConstants.Foci.Elements.Fire),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Nightmare_Cauchemar] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Nightmare_Cauchemar] = BuildData("Flaming Hooves", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            attackDamages[CreatureConstants.Nightmare_Cauchemar] = BuildData("Smoke", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Nightmare_Cauchemar] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Nightwalker] = BuildData("Slam", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Nightwalker] = BuildData("Crush Item", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Nightwalker] = BuildData("Desecrating Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));
            attackDamages[CreatureConstants.Nightwalker] = BuildData("Evil Gaze", string.Empty, "1d8 rounds paralyzed with fear", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Nightwalker] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Nightwalker] = BuildData("Summon Undead", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            attackDamages[CreatureConstants.Nightwing] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), "Magic Drain", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Nightwing] = BuildData("Desecrating Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));
            attackDamages[CreatureConstants.Nightwing] = BuildData("Magic Drain", string.Empty, "1 point enhancement bonus", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Nightwing] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Nightwing] = BuildData("Summon Undead", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            attackDamages[CreatureConstants.Nixie] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Nixie] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Nixie] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Nixie] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Nymph] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Nymph] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Nymph] = BuildData("Blinding Beauty", string.Empty, "Blinded permanently", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Nymph] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Nymph] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Nymph] = BuildData("Stunning Glance", string.Empty, "stunned 2d4 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.OchreJelly] = BuildData("Slam",
                damageHelper.BuildEntries(
                    "2d4", slapSlamDamageType, string.Empty,
                    "1d4", FeatConstants.Foci.Elements.Acid),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.OchreJelly] = BuildData("Acid", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
            attackDamages[CreatureConstants.OchreJelly] = BuildData("Constrict",
                damageHelper.BuildEntries(
                    "2d4", slapSlamDamageType, string.Empty,
                    "1d4", FeatConstants.Foci.Elements.Acid),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.OchreJelly] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Octopus] = BuildData("Arms", damageHelper.BuildEntries("0", tentacleDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Octopus] = BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Octopus] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Octopus_Giant] = BuildData("Tentacle", damageHelper.BuildEntries("1d4", tentacleDamageType), string.Empty, 1, "melee", 8, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Octopus_Giant] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Octopus_Giant] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Octopus_Giant] = BuildData("Constrict", damageHelper.BuildEntries("2d8", tentacleDamageType), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Ogre] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Ogre] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Ogre] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Ogre_Merrow] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Ogre_Merrow] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Ogre_Merrow] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.OgreMage] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.OgreMage] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.OgreMage] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.OgreMage] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Orc] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Orc] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Orc] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Orc_Half] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Orc_Half] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Orc_Half] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Otyugh] = BuildData("Tentacle", damageHelper.BuildEntries("1d6", tentacleDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Otyugh] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), "Disease", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Otyugh] = BuildData("Constrict", damageHelper.BuildEntries("1d6", tentacleDamageType), string.Empty, 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Otyugh] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Otyugh] = BuildData("Disease", string.Empty, "Filth Fever", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            attackDamages[CreatureConstants.Otyugh] = BuildData("Filth Fever",
                damageHelper.BuildEntries(
                    "1d3", AbilityConstants.Dexterity, "Incubation period 1d3 days",
                    "1d3", AbilityConstants.Constitution, "Incubation period 1d3 days"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Owl] = BuildData("Talons", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Owl_Giant] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Owl_Giant] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            attackDamages[CreatureConstants.Owlbear] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Owlbear] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Owlbear] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Pegasus] = BuildData("Hoof", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Pegasus] = BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Pegasus] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.PhantomFungus] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.PhaseSpider] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.PhaseSpider] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d8", AbilityConstants.Constitution, "Initial",
                    "1d8", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.PhaseSpider] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Phasm] = BuildData("Slam", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.PitFiend] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.PitFiend] = BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.PitFiend] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), "poison, disease", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.PitFiend] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.PitFiend] = BuildData("Constrict", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 2, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.PitFiend] = BuildData("Fear Aura", string.Empty, string.Empty, 0, "supernatural ability", 2, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Will, AbilityConstants.Charisma));
            attackDamages[CreatureConstants.PitFiend] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.PitFiend] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.PitFiend] = BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
            attackDamages[CreatureConstants.PitFiend] = BuildData("Poison",
                damageHelper.BuildEntries("1d6", AbilityConstants.Constitution, "Initial"),
                "Death (Secondary)", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            attackDamages[CreatureConstants.PitFiend] = BuildData("Disease", string.Empty, "Devil Chills", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            attackDamages[CreatureConstants.PitFiend] = BuildData("Devil Chills",
                damageHelper.BuildEntries("1d4", AbilityConstants.Strength, "Incubation period 1d4 days"),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Pixie] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Pixie] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Pixie] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Pixie] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Pixie] = BuildData("Special Arrow (Memory Loss)", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, false, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveDcBonus: 2));
            attackDamages[CreatureConstants.Pixie] = BuildData("Special Arrow (Sleep)", string.Empty, SpellConstants.Sleep, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, false, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveDcBonus: 2));

            attackDamages[CreatureConstants.Pixie_WithIrresistibleDance] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Pixie_WithIrresistibleDance] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Pixie_WithIrresistibleDance] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Pixie_WithIrresistibleDance] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Pixie_WithIrresistibleDance] = BuildData("Special Arrow (Memory Loss)", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, false, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveDcBonus: 2));
            attackDamages[CreatureConstants.Pixie_WithIrresistibleDance] = BuildData("Special Arrow (Sleep)", string.Empty, SpellConstants.Sleep, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, false, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveDcBonus: 2));

            attackDamages[CreatureConstants.Pony] = BuildData("Hoof", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));

            attackDamages[CreatureConstants.Pony_War] = BuildData("Hoof", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Porpoise] = BuildData("Slam", damageHelper.BuildEntries("2d4", slapSlamDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.PrayingMantis_Giant] = BuildData("Claws", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.PrayingMantis_Giant] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.PrayingMantis_Giant] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Pseudodragon] = BuildData("Sting",
                damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Piercing),
                "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Pseudodragon] = BuildData("Bite",
                damageHelper.BuildEntries("1", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Pseudodragon] = BuildData("Poison",
                string.Empty,
                "initial damage sleep for 1 minute, secondary damage sleep for 1d3 hours", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 2));

            attackDamages[CreatureConstants.PurpleWorm] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.PurpleWorm] = BuildData("Sting", damageHelper.BuildEntries("2d6", stingDamageType), "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.PurpleWorm] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.PurpleWorm] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Strength, "Initial",
                    "2d6", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.PurpleWorm] = BuildData("Swallow Whole",
                damageHelper.BuildEntries(
                    "2d8+12", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "8", FeatConstants.Foci.Elements.Acid),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Pyrohydra_5Heads] = BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 5, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Pyrohydra_5Heads] = BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Fire, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Pyrohydra_6Heads] = BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 6, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Pyrohydra_6Heads] = BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Fire, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Pyrohydra_7Heads] = BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 7, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Pyrohydra_7Heads] = BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Fire, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Pyrohydra_8Heads] = BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 8, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Pyrohydra_8Heads] = BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Fire, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Pyrohydra_9Heads] = BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 9, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Pyrohydra_9Heads] = BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Fire, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Pyrohydra_10Heads] = BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 10, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Pyrohydra_10Heads] = BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Fire, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Pyrohydra_11Heads] = BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 11, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Pyrohydra_11Heads] = BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Fire, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Pyrohydra_12Heads] = BuildData("Bite", damageHelper.BuildEntries("1d10", biteDamageType), string.Empty, 1, "melee", 12, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Pyrohydra_12Heads] = BuildData("Breath weapon", damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Fire, "Per living head"), string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Quasit] = BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), "poison", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Quasit] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Quasit] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Dexterity, "Initial",
                    "2d4", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution, 2));
            attackDamages[CreatureConstants.Quasit] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Rakshasa] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Rakshasa] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Rakshasa] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Rast] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Rast] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Rast] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Rast] = BuildData("Paralyzing Gaze", string.Empty, "Paralysis for 1d6 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Rast] = BuildData("Blood Drain",
                damageHelper.BuildEntries("1", AbilityConstants.Constitution),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Rat] = BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Rat_Dire] = BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), "Disease", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Rat_Dire] = BuildData("Disease", string.Empty, "Filth Fever", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
            attackDamages[CreatureConstants.Rat_Dire] = BuildData("Filth Fever",
                damageHelper.BuildEntries(
                    "1d3", AbilityConstants.Dexterity, "Incubation period 1d3 days",
                    "1d3", AbilityConstants.Constitution, "Incubation period 1d3 days"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Rat_Swarm] = BuildData("Swarm", damageHelper.BuildEntries("1d6"), "Disease", 0, "swarm", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Rat_Swarm] = BuildData("Disease", string.Empty, "Filth Fever", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
            attackDamages[CreatureConstants.Rat_Swarm] = BuildData("Filth Fever",
                damageHelper.BuildEntries(
                    "1d3", AbilityConstants.Dexterity, "Incubation period 1d3 days",
                    "1d3", AbilityConstants.Constitution, "Incubation period 1d3 days"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            attackDamages[CreatureConstants.Rat_Swarm] = BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Raven] = BuildData("Claws", damageHelper.BuildEntries("1d2", clawDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Ravid] = BuildData("Tail Slap", damageHelper.BuildEntries("1d6", slapSlamDamageType), "Positive Energy", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Ravid] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), "Positive Energy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Ravid] = BuildData("Tail Touch", string.Empty, "Positive Energy", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Ravid] = BuildData("Claw Touch", string.Empty, "Positive Energy", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Ravid] = BuildData("Positive Energy",
                damageHelper.BuildEntries("2d10", "Positive energy"),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Ravid] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.RazorBoar] = BuildData("Tusk Slash", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), "Vorpal Tusk", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.RazorBoar] = BuildData("Hoof", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.RazorBoar] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.RazorBoar] = BuildData("Vorpal Tusk", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.RazorBoar] = BuildData("Trample", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Remorhaz] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Remorhaz] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Remorhaz] = BuildData("Swallow Whole",
                damageHelper.BuildEntries(
                    "2d8+12", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "8d6", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Retriever] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Retriever] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Retriever] = BuildData("Eye Ray", string.Empty, string.Empty, 0, "ranged touch", 1, FeatConstants.Frequencies.Round, false, true, false, true, string.Empty, AbilityConstants.Dexterity));
            attackDamages[CreatureConstants.Retriever] = BuildData("Find Target", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));
            attackDamages[CreatureConstants.Retriever] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Rhinoceras] = BuildData("Gore", damageHelper.BuildEntries("2d6", goreDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Rhinoceras] = BuildData("Powerful Charge", damageHelper.BuildEntries("4d6", goreDamageType), string.Empty, 3, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            attackDamages[CreatureConstants.Roc] = BuildData("Talon", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Roc] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            attackDamages[CreatureConstants.Roper] = BuildData("Strand", string.Empty, "Drag", 0, "ranged touch", 6, FeatConstants.Frequencies.Round, false, true, false, false));
            attackDamages[CreatureConstants.Roper] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Roper] = BuildData("Drag", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Strength));
            attackDamages[CreatureConstants.Roper] = BuildData("Weakness",
                damageHelper.BuildEntries("2d8", AbilityConstants.Strength),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.RustMonster] = BuildData("Antennae", string.Empty, "Rust", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.RustMonster] = BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.RustMonster] = BuildData("Rust", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 4));

            attackDamages[CreatureConstants.Sahuagin] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Sahuagin] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Sahuagin] = BuildData("Talon", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Sahuagin] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Sahuagin] = BuildData("Blood Frenzy", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
            attackDamages[CreatureConstants.Sahuagin] = BuildData("Rake", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Sahuagin_Mutant] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Sahuagin_Mutant] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 2, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Sahuagin_Mutant] = BuildData("Talon", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Sahuagin_Mutant] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Sahuagin_Mutant] = BuildData("Blood Frenzy", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
            attackDamages[CreatureConstants.Sahuagin_Mutant] = BuildData("Rake", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Sahuagin_Malenti] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Sahuagin_Malenti] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Sahuagin_Malenti] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Sahuagin_Malenti] = BuildData("Blood Frenzy", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            attackDamages[CreatureConstants.Salamander_Flamebrother] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Salamander_Flamebrother] = BuildData("Tail Slap", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), "Heat", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Salamander_Flamebrother] = BuildData("Constrict", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), "Heat", 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Salamander_Flamebrother] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Salamander_Flamebrother] = BuildData("Heat",
                 damageHelper.BuildEntries("1d6", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));

            attackDamages[CreatureConstants.Salamander_Average] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Salamander_Average] = BuildData("Tail Slap", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), "Heat", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Salamander_Average] = BuildData("Constrict", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), "Heat", 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Salamander_Average] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Salamander_Average] = BuildData("Heat",
                 damageHelper.BuildEntries("1d6", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));

            attackDamages[CreatureConstants.Salamander_Noble] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Salamander_Noble] = BuildData("Tail Slap", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), "Heat", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Salamander_Noble] = BuildData("Constrict", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), "Heat", 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Salamander_Noble] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Salamander_Noble] = BuildData("Heat",
                 damageHelper.BuildEntries("1d8", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            attackDamages[CreatureConstants.Salamander_Noble] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Satyr] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, false, false));
            attackDamages[CreatureConstants.Satyr] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 0, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Satyr] = BuildData("Head butt", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Satyr_WithPipes] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, false, false));
            attackDamages[CreatureConstants.Satyr_WithPipes] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 0, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Satyr_WithPipes] = BuildData("Head butt", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Satyr_WithPipes] = BuildData("Pipes", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            attackDamages[CreatureConstants.Scorpion_Monstrous_Tiny] = BuildData("Claw", damageHelper.BuildEntries("1d2", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Tiny] = BuildData("Sting", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Piercing), "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Tiny] = BuildData("Constrict", damageHelper.BuildEntries("1d2", clawDamageType), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Tiny] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Tiny] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1", AbilityConstants.Constitution, "Initial",
                    "1", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Scorpion_Monstrous_Small] = BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Small] = BuildData("Sting", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Piercing), "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Small] = BuildData("Constrict", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Small] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Small] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d2", AbilityConstants.Constitution, "Initial",
                    "1d2", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Scorpion_Monstrous_Medium] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Medium] = BuildData("Sting", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Medium] = BuildData("Constrict", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Medium] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Medium] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d3", AbilityConstants.Constitution, "Initial",
                    "1d3", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Scorpion_Monstrous_Large] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Large] = BuildData("Sting", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Large] = BuildData("Constrict", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Large] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Large] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Constitution, "Initial",
                    "1d4", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Scorpion_Monstrous_Huge] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Huge] = BuildData("Sting", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Huge] = BuildData("Constrict", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Huge] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Huge] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Scorpion_Monstrous_Gargantuan] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Gargantuan] = BuildData("Sting", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Gargantuan] = BuildData("Constrict", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Gargantuan] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Gargantuan] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d8", AbilityConstants.Constitution, "Initial",
                    "1d8", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Scorpion_Monstrous_Colossal] = BuildData("Claw", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Colossal] = BuildData("Sting", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Colossal] = BuildData("Constrict", damageHelper.BuildEntries("2d8", clawDamageType), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Colossal] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Scorpion_Monstrous_Colossal] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d10", AbilityConstants.Constitution, "Initial",
                    "1d10", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Scorpionfolk] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Scorpionfolk] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 0, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Scorpionfolk] = BuildData("Sting", damageHelper.BuildEntries("1d8", stingDamageType), "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Scorpionfolk] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Scorpionfolk] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Dexterity, "Initial",
                    "1d4", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Scorpionfolk] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Scorpionfolk] = BuildData("Trample", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.SeaCat] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.SeaCat] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.SeaCat] = BuildData("Rend", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.SeaHag] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.SeaHag] = BuildData("Horrific Appearance",
                damageHelper.BuildEntries("2d6", AbilityConstants.Strength),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
            attackDamages[CreatureConstants.SeaHag] = BuildData("Evil Eye", string.Empty, string.Empty, 0, "supernatural ability", 3, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));

            attackDamages[CreatureConstants.Shadow] = BuildData("Incorporeal touch", string.Empty, "Strength Damage", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Shadow] = BuildData("Strength Damage",
                damageHelper.BuildEntries("1d6", AbilityConstants.Strength),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            attackDamages[CreatureConstants.Shadow] = BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Shadow_Greater] = BuildData("Incorporeal touch", string.Empty, "Strength Damage", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Shadow_Greater] = BuildData("Strength Damage",
                damageHelper.BuildEntries("1d8", AbilityConstants.Strength),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            attackDamages[CreatureConstants.Shadow_Greater] = BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.ShadowMastiff] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.ShadowMastiff] = BuildData("Bay", string.Empty, "panicked 2d4 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
            attackDamages[CreatureConstants.ShadowMastiff] = BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            attackDamages[CreatureConstants.ShamblingMound] = BuildData("Slam", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.ShamblingMound] = BuildData("Constrict", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.ShamblingMound] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Shark_Dire] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Shark_Dire] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Shark_Dire] = BuildData("Swallow Whole",
                damageHelper.BuildEntries(
                    "2d6+6", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "1d8+4", FeatConstants.Foci.Elements.Acid),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Shark_Huge] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Shark_Large] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Shark_Medium] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.ShieldGuardian] = BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.ShieldGuardian] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.ShockerLizard] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.ShockerLizard] = BuildData("Stunning Shock",
                damageHelper.BuildEntries("2d8", FeatConstants.Foci.Elements.Electricity),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
            attackDamages[CreatureConstants.ShockerLizard] = BuildData("Lethal Shock",
                damageHelper.BuildEntries("2d8", FeatConstants.Foci.Elements.Electricity, "per Shocker Lizard"),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, false, true, true, true, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Shrieker] = BuildData("Shriek", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Skum] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Skum] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Skum] = BuildData("Rake", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

            attackDamages[CreatureConstants.Slaad_Red] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Slaad_Red] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), "Implant", 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Slaad_Red] = BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            attackDamages[CreatureConstants.Slaad_Red] = BuildData("Implant", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Slaad_Red] = BuildData("Stunning Croak", string.Empty, "Stunned 1d3 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Slaad_Red] = BuildData("Summon Slaad", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            attackDamages[CreatureConstants.Slaad_Blue] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Slaad_Blue] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), "Disease", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Slaad_Blue] = BuildData("Disease", string.Empty, "Slaad Fever", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            attackDamages[CreatureConstants.Slaad_Blue] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Slaad_Blue] = BuildData("Slaad Fever",
                damageHelper.BuildEntries(
                    "1d3", AbilityConstants.Dexterity, "Incubation period 1 day",
                    "1d3", AbilityConstants.Charisma, "Incubation period 1 day"),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Slaad_Blue] = BuildData("Summon Slaad", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            attackDamages[CreatureConstants.Slaad_Green] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Slaad_Green] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Slaad_Green] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Slaad_Green] = BuildData("Summon Slaad", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            attackDamages[CreatureConstants.Slaad_Gray] = BuildData("Claw", damageHelper.BuildEntries("2d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Slaad_Gray] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Slaad_Gray] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Slaad_Gray] = BuildData("Summon Slaad", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            attackDamages[CreatureConstants.Slaad_Death] = BuildData("Claw", damageHelper.BuildEntries("3d6", clawDamageType), "Stun", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Slaad_Death] = BuildData("Bite", damageHelper.BuildEntries("2d10", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Slaad_Death] = BuildData("Stun", string.Empty, "Stunned 1 round", 0, "extraordinary ability", 3, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Wisdom, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Slaad_Death] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Slaad_Death] = BuildData("Summon Slaad", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            attackDamages[CreatureConstants.Snake_Constrictor] = BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Snake_Constrictor] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Snake_Constrictor] = BuildData("Constrict", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Snake_Constrictor_Giant] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Snake_Constrictor_Giant] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Snake_Constrictor_Giant] = BuildData("Constrict", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Snake_Viper_Tiny] = BuildData("Bite",
                damageHelper.BuildEntries("1", biteDamageType),
                "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Snake_Viper_Tiny] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Snake_Viper_Small] = BuildData("Bite", damageHelper.BuildEntries("1d2", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Snake_Viper_Small] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Snake_Viper_Medium] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Snake_Viper_Medium] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Snake_Viper_Large] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Snake_Viper_Large] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Snake_Viper_Huge] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Snake_Viper_Huge] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Spectre] = BuildData("Incorporeal touch", damageHelper.BuildEntries("1d8"), "Energy Drain", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Spectre] = BuildData("Energy Drain",
                damageHelper.BuildEntries("2", "Negative Level"),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Spectre] = BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Spider_Monstrous_Hunter_Tiny] = BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Spider_Monstrous_Hunter_Tiny] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d2", AbilityConstants.Strength, "Initial",
                    "1d2", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Spider_Monstrous_Hunter_Small] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Spider_Monstrous_Hunter_Small] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d3", AbilityConstants.Strength, "Initial",
                    "1d3", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Spider_Monstrous_Hunter_Medium] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Spider_Monstrous_Hunter_Medium] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Strength, "Initial",
                    "1d4", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Spider_Monstrous_Hunter_Large] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Spider_Monstrous_Hunter_Large] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Strength, "Initial",
                    "1d6", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Spider_Monstrous_Hunter_Huge] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Spider_Monstrous_Hunter_Huge] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d8", AbilityConstants.Strength, "Initial",
                    "1d8", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "2d6", AbilityConstants.Strength, "Initial",
                    "2d6", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Spider_Monstrous_Hunter_Colossal] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Spider_Monstrous_Hunter_Colossal] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "2d8", AbilityConstants.Strength, "Initial",
                    "2d8", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny] = BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d2", AbilityConstants.Strength, "Initial",
                    "1d2", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            attackDamages[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny] = BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Spider_Monstrous_WebSpinner_Small] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Spider_Monstrous_WebSpinner_Small] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d3", AbilityConstants.Strength, "Initial",
                    "1d3", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            attackDamages[CreatureConstants.Spider_Monstrous_WebSpinner_Small] = BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Spider_Monstrous_WebSpinner_Medium] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Spider_Monstrous_WebSpinner_Medium] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Strength, "Initial",
                    "1d4", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            attackDamages[CreatureConstants.Spider_Monstrous_WebSpinner_Medium] = BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Spider_Monstrous_WebSpinner_Large] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Spider_Monstrous_WebSpinner_Large] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Strength, "Initial",
                    "1d6", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            attackDamages[CreatureConstants.Spider_Monstrous_WebSpinner_Large] = BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Spider_Monstrous_WebSpinner_Huge] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Spider_Monstrous_WebSpinner_Huge] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d8", AbilityConstants.Strength, "Initial",
                    "1d8", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            attackDamages[CreatureConstants.Spider_Monstrous_WebSpinner_Huge] = BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "2d6", AbilityConstants.Strength, "Initial",
                    "2d6", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            attackDamages[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan] = BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "2d8", AbilityConstants.Strength, "Initial",
                    "2d8", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            attackDamages[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal] = BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Constitution));

            attackDamages[CreatureConstants.SpiderEater] = BuildData("Sting", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.SpiderEater] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.SpiderEater] = BuildData("Poison",
                string.Empty,
                "Paralysis for 1d8+5 weeks (Secondary)", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            attackDamages[CreatureConstants.SpiderEater] = BuildData("Implant", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, requiredGender: GenderConstants.Female));

            attackDamages[CreatureConstants.Spider_Swarm] = BuildData("Swarm", damageHelper.BuildEntries("1d6"), "poison", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Spider_Swarm] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d3", AbilityConstants.Strength, "Initial",
                    "1d3", AbilityConstants.Strength, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            attackDamages[CreatureConstants.Spider_Swarm] = BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            attackDamages[CreatureConstants.Squid] = BuildData("Arms",
                damageHelper.BuildEntries("0", tentacleDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Squid] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Squid] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Squid_Giant] = BuildData("Tentacle", damageHelper.BuildEntries("1d6", tentacleDamageType), string.Empty, 1, "melee", 10, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Squid_Giant] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Squid_Giant] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Squid_Giant] = BuildData("Constrict", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.StagBeetle_Giant] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.StagBeetle_Giant] = BuildData("Trample", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Stirge] = BuildData("Touch", string.Empty, "Attach", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Stirge] = BuildData("Attach", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Stirge] = BuildData("Blood Drain",
                damageHelper.BuildEntries("1d4", AbilityConstants.Constitution),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            attackDamages[CreatureConstants.Succubus] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Succubus] = BuildData("Energy Drain",
                damageHelper.BuildEntries("1", "Negative Level"),
                SpellConstants.Suggestion, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, string.Empty, AbilityConstants.Charisma));
            attackDamages[CreatureConstants.Succubus] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Succubus] = BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            attackDamages[CreatureConstants.Tarrasque] = BuildData("Bite", damageHelper.BuildEntries("4d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Tarrasque] = BuildData("Horn",
                damageHelper.BuildEntries("1d10", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Tarrasque] = BuildData("Claw",
                damageHelper.BuildEntries("1d12", clawDamageType),
                string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Tarrasque] = BuildData("Tail Slap",
                damageHelper.BuildEntries("3d8", slapSlamDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Tarrasque] = BuildData("Augmented Critical", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Tarrasque] = BuildData("Frightful Presence", string.Empty, "Shaken", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
            attackDamages[CreatureConstants.Tarrasque] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Tarrasque] = BuildData("Rush", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true));
            attackDamages[CreatureConstants.Tarrasque] = BuildData("Swallow Whole",
                damageHelper.BuildEntries(
                    "2d8+8", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "2d8+6", FeatConstants.Foci.Elements.Acid),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Tendriculos] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Tendriculos] = BuildData("Tendril",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Tendriculos] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Tendriculos] = BuildData("Swallow Whole",
                damageHelper.BuildEntries("2d6", FeatConstants.Foci.Elements.Acid),
                "Paralysis", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Tendriculos] = BuildData("Paralysis", string.Empty, "paralyzed for 3d6 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Thoqqua] = BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), "Heat, Burn", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Thoqqua] = BuildData("Heat", damageHelper.BuildEntries("2d6", FeatConstants.Foci.Elements.Fire), string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Thoqqua] = BuildData("Burn", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Tiefling] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Tiefling] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Tiefling] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Tiefling] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Tiger] = BuildData("Claw", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Tiger] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Tiger] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Tiger] = BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            attackDamages[CreatureConstants.Tiger] = BuildData("Rake", damageHelper.BuildEntries("1d8", clawDamageType), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

            attackDamages[CreatureConstants.Tiger_Dire] = BuildData("Claw", damageHelper.BuildEntries("2d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Tiger_Dire] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Tiger_Dire] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Tiger_Dire] = BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            attackDamages[CreatureConstants.Tiger_Dire] = BuildData("Rake", damageHelper.BuildEntries("2d4", clawDamageType), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

            attackDamages[CreatureConstants.Titan] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Titan] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Titan] = BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Titan] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Toad] = new[] { None });

            attackDamages[CreatureConstants.Tojanida_Juvenile] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Tojanida_Juvenile] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Tojanida_Juvenile] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Tojanida_Juvenile] = BuildData("Ink Cloud", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Tojanida_Adult] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Tojanida_Adult] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Tojanida_Adult] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Tojanida_Adult] = BuildData("Ink Cloud", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Tojanida_Elder] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Tojanida_Elder] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Tojanida_Elder] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Tojanida_Elder] = BuildData("Ink Cloud", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Treant] = BuildData("Slam", damageHelper.BuildEntries("2d6", slapSlamDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Treant] = BuildData("Animate Trees", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Treant] = BuildData("Double Damage Against Objects", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Treant] = BuildData("Trample", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Triceratops] = BuildData("Gore",
                damageHelper.BuildEntries("2d8", goreDamageType),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Triceratops] = BuildData("Powerful charge",
                damageHelper.BuildEntries("4d8", goreDamageType),
                string.Empty, 2, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Triceratops] = BuildData("Trample",
                damageHelper.BuildEntries("2d12", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

            attackDamages[CreatureConstants.Triton] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Triton] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Triton] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Triton] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Troglodyte] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Troglodyte] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Troglodyte] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Troglodyte] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Troglodyte] = BuildData("Stench", string.Empty, "Sickened 10 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Troll] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Troll] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Troll] = BuildData("Rend", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Troll_Scrag] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Troll_Scrag] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Troll_Scrag] = BuildData("Rend", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.TrumpetArchon] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.TrumpetArchon] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.TrumpetArchon] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.TrumpetArchon] = BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.TrumpetArchon] = BuildData("Trumpet", string.Empty, "1d4 rounds paralyzed", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Tyrannosaurus] = BuildData("Bite",
                damageHelper.BuildEntries("3d6", biteDamageType),
                string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Tyrannosaurus] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Tyrannosaurus] = BuildData("Swallow Whole",
                damageHelper.BuildEntries(
                    "2d8", AttributeConstants.DamageTypes.Bludgeoning, string.Empty,
                    "8", FeatConstants.Foci.Elements.Acid),
                string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.UmberHulk] = BuildData("Claw", damageHelper.BuildEntries("2d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.UmberHulk] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.UmberHulk] = BuildData("Confusing Gaze", string.Empty, SpellConstants.Confusion, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            attackDamages[CreatureConstants.UmberHulk_TrulyHorrid] = BuildData("Claw", damageHelper.BuildEntries("3d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.UmberHulk_TrulyHorrid] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.UmberHulk_TrulyHorrid] = BuildData("Confusing Gaze", string.Empty, SpellConstants.Confusion, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

            attackDamages[CreatureConstants.Unicorn] = BuildData("Horn",
                damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Unicorn] = BuildData("Hoof",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Unicorn] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.VampireSpawn] = BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), "Energy Drain", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.VampireSpawn] = BuildData("Blood Drain",
                damageHelper.BuildEntries("1d4", AbilityConstants.Constitution),
                "Vampire Spawn gains 5 temporary hit points", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            attackDamages[CreatureConstants.VampireSpawn] = BuildData("Domination", string.Empty, SpellConstants.DominatePerson, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
            attackDamages[CreatureConstants.VampireSpawn] = BuildData("Energy Drain",
                damageHelper.BuildEntries("1", "Negative Level"),
                "Vampire Spawn gains 5 temporary hit points", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.VampireSpawn] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Vargouille] = BuildData("Bite", damageHelper.BuildEntries("1d4", biteDamageType), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Vargouille] = BuildData("Shriek", string.Empty, "paralyzed 2d4 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 1));
            attackDamages[CreatureConstants.Vargouille] = BuildData("Kiss", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 4));
            attackDamages[CreatureConstants.Vargouille] = BuildData("Poison", string.Empty, "unable to heal the vargouille’s bite damage naturally or magically", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 1));

            attackDamages[CreatureConstants.VioletFungus] = BuildData("Tentacle", damageHelper.BuildEntries("1d6", tentacleDamageType), "Poison", 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.VioletFungus] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Strength, "Initial",
                    "1d4", AbilityConstants.Constitution, "Initial",
                    "1d4", AbilityConstants.Strength, "Secondary",
                    "1d4", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Vrock] = BuildData("Claw", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Vrock] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Vrock] = BuildData("Talon", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Vrock] = BuildData("Dance of Ruin", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true, SaveConstants.Reflex, AbilityConstants.Charisma));
            attackDamages[CreatureConstants.Vrock] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            attackDamages[CreatureConstants.Vrock] = BuildData("Spores",
                damageHelper.BuildEntries("1d8"),
                string.Empty, 0, "melee", 1, $"3 {FeatConstants.Frequencies.Round}", false, true, true, true));
            attackDamages[CreatureConstants.Vrock] = BuildData("Stunning Screech", string.Empty, string.Empty, 0, "melee", 1, FeatConstants.Frequencies.Hour, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
            attackDamages[CreatureConstants.Vrock] = BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

            attackDamages[CreatureConstants.Wasp_Giant] = BuildData("Sting", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Wasp_Giant] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Dexterity, "Initial",
                    "1d6", AbilityConstants.Dexterity, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Weasel] = BuildData("Bite", damageHelper.BuildEntries("1d3", biteDamageType), "Attach", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Weasel] = BuildData("Attach",
                damageHelper.BuildEntries("1d3", biteDamageType),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Weasel_Dire] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), "Attach", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Weasel_Dire] = BuildData("Attach",
                string.Empty,
                "Blood Drain", 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Weasel_Dire] = BuildData("Blood Drain",
                damageHelper.BuildEntries("1d4", AbilityConstants.Constitution),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Whale_Baleen] = BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Whale_Cachalot] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Whale_Cachalot] = BuildData("Tail Slap", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            attackDamages[CreatureConstants.Whale_Orca] = BuildData("Bite", damageHelper.BuildEntries("2d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.Wight] = BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), "Energy Drain", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Wight] = BuildData("Energy Drain", damageHelper.BuildEntries("1", "Negative Level"), "Wight gains 5 temporary hit points", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Wight] = BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.WillOWisp] = BuildData("Shock",
                damageHelper.BuildEntries("2d8", FeatConstants.Foci.Elements.Electricity),
                string.Empty, 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            attackDamages[CreatureConstants.WinterWolf] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), "Freezing Bite", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.WinterWolf] = BuildData("Breath Weapon",
                damageHelper.BuildEntries("4d6", FeatConstants.Foci.Elements.Cold),
                string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
            attackDamages[CreatureConstants.WinterWolf] = BuildData("Freezing Bite",
                damageHelper.BuildEntries("1d6", FeatConstants.Foci.Elements.Cold),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
            attackDamages[CreatureConstants.WinterWolf] = BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            attackDamages[CreatureConstants.Wolf] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Wolf] = BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            attackDamages[CreatureConstants.Wolf_Dire] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Wolf_Dire] = BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            attackDamages[CreatureConstants.Wolverine] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Wolverine] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Wolverine] = BuildData("Rage", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));

            attackDamages[CreatureConstants.Wolverine_Dire] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Wolverine_Dire] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Wolverine_Dire] = BuildData("Rage", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));

            attackDamages[CreatureConstants.Worg] = BuildData("Bite", damageHelper.BuildEntries("1d6", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Worg] = BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            attackDamages[CreatureConstants.Wraith] = BuildData("Incorporeal touch",
                damageHelper.BuildEntries("1d4"),
                $"Constitution Drain", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Wraith] = BuildData("Constitution Drain",
                damageHelper.BuildEntries("1d4", AbilityConstants.Constitution),
                "Wraith gains 5 temporary hit points", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Wraith] = BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Wraith_Dread] = BuildData("Incorporeal touch",
                damageHelper.BuildEntries("2d6"),
                $"Constitution Drain", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Wraith_Dread] = BuildData("Constitution Drain",
                damageHelper.BuildEntries("1d8", AbilityConstants.Constitution),
                "Dread Wraith gains 5 temporary hit points", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Wraith_Dread] = BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Wyvern] = BuildData("Sting", damageHelper.BuildEntries("1d6", stingDamageType), "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Wyvern] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Wyvern] = BuildData("Wing", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Wyvern] = BuildData("Talon", damageHelper.BuildEntries("2d6", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Wyvern] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "2d6", AbilityConstants.Constitution, "Initial",
                    "2d6", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.Wyvern] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            attackDamages[CreatureConstants.Xill] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 2, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Xill] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 2, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.Xill] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Xill] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Xill] = BuildData("Bite",
                damageHelper.BuildEntries("0", biteDamageType),
                "Paralysis", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Xill] = BuildData("Implant", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Xill] = BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.Xill] = BuildData("Paralysis", string.Empty, "paralyzed for 1d4 hours", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.Xorn_Minor] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Xorn_Minor] = BuildData("Claw", damageHelper.BuildEntries("1d3", clawDamageType), string.Empty, 0.5, "melee", 3, FeatConstants.Frequencies.Round, true, true, false, false));

            attackDamages[CreatureConstants.Xorn_Average] = BuildData("Bite", damageHelper.BuildEntries("4d6", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Xorn_Average] = BuildData("Claw", damageHelper.BuildEntries("1d4", clawDamageType), string.Empty, 0.5, "melee", 3, FeatConstants.Frequencies.Round, true, true, false, false));

            attackDamages[CreatureConstants.Xorn_Elder] = BuildData("Bite", damageHelper.BuildEntries("4d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Xorn_Elder] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 3, FeatConstants.Frequencies.Round, true, true, false, false));

            attackDamages[CreatureConstants.YethHound] = BuildData("Bite", damageHelper.BuildEntries("1d8", biteDamageType), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.YethHound] = BuildData("Bay", string.Empty, "panicked 2d4 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
            attackDamages[CreatureConstants.YethHound] = BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

            attackDamages[CreatureConstants.Yrthak] = BuildData("Bite", damageHelper.BuildEntries("2d8", biteDamageType), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Yrthak] = BuildData("Claw", damageHelper.BuildEntries("1d6", clawDamageType), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.Yrthak] = BuildData("Sonic Lance",
                damageHelper.BuildEntries("6d6", FeatConstants.Foci.Elements.Sonic),
                string.Empty, 0, "ranged touch", 1, $"2 {FeatConstants.Frequencies.Round}", false, true, true, false));
            attackDamages[CreatureConstants.Yrthak] = BuildData("Explosion",
                damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0, "supernatural ability", 1, $"2 {FeatConstants.Frequencies.Round}", true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

            attackDamages[CreatureConstants.YuanTi_Pureblood] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.YuanTi_Pureblood] = BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.YuanTi_Pureblood] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.YuanTi_Pureblood] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.YuanTi_Halfblood_SnakeHead] = BuildData(AttributeConstants.Melee,
                string.Empty,
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.YuanTi_Halfblood_SnakeHead] = BuildData(AttributeConstants.Ranged,
                string.Empty,
                string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.YuanTi_Halfblood_SnakeHead] = BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.YuanTi_Halfblood_SnakeHead] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.YuanTi_Halfblood_SnakeHead] = BuildData("Produce Acid",
                damageHelper.BuildEntries(
                    "3d6", FeatConstants.Foci.Elements.Acid, string.Empty,
                    "2d6", FeatConstants.Foci.Elements.Acid, "Grappling or pinning foe"),
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.YuanTi_Halfblood_SnakeHead] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.YuanTi_Halfblood_SnakeArms] = BuildData("Bite",
                damageHelper.BuildEntries("1d4", biteDamageType),
                "Poison", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.YuanTi_Halfblood_SnakeArms] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.YuanTi_Halfblood_SnakeArms] = BuildData("Produce Acid",
                damageHelper.BuildEntries(
                    "3d6", FeatConstants.Foci.Elements.Acid, string.Empty,
                    "2d6", FeatConstants.Foci.Elements.Acid, "Grappling or pinning foe"),
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.YuanTi_Halfblood_SnakeArms] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs] = BuildData(AttributeConstants.Melee,
                string.Empty,
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs] = BuildData(AttributeConstants.Ranged,
                string.Empty,
                string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs] = BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs] = BuildData("Produce Acid",
                damageHelper.BuildEntries(
                    "3d6", FeatConstants.Foci.Elements.Acid, string.Empty,
                    "2d6", FeatConstants.Foci.Elements.Acid, "Grappling or pinning foe"),
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs] = BuildData("Constrict",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            attackDamages[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.YuanTi_Halfblood_SnakeTail] = BuildData(AttributeConstants.Melee,
                string.Empty,
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.YuanTi_Halfblood_SnakeTail] = BuildData(AttributeConstants.Ranged,
                string.Empty,
                string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.YuanTi_Halfblood_SnakeTail] = BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.YuanTi_Halfblood_SnakeTail] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.YuanTi_Halfblood_SnakeTail] = BuildData("Produce Acid",
                damageHelper.BuildEntries(
                    "3d6", FeatConstants.Foci.Elements.Acid, string.Empty,
                    "2d6", FeatConstants.Foci.Elements.Acid, "Grappling or pinning foe"),
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.YuanTi_Halfblood_SnakeTail] = BuildData("Constrict",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            attackDamages[CreatureConstants.YuanTi_Halfblood_SnakeTail] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.YuanTi_Abomination] = BuildData(AttributeConstants.Melee,
                string.Empty,
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.YuanTi_Abomination] = BuildData(AttributeConstants.Ranged,
                string.Empty,
                string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
            attackDamages[CreatureConstants.YuanTi_Abomination] = BuildData("Bite",
                damageHelper.BuildEntries("2d6", biteDamageType),
                "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            attackDamages[CreatureConstants.YuanTi_Abomination] = BuildData("Poison",
                damageHelper.BuildEntries(
                    "1d6", AbilityConstants.Constitution, "Initial",
                    "1d6", AbilityConstants.Constitution, "Secondary"),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.YuanTi_Abomination] = BuildData("Aversion",
                string.Empty,
                "aversion 10 minutes", 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
            attackDamages[CreatureConstants.YuanTi_Abomination] = BuildData("Produce Acid",
                damageHelper.BuildEntries(
                    "3d6", FeatConstants.Foci.Elements.Acid, string.Empty,
                    "2d6", FeatConstants.Foci.Elements.Acid, "Grappling or pinning foe"),
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
            attackDamages[CreatureConstants.YuanTi_Abomination] = BuildData("Constrict",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
            attackDamages[CreatureConstants.YuanTi_Abomination] = BuildData("Improved Grab",
                string.Empty,
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            attackDamages[CreatureConstants.YuanTi_Abomination] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            attackDamages[CreatureConstants.Zelekhut] = BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, false, true, false));
            attackDamages[CreatureConstants.Zelekhut] = BuildData("Unarmed Strike", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            attackDamages[CreatureConstants.Zelekhut] = BuildData("Electrified Weapon",
                damageHelper.BuildEntries("1d6", FeatConstants.Foci.Elements.Electricity),
                string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            attackDamages[CreatureConstants.Zelekhut] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

            foreach (var kvp in attackDamages.SelectMany(d => d))
            {
                testCases[kvp.Key] = kvp.Value;
            }

            return new Dictionary<string, List<string>>(testCases.SelectMany(kvp => kvp.Value));
        }

        public static Dictionary<string, List<string>> GetTemplateDamageData()
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

            testCases[CreatureConstants.Templates.CelestialCreature] = BuildData("Smite Evil",
                string.Empty,
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            testCases[CreatureConstants.Templates.FiendishCreature] = BuildData("Smite Good",
                string.Empty,
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            testCases[CreatureConstants.Templates.Ghost] = BuildData("Corrupting Gaze",
                damageHelper.BuildEntries(
                    "2d10", string.Empty, string.Empty,
                    "1d4", AbilityConstants.Charisma),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true,
                SaveConstants.Fortitude, AbilityConstants.Charisma));
            testCases[CreatureConstants.Templates.Ghost] = BuildData("Corrupting Touch",
                damageHelper.BuildEntries("1d6"),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            testCases[CreatureConstants.Templates.Ghost] = BuildData("Draining Touch",
                damageHelper.BuildEntries("1d4", "Ability points (of ghost's choosing)"),
                "Ghost heals 5 points of damage", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
            testCases[CreatureConstants.Templates.Ghost] = BuildData("Frightful Moan",
                string.Empty,
                "Panic for 2d4 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true,
                SaveConstants.Will, AbilityConstants.Charisma));
            testCases[CreatureConstants.Templates.Ghost] = BuildData("Horrific Appearance",
                damageHelper.BuildEntries(
                    "1d4", AbilityConstants.Strength, string.Empty,
                    "1d4", AbilityConstants.Dexterity, string.Empty,
                    "1d4", AbilityConstants.Constitution),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true,
                SaveConstants.Fortitude, AbilityConstants.Charisma));
            testCases[CreatureConstants.Templates.Ghost] = BuildData("Malevolence",
                string.Empty,
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true,
                SaveConstants.Will, AbilityConstants.Charisma, 5));
            testCases[CreatureConstants.Templates.Ghost] = BuildData("Manifestation",
                string.Empty,
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Templates.Ghost] = BuildData(SpellConstants.Telekinesis,
                string.Empty,
                string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true,
                SaveConstants.Will, AbilityConstants.Charisma));

            testCases[CreatureConstants.Templates.HalfCelestial] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Templates.HalfCelestial] = BuildData("Smite Evil",
                string.Empty,
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            testCases[CreatureConstants.Templates.HalfDragon_Black] = BuildData("Claw",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.HalfDragon_Black] = BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.HalfDragon_Black] = BuildData("Breath Weapon",
                damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Acid),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                SaveConstants.Reflex, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.HalfDragon_Blue] = BuildData("Claw",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.HalfDragon_Blue] = BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.HalfDragon_Blue] = BuildData("Breath Weapon",
                damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Electricity),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                SaveConstants.Reflex, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.HalfDragon_Brass] = BuildData("Claw",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.HalfDragon_Brass] = BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.HalfDragon_Brass] = BuildData("Breath Weapon",
                damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                SaveConstants.Reflex, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.HalfDragon_Bronze] = BuildData("Claw",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.HalfDragon_Bronze] = BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.HalfDragon_Bronze] = BuildData("Breath Weapon",
                damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Electricity),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                SaveConstants.Reflex, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.HalfDragon_Copper] = BuildData("Claw",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.HalfDragon_Copper] = BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.HalfDragon_Copper] = BuildData("Breath Weapon",
                damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Acid),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                SaveConstants.Reflex, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.HalfDragon_Gold] = BuildData("Claw",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.HalfDragon_Gold] = BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.HalfDragon_Gold] = BuildData("Breath Weapon",
                damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                SaveConstants.Reflex, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.HalfDragon_Green] = BuildData("Claw",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.HalfDragon_Green] = BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.HalfDragon_Green] = BuildData("Breath Weapon",
                damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Acid, "Gas"),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                SaveConstants.Reflex, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.HalfDragon_Red] = BuildData("Claw",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.HalfDragon_Red] = BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.HalfDragon_Red] = BuildData("Breath Weapon",
                damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Fire),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                SaveConstants.Reflex, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.HalfDragon_Silver] = BuildData("Claw",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.HalfDragon_Silver] = BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.HalfDragon_Silver] = BuildData("Breath Weapon",
                damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Cold),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                SaveConstants.Reflex, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.HalfDragon_White] = BuildData("Claw",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.HalfDragon_White] = BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.HalfDragon_White] = BuildData("Breath Weapon",
                damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Cold),
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                SaveConstants.Reflex, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.HalfFiend] = BuildData("Claw",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.HalfFiend] = BuildData("Bite",
                damageHelper.BuildEntries("1d6", biteDamageType),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.HalfFiend] = BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                string.Empty,
                string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Templates.HalfFiend] = BuildData("Smite Good",
                string.Empty,
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

            testCases[CreatureConstants.Templates.Lich] = BuildData("Touch",
                damageHelper.BuildEntries("1d8+5"),
                "Paralyzing Touch", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false, SaveConstants.Will, AbilityConstants.Charisma));
            testCases[CreatureConstants.Templates.Lich] = BuildData("Fear Aura",
                string.Empty,
                "Fear", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Will, AbilityConstants.Charisma));
            testCases[CreatureConstants.Templates.Lich] = BuildData("Paralyzing Touch",
                string.Empty,
                "Paralyzed", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Charisma));

            testCases[CreatureConstants.Templates.None] = new[] { None });

            testCases[CreatureConstants.Templates.Skeleton] = BuildData("Claw",
                damageHelper.BuildEntries("1d4", clawDamageType),
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Templates.Vampire] = BuildData("Slam",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Vampire] = BuildData("Blood Drain",
                damageHelper.BuildEntries("1d4", AbilityConstants.Constitution),
                "Vampire gains 5 temporary hit points", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Templates.Vampire] = BuildData("Children of the Night",
                string.Empty,
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
            testCases[CreatureConstants.Templates.Vampire] = BuildData("Dominate",
                string.Empty,
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
            testCases[CreatureConstants.Templates.Vampire] = BuildData("Create Spawn",
                string.Empty,
                string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
            testCases[CreatureConstants.Templates.Vampire] = BuildData("Energy Drain",
                damageHelper.BuildEntries("2", "Negative Level"),
                "Vampire gains 5 temporary hit points", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural] = BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural] = BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural] = BuildData("Curse of Lycanthropy",
                string.Empty,
                "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true,
                SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Natural] = BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Natural] = BuildData("Gore (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Natural] = BuildData("Curse of Lycanthropy",
                string.Empty,
                "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural] = BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural] = BuildData("Gore (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural] = BuildData("Curse of Lycanthropy",
                string.Empty,
                "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Natural] = BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Natural] = BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Natural] = BuildData("Curse of Lycanthropy",
                string.Empty,
                "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural] = BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural] = BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural] = BuildData("Curse of Lycanthropy",
                string.Empty,
                "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Natural] = BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Natural] = BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Natural] = BuildData("Curse of Lycanthropy",
                string.Empty,
                "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural] = BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural] = BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural] = BuildData("Curse of Lycanthropy",
                string.Empty,
                "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Natural] = BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Natural] = BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Natural] = BuildData("Curse of Lycanthropy",
                string.Empty,
                "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural] = BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural] = BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural] = BuildData("Curse of Lycanthropy",
                string.Empty,
                "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted] = BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted] = BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Afflicted] = BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Afflicted] = BuildData("Gore (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted] = BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted] = BuildData("Gore (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Afflicted] = BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Afflicted] = BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted] = BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted] = BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted] = BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted] = BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted] = BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted] = BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted] = BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted] = BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted] = BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted] = BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Templates.Zombie] = BuildData("Slam",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Black_Afflicted] = BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Black_Afflicted] = BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural] = BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural] = BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural] = BuildData("Curse of Lycanthropy",
                string.Empty,
                "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true,
                SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Dire_Afflicted] = BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Dire_Afflicted] = BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural] = BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural] = BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural] = BuildData("Curse of Lycanthropy",
                string.Empty,
                "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true,
                SaveConstants.Fortitude, AbilityConstants.Constitution));

            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Polar_Afflicted] = BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Polar_Afflicted] = BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural] = BuildData("Claw (in Hybrid form)",
                damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural] = BuildData("Bite (in Hybrid form)",
                damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
            testCases[CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural] = BuildData("Curse of Lycanthropy",
                string.Empty,
                "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true,
                SaveConstants.Fortitude, AbilityConstants.Constitution));

            return testCases;
        }

        private static Dictionary<string, List<string>> BuildDamageDataForAttack(
            string creature,
            CreatureDataSelection creatureData,
            AttackDataSelection[] attacks,
            IEnumerable<AdvancementDataSelection> advancements,
            string roll,
            string type = "",
            string condition = "",
            string roll2 = null,
            string type2 = null,
            string condition2 = null)
        {
            var data = new Dictionary<string, List<string>>();

            foreach (var attack in attacks)
            {
                var key = attack.BuildDamageKey(creature, creatureData.Size);
                data[key] = [BuildData(roll, type, condition)];

                if (roll2 != null)
                    data[key].Add(BuildData(roll2, type2, condition2));

                var advancedSizes = advancements.Select(a => a.Size).Except([creatureData.Size]);
                foreach (var size in advancedSizes)
                {
                    key = attack.BuildDamageKey(creature, size);
                    var advancedRoll = roll;
                    var advancedRoll2 = roll2;

                    if (attack.IsNatural)
                    {
                        advancedRoll = GetAdjustedDamage(roll, creatureData.Size, size);
                        advancedRoll2 = GetAdjustedDamage(roll2, creatureData.Size, size);
                    }

                    data[key] = [BuildData(advancedRoll, type, condition)];

                    if (advancedRoll2 != null)
                        data[key].Add(BuildData(advancedRoll2, type2, condition2));
                }
            }

            return data;
        }

        private static string GetAdjustedDamage(string originalDamage, string originalSize, string advancedSize)
        {
            if (string.IsNullOrEmpty(originalDamage))
                return originalDamage;

            var damageMaps = new Dictionary<string, string>
            {
                ["2d8"] = "3d8",
                ["2d6"] = "3d6",
                ["1d10"] = "2d8",
                ["1d8"] = "2d6",
                ["1d6"] = "1d8",
                ["1d4"] = "1d6",
                ["1d3"] = "1d4",
                ["1d2"] = "1d3"
            };
            var orderedSizes = SizeConstants.GetOrdered();

            var adjustedDamage = originalDamage;
            var sizeDifference = Array.IndexOf(orderedSizes, advancedSize) - Array.IndexOf(orderedSizes, originalSize);

            while (sizeDifference-- > 0 && damageMaps.ContainsKey(adjustedDamage))
            {
                adjustedDamage = damageMaps[adjustedDamage];
            }

            return adjustedDamage;
        }

        private static string BuildData(string roll, string type, string condition = "")
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
