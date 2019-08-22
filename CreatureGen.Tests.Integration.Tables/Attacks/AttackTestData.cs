using CreatureGen.Abilities;
using CreatureGen.Creatures;
using CreatureGen.Defenses;
using CreatureGen.Feats;
using CreatureGen.Magic;
using CreatureGen.Selectors.Helpers;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using TreasureGen.Items;

namespace CreatureGen.Tests.Integration.Tables.Attacks
{
    public class AttackTestData
    {
        public const string None = "NONE";

        public static IEnumerable Creatures
        {
            get
            {
                var testCases = new Dictionary<string, List<string[]>>();
                var creatures = CreatureConstants.All();

                foreach (var creature in creatures)
                {
                    testCases[creature] = new List<string[]>();
                }

                testCases[CreatureConstants.Aasimar].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Aasimar].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Aasimar].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Aboleth].Add(AttackHelper.BuildData("Tentacle", $"1d6", "Slime", 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Aboleth].Add(AttackHelper.BuildData("Enslave", string.Empty, string.Empty, 0, "spell", 3, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma, 14));
                testCases[CreatureConstants.Aboleth].Add(AttackHelper.BuildData("Slime", string.Empty, string.Empty, 0, "melee", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution, 14));
                testCases[CreatureConstants.Aboleth].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Achaierai].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Achaierai].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Achaierai].Add(AttackHelper.BuildData("Black cloud", "2d6", "insanity", 0, "special", 3, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution, 13));

                testCases[CreatureConstants.Allip].Add(AttackHelper.BuildData("Incorporeal touch", string.Empty, $"1d4 Wisdom drain", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Allip].Add(AttackHelper.BuildData("Babble", string.Empty, string.Empty, 0, "special", 1, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Will, AbilityConstants.Charisma, 12));
                testCases[CreatureConstants.Allip].Add(AttackHelper.BuildData("Madness", string.Empty, "1d4 Wisdom drain", 0, "special", 1, FeatConstants.Frequencies.Round, false, true, false, true));
                testCases[CreatureConstants.Allip].Add(AttackHelper.BuildData("Wisdom drain", "1d4", string.Empty, 0, "special", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Androsphinx].Add(AttackHelper.BuildData("Claw", $"2d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Androsphinx].Add(AttackHelper.BuildData("Pounce", string.Empty, string.Empty, 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Androsphinx].Add(AttackHelper.BuildData("Rake", $"2d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Androsphinx].Add(AttackHelper.BuildData("Roar", string.Empty, string.Empty, 0, "ranged", 3, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma, 16));
                testCases[CreatureConstants.Androsphinx].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Angel_AstralDeva].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Angel_AstralDeva].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Angel_AstralDeva].Add(AttackHelper.BuildData("Stun", string.Empty, string.Empty, 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Angel_AstralDeva].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Angel_Planetar].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Angel_Planetar].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Angel_Planetar].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Angel_Planetar].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Angel_Solar].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Angel_Solar].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Angel_Solar].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Angel_Solar].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Angel_Solar].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Anvil_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Anvil_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Anvil_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Anvil_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Anvil_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Anvil_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Anvil_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Anvil_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Anvil_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Anvil_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Anvil_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Stone_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Stone_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Stone_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Stone_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Wood_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Wood_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Wood_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Wood_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Box_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Box_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Box_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Box_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Box_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Box_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Box_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Box_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Box_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Box_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Box_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Tiny].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Tiny].Add(AttackHelper.BuildData("Constrict", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Small].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Small].Add(AttackHelper.BuildData("Constrict", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Medium].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Medium].Add(AttackHelper.BuildData("Constrict", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Large].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Large].Add(AttackHelper.BuildData("Constrict", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Huge].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Huge].Add(AttackHelper.BuildData("Constrict", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Gargantuan].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Gargantuan].Add(AttackHelper.BuildData("Constrict", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Colossal].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Colossal].Add(AttackHelper.BuildData("Constrict", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carriage_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Carriage_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Carriage_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Carriage_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carriage_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carriage_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carriage_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carriage_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carriage_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carriage_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carriage_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Tiny].Add(AttackHelper.BuildData("Constrict", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Small].Add(AttackHelper.BuildData("Constrict", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Medium].Add(AttackHelper.BuildData("Constrict", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Large].Add(AttackHelper.BuildData("Constrict", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Chain_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Huge].Add(AttackHelper.BuildData("Constrict", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Chain_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Gargantuan].Add(AttackHelper.BuildData("Constrict", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Chain_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Colossal].Add(AttackHelper.BuildData("Constrict", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Chain_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chair_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Chair_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Chair_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Chair_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chair_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chair_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chair_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chair_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chair_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chair_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chair_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Tiny].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Tiny].Add(AttackHelper.BuildData("Constrict", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Small].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Small].Add(AttackHelper.BuildData("Constrict", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Medium].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Medium].Add(AttackHelper.BuildData("Constrict", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Large].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Large].Add(AttackHelper.BuildData("Constrict", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Huge].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Huge].Add(AttackHelper.BuildData("Constrict", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Gargantuan].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Gargantuan].Add(AttackHelper.BuildData("Constrict", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Colossal].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Colossal].Add(AttackHelper.BuildData("Constrict", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Ladder_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Ladder_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Ladder_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Ladder_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Ladder_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Ladder_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Ladder_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Ladder_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Ladder_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Ladder_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Ladder_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Tiny].Add(AttackHelper.BuildData("Constrict", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Small].Add(AttackHelper.BuildData("Constrict", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Medium].Add(AttackHelper.BuildData("Constrict", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Large].Add(AttackHelper.BuildData("Constrict", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Huge].Add(AttackHelper.BuildData("Constrict", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Gargantuan].Add(AttackHelper.BuildData("Constrict", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Colossal].Add(AttackHelper.BuildData("Constrict", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Tiny].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Tiny].Add(AttackHelper.BuildData("Constrict", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Small].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Small].Add(AttackHelper.BuildData("Constrict", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Medium].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Medium].Add(AttackHelper.BuildData("Constrict", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Large].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Large].Add(AttackHelper.BuildData("Constrict", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Huge].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Huge].Add(AttackHelper.BuildData("Constrict", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Gargantuan].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Gargantuan].Add(AttackHelper.BuildData("Constrict", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Colossal].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Colossal].Add(AttackHelper.BuildData("Constrict", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Sled_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Sled_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Sled_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Sled_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Sled_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Sled_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Sled_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Sled_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Sled_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Sled_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Sled_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Stool_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Stool_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Stool_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Stool_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Stool_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Stool_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Stool_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Stool_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Stool_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Stool_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Stool_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Table_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Table_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Table_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Table_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Table_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Table_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Table_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Table_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Table_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Table_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Table_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Tiny].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Tiny].Add(AttackHelper.BuildData("Constrict", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Small].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Small].Add(AttackHelper.BuildData("Constrict", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Medium].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Medium].Add(AttackHelper.BuildData("Constrict", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Large].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Large].Add(AttackHelper.BuildData("Constrict", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Huge].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Huge].Add(AttackHelper.BuildData("Constrict", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Gargantuan].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Gargantuan].Add(AttackHelper.BuildData("Constrict", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Colossal].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Colossal].Add(AttackHelper.BuildData("Constrict", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Wagon_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Wagon_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Wagon_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Wagon_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Wagon_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Wagon_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Wagon_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Wagon_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Wagon_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Wagon_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Wagon_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Ankheg].Add(AttackHelper.BuildData("Bite", $"2d6 + 1d4 acid", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ankheg].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Ankheg].Add(AttackHelper.BuildData("Spit Acid", $"4d4 acid", string.Empty, 0, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Annis].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Annis].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Annis].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Annis].Add(AttackHelper.BuildData("Rake", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Annis].Add(AttackHelper.BuildData("Rend", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Annis].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Ant_Giant_Worker].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ant_Giant_Worker].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Ant_Giant_Soldier].Add(AttackHelper.BuildData("Bite", $"2d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ant_Giant_Soldier].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Ant_Giant_Soldier].Add(AttackHelper.BuildData("Acid Sting", "1d4 Piercing damage + 1d4 acid damage", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Ant_Giant_Queen].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ant_Giant_Queen].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Ape].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ape].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Ape_Dire].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ape_Dire].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Ape_Dire].Add(AttackHelper.BuildData("Rend", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Aranea].Add(AttackHelper.BuildData("Bite", $"1d6", "poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Aranea].Add(AttackHelper.BuildData("Poison", "Initial damage 1d6 Str, Secondary damage 2d6 Str", string.Empty, 0, "melee", 1, FeatConstants.Frequencies.Round, false, true, false, true));
                testCases[CreatureConstants.Aranea].Add(AttackHelper.BuildData("Web", string.Empty, string.Empty, 0, "ranged", 6, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.Aranea].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Arrowhawk_Juvenile].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Arrowhawk_Juvenile].Add(AttackHelper.BuildData("Electricity ray", $"2d6", string.Empty, 0, "ranged touch", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Arrowhawk_Adult].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Arrowhawk_Adult].Add(AttackHelper.BuildData("Electricity ray", $"2d8", string.Empty, 0, "ranged touch", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Arrowhawk_Elder].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Arrowhawk_Elder].Add(AttackHelper.BuildData("Electricity ray", $"2d8", string.Empty, 0, "ranged touch", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.AssassinVine].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AssassinVine].Add(AttackHelper.BuildData("Constrict", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AssassinVine].Add(AttackHelper.BuildData("Entangle", string.Empty, string.Empty, 0, "melee", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.AssassinVine].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Athach].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Athach].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, false, false, false));
                testCases[CreatureConstants.Athach].Add(AttackHelper.BuildData("Bite", $"2d8", "poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Athach].Add(AttackHelper.BuildData("Rock", $"2d8", string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Athach].Add(AttackHelper.BuildData("Rock", $"2d8", string.Empty, 0.5, "ranged", 2, FeatConstants.Frequencies.Round, false, true, false, false));
                testCases[CreatureConstants.Athach].Add(AttackHelper.BuildData("Poison", $"Initial damage 1d6 Str, Secondary damage 2d6 Str", string.Empty, 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Avoral].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Avoral].Add(AttackHelper.BuildData("Wing", $"2d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Avoral].Add(AttackHelper.BuildData("Fear Aura", string.Empty, string.Empty, 0, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Avoral].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, false, true, true, true));

                testCases[CreatureConstants.Azer].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, true, false, true, false));
                testCases[CreatureConstants.Azer].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, false, false, true, false));
                testCases[CreatureConstants.Azer].Add(AttackHelper.BuildData("Heat", "1 fire", true, true, false, true));

                testCases[CreatureConstants.Babau].Add(AttackHelper.BuildData("Claw", $"1d6", true, true, true, false));
                testCases[CreatureConstants.Babau].Add(AttackHelper.BuildData("Claw", $"1d6", true, true, true, false));
                testCases[CreatureConstants.Babau].Add(AttackHelper.BuildData("Bite", $"1d6", true, true, false, false));
                testCases[CreatureConstants.Babau].Add(AttackHelper.BuildData("Sneak Attack", $"2d6", true, true, false, true));
                testCases[CreatureConstants.Babau].Add(AttackHelper.BuildData("Summon Demon", string.Empty, false, true, true, true));
                testCases[CreatureConstants.Babau].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, false, true, true, true));

                testCases[CreatureConstants.Baboon].Add(AttackHelper.BuildData("Bite", $"1d6", true, true, true, false));

                testCases[CreatureConstants.Badger].Add(AttackHelper.BuildData("Claw", $"1d2", true, true, true, false));
                testCases[CreatureConstants.Badger].Add(AttackHelper.BuildData("Claw", $"1d2", true, true, true, false));
                testCases[CreatureConstants.Badger].Add(AttackHelper.BuildData("Bite", $"1d3", true, true, false, false));
                testCases[CreatureConstants.Badger].Add(AttackHelper.BuildData("Rage", string.Empty, false, true, true, true));

                testCases[CreatureConstants.Badger_Dire].Add(AttackHelper.BuildData("Claw", $"1d4", true, true, true, false));
                testCases[CreatureConstants.Badger_Dire].Add(AttackHelper.BuildData("Claw", $"1d4", true, true, true, false));
                testCases[CreatureConstants.Badger_Dire].Add(AttackHelper.BuildData("Bite", $"1d6", true, true, false, false));
                testCases[CreatureConstants.Badger_Dire].Add(AttackHelper.BuildData("Rage", string.Empty, false, true, true, true));

                testCases[CreatureConstants.Balor].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, true, false, true, false));
                testCases[CreatureConstants.Balor].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, true, false, false, false));
                testCases[CreatureConstants.Balor].Add(AttackHelper.BuildData("Slam", $"1d10", true, true, true, false));
                testCases[CreatureConstants.Balor].Add(AttackHelper.BuildData("Slam", $"1d10", true, true, true, false));
                testCases[CreatureConstants.Balor].Add(AttackHelper.BuildData("Death Throes", "100", false, true, false, true));
                testCases[CreatureConstants.Balor].Add(AttackHelper.BuildData("Entangle", string.Empty, true, false, false, true));
                testCases[CreatureConstants.Balor].Add(AttackHelper.BuildData("Summon Demon", string.Empty, false, true, true, true));
                testCases[CreatureConstants.Balor].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, false, true, true, true));

                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(AttackHelper.BuildData("Claw", $"2d8 + fear", true, true, true, false));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(AttackHelper.BuildData("Claw", $"2d8 + fear", true, true, true, false));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(AttackHelper.BuildData("Fear", string.Empty, true, true, true, true));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(AttackHelper.BuildData("Improved Grab", string.Empty, true, true, false, true));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(AttackHelper.BuildData("Impale", "3d8+9", true, true, false, true));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(AttackHelper.BuildData("Summon Devil", string.Empty, false, true, true, true));

                testCases[CreatureConstants.Barghest].Add(AttackHelper.BuildData("Bite", $"1d6", true, true, true, false));
                testCases[CreatureConstants.Barghest].Add(AttackHelper.BuildData("Claw", $"1d4", true, true, false, false));
                testCases[CreatureConstants.Barghest].Add(AttackHelper.BuildData("Claw", $"1d4", true, true, false, false));
                testCases[CreatureConstants.Barghest].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, false, true, true, true));
                testCases[CreatureConstants.Barghest].Add(AttackHelper.BuildData("Feed", string.Empty, true, true, true, true));

                testCases[CreatureConstants.Barghest_Greater].Add(AttackHelper.BuildData("Bite", $"1d8", true, true, true, false));
                testCases[CreatureConstants.Barghest_Greater].Add(AttackHelper.BuildData("Claw", $"1d6", true, true, false, false));
                testCases[CreatureConstants.Barghest_Greater].Add(AttackHelper.BuildData("Claw", $"1d6", true, true, false, false));
                testCases[CreatureConstants.Barghest_Greater].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, false, true, true, true));
                testCases[CreatureConstants.Barghest_Greater].Add(AttackHelper.BuildData("Feed", string.Empty, true, true, true, true));

                testCases[CreatureConstants.Basilisk].Add(AttackHelper.BuildData("Bite", $"1d8", true, true, true, false));
                testCases[CreatureConstants.Basilisk].Add(AttackHelper.BuildData("Petrifying Gaze", string.Empty, false, true, true, true));

                testCases[CreatureConstants.Basilisk_AbyssalGreater].Add(AttackHelper.BuildData("Bite", $"2d8", true, true, true, false));
                testCases[CreatureConstants.Basilisk_AbyssalGreater].Add(AttackHelper.BuildData("Petrifying Gaze", string.Empty, false, true, true, true));
                testCases[CreatureConstants.Basilisk_AbyssalGreater].Add(AttackHelper.BuildData("Smite Good", string.Empty, true, true, true, true));

                //Bats have no attacks

                testCases[CreatureConstants.Bat_Dire].Add(AttackHelper.BuildData("Bite", $"1d8", true, true, true, false));

                testCases[CreatureConstants.Bat_Swarm].Add(AttackHelper.BuildData("Swarm", $"1d6", true, true, true, false));
                testCases[CreatureConstants.Bat_Swarm].Add(AttackHelper.BuildData("Distraction", string.Empty, true, true, true, true));
                testCases[CreatureConstants.Bat_Swarm].Add(AttackHelper.BuildData("Wounding", string.Empty, true, true, true, true));

                testCases[CreatureConstants.Bear_Black].Add(AttackHelper.BuildData("Claw", $"1d4", true, true, true, false));
                testCases[CreatureConstants.Bear_Black].Add(AttackHelper.BuildData("Claw", $"1d4", true, true, true, false));
                testCases[CreatureConstants.Bear_Black].Add(AttackHelper.BuildData("Bite", $"1d6", true, true, false, false));

                testCases[CreatureConstants.Bear_Brown].Add(AttackHelper.BuildData("Claw", $"1d8", true, true, true, false));
                testCases[CreatureConstants.Bear_Brown].Add(AttackHelper.BuildData("Claw", $"1d8", true, true, true, false));
                testCases[CreatureConstants.Bear_Brown].Add(AttackHelper.BuildData("Bite", $"2d6", true, true, false, false));
                testCases[CreatureConstants.Bear_Brown].Add(AttackHelper.BuildData("Improved Grab", string.Empty, true, true, false, true));

                testCases[CreatureConstants.Bear_Dire].Add(AttackHelper.BuildData("Claw", $"2d4", true, true, true, false));
                testCases[CreatureConstants.Bear_Dire].Add(AttackHelper.BuildData("Claw", $"2d4", true, true, true, false));
                testCases[CreatureConstants.Bear_Dire].Add(AttackHelper.BuildData("Bite", $"2d8", true, true, false, false));
                testCases[CreatureConstants.Bear_Dire].Add(AttackHelper.BuildData("Improved Grab", string.Empty, true, true, false, true));

                testCases[CreatureConstants.Bear_Polar].Add(AttackHelper.BuildData("Claw", $"1d8", true, true, true, false));
                testCases[CreatureConstants.Bear_Polar].Add(AttackHelper.BuildData("Claw", $"1d8", true, true, true, false));
                testCases[CreatureConstants.Bear_Polar].Add(AttackHelper.BuildData("Bite", $"2d6", true, true, false, false));
                testCases[CreatureConstants.Bear_Polar].Add(AttackHelper.BuildData("Improved Grab", string.Empty, true, true, false, true));

                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, true, false, true, false));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(AttackHelper.BuildData("Claw", $"2d8", true, true, true, false));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(AttackHelper.BuildData("Claw", $"2d8", true, true, true, false));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(AttackHelper.BuildData("Infernal Wound", string.Empty, true, true, false, true));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(AttackHelper.BuildData("Beard", "1d8+2 + disease", true, true, false, true));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(AttackHelper.BuildData("Battle Frenzy", string.Empty, false, true, true, true));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(AttackHelper.BuildData("Summon Devil", string.Empty, false, true, true, true));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(AttackHelper.BuildData("Disease", "Devil Chills", true, true, false, true));

                testCases[CreatureConstants.Bebilith].Add(AttackHelper.BuildData("Bite", $"2d6 + poison", true, true, true, false));
                testCases[CreatureConstants.Bebilith].Add(AttackHelper.BuildData("Claw", $"2d6", true, true, false, false));
                testCases[CreatureConstants.Bebilith].Add(AttackHelper.BuildData("Claw", $"2d6", true, true, false, false));
                testCases[CreatureConstants.Bebilith].Add(AttackHelper.BuildData("Web", string.Empty, false, true, true, true));
                testCases[CreatureConstants.Bebilith].Add(AttackHelper.BuildData("Poison", "Initial damage 1d6 Con, Secondary damage 2d6 Con", true, true, false, true));
                testCases[CreatureConstants.Bebilith].Add(AttackHelper.BuildData("Rend Armor", $"4d6+18", true, true, false, true));

                testCases[CreatureConstants.Bee_Giant].Add(AttackHelper.BuildData("Sting", $"1d4 + poison", true, true, true, false));
                testCases[CreatureConstants.Bee_Giant].Add(AttackHelper.BuildData("Poison", "Initial and secondary damage 1d4 Con", true, true, false, true));

                testCases[CreatureConstants.Behir].Add(AttackHelper.BuildData("Bite", $"2d4", true, true, true, false));
                testCases[CreatureConstants.Behir].Add(AttackHelper.BuildData("Breath Weapon", "7d6 electricity", false, true, true, true));
                testCases[CreatureConstants.Behir].Add(AttackHelper.BuildData("Constrict", "2d8+8", true, true, false, true));
                testCases[CreatureConstants.Behir].Add(AttackHelper.BuildData("Improved Grab", string.Empty, true, true, false, true));
                testCases[CreatureConstants.Behir].Add(AttackHelper.BuildData("Rake", "1d4+4", true, true, false, true));
                testCases[CreatureConstants.Behir].Add(AttackHelper.BuildData("Rake", "1d4+4", true, true, false, true));
                testCases[CreatureConstants.Behir].Add(AttackHelper.BuildData("Rake", "1d4+4", true, true, false, true));
                testCases[CreatureConstants.Behir].Add(AttackHelper.BuildData("Rake", "1d4+4", true, true, false, true));
                testCases[CreatureConstants.Behir].Add(AttackHelper.BuildData("Rake", "1d4+4", true, true, false, true));
                testCases[CreatureConstants.Behir].Add(AttackHelper.BuildData("Rake", "1d4+4", true, true, false, true));
                testCases[CreatureConstants.Behir].Add(AttackHelper.BuildData("Swallow Whole", string.Empty, true, true, false, true));

                testCases[CreatureConstants.Beholder].Add(AttackHelper.BuildData("Bite", $"2d4", true, true, false, false));
                testCases[CreatureConstants.Beholder].Add(AttackHelper.BuildData("Eye ray", SpellConstants.CharmMonster, false, true, true, true));
                testCases[CreatureConstants.Beholder].Add(AttackHelper.BuildData("Eye ray", SpellConstants.CharmPerson, false, true, true, true));
                testCases[CreatureConstants.Beholder].Add(AttackHelper.BuildData("Eye ray", SpellConstants.CureInflictModerateWounds, false, true, true, true));
                testCases[CreatureConstants.Beholder].Add(AttackHelper.BuildData("Eye ray", SpellConstants.Disintegrate, false, true, true, true));
                testCases[CreatureConstants.Beholder].Add(AttackHelper.BuildData("Eye ray", SpellConstants.Fear, false, true, true, true));
                testCases[CreatureConstants.Beholder].Add(AttackHelper.BuildData("Eye ray", SpellConstants.FingerOfDeath, false, true, true, true));
                testCases[CreatureConstants.Beholder].Add(AttackHelper.BuildData("Eye ray", SpellConstants.FleshToStone, false, true, true, true));
                testCases[CreatureConstants.Beholder].Add(AttackHelper.BuildData("Eye ray", SpellConstants.Sleep, false, true, true, true));
                testCases[CreatureConstants.Beholder].Add(AttackHelper.BuildData("Eye ray", SpellConstants.Slow, false, true, true, true));
                testCases[CreatureConstants.Beholder].Add(AttackHelper.BuildData("Eye ray", SpellConstants.Telekinesis, false, true, true, true));

                testCases[CreatureConstants.Beholder_Gauth].Add(AttackHelper.BuildData("Bite", $"1d6", true, true, false, false));
                testCases[CreatureConstants.Beholder_Gauth].Add(AttackHelper.BuildData("Eye ray", SpellConstants.Sleep, false, true, true, true));
                testCases[CreatureConstants.Beholder_Gauth].Add(AttackHelper.BuildData("Eye ray", SpellConstants.CureInflictModerateWounds, false, true, true, true));
                testCases[CreatureConstants.Beholder_Gauth].Add(AttackHelper.BuildData("Eye ray", SpellConstants.DispelMagic, false, true, true, true));
                testCases[CreatureConstants.Beholder_Gauth].Add(AttackHelper.BuildData("Eye ray", SpellConstants.ScorchingRay, false, true, true, true));
                testCases[CreatureConstants.Beholder_Gauth].Add(AttackHelper.BuildData("Eye ray", "Paralysis", false, true, true, true));
                testCases[CreatureConstants.Beholder_Gauth].Add(AttackHelper.BuildData("Eye ray", SpellConstants.RayOfExhaustion, false, true, true, true));

                testCases[CreatureConstants.Belker].Add(AttackHelper.BuildData("Wing", $"1d6", true, true, true, false));
                testCases[CreatureConstants.Belker].Add(AttackHelper.BuildData("Wing", $"1d6", true, true, true, false));
                testCases[CreatureConstants.Belker].Add(AttackHelper.BuildData("Bite", $"1d4", true, true, false, false));
                testCases[CreatureConstants.Belker].Add(AttackHelper.BuildData("Claw", $"1d3", true, true, false, false));
                testCases[CreatureConstants.Belker].Add(AttackHelper.BuildData("Claw", $"1d3", true, true, false, false));
                testCases[CreatureConstants.Belker].Add(AttackHelper.BuildData("Smoke Claw", $"3d4", true, true, true, true));

                //testCases[CreatureConstants.Bison].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.BlackPudding].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Split));

                //testCases[CreatureConstants.BlackPudding_Elder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Split));

                //testCases[CreatureConstants.BlinkDog].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blink, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.BlinkDog].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionDoor, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.BlinkDog].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.BlinkDog].Add(AttackHelper.BuildData(FeatConstants.Track));

                //testCases[CreatureConstants.Boar].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Boar_Dire].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Bodak].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: "Sunlight"));
                //testCases[CreatureConstants.Bodak].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Bodak].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Bodak].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Bodak].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                //testCases[CreatureConstants.BombardierBeetle_Giant].Add(AttackHelper.BuildData(None));

                testCases[CreatureConstants.BoneDevil_Osyluth].Add(AttackHelper.BuildData("Bite", $"1d8", true, true, true, false));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(AttackHelper.BuildData("Claw", $"1d4", true, true, false, false));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(AttackHelper.BuildData("Claw", $"1d4", true, true, false, false));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(AttackHelper.BuildData("Sting", $"3d4 + poison", true, true, false, false));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, false, true, true, true));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(AttackHelper.BuildData("Fear Aura", string.Empty, false, true, true, true));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(AttackHelper.BuildData("Poison", "Initial damage 1d6 Str, Secondary damage 2d6 Str", false, true, true, true));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(AttackHelper.BuildData("Summon Devil", string.Empty, false, true, true, true));

                //testCases[CreatureConstants.Bralani].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                //testCases[CreatureConstants.Bralani].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Humanoid or whirlwind form", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Bralani].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron or evil weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Bralani].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Bralani].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
                //testCases[CreatureConstants.Bralani].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Bralani].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Bralani].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 17));
                //testCases[CreatureConstants.Bralani].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Tongues, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                //testCases[CreatureConstants.Bralani].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blur, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Bralani].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmPerson, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Bralani].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Bralani].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MirrorImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Bralani].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WindWall, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Bralani].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LightningBolt, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Bralani].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureInflictSeriousWounds, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Bralani].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Scimitar, requiresEquipment: true));
                //testCases[CreatureConstants.Bralani].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));

                //testCases[CreatureConstants.Bugbear].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                //testCases[CreatureConstants.Bugbear].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Bugbear].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Morningstar, requiresEquipment: true));
                //testCases[CreatureConstants.Bugbear].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
                //testCases[CreatureConstants.Bugbear].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                //testCases[CreatureConstants.Bulette].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Bulette].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                //testCases[CreatureConstants.Camel_Bactrian].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Camel_Dromedary].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.CarrionCrawler].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Cat].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Centaur].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
                //testCases[CreatureConstants.Centaur].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
                //testCases[CreatureConstants.Centaur].Add(AttackHelper.BuildData(FeatConstants.MountedCombat));

                //testCases[CreatureConstants.Centipede_Monstrous_Tiny].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Centipede_Monstrous_Small].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Centipede_Monstrous_Medium].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Centipede_Monstrous_Large].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Centipede_Monstrous_Huge].Add(AttackHelper.BuildData(None));

                //testCases[CreatureConstants.Centipede_Monstrous_Gargantuan].Add(AttackHelper.BuildData(None));

                //testCases[CreatureConstants.Centipede_Monstrous_Colossal].Add(AttackHelper.BuildData(None));

                testCases[CreatureConstants.Centipede_Swarm].Add(AttackHelper.BuildData("Swarm", $"2d6 + poison", true, true, true, false));
                testCases[CreatureConstants.Centipede_Swarm].Add(AttackHelper.BuildData("Distraction", string.Empty, true, true, true, true));
                testCases[CreatureConstants.Centipede_Swarm].Add(AttackHelper.BuildData("Poison", "Initial and secondary damage 1d4 Dex", true, true, true, true));

                testCases[CreatureConstants.ChainDevil_Kyton].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, true, true, true, false));
                testCases[CreatureConstants.ChainDevil_Kyton].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, true, true, true, false));
                testCases[CreatureConstants.ChainDevil_Kyton].Add(AttackHelper.BuildData("Dancing Chains", string.Empty, false, true, true, true));
                testCases[CreatureConstants.ChainDevil_Kyton].Add(AttackHelper.BuildData("Unnerving Gaze", string.Empty, false, true, true, true));

                //testCases[CreatureConstants.ChaosBeast].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Critical hits"));
                //testCases[CreatureConstants.ChaosBeast].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Transformation"));
                //testCases[CreatureConstants.ChaosBeast].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 15));

                //testCases[CreatureConstants.Cheetah].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Cheetah].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Sprint, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));

                //testCases[CreatureConstants.Chimera].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Choker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Quickness));
                //testCases[CreatureConstants.Choker].Add(AttackHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));

                //testCases[CreatureConstants.Chuul].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Amphibious));
                //testCases[CreatureConstants.Chuul].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));

                //testCases[CreatureConstants.Cloaker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.ShadowShift));

                //testCases[CreatureConstants.Cockatrice].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Couatl].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Any Small or Medium humanoid", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Couatl].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EtherealJaunt, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Couatl].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 90));

                testCases[CreatureConstants.Criosphinx].Add(AttackHelper.BuildData("Gore", $"2d6", "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Criosphinx].Add(AttackHelper.BuildData("Claw", $"1d6", "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Criosphinx].Add(AttackHelper.BuildData("Pounce", string.Empty, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Criosphinx].Add(AttackHelper.BuildData("Rake", $"1d6", "melee", 2, FeatConstants.Frequencies.Round, true, true, false, true));

                //testCases[CreatureConstants.Crocodile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.HoldBreath));

                //testCases[CreatureConstants.Crocodile_Giant].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.HoldBreath));

                //testCases[CreatureConstants.Cryohydra_5Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Cryohydra_5Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Cryohydra_5Heads].Add(AttackHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                //testCases[CreatureConstants.Cryohydra_6Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 16, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Cryohydra_6Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Cryohydra_6Heads].Add(AttackHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                //testCases[CreatureConstants.Cryohydra_7Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 17, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Cryohydra_7Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Cryohydra_7Heads].Add(AttackHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                //testCases[CreatureConstants.Cryohydra_8Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 18, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Cryohydra_8Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Cryohydra_8Heads].Add(AttackHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                //testCases[CreatureConstants.Cryohydra_9Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 19, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Cryohydra_9Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Cryohydra_9Heads].Add(AttackHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                //testCases[CreatureConstants.Cryohydra_10Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Cryohydra_10Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Cryohydra_10Heads].Add(AttackHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                //testCases[CreatureConstants.Cryohydra_11Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 21, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Cryohydra_11Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Cryohydra_11Heads].Add(AttackHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                //testCases[CreatureConstants.Cryohydra_12Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 22, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Cryohydra_12Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Cryohydra_12Heads].Add(AttackHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                //testCases[CreatureConstants.Darkmantle].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsight, power: 90));

                //testCases[CreatureConstants.Deinonychus].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Delver].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Delver].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 6, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                //testCases[CreatureConstants.Delver].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                //testCases[CreatureConstants.Derro].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Madness));
                //testCases[CreatureConstants.Derro].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 15));
                //testCases[CreatureConstants.Derro].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: "Sunlight"));
                //testCases[CreatureConstants.Derro].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Derro].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GhostSound, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Derro].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Daze, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Derro].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SoundBurst, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Derro].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
                //testCases[CreatureConstants.Derro].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                //testCases[CreatureConstants.Derro_Sane].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 15));
                //testCases[CreatureConstants.Derro_Sane].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: "Sunlight"));
                //testCases[CreatureConstants.Derro_Sane].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Derro_Sane].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GhostSound, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Derro_Sane].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Daze, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Derro_Sane].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SoundBurst, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Derro_Sane].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
                //testCases[CreatureConstants.Derro_Sane].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                //testCases[CreatureConstants.Destrachan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsight, power: 100));
                //testCases[CreatureConstants.Destrachan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Gaze attacks"));
                //testCases[CreatureConstants.Destrachan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Visual effects"));
                //testCases[CreatureConstants.Destrachan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Illusions"));
                //testCases[CreatureConstants.Destrachan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Attacks that rely on sight"));

                //testCases[CreatureConstants.Devourer].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellDeflection));
                //testCases[CreatureConstants.Devourer].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));
                //testCases[CreatureConstants.Devourer].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Confusion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Devourer].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlUndead, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Devourer].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GhoulTouch, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Devourer].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlanarAlly_Lesser, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Devourer].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RayOfEnfeeblement, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Devourer].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpectralHand, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Devourer].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Devourer].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                //testCases[CreatureConstants.Digester].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Digester].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.DisplacerBeast].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Displacement));

                //testCases[CreatureConstants.DisplacerBeast_PackLord].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Displacement));

                //testCases[CreatureConstants.Djinni].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Djinni].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                //testCases[CreatureConstants.Djinni].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift + ": Genie and up to 8 other creatures, provided they all link hands with the genie", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Djinni].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Djinni].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Djinni].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateWater + ": creates wine instead of water", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Djinni].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MajorCreation + ": created vegetable matter is permanent", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Djinni].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PersistentImage, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Djinni].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WindWalk, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Djinni].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GaseousForm, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Djinni].Add(AttackHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));

                //testCases[CreatureConstants.Djinni_Noble].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Djinni_Noble].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                //testCases[CreatureConstants.Djinni_Noble].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift + ": Genie and up to 8 other creatures, provided they all link hands with the genie", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Djinni_Noble].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Djinni_Noble].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Djinni_Noble].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateWater + ": creates wine instead of water", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Djinni_Noble].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MajorCreation + ": created vegetable matter is permanent", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Djinni_Noble].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PersistentImage, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Djinni_Noble].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WindWalk, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Djinni_Noble].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GaseousForm, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Djinni_Noble].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Wish + ": 3 wishes to any non-genie who captures it", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Djinni_Noble].Add(AttackHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));

                //testCases[CreatureConstants.Dog].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Dog].Add(AttackHelper.BuildData(FeatConstants.Track));

                //testCases[CreatureConstants.Dog_Riding].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Dog_Riding].Add(AttackHelper.BuildData(FeatConstants.Track));

                //testCases[CreatureConstants.Donkey].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Doppelganger].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Any Small or Medium Humanoid", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Doppelganger].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Doppelganger].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                //testCases[CreatureConstants.Doppelganger].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Charm effects"));

                //testCases[CreatureConstants.DragonTurtle].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.DragonTurtle].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));

                //testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));

                //testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));

                //testCases[CreatureConstants.Dragon_Black_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Black_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Black_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Black_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Black_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));

                //testCases[CreatureConstants.Dragon_Black_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Black_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Black_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Black_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Black_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Black_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 17));

                //testCases[CreatureConstants.Dragon_Black_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Black_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Black_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Black_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Black_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Black_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Black_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Black_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 18));
                //testCases[CreatureConstants.Dragon_Black_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));
                //testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Black_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Black_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Black_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Black_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Black_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Black_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Black_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Black_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 22));
                //testCases[CreatureConstants.Dragon_Black_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Black_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Black_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Black_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Black_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Black_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Black_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Black_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Black_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Black_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 23));
                //testCases[CreatureConstants.Dragon_Black_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Black_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Black_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Black_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Black_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Black_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Black_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Black_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Black_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Black_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                //testCases[CreatureConstants.Dragon_Black_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Black_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Black_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.InsectPlague, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Black_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Black_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Black_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Black_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Black_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Black_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Black_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Black_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 26));
                //testCases[CreatureConstants.Dragon_Black_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Black_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Black_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.InsectPlague, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 28));
                //testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.CorruptWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.InsectPlague, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.CharmReptiles, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Blue_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Blue_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Blue_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Blue_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Dragon_Blue_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SoundImitation));

                //testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SoundImitation));
                //testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 19));

                //testCases[CreatureConstants.Dragon_Blue_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Blue_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Blue_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Blue_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Dragon_Blue_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Blue_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SoundImitation));
                //testCases[CreatureConstants.Dragon_Blue_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Blue_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));
                //testCases[CreatureConstants.Dragon_Blue_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SoundImitation));
                //testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 22));
                //testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Blue_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Blue_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Blue_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Blue_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Dragon_Blue_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Blue_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SoundImitation));
                //testCases[CreatureConstants.Dragon_Blue_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Blue_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 24));
                //testCases[CreatureConstants.Dragon_Blue_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Blue_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HallucinatoryTerrain, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SoundImitation));
                //testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                //testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HallucinatoryTerrain, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Blue_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Blue_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Blue_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Blue_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Dragon_Blue_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Blue_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SoundImitation));
                //testCases[CreatureConstants.Dragon_Blue_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Blue_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 27));
                //testCases[CreatureConstants.Dragon_Blue_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Blue_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HallucinatoryTerrain, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Blue_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Veil, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SoundImitation));
                //testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 29));
                //testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HallucinatoryTerrain, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Veil, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.CreateDestroyWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SoundImitation));
                //testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 31));
                //testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HallucinatoryTerrain, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Veil, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MirageArcana, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));

                //testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));

                //testCases[CreatureConstants.Dragon_Green_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Green_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Green_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Green_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Green_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));

                //testCases[CreatureConstants.Dragon_Green_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Green_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Green_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Green_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Green_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));

                //testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 19));

                //testCases[CreatureConstants.Dragon_Green_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Green_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Green_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Green_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Green_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Green_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Green_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));
                //testCases[CreatureConstants.Dragon_Green_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 22));
                //testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Green_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Green_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Green_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Green_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Green_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Green_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Green_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 24));
                //testCases[CreatureConstants.Dragon_Green_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Green_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Green_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Green_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Green_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Green_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Green_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Green_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Green_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                //testCases[CreatureConstants.Dragon_Green_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Green_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Green_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Green_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Green_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Green_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Green_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Green_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Green_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 27));
                //testCases[CreatureConstants.Dragon_Green_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Green_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Green_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DominatePerson, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Green_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Green_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Green_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Green_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Green_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Green_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Green_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 28));
                //testCases[CreatureConstants.Dragon_Green_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Green_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Green_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DominatePerson, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 30));
                //testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlantGrowth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DominatePerson, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CommandPlants, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));

                //testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));

                //testCases[CreatureConstants.Dragon_Red_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Red_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Red_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));

                //testCases[CreatureConstants.Dragon_Red_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Red_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Red_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Red_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 4, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 5, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 19));

                //testCases[CreatureConstants.Dragon_Red_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Red_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Red_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Red_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 6, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Red_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Red_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));

                //testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 7, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 23));

                //testCases[CreatureConstants.Dragon_Red_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Red_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Red_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Red_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 8, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Red_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Red_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 24));
                //testCases[CreatureConstants.Dragon_Red_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Red_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Red_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Red_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Red_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 9, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Red_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Red_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 26));
                //testCases[CreatureConstants.Dragon_Red_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Red_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Red_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Red_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Red_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 10, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Red_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Red_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 28));
                //testCases[CreatureConstants.Dragon_Red_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Red_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FindThePath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Red_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Red_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Red_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Red_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 11, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Red_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Red_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 30));
                //testCases[CreatureConstants.Dragon_Red_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Red_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FindThePath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateObject, frequencyQuantity: 12, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 32));
                //testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FindThePath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DiscernLocation, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_White_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_White_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_White_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_White_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));

                //testCases[CreatureConstants.Dragon_White_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_White_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_White_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_White_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));

                //testCases[CreatureConstants.Dragon_White_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_White_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_White_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_White_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));

                //testCases[CreatureConstants.Dragon_White_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_White_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_White_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_White_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                //testCases[CreatureConstants.Dragon_White_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_White_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_White_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_White_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_White_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                //testCases[CreatureConstants.Dragon_White_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_White_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_White_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 16));

                //testCases[CreatureConstants.Dragon_White_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_White_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_White_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_White_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                //testCases[CreatureConstants.Dragon_White_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_White_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_White_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 18));
                //testCases[CreatureConstants.Dragon_White_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_White_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_White_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_White_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_White_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                //testCases[CreatureConstants.Dragon_White_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_White_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_White_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 20));
                //testCases[CreatureConstants.Dragon_White_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_White_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_White_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_White_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_White_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                //testCases[CreatureConstants.Dragon_White_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_White_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_White_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));
                //testCases[CreatureConstants.Dragon_White_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_White_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FreezingFog, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_White_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_White_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_White_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_White_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                //testCases[CreatureConstants.Dragon_White_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_White_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_White_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 23));
                //testCases[CreatureConstants.Dragon_White_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_White_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FreezingFog, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_White_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_White_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_White_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_White_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                //testCases[CreatureConstants.Dragon_White_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_White_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_White_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 24));
                //testCases[CreatureConstants.Dragon_White_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_White_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FreezingFog, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_White_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfIce, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_White_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_White_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_White_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_White_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                //testCases[CreatureConstants.Dragon_White_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_White_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_White_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                //testCases[CreatureConstants.Dragon_White_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_White_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FreezingFog, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_White_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfIce, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Icewalking));
                //testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 27));
                //testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FreezingFog, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfIce, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                //testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                //testCases[CreatureConstants.Dragon_Brass_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Brass_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Brass_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Brass_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                //testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 40 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 50 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 18));

                //testCases[CreatureConstants.Dragon_Brass_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Brass_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Brass_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Brass_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Dragon_Brass_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 60 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Brass_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Brass_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 20));
                //testCases[CreatureConstants.Dragon_Brass_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 70 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 22));
                //testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Brass_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Brass_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Brass_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Brass_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Dragon_Brass_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 80 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Brass_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Brass_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 24));
                //testCases[CreatureConstants.Dragon_Brass_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Brass_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 90 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                //testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Brass_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Brass_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Brass_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Brass_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Dragon_Brass_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 100 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Brass_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Brass_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 27));
                //testCases[CreatureConstants.Dragon_Brass_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Brass_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Brass_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 110 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 28));
                //testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EndureElements + ": radius 120 ft.", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 30));
                //testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SummonMonsterVII + ": one Djinni", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                //testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                //testCases[CreatureConstants.Dragon_Bronze_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Bronze_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Bronze_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Bronze_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Dragon_Bronze_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Bronze_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Dragon_Bronze_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 20));

                //testCases[CreatureConstants.Dragon_Bronze_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Bronze_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Bronze_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Bronze_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Dragon_Bronze_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Bronze_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Dragon_Bronze_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Bronze_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Bronze_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 22));
                //testCases[CreatureConstants.Dragon_Bronze_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Bronze_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 23));
                //testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Bronze_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Bronze_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Bronze_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Bronze_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Dragon_Bronze_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Bronze_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Dragon_Bronze_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Bronze_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Bronze_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                //testCases[CreatureConstants.Dragon_Bronze_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Bronze_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Bronze_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 26));
                //testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 28));
                //testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 29));
                //testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 31));
                //testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));

                //testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));

                //testCases[CreatureConstants.Dragon_Copper_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Copper_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Copper_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Copper_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Copper_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));

                //testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));

                //testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                //testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 19));

                //testCases[CreatureConstants.Dragon_Copper_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Copper_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Copper_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Copper_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Copper_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                //testCases[CreatureConstants.Dragon_Copper_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Copper_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));
                //testCases[CreatureConstants.Dragon_Copper_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                //testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 23));
                //testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Copper_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Copper_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Copper_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Copper_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Copper_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                //testCases[CreatureConstants.Dragon_Copper_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Copper_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                //testCases[CreatureConstants.Dragon_Copper_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Copper_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteMudToRock, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Copper_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteRockToMud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                //testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 26));
                //testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteMudToRock, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteRockToMud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Copper_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Copper_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Copper_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Copper_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Copper_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                //testCases[CreatureConstants.Dragon_Copper_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Copper_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 28));
                //testCases[CreatureConstants.Dragon_Copper_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Copper_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteMudToRock, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Copper_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteRockToMud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Copper_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfStone, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                //testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 29));
                //testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteMudToRock, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteRockToMud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfStone, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                //testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 31));
                //testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteMudToRock, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteRockToMud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfStone, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MoveEarth, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));

                //testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));

                //testCases[CreatureConstants.Dragon_Gold_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Gold_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Gold_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Gold_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));

                //testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));

                //testCases[CreatureConstants.Dragon_Gold_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Gold_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Gold_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Gold_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Gold_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Gold_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 23));
                //testCases[CreatureConstants.Dragon_Gold_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LuckBonus, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                //testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LuckBonus, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Gold_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Gold_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Gold_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Gold_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Gold_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Gold_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 27));
                //testCases[CreatureConstants.Dragon_Gold_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LuckBonus, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GeasQuest, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 28));
                //testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LuckBonus, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GeasQuest, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Gold_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Gold_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Gold_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Gold_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Gold_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Gold_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 30));
                //testCases[CreatureConstants.Dragon_Gold_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LuckBonus, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GeasQuest, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Sunburst, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 31));
                //testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LuckBonus, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GeasQuest, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Sunburst, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Bless, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 33));
                //testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LuckBonus, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GeasQuest, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Sunburst, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Foresight, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));

                //testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));

                //testCases[CreatureConstants.Dragon_Silver_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Silver_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Silver_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Silver_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Silver_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));

                //testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));
                //testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));
                //testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 20));

                //testCases[CreatureConstants.Dragon_Silver_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Silver_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Silver_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Silver_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Silver_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));
                //testCases[CreatureConstants.Dragon_Silver_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Silver_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 22));
                //testCases[CreatureConstants.Dragon_Silver_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));
                //testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 24));
                //testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Silver_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Silver_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Silver_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Silver_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Silver_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));
                //testCases[CreatureConstants.Dragon_Silver_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Silver_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 26));
                //testCases[CreatureConstants.Dragon_Silver_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));
                //testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 27));
                //testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Silver_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Silver_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Silver_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Silver_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Silver_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));
                //testCases[CreatureConstants.Dragon_Silver_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Silver_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 29));
                //testCases[CreatureConstants.Dragon_Silver_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));
                //testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 30));
                //testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSenses));
                //testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Animal or Humanoid form of Medium size or smaller", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Cloudwalking));
                //testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 32));
                //testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ReverseGravity, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Dragonne].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Dretch].Add(AttackHelper.BuildData("Claw", $"1d6", true, true, true, false));
                testCases[CreatureConstants.Dretch].Add(AttackHelper.BuildData("Claw", $"1d6", true, true, true, false));
                testCases[CreatureConstants.Dretch].Add(AttackHelper.BuildData("Bite", $"1d4", true, true, false, false));
                testCases[CreatureConstants.Dretch].Add(AttackHelper.BuildData("Summon Demon", string.Empty, false, true, true, true));
                testCases[CreatureConstants.Dretch].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, false, true, true, true));

                //testCases[CreatureConstants.Drider].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 17));
                //testCases[CreatureConstants.Drider].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DancingLights, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Drider].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ClairaudienceClairvoyance, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Drider].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Drider].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Drider].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Drider].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Drider].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FaerieFire, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Drider].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Levitate, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Drider].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Drider].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Dagger, requiresEquipment: true));
                //testCases[CreatureConstants.Drider].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));

                //testCases[CreatureConstants.Dryad].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, power: 5, focus: "Vulnerable to cold iron weapons", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Dryad].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.TreeDependent));
                //testCases[CreatureConstants.Dryad].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WildEmpathy));
                //testCases[CreatureConstants.Dryad].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Dryad].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithPlants, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Dryad].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TreeShape, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Dryad].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmPerson, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dryad].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeepSlumber, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dryad].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TreeStride, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dryad].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dryad].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Dagger, requiresEquipment: true));
                //testCases[CreatureConstants.Dryad].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));

                //testCases[CreatureConstants.Dwarf_Deep].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 90));
                //testCases[CreatureConstants.Dwarf_Deep].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WeaponFamiliarity, focus: WeaponConstants.DwarvenUrgrosh, requiresEquipment: true));
                //testCases[CreatureConstants.Dwarf_Deep].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WeaponFamiliarity, focus: WeaponConstants.DwarvenWaraxe, requiresEquipment: true));
                //testCases[CreatureConstants.Dwarf_Deep].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LightSensitivity));
                //testCases[CreatureConstants.Dwarf_Deep].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.DwarvenWaraxe, requiresEquipment: true));
                //testCases[CreatureConstants.Dwarf_Deep].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));

                //testCases[CreatureConstants.Dwarf_Duergar].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Dwarf_Duergar].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                //testCases[CreatureConstants.Dwarf_Duergar].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Phantasms"));
                //testCases[CreatureConstants.Dwarf_Duergar].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                //testCases[CreatureConstants.Dwarf_Duergar].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LightSensitivity));
                //testCases[CreatureConstants.Dwarf_Duergar].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EnlargePerson + ": only self + carried items", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dwarf_Duergar].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + ": only self + carried items", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Dwarf_Duergar].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Warhammer, requiresEquipment: true));
                //testCases[CreatureConstants.Dwarf_Duergar].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.LightCrossbow, requiresEquipment: true));

                //testCases[CreatureConstants.Dwarf_Hill].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                //testCases[CreatureConstants.Dwarf_Hill].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WeaponFamiliarity, focus: WeaponConstants.DwarvenUrgrosh, requiresEquipment: true));
                //testCases[CreatureConstants.Dwarf_Hill].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WeaponFamiliarity, focus: WeaponConstants.DwarvenWaraxe, requiresEquipment: true));
                //testCases[CreatureConstants.Dwarf_Hill].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.DwarvenWaraxe, requiresEquipment: true));
                //testCases[CreatureConstants.Dwarf_Hill].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));

                //testCases[CreatureConstants.Dwarf_Mountain].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                //testCases[CreatureConstants.Dwarf_Mountain].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WeaponFamiliarity, focus: WeaponConstants.DwarvenUrgrosh, requiresEquipment: true));
                //testCases[CreatureConstants.Dwarf_Mountain].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WeaponFamiliarity, focus: WeaponConstants.DwarvenWaraxe, requiresEquipment: true));
                //testCases[CreatureConstants.Dwarf_Mountain].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.DwarvenWaraxe, requiresEquipment: true));
                //testCases[CreatureConstants.Dwarf_Mountain].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));

                //testCases[CreatureConstants.Eagle].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Eagle_Giant].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Evasion));

                //testCases[CreatureConstants.Efreeti].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Any Small, Medium, or Large Humanoid or Giant", frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Efreeti].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                //testCases[CreatureConstants.Efreeti].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift + ": Genie and up to 8 other creatures, provided they all link hands with the genie", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Efreeti].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Efreeti].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ProduceFlame, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Efreeti].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Pyrotechnics, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Efreeti].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ScorchingRay + ": 1 ray only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Efreeti].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Efreeti].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfFire, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Efreeti].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Wish + ": Grant up to 3 wishes to nongenies", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Efreeti].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PermanentImage, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Efreeti].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GaseousForm, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Efreeti].Add(AttackHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));

                //testCases[CreatureConstants.Elasmosaurus].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Elemental_Air_Small].Add(AttackHelper.BuildData(None));

                //testCases[CreatureConstants.Elemental_Air_Medium].Add(AttackHelper.BuildData(None));

                //testCases[CreatureConstants.Elemental_Air_Large].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                //testCases[CreatureConstants.Elemental_Air_Huge].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                //testCases[CreatureConstants.Elemental_Air_Greater].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                //testCases[CreatureConstants.Elemental_Air_Elder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                //testCases[CreatureConstants.Elemental_Earth_Small].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EarthGlide));

                //testCases[CreatureConstants.Elemental_Earth_Medium].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EarthGlide));

                //testCases[CreatureConstants.Elemental_Earth_Large].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EarthGlide));
                //testCases[CreatureConstants.Elemental_Earth_Large].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                //testCases[CreatureConstants.Elemental_Earth_Huge].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EarthGlide));
                //testCases[CreatureConstants.Elemental_Earth_Huge].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                //testCases[CreatureConstants.Elemental_Earth_Greater].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EarthGlide));
                //testCases[CreatureConstants.Elemental_Earth_Greater].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                //testCases[CreatureConstants.Elemental_Earth_Elder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EarthGlide));
                //testCases[CreatureConstants.Elemental_Earth_Elder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                //testCases[CreatureConstants.Elemental_Fire_Small].Add(AttackHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                //testCases[CreatureConstants.Elemental_Fire_Small].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Elemental_Fire_Medium].Add(AttackHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                //testCases[CreatureConstants.Elemental_Fire_Medium].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Elemental_Fire_Large].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Elemental_Fire_Large].Add(AttackHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                //testCases[CreatureConstants.Elemental_Fire_Large].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Elemental_Fire_Huge].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Elemental_Fire_Huge].Add(AttackHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                //testCases[CreatureConstants.Elemental_Fire_Huge].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Elemental_Fire_Greater].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Elemental_Fire_Greater].Add(AttackHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                //testCases[CreatureConstants.Elemental_Fire_Greater].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Elemental_Fire_Elder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Elemental_Fire_Elder].Add(AttackHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                //testCases[CreatureConstants.Elemental_Fire_Elder].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Elemental_Water_Small].Add(AttackHelper.BuildData(None));

                //testCases[CreatureConstants.Elemental_Water_Medium].Add(AttackHelper.BuildData(None));

                //testCases[CreatureConstants.Elemental_Water_Large].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                //testCases[CreatureConstants.Elemental_Water_Huge].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                //testCases[CreatureConstants.Elemental_Water_Greater].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                //testCases[CreatureConstants.Elemental_Water_Elder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                //testCases[CreatureConstants.Elephant].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Elf_Aquatic].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Gills));
                //testCases[CreatureConstants.Elf_Aquatic].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision_Superior));
                //testCases[CreatureConstants.Elf_Aquatic].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Spear, requiresEquipment: true));
                //testCases[CreatureConstants.Elf_Aquatic].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Longspear, requiresEquipment: true));
                //testCases[CreatureConstants.Elf_Aquatic].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Shortspear, requiresEquipment: true));
                //testCases[CreatureConstants.Elf_Aquatic].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Trident, requiresEquipment: true));
                //testCases[CreatureConstants.Elf_Aquatic].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Exotic, focus: WeaponConstants.Net, requiresEquipment: true));

                //testCases[CreatureConstants.Elf_Drow].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Elf_Drow].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 11));
                //testCases[CreatureConstants.Elf_Drow].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LightBlindness));
                //testCases[CreatureConstants.Elf_Drow].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DancingLights, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Elf_Drow].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Elf_Drow].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FaerieFire, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Elf_Drow].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
                //testCases[CreatureConstants.Elf_Drow].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Rapier, requiresEquipment: true));
                //testCases[CreatureConstants.Elf_Drow].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Exotic, focus: WeaponConstants.HandCrossbow, requiresEquipment: true));

                //testCases[CreatureConstants.Elf_Gray].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                //testCases[CreatureConstants.Elf_Gray].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
                //testCases[CreatureConstants.Elf_Gray].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Rapier, requiresEquipment: true));
                //testCases[CreatureConstants.Elf_Gray].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));
                //testCases[CreatureConstants.Elf_Gray].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
                //testCases[CreatureConstants.Elf_Gray].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
                //testCases[CreatureConstants.Elf_Gray].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeShortbow, requiresEquipment: true));

                //testCases[CreatureConstants.Elf_Half].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                //testCases[CreatureConstants.Elf_Half].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.ElvenBlood));
                //testCases[CreatureConstants.Elf_Half].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));

                //testCases[CreatureConstants.Elf_High].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                //testCases[CreatureConstants.Elf_High].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
                //testCases[CreatureConstants.Elf_High].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Rapier, requiresEquipment: true));
                //testCases[CreatureConstants.Elf_High].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));
                //testCases[CreatureConstants.Elf_High].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
                //testCases[CreatureConstants.Elf_High].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
                //testCases[CreatureConstants.Elf_High].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeShortbow, requiresEquipment: true));

                //testCases[CreatureConstants.Elf_Wild].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                //testCases[CreatureConstants.Elf_Wild].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
                //testCases[CreatureConstants.Elf_Wild].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Rapier, requiresEquipment: true));
                //testCases[CreatureConstants.Elf_Wild].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));
                //testCases[CreatureConstants.Elf_Wild].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
                //testCases[CreatureConstants.Elf_Wild].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
                //testCases[CreatureConstants.Elf_Wild].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeShortbow, requiresEquipment: true));

                //testCases[CreatureConstants.Elf_Wood].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                //testCases[CreatureConstants.Elf_Wood].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
                //testCases[CreatureConstants.Elf_Wood].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Rapier, requiresEquipment: true));
                //testCases[CreatureConstants.Elf_Wood].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));
                //testCases[CreatureConstants.Elf_Wood].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
                //testCases[CreatureConstants.Elf_Wood].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
                //testCases[CreatureConstants.Elf_Wood].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeShortbow, requiresEquipment: true));

                testCases[CreatureConstants.Erinyes].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, true, true, true, false));
                testCases[CreatureConstants.Erinyes].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, false, true, true, false));
                testCases[CreatureConstants.Erinyes].Add(AttackHelper.BuildData("Rope", "Entangle", false, true, true, false));
                testCases[CreatureConstants.Erinyes].Add(AttackHelper.BuildData("Entangle", string.Empty, true, true, false, true));
                testCases[CreatureConstants.Erinyes].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, false, true, true, true));
                testCases[CreatureConstants.Erinyes].Add(AttackHelper.BuildData("Summon Devil", string.Empty, false, true, true, true));

                //testCases[CreatureConstants.EtherealFilcher].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.EtherealFilcher].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EtherealJaunt, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                //testCases[CreatureConstants.EtherealMarauder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EtherealJaunt, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                //testCases[CreatureConstants.Ettercap].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));

                //testCases[CreatureConstants.Ettin].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.TwoWeaponFighting_Superior, requiresEquipment: true));
                //testCases[CreatureConstants.Ettin].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Morningstar, requiresEquipment: true));
                //testCases[CreatureConstants.Ettin].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
                //testCases[CreatureConstants.Ettin].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                //testCases[CreatureConstants.Ettin].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                //testCases[CreatureConstants.FireBeetle_Giant].Add(AttackHelper.BuildData(None));

                //testCases[CreatureConstants.FormianWorker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.HiveMind));
                //testCases[CreatureConstants.FormianWorker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureInflictSeriousWounds + ": 8 workers work together to cast the spell", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.FormianWorker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MakeWhole + ": 3 workers work together to cast the spell", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.FormianWorker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                //testCases[CreatureConstants.FormianWorker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
                //testCases[CreatureConstants.FormianWorker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                //testCases[CreatureConstants.FormianWorker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.FormianWorker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.FormianWorker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Sonic, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                //testCases[CreatureConstants.FormianWarrior].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.HiveMind));
                //testCases[CreatureConstants.FormianWarrior].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 18));
                //testCases[CreatureConstants.FormianWarrior].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                //testCases[CreatureConstants.FormianWarrior].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
                //testCases[CreatureConstants.FormianWarrior].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                //testCases[CreatureConstants.FormianWarrior].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.FormianWarrior].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.FormianWarrior].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Sonic, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                //testCases[CreatureConstants.FormianTaskmaster].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.HiveMind));
                //testCases[CreatureConstants.FormianTaskmaster].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));
                //testCases[CreatureConstants.FormianTaskmaster].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                //testCases[CreatureConstants.FormianTaskmaster].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DominateMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.FormianTaskmaster].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                //testCases[CreatureConstants.FormianTaskmaster].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
                //testCases[CreatureConstants.FormianTaskmaster].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                //testCases[CreatureConstants.FormianTaskmaster].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.FormianTaskmaster].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.FormianTaskmaster].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Sonic, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                //testCases[CreatureConstants.FormianMyrmarch].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.FormianMyrmarch].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.HiveMind));
                //testCases[CreatureConstants.FormianMyrmarch].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                //testCases[CreatureConstants.FormianMyrmarch].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.FormianMyrmarch].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ClairaudienceClairvoyance, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.FormianMyrmarch].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.FormianMyrmarch].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.FormianMyrmarch].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.FormianMyrmarch].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.FormianMyrmarch].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Dictum, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.FormianMyrmarch].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.OrdersWrath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.FormianMyrmarch].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                //testCases[CreatureConstants.FormianMyrmarch].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
                //testCases[CreatureConstants.FormianMyrmarch].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                //testCases[CreatureConstants.FormianMyrmarch].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.FormianMyrmarch].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.FormianMyrmarch].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Sonic, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.FormianMyrmarch].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));

                //testCases[CreatureConstants.FormianQueen].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.FormianQueen].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.HiveMind));
                //testCases[CreatureConstants.FormianQueen].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 30));
                //testCases[CreatureConstants.FormianQueen].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CalmEmotions, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.FormianQueen].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.FormianQueen].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ClairaudienceClairvoyance, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.FormianQueen].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.FormianQueen].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.FormianQueen].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Dictum, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.FormianQueen].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Divination, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.FormianQueen].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.FormianQueen].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.FormianQueen].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.OrdersWrath, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.FormianQueen].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ShieldOfLaw, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.FormianQueen].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.FormianQueen].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                //testCases[CreatureConstants.FormianQueen].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
                //testCases[CreatureConstants.FormianQueen].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                //testCases[CreatureConstants.FormianQueen].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.FormianQueen].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.FormianQueen].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Sonic, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.FormianQueen].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, focus: "Any intelligent creature within 50 miles whose presence she is aware of"));
                //testCases[CreatureConstants.FormianQueen].Add(AttackHelper.BuildData(FeatConstants.EschewMaterials));

                //testCases[CreatureConstants.FrostWorm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DeathThroes));

                //testCases[CreatureConstants.Gargoyle].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Gargoyle].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Freeze));

                //testCases[CreatureConstants.Gargoyle_Kapoacinth].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Gargoyle_Kapoacinth].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Freeze));

                //testCases[CreatureConstants.GelatinousCube].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.GelatinousCube].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Transparent));

                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Humanoid and globe forms", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to evil, cold iron weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 28));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.ProtectiveAura));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Tongues, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Aid, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ColorSpray, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ComprehendLanguages, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ContinualFlame, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureInflictLightWounds, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DancingLights, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DisguiseSelf, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility_Greater + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MajorImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": self plus 50 pounds of objects only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChainLightning, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PrismaticSpray, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfForce, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));

                //testCases[CreatureConstants.Ghoul].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.TurnResistance, power: 2));

                //testCases[CreatureConstants.Ghoul_Ghast].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.TurnResistance, power: 2));

                //testCases[CreatureConstants.Ghoul_Lacedon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.TurnResistance, power: 2));

                //testCases[CreatureConstants.Giant_Cloud].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.RockCatching, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Giant_Cloud].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Giant_Cloud].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.OversizedWeapon, focus: SizeConstants.Gargantuan, requiresEquipment: true));
                //testCases[CreatureConstants.Giant_Cloud].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Levitate + ": self plus 2,000 pounds", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Giant_Cloud].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ObscuringMist, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Giant_Cloud].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FogCloud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Giant_Cloud].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Morningstar, requiresEquipment: true));
                //testCases[CreatureConstants.Giant_Cloud].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                //testCases[CreatureConstants.Giant_Fire].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.RockCatching, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Giant_Fire].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));
                //testCases[CreatureConstants.Giant_Fire].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Heavy, requiresEquipment: true));
                //testCases[CreatureConstants.Giant_Fire].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                //testCases[CreatureConstants.Giant_Fire].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                //testCases[CreatureConstants.Giant_Frost].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.RockCatching, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Giant_Frost].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greataxe, requiresEquipment: true));
                //testCases[CreatureConstants.Giant_Frost].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                //testCases[CreatureConstants.Giant_Hill].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.RockCatching, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Giant_Hill].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatclub, requiresEquipment: true));
                //testCases[CreatureConstants.Giant_Hill].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                //testCases[CreatureConstants.Giant_Hill].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                //testCases[CreatureConstants.Giant_Stone].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                //testCases[CreatureConstants.Giant_Stone].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.RockCatching, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Giant_Stone].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatclub, requiresEquipment: true));
                //testCases[CreatureConstants.Giant_Stone].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                //testCases[CreatureConstants.Giant_Stone].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                //testCases[CreatureConstants.Giant_Stone_Elder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                //testCases[CreatureConstants.Giant_Stone_Elder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.RockCatching, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Giant_Stone_Elder].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatclub, requiresEquipment: true));
                //testCases[CreatureConstants.Giant_Stone_Elder].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                //testCases[CreatureConstants.Giant_Stone_Elder].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                //testCases[CreatureConstants.Giant_Stone_Elder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneShape, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Giant_Stone_Elder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StoneTell, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Giant_Stone_Elder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteRockToMud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Giant_Stone_Elder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TransmuteMudToRock, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Giant_Storm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Giant_Storm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterBreathing));
                //testCases[CreatureConstants.Giant_Storm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.RockCatching, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Giant_Storm].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));
                //testCases[CreatureConstants.Giant_Storm].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
                //testCases[CreatureConstants.Giant_Storm].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                //testCases[CreatureConstants.Giant_Storm].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                //testCases[CreatureConstants.Giant_Storm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CallLightning, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Giant_Storm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChainLightning, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Giant_Storm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Giant_Storm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Levitate, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Giant_Storm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FreedomOfMovement, frequencyTimePeriod: FeatConstants.Frequencies.Constant));

                //testCases[CreatureConstants.GibberingMouther].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Amorphous));
                //testCases[CreatureConstants.GibberingMouther].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to bludgeoning weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                //testCases[CreatureConstants.Girallon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Githyanki].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                //testCases[CreatureConstants.Githyanki].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));
                //testCases[CreatureConstants.Githyanki].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
                //testCases[CreatureConstants.Githyanki].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                //testCases[CreatureConstants.Githyanki].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                //testCases[CreatureConstants.Githyanki].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 6));
                //testCases[CreatureConstants.Githyanki].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Daze, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Githyanki].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MageHand, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Githzerai].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                //testCases[CreatureConstants.Githzerai].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
                //testCases[CreatureConstants.Githzerai].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
                //testCases[CreatureConstants.Githzerai].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 6));
                //testCases[CreatureConstants.Githzerai].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.InertialArmor, power: 4));
                //testCases[CreatureConstants.Githzerai].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Daze, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Githzerai].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FeatherFall, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Githzerai].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Shatter, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                testCases[CreatureConstants.Glabrezu].Add(AttackHelper.BuildData("Pincer", $"2d8", true, true, true, false));
                testCases[CreatureConstants.Glabrezu].Add(AttackHelper.BuildData("Pincer", $"2d8", true, true, true, false));
                testCases[CreatureConstants.Glabrezu].Add(AttackHelper.BuildData("Claw", $"1d6", true, true, false, false));
                testCases[CreatureConstants.Glabrezu].Add(AttackHelper.BuildData("Claw", $"1d6", true, true, false, false));
                testCases[CreatureConstants.Glabrezu].Add(AttackHelper.BuildData("Bite", $"1d8", true, true, false, false));
                testCases[CreatureConstants.Glabrezu].Add(AttackHelper.BuildData("Improved Grab", string.Empty, true, true, false, true));
                testCases[CreatureConstants.Glabrezu].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, false, true, true, true));
                testCases[CreatureConstants.Glabrezu].Add(AttackHelper.BuildData("Summon Demon", string.Empty, false, true, true, true));

                //testCases[CreatureConstants.Gnoll].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                //testCases[CreatureConstants.Gnoll].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Battleaxe, requiresEquipment: true));
                //testCases[CreatureConstants.Gnoll].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
                //testCases[CreatureConstants.Gnoll].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                //testCases[CreatureConstants.Gnome_Forest].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
                //testCases[CreatureConstants.Gnome_Forest].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AttackBonus, focus: CreatureConstants.Types.Subtypes.Orc, power: 1));
                //testCases[CreatureConstants.Gnome_Forest].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AttackBonus, focus: CreatureConstants.Types.Subtypes.Reptilian, power: 1));
                //testCases[CreatureConstants.Gnome_Forest].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals + ": on a very basic level with forest animals", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Gnome_Forest].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DancingLights, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day)); //TODO: Minimum charisma
                //testCases[CreatureConstants.Gnome_Forest].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GhostSound, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day)); //TODO: Minimum charisma
                //testCases[CreatureConstants.Gnome_Forest].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Prestidigitation, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day)); //TODO: Minimum charisma
                //testCases[CreatureConstants.Gnome_Forest].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PassWithoutTrace + ": self only", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                //testCases[CreatureConstants.Gnome_Rock].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
                //testCases[CreatureConstants.Gnome_Rock].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals + ": burrowing mammals only, duration 1 minute", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Gnome_Rock].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DancingLights, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day)); //TODO: Minimum charisma
                //testCases[CreatureConstants.Gnome_Rock].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GhostSound, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day)); //TODO: Minimum charisma
                //testCases[CreatureConstants.Gnome_Rock].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Prestidigitation, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day)); //TODO: Minimum charisma

                //testCases[CreatureConstants.Gnome_Svirfneblin].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.HeavyPick, requiresEquipment: true));
                //testCases[CreatureConstants.Gnome_Svirfneblin].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                //testCases[CreatureConstants.Gnome_Svirfneblin].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Heavy, requiresEquipment: true));
                //testCases[CreatureConstants.Gnome_Svirfneblin].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 120));
                //testCases[CreatureConstants.Gnome_Svirfneblin].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Stonecunning));
                //testCases[CreatureConstants.Gnome_Svirfneblin].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 11));
                //testCases[CreatureConstants.Gnome_Svirfneblin].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BlindnessDeafness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Gnome_Svirfneblin].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blur, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Gnome_Svirfneblin].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DisguiseSelf, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Goblin].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Morningstar, requiresEquipment: true));
                //testCases[CreatureConstants.Goblin].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
                //testCases[CreatureConstants.Goblin].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                //testCases[CreatureConstants.Goblin].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));

                //testCases[CreatureConstants.Golem_Clay].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to adamantine, bludgeoning weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Golem_Clay].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Haste + ": after at least 1 round of combat, lasts 3 rounds", frequencyTimePeriod: FeatConstants.Frequencies.Day, frequencyQuantity: 1));
                //testCases[CreatureConstants.Golem_Clay].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Magic (see creature description)"));

                //testCases[CreatureConstants.Golem_Flesh].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to adamantine weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Golem_Flesh].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Magic (see creature description)"));

                //testCases[CreatureConstants.Golem_Iron].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to adamantine weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Golem_Iron].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Magic (see creature description)"));

                //testCases[CreatureConstants.Golem_Stone].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to adamantine weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Golem_Stone].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Magic (see creature description)"));

                //testCases[CreatureConstants.Golem_Stone_Greater].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to adamantine weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Golem_Stone_Greater].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Magic (see creature description)"));

                //testCases[CreatureConstants.Gorgon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.GrayOoze].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                //testCases[CreatureConstants.GrayOoze].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                //testCases[CreatureConstants.GrayOoze].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Transparent));

                //testCases[CreatureConstants.GrayRender].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.GreenHag].Add(AttackHelper.BuildData("Claw", $"1d4", true, true, true, false));
                testCases[CreatureConstants.GreenHag].Add(AttackHelper.BuildData("Claw", $"1d4", true, true, true, false));
                testCases[CreatureConstants.GreenHag].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, false, true, true, true));
                testCases[CreatureConstants.GreenHag].Add(AttackHelper.BuildData("Weakness", "2d4 Strength damage", false, true, true, true));
                testCases[CreatureConstants.GreenHag].Add(AttackHelper.BuildData("Mimicry", string.Empty, false, true, true, true));

                //testCases[CreatureConstants.Grick].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Grick].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Grick].Add(AttackHelper.BuildData(FeatConstants.Track));

                //testCases[CreatureConstants.Griffon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Grig].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Grig].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 17));
                //testCases[CreatureConstants.Grig].Add(AttackHelper.BuildData(FeatConstants.Dodge, power: 1));
                //testCases[CreatureConstants.Grig].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));
                //testCases[CreatureConstants.Grig].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DisguiseSelf, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Grig].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Grig].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Grig].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Pyrotechnics, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Grig].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Grig].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
                //testCases[CreatureConstants.Grig].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));

                //testCases[CreatureConstants.Grig_WithFiddle].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Grig_WithFiddle].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 17));
                //testCases[CreatureConstants.Grig_WithFiddle].Add(AttackHelper.BuildData(FeatConstants.Dodge, power: 1));
                //testCases[CreatureConstants.Grig_WithFiddle].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));
                //testCases[CreatureConstants.Grig_WithFiddle].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DisguiseSelf, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Grig_WithFiddle].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Grig_WithFiddle].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Grig_WithFiddle].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Pyrotechnics, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Grig_WithFiddle].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Grig_WithFiddle].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
                //testCases[CreatureConstants.Grig_WithFiddle].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));

                //testCases[CreatureConstants.Grimlock].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsight, power: 40));
                //testCases[CreatureConstants.Grimlock].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Gaze attacks"));
                //testCases[CreatureConstants.Grimlock].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Visual effects"));
                //testCases[CreatureConstants.Grimlock].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Illusions"));
                //testCases[CreatureConstants.Grimlock].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Attack forms that rely on sight"));
                //testCases[CreatureConstants.Grimlock].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Grimlock].Add(AttackHelper.BuildData(FeatConstants.Track));

                testCases[CreatureConstants.Gynosphinx].Add(AttackHelper.BuildData("Claw", $"1d6", "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Gynosphinx].Add(AttackHelper.BuildData("Pounce", string.Empty, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Gynosphinx].Add(AttackHelper.BuildData("Rake", $"1d6", "melee", 2, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Gynosphinx].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //testCases[CreatureConstants.Halfling_Deep].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                //testCases[CreatureConstants.Halfling_Deep].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Stonecunning));

                //testCases[CreatureConstants.Halfling_Lightfoot].Add(AttackHelper.BuildData(None));

                //testCases[CreatureConstants.Halfling_Tallfellow].Add(AttackHelper.BuildData(None));

                //testCases[CreatureConstants.Harpy].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Club, requiresEquipment: true));

                //testCases[CreatureConstants.Hawk].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.HellHound].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.HellHound].Add(AttackHelper.BuildData(FeatConstants.Track));

                //testCases[CreatureConstants.HellHound_NessianWarhound].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.HellHound_NessianWarhound].Add(AttackHelper.BuildData(FeatConstants.Track));

                testCases[CreatureConstants.Hellcat_Bezekira].Add(AttackHelper.BuildData("Claw", $"1d8", true, true, true, false));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(AttackHelper.BuildData("Claw", $"1d8", true, true, true, false));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(AttackHelper.BuildData("Bite", $"2d8", true, true, false, false));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(AttackHelper.BuildData("Improved Grab", string.Empty, true, true, false, true));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(AttackHelper.BuildData("Pounce", string.Empty, true, true, true, true));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(AttackHelper.BuildData("Rake", $"1d8", true, true, false, true));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(AttackHelper.BuildData("Rake", $"1d8", true, true, false, true));

                testCases[CreatureConstants.Hellwasp_Swarm].Add(AttackHelper.BuildData("Swarm", $"3d6 + poison", true, true, true, false));
                testCases[CreatureConstants.Hellwasp_Swarm].Add(AttackHelper.BuildData("Distraction", string.Empty, true, true, true, true));
                testCases[CreatureConstants.Hellwasp_Swarm].Add(AttackHelper.BuildData("Inhabit", string.Empty, true, true, true, true));
                testCases[CreatureConstants.Hellwasp_Swarm].Add(AttackHelper.BuildData("Poison", "Initial and secondary damage 1d6 Dex", true, true, true, true));

                testCases[CreatureConstants.Hezrou].Add(AttackHelper.BuildData("Bite", $"4d4", true, true, true, false));
                testCases[CreatureConstants.Hezrou].Add(AttackHelper.BuildData("Claw", $"1d8", true, true, false, false));
                testCases[CreatureConstants.Hezrou].Add(AttackHelper.BuildData("Claw", $"1d8", true, true, false, false));
                testCases[CreatureConstants.Hezrou].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, false, true, true, true));
                testCases[CreatureConstants.Hezrou].Add(AttackHelper.BuildData("Stench", string.Empty, false, true, false, true));
                testCases[CreatureConstants.Hezrou].Add(AttackHelper.BuildData("Improved Grab", string.Empty, true, true, false, true));
                testCases[CreatureConstants.Hezrou].Add(AttackHelper.BuildData("Summon Demon", string.Empty, false, true, true, true));

                testCases[CreatureConstants.Hieracosphinx].Add(AttackHelper.BuildData("Bite", $"1d10", "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Hieracosphinx].Add(AttackHelper.BuildData("Claw", $"1d6", "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Hieracosphinx].Add(AttackHelper.BuildData("Pounce", string.Empty, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Hieracosphinx].Add(AttackHelper.BuildData("Rake", $"1d6", "melee", 2, FeatConstants.Frequencies.Round, true, true, false, true));

                //testCases[CreatureConstants.Hippogriff].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Hobgoblin].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                //testCases[CreatureConstants.Hobgoblin].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
                //testCases[CreatureConstants.Hobgoblin].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
                //testCases[CreatureConstants.Hobgoblin].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));

                //testCases[CreatureConstants.Homunculus].Add(AttackHelper.BuildData(None));

                testCases[CreatureConstants.HornedDevil_Cornugon].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, true, false, true, false));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(AttackHelper.BuildData("Claw", $"2d6", true, true, true, false));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(AttackHelper.BuildData("Claw", $"2d6", true, true, true, false));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(AttackHelper.BuildData("Bite", $"2d8", true, true, false, false));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(AttackHelper.BuildData("Tail", $"2d6 + infernal wound", true, true, false, false));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(AttackHelper.BuildData("Fear Aura", string.Empty, false, true, false, true));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(AttackHelper.BuildData("Infernal Wound", string.Empty, true, true, false, true));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, false, true, true, true));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(AttackHelper.BuildData("Stun", string.Empty, true, true, false, true));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(AttackHelper.BuildData("Summon Devil", string.Empty, false, true, true, true));

                //testCases[CreatureConstants.Horse_Heavy].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Horse_Heavy_War].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Horse_Light].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Horse_Light_War].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.HoundArchon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Any canine form of Small to Large size", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.HoundArchon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to evil weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.HoundArchon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.HoundArchon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 16));
                //testCases[CreatureConstants.HoundArchon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Aid, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.HoundArchon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ContinualFlame, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.HoundArchon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.HoundArchon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Message, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.HoundArchon].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));

                //testCases[CreatureConstants.Howler].Add(AttackHelper.BuildData(None));

                //testCases[CreatureConstants.Human].Add(AttackHelper.BuildData(None));

                //testCases[CreatureConstants.Hydra_5Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Hydra_5Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Hydra_5Heads].Add(AttackHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                //testCases[CreatureConstants.Hydra_6Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 16, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Hydra_6Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Hydra_6Heads].Add(AttackHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                //testCases[CreatureConstants.Hydra_7Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 17, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Hydra_7Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Hydra_7Heads].Add(AttackHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                //testCases[CreatureConstants.Hydra_8Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 18, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Hydra_8Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Hydra_8Heads].Add(AttackHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                //testCases[CreatureConstants.Hydra_9Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 19, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Hydra_9Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Hydra_9Heads].Add(AttackHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                //testCases[CreatureConstants.Hydra_10Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Hydra_10Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Hydra_10Heads].Add(AttackHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                //testCases[CreatureConstants.Hydra_11Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 21, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Hydra_11Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Hydra_11Heads].Add(AttackHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                //testCases[CreatureConstants.Hydra_12Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 22, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Hydra_12Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Hydra_12Heads].Add(AttackHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                //testCases[CreatureConstants.Hyena].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.IceDevil_Gelugon].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, true, false, true, false));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(AttackHelper.BuildData("Claw", $"1d10", true, true, true, false));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(AttackHelper.BuildData("Claw", $"1d10", true, true, true, false));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(AttackHelper.BuildData("Bite", $"2d6", true, true, false, false));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(AttackHelper.BuildData("Tail", $"3d6 + slow", true, true, false, false));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(AttackHelper.BuildData("Fear Aura", string.Empty, false, true, false, true));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, false, true, true, true));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(AttackHelper.BuildData("Slow", string.Empty, true, true, false, true));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(AttackHelper.BuildData("Summon Devil", string.Empty, false, true, true, true));

                testCases[CreatureConstants.Imp].Add(AttackHelper.BuildData("Sting", $"1d4 + poison", true, true, true, false));
                testCases[CreatureConstants.Imp].Add(AttackHelper.BuildData("Poison", $"Initial damage 1d4 Dex, Secondary damage 2d4 Dex", true, true, false, true));
                testCases[CreatureConstants.Imp].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, false, true, true, true));

                //testCases[CreatureConstants.InvisibleStalker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.NaturalInvisibility));
                //testCases[CreatureConstants.InvisibleStalker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tracking_Improved));

                //testCases[CreatureConstants.Janni].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                //testCases[CreatureConstants.Janni].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.ElementalEndurance));
                //testCases[CreatureConstants.Janni].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift + ": Genie and up to 8 other creatures, provided they all link hands with the genie", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Janni].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility + ": self only", frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Janni].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Janni].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CreateFoodAndWater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Janni].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EtherealJaunt, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Janni].Add(AttackHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                //testCases[CreatureConstants.Janni].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                //testCases[CreatureConstants.Janni].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                //testCases[CreatureConstants.Janni].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Scimitar, requiresEquipment: true));
                //testCases[CreatureConstants.Janni].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));

                //testCases[CreatureConstants.Kobold].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                //testCases[CreatureConstants.Kobold].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LightSensitivity));
                //testCases[CreatureConstants.Kobold].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Spear, requiresEquipment: true));
                //testCases[CreatureConstants.Kobold].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Sling, requiresEquipment: true));

                //testCases[CreatureConstants.Kolyarut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to chaotic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Kolyarut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Kolyarut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 22));
                //testCases[CreatureConstants.Kolyarut].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longsword, requiresEquipment: true));
                //testCases[CreatureConstants.Kolyarut].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                //testCases[CreatureConstants.Kolyarut].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                //testCases[CreatureConstants.Kolyarut].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Heavy, requiresEquipment: true));
                //testCases[CreatureConstants.Kolyarut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DiscernLies, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Kolyarut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DisguiseSelf, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Kolyarut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fear, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Kolyarut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldPerson, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Kolyarut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Kolyarut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateCreature, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Kolyarut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Kolyarut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Kolyarut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MarkOfJustice, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Kolyarut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GeasQuest, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week));

                //testCases[CreatureConstants.Kraken].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.InkCloud));
                //testCases[CreatureConstants.Kraken].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Jet));
                //testCases[CreatureConstants.Kraken].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWeather, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Kraken].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ControlWinds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Kraken].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DominateAnimal, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Kraken].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ResistEnergy, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Krenshar].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Krenshar].Add(AttackHelper.BuildData(FeatConstants.Track));

                //testCases[CreatureConstants.KuoToa].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Adhesive));
                //testCases[CreatureConstants.KuoToa].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Amphibious));
                //testCases[CreatureConstants.KuoToa].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                //testCases[CreatureConstants.KuoToa].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Paralysis"));
                //testCases[CreatureConstants.KuoToa].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenSight));
                //testCases[CreatureConstants.KuoToa].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LightBlindness));
                //testCases[CreatureConstants.KuoToa].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.KuoToa].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Slippery));
                //testCases[CreatureConstants.KuoToa].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Shortspear, requiresEquipment: true));
                //testCases[CreatureConstants.KuoToa].Add(AttackHelper.BuildData(FeatConstants.Alertness, power: 2));

                //testCases[CreatureConstants.Lamia].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Dagger, requiresEquipment: true));
                //testCases[CreatureConstants.Lamia].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DisguiseSelf, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Lamia].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Ventriloquism, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Lamia].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Lamia].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MajorImage, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Lamia].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MirrorImage, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Lamia].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Lamia].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeepSlumber, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Lammasu].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstAlignment, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                //testCases[CreatureConstants.Lammasu].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility_Greater + ": self only", frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Lammasu].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionDoor, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.LanternArchon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic, evil weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.LanternArchon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Aid, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.LanternArchon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ContinualFlame, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.LanternArchon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                testCases[CreatureConstants.Lemure].Add(AttackHelper.BuildData("Claw", $"1d4", true, true, true, false));
                testCases[CreatureConstants.Lemure].Add(AttackHelper.BuildData("Claw", $"1d4", true, true, true, false));

                //testCases[CreatureConstants.Leonal].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to evil, silver weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Leonal].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Petrification"));
                //testCases[CreatureConstants.Leonal].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Leonal].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LayOnHands));
                //testCases[CreatureConstants.Leonal].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.ProtectiveAura));
                //testCases[CreatureConstants.Leonal].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Leonal].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Sonic, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Leonal].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 28));
                //testCases[CreatureConstants.Leonal].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals + ": does not require sound", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Leonal].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Leonal].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fireball, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Leonal].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Leonal].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfForce, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Leonal].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureInflictCriticalWounds, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Leonal].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.NeutralizePoison, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Leonal].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveDisease, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Leonal].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HealHarm, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Leopard].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Lillend].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                //testCases[CreatureConstants.Lillend].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Lillend].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Lillend].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HallucinatoryTerrain, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Lillend].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Knock, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Lillend].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Light, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Lillend].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmPerson, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Lillend].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithAnimals, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Lillend].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpeakWithPlants, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Lillend].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: "Bardic music ability as a 6th-level Bard", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day)); //HACK: Once this is in a core project and incorporates class features as well, we will add it that way

                //testCases[CreatureConstants.Lion].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Lion_Dire].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Lizard].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Lizard_Monitor].Add(AttackHelper.BuildData(None));

                //testCases[CreatureConstants.Lizardfolk].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.HoldBreath));
                //testCases[CreatureConstants.Lizardfolk].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                //testCases[CreatureConstants.Locathah].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Longspear, requiresEquipment: true));
                //testCases[CreatureConstants.Locathah].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.LightCrossbow, requiresEquipment: true));

                testCases[CreatureConstants.Locust_Swarm].Add(AttackHelper.BuildData("Swarm", $"2d6", true, true, true, false));
                testCases[CreatureConstants.Locust_Swarm].Add(AttackHelper.BuildData("Distraction", string.Empty, true, true, true, true));

                //testCases[CreatureConstants.Magmin].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Magmin].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.MeltWeapons));

                //testCases[CreatureConstants.MantaRay].Add(AttackHelper.BuildData(None));

                //testCases[CreatureConstants.Manticore].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Manticore].Add(AttackHelper.BuildData(FeatConstants.Track));

                testCases[CreatureConstants.Marilith].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, true, false, true, false));
                testCases[CreatureConstants.Marilith].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, true, false, false, false));
                testCases[CreatureConstants.Marilith].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, true, false, false, false));
                testCases[CreatureConstants.Marilith].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, true, false, false, false));
                testCases[CreatureConstants.Marilith].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, true, false, false, false));
                testCases[CreatureConstants.Marilith].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, true, false, false, false));
                testCases[CreatureConstants.Marilith].Add(AttackHelper.BuildData("Tail Slap", $"4d6", true, true, false, false));
                testCases[CreatureConstants.Marilith].Add(AttackHelper.BuildData("Slam", $"1d8", true, true, true, false));
                testCases[CreatureConstants.Marilith].Add(AttackHelper.BuildData("Slam", $"1d8", true, true, false, false));
                testCases[CreatureConstants.Marilith].Add(AttackHelper.BuildData("Slam", $"1d8", true, true, false, false));
                testCases[CreatureConstants.Marilith].Add(AttackHelper.BuildData("Slam", $"1d8", true, true, false, false));
                testCases[CreatureConstants.Marilith].Add(AttackHelper.BuildData("Slam", $"1d8", true, true, false, false));
                testCases[CreatureConstants.Marilith].Add(AttackHelper.BuildData("Slam", $"1d8", true, true, false, false));
                testCases[CreatureConstants.Marilith].Add(AttackHelper.BuildData("Constrict", $"4d6+13", true, true, false, true));
                testCases[CreatureConstants.Marilith].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, false, true, true, true));
                testCases[CreatureConstants.Marilith].Add(AttackHelper.BuildData("Improved Grab", string.Empty, true, true, false, true));
                testCases[CreatureConstants.Marilith].Add(AttackHelper.BuildData("Summon Demon", string.Empty, false, true, true, true));

                //testCases[CreatureConstants.Marut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to chaotic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Marut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Marut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                //testCases[CreatureConstants.Marut].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                //testCases[CreatureConstants.Marut].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                //testCases[CreatureConstants.Marut].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Heavy, requiresEquipment: true));
                //testCases[CreatureConstants.Marut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AirWalk, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Marut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionDoor, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Marut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fear, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Marut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Command_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Marut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Marut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureInflictLightWounds_Mass, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Marut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateCreature, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Marut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Marut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChainLightning, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Marut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CircleOfDeath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Marut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MarkOfJustice, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Marut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfForce, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Marut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Earthquake, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week));
                //testCases[CreatureConstants.Marut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GeasQuest, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week));
                //testCases[CreatureConstants.Marut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week));

                //testCases[CreatureConstants.Medusa].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
                //testCases[CreatureConstants.Medusa].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Dagger, requiresEquipment: true));

                //testCases[CreatureConstants.Megaraptor].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Mephit_Air].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Exposed to moving air", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Mephit_Air].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Mephit_Air].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blur, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                //testCases[CreatureConstants.Mephit_Air].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GustOfWind, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Mephit_Dust].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "In arid, dusty environment", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Mephit_Dust].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Mephit_Dust].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blur, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                //testCases[CreatureConstants.Mephit_Dust].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WindWall, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Mephit_Earth].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Underground or buried up to its waist in earth", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Mephit_Earth].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Mephit_Earth].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EnlargePerson + ": self only", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                //testCases[CreatureConstants.Mephit_Earth].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SoftenEarthAndStone, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Mephit_Fire].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Touching a flame at least as large as a torch", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Mephit_Fire].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Mephit_Fire].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ScorchingRay, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                //testCases[CreatureConstants.Mephit_Fire].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HeatMetal, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Mephit_Ice].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Touching a piece of ice at least Tiny in size, or ambient temperature is freezing or lower", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Mephit_Ice].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Mephit_Ice].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicMissile, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                //testCases[CreatureConstants.Mephit_Ice].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChillMetal, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Mephit_Magma].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Touching magma, lava, or a flame at least as large as a torch", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Mephit_Magma].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Mephit_Magma].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "A pool of lava 3 feet in diameter and 6 inches deep", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                //testCases[CreatureConstants.Mephit_Magma].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Pyrotechnics, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Mephit_Ooze].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "In a wet or muddy environment", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Mephit_Ooze].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Mephit_Ooze].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AcidArrow, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                //testCases[CreatureConstants.Mephit_Ooze].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StinkingCloud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Mephit_Salt].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "In an arid environment", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Mephit_Salt].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Mephit_Salt].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Glitterdust, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                //testCases[CreatureConstants.Mephit_Salt].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: "Draw moisture from the air", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Mephit_Steam].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Touching boiling water or in a hot, humid area", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Mephit_Steam].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Mephit_Steam].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Blur, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                //testCases[CreatureConstants.Mephit_Steam].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: "Rainstorm of boiling water", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Mephit_Water].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, focus: "Exposed to rain or submerged up to its waist in water", power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Mephit_Water].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Mephit_Water].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AcidArrow, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hour));
                //testCases[CreatureConstants.Mephit_Water].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.StinkingCloud, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Merfolk].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Trident, requiresEquipment: true));
                //testCases[CreatureConstants.Merfolk].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.HeavyCrossbow, requiresEquipment: true));
                //testCases[CreatureConstants.Merfolk].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                //testCases[CreatureConstants.Merfolk].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Amphibious));
                //testCases[CreatureConstants.Merfolk].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision));

                //testCases[CreatureConstants.Mimic].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Mimic].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.MimicShape));

                //testCases[CreatureConstants.MindFlayer].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));
                //testCases[CreatureConstants.MindFlayer].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                //testCases[CreatureConstants.MindFlayer].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.MindFlayer].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.MindFlayer].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Levitate, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.MindFlayer].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.MindFlayer].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                //testCases[CreatureConstants.Minotaur].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.NaturalCunning));
                //testCases[CreatureConstants.Minotaur].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Minotaur].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greataxe, requiresEquipment: true));

                //testCases[CreatureConstants.Mohrg].Add(AttackHelper.BuildData(None));

                //testCases[CreatureConstants.Monkey].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Mule].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Mummy].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Mummy].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));

                //testCases[CreatureConstants.Naga_Dark].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                //testCases[CreatureConstants.Naga_Dark].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Any form of mind reading"));
                //testCases[CreatureConstants.Naga_Dark].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));

                //testCases[CreatureConstants.Naga_Guardian].Add(AttackHelper.BuildData(FeatConstants.EschewMaterials));

                //testCases[CreatureConstants.Naga_Spirit].Add(AttackHelper.BuildData(FeatConstants.EschewMaterials));

                //testCases[CreatureConstants.Naga_Water].Add(AttackHelper.BuildData(FeatConstants.EschewMaterials));

                testCases[CreatureConstants.Nalfeshnee].Add(AttackHelper.BuildData("Bite", $"2d8", true, true, true, false));
                testCases[CreatureConstants.Nalfeshnee].Add(AttackHelper.BuildData("Claw", $"1d8", true, true, false, false));
                testCases[CreatureConstants.Nalfeshnee].Add(AttackHelper.BuildData("Claw", $"1d8", true, true, false, false));
                testCases[CreatureConstants.Nalfeshnee].Add(AttackHelper.BuildData("Smite", string.Empty, false, true, true, true));
                testCases[CreatureConstants.Nalfeshnee].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, false, true, true, true));
                testCases[CreatureConstants.Nalfeshnee].Add(AttackHelper.BuildData("Summon Demon", string.Empty, false, true, true, true));

                //testCases[CreatureConstants.NightHag].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Any Small or Medium Humanoid", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.NightHag].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron, magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.NightHag].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                //testCases[CreatureConstants.NightHag].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                //testCases[CreatureConstants.NightHag].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Charm"));
                //testCases[CreatureConstants.NightHag].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Sleep"));
                //testCases[CreatureConstants.NightHag].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Fear"));
                //testCases[CreatureConstants.NightHag].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 25));

                //testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AversionToDaylight));
                //testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DesecratingAura));
                //testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to silver, magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                //testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 31));
                //testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                //testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));
                //testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Contagion, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Haste, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyBlight, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ConeOfCold, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Confusion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FingerOfDeath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster_Mass, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Nightmare].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AstralProjection, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Nightmare].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Etherealness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                //testCases[CreatureConstants.Nightmare_Cauchemar].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AstralProjection, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Nightmare_Cauchemar].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Etherealness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                //testCases[CreatureConstants.Nightwalker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AversionToDaylight));
                //testCases[CreatureConstants.Nightwalker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DesecratingAura));
                //testCases[CreatureConstants.Nightwalker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to silver, magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Nightwalker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                //testCases[CreatureConstants.Nightwalker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 29));
                //testCases[CreatureConstants.Nightwalker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                //testCases[CreatureConstants.Nightwalker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Contagion, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Nightwalker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Nightwalker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Nightwalker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Nightwalker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Haste, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Nightwalker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Nightwalker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyBlight, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Nightwalker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Confusion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Nightwalker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Nightwalker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Nightwalker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ConeOfCold, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Nightwalker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FingerOfDeath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Nightwalker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Nightwing].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AversionToDaylight));
                //testCases[CreatureConstants.Nightwing].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DesecratingAura));
                //testCases[CreatureConstants.Nightwing].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to silver, magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Nightwing].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                //testCases[CreatureConstants.Nightwing].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 27));
                //testCases[CreatureConstants.Nightwing].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                //testCases[CreatureConstants.Nightwing].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Contagion, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Nightwing].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Nightwing].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Nightwing].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Haste, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Nightwing].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SeeInvisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Nightwing].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyBlight, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Nightwing].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Confusion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Nightwing].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Nightwing].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Nightwing].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Nightwing].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ConeOfCold, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Nightwing].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FingerOfDeath, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Nightwing].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PlaneShift, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Nixie].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Amphibious));
                //testCases[CreatureConstants.Nixie].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Nixie].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 16));
                //testCases[CreatureConstants.Nixie].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WildEmpathy));
                //testCases[CreatureConstants.Nixie].Add(AttackHelper.BuildData(FeatConstants.Dodge, power: 1));
                //testCases[CreatureConstants.Nixie].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));
                //testCases[CreatureConstants.Nixie].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmPerson, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Nixie].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WaterBreathing, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Nixie].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
                //testCases[CreatureConstants.Nixie].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.LightCrossbow, requiresEquipment: true));

                //testCases[CreatureConstants.Nymph].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Nymph].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.UnearthlyGrace));
                //testCases[CreatureConstants.Nymph].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WildEmpathy));
                //testCases[CreatureConstants.Nymph].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionDoor, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Nymph].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Dagger, requiresEquipment: true));

                //testCases[CreatureConstants.OchreJelly].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Split));

                //testCases[CreatureConstants.Octopus].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.InkCloud));
                //testCases[CreatureConstants.Octopus].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Jet));

                //testCases[CreatureConstants.Octopus_Giant].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.InkCloud));
                //testCases[CreatureConstants.Octopus_Giant].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Jet));

                //testCases[CreatureConstants.Ogre].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                //testCases[CreatureConstants.Ogre].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
                //testCases[CreatureConstants.Ogre].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatclub, requiresEquipment: true));
                //testCases[CreatureConstants.Ogre].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                //testCases[CreatureConstants.Ogre].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));

                //testCases[CreatureConstants.Ogre_Merrow].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                //testCases[CreatureConstants.Ogre_Merrow].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
                //testCases[CreatureConstants.Ogre_Merrow].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Longspear, requiresEquipment: true));
                //testCases[CreatureConstants.Ogre_Merrow].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                //testCases[CreatureConstants.Ogre_Merrow].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));

                //testCases[CreatureConstants.OgreMage].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Any Small, Medium, or Large Humanoid or Giant", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.OgreMage].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                //testCases[CreatureConstants.OgreMage].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Regeneration, focus: "Fire and acid deal normal damage", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.OgreMage].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));
                //testCases[CreatureConstants.OgreMage].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));
                //testCases[CreatureConstants.OgreMage].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                //testCases[CreatureConstants.OgreMage].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Flight));
                //testCases[CreatureConstants.OgreMage].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 19));
                //testCases[CreatureConstants.OgreMage].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.OgreMage].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.OgreMage].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmPerson, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.OgreMage].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ConeOfCold, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.OgreMage].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GaseousForm, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.OgreMage].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Sleep, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Orc].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                //testCases[CreatureConstants.Orc].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LightSensitivity));
                //testCases[CreatureConstants.Orc].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Falchion, requiresEquipment: true));
                //testCases[CreatureConstants.Orc].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greataxe, requiresEquipment: true));
                //testCases[CreatureConstants.Orc].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
                //testCases[CreatureConstants.Orc].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));

                //testCases[CreatureConstants.Orc_Half].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.OrcBlood));

                //testCases[CreatureConstants.Otyugh].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Owl].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Owl_Giant].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LowLightVision_Superior));

                //testCases[CreatureConstants.Owlbear].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Pegasus].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Pegasus].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment + ": within 60-foot radius", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                //testCases[CreatureConstants.PhantomFungus].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility_Greater, frequencyTimePeriod: FeatConstants.Frequencies.Constant));

                //testCases[CreatureConstants.PhaseSpider].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.EtherealJaunt, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                //testCases[CreatureConstants.Phasm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "Any form Large size or smaller, including Humanoid", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Phasm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Amorphous));
                //testCases[CreatureConstants.Phasm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Phasm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));
                //testCases[CreatureConstants.Phasm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                testCases[CreatureConstants.PitFiend].Add(AttackHelper.BuildData("Claw", $"2d8", true, true, true, false));
                testCases[CreatureConstants.PitFiend].Add(AttackHelper.BuildData("Claw", $"2d8", true, true, true, false));
                testCases[CreatureConstants.PitFiend].Add(AttackHelper.BuildData("Wing", $"2d6", true, true, false, false));
                testCases[CreatureConstants.PitFiend].Add(AttackHelper.BuildData("Wing", $"2d6", true, true, false, false));
                testCases[CreatureConstants.PitFiend].Add(AttackHelper.BuildData("Bite", $"4d6 + poison + disease", true, true, false, false));
                testCases[CreatureConstants.PitFiend].Add(AttackHelper.BuildData("Tail Slap", $"2d8", true, true, false, false));
                testCases[CreatureConstants.PitFiend].Add(AttackHelper.BuildData("Constrict", $"2d8+26", true, true, false, true));
                testCases[CreatureConstants.PitFiend].Add(AttackHelper.BuildData("Fear Aura", string.Empty, false, true, false, true));
                testCases[CreatureConstants.PitFiend].Add(AttackHelper.BuildData("Improved Grab", string.Empty, true, true, false, true));
                testCases[CreatureConstants.PitFiend].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, false, true, true, true));
                testCases[CreatureConstants.PitFiend].Add(AttackHelper.BuildData("Summon Devil", string.Empty, false, true, true, true));
                testCases[CreatureConstants.PitFiend].Add(AttackHelper.BuildData("Poison", "Initial damage 1d6 Con, Secondary damage death", true, true, false, true));
                testCases[CreatureConstants.PitFiend].Add(AttackHelper.BuildData("Disease", "Devil Chills", true, true, false, true));

                //testCases[CreatureConstants.Pixie].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Pixie].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 15));
                //testCases[CreatureConstants.Pixie].Add(AttackHelper.BuildData(FeatConstants.Dodge, power: 1));
                //testCases[CreatureConstants.Pixie].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));
                //testCases[CreatureConstants.Pixie].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Pixie].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Confusion_Lesser, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Pixie].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DancingLights, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Pixie].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Pixie].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Pixie].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Pixie].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Pixie].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PermanentImage + ": visual and auditory elements only", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Pixie].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
                //testCases[CreatureConstants.Pixie].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));

                //testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 15));
                //testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(AttackHelper.BuildData(FeatConstants.Dodge, power: 1));
                //testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));
                //testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Confusion_Lesser, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DancingLights, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PermanentImage + ": visual and auditory elements only", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.IrresistibleDance, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.ShortSword, requiresEquipment: true));
                //testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));

                //testCases[CreatureConstants.Pony].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Pony_War].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Porpoise].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsight, power: 120));
                //testCases[CreatureConstants.Porpoise].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.HoldBreath));

                //testCases[CreatureConstants.PrayingMantis_Giant].Add(AttackHelper.BuildData(None));

                //testCases[CreatureConstants.Pseudodragon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 60));
                //testCases[CreatureConstants.Pseudodragon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 19));
                //testCases[CreatureConstants.Pseudodragon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 60));
                //testCases[CreatureConstants.Pseudodragon].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.PurpleWorm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                //testCases[CreatureConstants.Pyrohydra_5Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Pyrohydra_5Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Pyrohydra_5Heads].Add(AttackHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                //testCases[CreatureConstants.Pyrohydra_6Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 16, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Pyrohydra_6Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Pyrohydra_6Heads].Add(AttackHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                //testCases[CreatureConstants.Pyrohydra_7Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 17, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Pyrohydra_7Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Pyrohydra_7Heads].Add(AttackHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                //testCases[CreatureConstants.Pyrohydra_8Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 18, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Pyrohydra_8Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Pyrohydra_8Heads].Add(AttackHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                //testCases[CreatureConstants.Pyrohydra_9Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 19, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Pyrohydra_9Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Pyrohydra_9Heads].Add(AttackHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                //testCases[CreatureConstants.Pyrohydra_10Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 20, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Pyrohydra_10Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Pyrohydra_10Heads].Add(AttackHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                //testCases[CreatureConstants.Pyrohydra_11Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 21, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Pyrohydra_11Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Pyrohydra_11Heads].Add(AttackHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                //testCases[CreatureConstants.Pyrohydra_12Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 22, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Pyrohydra_12Heads].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Pyrohydra_12Heads].Add(AttackHelper.BuildData(FeatConstants.CombatReflexes, focus: "Can use all of its heads for Attacks of Opportunity"));

                testCases[CreatureConstants.Quasit].Add(AttackHelper.BuildData("Claw", $"1d3 + poison", true, true, true, false));
                testCases[CreatureConstants.Quasit].Add(AttackHelper.BuildData("Claw", $"1d3 + poison", true, true, true, false));
                testCases[CreatureConstants.Quasit].Add(AttackHelper.BuildData("Bite", $"1d4", true, true, false, false));
                testCases[CreatureConstants.Quasit].Add(AttackHelper.BuildData("Poison", $"Initial damage 1d4 Dex, Secondary damage 2d4 Dex", true, true, false, true));
                testCases[CreatureConstants.Quasit].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, false, true, true, true));

                //testCases[CreatureConstants.Rakshasa].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Any Humanoid form", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Rakshasa].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to good, piercing weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Rakshasa].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 27));
                //testCases[CreatureConstants.Rakshasa].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectThoughts, frequencyTimePeriod: FeatConstants.Frequencies.Constant));

                //testCases[CreatureConstants.Rast].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Flight));

                //testCases[CreatureConstants.Rat].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Rat].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Rat_Dire].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Rat_Dire].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Rat_Swarm].Add(AttackHelper.BuildData("Swarm", $"1d6 + disease", true, true, true, false));
                testCases[CreatureConstants.Rat_Swarm].Add(AttackHelper.BuildData("Disease", "Filth Fever", true, true, true, true));
                testCases[CreatureConstants.Rat_Swarm].Add(AttackHelper.BuildData("Distraction", string.Empty, true, true, true, true));

                //testCases[CreatureConstants.Raven].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Ravid].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                //testCases[CreatureConstants.Ravid].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Flight));
                //testCases[CreatureConstants.Ravid].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AnimateObjects, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                //testCases[CreatureConstants.RazorBoar].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "No physical vulnerabilities", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.RazorBoar].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.RazorBoar].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.RazorBoar].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));

                //testCases[CreatureConstants.Remorhaz].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Heat));
                //testCases[CreatureConstants.Remorhaz].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                testCases[CreatureConstants.Retriever].Add(AttackHelper.BuildData("Claw", $"2d6", true, true, true, false));
                testCases[CreatureConstants.Retriever].Add(AttackHelper.BuildData("Claw", $"2d6", true, true, true, false));
                testCases[CreatureConstants.Retriever].Add(AttackHelper.BuildData("Claw", $"2d6", true, true, true, false));
                testCases[CreatureConstants.Retriever].Add(AttackHelper.BuildData("Claw", $"2d6", true, true, true, false));
                testCases[CreatureConstants.Retriever].Add(AttackHelper.BuildData("Bite", $"1d8", true, true, false, false));
                testCases[CreatureConstants.Retriever].Add(AttackHelper.BuildData("Eye Ray", string.Empty, false, true, false, true));
                testCases[CreatureConstants.Retriever].Add(AttackHelper.BuildData("Find Target", string.Empty, false, true, false, true));
                testCases[CreatureConstants.Retriever].Add(AttackHelper.BuildData("Improved Grab", string.Empty, false, true, false, true));

                //testCases[CreatureConstants.Rhinoceras].Add(AttackHelper.BuildData(None));

                //testCases[CreatureConstants.Roc].Add(AttackHelper.BuildData(None));

                //testCases[CreatureConstants.Roper].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.Roper].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Roper].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 30));
                //testCases[CreatureConstants.Roper].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));

                //testCases[CreatureConstants.RustMonster].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Sahuagin].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 30));
                //testCases[CreatureConstants.Sahuagin].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FreshwaterSensitivity));
                //testCases[CreatureConstants.Sahuagin].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LightBlindness));
                //testCases[CreatureConstants.Sahuagin].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpeakWithSharks));
                //testCases[CreatureConstants.Sahuagin].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterDependent));
                //testCases[CreatureConstants.Sahuagin].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.HeavyCrossbow, requiresEquipment: true));
                //testCases[CreatureConstants.Sahuagin].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Trident, requiresEquipment: true));
                //testCases[CreatureConstants.Sahuagin].Add(AttackHelper.BuildData(FeatConstants.Monster.Multiattack));

                //testCases[CreatureConstants.Sahuagin_Mutant].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 30));
                //testCases[CreatureConstants.Sahuagin_Mutant].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FreshwaterSensitivity));
                //testCases[CreatureConstants.Sahuagin_Mutant].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LightBlindness));
                //testCases[CreatureConstants.Sahuagin_Mutant].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpeakWithSharks));
                //testCases[CreatureConstants.Sahuagin_Mutant].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterDependent));
                //testCases[CreatureConstants.Sahuagin_Mutant].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.HeavyCrossbow, requiresEquipment: true));
                //testCases[CreatureConstants.Sahuagin_Mutant].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Trident, requiresEquipment: true));
                //testCases[CreatureConstants.Sahuagin_Mutant].Add(AttackHelper.BuildData(FeatConstants.Monster.Multiattack));

                //testCases[CreatureConstants.Sahuagin_Malenti].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense, power: 30));
                //testCases[CreatureConstants.Sahuagin_Malenti].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FreshwaterSensitivity));
                //testCases[CreatureConstants.Sahuagin_Malenti].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.LightSensitivity));
                //testCases[CreatureConstants.Sahuagin_Malenti].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpeakWithSharks));
                //testCases[CreatureConstants.Sahuagin_Malenti].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WaterDependent));
                //testCases[CreatureConstants.Sahuagin_Malenti].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.HeavyCrossbow, requiresEquipment: true));
                //testCases[CreatureConstants.Sahuagin_Malenti].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Trident, requiresEquipment: true));
                //testCases[CreatureConstants.Sahuagin_Malenti].Add(AttackHelper.BuildData(FeatConstants.Monster.Multiattack));

                //testCases[CreatureConstants.Salamander_Flamebrother].Add(AttackHelper.BuildData(FeatConstants.Monster.Multiattack));

                //testCases[CreatureConstants.Salamander_Average].Add(AttackHelper.BuildData(FeatConstants.Monster.Multiattack));
                //testCases[CreatureConstants.Salamander_Average].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));

                //testCases[CreatureConstants.Salamander_Noble].Add(AttackHelper.BuildData(FeatConstants.Monster.Multiattack));
                //testCases[CreatureConstants.Salamander_Noble].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to magic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Salamander_Noble].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BurningHands, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Salamander_Noble].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fireball, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Salamander_Noble].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FlamingSphere, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Salamander_Noble].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WallOfFire, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Salamander_Noble].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Salamander_Noble].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SummonMonsterVII + ": Huge fire elemental", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Satyr].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Satyr].Add(AttackHelper.BuildData(FeatConstants.Alertness, power: 2));
                //testCases[CreatureConstants.Satyr].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
                //testCases[CreatureConstants.Satyr].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Dagger, requiresEquipment: true));

                //testCases[CreatureConstants.Satyr_WithPipes].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to cold iron weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Satyr_WithPipes].Add(AttackHelper.BuildData(FeatConstants.Alertness, power: 2));
                //testCases[CreatureConstants.Satyr_WithPipes].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Shortbow, requiresEquipment: true));
                //testCases[CreatureConstants.Satyr_WithPipes].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Dagger, requiresEquipment: true));

                //testCases[CreatureConstants.Scorpion_Monstrous_Tiny].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));
                //testCases[CreatureConstants.Scorpion_Monstrous_Tiny].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Scorpion_Monstrous_Small].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));
                //testCases[CreatureConstants.Scorpion_Monstrous_Small].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Scorpion_Monstrous_Medium].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                //testCases[CreatureConstants.Scorpion_Monstrous_Large].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                //testCases[CreatureConstants.Scorpion_Monstrous_Huge].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                //testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                //testCases[CreatureConstants.Scorpion_Monstrous_Colossal].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                //testCases[CreatureConstants.Scorpionfolk].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Scorpionfolk].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 18));
                //testCases[CreatureConstants.Scorpionfolk].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MajorImage, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Scorpionfolk].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MirrorImage, frequencyQuantity: 2, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Scorpionfolk].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Lance, requiresEquipment: true));

                //testCases[CreatureConstants.SeaCat].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.HoldBreath));
                //testCases[CreatureConstants.SeaCat].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.SeaHag].Add(AttackHelper.BuildData("Claw", $"1d4", true, true, true, false));
                testCases[CreatureConstants.SeaHag].Add(AttackHelper.BuildData("Claw", $"1d4", true, true, true, false));
                testCases[CreatureConstants.SeaHag].Add(AttackHelper.BuildData("Horrific Appearance", "2d6 Strength damage", false, true, true, true));
                testCases[CreatureConstants.SeaHag].Add(AttackHelper.BuildData("Evil Eye", string.Empty, false, true, true, true));

                //testCases[CreatureConstants.Shadow].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.TurnResistance, power: 2));

                //testCases[CreatureConstants.Shadow_Greater].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.TurnResistance, power: 2));

                //testCases[CreatureConstants.ShadowMastiff].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.ShadowBlend));
                //testCases[CreatureConstants.ShadowMastiff].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.ShadowMastiff].Add(AttackHelper.BuildData(FeatConstants.Track));

                //testCases[CreatureConstants.ShamblingMound].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 60));
                //testCases[CreatureConstants.ShamblingMound].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));
                //testCases[CreatureConstants.ShamblingMound].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                //testCases[CreatureConstants.Shark_Dire].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenScent));

                //testCases[CreatureConstants.Shark_Huge].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenScent));
                //testCases[CreatureConstants.Shark_Huge].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense));

                //testCases[CreatureConstants.Shark_Large].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenScent));
                //testCases[CreatureConstants.Shark_Large].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense));

                //testCases[CreatureConstants.Shark_Medium].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.KeenScent));
                //testCases[CreatureConstants.Shark_Medium].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsense));

                //testCases[CreatureConstants.ShieldGuardian].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.ShieldGuardian].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FindMaster));
                //testCases[CreatureConstants.ShieldGuardian].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Guard));
                //testCases[CreatureConstants.ShieldGuardian].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellStoring));
                //testCases[CreatureConstants.ShieldGuardian].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ShieldOther + ": within 100 feet of the amulet.  Does not provide spell's AC or save bonuses", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                //testCases[CreatureConstants.ShockerLizard].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.ElectricitySense));
                //testCases[CreatureConstants.ShockerLizard].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Electricity));

                //testCases[CreatureConstants.Shrieker].Add(AttackHelper.BuildData(None));

                //testCases[CreatureConstants.Skum].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Amphibious));

                //testCases[CreatureConstants.Slaad_Red].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Slaad_Red].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Sonic));
                //testCases[CreatureConstants.Slaad_Red].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Slaad_Red].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Slaad_Red].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Slaad_Red].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                //testCases[CreatureConstants.Slaad_Blue].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Slaad_Blue].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Sonic));
                //testCases[CreatureConstants.Slaad_Blue].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Slaad_Blue].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Slaad_Blue].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Slaad_Blue].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                //testCases[CreatureConstants.Slaad_Green].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Medium or Large humanoid form", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Slaad_Green].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Slaad_Green].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Sonic));
                //testCases[CreatureConstants.Slaad_Green].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Slaad_Green].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Slaad_Green].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Slaad_Green].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                //testCases[CreatureConstants.Slaad_Gray].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Any humanoid form", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Slaad_Gray].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, power: 10, focus: "Vulnerable to lawful weapons", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Slaad_Gray].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Slaad_Gray].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Sonic));
                //testCases[CreatureConstants.Slaad_Gray].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Slaad_Gray].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Slaad_Gray].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Slaad_Gray].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                //testCases[CreatureConstants.Slaad_Death].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Any humanoid form", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Slaad_Death].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, power: 10, focus: "Vulnerable to lawful weapons", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Slaad_Death].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Slaad_Death].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Sonic));
                //testCases[CreatureConstants.Slaad_Death].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Acid, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Slaad_Death].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Slaad_Death].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Slaad_Death].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Slaad_Death].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Telepathy, power: 100));

                //testCases[CreatureConstants.Snake_Constrictor].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Snake_Constrictor_Giant].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Snake_Viper_Tiny].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Snake_Viper_Small].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Snake_Viper_Medium].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Snake_Viper_Large].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Snake_Viper_Huge].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Spectre].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.TurnResistance, power: 2));
                //testCases[CreatureConstants.Spectre].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SunlightPowerlessness));
                //testCases[CreatureConstants.Spectre].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.UnnaturalAura));

                //testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));
                //testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Spider_Monstrous_Hunter_Small].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));
                //testCases[CreatureConstants.Spider_Monstrous_Hunter_Small].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));
                //testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Spider_Monstrous_Hunter_Large].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                //testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                //testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                //testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                //testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));
                //testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));
                //testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));
                //testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                //testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                //testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                //testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                //testCases[CreatureConstants.SpiderEater].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FreedomOfMovement, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                //testCases[CreatureConstants.SpiderEater].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                testCases[CreatureConstants.Spider_Swarm].Add(AttackHelper.BuildData("Swarm", $"1d6 + poison", true, true, true, false));
                testCases[CreatureConstants.Spider_Swarm].Add(AttackHelper.BuildData("Poison", "Initial and secondary damage 1d3 Str", true, true, true, true));
                testCases[CreatureConstants.Spider_Swarm].Add(AttackHelper.BuildData("Distraction", string.Empty, true, true, true, true));

                //testCases[CreatureConstants.Squid].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.InkCloud));
                //testCases[CreatureConstants.Squid].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Jet));

                //testCases[CreatureConstants.Squid_Giant].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.InkCloud));
                //testCases[CreatureConstants.Squid_Giant].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Jet));

                //testCases[CreatureConstants.StagBeetle_Giant].Add(AttackHelper.BuildData(None));

                //testCases[CreatureConstants.Stirge].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                testCases[CreatureConstants.Succubus].Add(AttackHelper.BuildData("Claw", $"1d6", true, true, true, false));
                testCases[CreatureConstants.Succubus].Add(AttackHelper.BuildData("Claw", $"1d6", true, true, true, false));
                testCases[CreatureConstants.Succubus].Add(AttackHelper.BuildData("Energy Drain", string.Empty, true, true, true, true));
                testCases[CreatureConstants.Succubus].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, false, true, true, true));
                testCases[CreatureConstants.Succubus].Add(AttackHelper.BuildData("Summon Demon", string.Empty, false, true, true, true));

                //testCases[CreatureConstants.Tarrasque].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Carapace));
                //testCases[CreatureConstants.Tarrasque].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to epic weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Tarrasque].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                //testCases[CreatureConstants.Tarrasque].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                //testCases[CreatureConstants.Tarrasque].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Disease"));
                //testCases[CreatureConstants.Tarrasque].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Energy Drain"));
                //testCases[CreatureConstants.Tarrasque].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Ability Damage"));
                //testCases[CreatureConstants.Tarrasque].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Regeneration, focus: "No form of attack deals lethal damage to the tarrasque", power: 40, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Tarrasque].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Tarrasque].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 32));

                //testCases[CreatureConstants.Tendriculos].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Regeneration, focus: "Bludgeoning weapons and acid deal normal damage", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                //testCases[CreatureConstants.Thoqqua].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));
                //testCases[CreatureConstants.Thoqqua].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Heat));

                testCases[CreatureConstants.Tiefling].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Tiefling].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Tiefling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //testCases[CreatureConstants.Tiger].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Tiger_Dire].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.OversizedWeapon, focus: SizeConstants.Gargantuan, requiresEquipment: true));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Warhammer, requiresEquipment: true));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Heavy, requiresEquipment: true));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to lawful weapons", power: 15, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 32));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ChainLightning, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureInflictCriticalWounds, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.FireStorm, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic_Greater, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Invisibility, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.InvisibilityPurge, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Levitate, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.PersistentImage, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Etherealness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.WordOfChaos, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SummonNaturesAllyIX, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Gate, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Maze, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MeteorSwarm, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Daylight, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HolySmite, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.RemoveCurse, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Restoration_Greater, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BestowCurse, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.UnholyBlight, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CrushingHand, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.ChangeShape, focus: "Any Small or Medium Humanoid", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                //testCases[CreatureConstants.Toad].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Amphibious));

                //testCases[CreatureConstants.Tojanida_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AllAroundVision));
                //testCases[CreatureConstants.Tojanida_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Tojanida_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                //testCases[CreatureConstants.Tojanida_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                //testCases[CreatureConstants.Tojanida_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Tojanida_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                //testCases[CreatureConstants.Tojanida_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AllAroundVision));
                //testCases[CreatureConstants.Tojanida_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Tojanida_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                //testCases[CreatureConstants.Tojanida_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                //testCases[CreatureConstants.Tojanida_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Tojanida_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                //testCases[CreatureConstants.Tojanida_Elder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AllAroundVision));
                //testCases[CreatureConstants.Tojanida_Elder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Acid));
                //testCases[CreatureConstants.Tojanida_Elder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                //testCases[CreatureConstants.Tojanida_Elder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                //testCases[CreatureConstants.Tojanida_Elder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Tojanida_Elder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Fire, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                //testCases[CreatureConstants.Treant].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to slashing weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Treant].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Fire));

                //testCases[CreatureConstants.Triceratops].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Triton].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SummonNaturesAllyIV, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.Troglodyte].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Club, requiresEquipment: true));
                //testCases[CreatureConstants.Troglodyte].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Simple, focus: WeaponConstants.Javelin, requiresEquipment: true));
                //testCases[CreatureConstants.Troglodyte].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 90));
                //testCases[CreatureConstants.Troglodyte].Add(AttackHelper.BuildData(FeatConstants.Monster.Multiattack));

                //testCases[CreatureConstants.Troll].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 90));
                //testCases[CreatureConstants.Troll].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Regeneration, focus: "Fire and acid deal normal damage", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Troll].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Troll_Scrag].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Darkvision, power: 90));
                //testCases[CreatureConstants.Troll_Scrag].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Regeneration, focus: "Fire and acid deal normal damage; only regenerates when immersed in water", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Troll_Scrag].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.TrumpetArchon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to evil weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.TrumpetArchon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 29));
                //testCases[CreatureConstants.TrumpetArchon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ContinualFlame, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.TrumpetArchon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.TrumpetArchon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Message, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.TrumpetArchon].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Greatsword, requiresEquipment: true));

                //testCases[CreatureConstants.Tyrannosaurus].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.UmberHulk].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                //testCases[CreatureConstants.UmberHulk_TrulyHorrid].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                //testCases[CreatureConstants.Unicorn].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MagicCircleAgainstAlignment, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                //testCases[CreatureConstants.Unicorn].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectAlignment, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Unicorn].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Teleport_Greater + ": within its forest home", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Unicorn].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureInflictLightWounds, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Unicorn].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CureInflictModerateWounds, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Unicorn].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.NeutralizePoison, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Unicorn].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.WildEmpathy));
                //testCases[CreatureConstants.Unicorn].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Unicorn].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Poison"));
                //testCases[CreatureConstants.Unicorn].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Charm"));
                //testCases[CreatureConstants.Unicorn].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Compulsion"));

                //testCases[CreatureConstants.VampireSpawn].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.TurnResistance, power: 2));
                //testCases[CreatureConstants.VampireSpawn].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to silver weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.VampireSpawn].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 2, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.VampireSpawn].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.GaseousForm, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.VampireSpawn].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.SpiderClimb, frequencyTimePeriod: FeatConstants.Frequencies.Constant));
                //testCases[CreatureConstants.VampireSpawn].Add(AttackHelper.BuildData(FeatConstants.Alertness, power: 2));
                //testCases[CreatureConstants.VampireSpawn].Add(AttackHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));
                //testCases[CreatureConstants.VampireSpawn].Add(AttackHelper.BuildData(FeatConstants.LightningReflexes, power: 2));
                //testCases[CreatureConstants.VampireSpawn].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Cold, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.VampireSpawn].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));

                //testCases[CreatureConstants.Vargouille].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.VioletFungus].Add(AttackHelper.BuildData(None));

                testCases[CreatureConstants.Vrock].Add(AttackHelper.BuildData("Claw", $"2d6", true, true, true, false));
                testCases[CreatureConstants.Vrock].Add(AttackHelper.BuildData("Claw", $"2d6", true, true, true, false));
                testCases[CreatureConstants.Vrock].Add(AttackHelper.BuildData("Bite", $"1d8", true, true, false, false));
                testCases[CreatureConstants.Vrock].Add(AttackHelper.BuildData("Talon", $"1d6", true, true, false, false));
                testCases[CreatureConstants.Vrock].Add(AttackHelper.BuildData("Talon", $"1d6", true, true, false, false));
                testCases[CreatureConstants.Vrock].Add(AttackHelper.BuildData("Dance of Ruin", string.Empty, false, true, true, true));
                testCases[CreatureConstants.Vrock].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, false, true, true, true));
                testCases[CreatureConstants.Vrock].Add(AttackHelper.BuildData("Spores", string.Empty, false, true, true, true));
                testCases[CreatureConstants.Vrock].Add(AttackHelper.BuildData("Stunning Screech", string.Empty, false, true, true, true));
                testCases[CreatureConstants.Vrock].Add(AttackHelper.BuildData("Summon Demon", string.Empty, false, true, true, true));

                //testCases[CreatureConstants.Wasp_Giant].Add(AttackHelper.BuildData(None));

                //testCases[CreatureConstants.Weasel].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Weasel].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Weasel_Dire].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Weasel_Dire].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.Whale_Baleen].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsight, power: 120));
                //testCases[CreatureConstants.Whale_Baleen].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.HoldBreath));

                //testCases[CreatureConstants.Whale_Cachalot].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsight, power: 120));
                //testCases[CreatureConstants.Whale_Cachalot].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.HoldBreath));

                //testCases[CreatureConstants.Whale_Orca].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsight, power: 120));
                //testCases[CreatureConstants.Whale_Orca].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.HoldBreath));

                //testCases[CreatureConstants.Wight].Add(AttackHelper.BuildData(None));

                //testCases[CreatureConstants.WillOWisp].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Spells and spell-like effects that allow spell resistance, except Magic Missile and Maze"));
                //testCases[CreatureConstants.WillOWisp].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.NaturalInvisibility));
                //testCases[CreatureConstants.WillOWisp].Add(AttackHelper.BuildData(FeatConstants.WeaponFinesse));

                //testCases[CreatureConstants.WinterWolf].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Wolf].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Wolf].Add(AttackHelper.BuildData(FeatConstants.Track));

                //testCases[CreatureConstants.Wolf_Dire].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Wolf_Dire].Add(AttackHelper.BuildData(FeatConstants.Track));

                //testCases[CreatureConstants.Wolverine].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Wolverine].Add(AttackHelper.BuildData(FeatConstants.Track));

                //testCases[CreatureConstants.Wolverine_Dire].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Wolverine_Dire].Add(AttackHelper.BuildData(FeatConstants.Track));

                //testCases[CreatureConstants.Worg].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Wraith].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DaylightPowerlessness));
                //testCases[CreatureConstants.Wraith].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.TurnResistance, power: 2));
                //testCases[CreatureConstants.Wraith].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.UnnaturalAura));
                //testCases[CreatureConstants.Wraith].Add(AttackHelper.BuildData(FeatConstants.Alertness, power: 2));
                //testCases[CreatureConstants.Wraith].Add(AttackHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));

                //testCases[CreatureConstants.Wraith_Dread].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Lifesense, power: 60));
                //testCases[CreatureConstants.Wraith_Dread].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DaylightPowerlessness));
                //testCases[CreatureConstants.Wraith_Dread].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.TurnResistance, power: 2));
                //testCases[CreatureConstants.Wraith_Dread].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.UnnaturalAura));
                //testCases[CreatureConstants.Wraith_Dread].Add(AttackHelper.BuildData(FeatConstants.Alertness, power: 2));
                //testCases[CreatureConstants.Wraith_Dread].Add(AttackHelper.BuildData(FeatConstants.Initiative_Improved, power: 4));

                //testCases[CreatureConstants.Wyvern].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.Wyvern].Add(AttackHelper.BuildData(FeatConstants.Monster.Multiattack));

                //testCases[CreatureConstants.Xill].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 21));
                //testCases[CreatureConstants.Xill].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Planewalk));
                //testCases[CreatureConstants.Xill].Add(AttackHelper.BuildData(FeatConstants.Monster.Multiattack));

                //testCases[CreatureConstants.Xorn_Minor].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AllAroundVision));
                //testCases[CreatureConstants.Xorn_Minor].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EarthGlide));
                //testCases[CreatureConstants.Xorn_Minor].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to bludgeoning weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Xorn_Minor].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                //testCases[CreatureConstants.Xorn_Minor].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                //testCases[CreatureConstants.Xorn_Minor].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Xorn_Minor].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));

                //testCases[CreatureConstants.Xorn_Average].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AllAroundVision));
                //testCases[CreatureConstants.Xorn_Average].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EarthGlide));
                //testCases[CreatureConstants.Xorn_Average].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to bludgeoning weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Xorn_Average].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                //testCases[CreatureConstants.Xorn_Average].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                //testCases[CreatureConstants.Xorn_Average].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Xorn_Average].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));
                //testCases[CreatureConstants.Xorn_Average].Add(AttackHelper.BuildData(FeatConstants.Cleave));

                //testCases[CreatureConstants.Xorn_Elder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AllAroundVision));
                //testCases[CreatureConstants.Xorn_Elder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EarthGlide));
                //testCases[CreatureConstants.Xorn_Elder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to bludgeoning weapons", power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Xorn_Elder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Fire));
                //testCases[CreatureConstants.Xorn_Elder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: FeatConstants.Foci.Elements.Cold));
                //testCases[CreatureConstants.Xorn_Elder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.EnergyResistance, focus: FeatConstants.Foci.Elements.Electricity, power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Round));
                //testCases[CreatureConstants.Xorn_Elder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Tremorsense, power: 60));
                //testCases[CreatureConstants.Xorn_Elder].Add(AttackHelper.BuildData(FeatConstants.Cleave));

                //testCases[CreatureConstants.YethHound].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to silver weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.YethHound].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Flight));
                //testCases[CreatureConstants.YethHound].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));

                //testCases[CreatureConstants.Yrthak].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Blindsight, power: 120));
                //testCases[CreatureConstants.Yrthak].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Gaze attacks"));
                //testCases[CreatureConstants.Yrthak].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Visual effects"));
                //testCases[CreatureConstants.Yrthak].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Illusions"));
                //testCases[CreatureConstants.Yrthak].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Immunity, focus: "Attacks that rely on sight"));
                //testCases[CreatureConstants.Yrthak].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Vulnerability, focus: FeatConstants.Foci.Elements.Sonic));

                //testCases[CreatureConstants.YuanTi_Pureblood].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Scimitar, requiresEquipment: true));
                //testCases[CreatureConstants.YuanTi_Pureblood].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Longbow, requiresEquipment: true));
                //testCases[CreatureConstants.YuanTi_Pureblood].Add(AttackHelper.BuildData(FeatConstants.Alertness, power: 2));
                //testCases[CreatureConstants.YuanTi_Pureblood].Add(AttackHelper.BuildData(FeatConstants.BlindFight, power: 2));
                //testCases[CreatureConstants.YuanTi_Pureblood].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "a Tiny to Large viper", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.YuanTi_Pureblood].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 14));
                //testCases[CreatureConstants.YuanTi_Pureblood].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectPoison, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.YuanTi_Pureblood].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AnimalTrance, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.YuanTi_Pureblood].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CauseFear, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.YuanTi_Pureblood].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CharmPerson, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.YuanTi_Pureblood].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Darkness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.YuanTi_Pureblood].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.YuanTi_Halfblood].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Scimitar, requiresEquipment: true));
                //testCases[CreatureConstants.YuanTi_Halfblood].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
                //testCases[CreatureConstants.YuanTi_Halfblood].Add(AttackHelper.BuildData(FeatConstants.Alertness, power: 2));
                //testCases[CreatureConstants.YuanTi_Halfblood].Add(AttackHelper.BuildData(FeatConstants.BlindFight, power: 2));
                //testCases[CreatureConstants.YuanTi_Halfblood].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "a Tiny to Large viper", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.YuanTi_Halfblood].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.ChameleonPower));
                //testCases[CreatureConstants.YuanTi_Halfblood].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.YuanTi_Halfblood].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 16));
                //testCases[CreatureConstants.YuanTi_Halfblood].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectPoison, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.YuanTi_Halfblood].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AnimalTrance, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.YuanTi_Halfblood].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.CauseFear, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.YuanTi_Halfblood].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.YuanTi_Halfblood].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.NeutralizePoison, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.YuanTi_Halfblood].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.YuanTi_Halfblood].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));

                //testCases[CreatureConstants.YuanTi_Abomination].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.Scimitar, requiresEquipment: true));
                //testCases[CreatureConstants.YuanTi_Abomination].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Martial, focus: WeaponConstants.CompositeLongbow, requiresEquipment: true));
                //testCases[CreatureConstants.YuanTi_Abomination].Add(AttackHelper.BuildData(FeatConstants.Alertness, power: 2));
                //testCases[CreatureConstants.YuanTi_Abomination].Add(AttackHelper.BuildData(FeatConstants.BlindFight, power: 2));
                //testCases[CreatureConstants.YuanTi_Abomination].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.AlternateForm, focus: "a Tiny to Large viper", frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.YuanTi_Abomination].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.ChameleonPower));
                //testCases[CreatureConstants.YuanTi_Abomination].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Scent));
                //testCases[CreatureConstants.YuanTi_Abomination].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 18));
                //testCases[CreatureConstants.YuanTi_Abomination].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DetectPoison, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.YuanTi_Abomination].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.AnimalTrance, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.YuanTi_Abomination].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Suggestion, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.YuanTi_Abomination].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.NeutralizePoison, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.YuanTi_Abomination].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DeeperDarkness, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.YuanTi_Abomination].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.BalefulPolymorph + ": into snake form only", frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.YuanTi_Abomination].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fear, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.YuanTi_Abomination].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Entangle, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));

                //testCases[CreatureConstants.Zelekhut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.DamageReduction, focus: "Vulnerable to chaotic weapons", power: 10, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Hit));
                //testCases[CreatureConstants.Zelekhut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.FastHealing, power: 5, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Turn));
                //testCases[CreatureConstants.Zelekhut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellResistance, power: 20));
                //testCases[CreatureConstants.Zelekhut].Add(AttackHelper.BuildData(FeatConstants.WeaponProficiency_Exotic, focus: WeaponConstants.SpikedChain, requiresEquipment: true));
                //testCases[CreatureConstants.Zelekhut].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Light, requiresEquipment: true));
                //testCases[CreatureConstants.Zelekhut].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Medium, requiresEquipment: true));
                //testCases[CreatureConstants.Zelekhut].Add(AttackHelper.BuildData(FeatConstants.ArmorProficiency_Heavy, requiresEquipment: true));
                //testCases[CreatureConstants.Zelekhut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.ClairaudienceClairvoyance, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Zelekhut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DimensionalAnchor, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Zelekhut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.DispelMagic, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Zelekhut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Fear, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Zelekhut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldPerson, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Zelekhut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.LocateCreature, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Zelekhut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.TrueSeeing, frequencyTimePeriod: FeatConstants.Frequencies.AtWill));
                //testCases[CreatureConstants.Zelekhut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.HoldMonster, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Zelekhut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.MarkOfJustice, frequencyQuantity: 3, frequencyTimePeriod: FeatConstants.Frequencies.Day));
                //testCases[CreatureConstants.Zelekhut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Earthquake, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week));
                //testCases[CreatureConstants.Zelekhut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, focus: SpellConstants.Geas_Lesser, frequencyQuantity: 1, frequencyTimePeriod: FeatConstants.Frequencies.Week));
                //testCases[CreatureConstants.Zelekhut].Add(AttackHelper.BuildData(FeatConstants.MountedCombat));

                return TestDataHelper.EnumerateTestCases(testCases);
            }
        }

        private static class TestDataHelper
        {
            public static IEnumerable EnumerateTestCases(Dictionary<string, List<string[]>> testCases)
            {
                foreach (var testCase in testCases)
                {
                    yield return new TestCaseData(testCase.Key, testCase.Value);
                }
            }
        }
    }
}
