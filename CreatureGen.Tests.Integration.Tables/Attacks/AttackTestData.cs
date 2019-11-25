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
                testCases[CreatureConstants.Aasimar].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Aasimar].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Aboleth].Add(AttackHelper.BuildData("Tentacle", $"1d6", "Slime", 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Aboleth].Add(AttackHelper.BuildData("Enslave", string.Empty, string.Empty, 0, "supernatural ability", 3, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Aboleth].Add(AttackHelper.BuildData("Slime", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Aboleth].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Psionic, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Achaierai].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Achaierai].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Achaierai].Add(AttackHelper.BuildData("Black cloud", "2d6", "insanity", 0, "extraordinary ability", 3, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Allip].Add(AttackHelper.BuildData("Incorporeal touch", string.Empty, $"1d4 Wisdom drain", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Allip].Add(AttackHelper.BuildData("Babble", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Allip].Add(AttackHelper.BuildData("Madness", string.Empty, "1d4 Wisdom drain", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));
                testCases[CreatureConstants.Allip].Add(AttackHelper.BuildData("Wisdom drain", "1d4", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));

                testCases[CreatureConstants.Androsphinx].Add(AttackHelper.BuildData("Claw", $"2d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Androsphinx].Add(AttackHelper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Androsphinx].Add(AttackHelper.BuildData("Rake", $"2d4", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Androsphinx].Add(AttackHelper.BuildData("Roar", string.Empty, string.Empty, 0, "supernatural ability", 3, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Androsphinx].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Angel_AstralDeva].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Angel_AstralDeva].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Angel_AstralDeva].Add(AttackHelper.BuildData("Stun", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Angel_AstralDeva].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Angel_Planetar].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Angel_Planetar].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Angel_Planetar].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Angel_Planetar].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Angel_Solar].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Angel_Solar].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Angel_Solar].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Angel_Solar].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Angel_Solar].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Anvil_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Anvil_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Anvil_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Anvil_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Anvil_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Anvil_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Anvil_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Anvil_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Anvil_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Anvil_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Anvil_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Stone_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Stone_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Stone_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Stone_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Wood_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Wood_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Wood_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Wood_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Box_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Box_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Box_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Box_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Box_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Box_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Box_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Box_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Box_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Box_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Box_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Tiny].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Tiny].Add(AttackHelper.BuildData("Constrict", $"1d3", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Small].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Small].Add(AttackHelper.BuildData("Constrict", $"1d4", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Medium].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Medium].Add(AttackHelper.BuildData("Constrict", $"1d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Large].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Large].Add(AttackHelper.BuildData("Constrict", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Huge].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Huge].Add(AttackHelper.BuildData("Constrict", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Gargantuan].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Gargantuan].Add(AttackHelper.BuildData("Constrict", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Colossal].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Colossal].Add(AttackHelper.BuildData("Constrict", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carriage_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Carriage_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Carriage_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Carriage_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carriage_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carriage_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carriage_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carriage_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carriage_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carriage_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carriage_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Tiny].Add(AttackHelper.BuildData("Constrict", $"1d3", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Small].Add(AttackHelper.BuildData("Constrict", $"1d4", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Medium].Add(AttackHelper.BuildData("Constrict", $"1d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Large].Add(AttackHelper.BuildData("Constrict", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Chain_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Huge].Add(AttackHelper.BuildData("Constrict", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Chain_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Gargantuan].Add(AttackHelper.BuildData("Constrict", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Chain_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Colossal].Add(AttackHelper.BuildData("Constrict", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Chain_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chair_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Chair_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Chair_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Chair_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chair_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chair_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chair_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chair_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chair_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chair_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chair_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Tiny].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Tiny].Add(AttackHelper.BuildData("Constrict", $"1d3", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Small].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Small].Add(AttackHelper.BuildData("Constrict", $"1d4", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Medium].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Medium].Add(AttackHelper.BuildData("Constrict", $"1d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Large].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Large].Add(AttackHelper.BuildData("Constrict", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Huge].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Huge].Add(AttackHelper.BuildData("Constrict", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Gargantuan].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Gargantuan].Add(AttackHelper.BuildData("Constrict", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Colossal].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Colossal].Add(AttackHelper.BuildData("Constrict", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Ladder_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Ladder_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Ladder_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Ladder_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Ladder_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Ladder_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Ladder_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Ladder_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Ladder_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Ladder_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Ladder_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Tiny].Add(AttackHelper.BuildData("Constrict", $"1d3", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Small].Add(AttackHelper.BuildData("Constrict", $"1d4", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Medium].Add(AttackHelper.BuildData("Constrict", $"1d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Large].Add(AttackHelper.BuildData("Constrict", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Huge].Add(AttackHelper.BuildData("Constrict", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Gargantuan].Add(AttackHelper.BuildData("Constrict", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Colossal].Add(AttackHelper.BuildData("Constrict", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Tiny].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Tiny].Add(AttackHelper.BuildData("Constrict", $"1d3", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Small].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Small].Add(AttackHelper.BuildData("Constrict", $"1d4", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Medium].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Medium].Add(AttackHelper.BuildData("Constrict", $"1d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Large].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Large].Add(AttackHelper.BuildData("Constrict", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Huge].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Huge].Add(AttackHelper.BuildData("Constrict", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Gargantuan].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Gargantuan].Add(AttackHelper.BuildData("Constrict", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Colossal].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Colossal].Add(AttackHelper.BuildData("Constrict", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Sled_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Sled_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Sled_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Sled_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Sled_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Sled_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Sled_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Sled_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Sled_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Sled_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Sled_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Stool_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Stool_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Stool_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Stool_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Stool_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Stool_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Stool_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Stool_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Stool_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Stool_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Stool_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Table_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Table_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Table_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Table_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Table_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Table_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Table_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Table_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Table_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Table_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Table_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Tiny].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Tiny].Add(AttackHelper.BuildData("Constrict", $"1d3", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Small].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Small].Add(AttackHelper.BuildData("Constrict", $"1d4", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Medium].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Medium].Add(AttackHelper.BuildData("Constrict", $"1d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Large].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Large].Add(AttackHelper.BuildData("Constrict", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Huge].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Huge].Add(AttackHelper.BuildData("Constrict", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Gargantuan].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Gargantuan].Add(AttackHelper.BuildData("Constrict", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Colossal].Add(AttackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Colossal].Add(AttackHelper.BuildData("Constrict", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Wagon_Tiny].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Wagon_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Wagon_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Wagon_Large].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Wagon_Large].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Wagon_Huge].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Wagon_Huge].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Wagon_Gargantuan].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Wagon_Gargantuan].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Wagon_Colossal].Add(AttackHelper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Wagon_Colossal].Add(AttackHelper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Ankheg].Add(AttackHelper.BuildData("Bite", $"2d6 + 1d4 acid", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ankheg].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Ankheg].Add(AttackHelper.BuildData("Spit Acid", $"4d4 acid", string.Empty, 0, "extraordinary ability", 1, $"6 {FeatConstants.Frequencies.Hour}", false, true, true, true));

                testCases[CreatureConstants.Annis].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Annis].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Annis].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Annis].Add(AttackHelper.BuildData("Rake", $"1d6", string.Empty, 1, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Annis].Add(AttackHelper.BuildData("Rend", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Annis].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Ant_Giant_Worker].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ant_Giant_Worker].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Ant_Giant_Soldier].Add(AttackHelper.BuildData("Bite", $"2d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ant_Giant_Soldier].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Ant_Giant_Soldier].Add(AttackHelper.BuildData("Acid Sting", "1d4 Piercing damage + 1d4 acid damage", string.Empty, 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Ant_Giant_Queen].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ant_Giant_Queen].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Ape].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ape].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Ape_Dire].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ape_Dire].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Ape_Dire].Add(AttackHelper.BuildData("Rend", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Aranea].Add(AttackHelper.BuildData("Bite", $"1d6", "poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Aranea].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial damage 1d6 Str, Secondary damage 2d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, false, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Aranea].Add(AttackHelper.BuildData("Web", string.Empty, string.Empty, 0, "ranged, extraordinary ability", 6, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.Aranea].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Arrowhawk_Juvenile].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Arrowhawk_Juvenile].Add(AttackHelper.BuildData("Electricity ray", $"2d6", string.Empty, 0, "ranged touch, supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Arrowhawk_Adult].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Arrowhawk_Adult].Add(AttackHelper.BuildData("Electricity ray", $"2d8", string.Empty, 0, "ranged touch, supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Arrowhawk_Elder].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Arrowhawk_Elder].Add(AttackHelper.BuildData("Electricity ray", $"2d8", string.Empty, 0, "ranged touch, supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.AssassinVine].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AssassinVine].Add(AttackHelper.BuildData("Constrict", $"1d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AssassinVine].Add(AttackHelper.BuildData("Entangle", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.AssassinVine].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Athach].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Athach].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, false, false, false));
                testCases[CreatureConstants.Athach].Add(AttackHelper.BuildData("Bite", $"2d8", "poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Athach].Add(AttackHelper.BuildData("Rock", $"2d8", string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Athach].Add(AttackHelper.BuildData("Rock", $"2d8", string.Empty, 0.5, "ranged", 2, FeatConstants.Frequencies.Round, false, true, false, false));
                testCases[CreatureConstants.Athach].Add(AttackHelper.BuildData("Poison", string.Empty, $"Initial damage 1d6 Str, Secondary damage 2d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Avoral].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Avoral].Add(AttackHelper.BuildData("Wing", $"2d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Avoral].Add(AttackHelper.BuildData("Fear Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Avoral].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Azer].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Azer].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Azer].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Azer].Add(AttackHelper.BuildData("Heat", "1 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));

                testCases[CreatureConstants.Babau].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Babau].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Babau].Add(AttackHelper.BuildData("Sneak Attack", $"2d6", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Babau].Add(AttackHelper.BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.Babau].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Baboon].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Badger].Add(AttackHelper.BuildData("Claw", $"1d2", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Badger].Add(AttackHelper.BuildData("Bite", $"1d3", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Badger].Add(AttackHelper.BuildData("Rage", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Badger_Dire].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Badger_Dire].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Badger_Dire].Add(AttackHelper.BuildData("Rage", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Balor].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Balor].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, false, false));
                testCases[CreatureConstants.Balor].Add(AttackHelper.BuildData("Slam", $"1d10", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Balor].Add(AttackHelper.BuildData("Death Throes", "100", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Life, false, true, false, true, SaveConstants.Reflex, AbilityConstants.Constitution));
                testCases[CreatureConstants.Balor].Add(AttackHelper.BuildData("Entangle", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, false, false, true));
                testCases[CreatureConstants.Balor].Add(AttackHelper.BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.Balor].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(AttackHelper.BuildData("Claw", $"2d8", "fear", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(AttackHelper.BuildData("Fear", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(AttackHelper.BuildData("Impale", "3d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(AttackHelper.BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Barghest].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Barghest].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Barghest].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Barghest].Add(AttackHelper.BuildData("Feed", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Barghest_Greater].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Barghest_Greater].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Barghest_Greater].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Barghest_Greater].Add(AttackHelper.BuildData("Feed", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Basilisk].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Basilisk].Add(AttackHelper.BuildData("Petrifying Gaze", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));

                testCases[CreatureConstants.Basilisk_AbyssalGreater].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Basilisk_AbyssalGreater].Add(AttackHelper.BuildData("Petrifying Gaze", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.Basilisk_AbyssalGreater].Add(AttackHelper.BuildData("Smite Good", "18", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Bat].Add(new[] { None });

                testCases[CreatureConstants.Bat_Dire].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Bat_Swarm].Add(AttackHelper.BuildData("Swarm", $"1d6", string.Empty, 0, "swarm", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bat_Swarm].Add(AttackHelper.BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Bat_Swarm].Add(AttackHelper.BuildData("Wounding", "1", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));

                testCases[CreatureConstants.Bear_Black].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bear_Black].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Bear_Brown].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bear_Brown].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Bear_Brown].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Bear_Dire].Add(AttackHelper.BuildData("Claw", $"2d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bear_Dire].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Bear_Dire].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Bear_Polar].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bear_Polar].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Bear_Polar].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(AttackHelper.BuildData("Claw", $"2d8", "Infernal Wound", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(AttackHelper.BuildData("Infernal Wound", "2", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, string.Empty, AbilityConstants.Constitution));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(AttackHelper.BuildData("Beard", "1d8", "Disease", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(AttackHelper.BuildData("Battle Frenzy", string.Empty, string.Empty, 0, "extraordinary ability", 2, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(AttackHelper.BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(AttackHelper.BuildData("Disease", string.Empty, "Devil Chills", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(AttackHelper.BuildData("Devil Chills", string.Empty, "incubation period 1d4 days, damage 1d4 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Bebilith].Add(AttackHelper.BuildData("Bite", $"2d6", "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bebilith].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Bebilith].Add(AttackHelper.BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 4, FeatConstants.Frequencies.Day, false, true, true, true, string.Empty, AbilityConstants.Constitution));
                testCases[CreatureConstants.Bebilith].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial damage 1d6 Con, Secondary damage 2d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Bebilith].Add(AttackHelper.BuildData("Rend Armor", $"4d6", string.Empty, 2, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Bee_Giant].Add(AttackHelper.BuildData("Sting", $"1d4", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Life, true, true, true, false));
                testCases[CreatureConstants.Bee_Giant].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d4 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Behir].Add(AttackHelper.BuildData("Bite", $"2d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Behir].Add(AttackHelper.BuildData("Breath Weapon", "7d6 electricity", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
                testCases[CreatureConstants.Behir].Add(AttackHelper.BuildData("Constrict", "2d8", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Behir].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Behir].Add(AttackHelper.BuildData("Rake", "1d4", string.Empty, 0.5, "extraordinary ability", 6, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Behir].Add(AttackHelper.BuildData("Swallow Whole", "2d8 bludgeoning + 8 acid", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Beholder].Add(AttackHelper.BuildData("Bite", $"2d4", string.Empty, .5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Beholder].Add(AttackHelper.BuildData("Eye ray", string.Empty, SpellConstants.CharmMonster, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(AttackHelper.BuildData("Eye ray", string.Empty, SpellConstants.CharmPerson, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(AttackHelper.BuildData("Eye ray", string.Empty, SpellConstants.CureInflictModerateWounds, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(AttackHelper.BuildData("Eye ray", string.Empty, SpellConstants.Disintegrate, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(AttackHelper.BuildData("Eye ray", string.Empty, SpellConstants.Fear, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(AttackHelper.BuildData("Eye ray", string.Empty, SpellConstants.FingerOfDeath, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(AttackHelper.BuildData("Eye ray", string.Empty, SpellConstants.FleshToStone, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(AttackHelper.BuildData("Eye ray", string.Empty, SpellConstants.Sleep, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(AttackHelper.BuildData("Eye ray", string.Empty, SpellConstants.Slow, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(AttackHelper.BuildData("Eye ray", string.Empty, SpellConstants.Telekinesis, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));

                testCases[CreatureConstants.Beholder_Gauth].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, .5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Beholder_Gauth].Add(AttackHelper.BuildData("Eye ray", string.Empty, SpellConstants.Sleep, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder_Gauth].Add(AttackHelper.BuildData("Eye ray", string.Empty, SpellConstants.CureInflictModerateWounds, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder_Gauth].Add(AttackHelper.BuildData("Eye ray", string.Empty, SpellConstants.DispelMagic, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Beholder_Gauth].Add(AttackHelper.BuildData("Eye ray", string.Empty, SpellConstants.ScorchingRay, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Beholder_Gauth].Add(AttackHelper.BuildData("Eye ray", string.Empty, "Paralysis", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder_Gauth].Add(AttackHelper.BuildData("Eye ray", string.Empty, SpellConstants.RayOfExhaustion, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Belker].Add(AttackHelper.BuildData("Wing", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Belker].Add(AttackHelper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Belker].Add(AttackHelper.BuildData("Claw", $"1d3", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Belker].Add(AttackHelper.BuildData("Smoke Claw", $"3d4", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Bison].Add(AttackHelper.BuildData("Gore", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bison].Add(AttackHelper.BuildData("Stampede", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, SaveConstants.Reflex, AbilityConstants.Strength));

                testCases[CreatureConstants.BlackPudding].Add(AttackHelper.BuildData("Slam", $"2d6 + 2d6 acid", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.BlackPudding].Add(AttackHelper.BuildData("Acid", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
                testCases[CreatureConstants.BlackPudding].Add(AttackHelper.BuildData("Constrict", $"2d6 + 2d6 acid", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.BlackPudding].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.BlackPudding_Elder].Add(AttackHelper.BuildData("Slam", $"3d6 + 3d6 acid", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.BlackPudding_Elder].Add(AttackHelper.BuildData("Acid", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
                testCases[CreatureConstants.BlackPudding_Elder].Add(AttackHelper.BuildData("Constrict", $"2d8 + 2d6 acid", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.BlackPudding_Elder].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.BlinkDog].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Boar].Add(AttackHelper.BuildData("Gore", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Boar].Add(AttackHelper.BuildData("Ferocity", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Boar_Dire].Add(AttackHelper.BuildData("Gore", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Boar_Dire].Add(AttackHelper.BuildData("Ferocity", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Bodak].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bodak].Add(AttackHelper.BuildData("Death Gaze", string.Empty, "Death", 1.5, "melee", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));

                testCases[CreatureConstants.BombardierBeetle_Giant].Add(AttackHelper.BuildData("Bite", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.BombardierBeetle_Giant].Add(AttackHelper.BuildData("Acid Spray", $"1d4 acid", string.Empty, 2, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, false, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.BoneDevil_Osyluth].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(AttackHelper.BuildData("Sting", $"3d4", "Poison", 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(AttackHelper.BuildData("Fear Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial damage 1d6 Str, Secondary damage 2d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(AttackHelper.BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Bralani].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Bralani].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Bralani].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bralani].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Bralani].Add(AttackHelper.BuildData("Whirlwind blast", $"3d6", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Bugbear].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Bugbear].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Bugbear].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Bulette].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bulette].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Bulette].Add(AttackHelper.BuildData("Leap", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Camel_Bactrian].Add(AttackHelper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Camel_Dromedary].Add(AttackHelper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.CarrionCrawler].Add(AttackHelper.BuildData("Tentacle", string.Empty, "Paralysis", 0, "melee", 8, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.CarrionCrawler].Add(AttackHelper.BuildData("Bite", "1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.CarrionCrawler].Add(AttackHelper.BuildData("Paralysis", string.Empty, "paralyzed for 2d4 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Cat].Add(AttackHelper.BuildData("Claw", $"1d2", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cat].Add(AttackHelper.BuildData("Bite", $"1d3", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Centaur].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Centaur].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Centaur].Add(AttackHelper.BuildData("Unarmed Strike", "1d4", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Centaur].Add(AttackHelper.BuildData("Hoof", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Centipede_Monstrous_Tiny].Add(AttackHelper.BuildData("Bite", $"1d3", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Centipede_Monstrous_Tiny].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Centipede_Monstrous_Small].Add(AttackHelper.BuildData("Bite", $"1d4", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Centipede_Monstrous_Small].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d2 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Centipede_Monstrous_Medium].Add(AttackHelper.BuildData("Bite", $"1d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Centipede_Monstrous_Medium].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d3 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Centipede_Monstrous_Large].Add(AttackHelper.BuildData("Bite", $"1d8", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Centipede_Monstrous_Large].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d4 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Centipede_Monstrous_Huge].Add(AttackHelper.BuildData("Bite", $"2d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Centipede_Monstrous_Huge].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Centipede_Monstrous_Gargantuan].Add(AttackHelper.BuildData("Bite", $"2d8", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Centipede_Monstrous_Gargantuan].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d8 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Centipede_Monstrous_Colossal].Add(AttackHelper.BuildData("Bite", $"4d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Centipede_Monstrous_Colossal].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 2d6 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Centipede_Swarm].Add(AttackHelper.BuildData("Swarm", $"2d6", "Poison", 0, "swarm", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Centipede_Swarm].Add(AttackHelper.BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Centipede_Swarm].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d4 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.ChainDevil_Kyton].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.ChainDevil_Kyton].Add(AttackHelper.BuildData("Dancing Chains", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.ChainDevil_Kyton].Add(AttackHelper.BuildData("Unnerving Gaze", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));

                testCases[CreatureConstants.ChaosBeast].Add(AttackHelper.BuildData("Claw", $"1d3", "Corporeal Instability", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.ChaosBeast].Add(AttackHelper.BuildData("Corporeal Instability", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Cheetah].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Cheetah].Add(AttackHelper.BuildData("Claw", $"1d2", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cheetah].Add(AttackHelper.BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Chimera_Black].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Black].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Black].Add(AttackHelper.BuildData("Gore", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Black].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Chimera_Black].Add(AttackHelper.BuildData("Breath weapon", $"3d8 acid", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Chimera_Blue].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Blue].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Blue].Add(AttackHelper.BuildData("Gore", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Blue].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Chimera_Blue].Add(AttackHelper.BuildData("Breath weapon", $"3d8 electricity", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Chimera_Green].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Green].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Green].Add(AttackHelper.BuildData("Gore", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Green].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Chimera_Green].Add(AttackHelper.BuildData("Breath weapon", $"3d8 acid", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Chimera_Red].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Red].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Red].Add(AttackHelper.BuildData("Gore", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Red].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Chimera_Red].Add(AttackHelper.BuildData("Breath weapon", $"3d8 fire", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Chimera_White].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_White].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_White].Add(AttackHelper.BuildData("Gore", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_White].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Chimera_White].Add(AttackHelper.BuildData("Breath weapon", $"3d8 cold", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Choker].Add(AttackHelper.BuildData("Tentacle", $"1d3", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Choker].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Choker].Add(AttackHelper.BuildData("Constrict", $"1d3", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Chuul].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chuul].Add(AttackHelper.BuildData("Constrict", $"3d6", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Chuul].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Chuul].Add(AttackHelper.BuildData("Paralytic Tentacles", "1d8", "6 round paralysis", 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Cloaker].Add(AttackHelper.BuildData("Tail slap", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cloaker].Add(AttackHelper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cloaker].Add(AttackHelper.BuildData("Moan", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma));
                testCases[CreatureConstants.Cloaker].Add(AttackHelper.BuildData("Engulf", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Cockatrice].Add(AttackHelper.BuildData("Bite", $"1d4", "Petrification", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cockatrice].Add(AttackHelper.BuildData("Petrification", string.Empty, string.Empty, 1, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Couatl].Add(AttackHelper.BuildData("Bite", $"1d3", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Couatl].Add(AttackHelper.BuildData("Poison", string.Empty, "Injury, initial damage 2d4 Str, secondary damage 4d4 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, false, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Couatl].Add(AttackHelper.BuildData("Constrict", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Couatl].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Couatl].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Psionic, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Couatl].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Criosphinx].Add(AttackHelper.BuildData("Gore", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Criosphinx].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Criosphinx].Add(AttackHelper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Criosphinx].Add(AttackHelper.BuildData("Rake", $"1d6", string.Empty, 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Crocodile].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Crocodile].Add(AttackHelper.BuildData("Tail slap", $"1d12", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Crocodile].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Crocodile_Giant].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Crocodile_Giant].Add(AttackHelper.BuildData("Tail slap", $"1d12", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Crocodile_Giant].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Cryohydra_5Heads].Add(AttackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 5, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cryohydra_5Heads].Add(AttackHelper.BuildData("Breath weapon", $"3d6 cold per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Cryohydra_6Heads].Add(AttackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 6, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cryohydra_6Heads].Add(AttackHelper.BuildData("Breath weapon", $"3d6 cold per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Cryohydra_7Heads].Add(AttackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 7, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cryohydra_7Heads].Add(AttackHelper.BuildData("Breath weapon", $"3d6 cold per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Cryohydra_8Heads].Add(AttackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 8, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cryohydra_8Heads].Add(AttackHelper.BuildData("Breath weapon", $"3d6 cold per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Cryohydra_9Heads].Add(AttackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 9, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cryohydra_9Heads].Add(AttackHelper.BuildData("Breath weapon", $"3d6 cold per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Cryohydra_10Heads].Add(AttackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 10, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cryohydra_10Heads].Add(AttackHelper.BuildData("Breath weapon", $"3d6 cold per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Cryohydra_11Heads].Add(AttackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 11, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cryohydra_11Heads].Add(AttackHelper.BuildData("Breath weapon", $"3d6 cold per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Cryohydra_12Heads].Add(AttackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 12, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cryohydra_12Heads].Add(AttackHelper.BuildData("Breath weapon", $"3d6 cold per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Darkmantle].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Darkmantle].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Darkmantle].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Darkmantle].Add(AttackHelper.BuildData("Constrict", $"1d4", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Deinonychus].Add(AttackHelper.BuildData("Talons", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Deinonychus].Add(AttackHelper.BuildData("Foreclaw", $"1d3", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Deinonychus].Add(AttackHelper.BuildData("Bite", $"2d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Deinonychus].Add(AttackHelper.BuildData("Pounce", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Delver].Add(AttackHelper.BuildData("Slam", $"1d6 bludgeoning + 2d6 acid", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Delver].Add(AttackHelper.BuildData("Corrosive Slime", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Derro].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Derro].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Derro].Add(AttackHelper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Derro].Add(AttackHelper.BuildData("Poison use", string.Empty, "Greenblood Oil or Monstrous Spider Venom", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Derro].Add(AttackHelper.BuildData("Greenblood Oil", string.Empty, "Injury DC 13, Initial 1 Con, Secondary 1d2 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Derro].Add(AttackHelper.BuildData("Monstrous Spider Venom", string.Empty, "Injury DC 12, Initial and secondary 1d4 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Derro].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Derro].Add(AttackHelper.BuildData("Sneak Attack", $"1d6", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Derro_Sane].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Derro_Sane].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Derro_Sane].Add(AttackHelper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Derro_Sane].Add(AttackHelper.BuildData("Poison use", string.Empty, "Greenblood Oil or Monstrous Spider Venom", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Derro_Sane].Add(AttackHelper.BuildData("Greenblood Oil", string.Empty, "Injury DC 13, Initial 1 Con, Secondary 1d2 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Derro_Sane].Add(AttackHelper.BuildData("Monstrous Spider Venom", string.Empty, "Injury DC 12, Initial and secondary 1d4 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Derro_Sane].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Derro_Sane].Add(AttackHelper.BuildData("Sneak Attack", $"1d6", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Destrachan].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Destrachan].Add(AttackHelper.BuildData("Destructive harmonics", string.Empty, string.Empty, 1, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Charisma));

                testCases[CreatureConstants.Devourer].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Devourer].Add(AttackHelper.BuildData("Energy Drain", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Charisma));
                testCases[CreatureConstants.Devourer].Add(AttackHelper.BuildData("Trap Essence", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Devourer].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Digester].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Digester].Add(AttackHelper.BuildData("Acid Spray", string.Empty, string.Empty, 0, "extraordinary ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.DisplacerBeast].Add(AttackHelper.BuildData("Tentacle", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.DisplacerBeast].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.DisplacerBeast_PackLord].Add(AttackHelper.BuildData("Tentacle", $"1d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.DisplacerBeast_PackLord].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Djinni].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Djinni].Add(AttackHelper.BuildData("Air mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Djinni].Add(AttackHelper.BuildData("Whirlwind", string.Empty, string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex, saveDcBonus: 3));
                testCases[CreatureConstants.Djinni].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Djinni_Noble].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Djinni_Noble].Add(AttackHelper.BuildData("Air mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Djinni_Noble].Add(AttackHelper.BuildData("Whirlwind", string.Empty, string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex, saveDcBonus: 3));
                testCases[CreatureConstants.Djinni_Noble].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Dog].Add(AttackHelper.BuildData("Bite", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Dog_Riding].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Donkey].Add(AttackHelper.BuildData("Bite", $"1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Doppelganger].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Doppelganger].Add(AttackHelper.BuildData("Detect Thoughts", string.Empty, string.Empty, 1, "supernatural ability", 0, FeatConstants.Frequencies.Constant, false, true, true, true));

                testCases[CreatureConstants.DragonTurtle].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.DragonTurtle].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.DragonTurtle].Add(AttackHelper.BuildData("Breath Weapon", "12d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
                testCases[CreatureConstants.DragonTurtle].Add(AttackHelper.BuildData("Capsize", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //Tiny
                testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(AttackHelper.BuildData("Bite", $"1d4", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(AttackHelper.BuildData("Claw", $"1d3", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(AttackHelper.BuildData("Breath Weapon", $"2d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //small
                testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(AttackHelper.BuildData("Breath Weapon", $"4d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //medium
                testCases[CreatureConstants.Dragon_Black_Young].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_Young].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Young].Add(AttackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Young].Add(AttackHelper.BuildData("Breath Weapon", $"6d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //medium
                testCases[CreatureConstants.Dragon_Black_Juvenile].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_Juvenile].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Juvenile].Add(AttackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Juvenile].Add(AttackHelper.BuildData("Breath Weapon", $"8d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(AttackHelper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(AttackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(AttackHelper.BuildData("Breath Weapon", $"10d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //large
                testCases[CreatureConstants.Dragon_Black_Adult].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(AttackHelper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(AttackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(AttackHelper.BuildData("Breath Weapon", $"12d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(AttackHelper.BuildData("Breath Weapon", $"14d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Black_Old].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_Old].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Old].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Old].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Old].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Old].Add(AttackHelper.BuildData("Breath Weapon", $"16d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Old].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_Old].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(AttackHelper.BuildData("Breath Weapon", $"18d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(AttackHelper.BuildData("Breath Weapon", $"20d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(AttackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(AttackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(AttackHelper.BuildData("Breath Weapon", $"22d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(AttackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(AttackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(AttackHelper.BuildData("Breath Weapon", $"24d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //small
                testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(AttackHelper.BuildData("Breath Weapon", $"2d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //medium
                testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(AttackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(AttackHelper.BuildData("Breath Weapon", $"4d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //medium
                testCases[CreatureConstants.Dragon_Blue_Young].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_Young].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Young].Add(AttackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Young].Add(AttackHelper.BuildData("Breath Weapon", $"6d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //large
                testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(AttackHelper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(AttackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(AttackHelper.BuildData("Breath Weapon", $"8d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(AttackHelper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(AttackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(AttackHelper.BuildData("Breath Weapon", $"10d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(AttackHelper.BuildData("Breath Weapon", $"12d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(AttackHelper.BuildData("Breath Weapon", $"14d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Blue_Old].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(AttackHelper.BuildData("Breath Weapon", $"16d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(AttackHelper.BuildData("Breath Weapon", $"18d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(AttackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(AttackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(AttackHelper.BuildData("Breath Weapon", $"20d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(AttackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(AttackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(AttackHelper.BuildData("Breath Weapon", $"22d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(AttackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(AttackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(AttackHelper.BuildData("Breath Weapon", $"24d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //small
                testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(AttackHelper.BuildData("Breath Weapon", $"2d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //medium
                testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(AttackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(AttackHelper.BuildData("Breath Weapon", $"4d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //medium
                testCases[CreatureConstants.Dragon_Green_Young].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_Young].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Young].Add(AttackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Young].Add(AttackHelper.BuildData("Breath Weapon", $"6d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //large
                testCases[CreatureConstants.Dragon_Green_Juvenile].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_Juvenile].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Juvenile].Add(AttackHelper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Juvenile].Add(AttackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Juvenile].Add(AttackHelper.BuildData("Breath Weapon", $"8d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Juvenile].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(AttackHelper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(AttackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(AttackHelper.BuildData("Breath Weapon", $"10d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Green_Adult].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(AttackHelper.BuildData("Breath Weapon", $"12d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(AttackHelper.BuildData("Breath Weapon", $"14d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Green_Old].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_Old].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Old].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Old].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Old].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Old].Add(AttackHelper.BuildData("Breath Weapon", $"16d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Old].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_Old].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(AttackHelper.BuildData("Breath Weapon", $"18d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(AttackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(AttackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(AttackHelper.BuildData("Breath Weapon", $"20d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(AttackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(AttackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(AttackHelper.BuildData("Breath Weapon", $"22d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(AttackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(AttackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(AttackHelper.BuildData("Breath Weapon", $"24d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //medium
                testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(AttackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(AttackHelper.BuildData("Breath Weapon", $"2d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //large
                testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(AttackHelper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(AttackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(AttackHelper.BuildData("Breath Weapon", $"4d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //large
                testCases[CreatureConstants.Dragon_Red_Young].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_Young].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Young].Add(AttackHelper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Young].Add(AttackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Young].Add(AttackHelper.BuildData("Breath Weapon", $"6d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Young].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(AttackHelper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(AttackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(AttackHelper.BuildData("Breath Weapon", $"8d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //huge
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(AttackHelper.BuildData("Breath Weapon", $"10d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Red_Adult].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(AttackHelper.BuildData("Breath Weapon", $"12d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(AttackHelper.BuildData("Breath Weapon", $"14d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Red_Old].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_Old].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Old].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Old].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Old].Add(AttackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Old].Add(AttackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Old].Add(AttackHelper.BuildData("Breath Weapon", $"16d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Old].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Old].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(AttackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(AttackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(AttackHelper.BuildData("Breath Weapon", $"18d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(AttackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(AttackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(AttackHelper.BuildData("Breath Weapon", $"20d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(AttackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(AttackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(AttackHelper.BuildData("Breath Weapon", $"22d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //colossal
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(AttackHelper.BuildData("Bite", $"4d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(AttackHelper.BuildData("Claw", $"4d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(AttackHelper.BuildData("Wing", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(AttackHelper.BuildData("Tail Slap", $"4d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(AttackHelper.BuildData("Crush", $"4d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(AttackHelper.BuildData("Tail Sweep", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(AttackHelper.BuildData("Breath Weapon", $"24d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //tiny
                testCases[CreatureConstants.Dragon_White_Wyrmling].Add(AttackHelper.BuildData("Bite", $"1d4", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_Wyrmling].Add(AttackHelper.BuildData("Claw", $"1d3", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Wyrmling].Add(AttackHelper.BuildData("Breath Weapon", $"1d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //small
                testCases[CreatureConstants.Dragon_White_VeryYoung].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_VeryYoung].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_VeryYoung].Add(AttackHelper.BuildData("Breath Weapon", $"2d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //medium
                testCases[CreatureConstants.Dragon_White_Young].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_Young].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Young].Add(AttackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Young].Add(AttackHelper.BuildData("Breath Weapon", $"3d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //medium
                testCases[CreatureConstants.Dragon_White_Juvenile].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_Juvenile].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Juvenile].Add(AttackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Juvenile].Add(AttackHelper.BuildData("Breath Weapon", $"4d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(AttackHelper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(AttackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(AttackHelper.BuildData("Breath Weapon", $"5d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //large
                testCases[CreatureConstants.Dragon_White_Adult].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_Adult].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Adult].Add(AttackHelper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Adult].Add(AttackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Adult].Add(AttackHelper.BuildData("Breath Weapon", $"6d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Adult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_Adult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(AttackHelper.BuildData("Breath Weapon", $"7d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_White_Old].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_Old].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Old].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Old].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Old].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Old].Add(AttackHelper.BuildData("Breath Weapon", $"8d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Old].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_Old].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(AttackHelper.BuildData("Breath Weapon", $"9d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_White_Ancient].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(AttackHelper.BuildData("Breath Weapon", $"10d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(AttackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(AttackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(AttackHelper.BuildData("Breath Weapon", $"11d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(AttackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(AttackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(AttackHelper.BuildData("Breath Weapon", $"12d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //tiny
                testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(AttackHelper.BuildData("Bite", $"1d4", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(AttackHelper.BuildData("Claw", $"1d3", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(AttackHelper.BuildData("Breath Weapon (fire)", $"1d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(AttackHelper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+1 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //small
                testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(AttackHelper.BuildData("Breath Weapon (fire)", $"2d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(AttackHelper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+2 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //medium
                testCases[CreatureConstants.Dragon_Brass_Young].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_Young].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Young].Add(AttackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Young].Add(AttackHelper.BuildData("Breath Weapon (fire)", $"3d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Young].Add(AttackHelper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+3 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_Young].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //medium
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(AttackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(AttackHelper.BuildData("Breath Weapon (fire)", $"4d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(AttackHelper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+4 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(AttackHelper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(AttackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(AttackHelper.BuildData("Breath Weapon (fire)", $"5d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(AttackHelper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+5 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //large
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(AttackHelper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(AttackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(AttackHelper.BuildData("Breath Weapon (fire)", $"6d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(AttackHelper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+6 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(AttackHelper.BuildData("Breath Weapon (fire)", $"7d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(AttackHelper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+7 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Brass_Old].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(AttackHelper.BuildData("Breath Weapon (fire)", $"8d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(AttackHelper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+8 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(AttackHelper.BuildData("Breath Weapon (fire)", $"9d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(AttackHelper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+9 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(AttackHelper.BuildData("Breath Weapon (fire)", $"10d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(AttackHelper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+10 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(AttackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(AttackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(AttackHelper.BuildData("Breath Weapon (fire)", $"11d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(AttackHelper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+11 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(AttackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(AttackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(AttackHelper.BuildData("Breath Weapon (fire)", $"12d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(AttackHelper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+12 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //small
                testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(AttackHelper.BuildData("Breath Weapon (electricity)", $"2d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(AttackHelper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+1 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //medium
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(AttackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(AttackHelper.BuildData("Breath Weapon (electricity)", $"4d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(AttackHelper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+2 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //medium
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(AttackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(AttackHelper.BuildData("Breath Weapon (electricity)", $"6d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(AttackHelper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+3 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(AttackHelper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(AttackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(AttackHelper.BuildData("Breath Weapon (electricity)", $"8d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(AttackHelper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+4 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(AttackHelper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(AttackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(AttackHelper.BuildData("Breath Weapon (electricity)", $"10d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(AttackHelper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+5 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(AttackHelper.BuildData("Breath Weapon (electricity)", $"12d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(AttackHelper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+6 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(AttackHelper.BuildData("Breath Weapon (electricity)", $"14d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(AttackHelper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+7 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(AttackHelper.BuildData("Breath Weapon (electricity)", $"16d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(AttackHelper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+8 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(AttackHelper.BuildData("Breath Weapon (electricity)", $"18d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(AttackHelper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+9 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(AttackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(AttackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(AttackHelper.BuildData("Breath Weapon (electricity)", $"20d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(AttackHelper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+10 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(AttackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(AttackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(AttackHelper.BuildData("Breath Weapon (electricity)", $"22d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(AttackHelper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+11 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(AttackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(AttackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(AttackHelper.BuildData("Breath Weapon (electricity)", $"24d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(AttackHelper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+12 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //tiny
                testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(AttackHelper.BuildData("Bite", $"1d4", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(AttackHelper.BuildData("Claw", $"1d3", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(AttackHelper.BuildData("Breath Weapon (acid)", $"2d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(AttackHelper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed 1d6+1 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                //small
                testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(AttackHelper.BuildData("Breath Weapon (acid)", $"4d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(AttackHelper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed 1d6+2 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                //medium
                testCases[CreatureConstants.Dragon_Copper_Young].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_Young].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Young].Add(AttackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Young].Add(AttackHelper.BuildData("Breath Weapon (acid)", $"6d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Young].Add(AttackHelper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed 1d6+3 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_Young].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //medium
                testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(AttackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(AttackHelper.BuildData("Breath Weapon (acid)", $"8d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(AttackHelper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed 1d6+4 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(AttackHelper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(AttackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(AttackHelper.BuildData("Breath Weapon (acid)", $"10d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(AttackHelper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed 1d6+5 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(AttackHelper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(AttackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(AttackHelper.BuildData("Breath Weapon (acid)", $"12d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(AttackHelper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed 1d6+6 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(AttackHelper.BuildData("Breath Weapon (acid)", $"14d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(AttackHelper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed 1d6+7 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Copper_Old].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(AttackHelper.BuildData("Breath Weapon (acid)", $"16d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(AttackHelper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed 1d6+8 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(AttackHelper.BuildData("Breath Weapon (acid)", $"18d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(AttackHelper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed 1d6+9 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(AttackHelper.BuildData("Breath Weapon (acid)", $"20d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(AttackHelper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed 1d6+10 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(AttackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(AttackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(AttackHelper.BuildData("Breath Weapon (acid)", $"22d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(AttackHelper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed 1d6+11 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(AttackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(AttackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(AttackHelper.BuildData("Breath Weapon (acid)", $"24d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(AttackHelper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed 1d6+12 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //medium
                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(AttackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(AttackHelper.BuildData("Breath Weapon (fire)", $"2d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(AttackHelper.BuildData("Breath Weapon (weakening gas)", string.Empty, "1 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                //large
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(AttackHelper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(AttackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(AttackHelper.BuildData("Breath Weapon (fire)", $"4d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(AttackHelper.BuildData("Breath Weapon (weakening gas)", string.Empty, "2 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                //large
                testCases[CreatureConstants.Dragon_Gold_Young].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(AttackHelper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(AttackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(AttackHelper.BuildData("Breath Weapon (fire)", $"6d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(AttackHelper.BuildData("Breath Weapon (weakening gas)", string.Empty, "3 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(AttackHelper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(AttackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(AttackHelper.BuildData("Breath Weapon (fire)", $"8d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(AttackHelper.BuildData("Breath Weapon (weakening gas)", string.Empty, "4 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //huge
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(AttackHelper.BuildData("Breath Weapon (fire)", $"10d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(AttackHelper.BuildData("Breath Weapon (weakening gas)", string.Empty, "5 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(AttackHelper.BuildData("Breath Weapon (fire)", $"12d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(AttackHelper.BuildData("Breath Weapon (weakening gas)", string.Empty, "6 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(AttackHelper.BuildData("Breath Weapon (fire)", $"14d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(AttackHelper.BuildData("Breath Weapon (weakening gas)", string.Empty, "7 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Gold_Old].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(AttackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(AttackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(AttackHelper.BuildData("Breath Weapon (fire)", $"16d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(AttackHelper.BuildData("Breath Weapon (weakening gas)", string.Empty, "8 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(AttackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(AttackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(AttackHelper.BuildData("Breath Weapon (fire)", $"18d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(AttackHelper.BuildData("Breath Weapon (weakening gas)", string.Empty, "9 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(AttackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(AttackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(AttackHelper.BuildData("Breath Weapon (fire)", $"20d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(AttackHelper.BuildData("Breath Weapon (weakening gas)", string.Empty, "10 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //colossal
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(AttackHelper.BuildData("Bite", $"4d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(AttackHelper.BuildData("Claw", $"4d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(AttackHelper.BuildData("Wing", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(AttackHelper.BuildData("Tail Slap", $"4d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(AttackHelper.BuildData("Crush", $"4d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(AttackHelper.BuildData("Tail Sweep", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(AttackHelper.BuildData("Breath Weapon (fire)", $"22d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(AttackHelper.BuildData("Breath Weapon (weakening gas)", string.Empty, "11 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //colossal
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(AttackHelper.BuildData("Bite", $"4d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(AttackHelper.BuildData("Claw", $"4d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(AttackHelper.BuildData("Wing", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(AttackHelper.BuildData("Tail Slap", $"4d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(AttackHelper.BuildData("Crush", $"4d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(AttackHelper.BuildData("Tail Sweep", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(AttackHelper.BuildData("Breath Weapon (fire)", $"24d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(AttackHelper.BuildData("Breath Weapon (weakening gas)", string.Empty, "12 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //small
                testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(AttackHelper.BuildData("Breath Weapon (cold)", $"2d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(AttackHelper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+1 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                //medium
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(AttackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(AttackHelper.BuildData("Breath Weapon (cold)", $"4d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(AttackHelper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+2 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                //medium
                testCases[CreatureConstants.Dragon_Silver_Young].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(AttackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(AttackHelper.BuildData("Breath Weapon (cold)", $"6d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(AttackHelper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+3 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(AttackHelper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(AttackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(AttackHelper.BuildData("Breath Weapon (cold)", $"8d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(AttackHelper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+4 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(AttackHelper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(AttackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(AttackHelper.BuildData("Breath Weapon (cold)", $"10d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(AttackHelper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+5 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(AttackHelper.BuildData("Breath Weapon (cold)", $"12d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(AttackHelper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+6 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(AttackHelper.BuildData("Breath Weapon (cold)", $"14d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(AttackHelper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+7 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Silver_Old].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(AttackHelper.BuildData("Breath Weapon (cold)", $"16d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(AttackHelper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+8 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(AttackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(AttackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(AttackHelper.BuildData("Breath Weapon (cold)", $"18d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(AttackHelper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+9 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(AttackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(AttackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(AttackHelper.BuildData("Breath Weapon (cold)", $"20d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(AttackHelper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+10 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(AttackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(AttackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(AttackHelper.BuildData("Breath Weapon (cold)", $"22d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(AttackHelper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+11 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //colossal
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(AttackHelper.BuildData("Bite", $"4d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(AttackHelper.BuildData("Claw", $"4d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(AttackHelper.BuildData("Wing", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(AttackHelper.BuildData("Tail Slap", $"4d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(AttackHelper.BuildData("Crush", $"4d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(AttackHelper.BuildData("Tail Sweep", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(AttackHelper.BuildData("Breath Weapon (cold)", $"24d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(AttackHelper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+12 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.Dragonne].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragonne].Add(AttackHelper.BuildData("Claw", $"2d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragonne].Add(AttackHelper.BuildData("Pounce", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Dragonne].Add(AttackHelper.BuildData("Roar", string.Empty, string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));

                testCases[CreatureConstants.Dretch].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dretch].Add(AttackHelper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dretch].Add(AttackHelper.BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.Dretch].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Drider].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Drider].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Drider].Add(AttackHelper.BuildData("Bite", $"1d4", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Drider].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Drider].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Drider].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Dryad].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Dryad].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Dryad].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dryad].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Dwarf_Deep].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Dwarf_Deep].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Dwarf_Deep].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Dwarf_Duergar].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Dwarf_Duergar].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Dwarf_Duergar].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dwarf_Duergar].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Dwarf_Hill].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Dwarf_Hill].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Dwarf_Hill].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Dwarf_Mountain].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Dwarf_Mountain].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Dwarf_Mountain].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Eagle].Add(AttackHelper.BuildData("Talons", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Eagle].Add(AttackHelper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Eagle_Giant].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Eagle_Giant].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Efreeti].Add(AttackHelper.BuildData("Slam", $"1d8", "Heat", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Efreeti].Add(AttackHelper.BuildData("Change Size", string.Empty, string.Empty, 0, "spell-like ability", 2, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Efreeti].Add(AttackHelper.BuildData("Heat", "1d6 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, false, true, true, true));
                testCases[CreatureConstants.Efreeti].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Elasmosaurus].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Elemental_Air_Small].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Air_Small].Add(AttackHelper.BuildData("Air mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Air_Small].Add(AttackHelper.BuildData("Whirlwind", "1d4", string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Air_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Air_Medium].Add(AttackHelper.BuildData("Air mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Air_Medium].Add(AttackHelper.BuildData("Whirlwind", "1d6", string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Air_Large].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Air_Large].Add(AttackHelper.BuildData("Air mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Air_Large].Add(AttackHelper.BuildData("Whirlwind", "2d6", string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Air_Huge].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Air_Huge].Add(AttackHelper.BuildData("Air mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Air_Huge].Add(AttackHelper.BuildData("Whirlwind", "2d8", string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Air_Greater].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Air_Greater].Add(AttackHelper.BuildData("Air mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Air_Greater].Add(AttackHelper.BuildData("Whirlwind", "2d8", string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Air_Elder].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Air_Elder].Add(AttackHelper.BuildData("Air mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Air_Elder].Add(AttackHelper.BuildData("Whirlwind", "2d8", string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Earth_Small].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Earth_Small].Add(AttackHelper.BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Earth_Small].Add(AttackHelper.BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Elemental_Earth_Medium].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Earth_Medium].Add(AttackHelper.BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Earth_Medium].Add(AttackHelper.BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Elemental_Earth_Large].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Earth_Large].Add(AttackHelper.BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Earth_Large].Add(AttackHelper.BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Elemental_Earth_Huge].Add(AttackHelper.BuildData("Slam", $"2d10", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Earth_Huge].Add(AttackHelper.BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Earth_Huge].Add(AttackHelper.BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Elemental_Earth_Greater].Add(AttackHelper.BuildData("Slam", $"2d10", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Earth_Greater].Add(AttackHelper.BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Earth_Greater].Add(AttackHelper.BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Elemental_Earth_Elder].Add(AttackHelper.BuildData("Slam", $"2d10", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Earth_Elder].Add(AttackHelper.BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Earth_Elder].Add(AttackHelper.BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Elemental_Fire_Small].Add(AttackHelper.BuildData("Slam", $"1d4", "Burn", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Fire_Small].Add(AttackHelper.BuildData("Burn", "1d4 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Fire_Medium].Add(AttackHelper.BuildData("Slam", $"1d6", "Burn", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Fire_Medium].Add(AttackHelper.BuildData("Burn", "1d6 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Fire_Large].Add(AttackHelper.BuildData("Slam", $"2d6", "Burn", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Fire_Large].Add(AttackHelper.BuildData("Burn", "2d6 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Fire_Huge].Add(AttackHelper.BuildData("Slam", $"2d8", "Burn", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Fire_Huge].Add(AttackHelper.BuildData("Burn", "2d8 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Fire_Greater].Add(AttackHelper.BuildData("Slam", $"2d8", "Burn", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Fire_Greater].Add(AttackHelper.BuildData("Burn", "2d8 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Fire_Elder].Add(AttackHelper.BuildData("Slam", $"2d8", "Burn", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Fire_Elder].Add(AttackHelper.BuildData("Burn", "2d8 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Water_Small].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Water_Small].Add(AttackHelper.BuildData("Water mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Small].Add(AttackHelper.BuildData("Drench", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Small].Add(AttackHelper.BuildData("Vortex", "1d4", string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Water_Medium].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Water_Medium].Add(AttackHelper.BuildData("Water mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Medium].Add(AttackHelper.BuildData("Drench", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Medium].Add(AttackHelper.BuildData("Vortex", "1d6", string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Water_Large].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Water_Large].Add(AttackHelper.BuildData("Water mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Large].Add(AttackHelper.BuildData("Drench", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Large].Add(AttackHelper.BuildData("Vortex", "2d6", string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Water_Huge].Add(AttackHelper.BuildData("Slam", $"2d10", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Water_Huge].Add(AttackHelper.BuildData("Water mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Huge].Add(AttackHelper.BuildData("Drench", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Huge].Add(AttackHelper.BuildData("Vortex", "2d8", string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Water_Greater].Add(AttackHelper.BuildData("Slam", $"2d10", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Water_Greater].Add(AttackHelper.BuildData("Water mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Greater].Add(AttackHelper.BuildData("Drench", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Greater].Add(AttackHelper.BuildData("Vortex", "2d8", string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Water_Elder].Add(AttackHelper.BuildData("Slam", $"2d10", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Water_Elder].Add(AttackHelper.BuildData("Water mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Elder].Add(AttackHelper.BuildData("Drench", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Elder].Add(AttackHelper.BuildData("Vortex", "2d8", string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elephant].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elephant].Add(AttackHelper.BuildData("Stamp", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Elephant].Add(AttackHelper.BuildData("Gore", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elephant].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elf_Aquatic].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Elf_Aquatic].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Elf_Aquatic].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Elf_Drow].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Elf_Drow].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Elf_Drow].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elf_Drow].Add(AttackHelper.BuildData("Poison", string.Empty, "DC 13 Fort, Initial 1 minute unconscious, Secondary 2d4 hours unconscious", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
                testCases[CreatureConstants.Elf_Drow].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Elf_Gray].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Elf_Gray].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Elf_Gray].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Elf_Half].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Elf_Half].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Elf_Half].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Elf_High].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Elf_High].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Elf_High].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Elf_Wild].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Elf_Wild].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Elf_Wild].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Elf_Wood].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Elf_Wood].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Elf_Wood].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Erinyes].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Erinyes].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Erinyes].Add(AttackHelper.BuildData("Rope", string.Empty, "Entangle", 0, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Erinyes].Add(AttackHelper.BuildData("Entangle", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Erinyes].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Erinyes].Add(AttackHelper.BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.EtherealFilcher].Add(AttackHelper.BuildData("Bite", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.EtherealMarauder].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Ettercap].Add(AttackHelper.BuildData("Bite", $"1d8", "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ettercap].Add(AttackHelper.BuildData("Claw", $"1d3", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Ettercap].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial damage 1d6 Dex, secondary damage 2d6 Dex", 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 2));
                testCases[CreatureConstants.Ettercap].Add(AttackHelper.BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, true, true, false, true, saveAbility: AbilityConstants.Constitution));

                testCases[CreatureConstants.Ettin].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Ettin].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Ettin].Add(AttackHelper.BuildData("Unarmed Strike", "1d4", string.Empty, 1.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.FireBeetle_Giant].Add(AttackHelper.BuildData("Bite", $"2d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.FormianWorker].Add(AttackHelper.BuildData("Bite", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.FormianWarrior].Add(AttackHelper.BuildData("Sting", $"2d4", "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.FormianWarrior].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.FormianWarrior].Add(AttackHelper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.FormianWarrior].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.FormianTaskmaster].Add(AttackHelper.BuildData("Sting", $"2d4", "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.FormianTaskmaster].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.FormianTaskmaster].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.FormianTaskmaster].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.FormianTaskmaster].Add(AttackHelper.BuildData("Dominated creature", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.FormianMyrmarch].Add(AttackHelper.BuildData("Sting", $"2d4", "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.FormianMyrmarch].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.FormianMyrmarch].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.FormianMyrmarch].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.FormianMyrmarch].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.FormianQueen].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.FormianQueen].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.FrostWorm].Add(AttackHelper.BuildData("Bite", $"2d8", "Cold", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.FrostWorm].Add(AttackHelper.BuildData("Trill", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.FrostWorm].Add(AttackHelper.BuildData("Cold", "1d8 Cold", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.FrostWorm].Add(AttackHelper.BuildData("Breath weapon", "15d6 cold", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Gargoyle].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Gargoyle].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Gargoyle].Add(AttackHelper.BuildData("Gore", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Gargoyle_Kapoacinth].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Gargoyle_Kapoacinth].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Gargoyle_Kapoacinth].Add(AttackHelper.BuildData("Gore", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.GelatinousCube].Add(AttackHelper.BuildData("Slam", $"1d6 + 1d6 acid", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.GelatinousCube].Add(AttackHelper.BuildData("Acid", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.GelatinousCube].Add(AttackHelper.BuildData("Engulf", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, SaveConstants.Reflex, AbilityConstants.Strength, 1));
                testCases[CreatureConstants.GelatinousCube].Add(AttackHelper.BuildData("Paralysis", string.Empty, "3d6 rounds of paralysis", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData("Light Ray", "2d12", string.Empty, 0, "ranged touch", 2, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Ghaele].Add(AttackHelper.BuildData("Gaze", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.Ghoul].Add(AttackHelper.BuildData("Bite", $"1d6", "Disease, Paralysis", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ghoul].Add(AttackHelper.BuildData("Claw", $"1d3", "Paralysis", 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Ghoul].Add(AttackHelper.BuildData("Disease", string.Empty, "Ghoul Fever", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Ghoul].Add(AttackHelper.BuildData("Ghoul Fever", string.Empty, "incubation period 1 day, damage 1d3 Con and 1d3 Dex", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Ghoul].Add(AttackHelper.BuildData("Paralysis", string.Empty, "1d4+1 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Ghoul_Ghast].Add(AttackHelper.BuildData("Bite", $"1d6", "Disease, Paralysis", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ghoul_Ghast].Add(AttackHelper.BuildData("Claw", $"1d3", "Paralysis", 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Ghoul_Ghast].Add(AttackHelper.BuildData("Disease", string.Empty, "Ghoul Fever", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Ghoul_Ghast].Add(AttackHelper.BuildData("Ghoul Fever", string.Empty, "incubation period 1 day, damage 1d3 Con and 1d3 Dex", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Ghoul_Ghast].Add(AttackHelper.BuildData("Paralysis", string.Empty, "1d4+1 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Ghoul_Ghast].Add(AttackHelper.BuildData("Stench", string.Empty, "1d6+4 rounds sickened", 0, "melee", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Ghoul_Lacedon].Add(AttackHelper.BuildData("Bite", $"1d6", "Disease, Paralysis", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ghoul_Lacedon].Add(AttackHelper.BuildData("Claw", $"1d3", "Paralysis", 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Ghoul_Lacedon].Add(AttackHelper.BuildData("Disease", string.Empty, "Ghoul Fever", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Ghoul_Lacedon].Add(AttackHelper.BuildData("Ghoul Fever", string.Empty, "incubation period 1 day, damage 1d3 Con and 1d3 Dex", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Ghoul_Lacedon].Add(AttackHelper.BuildData("Paralysis", string.Empty, "1d4+1 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Giant_Cloud].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Giant_Cloud].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Giant_Cloud].Add(AttackHelper.BuildData("Rock", $"2d8", string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Giant_Cloud].Add(AttackHelper.BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Giant_Cloud].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Giant_Fire].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Giant_Fire].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Giant_Fire].Add(AttackHelper.BuildData("Rock", $"2d6", string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Giant_Fire].Add(AttackHelper.BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, false));

                testCases[CreatureConstants.Giant_Frost].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Giant_Frost].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Giant_Frost].Add(AttackHelper.BuildData("Rock", $"2d6", string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Giant_Frost].Add(AttackHelper.BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, false));

                testCases[CreatureConstants.Giant_Hill].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Giant_Hill].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Giant_Hill].Add(AttackHelper.BuildData("Rock", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Giant_Hill].Add(AttackHelper.BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, false));

                testCases[CreatureConstants.Giant_Stone].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Giant_Stone].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Giant_Stone].Add(AttackHelper.BuildData("Rock", $"2d8", string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Giant_Stone].Add(AttackHelper.BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, false));

                testCases[CreatureConstants.Giant_Stone_Elder].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Giant_Stone_Elder].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Giant_Stone_Elder].Add(AttackHelper.BuildData("Rock", $"2d8", string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Giant_Stone_Elder].Add(AttackHelper.BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Giant_Stone_Elder].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Giant_Storm].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Giant_Storm].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Giant_Storm].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Giant_Storm].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.GibberingMouther].Add(AttackHelper.BuildData("Bite", $"1", string.Empty, 1, "melee", 6, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.GibberingMouther].Add(AttackHelper.BuildData("Spittle", $"1d4 acid", "Blindness", 0, "ranged touch", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.GibberingMouther].Add(AttackHelper.BuildData("Blindness", string.Empty, "1d4 rounds blinded", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.GibberingMouther].Add(AttackHelper.BuildData("Gibbering", string.Empty, "1d2 rounds Confusion", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.GibberingMouther].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.GibberingMouther].Add(AttackHelper.BuildData("Swallow Whole", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.GibberingMouther].Add(AttackHelper.BuildData("Blood Drain", string.Empty, "1d4 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.GibberingMouther].Add(AttackHelper.BuildData("Ground Manipulation", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Girallon].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Girallon].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Girallon].Add(AttackHelper.BuildData("Rend", $"2d4", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Githyanki].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Githyanki].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Githyanki].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Githyanki].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Psionic, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Githzerai].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Githzerai].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Githzerai].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Githzerai].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Psionic, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Glabrezu].Add(AttackHelper.BuildData("Pincer", $"2d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Glabrezu].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Glabrezu].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Glabrezu].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Glabrezu].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Glabrezu].Add(AttackHelper.BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Gnoll].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Gnoll].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Gnoll].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Gnome_Forest].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Gnome_Forest].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Gnome_Forest].Add(AttackHelper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Gnome_Rock].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Gnome_Rock].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Gnome_Rock].Add(AttackHelper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Gnome_Svirfneblin].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Gnome_Svirfneblin].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Gnome_Svirfneblin].Add(AttackHelper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Gnome_Svirfneblin].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Goblin].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Goblin].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Goblin].Add(AttackHelper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Golem_Clay].Add(AttackHelper.BuildData("Slam", $"2d10", "Cursed Wound", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Golem_Clay].Add(AttackHelper.BuildData("Berserk", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Golem_Clay].Add(AttackHelper.BuildData("Cursed Wound", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));

                testCases[CreatureConstants.Golem_Flesh].Add(AttackHelper.BuildData("Slam", $"2d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Golem_Flesh].Add(AttackHelper.BuildData("Berserk", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Golem_Iron].Add(AttackHelper.BuildData("Slam", $"2d10", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Golem_Iron].Add(AttackHelper.BuildData("Breath weapon", string.Empty, "Poisonous Gas", 0, "supernatural ability", 1, $"1d4+1 {FeatConstants.Frequencies.Round}", false, true, true, true));
                testCases[CreatureConstants.Golem_Iron].Add(AttackHelper.BuildData("Poisonous Gas", string.Empty, "Initial damage 1d4 Con, secondary damage 3d4 Con", 0, "melee", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Golem_Stone].Add(AttackHelper.BuildData("Slam", $"2d10", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Golem_Stone].Add(AttackHelper.BuildData("Slow", string.Empty, string.Empty, 0, "supernatural ability", 1, $"2 {FeatConstants.Frequencies.Round}", true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));

                testCases[CreatureConstants.Golem_Stone_Greater].Add(AttackHelper.BuildData("Slam", $"4d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Golem_Stone_Greater].Add(AttackHelper.BuildData("Slow", string.Empty, string.Empty, 0, "supernatural ability", 1, $"2 {FeatConstants.Frequencies.Round}", true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));

                testCases[CreatureConstants.Gorgon].Add(AttackHelper.BuildData("Gore", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Gorgon].Add(AttackHelper.BuildData("Breath weapon", string.Empty, "Turn to stone", 0, "supernatural ability", 5, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Gorgon].Add(AttackHelper.BuildData("Trample", "1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.GrayOoze].Add(AttackHelper.BuildData("Slam", $"1d6 + 1d6 acid", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.GrayOoze].Add(AttackHelper.BuildData("Acid", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
                testCases[CreatureConstants.GrayOoze].Add(AttackHelper.BuildData("Constrict", $"1d6 + 1d6 acid", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.GrayOoze].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.GrayRender].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.GrayRender].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.GrayRender].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.GrayRender].Add(AttackHelper.BuildData("Rend", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.GreenHag].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.GreenHag].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.GreenHag].Add(AttackHelper.BuildData("Weakness", string.Empty, "2d4 Strength damage", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.GreenHag].Add(AttackHelper.BuildData("Mimicry", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Grick].Add(AttackHelper.BuildData("Tentacle", $"1d4", string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Grick].Add(AttackHelper.BuildData("Bite", $"1d3", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Griffon].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Griffon].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Griffon].Add(AttackHelper.BuildData("Pounce", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Griffon].Add(AttackHelper.BuildData("Rake", $"1d6", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Grig].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Grig].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Grig].Add(AttackHelper.BuildData("Unarmed Strike", "1", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Grig].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Grig_WithFiddle].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Grig_WithFiddle].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Grig_WithFiddle].Add(AttackHelper.BuildData("Unarmed Strike", "1", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Grig_WithFiddle].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Grig_WithFiddle].Add(AttackHelper.BuildData("Fiddle", string.Empty, SpellConstants.IrresistibleDance, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.Grimlock].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Grimlock].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Gynosphinx].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Gynosphinx].Add(AttackHelper.BuildData("Pounce", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Gynosphinx].Add(AttackHelper.BuildData("Rake", $"1d6", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Gynosphinx].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Halfling_Deep].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Halfling_Deep].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Halfling_Deep].Add(AttackHelper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Halfling_Lightfoot].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Halfling_Lightfoot].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Halfling_Lightfoot].Add(AttackHelper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Halfling_Tallfellow].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Halfling_Tallfellow].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Halfling_Tallfellow].Add(AttackHelper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Harpy].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Harpy].Add(AttackHelper.BuildData("Claw", $"1d3", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Harpy].Add(AttackHelper.BuildData("Captivating Song", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.Hawk].Add(AttackHelper.BuildData("Talons", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.HellHound].Add(AttackHelper.BuildData("Bite", $"1d8", "Fiery Bite", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.HellHound].Add(AttackHelper.BuildData("Fiery Bite", $"1d6 fire", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.HellHound].Add(AttackHelper.BuildData("Breath weapon", "2d6 fire", string.Empty, 0, "supernatural ability", 1, $"2d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.HellHound_NessianWarhound].Add(AttackHelper.BuildData("Bite", $"2d6", "Fiery Bite", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.HellHound_NessianWarhound].Add(AttackHelper.BuildData("Fiery Bite", $"1d8 fire", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.HellHound_NessianWarhound].Add(AttackHelper.BuildData("Breath weapon", "3d6 fire", string.Empty, 0, "supernatural ability", 1, $"2d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Hellcat_Bezekira].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(AttackHelper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(AttackHelper.BuildData("Rake", $"1d8", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Hellwasp_Swarm].Add(AttackHelper.BuildData("Swarm", $"3d6", "poison", 0, "swarm", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Hellwasp_Swarm].Add(AttackHelper.BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Hellwasp_Swarm].Add(AttackHelper.BuildData("Inhabit", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, true, true, true, true));
                testCases[CreatureConstants.Hellwasp_Swarm].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d4 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));

                testCases[CreatureConstants.Hezrou].Add(AttackHelper.BuildData("Bite", $"4d4", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Hezrou].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Hezrou].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Hezrou].Add(AttackHelper.BuildData("Stench", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Hezrou].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Hezrou].Add(AttackHelper.BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Hieracosphinx].Add(AttackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Hieracosphinx].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Hieracosphinx].Add(AttackHelper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Hieracosphinx].Add(AttackHelper.BuildData("Rake", $"1d6", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Hippogriff].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Hippogriff].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hobgoblin].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Hobgoblin].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Hobgoblin].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Homunculus].Add(AttackHelper.BuildData("Bite", $"1d4", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Homunculus].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial damage sleep for 1 minute, secondary damage sleep for another 5d6 minutes", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 2));

                testCases[CreatureConstants.HornedDevil_Cornugon].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(AttackHelper.BuildData("Tail", $"2d6", "infernal wound", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(AttackHelper.BuildData("Fear Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(AttackHelper.BuildData("Infernal Wound", "2", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, string.Empty, AbilityConstants.Constitution));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(AttackHelper.BuildData("Stun", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Strength));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(AttackHelper.BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Horse_Heavy].Add(AttackHelper.BuildData("Hoof", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Horse_Heavy_War].Add(AttackHelper.BuildData("Hoof", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Horse_Heavy_War].Add(AttackHelper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Horse_Light].Add(AttackHelper.BuildData("Hoof", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Horse_Light_War].Add(AttackHelper.BuildData("Hoof", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Horse_Light_War].Add(AttackHelper.BuildData("Bite", $"1d3", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.HoundArchon].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.HoundArchon].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.HoundArchon].Add(AttackHelper.BuildData("Slam", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.HoundArchon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Howler].Add(AttackHelper.BuildData("Bite", $"2d8", "1d4 Quills", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Howler].Add(AttackHelper.BuildData("Quill", "1d6", string.Empty, 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Dexterity, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Howler].Add(AttackHelper.BuildData("Howl", string.Empty, "1 Wis", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hour, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.Human].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Human].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Human].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hydra_5Heads].Add(AttackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 5, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hydra_6Heads].Add(AttackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 6, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hydra_7Heads].Add(AttackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 7, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hydra_8Heads].Add(AttackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 8, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hydra_9Heads].Add(AttackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 9, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hydra_10Heads].Add(AttackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 10, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hydra_11Heads].Add(AttackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 11, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hydra_12Heads].Add(AttackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 12, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hyena].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Hyena].Add(AttackHelper.BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.IceDevil_Gelugon].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(AttackHelper.BuildData("Claw", $"1d10", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(AttackHelper.BuildData("Tail", $"3d6", "slow", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(AttackHelper.BuildData("Fear Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(AttackHelper.BuildData("Slow", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(AttackHelper.BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Imp].Add(AttackHelper.BuildData("Sting", $"1d4", "poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Imp].Add(AttackHelper.BuildData("Poison", string.Empty, $"Initial damage 1d4 Dex, Secondary damage 2d4 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution, 2));
                testCases[CreatureConstants.Imp].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.InvisibleStalker].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Janni].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Janni].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Janni].Add(AttackHelper.BuildData("Change Size", string.Empty, string.Empty, 0, "spell-like ability", 2, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Janni].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Kobold].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Kobold].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Kobold].Add(AttackHelper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Kolyarut].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Kolyarut].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Kolyarut].Add(AttackHelper.BuildData("Vampiric Touch", $"5d6", string.Empty, 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Kolyarut].Add(AttackHelper.BuildData("Enervation Ray", string.Empty, "1d4 negative levels", 0, "ranged touch", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Kolyarut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Kraken].Add(AttackHelper.BuildData("Tentacle", $"2d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Kraken].Add(AttackHelper.BuildData("Arm", $"1d6", string.Empty, 0.5, "melee", 6, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Kraken].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Kraken].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Kraken].Add(AttackHelper.BuildData("Constrict (Tentacle)", $"2d8", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Kraken].Add(AttackHelper.BuildData("Constrict (Arm)", $"1d6", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Krenshar].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Krenshar].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Krenshar].Add(AttackHelper.BuildData("Scare", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Krenshar].Add(AttackHelper.BuildData("Scare with Screech", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.KuoToa].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.KuoToa].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.KuoToa].Add(AttackHelper.BuildData("Lightning Bolt", "1d6 per whip", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", true, true, true, false, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Lamia].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Lamia].Add(AttackHelper.BuildData("Touch", string.Empty, "1d4 Wisdom Drain", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Lamia].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Lamia].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Lammasu].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Lammasu].Add(AttackHelper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Lammasu].Add(AttackHelper.BuildData("Rake", $"1d6", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Lammasu].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.LanternArchon].Add(AttackHelper.BuildData("Light Ray", $"1d6", string.Empty, 0, "ranged touch", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.LanternArchon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Lemure].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Leonal].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Leonal].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Leonal].Add(AttackHelper.BuildData("Roar", "2d6 sonic", string.Empty, 0, "supernatural ability", 3, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.Leonal].Add(AttackHelper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Leonal].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Leonal].Add(AttackHelper.BuildData("Rake", $"1d6", string.Empty, 1, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Leonal].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Leopard].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Leopard].Add(AttackHelper.BuildData("Claw", $"1d3", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Leopard].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Leopard].Add(AttackHelper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Leopard].Add(AttackHelper.BuildData("Rake", $"1d3", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Lillend].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Lillend].Add(AttackHelper.BuildData("Unarmed Strike", "1d4", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Lillend].Add(AttackHelper.BuildData("Tail slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Lillend].Add(AttackHelper.BuildData("Constrict", $"2d6", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Lillend].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Lillend].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Lillend].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Lion].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Lion].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Lion].Add(AttackHelper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Lion].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Lion].Add(AttackHelper.BuildData("Rake", $"1d4", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Lion_Dire].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Lion_Dire].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Lion_Dire].Add(AttackHelper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Lion_Dire].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Lion_Dire].Add(AttackHelper.BuildData("Rake", $"1d6", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Lizard].Add(AttackHelper.BuildData("Bite", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Lizard_Monitor].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Lizardfolk].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Lizardfolk].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Lizardfolk].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Lizardfolk].Add(AttackHelper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Locathah].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Locathah].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Locathah].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Locust_Swarm].Add(AttackHelper.BuildData("Swarm", $"2d6", string.Empty, 0, "swarm", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Locust_Swarm].Add(AttackHelper.BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Magmin].Add(AttackHelper.BuildData("Burning Touch", $"1d8 fire", "Combustion", 0, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Magmin].Add(AttackHelper.BuildData("Slam", $"1d3", "Combustion", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Magmin].Add(AttackHelper.BuildData("Combustion", $"1d8 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Magmin].Add(AttackHelper.BuildData("Fiery Aura", $"1d6 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.MantaRay].Add(AttackHelper.BuildData("Ram", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Manticore].Add(AttackHelper.BuildData("Claw", $"2d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Manticore].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Manticore].Add(AttackHelper.BuildData("Spikes", string.Empty, "Tail Spikes", 0, "ranged", 6, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Manticore].Add(AttackHelper.BuildData("Tail Spikes", $"1d8", string.Empty, 0.5, "extraordinary ability", 24, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Marilith].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Marilith].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 5, FeatConstants.Frequencies.Round, true, false, false, false));
                testCases[CreatureConstants.Marilith].Add(AttackHelper.BuildData("Tail Slap", $"4d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Marilith].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1, "melee", 6, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Marilith].Add(AttackHelper.BuildData("Constrict", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Strength));
                testCases[CreatureConstants.Marilith].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Marilith].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Marilith].Add(AttackHelper.BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Marut].Add(AttackHelper.BuildData("Slam", $"2d6", "Fist of Thunder", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Marut].Add(AttackHelper.BuildData("Slam", $"2d6", "Fist of Lightning", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Marut].Add(AttackHelper.BuildData("Fist of Thunder", $"3d6 sonic", "deafened 2d6 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Marut].Add(AttackHelper.BuildData("Fist of Lightning", $"3d6 electricity", "blinded 2d6 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Marut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Medusa].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Medusa].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Medusa].Add(AttackHelper.BuildData("Snakes", $"1d4", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Medusa].Add(AttackHelper.BuildData("Petrifying Gaze", string.Empty, "Permanent petrification", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.Medusa].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial damage 1d6 Str, Secondary damage 2d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Megaraptor].Add(AttackHelper.BuildData("Talons", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Megaraptor].Add(AttackHelper.BuildData("Foreclaw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Megaraptor].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Megaraptor].Add(AttackHelper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Mephit_Air].Add(AttackHelper.BuildData("Claw", $"1d3", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Air].Add(AttackHelper.BuildData("Breath weapon", $"1d8", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Air].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Air].Add(AttackHelper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Dust].Add(AttackHelper.BuildData("Claw", $"1d3", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Dust].Add(AttackHelper.BuildData("Breath weapon", $"1d4", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Dust].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Dust].Add(AttackHelper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Earth].Add(AttackHelper.BuildData("Claw", $"1d3", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Earth].Add(AttackHelper.BuildData("Breath weapon", $"1d8", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Earth].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Earth].Add(AttackHelper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Fire].Add(AttackHelper.BuildData("Claw", $"1d3 + 1d4 fire", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Fire].Add(AttackHelper.BuildData("Breath weapon", $"1d8 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Fire].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Fire].Add(AttackHelper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Ice].Add(AttackHelper.BuildData("Claw", $"1d3 + 1d4 cold", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Ice].Add(AttackHelper.BuildData("Breath weapon", $"1d4 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Ice].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Ice].Add(AttackHelper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Magma].Add(AttackHelper.BuildData("Claw", $"1d3 + 1d4 fire", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Magma].Add(AttackHelper.BuildData("Breath weapon", $"1d4 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Magma].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Magma].Add(AttackHelper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Ooze].Add(AttackHelper.BuildData("Claw", $"1d3", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Ooze].Add(AttackHelper.BuildData("Breath weapon", $"1d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Ooze].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Ooze].Add(AttackHelper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Salt].Add(AttackHelper.BuildData("Claw", $"1d3", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Salt].Add(AttackHelper.BuildData("Breath weapon", $"1d4", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Salt].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Salt].Add(AttackHelper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Steam].Add(AttackHelper.BuildData("Claw", $"1d3 + 1d4 fire", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Steam].Add(AttackHelper.BuildData("Breath weapon", $"1d4 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Steam].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Steam].Add(AttackHelper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Water].Add(AttackHelper.BuildData("Claw", $"1d3", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Water].Add(AttackHelper.BuildData("Breath weapon", $"1d8 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Water].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Water].Add(AttackHelper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Merfolk].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Merfolk].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Merfolk].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Mimic].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mimic].Add(AttackHelper.BuildData("Adhesive", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Mimic].Add(AttackHelper.BuildData("Crush", $"1d8", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.MindFlayer].Add(AttackHelper.BuildData("Tentacle", $"1d4", string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.MindFlayer].Add(AttackHelper.BuildData("Mind Blast", string.Empty, "3d4 rounds stunned", 1, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.MindFlayer].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.Psionic, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.MindFlayer].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.MindFlayer].Add(AttackHelper.BuildData("Extract", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Minotaur].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Minotaur].Add(AttackHelper.BuildData("Gore", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Minotaur].Add(AttackHelper.BuildData("Powerful Charge", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Mohrg].Add(AttackHelper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mohrg].Add(AttackHelper.BuildData("Tongue", string.Empty, "Paralyzing Touch", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mohrg].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Mohrg].Add(AttackHelper.BuildData("Paralyzing Touch", string.Empty, "1d4 minutes paralyzed", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Mohrg].Add(AttackHelper.BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));

                testCases[CreatureConstants.Monkey].Add(AttackHelper.BuildData("Bite", $"1d3", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Mule].Add(AttackHelper.BuildData("Hoof", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Mummy].Add(AttackHelper.BuildData("Slam", $"1d6", "Mummy Rot", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mummy].Add(AttackHelper.BuildData("Despair", string.Empty, "1d4 rounds fear", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.Mummy].Add(AttackHelper.BuildData("Mummy Rot", string.Empty, "incubation period 1 minute; damage 1d6 Con and 1d6 Cha", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Naga_Dark].Add(AttackHelper.BuildData("Sting", $"2d4", "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Naga_Dark].Add(AttackHelper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Naga_Dark].Add(AttackHelper.BuildData("Poison", string.Empty, "lapse into a nightmare-haunted sleep for 2d4 minutes", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Naga_Dark].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Naga_Guardian].Add(AttackHelper.BuildData("Bite", $"2d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Naga_Guardian].Add(AttackHelper.BuildData("Spit", string.Empty, "Poison", 0, "ranged touch", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Naga_Guardian].Add(AttackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d10 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Naga_Guardian].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Naga_Spirit].Add(AttackHelper.BuildData("Bite", $"2d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Naga_Spirit].Add(AttackHelper.BuildData("Charming Gaze", string.Empty, SpellConstants.CharmPerson, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.Naga_Spirit].Add(AttackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d8 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Naga_Spirit].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Naga_Water].Add(AttackHelper.BuildData("Bite", $"2d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Naga_Water].Add(AttackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d8 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Naga_Water].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Nalfeshnee].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Nalfeshnee].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Nalfeshnee].Add(AttackHelper.BuildData("Smite", string.Empty, string.Empty, 1, "supernatural ability", 3, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Nalfeshnee].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Nalfeshnee].Add(AttackHelper.BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.NightHag].Add(AttackHelper.BuildData("Bite", $"2d6", "Disease", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.NightHag].Add(AttackHelper.BuildData("Disease", string.Empty, "incubation period 1 day, damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.NightHag].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.NightHag].Add(AttackHelper.BuildData("Dream Haunting", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData("Sting", $"2d8", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData("Desecrating Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));
                testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData("Energy Drain", string.Empty, "1 negative level", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 2d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData("Summon Undead", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.Nightcrawler].Add(AttackHelper.BuildData("Swallow Whole", "2d8+12 bludgeoning + 12 acid", "Energy Drain", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Nightmare].Add(AttackHelper.BuildData("Hoof", $"1d8", "1d4 fire", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Nightmare].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Nightmare].Add(AttackHelper.BuildData("Flaming Hooves", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Nightmare].Add(AttackHelper.BuildData("Smoke", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Nightmare_Cauchemar].Add(AttackHelper.BuildData("Hoof", $"2d6", "1d4 fire", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Nightmare_Cauchemar].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Nightmare_Cauchemar].Add(AttackHelper.BuildData("Flaming Hooves", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Nightmare_Cauchemar].Add(AttackHelper.BuildData("Smoke", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Nightwalker].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Nightwalker].Add(AttackHelper.BuildData("Crush Item", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Nightwalker].Add(AttackHelper.BuildData("Desecrating Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));
                testCases[CreatureConstants.Nightwalker].Add(AttackHelper.BuildData("Evil Gaze", string.Empty, "1d8 rounds paralyzed with fear", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.Nightwalker].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Nightwalker].Add(AttackHelper.BuildData("Summon Undead", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Nightwing].Add(AttackHelper.BuildData("Bite", $"2d6", "Magic Drain", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Nightwing].Add(AttackHelper.BuildData("Desecrating Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));
                testCases[CreatureConstants.Nightwing].Add(AttackHelper.BuildData("Magic Drain", string.Empty, "1 point enhancement bonus", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Nightwing].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Nightwing].Add(AttackHelper.BuildData("Summon Undead", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Nixie].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Nixie].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Nixie].Add(AttackHelper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Nixie].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Nymph].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Nymph].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Nymph].Add(AttackHelper.BuildData("Blinding Beauty", string.Empty, "Blinded permanently", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Nymph].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Nymph].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Nymph].Add(AttackHelper.BuildData("Stunning Glance", string.Empty, "stunned 2d4 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.OchreJelly].Add(AttackHelper.BuildData("Slam", $"2d4 + 1d4 acid", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.OchreJelly].Add(AttackHelper.BuildData("Acid", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
                testCases[CreatureConstants.OchreJelly].Add(AttackHelper.BuildData("Constrict", $"2d4 + 1d4 acid", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.OchreJelly].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Octopus].Add(AttackHelper.BuildData("Arms", $"0", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Octopus].Add(AttackHelper.BuildData("Bite", $"1d3", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Octopus].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Octopus_Giant].Add(AttackHelper.BuildData("Tentacle", $"1d4", string.Empty, 1, "melee", 8, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Octopus_Giant].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Octopus_Giant].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Octopus_Giant].Add(AttackHelper.BuildData("Constrict", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Ogre].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Ogre].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Ogre].Add(AttackHelper.BuildData("Unarmed Strike", "1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Ogre_Merrow].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Ogre_Merrow].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Ogre_Merrow].Add(AttackHelper.BuildData("Unarmed Strike", "1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.OgreMage].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.OgreMage].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.OgreMage].Add(AttackHelper.BuildData("Unarmed Strike", "1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.OgreMage].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Orc].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Orc].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Orc].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Orc_Half].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Otyugh].Add(AttackHelper.BuildData("Tentacle", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Otyugh].Add(AttackHelper.BuildData("Bite", $"1d4", "Disease", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Otyugh].Add(AttackHelper.BuildData("Constrict", $"1d6", string.Empty, 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Otyugh].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Otyugh].Add(AttackHelper.BuildData("Disease", string.Empty, "Filth Fever", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Otyugh].Add(AttackHelper.BuildData("Filth Fever", string.Empty, "incubation period 1d3 days; damage 1d3 Dex and 1d3 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Owl].Add(AttackHelper.BuildData("Talons", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Owl_Giant].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Owl_Giant].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Owlbear].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Owlbear].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Owlbear].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Pegasus].Add(AttackHelper.BuildData("Hoof", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pegasus].Add(AttackHelper.BuildData("Bite", $"1d3", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Pegasus].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.PhantomFungus].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.PhaseSpider].Add(AttackHelper.BuildData("Bite", $"1d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.PhaseSpider].Add(AttackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d8 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Phasm].Add(AttackHelper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.PitFiend].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.PitFiend].Add(AttackHelper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.PitFiend].Add(AttackHelper.BuildData("Bite", $"4d6", "poison, disease", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.PitFiend].Add(AttackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.PitFiend].Add(AttackHelper.BuildData("Constrict", $"2d8", string.Empty, 2, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.PitFiend].Add(AttackHelper.BuildData("Fear Aura", string.Empty, string.Empty, 0, "supernatural ability", 2, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.PitFiend].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.PitFiend].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.PitFiend].Add(AttackHelper.BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.PitFiend].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial damage 1d6 Con, Secondary damage death", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.PitFiend].Add(AttackHelper.BuildData("Disease", string.Empty, "Devil Chills", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.PitFiend].Add(AttackHelper.BuildData("Devil Chills", string.Empty, "incubation period 1d4 days, damage 1d4 Str", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Pixie].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Pixie].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Pixie].Add(AttackHelper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pixie].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Pixie].Add(AttackHelper.BuildData("Special Arrow (Memory Loss)", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, false, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveDcBonus: 2));
                testCases[CreatureConstants.Pixie].Add(AttackHelper.BuildData("Special Arrow (Sleep)", string.Empty, SpellConstants.Sleep, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, false, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveDcBonus: 2));

                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(AttackHelper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(AttackHelper.BuildData("Special Arrow (Memory Loss)", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, false, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveDcBonus: 2));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(AttackHelper.BuildData("Special Arrow (Sleep)", string.Empty, SpellConstants.Sleep, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, false, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveDcBonus: 2));

                testCases[CreatureConstants.Pony].Add(AttackHelper.BuildData("Hoof", $"1d3", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Pony_War].Add(AttackHelper.BuildData("Hoof", $"1d3", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Porpoise].Add(AttackHelper.BuildData("Slam", $"2d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.PrayingMantis_Giant].Add(AttackHelper.BuildData("Claws", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.PrayingMantis_Giant].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.PrayingMantis_Giant].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Pseudodragon].Add(AttackHelper.BuildData("Sting", $"1d3", "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pseudodragon].Add(AttackHelper.BuildData("Bite", $"1", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Pseudodragon].Add(AttackHelper.BuildData("Poison", string.Empty, "initial damage sleep for 1 minute, secondary damage sleep for 1d3 hours", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 2));

                testCases[CreatureConstants.PurpleWorm].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.PurpleWorm].Add(AttackHelper.BuildData("Sting", $"2d6", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.PurpleWorm].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.PurpleWorm].Add(AttackHelper.BuildData("Poison", string.Empty, "initial damage 1d6 Str, secondary damage 2d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.PurpleWorm].Add(AttackHelper.BuildData("Swallow Whole", "2d8+12 bludgeoning", "8 acid", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Pyrohydra_5Heads].Add(AttackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 5, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pyrohydra_5Heads].Add(AttackHelper.BuildData("Breath weapon", $"3d6 fire per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Pyrohydra_6Heads].Add(AttackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 6, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pyrohydra_6Heads].Add(AttackHelper.BuildData("Breath weapon", $"3d6 fire per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Pyrohydra_7Heads].Add(AttackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 7, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pyrohydra_7Heads].Add(AttackHelper.BuildData("Breath weapon", $"3d6 fire per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Pyrohydra_8Heads].Add(AttackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 8, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pyrohydra_8Heads].Add(AttackHelper.BuildData("Breath weapon", $"3d6 fire per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Pyrohydra_9Heads].Add(AttackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 9, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pyrohydra_9Heads].Add(AttackHelper.BuildData("Breath weapon", $"3d6 fire per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Pyrohydra_10Heads].Add(AttackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 10, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pyrohydra_10Heads].Add(AttackHelper.BuildData("Breath weapon", $"3d6 fire per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Pyrohydra_11Heads].Add(AttackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 11, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pyrohydra_11Heads].Add(AttackHelper.BuildData("Breath weapon", $"3d6 fire per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Pyrohydra_12Heads].Add(AttackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 12, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pyrohydra_12Heads].Add(AttackHelper.BuildData("Breath weapon", $"3d6 fire per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Quasit].Add(AttackHelper.BuildData("Claw", $"1d3", "poison", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Quasit].Add(AttackHelper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Quasit].Add(AttackHelper.BuildData("Poison", string.Empty, $"Initial damage 1d4 Dex, Secondary damage 2d4 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution, 2));
                testCases[CreatureConstants.Quasit].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Rakshasa].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Rakshasa].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Rakshasa].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Rakshasa].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Rast].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Rast].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Rast].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Rast].Add(AttackHelper.BuildData("Paralyzing Gaze", string.Empty, "Paralysis for 1d6 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Rast].Add(AttackHelper.BuildData("Blood Drain", string.Empty, "1 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Rat].Add(AttackHelper.BuildData("Bite", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Rat_Dire].Add(AttackHelper.BuildData("Bite", $"1d3", "Disease", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Rat_Dire].Add(AttackHelper.BuildData("Disease", string.Empty, "Filth Fever", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
                testCases[CreatureConstants.Rat_Dire].Add(AttackHelper.BuildData("Filth Fever", string.Empty, "incubation period 1d3 days, damage 1d3 Dex and 1d3 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Rat_Swarm].Add(AttackHelper.BuildData("Swarm", $"1d6", "Disease", 0, "swarm", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Rat_Swarm].Add(AttackHelper.BuildData("Disease", string.Empty, "Filth Fever", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
                testCases[CreatureConstants.Rat_Swarm].Add(AttackHelper.BuildData("Filth Fever", string.Empty, "incubation period 1d3 days, damage 1d3 Dex and 1d3 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Rat_Swarm].Add(AttackHelper.BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Raven].Add(AttackHelper.BuildData("Claws", $"1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Ravid].Add(AttackHelper.BuildData("Tail Slap", $"1d6", "Positive Energy", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ravid].Add(AttackHelper.BuildData("Claw", $"1d4", "Positive Energy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Ravid].Add(AttackHelper.BuildData("Tail Touch", string.Empty, "Positive Energy", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ravid].Add(AttackHelper.BuildData("Claw Touch", string.Empty, "Positive Energy", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Ravid].Add(AttackHelper.BuildData("Positive Energy", "2d10 positive energy", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Ravid].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.RazorBoar].Add(AttackHelper.BuildData("Tusk Slash", $"1d8", "Vorpal Tusk", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.RazorBoar].Add(AttackHelper.BuildData("Hoof", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.RazorBoar].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.RazorBoar].Add(AttackHelper.BuildData("Vorpal Tusk", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.RazorBoar].Add(AttackHelper.BuildData("Trample", "2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Remorhaz].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Remorhaz].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Remorhaz].Add(AttackHelper.BuildData("Swallow Whole", "2d8+12 bludgeoning", "8d6 fire", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Retriever].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Retriever].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Retriever].Add(AttackHelper.BuildData("Eye Ray", string.Empty, string.Empty, 0, "ranged touch", 1, FeatConstants.Frequencies.Round, false, true, false, true, string.Empty, AbilityConstants.Dexterity));
                testCases[CreatureConstants.Retriever].Add(AttackHelper.BuildData("Find Target", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));
                testCases[CreatureConstants.Retriever].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Rhinoceras].Add(AttackHelper.BuildData("Gore", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Rhinoceras].Add(AttackHelper.BuildData("Powerful Charge", $"4d6", string.Empty, 3, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Roc].Add(AttackHelper.BuildData("Talon", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Roc].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Roper].Add(AttackHelper.BuildData("Strand", string.Empty, "Drag", 0, "ranged touch", 6, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Roper].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Roper].Add(AttackHelper.BuildData("Drag", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Strength));
                testCases[CreatureConstants.Roper].Add(AttackHelper.BuildData("Weakness", string.Empty, "2d8 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.RustMonster].Add(AttackHelper.BuildData("Antennae", string.Empty, "Rust", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.RustMonster].Add(AttackHelper.BuildData("Bite", "1d3", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.RustMonster].Add(AttackHelper.BuildData("Rust", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 4));

                testCases[CreatureConstants.Sahuagin].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Sahuagin].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Sahuagin].Add(AttackHelper.BuildData("Talon", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Sahuagin].Add(AttackHelper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Sahuagin].Add(AttackHelper.BuildData("Blood Frenzy", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.Sahuagin].Add(AttackHelper.BuildData("Rake", $"1d4", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Sahuagin_Mutant].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Sahuagin_Mutant].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 2, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Sahuagin_Mutant].Add(AttackHelper.BuildData("Talon", $"1d4", string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Sahuagin_Mutant].Add(AttackHelper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Sahuagin_Mutant].Add(AttackHelper.BuildData("Blood Frenzy", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.Sahuagin_Mutant].Add(AttackHelper.BuildData("Rake", $"1d4", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Sahuagin_Malenti].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Sahuagin_Malenti].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Sahuagin_Malenti].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Sahuagin_Malenti].Add(AttackHelper.BuildData("Blood Frenzy", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Salamander_Flamebrother].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Salamander_Flamebrother].Add(AttackHelper.BuildData("Tail Slap", $"1d4", "Heat", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Salamander_Flamebrother].Add(AttackHelper.BuildData("Constrict", $"1d4", "Heat", 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Salamander_Flamebrother].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Salamander_Flamebrother].Add(AttackHelper.BuildData("Heat", "1d6 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));

                testCases[CreatureConstants.Salamander_Average].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Salamander_Average].Add(AttackHelper.BuildData("Tail Slap", $"2d6", "Heat", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Salamander_Average].Add(AttackHelper.BuildData("Constrict", $"2d6", "Heat", 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Salamander_Average].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Salamander_Average].Add(AttackHelper.BuildData("Heat", "1d6 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));

                testCases[CreatureConstants.Salamander_Noble].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Salamander_Noble].Add(AttackHelper.BuildData("Tail Slap", $"2d8", "Heat", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Salamander_Noble].Add(AttackHelper.BuildData("Constrict", $"2d8", "Heat", 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Salamander_Noble].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Salamander_Noble].Add(AttackHelper.BuildData("Heat", "1d8 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Salamander_Noble].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Satyr].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, false, false));
                testCases[CreatureConstants.Satyr].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 0, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Satyr].Add(AttackHelper.BuildData("Head butt", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Satyr_WithPipes].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, false, false));
                testCases[CreatureConstants.Satyr_WithPipes].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 0, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Satyr_WithPipes].Add(AttackHelper.BuildData("Head butt", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Satyr_WithPipes].Add(AttackHelper.BuildData("Pipes", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.Scorpion_Monstrous_Tiny].Add(AttackHelper.BuildData("Claw", $"1d2", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Tiny].Add(AttackHelper.BuildData("Sting", $"1d2", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Tiny].Add(AttackHelper.BuildData("Constrict", $"1d2", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Tiny].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Tiny].Add(AttackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Scorpion_Monstrous_Small].Add(AttackHelper.BuildData("Claw", $"1d3", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Small].Add(AttackHelper.BuildData("Sting", $"1d3", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Small].Add(AttackHelper.BuildData("Constrict", $"1d3", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Small].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Small].Add(AttackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d2 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Scorpion_Monstrous_Medium].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Medium].Add(AttackHelper.BuildData("Sting", $"1d4", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Medium].Add(AttackHelper.BuildData("Constrict", $"1d4", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Medium].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Medium].Add(AttackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d3 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Scorpion_Monstrous_Large].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Large].Add(AttackHelper.BuildData("Sting", $"1d6", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Large].Add(AttackHelper.BuildData("Constrict", $"1d6", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Large].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Large].Add(AttackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d4 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Scorpion_Monstrous_Huge].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Huge].Add(AttackHelper.BuildData("Sting", $"1d8", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Huge].Add(AttackHelper.BuildData("Constrict", $"1d8", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Huge].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Huge].Add(AttackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan].Add(AttackHelper.BuildData("Sting", $"2d6", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan].Add(AttackHelper.BuildData("Constrict", $"2d6", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan].Add(AttackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d8 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Scorpion_Monstrous_Colossal].Add(AttackHelper.BuildData("Claw", $"2d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Colossal].Add(AttackHelper.BuildData("Sting", $"2d8", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Colossal].Add(AttackHelper.BuildData("Constrict", $"2d8", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Colossal].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Colossal].Add(AttackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d10 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Scorpionfolk].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Scorpionfolk].Add(AttackHelper.BuildData("Sting", $"1d8", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpionfolk].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpionfolk].Add(AttackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d4 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Scorpionfolk].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Scorpionfolk].Add(AttackHelper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.SeaCat].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.SeaCat].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.SeaCat].Add(AttackHelper.BuildData("Rend", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.SeaHag].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.SeaHag].Add(AttackHelper.BuildData("Horrific Appearance", string.Empty, "2d6 Strength damage", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.SeaHag].Add(AttackHelper.BuildData("Evil Eye", string.Empty, string.Empty, 0, "supernatural ability", 3, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));

                testCases[CreatureConstants.Shadow].Add(AttackHelper.BuildData("Incorporeal touch", string.Empty, "Strength Damage", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Shadow].Add(AttackHelper.BuildData("Strength Damage", string.Empty, "1d6 Str", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Shadow].Add(AttackHelper.BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Shadow_Greater].Add(AttackHelper.BuildData("Incorporeal touch", string.Empty, "Strength Damage", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Shadow_Greater].Add(AttackHelper.BuildData("Strength Damage", string.Empty, "1d8 Str", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Shadow_Greater].Add(AttackHelper.BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.ShadowMastiff].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.ShadowMastiff].Add(AttackHelper.BuildData("Bay", string.Empty, "panicked 2d4 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.ShadowMastiff].Add(AttackHelper.BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.ShamblingMound].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.ShamblingMound].Add(AttackHelper.BuildData("Constrict", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.ShamblingMound].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Shark_Dire].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Shark_Dire].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Shark_Dire].Add(AttackHelper.BuildData("Swallow Whole", "2d6+6 bludgeoning", "1d8+4 acid", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Shark_Huge].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Shark_Large].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Shark_Medium].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.ShieldGuardian].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.ShockerLizard].Add(AttackHelper.BuildData("Bite", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.ShockerLizard].Add(AttackHelper.BuildData("Stunning Shock", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.ShockerLizard].Add(AttackHelper.BuildData("Lethal Shock", $"2d8 electricity per shocker lizard", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, false, true, true, true, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Shrieker].Add(AttackHelper.BuildData("Shriek", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Skum].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Skum].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Skum].Add(AttackHelper.BuildData("Rake", $"1d6", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Slaad_Red].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Slaad_Red].Add(AttackHelper.BuildData("Claw", $"1d4", "Implant", 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Slaad_Red].Add(AttackHelper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Slaad_Red].Add(AttackHelper.BuildData("Implant", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Slaad_Red].Add(AttackHelper.BuildData("Stunning Croak", string.Empty, "Stunned 1d3 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Slaad_Red].Add(AttackHelper.BuildData("Summon Slaad", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Slaad_Blue].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Slaad_Blue].Add(AttackHelper.BuildData("Bite", $"2d8", "Disease", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Slaad_Blue].Add(AttackHelper.BuildData("Disease", string.Empty, "Slaad Fever", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Slaad_Blue].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Slaad_Blue].Add(AttackHelper.BuildData("Slaad Fever", string.Empty, "incubation period 1 day, damage 1d3 Dex and 1d3 Cha", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Slaad_Blue].Add(AttackHelper.BuildData("Summon Slaad", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Slaad_Green].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Slaad_Green].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Slaad_Green].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Slaad_Green].Add(AttackHelper.BuildData("Summon Slaad", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Slaad_Gray].Add(AttackHelper.BuildData("Claw", $"2d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Slaad_Gray].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Slaad_Gray].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Slaad_Gray].Add(AttackHelper.BuildData("Summon Slaad", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Slaad_Death].Add(AttackHelper.BuildData("Claw", $"3d6", "Stun", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Slaad_Death].Add(AttackHelper.BuildData("Bite", $"2d10", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Slaad_Death].Add(AttackHelper.BuildData("Stun", string.Empty, "Stunned 1 round", 0, "extraordinary ability", 3, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Wisdom, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Slaad_Death].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Slaad_Death].Add(AttackHelper.BuildData("Summon Slaad", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Snake_Constrictor].Add(AttackHelper.BuildData("Bite", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Snake_Constrictor].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Snake_Constrictor].Add(AttackHelper.BuildData("Constrict", $"1d3", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Snake_Constrictor_Giant].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Snake_Constrictor_Giant].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Snake_Constrictor_Giant].Add(AttackHelper.BuildData("Constrict", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Snake_Viper_Tiny].Add(AttackHelper.BuildData("Bite", $"1", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Snake_Viper_Tiny].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Snake_Viper_Small].Add(AttackHelper.BuildData("Bite", $"1d2", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Snake_Viper_Small].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Snake_Viper_Medium].Add(AttackHelper.BuildData("Bite", $"1d4", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Snake_Viper_Medium].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Snake_Viper_Large].Add(AttackHelper.BuildData("Bite", $"1d4", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Snake_Viper_Large].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Snake_Viper_Huge].Add(AttackHelper.BuildData("Bite", $"1d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Snake_Viper_Huge].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spectre].Add(AttackHelper.BuildData("Incorporeal touch", $"1d8", "Energy Drain", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spectre].Add(AttackHelper.BuildData("Energy Drain", string.Empty, "2 negative levels", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Spectre].Add(AttackHelper.BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny].Add(AttackHelper.BuildData("Bite", $"1d3", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d2 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Small].Add(AttackHelper.BuildData("Bite", $"1d4", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Small].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d3 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium].Add(AttackHelper.BuildData("Bite", $"1d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d4 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Large].Add(AttackHelper.BuildData("Bite", $"1d8", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Large].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge].Add(AttackHelper.BuildData("Bite", $"2d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d8 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan].Add(AttackHelper.BuildData("Bite", $"2d8", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 2d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal].Add(AttackHelper.BuildData("Bite", $"4d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 2d8 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny].Add(AttackHelper.BuildData("Bite", $"1d3", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d2 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny].Add(AttackHelper.BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small].Add(AttackHelper.BuildData("Bite", $"1d4", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d3 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small].Add(AttackHelper.BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium].Add(AttackHelper.BuildData("Bite", $"1d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d4 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium].Add(AttackHelper.BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large].Add(AttackHelper.BuildData("Bite", $"1d8", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large].Add(AttackHelper.BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge].Add(AttackHelper.BuildData("Bite", $"2d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d8 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge].Add(AttackHelper.BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan].Add(AttackHelper.BuildData("Bite", $"2d8", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 2d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan].Add(AttackHelper.BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal].Add(AttackHelper.BuildData("Bite", $"4d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 2d8 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal].Add(AttackHelper.BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, AbilityConstants.Constitution));

                testCases[CreatureConstants.SpiderEater].Add(AttackHelper.BuildData("Sting", $"1d8", "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.SpiderEater].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.SpiderEater].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial damage none, secondary damage paralysis for 1d8+5 weeks", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.SpiderEater].Add(AttackHelper.BuildData("Implant", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Spider_Swarm].Add(AttackHelper.BuildData("Swarm", $"1d6", "poison", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Swarm].Add(AttackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d3 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Spider_Swarm].Add(AttackHelper.BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Squid].Add(AttackHelper.BuildData("Arms", $"0", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Squid].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Squid].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Squid_Giant].Add(AttackHelper.BuildData("Tentacle", $"1d6", string.Empty, 1, "melee", 10, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Squid_Giant].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Squid_Giant].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Squid_Giant].Add(AttackHelper.BuildData("Constrict", $"1d6", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.StagBeetle_Giant].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.StagBeetle_Giant].Add(AttackHelper.BuildData("Trample", $"2d8", string.Empty, 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Stirge].Add(AttackHelper.BuildData("Touch", string.Empty, "Attach", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Stirge].Add(AttackHelper.BuildData("Attach", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Stirge].Add(AttackHelper.BuildData("Blood Drain", string.Empty, "1d4 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Succubus].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Succubus].Add(AttackHelper.BuildData("Energy Drain", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, string.Empty, AbilityConstants.Charisma));
                testCases[CreatureConstants.Succubus].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Succubus].Add(AttackHelper.BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Tarrasque].Add(AttackHelper.BuildData("Bite", $"4d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Tarrasque].Add(AttackHelper.BuildData("Horn", $"1d10", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tarrasque].Add(AttackHelper.BuildData("Claw", $"1d12", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tarrasque].Add(AttackHelper.BuildData("Tail Slap", $"3d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tarrasque].Add(AttackHelper.BuildData("Augmented Critical", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tarrasque].Add(AttackHelper.BuildData("Frightful Presence", string.Empty, "Shaken", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.Tarrasque].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tarrasque].Add(AttackHelper.BuildData("Rush", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true));
                testCases[CreatureConstants.Tarrasque].Add(AttackHelper.BuildData("Swallow Whole", "2d8+8 bludgeoning + 2d8+6 acid", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Tendriculos].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Tendriculos].Add(AttackHelper.BuildData("Tendril", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tendriculos].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tendriculos].Add(AttackHelper.BuildData("Swallow Whole", "2d6 acid", "Paralysis", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tendriculos].Add(AttackHelper.BuildData("Paralysis", string.Empty, "paralyzed for 3d6 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Thoqqua].Add(AttackHelper.BuildData("Slam", $"1d6", "Heat, Burn", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Thoqqua].Add(AttackHelper.BuildData("Heat", $"2d6 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Thoqqua].Add(AttackHelper.BuildData("Burn", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Tiefling].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Tiefling].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Tiefling].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Tiger].Add(AttackHelper.BuildData("Claw", $"1d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Tiger].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tiger].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tiger].Add(AttackHelper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Tiger].Add(AttackHelper.BuildData("Rake", $"1d8", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Tiger_Dire].Add(AttackHelper.BuildData("Claw", $"2d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Tiger_Dire].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tiger_Dire].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tiger_Dire].Add(AttackHelper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Tiger_Dire].Add(AttackHelper.BuildData("Rake", $"2d4", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData("Slam", $"1d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Titan].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Toad].Add(new[] { None });

                testCases[CreatureConstants.Tojanida_Juvenile].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Tojanida_Juvenile].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tojanida_Juvenile].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tojanida_Juvenile].Add(AttackHelper.BuildData("Ink Cloud", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Tojanida_Adult].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Tojanida_Adult].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tojanida_Adult].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tojanida_Adult].Add(AttackHelper.BuildData("Ink Cloud", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Tojanida_Elder].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Tojanida_Elder].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tojanida_Elder].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tojanida_Elder].Add(AttackHelper.BuildData("Ink Cloud", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Treant].Add(AttackHelper.BuildData("Slam", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Treant].Add(AttackHelper.BuildData("Animate Trees", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Treant].Add(AttackHelper.BuildData("Double Damage Against Objects", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Treant].Add(AttackHelper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Triceratops].Add(AttackHelper.BuildData("Gore", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Triceratops].Add(AttackHelper.BuildData("Powerful charge", $"4d8", string.Empty, 2, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Triceratops].Add(AttackHelper.BuildData("Trample", $"2d12", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Triton].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Triton].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Triton].Add(AttackHelper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Triton].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Troglodyte].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Troglodyte].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Troglodyte].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Troglodyte].Add(AttackHelper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Troglodyte].Add(AttackHelper.BuildData("Stench", string.Empty, "Sickened 10 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Troll].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Troll].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Troll].Add(AttackHelper.BuildData("Rend", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Troll_Scrag].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Troll_Scrag].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Troll_Scrag].Add(AttackHelper.BuildData("Rend", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.TrumpetArchon].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.TrumpetArchon].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.TrumpetArchon].Add(AttackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.TrumpetArchon].Add(AttackHelper.BuildData("Trumpet", string.Empty, "1d4 rounds paralyzed", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Tyrannosaurus].Add(AttackHelper.BuildData("Bite", $"3d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Tyrannosaurus].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tyrannosaurus].Add(AttackHelper.BuildData("Swallow Whole", "2d8 bludgeoning + 8 acid", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.UmberHulk].Add(AttackHelper.BuildData("Claw", $"2d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.UmberHulk].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.UmberHulk].Add(AttackHelper.BuildData("Confusing Gaze", string.Empty, SpellConstants.Confusion, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.UmberHulk_TrulyHorrid].Add(AttackHelper.BuildData("Claw", $"3d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.UmberHulk_TrulyHorrid].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.UmberHulk_TrulyHorrid].Add(AttackHelper.BuildData("Confusing Gaze", string.Empty, SpellConstants.Confusion, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.Unicorn].Add(AttackHelper.BuildData("Horn", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Unicorn].Add(AttackHelper.BuildData("Hoof", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.VampireSpawn].Add(AttackHelper.BuildData("Slam", $"1d6", "Energy Drain", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.VampireSpawn].Add(AttackHelper.BuildData("Blood Drain", string.Empty, "1d4 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.VampireSpawn].Add(AttackHelper.BuildData("Domination", string.Empty, SpellConstants.DominatePerson, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.VampireSpawn].Add(AttackHelper.BuildData("Energy Drain", string.Empty, "1 negative level", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Vargouille].Add(AttackHelper.BuildData("Bite", $"1d4", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Vargouille].Add(AttackHelper.BuildData("Shriek", string.Empty, "paralyzed 2d4 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 1));
                testCases[CreatureConstants.Vargouille].Add(AttackHelper.BuildData("Kiss", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 4));
                testCases[CreatureConstants.Vargouille].Add(AttackHelper.BuildData("Poison", string.Empty, "unable to heal the vargouille’s bite damage naturally or magically", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 1));

                testCases[CreatureConstants.VioletFungus].Add(AttackHelper.BuildData("Tentacle", $"1d6", "Poison", 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.VioletFungus].Add(AttackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d4 Str and 1d4 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Vrock].Add(AttackHelper.BuildData("Claw", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Vrock].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Vrock].Add(AttackHelper.BuildData("Talon", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Vrock].Add(AttackHelper.BuildData("Dance of Ruin", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true, SaveConstants.Reflex, AbilityConstants.Charisma));
                testCases[CreatureConstants.Vrock].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Vrock].Add(AttackHelper.BuildData("Spores", "1d8", string.Empty, 0, "melee", 1, FeatConstants.Frequencies.Minute, false, true, true, true));
                testCases[CreatureConstants.Vrock].Add(AttackHelper.BuildData("Stunning Screech", string.Empty, string.Empty, 0, "melee", 1, FeatConstants.Frequencies.Hour, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Vrock].Add(AttackHelper.BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Wasp_Giant].Add(AttackHelper.BuildData("Sting", $"1d3", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wasp_Giant].Add(AttackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d6 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Weasel].Add(AttackHelper.BuildData("Bite", $"1d3", "Attach", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Weasel].Add(AttackHelper.BuildData("Attach", $"1d3", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Weasel_Dire].Add(AttackHelper.BuildData("Bite", $"1d6", "Attach", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Weasel_Dire].Add(AttackHelper.BuildData("Attach", string.Empty, "Blood Drain", 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Weasel_Dire].Add(AttackHelper.BuildData("Blood Drain", string.Empty, "1d4 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Whale_Baleen].Add(AttackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Whale_Cachalot].Add(AttackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Whale_Cachalot].Add(AttackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Whale_Orca].Add(AttackHelper.BuildData("Bite", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Wight].Add(AttackHelper.BuildData("Slam", $"1d4", "Energy Drain", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wight].Add(AttackHelper.BuildData("Energy Drain", string.Empty, "1 negative level", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Wight].Add(AttackHelper.BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.WillOWisp].Add(AttackHelper.BuildData("Shock", string.Empty, "2d8 electricity", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.WinterWolf].Add(AttackHelper.BuildData("Bite", $"1d8", "Freezing Bite", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.WinterWolf].Add(AttackHelper.BuildData("Breath Weapon", "4d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
                testCases[CreatureConstants.WinterWolf].Add(AttackHelper.BuildData("Freezing Bite", "1d6 cold", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
                testCases[CreatureConstants.WinterWolf].Add(AttackHelper.BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Wolf].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wolf].Add(AttackHelper.BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Wolf_Dire].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wolf_Dire].Add(AttackHelper.BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Wolverine].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wolverine].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Wolverine].Add(AttackHelper.BuildData("Rage", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));

                testCases[CreatureConstants.Wolverine_Dire].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wolverine_Dire].Add(AttackHelper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Wolverine_Dire].Add(AttackHelper.BuildData("Rage", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));

                testCases[CreatureConstants.Worg].Add(AttackHelper.BuildData("Bite", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Worg].Add(AttackHelper.BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Wraith].Add(AttackHelper.BuildData("Incorporeal touch", "1d4", $"Constitution Drain", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wraith].Add(AttackHelper.BuildData("Constitution Drain", string.Empty, $"1d4 Con", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Wraith].Add(AttackHelper.BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Wraith_Dread].Add(AttackHelper.BuildData("Incorporeal touch", "2d6", $"Constitution Drain", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wraith_Dread].Add(AttackHelper.BuildData("Constitution Drain", string.Empty, $"1d8 Con", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Wraith_Dread].Add(AttackHelper.BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Wyvern].Add(AttackHelper.BuildData("Sting", $"1d6", "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wyvern].Add(AttackHelper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Wyvern].Add(AttackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Wyvern].Add(AttackHelper.BuildData("Talon", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Wyvern].Add(AttackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 2d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Wyvern].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Xill].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 2, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Xill].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 2, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Xill].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Xill].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Xill].Add(AttackHelper.BuildData("Bite", string.Empty, "Paralysis", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Xill].Add(AttackHelper.BuildData("Implant", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Xill].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Xill].Add(AttackHelper.BuildData("Paralysis", string.Empty, "paralyzed for 1d4 hours", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Xorn_Minor].Add(AttackHelper.BuildData("Bite", "2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Xorn_Minor].Add(AttackHelper.BuildData("Claw", $"1d3", string.Empty, 0.5, "melee", 3, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Xorn_Average].Add(AttackHelper.BuildData("Bite", "4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Xorn_Average].Add(AttackHelper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 3, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Xorn_Elder].Add(AttackHelper.BuildData("Bite", "4d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Xorn_Elder].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 3, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.YethHound].Add(AttackHelper.BuildData("Bite", "1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.YethHound].Add(AttackHelper.BuildData("Bay", string.Empty, "panicked 2d4 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.YethHound].Add(AttackHelper.BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Yrthak].Add(AttackHelper.BuildData("Bite", "2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Yrthak].Add(AttackHelper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Yrthak].Add(AttackHelper.BuildData("Sonic Lance", "6d6 sonic", string.Empty, 0, "ranged touch", 1, $"2 {FeatConstants.Frequencies.Round}", false, true, true, false));
                testCases[CreatureConstants.Yrthak].Add(AttackHelper.BuildData("Explosion", "2d6 piercing", string.Empty, 0, "supernatural ability", 1, $"2 {FeatConstants.Frequencies.Round}", true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.YuanTi_Pureblood].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.YuanTi_Pureblood].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.YuanTi_Pureblood].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(AttackHelper.BuildData("Bite", "1d6", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(AttackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(AttackHelper.BuildData("Produce Acid", "3d6 acid", "initial and secondary damage 1d6 Con", 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(AttackHelper.BuildData("Bite", "1d4", "Poison", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(AttackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(AttackHelper.BuildData("Produce Acid", "3d6 acid", "initial and secondary damage 1d6 Con", 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(AttackHelper.BuildData("Bite", "1d6", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(AttackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(AttackHelper.BuildData("Produce Acid", "3d6 acid", "initial and secondary damage 1d6 Con", 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(AttackHelper.BuildData("Constrict", $"1d4", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(AttackHelper.BuildData("Bite", "1d6", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(AttackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(AttackHelper.BuildData("Produce Acid", "3d6 acid", "initial and secondary damage 1d6 Con", 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(AttackHelper.BuildData("Constrict", $"1d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.YuanTi_Abomination].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.YuanTi_Abomination].Add(AttackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.YuanTi_Abomination].Add(AttackHelper.BuildData("Bite", "2d6", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.YuanTi_Abomination].Add(AttackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Abomination].Add(AttackHelper.BuildData("Aversion", string.Empty, "aversion 10 minutes", 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.YuanTi_Abomination].Add(AttackHelper.BuildData("Produce Acid", "3d6 acid", "initial and secondary damage 1d6 Con", 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Abomination].Add(AttackHelper.BuildData("Constrict", $"1d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.YuanTi_Abomination].Add(AttackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.YuanTi_Abomination].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Zelekhut].Add(AttackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Zelekhut].Add(AttackHelper.BuildData("Electrified Weapon", "1d6 electricity", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Zelekhut].Add(AttackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

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
