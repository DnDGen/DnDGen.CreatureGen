using DnDGen.CreatureGen.Abilities;
using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Feats;
using DnDGen.CreatureGen.Magics;
using DnDGen.CreatureGen.Selectors.Helpers;
using DnDGen.TreasureGen.Items;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Attacks
{
    public class AttackTestData
    {
        public const string None = "NONE";

        public static IEnumerable Templates
        {
            get
            {
                var testCases = new Dictionary<string, List<string[]>>();
                var helper = new AttackHelper();
                var templates = CreatureConstants.Templates.GetAll();

                foreach (var template in templates)
                {
                    testCases[template] = new List<string[]>();
                }

                testCases[CreatureConstants.Templates.CelestialCreature].Add(helper.BuildData("Smite Evil", "0", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Templates.FiendishCreature].Add(helper.BuildData("Smite Good", "0", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Templates.Ghost].Add(helper.BuildData("Corrupting Gaze", "2d10", "1d4 Charisma damage", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.Templates.Ghost].Add(helper.BuildData("Corrupting Touch", "1d6", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Templates.Ghost].Add(helper.BuildData("Draining Touch", string.Empty, "1d4 Ability points (of ghost's choosing), Ghost heals 5 points of damage", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Templates.Ghost].Add(helper.BuildData("Frightful Moan", string.Empty, "Panic for 2d4 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Templates.Ghost].Add(helper.BuildData("Horrific Appearance", string.Empty, "1d4 Str, 1d4 Dex, and 1d4 Con", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.Templates.Ghost].Add(helper.BuildData("Malevolence", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma, 5));
                testCases[CreatureConstants.Templates.Ghost].Add(helper.BuildData("Manifestation", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Templates.Ghost].Add(helper.BuildData("Telekinesis", string.Empty, "Panic for 2d4 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));

                testCases[CreatureConstants.Templates.HalfCelestial].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Templates.HalfCelestial].Add(helper.BuildData("Smite Evil", "0", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Templates.HalfDragon_Black].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.HalfDragon_Black].Add(helper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.HalfDragon_Black].Add(helper.BuildData("Breath Weapon", "6d8 acid", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.HalfDragon_Blue].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.HalfDragon_Blue].Add(helper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.HalfDragon_Blue].Add(helper.BuildData("Breath Weapon", "6d8 electricity", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.HalfDragon_Brass].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.HalfDragon_Brass].Add(helper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.HalfDragon_Brass].Add(helper.BuildData("Breath Weapon", "6d8 fire", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.HalfDragon_Bronze].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.HalfDragon_Bronze].Add(helper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.HalfDragon_Bronze].Add(helper.BuildData("Breath Weapon", "6d8 electricity", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.HalfDragon_Copper].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.HalfDragon_Copper].Add(helper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.HalfDragon_Copper].Add(helper.BuildData("Breath Weapon", "6d8 acid", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.HalfDragon_Gold].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.HalfDragon_Gold].Add(helper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.HalfDragon_Gold].Add(helper.BuildData("Breath Weapon", "6d8 fire", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.HalfDragon_Green].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.HalfDragon_Green].Add(helper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.HalfDragon_Green].Add(helper.BuildData("Breath Weapon", "6d8 acid gas", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.HalfDragon_Red].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.HalfDragon_Red].Add(helper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.HalfDragon_Red].Add(helper.BuildData("Breath Weapon", "6d8 fire", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.HalfDragon_Silver].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.HalfDragon_Silver].Add(helper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.HalfDragon_Silver].Add(helper.BuildData("Breath Weapon", "6d8 cold", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.HalfDragon_White].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.HalfDragon_White].Add(helper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.HalfDragon_White].Add(helper.BuildData("Breath Weapon", "6d8 cold", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.HalfFiend].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.HalfFiend].Add(helper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.HalfFiend].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Templates.HalfFiend].Add(helper.BuildData("Smite Good", "0", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Templates.Lich].Add(helper.BuildData("Touch", $"1d8+5", "Paralyzing Touch", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Templates.Lich].Add(helper.BuildData("Fear Aura", string.Empty, "Fear", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Templates.Lich].Add(helper.BuildData("Paralyzing Touch", string.Empty, "Paralyzed", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Charisma));

                testCases[CreatureConstants.Templates.None].Add(new[] { None });

                testCases[CreatureConstants.Templates.Skeleton].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Templates.Vampire].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Vampire].Add(helper.BuildData("Blood Drain", string.Empty, "1d4 Con, Vampire gains 5 HP", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Templates.Vampire].Add(helper.BuildData("Children of the Night", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Templates.Vampire].Add(helper.BuildData("Dominate", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Templates.Vampire].Add(helper.BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Templates.Vampire].Add(helper.BuildData("Energy Drain", string.Empty, "2 negative levels, Vampire gains 5 HP", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural].Add(helper.BuildData("Claw (in Hybrid form)", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural].Add(helper.BuildData("Bite (in Hybrid form)", $"1d6", "Curse of Lycanthropy", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural].Add(helper.BuildData("Curse of Lycanthropy", string.Empty, "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.Lycanthrope_Boar_Natural].Add(helper.BuildData("Claw (in Hybrid form)", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Boar_Natural].Add(helper.BuildData("Bite (in Hybrid form)", $"1d6", "Curse of Lycanthropy", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Boar_Natural].Add(helper.BuildData("Curse of Lycanthropy", string.Empty, "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural].Add(helper.BuildData("Claw (in Hybrid form)", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural].Add(helper.BuildData("Gore (in Hybrid form)", $"1d6", "Curse of Lycanthropy", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural].Add(helper.BuildData("Curse of Lycanthropy", string.Empty, "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural].Add(helper.BuildData("Claw (in Hybrid form)", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural].Add(helper.BuildData("Bite (in Hybrid form)", $"1d6", "Curse of Lycanthropy", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural].Add(helper.BuildData("Curse of Lycanthropy", string.Empty, "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Natural].Add(helper.BuildData("Claw (in Hybrid form)", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Natural].Add(helper.BuildData("Bite (in Hybrid form)", $"1d6", "Curse of Lycanthropy", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Natural].Add(helper.BuildData("Curse of Lycanthropy", string.Empty, "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Natural].Add(helper.BuildData("Claw (in Hybrid form)", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Natural].Add(helper.BuildData("Bite (in Hybrid form)", $"1d6", "Curse of Lycanthropy", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Natural].Add(helper.BuildData("Curse of Lycanthropy", string.Empty, "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural].Add(helper.BuildData("Claw (in Hybrid form)", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural].Add(helper.BuildData("Bite (in Hybrid form)", $"1d6", "Curse of Lycanthropy", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural].Add(helper.BuildData("Curse of Lycanthropy", string.Empty, "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted].Add(helper.BuildData("Claw (in Hybrid form)", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted].Add(helper.BuildData("Bite (in Hybrid form)", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Templates.Lycanthrope_Boar_Afflicted].Add(helper.BuildData("Claw (in Hybrid form)", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Boar_Afflicted].Add(helper.BuildData("Bite (in Hybrid form)", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted].Add(helper.BuildData("Claw (in Hybrid form)", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted].Add(helper.BuildData("Gore (in Hybrid form)", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted].Add(helper.BuildData("Claw (in Hybrid form)", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted].Add(helper.BuildData("Bite (in Hybrid form)", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted].Add(helper.BuildData("Claw (in Hybrid form)", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted].Add(helper.BuildData("Bite (in Hybrid form)", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted].Add(helper.BuildData("Claw (in Hybrid form)", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted].Add(helper.BuildData("Bite (in Hybrid form)", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted].Add(helper.BuildData("Claw (in Hybrid form)", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted].Add(helper.BuildData("Bite (in Hybrid form)", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Templates.Zombie].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                return TestDataHelper.EnumerateTestCases(testCases);
            }
        }

        public static IEnumerable Creatures
        {
            get
            {
                var testCases = new Dictionary<string, List<string[]>>();
                var helper = new AttackHelper();
                var creatures = CreatureConstants.GetAll();

                foreach (var creature in creatures)
                {
                    testCases[creature] = new List<string[]>();
                }

                testCases[CreatureConstants.Aasimar].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Aasimar].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Aasimar].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Aasimar].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Aboleth].Add(helper.BuildData("Tentacle", $"1d6", "Slime", 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Aboleth].Add(helper.BuildData("Enslave", string.Empty, string.Empty, 0, "supernatural ability", 3, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Aboleth].Add(helper.BuildData("Slime", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Aboleth].Add(helper.BuildData(FeatConstants.SpecialQualities.Psionic, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Achaierai].Add(helper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Achaierai].Add(helper.BuildData("Bite", $"4d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Achaierai].Add(helper.BuildData("Black cloud", "2d6", "insanity", 0, "extraordinary ability", 3, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Allip].Add(helper.BuildData("Incorporeal touch", string.Empty, $"1d4 Wisdom drain", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Allip].Add(helper.BuildData("Babble", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Allip].Add(helper.BuildData("Madness", string.Empty, "1d4 Wisdom drain", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));
                testCases[CreatureConstants.Allip].Add(helper.BuildData("Wisdom drain", "1d4", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));

                testCases[CreatureConstants.Androsphinx].Add(helper.BuildData("Claw", $"2d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Androsphinx].Add(helper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Androsphinx].Add(helper.BuildData("Rake", $"2d4", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Androsphinx].Add(helper.BuildData("Roar", string.Empty, string.Empty, 0, "supernatural ability", 3, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Androsphinx].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Angel_AstralDeva].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Angel_AstralDeva].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Angel_AstralDeva].Add(helper.BuildData("Stun", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Angel_AstralDeva].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Angel_Planetar].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Angel_Planetar].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Angel_Planetar].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Angel_Planetar].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Angel_Solar].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Angel_Solar].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Angel_Solar].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Angel_Solar].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Angel_Solar].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Anvil_Tiny].Add(helper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Anvil_Small].Add(helper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Anvil_Medium].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Anvil_Large].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Anvil_Large].Add(helper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Anvil_Huge].Add(helper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Anvil_Huge].Add(helper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Anvil_Gargantuan].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Anvil_Gargantuan].Add(helper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Anvil_Colossal].Add(helper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Anvil_Colossal].Add(helper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Tiny].Add(helper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Small].Add(helper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Medium].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Large].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Stone_Large].Add(helper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Huge].Add(helper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Stone_Huge].Add(helper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Gargantuan].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Stone_Gargantuan].Add(helper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Colossal].Add(helper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Stone_Colossal].Add(helper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Tiny].Add(helper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Small].Add(helper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Medium].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Large].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Wood_Large].Add(helper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Huge].Add(helper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Wood_Huge].Add(helper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Gargantuan].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Wood_Gargantuan].Add(helper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Colossal].Add(helper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Wood_Colossal].Add(helper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Box_Tiny].Add(helper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Box_Small].Add(helper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Box_Medium].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Box_Large].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Box_Large].Add(helper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Box_Huge].Add(helper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Box_Huge].Add(helper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Box_Gargantuan].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Box_Gargantuan].Add(helper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Box_Colossal].Add(helper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Box_Colossal].Add(helper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Tiny].Add(helper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Tiny].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Tiny].Add(helper.BuildData("Constrict", $"1d3", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Small].Add(helper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Small].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Small].Add(helper.BuildData("Constrict", $"1d4", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Medium].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Medium].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Medium].Add(helper.BuildData("Constrict", $"1d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Large].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Large].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Large].Add(helper.BuildData("Constrict", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Huge].Add(helper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Huge].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Huge].Add(helper.BuildData("Constrict", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Gargantuan].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Gargantuan].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Gargantuan].Add(helper.BuildData("Constrict", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Colossal].Add(helper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Colossal].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Colossal].Add(helper.BuildData("Constrict", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carriage_Tiny].Add(helper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Carriage_Small].Add(helper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Carriage_Medium].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Carriage_Large].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carriage_Large].Add(helper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carriage_Huge].Add(helper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carriage_Huge].Add(helper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carriage_Gargantuan].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carriage_Gargantuan].Add(helper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carriage_Colossal].Add(helper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carriage_Colossal].Add(helper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Tiny].Add(helper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Tiny].Add(helper.BuildData("Constrict", $"1d3", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Small].Add(helper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Small].Add(helper.BuildData("Constrict", $"1d4", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Medium].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Medium].Add(helper.BuildData("Constrict", $"1d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Large].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Large].Add(helper.BuildData("Constrict", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Chain_Large].Add(helper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Huge].Add(helper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Huge].Add(helper.BuildData("Constrict", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Chain_Huge].Add(helper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Gargantuan].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Gargantuan].Add(helper.BuildData("Constrict", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Chain_Gargantuan].Add(helper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Colossal].Add(helper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Colossal].Add(helper.BuildData("Constrict", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Chain_Colossal].Add(helper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chair_Tiny].Add(helper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Chair_Small].Add(helper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Chair_Medium].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Chair_Large].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chair_Large].Add(helper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chair_Huge].Add(helper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chair_Huge].Add(helper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chair_Gargantuan].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chair_Gargantuan].Add(helper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chair_Colossal].Add(helper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chair_Colossal].Add(helper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Tiny].Add(helper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Tiny].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Tiny].Add(helper.BuildData("Constrict", $"1d3", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Small].Add(helper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Small].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Small].Add(helper.BuildData("Constrict", $"1d4", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Medium].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Medium].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Medium].Add(helper.BuildData("Constrict", $"1d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Large].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Large].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Large].Add(helper.BuildData("Constrict", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Huge].Add(helper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Huge].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Huge].Add(helper.BuildData("Constrict", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Gargantuan].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Gargantuan].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Gargantuan].Add(helper.BuildData("Constrict", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Colossal].Add(helper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Colossal].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Colossal].Add(helper.BuildData("Constrict", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Ladder_Tiny].Add(helper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Ladder_Small].Add(helper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Ladder_Medium].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Ladder_Large].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Ladder_Large].Add(helper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Ladder_Huge].Add(helper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Ladder_Huge].Add(helper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Ladder_Gargantuan].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Ladder_Gargantuan].Add(helper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Ladder_Colossal].Add(helper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Ladder_Colossal].Add(helper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Tiny].Add(helper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Tiny].Add(helper.BuildData("Constrict", $"1d3", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Small].Add(helper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Small].Add(helper.BuildData("Constrict", $"1d4", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Medium].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Medium].Add(helper.BuildData("Constrict", $"1d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Large].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Large].Add(helper.BuildData("Constrict", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Huge].Add(helper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Huge].Add(helper.BuildData("Constrict", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Gargantuan].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Gargantuan].Add(helper.BuildData("Constrict", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Colossal].Add(helper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Colossal].Add(helper.BuildData("Constrict", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Tiny].Add(helper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Tiny].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Tiny].Add(helper.BuildData("Constrict", $"1d3", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Small].Add(helper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Small].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Small].Add(helper.BuildData("Constrict", $"1d4", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Medium].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Medium].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Medium].Add(helper.BuildData("Constrict", $"1d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Large].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Large].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Large].Add(helper.BuildData("Constrict", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Huge].Add(helper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Huge].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Huge].Add(helper.BuildData("Constrict", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Gargantuan].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Gargantuan].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Gargantuan].Add(helper.BuildData("Constrict", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Colossal].Add(helper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Colossal].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Colossal].Add(helper.BuildData("Constrict", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Sled_Tiny].Add(helper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Sled_Small].Add(helper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Sled_Medium].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Sled_Large].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Sled_Large].Add(helper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Sled_Huge].Add(helper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Sled_Huge].Add(helper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Sled_Gargantuan].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Sled_Gargantuan].Add(helper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Sled_Colossal].Add(helper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Sled_Colossal].Add(helper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Tiny].Add(helper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Small].Add(helper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Medium].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Large].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Large].Add(helper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Huge].Add(helper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Huge].Add(helper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Gargantuan].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Gargantuan].Add(helper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Colossal].Add(helper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Colossal].Add(helper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Tiny].Add(helper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Small].Add(helper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Medium].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Large].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Large].Add(helper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Huge].Add(helper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Huge].Add(helper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Gargantuan].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Gargantuan].Add(helper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Colossal].Add(helper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Colossal].Add(helper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Stool_Tiny].Add(helper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Stool_Small].Add(helper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Stool_Medium].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Stool_Large].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Stool_Large].Add(helper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Stool_Huge].Add(helper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Stool_Huge].Add(helper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Stool_Gargantuan].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Stool_Gargantuan].Add(helper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Stool_Colossal].Add(helper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Stool_Colossal].Add(helper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Table_Tiny].Add(helper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Table_Small].Add(helper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Table_Medium].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Table_Large].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Table_Large].Add(helper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Table_Huge].Add(helper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Table_Huge].Add(helper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Table_Gargantuan].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Table_Gargantuan].Add(helper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Table_Colossal].Add(helper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Table_Colossal].Add(helper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Tiny].Add(helper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Tiny].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Tiny].Add(helper.BuildData("Constrict", $"1d3", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Small].Add(helper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Small].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Small].Add(helper.BuildData("Constrict", $"1d4", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Medium].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Medium].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Medium].Add(helper.BuildData("Constrict", $"1d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Large].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Large].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Large].Add(helper.BuildData("Constrict", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Huge].Add(helper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Huge].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Huge].Add(helper.BuildData("Constrict", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Gargantuan].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Gargantuan].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Gargantuan].Add(helper.BuildData("Constrict", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Colossal].Add(helper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Colossal].Add(helper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Colossal].Add(helper.BuildData("Constrict", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Wagon_Tiny].Add(helper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Wagon_Small].Add(helper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Wagon_Medium].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Wagon_Large].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Wagon_Large].Add(helper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Wagon_Huge].Add(helper.BuildData("Slam", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Wagon_Huge].Add(helper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Wagon_Gargantuan].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Wagon_Gargantuan].Add(helper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Wagon_Colossal].Add(helper.BuildData("Slam", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Wagon_Colossal].Add(helper.BuildData("Trample", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Ankheg].Add(helper.BuildData("Bite", $"2d6 + 1d4 acid", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ankheg].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Ankheg].Add(helper.BuildData("Spit Acid", $"4d4 acid", string.Empty, 0, "extraordinary ability", 1, $"6 {FeatConstants.Frequencies.Hour}", false, true, true, true));

                testCases[CreatureConstants.Annis].Add(helper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Annis].Add(helper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Annis].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Annis].Add(helper.BuildData("Rake", $"1d6", string.Empty, 1, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Annis].Add(helper.BuildData("Rend", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Annis].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Ant_Giant_Worker].Add(helper.BuildData("Bite", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ant_Giant_Worker].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Ant_Giant_Soldier].Add(helper.BuildData("Bite", $"2d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ant_Giant_Soldier].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Ant_Giant_Soldier].Add(helper.BuildData("Acid Sting", "1d4 Piercing damage + 1d4 acid damage", string.Empty, 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Ant_Giant_Queen].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ant_Giant_Queen].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Ape].Add(helper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ape].Add(helper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Ape_Dire].Add(helper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ape_Dire].Add(helper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Ape_Dire].Add(helper.BuildData("Rend", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Aranea].Add(helper.BuildData("Bite", $"1d6", "poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Aranea].Add(helper.BuildData("Poison", string.Empty, "Initial damage 1d6 Str, Secondary damage 2d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, false, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Aranea].Add(helper.BuildData("Web", string.Empty, string.Empty, 0, "ranged, extraordinary ability", 6, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.Aranea].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Arrowhawk_Juvenile].Add(helper.BuildData("Bite", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Arrowhawk_Juvenile].Add(helper.BuildData("Electricity ray", $"2d6", string.Empty, 0, "ranged touch, supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Arrowhawk_Adult].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Arrowhawk_Adult].Add(helper.BuildData("Electricity ray", $"2d8", string.Empty, 0, "ranged touch, supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Arrowhawk_Elder].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Arrowhawk_Elder].Add(helper.BuildData("Electricity ray", $"2d8", string.Empty, 0, "ranged touch, supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.AssassinVine].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AssassinVine].Add(helper.BuildData("Constrict", $"1d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AssassinVine].Add(helper.BuildData("Entangle", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.AssassinVine].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Athach].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Athach].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, false, false, false));
                testCases[CreatureConstants.Athach].Add(helper.BuildData("Bite", $"2d8", "poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Athach].Add(helper.BuildData("Rock", $"2d8", string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Athach].Add(helper.BuildData("Rock", $"2d8", string.Empty, 0.5, "ranged", 2, FeatConstants.Frequencies.Round, false, true, false, false));
                testCases[CreatureConstants.Athach].Add(helper.BuildData("Poison", string.Empty, $"Initial damage 1d6 Str, Secondary damage 2d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Avoral].Add(helper.BuildData("Claw", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Avoral].Add(helper.BuildData("Wing", $"2d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Avoral].Add(helper.BuildData("Fear Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Avoral].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Azer].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Azer].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Azer].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Azer].Add(helper.BuildData("Heat", "1 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));

                testCases[CreatureConstants.Babau].Add(helper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Babau].Add(helper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Babau].Add(helper.BuildData("Sneak Attack", $"2d6", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Babau].Add(helper.BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.Babau].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Baboon].Add(helper.BuildData("Bite", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Badger].Add(helper.BuildData("Claw", $"1d2", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Badger].Add(helper.BuildData("Bite", $"1d3", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Badger].Add(helper.BuildData("Rage", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Badger_Dire].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Badger_Dire].Add(helper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Badger_Dire].Add(helper.BuildData("Rage", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Balor].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Balor].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, false, false));
                testCases[CreatureConstants.Balor].Add(helper.BuildData("Slam", $"1d10", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Balor].Add(helper.BuildData("Death Throes", "100", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Life, false, true, false, true, SaveConstants.Reflex, AbilityConstants.Constitution));
                testCases[CreatureConstants.Balor].Add(helper.BuildData("Entangle", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, false, false, true));
                testCases[CreatureConstants.Balor].Add(helper.BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.Balor].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(helper.BuildData("Claw", $"2d8", "fear", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(helper.BuildData("Fear", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(helper.BuildData("Impale", "3d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(helper.BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Barghest].Add(helper.BuildData("Bite", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Barghest].Add(helper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Barghest].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Barghest].Add(helper.BuildData("Feed", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Barghest_Greater].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Barghest_Greater].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Barghest_Greater].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Barghest_Greater].Add(helper.BuildData("Feed", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Basilisk].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Basilisk].Add(helper.BuildData("Petrifying Gaze", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));

                testCases[CreatureConstants.Basilisk_Greater].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Basilisk_Greater].Add(helper.BuildData("Petrifying Gaze", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));

                testCases[CreatureConstants.Bat].Add(new[] { None });

                testCases[CreatureConstants.Bat_Dire].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Bat_Swarm].Add(helper.BuildData("Swarm", $"1d6", string.Empty, 0, "swarm", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bat_Swarm].Add(helper.BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Bat_Swarm].Add(helper.BuildData("Wounding", "1", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));

                testCases[CreatureConstants.Bear_Black].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bear_Black].Add(helper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Bear_Brown].Add(helper.BuildData("Claw", $"1d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bear_Brown].Add(helper.BuildData("Bite", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Bear_Brown].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Bear_Dire].Add(helper.BuildData("Claw", $"2d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bear_Dire].Add(helper.BuildData("Bite", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Bear_Dire].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Bear_Polar].Add(helper.BuildData("Claw", $"1d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bear_Polar].Add(helper.BuildData("Bite", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Bear_Polar].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(helper.BuildData("Claw", $"2d8", "Infernal Wound", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(helper.BuildData("Infernal Wound", "2", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, string.Empty, AbilityConstants.Constitution));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(helper.BuildData("Beard", "1d8", "Disease", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(helper.BuildData("Battle Frenzy", string.Empty, string.Empty, 0, "extraordinary ability", 2, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(helper.BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(helper.BuildData("Disease", string.Empty, "Devil Chills", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(helper.BuildData("Devil Chills", string.Empty, "incubation period 1d4 days, damage 1d4 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Bebilith].Add(helper.BuildData("Bite", $"2d6", "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bebilith].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Bebilith].Add(helper.BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 4, FeatConstants.Frequencies.Day, false, true, true, true, string.Empty, AbilityConstants.Constitution));
                testCases[CreatureConstants.Bebilith].Add(helper.BuildData("Poison", string.Empty, "Initial damage 1d6 Con, Secondary damage 2d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Bebilith].Add(helper.BuildData("Rend Armor", $"4d6", string.Empty, 2, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Bebilith].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Bee_Giant].Add(helper.BuildData("Sting", $"1d4", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Life, true, true, true, false));
                testCases[CreatureConstants.Bee_Giant].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d4 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Behir].Add(helper.BuildData("Bite", $"2d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Behir].Add(helper.BuildData("Breath Weapon", "7d6 electricity", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
                testCases[CreatureConstants.Behir].Add(helper.BuildData("Constrict", "2d8", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Behir].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Behir].Add(helper.BuildData("Rake", "1d4", string.Empty, 0.5, "extraordinary ability", 6, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Behir].Add(helper.BuildData("Swallow Whole", "2d8 bludgeoning + 8 acid", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Beholder].Add(helper.BuildData("Bite", $"2d4", string.Empty, .5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Beholder].Add(helper.BuildData("Eye ray", string.Empty, SpellConstants.CharmMonster, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(helper.BuildData("Eye ray", string.Empty, SpellConstants.CharmPerson, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(helper.BuildData("Eye ray", string.Empty, SpellConstants.InflictModerateWounds, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(helper.BuildData("Eye ray", string.Empty, SpellConstants.Disintegrate, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(helper.BuildData("Eye ray", string.Empty, SpellConstants.Fear, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(helper.BuildData("Eye ray", string.Empty, SpellConstants.FingerOfDeath, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(helper.BuildData("Eye ray", string.Empty, SpellConstants.FleshToStone, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(helper.BuildData("Eye ray", string.Empty, SpellConstants.Sleep, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(helper.BuildData("Eye ray", string.Empty, SpellConstants.Slow, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(helper.BuildData("Eye ray", string.Empty, SpellConstants.Telekinesis, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));

                testCases[CreatureConstants.Beholder_Gauth].Add(helper.BuildData("Bite", $"1d6", string.Empty, .5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Beholder_Gauth].Add(helper.BuildData("Eye ray", string.Empty, SpellConstants.Sleep, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder_Gauth].Add(helper.BuildData("Eye ray", string.Empty, SpellConstants.InflictModerateWounds, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder_Gauth].Add(helper.BuildData("Eye ray", string.Empty, SpellConstants.DispelMagic, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Beholder_Gauth].Add(helper.BuildData("Eye ray", string.Empty, SpellConstants.ScorchingRay, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Beholder_Gauth].Add(helper.BuildData("Eye ray", string.Empty, "Paralysis", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder_Gauth].Add(helper.BuildData("Eye ray", string.Empty, SpellConstants.RayOfExhaustion, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Belker].Add(helper.BuildData("Wing", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Belker].Add(helper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Belker].Add(helper.BuildData("Claw", $"1d3", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Belker].Add(helper.BuildData("Smoke Claw", $"3d4", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Bison].Add(helper.BuildData("Gore", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bison].Add(helper.BuildData("Stampede", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, SaveConstants.Reflex, AbilityConstants.Strength));

                testCases[CreatureConstants.BlackPudding].Add(helper.BuildData("Slam", $"2d6 + 2d6 acid", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.BlackPudding].Add(helper.BuildData("Acid", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
                testCases[CreatureConstants.BlackPudding].Add(helper.BuildData("Constrict", $"2d6 + 2d6 acid", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.BlackPudding].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.BlackPudding_Elder].Add(helper.BuildData("Slam", $"3d6 + 3d6 acid", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.BlackPudding_Elder].Add(helper.BuildData("Acid", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
                testCases[CreatureConstants.BlackPudding_Elder].Add(helper.BuildData("Constrict", $"2d8 + 2d6 acid", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.BlackPudding_Elder].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.BlinkDog].Add(helper.BuildData("Bite", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.BlinkDog].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Boar].Add(helper.BuildData("Gore", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Boar].Add(helper.BuildData("Ferocity", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Boar_Dire].Add(helper.BuildData("Gore", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Boar_Dire].Add(helper.BuildData("Ferocity", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Bodak].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bodak].Add(helper.BuildData("Death Gaze", string.Empty, "Death", 1.5, "melee", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));

                testCases[CreatureConstants.BombardierBeetle_Giant].Add(helper.BuildData("Bite", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.BombardierBeetle_Giant].Add(helper.BuildData("Acid Spray", $"1d4 acid", string.Empty, 2, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, false, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.BoneDevil_Osyluth].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(helper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(helper.BuildData("Sting", $"3d4", "Poison", 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(helper.BuildData("Fear Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(helper.BuildData("Poison", string.Empty, "Initial damage 1d6 Str, Secondary damage 2d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(helper.BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Bralani].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Bralani].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Bralani].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bralani].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Bralani].Add(helper.BuildData("Whirlwind blast", $"3d6", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Bugbear].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Bugbear].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Bugbear].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Bulette].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bulette].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Bulette].Add(helper.BuildData("Leap", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Camel_Bactrian].Add(helper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Camel_Dromedary].Add(helper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.CarrionCrawler].Add(helper.BuildData("Tentacle", string.Empty, "Paralysis", 0, "melee", 8, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.CarrionCrawler].Add(helper.BuildData("Bite", "1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.CarrionCrawler].Add(helper.BuildData("Paralysis", string.Empty, "paralyzed for 2d4 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Cat].Add(helper.BuildData("Claw", $"1d2", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cat].Add(helper.BuildData("Bite", $"1d3", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Centaur].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Centaur].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Centaur].Add(helper.BuildData("Unarmed Strike", "1d4", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Centaur].Add(helper.BuildData("Hoof", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Centipede_Monstrous_Tiny].Add(helper.BuildData("Bite", $"1d3", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Centipede_Monstrous_Tiny].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Centipede_Monstrous_Small].Add(helper.BuildData("Bite", $"1d4", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Centipede_Monstrous_Small].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d2 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Centipede_Monstrous_Medium].Add(helper.BuildData("Bite", $"1d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Centipede_Monstrous_Medium].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d3 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Centipede_Monstrous_Large].Add(helper.BuildData("Bite", $"1d8", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Centipede_Monstrous_Large].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d4 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Centipede_Monstrous_Huge].Add(helper.BuildData("Bite", $"2d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Centipede_Monstrous_Huge].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Centipede_Monstrous_Gargantuan].Add(helper.BuildData("Bite", $"2d8", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Centipede_Monstrous_Gargantuan].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d8 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Centipede_Monstrous_Colossal].Add(helper.BuildData("Bite", $"4d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Centipede_Monstrous_Colossal].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 2d6 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Centipede_Swarm].Add(helper.BuildData("Swarm", $"2d6", "Poison", 0, "swarm", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Centipede_Swarm].Add(helper.BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Centipede_Swarm].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d4 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.ChainDevil_Kyton].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.ChainDevil_Kyton].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.ChainDevil_Kyton].Add(helper.BuildData("Dancing Chains", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.ChainDevil_Kyton].Add(helper.BuildData("Unnerving Gaze", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));

                testCases[CreatureConstants.ChaosBeast].Add(helper.BuildData("Claw", $"1d3", "Corporeal Instability", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.ChaosBeast].Add(helper.BuildData("Corporeal Instability", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Cheetah].Add(helper.BuildData("Bite", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Cheetah].Add(helper.BuildData("Claw", $"1d2", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cheetah].Add(helper.BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Chimera_Black].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Black].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Black].Add(helper.BuildData("Gore", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Black].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Chimera_Black].Add(helper.BuildData("Breath weapon", $"3d8 acid", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Chimera_Blue].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Blue].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Blue].Add(helper.BuildData("Gore", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Blue].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Chimera_Blue].Add(helper.BuildData("Breath weapon", $"3d8 electricity", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Chimera_Green].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Green].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Green].Add(helper.BuildData("Gore", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Green].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Chimera_Green].Add(helper.BuildData("Breath weapon", $"3d8 acid", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Chimera_Red].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Red].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Red].Add(helper.BuildData("Gore", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Red].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Chimera_Red].Add(helper.BuildData("Breath weapon", $"3d8 fire", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Chimera_White].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_White].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_White].Add(helper.BuildData("Gore", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_White].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Chimera_White].Add(helper.BuildData("Breath weapon", $"3d8 cold", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Choker].Add(helper.BuildData("Tentacle", $"1d3", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Choker].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Choker].Add(helper.BuildData("Constrict", $"1d3", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Chuul].Add(helper.BuildData("Claw", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chuul].Add(helper.BuildData("Constrict", $"3d6", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Chuul].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Chuul].Add(helper.BuildData("Paralytic Tentacles", "1d8", "6 round paralysis", 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Cloaker].Add(helper.BuildData("Tail slap", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cloaker].Add(helper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cloaker].Add(helper.BuildData("Moan", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma));
                testCases[CreatureConstants.Cloaker].Add(helper.BuildData("Engulf", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Cockatrice].Add(helper.BuildData("Bite", $"1d4", "Petrification", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cockatrice].Add(helper.BuildData("Petrification", string.Empty, string.Empty, 1, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Couatl].Add(helper.BuildData("Bite", $"1d3", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Couatl].Add(helper.BuildData("Poison", string.Empty, "Injury, initial damage 2d4 Str, secondary damage 4d4 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, false, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Couatl].Add(helper.BuildData("Constrict", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Couatl].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Couatl].Add(helper.BuildData(FeatConstants.SpecialQualities.Psionic, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Couatl].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Couatl].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Criosphinx].Add(helper.BuildData("Gore", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Criosphinx].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Criosphinx].Add(helper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Criosphinx].Add(helper.BuildData("Rake", $"1d6", string.Empty, 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Crocodile].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Crocodile].Add(helper.BuildData("Tail slap", $"1d12", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Crocodile].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Crocodile_Giant].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Crocodile_Giant].Add(helper.BuildData("Tail slap", $"1d12", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Crocodile_Giant].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Cryohydra_5Heads].Add(helper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 5, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cryohydra_5Heads].Add(helper.BuildData("Breath weapon", $"3d6 cold per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Cryohydra_6Heads].Add(helper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 6, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cryohydra_6Heads].Add(helper.BuildData("Breath weapon", $"3d6 cold per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Cryohydra_7Heads].Add(helper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 7, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cryohydra_7Heads].Add(helper.BuildData("Breath weapon", $"3d6 cold per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Cryohydra_8Heads].Add(helper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 8, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cryohydra_8Heads].Add(helper.BuildData("Breath weapon", $"3d6 cold per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Cryohydra_9Heads].Add(helper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 9, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cryohydra_9Heads].Add(helper.BuildData("Breath weapon", $"3d6 cold per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Cryohydra_10Heads].Add(helper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 10, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cryohydra_10Heads].Add(helper.BuildData("Breath weapon", $"3d6 cold per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Cryohydra_11Heads].Add(helper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 11, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cryohydra_11Heads].Add(helper.BuildData("Breath weapon", $"3d6 cold per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Cryohydra_12Heads].Add(helper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 12, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cryohydra_12Heads].Add(helper.BuildData("Breath weapon", $"3d6 cold per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Darkmantle].Add(helper.BuildData("Slam", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Darkmantle].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Darkmantle].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Darkmantle].Add(helper.BuildData("Constrict", $"1d4", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Deinonychus].Add(helper.BuildData("Talons", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Deinonychus].Add(helper.BuildData("Foreclaw", $"1d3", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Deinonychus].Add(helper.BuildData("Bite", $"2d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Deinonychus].Add(helper.BuildData("Pounce", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Delver].Add(helper.BuildData("Slam", $"1d6 bludgeoning + 2d6 acid", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Delver].Add(helper.BuildData("Corrosive Slime", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Delver].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Derro].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Derro].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Derro].Add(helper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Derro].Add(helper.BuildData("Poison use", string.Empty, "Greenblood Oil or Monstrous Spider Venom", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Derro].Add(helper.BuildData("Greenblood Oil", string.Empty, "Injury DC 13, Initial 1 Con, Secondary 1d2 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Derro].Add(helper.BuildData("Monstrous Spider Venom", string.Empty, "Injury DC 12, Initial and secondary 1d4 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Derro].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Derro].Add(helper.BuildData("Sneak Attack", $"1d6", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Derro_Sane].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Derro_Sane].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Derro_Sane].Add(helper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Derro_Sane].Add(helper.BuildData("Poison use", string.Empty, "Greenblood Oil or Monstrous Spider Venom", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Derro_Sane].Add(helper.BuildData("Greenblood Oil", string.Empty, "Injury DC 13, Initial 1 Con, Secondary 1d2 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Derro_Sane].Add(helper.BuildData("Monstrous Spider Venom", string.Empty, "Injury DC 12, Initial and secondary 1d4 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Derro_Sane].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Derro_Sane].Add(helper.BuildData("Sneak Attack", $"1d6", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Destrachan].Add(helper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Destrachan].Add(helper.BuildData("Destructive harmonics", string.Empty, string.Empty, 1, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Charisma));

                testCases[CreatureConstants.Devourer].Add(helper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Devourer].Add(helper.BuildData("Energy Drain", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Charisma));
                testCases[CreatureConstants.Devourer].Add(helper.BuildData("Trap Essence", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Devourer].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Digester].Add(helper.BuildData("Claw", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Digester].Add(helper.BuildData("Acid Spray", string.Empty, string.Empty, 0, "extraordinary ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.DisplacerBeast].Add(helper.BuildData("Tentacle", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.DisplacerBeast].Add(helper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.DisplacerBeast_PackLord].Add(helper.BuildData("Tentacle", $"1d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.DisplacerBeast_PackLord].Add(helper.BuildData("Bite", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Djinni].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Djinni].Add(helper.BuildData("Air mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Djinni].Add(helper.BuildData("Whirlwind", string.Empty, string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex, saveDcBonus: 3));
                testCases[CreatureConstants.Djinni].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Djinni_Noble].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Djinni_Noble].Add(helper.BuildData("Air mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Djinni_Noble].Add(helper.BuildData("Whirlwind", string.Empty, string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex, saveDcBonus: 3));
                testCases[CreatureConstants.Djinni_Noble].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Dog].Add(helper.BuildData("Bite", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Dog_Riding].Add(helper.BuildData("Bite", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Donkey].Add(helper.BuildData("Bite", $"1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Doppelganger].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Doppelganger].Add(helper.BuildData("Detect Thoughts", string.Empty, string.Empty, 1, "supernatural ability", 0, FeatConstants.Frequencies.Constant, false, true, true, true));
                testCases[CreatureConstants.Doppelganger].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.DragonTurtle].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.DragonTurtle].Add(helper.BuildData("Claw", $"2d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.DragonTurtle].Add(helper.BuildData("Breath Weapon", "12d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
                testCases[CreatureConstants.DragonTurtle].Add(helper.BuildData("Capsize", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //Tiny
                testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(helper.BuildData("Bite", $"1d4", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(helper.BuildData("Claw", $"1d3", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(helper.BuildData("Breath Weapon", $"2d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //small
                testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(helper.BuildData("Bite", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(helper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(helper.BuildData("Breath Weapon", $"4d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //medium
                testCases[CreatureConstants.Dragon_Black_Young].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_Young].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Young].Add(helper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Young].Add(helper.BuildData("Breath Weapon", $"6d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //medium
                testCases[CreatureConstants.Dragon_Black_Juvenile].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_Juvenile].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Juvenile].Add(helper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Juvenile].Add(helper.BuildData("Breath Weapon", $"8d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Juvenile].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(helper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(helper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(helper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(helper.BuildData("Breath Weapon", $"10d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //large
                testCases[CreatureConstants.Dragon_Black_Adult].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(helper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(helper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(helper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(helper.BuildData("Breath Weapon", $"12d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(helper.BuildData("Breath Weapon", $"14d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Black_Old].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_Old].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Old].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Old].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Old].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Old].Add(helper.BuildData("Breath Weapon", $"16d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Old].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_Old].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_Old].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(helper.BuildData("Breath Weapon", $"18d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(helper.BuildData("Breath Weapon", $"20d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(helper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(helper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(helper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(helper.BuildData("Breath Weapon", $"22d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(helper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(helper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(helper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(helper.BuildData("Breath Weapon", $"24d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //small
                testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(helper.BuildData("Bite", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(helper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(helper.BuildData("Breath Weapon", $"2d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //medium
                testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(helper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(helper.BuildData("Breath Weapon", $"4d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //medium
                testCases[CreatureConstants.Dragon_Blue_Young].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_Young].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Young].Add(helper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Young].Add(helper.BuildData("Breath Weapon", $"6d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //large
                testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(helper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(helper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(helper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(helper.BuildData("Breath Weapon", $"8d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(helper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(helper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(helper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(helper.BuildData("Breath Weapon", $"10d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(helper.BuildData("Breath Weapon", $"12d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(helper.BuildData("Breath Weapon", $"14d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Blue_Old].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(helper.BuildData("Breath Weapon", $"16d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(helper.BuildData("Breath Weapon", $"18d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(helper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(helper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(helper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(helper.BuildData("Breath Weapon", $"20d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(helper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(helper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(helper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(helper.BuildData("Breath Weapon", $"22d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(helper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(helper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(helper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(helper.BuildData("Breath Weapon", $"24d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //small
                testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(helper.BuildData("Bite", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(helper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(helper.BuildData("Breath Weapon", $"2d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //medium
                testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(helper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(helper.BuildData("Breath Weapon", $"4d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //medium
                testCases[CreatureConstants.Dragon_Green_Young].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_Young].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Young].Add(helper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Young].Add(helper.BuildData("Breath Weapon", $"6d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //large
                testCases[CreatureConstants.Dragon_Green_Juvenile].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_Juvenile].Add(helper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Juvenile].Add(helper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Juvenile].Add(helper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Juvenile].Add(helper.BuildData("Breath Weapon", $"8d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Juvenile].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(helper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(helper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(helper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(helper.BuildData("Breath Weapon", $"10d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Green_Adult].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(helper.BuildData("Breath Weapon", $"12d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(helper.BuildData("Breath Weapon", $"14d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Green_Old].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_Old].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Old].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Old].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Old].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Old].Add(helper.BuildData("Breath Weapon", $"16d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Old].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_Old].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_Old].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(helper.BuildData("Breath Weapon", $"18d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(helper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(helper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(helper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(helper.BuildData("Breath Weapon", $"20d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(helper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(helper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(helper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(helper.BuildData("Breath Weapon", $"22d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(helper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(helper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(helper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(helper.BuildData("Breath Weapon", $"24d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //medium
                testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(helper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(helper.BuildData("Breath Weapon", $"2d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //large
                testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(helper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(helper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(helper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(helper.BuildData("Breath Weapon", $"4d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //large
                testCases[CreatureConstants.Dragon_Red_Young].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_Young].Add(helper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Young].Add(helper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Young].Add(helper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Young].Add(helper.BuildData("Breath Weapon", $"6d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Young].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(helper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(helper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(helper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(helper.BuildData("Breath Weapon", $"8d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //huge
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(helper.BuildData("Breath Weapon", $"10d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Red_Adult].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(helper.BuildData("Breath Weapon", $"12d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(helper.BuildData("Breath Weapon", $"14d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Red_Old].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_Old].Add(helper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Old].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Old].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Old].Add(helper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Old].Add(helper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Old].Add(helper.BuildData("Breath Weapon", $"16d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Old].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Old].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Old].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(helper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(helper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(helper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(helper.BuildData("Breath Weapon", $"18d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(helper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(helper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(helper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(helper.BuildData("Breath Weapon", $"20d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(helper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(helper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(helper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(helper.BuildData("Breath Weapon", $"22d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //colossal
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(helper.BuildData("Bite", $"4d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(helper.BuildData("Claw", $"4d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(helper.BuildData("Wing", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(helper.BuildData("Tail Slap", $"4d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(helper.BuildData("Crush", $"4d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(helper.BuildData("Tail Sweep", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(helper.BuildData("Breath Weapon", $"24d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //tiny
                testCases[CreatureConstants.Dragon_White_Wyrmling].Add(helper.BuildData("Bite", $"1d4", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_Wyrmling].Add(helper.BuildData("Claw", $"1d3", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Wyrmling].Add(helper.BuildData("Breath Weapon", $"1d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //small
                testCases[CreatureConstants.Dragon_White_VeryYoung].Add(helper.BuildData("Bite", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_VeryYoung].Add(helper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_VeryYoung].Add(helper.BuildData("Breath Weapon", $"2d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //medium
                testCases[CreatureConstants.Dragon_White_Young].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_Young].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Young].Add(helper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Young].Add(helper.BuildData("Breath Weapon", $"3d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //medium
                testCases[CreatureConstants.Dragon_White_Juvenile].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_Juvenile].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Juvenile].Add(helper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Juvenile].Add(helper.BuildData("Breath Weapon", $"4d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Juvenile].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(helper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(helper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(helper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(helper.BuildData("Breath Weapon", $"5d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //large
                testCases[CreatureConstants.Dragon_White_Adult].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_Adult].Add(helper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Adult].Add(helper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Adult].Add(helper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Adult].Add(helper.BuildData("Breath Weapon", $"6d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Adult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_Adult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_Adult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(helper.BuildData("Breath Weapon", $"7d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_White_Old].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_Old].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Old].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Old].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Old].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Old].Add(helper.BuildData("Breath Weapon", $"8d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Old].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_Old].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_Old].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(helper.BuildData("Breath Weapon", $"9d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_White_Ancient].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(helper.BuildData("Breath Weapon", $"10d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(helper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(helper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(helper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(helper.BuildData("Breath Weapon", $"11d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(helper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(helper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(helper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(helper.BuildData("Breath Weapon", $"12d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //tiny
                testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(helper.BuildData("Bite", $"1d4", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(helper.BuildData("Claw", $"1d3", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(helper.BuildData("Breath Weapon (fire)", $"1d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(helper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+1 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //small
                testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(helper.BuildData("Bite", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(helper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(helper.BuildData("Breath Weapon (fire)", $"2d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(helper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+2 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //medium
                testCases[CreatureConstants.Dragon_Brass_Young].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_Young].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Young].Add(helper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Young].Add(helper.BuildData("Breath Weapon (fire)", $"3d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Young].Add(helper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+3 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_Young].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Young].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //medium
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(helper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(helper.BuildData("Breath Weapon (fire)", $"4d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(helper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+4 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(helper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(helper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(helper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(helper.BuildData("Breath Weapon (fire)", $"5d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(helper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+5 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //large
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(helper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(helper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(helper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(helper.BuildData("Breath Weapon (fire)", $"6d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(helper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+6 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(helper.BuildData("Breath Weapon (fire)", $"7d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(helper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+7 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Brass_Old].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(helper.BuildData("Breath Weapon (fire)", $"8d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(helper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+8 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(helper.BuildData("Breath Weapon (fire)", $"9d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(helper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+9 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(helper.BuildData("Breath Weapon (fire)", $"10d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(helper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+10 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(helper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(helper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(helper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(helper.BuildData("Breath Weapon (fire)", $"11d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(helper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+11 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(helper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(helper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(helper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(helper.BuildData("Breath Weapon (fire)", $"12d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(helper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+12 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //small
                testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(helper.BuildData("Bite", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(helper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(helper.BuildData("Breath Weapon (electricity)", $"2d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(helper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+1 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //medium
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(helper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(helper.BuildData("Breath Weapon (electricity)", $"4d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(helper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+2 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //medium
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(helper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(helper.BuildData("Breath Weapon (electricity)", $"6d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(helper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+3 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(helper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(helper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(helper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(helper.BuildData("Breath Weapon (electricity)", $"8d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(helper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+4 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(helper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(helper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(helper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(helper.BuildData("Breath Weapon (electricity)", $"10d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(helper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+5 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(helper.BuildData("Breath Weapon (electricity)", $"12d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(helper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+6 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(helper.BuildData("Breath Weapon (electricity)", $"14d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(helper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+7 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(helper.BuildData("Breath Weapon (electricity)", $"16d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(helper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+8 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(helper.BuildData("Breath Weapon (electricity)", $"18d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(helper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+9 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(helper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(helper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(helper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(helper.BuildData("Breath Weapon (electricity)", $"20d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(helper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+10 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(helper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(helper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(helper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(helper.BuildData("Breath Weapon (electricity)", $"22d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(helper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+11 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(helper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(helper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(helper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(helper.BuildData("Breath Weapon (electricity)", $"24d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(helper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+12 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //tiny
                testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(helper.BuildData("Bite", $"1d4", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(helper.BuildData("Claw", $"1d3", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(helper.BuildData("Breath Weapon (acid)", $"2d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(helper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+1 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                //small
                testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(helper.BuildData("Bite", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(helper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(helper.BuildData("Breath Weapon (acid)", $"4d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(helper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+2 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                //medium
                testCases[CreatureConstants.Dragon_Copper_Young].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_Young].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Young].Add(helper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Young].Add(helper.BuildData("Breath Weapon (acid)", $"6d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Young].Add(helper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+3 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_Young].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //medium
                testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(helper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(helper.BuildData("Breath Weapon (acid)", $"8d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(helper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+4 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(helper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(helper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(helper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(helper.BuildData("Breath Weapon (acid)", $"10d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(helper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+5 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(helper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(helper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(helper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(helper.BuildData("Breath Weapon (acid)", $"12d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(helper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+6 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(helper.BuildData("Breath Weapon (acid)", $"14d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(helper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+7 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Copper_Old].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(helper.BuildData("Breath Weapon (acid)", $"16d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(helper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+8 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(helper.BuildData("Breath Weapon (acid)", $"18d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(helper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+9 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(helper.BuildData("Breath Weapon (acid)", $"20d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(helper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+10 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(helper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(helper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(helper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(helper.BuildData("Breath Weapon (acid)", $"22d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(helper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+11 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(helper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(helper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(helper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(helper.BuildData("Breath Weapon (acid)", $"24d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(helper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+12 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //medium
                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(helper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(helper.BuildData("Breath Weapon (fire)", $"2d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(helper.BuildData("Breath Weapon (weakening gas)", string.Empty, "1 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                //large
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(helper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(helper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(helper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(helper.BuildData("Breath Weapon (fire)", $"4d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(helper.BuildData("Breath Weapon (weakening gas)", string.Empty, "2 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                //large
                testCases[CreatureConstants.Dragon_Gold_Young].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(helper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(helper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(helper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(helper.BuildData("Breath Weapon (fire)", $"6d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(helper.BuildData("Breath Weapon (weakening gas)", string.Empty, "3 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(helper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(helper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(helper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(helper.BuildData("Breath Weapon (fire)", $"8d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(helper.BuildData("Breath Weapon (weakening gas)", string.Empty, "4 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //huge
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(helper.BuildData("Breath Weapon (fire)", $"10d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(helper.BuildData("Breath Weapon (weakening gas)", string.Empty, "5 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(helper.BuildData("Breath Weapon (fire)", $"12d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(helper.BuildData("Breath Weapon (weakening gas)", string.Empty, "6 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(helper.BuildData("Breath Weapon (fire)", $"14d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(helper.BuildData("Breath Weapon (weakening gas)", string.Empty, "7 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Gold_Old].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(helper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(helper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(helper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(helper.BuildData("Breath Weapon (fire)", $"16d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(helper.BuildData("Breath Weapon (weakening gas)", string.Empty, "8 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(helper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(helper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(helper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(helper.BuildData("Breath Weapon (fire)", $"18d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(helper.BuildData("Breath Weapon (weakening gas)", string.Empty, "9 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(helper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(helper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(helper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(helper.BuildData("Breath Weapon (fire)", $"20d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(helper.BuildData("Breath Weapon (weakening gas)", string.Empty, "10 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //colossal
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(helper.BuildData("Bite", $"4d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(helper.BuildData("Claw", $"4d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(helper.BuildData("Wing", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(helper.BuildData("Tail Slap", $"4d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(helper.BuildData("Crush", $"4d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(helper.BuildData("Tail Sweep", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(helper.BuildData("Breath Weapon (fire)", $"22d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(helper.BuildData("Breath Weapon (weakening gas)", string.Empty, "11 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //colossal
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(helper.BuildData("Bite", $"4d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(helper.BuildData("Claw", $"4d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(helper.BuildData("Wing", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(helper.BuildData("Tail Slap", $"4d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(helper.BuildData("Crush", $"4d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(helper.BuildData("Tail Sweep", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(helper.BuildData("Breath Weapon (fire)", $"24d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(helper.BuildData("Breath Weapon (weakening gas)", string.Empty, "12 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //small
                testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(helper.BuildData("Bite", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(helper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(helper.BuildData("Breath Weapon (cold)", $"2d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(helper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+1 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                //medium
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(helper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(helper.BuildData("Breath Weapon (cold)", $"4d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(helper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+2 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                //medium
                testCases[CreatureConstants.Dragon_Silver_Young].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(helper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(helper.BuildData("Breath Weapon (cold)", $"6d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(helper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+3 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(helper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(helper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(helper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(helper.BuildData("Breath Weapon (cold)", $"8d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(helper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+4 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(helper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(helper.BuildData("Wing", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(helper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(helper.BuildData("Breath Weapon (cold)", $"10d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(helper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+5 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(helper.BuildData("Breath Weapon (cold)", $"12d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(helper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+6 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(helper.BuildData("Breath Weapon (cold)", $"14d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(helper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+7 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Silver_Old].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(helper.BuildData("Breath Weapon (cold)", $"16d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(helper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+8 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(helper.BuildData("Claw", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(helper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(helper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(helper.BuildData("Breath Weapon (cold)", $"18d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(helper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+9 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(helper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(helper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(helper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(helper.BuildData("Breath Weapon (cold)", $"20d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(helper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+10 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(helper.BuildData("Claw", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(helper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(helper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(helper.BuildData("Breath Weapon (cold)", $"22d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(helper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+11 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //colossal
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(helper.BuildData("Bite", $"4d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(helper.BuildData("Claw", $"4d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(helper.BuildData("Wing", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(helper.BuildData("Tail Slap", $"4d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(helper.BuildData("Crush", $"4d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(helper.BuildData("Tail Sweep", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(helper.BuildData("Breath Weapon (cold)", $"24d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(helper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+12 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(helper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.Dragonne].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragonne].Add(helper.BuildData("Claw", $"2d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragonne].Add(helper.BuildData("Pounce", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Dragonne].Add(helper.BuildData("Roar", string.Empty, string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));

                testCases[CreatureConstants.Dretch].Add(helper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dretch].Add(helper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dretch].Add(helper.BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.Dretch].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Drider].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Drider].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Drider].Add(helper.BuildData("Bite", $"1d4", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Drider].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Drider].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Drider].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Dryad].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Dryad].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Dryad].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dryad].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Dwarf_Deep].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Dwarf_Deep].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Dwarf_Deep].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Dwarf_Duergar].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Dwarf_Duergar].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Dwarf_Duergar].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dwarf_Duergar].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Dwarf_Hill].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Dwarf_Hill].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Dwarf_Hill].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Dwarf_Mountain].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Dwarf_Mountain].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Dwarf_Mountain].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Eagle].Add(helper.BuildData("Talons", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Eagle].Add(helper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Eagle_Giant].Add(helper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Eagle_Giant].Add(helper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Efreeti].Add(helper.BuildData("Slam", $"1d8", "Heat", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Efreeti].Add(helper.BuildData("Change Size", string.Empty, string.Empty, 0, "spell-like ability", 2, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Efreeti].Add(helper.BuildData("Heat", "1d6 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, false, true, true, true));
                testCases[CreatureConstants.Efreeti].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Elasmosaurus].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Elemental_Air_Small].Add(helper.BuildData("Slam", $"1d4", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Air_Small].Add(helper.BuildData("Air mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Air_Small].Add(helper.BuildData("Whirlwind", "1d4", string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Air_Medium].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Air_Medium].Add(helper.BuildData("Air mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Air_Medium].Add(helper.BuildData("Whirlwind", "1d6", string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Air_Large].Add(helper.BuildData("Slam", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Air_Large].Add(helper.BuildData("Air mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Air_Large].Add(helper.BuildData("Whirlwind", "2d6", string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Air_Huge].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Air_Huge].Add(helper.BuildData("Air mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Air_Huge].Add(helper.BuildData("Whirlwind", "2d8", string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Air_Greater].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Air_Greater].Add(helper.BuildData("Air mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Air_Greater].Add(helper.BuildData("Whirlwind", "2d8", string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Air_Elder].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Air_Elder].Add(helper.BuildData("Air mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Air_Elder].Add(helper.BuildData("Whirlwind", "2d8", string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Earth_Small].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Earth_Small].Add(helper.BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Earth_Small].Add(helper.BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Elemental_Earth_Medium].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Earth_Medium].Add(helper.BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Earth_Medium].Add(helper.BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Elemental_Earth_Large].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Earth_Large].Add(helper.BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Earth_Large].Add(helper.BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Elemental_Earth_Huge].Add(helper.BuildData("Slam", $"2d10", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Earth_Huge].Add(helper.BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Earth_Huge].Add(helper.BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Elemental_Earth_Greater].Add(helper.BuildData("Slam", $"2d10", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Earth_Greater].Add(helper.BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Earth_Greater].Add(helper.BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Elemental_Earth_Elder].Add(helper.BuildData("Slam", $"2d10", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Earth_Elder].Add(helper.BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Earth_Elder].Add(helper.BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Elemental_Fire_Small].Add(helper.BuildData("Slam", $"1d4", "Burn", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Fire_Small].Add(helper.BuildData("Burn", "1d4 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Fire_Medium].Add(helper.BuildData("Slam", $"1d6", "Burn", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Fire_Medium].Add(helper.BuildData("Burn", "1d6 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Fire_Large].Add(helper.BuildData("Slam", $"2d6", "Burn", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Fire_Large].Add(helper.BuildData("Burn", "2d6 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Fire_Huge].Add(helper.BuildData("Slam", $"2d8", "Burn", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Fire_Huge].Add(helper.BuildData("Burn", "2d8 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Fire_Greater].Add(helper.BuildData("Slam", $"2d8", "Burn", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Fire_Greater].Add(helper.BuildData("Burn", "2d8 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Fire_Elder].Add(helper.BuildData("Slam", $"2d8", "Burn", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Fire_Elder].Add(helper.BuildData("Burn", "2d8 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Water_Small].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Water_Small].Add(helper.BuildData("Water mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Small].Add(helper.BuildData("Drench", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Small].Add(helper.BuildData("Vortex", "1d4", string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Water_Medium].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Water_Medium].Add(helper.BuildData("Water mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Medium].Add(helper.BuildData("Drench", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Medium].Add(helper.BuildData("Vortex", "1d6", string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Water_Large].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Water_Large].Add(helper.BuildData("Water mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Large].Add(helper.BuildData("Drench", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Large].Add(helper.BuildData("Vortex", "2d6", string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Water_Huge].Add(helper.BuildData("Slam", $"2d10", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Water_Huge].Add(helper.BuildData("Water mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Huge].Add(helper.BuildData("Drench", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Huge].Add(helper.BuildData("Vortex", "2d8", string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Water_Greater].Add(helper.BuildData("Slam", $"2d10", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Water_Greater].Add(helper.BuildData("Water mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Greater].Add(helper.BuildData("Drench", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Greater].Add(helper.BuildData("Vortex", "2d8", string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Water_Elder].Add(helper.BuildData("Slam", $"2d10", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Water_Elder].Add(helper.BuildData("Water mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Elder].Add(helper.BuildData("Drench", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Elder].Add(helper.BuildData("Vortex", "2d8", string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elephant].Add(helper.BuildData("Slam", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elephant].Add(helper.BuildData("Stamp", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Elephant].Add(helper.BuildData("Gore", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elephant].Add(helper.BuildData("Trample", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elf_Aquatic].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Elf_Aquatic].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Elf_Aquatic].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Elf_Drow].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Elf_Drow].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Elf_Drow].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elf_Drow].Add(helper.BuildData("Poison", string.Empty, "DC 13 Fort, Initial 1 minute unconscious, Secondary 2d4 hours unconscious", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
                testCases[CreatureConstants.Elf_Drow].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Elf_Gray].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Elf_Gray].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Elf_Gray].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Elf_Half].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Elf_Half].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Elf_Half].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Elf_High].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Elf_High].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Elf_High].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Elf_Wild].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Elf_Wild].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Elf_Wild].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Elf_Wood].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Elf_Wood].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Elf_Wood].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Erinyes].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Erinyes].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Erinyes].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Erinyes].Add(helper.BuildData("Rope", string.Empty, "Entangle", 0, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Erinyes].Add(helper.BuildData("Entangle", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Erinyes].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Erinyes].Add(helper.BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.EtherealFilcher].Add(helper.BuildData("Bite", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.EtherealFilcher].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.EtherealMarauder].Add(helper.BuildData("Bite", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.EtherealMarauder].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Ettercap].Add(helper.BuildData("Bite", $"1d8", "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ettercap].Add(helper.BuildData("Claw", $"1d3", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Ettercap].Add(helper.BuildData("Poison", string.Empty, "Initial damage 1d6 Dex, secondary damage 2d6 Dex", 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 2));
                testCases[CreatureConstants.Ettercap].Add(helper.BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, true, true, false, true, saveAbility: AbilityConstants.Constitution));

                testCases[CreatureConstants.Ettin].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 2, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Ettin].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 2, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Ettin].Add(helper.BuildData("Unarmed Strike", "1d4", string.Empty, 1.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.FireBeetle_Giant].Add(helper.BuildData("Bite", $"2d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.FormianWorker].Add(helper.BuildData("Bite", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.FormianWorker].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.FormianWarrior].Add(helper.BuildData("Sting", $"2d4", "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.FormianWarrior].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.FormianWarrior].Add(helper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.FormianWarrior].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.FormianTaskmaster].Add(helper.BuildData("Sting", $"2d4", "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.FormianTaskmaster].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.FormianTaskmaster].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.FormianTaskmaster].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.FormianTaskmaster].Add(helper.BuildData("Dominated creature", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.FormianMyrmarch].Add(helper.BuildData("Sting", $"2d4", "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.FormianMyrmarch].Add(helper.BuildData("Bite", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.FormianMyrmarch].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.FormianMyrmarch].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.FormianMyrmarch].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.FormianQueen].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.FormianQueen].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.FrostWorm].Add(helper.BuildData("Bite", $"2d8", "Cold", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.FrostWorm].Add(helper.BuildData("Trill", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.FrostWorm].Add(helper.BuildData("Cold", "1d8 Cold", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.FrostWorm].Add(helper.BuildData("Breath weapon", "15d6 cold", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Gargoyle].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Gargoyle].Add(helper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Gargoyle].Add(helper.BuildData("Gore", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Gargoyle_Kapoacinth].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Gargoyle_Kapoacinth].Add(helper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Gargoyle_Kapoacinth].Add(helper.BuildData("Gore", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.GelatinousCube].Add(helper.BuildData("Slam", $"1d6 + 1d6 acid", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.GelatinousCube].Add(helper.BuildData("Acid", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.GelatinousCube].Add(helper.BuildData("Engulf", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, SaveConstants.Reflex, AbilityConstants.Strength, 1));
                testCases[CreatureConstants.GelatinousCube].Add(helper.BuildData("Paralysis", string.Empty, "3d6 rounds of paralysis", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Ghaele].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Ghaele].Add(helper.BuildData("Light Ray", "2d12", string.Empty, 0, "ranged touch", 2, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Ghaele].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Ghaele].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Ghaele].Add(helper.BuildData("Gaze", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.Ghoul].Add(helper.BuildData("Bite", $"1d6", "Disease, Paralysis", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ghoul].Add(helper.BuildData("Claw", $"1d3", "Paralysis", 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Ghoul].Add(helper.BuildData("Disease", string.Empty, "Ghoul Fever", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Ghoul].Add(helper.BuildData("Ghoul Fever", string.Empty, "incubation period 1 day, damage 1d3 Con and 1d3 Dex", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Ghoul].Add(helper.BuildData("Paralysis", string.Empty, "1d4+1 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Ghoul_Ghast].Add(helper.BuildData("Bite", $"1d6", "Disease, Paralysis", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ghoul_Ghast].Add(helper.BuildData("Claw", $"1d3", "Paralysis", 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Ghoul_Ghast].Add(helper.BuildData("Disease", string.Empty, "Ghoul Fever", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Ghoul_Ghast].Add(helper.BuildData("Ghoul Fever", string.Empty, "incubation period 1 day, damage 1d3 Con and 1d3 Dex", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Ghoul_Ghast].Add(helper.BuildData("Paralysis", string.Empty, "1d4+1 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Ghoul_Ghast].Add(helper.BuildData("Stench", string.Empty, "1d6+4 rounds sickened", 0, "melee", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Ghoul_Lacedon].Add(helper.BuildData("Bite", $"1d6", "Disease, Paralysis", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ghoul_Lacedon].Add(helper.BuildData("Claw", $"1d3", "Paralysis", 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Ghoul_Lacedon].Add(helper.BuildData("Disease", string.Empty, "Ghoul Fever", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Ghoul_Lacedon].Add(helper.BuildData("Ghoul Fever", string.Empty, "incubation period 1 day, damage 1d3 Con and 1d3 Dex", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Ghoul_Lacedon].Add(helper.BuildData("Paralysis", string.Empty, "1d4+1 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Giant_Cloud].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Giant_Cloud].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Giant_Cloud].Add(helper.BuildData("Rock", $"2d8", string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Giant_Cloud].Add(helper.BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Giant_Cloud].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Giant_Fire].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Giant_Fire].Add(helper.BuildData("Slam", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Giant_Fire].Add(helper.BuildData("Rock", $"2d6", string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Giant_Fire].Add(helper.BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Giant_Frost].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Giant_Frost].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Giant_Frost].Add(helper.BuildData("Rock", $"2d6", string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Giant_Frost].Add(helper.BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Giant_Hill].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Giant_Hill].Add(helper.BuildData("Slam", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Giant_Hill].Add(helper.BuildData("Rock", $"2d6", string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Giant_Hill].Add(helper.BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Giant_Stone].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Giant_Stone].Add(helper.BuildData("Slam", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Giant_Stone].Add(helper.BuildData("Rock", $"2d8", string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Giant_Stone].Add(helper.BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Giant_Stone_Elder].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Giant_Stone_Elder].Add(helper.BuildData("Slam", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Giant_Stone_Elder].Add(helper.BuildData("Rock", $"2d8", string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Giant_Stone_Elder].Add(helper.BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Giant_Stone_Elder].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Giant_Storm].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Giant_Storm].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Giant_Storm].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Giant_Storm].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.GibberingMouther].Add(helper.BuildData("Bite", $"1", string.Empty, 1, "melee", 6, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.GibberingMouther].Add(helper.BuildData("Spittle", $"1d4 acid", "Blindness", 0, "ranged touch", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.GibberingMouther].Add(helper.BuildData("Blindness", string.Empty, "1d4 rounds blinded", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.GibberingMouther].Add(helper.BuildData("Gibbering", string.Empty, "1d2 rounds Confusion", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.GibberingMouther].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.GibberingMouther].Add(helper.BuildData("Swallow Whole", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.GibberingMouther].Add(helper.BuildData("Blood Drain", string.Empty, "1d4 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.GibberingMouther].Add(helper.BuildData("Ground Manipulation", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Girallon].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Girallon].Add(helper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Girallon].Add(helper.BuildData("Rend", $"2d4", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Githyanki].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Githyanki].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Githyanki].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Githyanki].Add(helper.BuildData(FeatConstants.SpecialQualities.Psionic, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Githzerai].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Githzerai].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Githzerai].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Githzerai].Add(helper.BuildData(FeatConstants.SpecialQualities.Psionic, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Glabrezu].Add(helper.BuildData("Pincer", $"2d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Glabrezu].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Glabrezu].Add(helper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Glabrezu].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Glabrezu].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Glabrezu].Add(helper.BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Gnoll].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Gnoll].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Gnoll].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Gnome_Forest].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Gnome_Forest].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Gnome_Forest].Add(helper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Gnome_Forest].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Gnome_Rock].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Gnome_Rock].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Gnome_Rock].Add(helper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Gnome_Rock].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Gnome_Svirfneblin].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Gnome_Svirfneblin].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Gnome_Svirfneblin].Add(helper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Gnome_Svirfneblin].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Goblin].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Goblin].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Goblin].Add(helper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Golem_Clay].Add(helper.BuildData("Slam", $"2d10", "Cursed Wound", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Golem_Clay].Add(helper.BuildData("Berserk", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Golem_Clay].Add(helper.BuildData("Cursed Wound", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
                testCases[CreatureConstants.Golem_Clay].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Golem_Flesh].Add(helper.BuildData("Slam", $"2d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Golem_Flesh].Add(helper.BuildData("Berserk", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Golem_Iron].Add(helper.BuildData("Slam", $"2d10", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Golem_Iron].Add(helper.BuildData("Breath weapon", string.Empty, "Poisonous Gas", 0, "supernatural ability", 1, $"1d4+1 {FeatConstants.Frequencies.Round}", false, true, true, true));
                testCases[CreatureConstants.Golem_Iron].Add(helper.BuildData("Poisonous Gas", string.Empty, "Initial damage 1d4 Con, secondary damage 3d4 Con", 0, "melee", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Golem_Stone].Add(helper.BuildData("Slam", $"2d10", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Golem_Stone].Add(helper.BuildData("Slow", string.Empty, string.Empty, 0, "supernatural ability", 1, $"2 {FeatConstants.Frequencies.Round}", true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));

                testCases[CreatureConstants.Golem_Stone_Greater].Add(helper.BuildData("Slam", $"4d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Golem_Stone_Greater].Add(helper.BuildData("Slow", string.Empty, string.Empty, 0, "supernatural ability", 1, $"2 {FeatConstants.Frequencies.Round}", true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));

                testCases[CreatureConstants.Gorgon].Add(helper.BuildData("Gore", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Gorgon].Add(helper.BuildData("Breath weapon", string.Empty, "Turn to stone", 0, "supernatural ability", 5, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Gorgon].Add(helper.BuildData("Trample", "1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.GrayOoze].Add(helper.BuildData("Slam", $"1d6 + 1d6 acid", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.GrayOoze].Add(helper.BuildData("Acid", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
                testCases[CreatureConstants.GrayOoze].Add(helper.BuildData("Constrict", $"1d6 + 1d6 acid", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.GrayOoze].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.GrayRender].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.GrayRender].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.GrayRender].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.GrayRender].Add(helper.BuildData("Rend", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.GreenHag].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.GreenHag].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.GreenHag].Add(helper.BuildData("Weakness", string.Empty, "2d4 Strength damage", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.GreenHag].Add(helper.BuildData("Mimicry", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Grick].Add(helper.BuildData("Tentacle", $"1d4", string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Grick].Add(helper.BuildData("Bite", $"1d3", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Griffon].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Griffon].Add(helper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Griffon].Add(helper.BuildData("Pounce", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Griffon].Add(helper.BuildData("Rake", $"1d6", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Grig].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Grig].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Grig].Add(helper.BuildData("Unarmed Strike", "1", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Grig].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Grig_WithFiddle].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Grig_WithFiddle].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Grig_WithFiddle].Add(helper.BuildData("Unarmed Strike", "1", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Grig_WithFiddle].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Grig_WithFiddle].Add(helper.BuildData("Fiddle", string.Empty, SpellConstants.IrresistibleDance, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.Grimlock].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Grimlock].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Gynosphinx].Add(helper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Gynosphinx].Add(helper.BuildData("Pounce", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Gynosphinx].Add(helper.BuildData("Rake", $"1d6", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Gynosphinx].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Halfling_Deep].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Halfling_Deep].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Halfling_Deep].Add(helper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Halfling_Lightfoot].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Halfling_Lightfoot].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Halfling_Lightfoot].Add(helper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Halfling_Tallfellow].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Halfling_Tallfellow].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Halfling_Tallfellow].Add(helper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Harpy].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Harpy].Add(helper.BuildData("Claw", $"1d3", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Harpy].Add(helper.BuildData("Captivating Song", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.Hawk].Add(helper.BuildData("Talons", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.HellHound].Add(helper.BuildData("Bite", $"1d8", "Fiery Bite", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.HellHound].Add(helper.BuildData("Fiery Bite", $"1d6 fire", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.HellHound].Add(helper.BuildData("Breath weapon", "2d6 fire", string.Empty, 0, "supernatural ability", 1, $"2d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.HellHound_NessianWarhound].Add(helper.BuildData("Bite", $"2d6", "Fiery Bite", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.HellHound_NessianWarhound].Add(helper.BuildData("Fiery Bite", $"1d8 fire", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.HellHound_NessianWarhound].Add(helper.BuildData("Breath weapon", "3d6 fire", string.Empty, 0, "supernatural ability", 1, $"2d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Hellcat_Bezekira].Add(helper.BuildData("Claw", $"1d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(helper.BuildData("Bite", $"2d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(helper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(helper.BuildData("Rake", $"1d8", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Hellwasp_Swarm].Add(helper.BuildData("Swarm", $"3d6", "poison", 0, "swarm", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Hellwasp_Swarm].Add(helper.BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Hellwasp_Swarm].Add(helper.BuildData("Inhabit", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, true, true, true, true));
                testCases[CreatureConstants.Hellwasp_Swarm].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d4 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));

                testCases[CreatureConstants.Hezrou].Add(helper.BuildData("Bite", $"4d4", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Hezrou].Add(helper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Hezrou].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Hezrou].Add(helper.BuildData("Stench", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Hezrou].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Hezrou].Add(helper.BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Hieracosphinx].Add(helper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Hieracosphinx].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Hieracosphinx].Add(helper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Hieracosphinx].Add(helper.BuildData("Rake", $"1d6", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Hippogriff].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Hippogriff].Add(helper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hobgoblin].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Hobgoblin].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Hobgoblin].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Homunculus].Add(helper.BuildData("Bite", $"1d4", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Homunculus].Add(helper.BuildData("Poison", string.Empty, "Initial damage sleep for 1 minute, secondary damage sleep for another 5d6 minutes", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 2));

                testCases[CreatureConstants.HornedDevil_Cornugon].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(helper.BuildData("Claw", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(helper.BuildData("Bite", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(helper.BuildData("Tail", $"2d6", "infernal wound", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(helper.BuildData("Fear Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(helper.BuildData("Infernal Wound", "2", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, string.Empty, AbilityConstants.Constitution));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(helper.BuildData("Stun", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Strength));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(helper.BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Horse_Heavy].Add(helper.BuildData("Hoof", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Horse_Heavy_War].Add(helper.BuildData("Hoof", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Horse_Heavy_War].Add(helper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Horse_Light].Add(helper.BuildData("Hoof", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Horse_Light_War].Add(helper.BuildData("Hoof", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Horse_Light_War].Add(helper.BuildData("Bite", $"1d3", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.HoundArchon].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.HoundArchon].Add(helper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.HoundArchon].Add(helper.BuildData("Slam", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.HoundArchon].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Howler].Add(helper.BuildData("Bite", $"2d8", "1d4 Quills", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Howler].Add(helper.BuildData("Quill", "1d6", string.Empty, 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Dexterity, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Howler].Add(helper.BuildData("Howl", string.Empty, "1 Wis", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hour, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.Human].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Human].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Human].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hydra_5Heads].Add(helper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 5, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hydra_6Heads].Add(helper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 6, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hydra_7Heads].Add(helper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 7, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hydra_8Heads].Add(helper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 8, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hydra_9Heads].Add(helper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 9, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hydra_10Heads].Add(helper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 10, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hydra_11Heads].Add(helper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 11, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hydra_12Heads].Add(helper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 12, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hyena].Add(helper.BuildData("Bite", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Hyena].Add(helper.BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.IceDevil_Gelugon].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(helper.BuildData("Claw", $"1d10", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(helper.BuildData("Bite", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(helper.BuildData("Tail", $"3d6", "slow", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(helper.BuildData("Fear Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(helper.BuildData("Slow", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(helper.BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Imp].Add(helper.BuildData("Sting", $"1d4", "poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Imp].Add(helper.BuildData("Poison", string.Empty, $"Initial damage 1d4 Dex, Secondary damage 2d4 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution, 2));
                testCases[CreatureConstants.Imp].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.InvisibleStalker].Add(helper.BuildData("Slam", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Janni].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Janni].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Janni].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Janni].Add(helper.BuildData("Change Size", string.Empty, string.Empty, 0, "spell-like ability", 2, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Janni].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Kobold].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Kobold].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Kobold].Add(helper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Kolyarut].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Kolyarut].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Kolyarut].Add(helper.BuildData("Vampiric Touch", $"5d6", string.Empty, 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Kolyarut].Add(helper.BuildData("Enervation Ray", string.Empty, "1d4 negative levels", 0, "ranged touch", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Kolyarut].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Kraken].Add(helper.BuildData("Tentacle", $"2d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Kraken].Add(helper.BuildData("Arm", $"1d6", string.Empty, 0.5, "melee", 6, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Kraken].Add(helper.BuildData("Bite", $"4d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Kraken].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Kraken].Add(helper.BuildData("Constrict (Tentacle)", $"2d8", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Kraken].Add(helper.BuildData("Constrict (Arm)", $"1d6", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Kraken].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Krenshar].Add(helper.BuildData("Bite", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Krenshar].Add(helper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Krenshar].Add(helper.BuildData("Scare", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Krenshar].Add(helper.BuildData("Scare with Screech", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.KuoToa].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.KuoToa].Add(helper.BuildData("Bite", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.KuoToa].Add(helper.BuildData("Lightning Bolt", "1d6 per whip", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", true, true, true, false, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Lamia].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Lamia].Add(helper.BuildData("Touch", string.Empty, "Wisdom Drain", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Lamia].Add(helper.BuildData("Wisdom Drain", string.Empty, "1d4 Wisdom Drain", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Lamia].Add(helper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Lamia].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Lammasu].Add(helper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Lammasu].Add(helper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Lammasu].Add(helper.BuildData("Rake", $"1d6", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Lammasu].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Lammasu].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.LanternArchon].Add(helper.BuildData("Light Ray", $"1d6", string.Empty, 0, "ranged touch", 2, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.LanternArchon].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Lemure].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Leonal].Add(helper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Leonal].Add(helper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Leonal].Add(helper.BuildData("Roar", "2d6 sonic", string.Empty, 0, "supernatural ability", 3, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.Leonal].Add(helper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Leonal].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Leonal].Add(helper.BuildData("Rake", $"1d6", string.Empty, 1, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Leonal].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Leopard].Add(helper.BuildData("Bite", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Leopard].Add(helper.BuildData("Claw", $"1d3", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Leopard].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Leopard].Add(helper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Leopard].Add(helper.BuildData("Rake", $"1d3", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Lillend].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Lillend].Add(helper.BuildData("Unarmed Strike", "1d4", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Lillend].Add(helper.BuildData("Tail slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Lillend].Add(helper.BuildData("Constrict", $"2d6", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Lillend].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Lillend].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Lillend].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Lion].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Lion].Add(helper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Lion].Add(helper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Lion].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Lion].Add(helper.BuildData("Rake", $"1d4", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Lion_Dire].Add(helper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Lion_Dire].Add(helper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Lion_Dire].Add(helper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Lion_Dire].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Lion_Dire].Add(helper.BuildData("Rake", $"1d6", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Lizard].Add(helper.BuildData("Bite", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Lizard_Monitor].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Lizardfolk].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Lizardfolk].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Lizardfolk].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Lizardfolk].Add(helper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Locathah].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Locathah].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Locathah].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Locust_Swarm].Add(helper.BuildData("Swarm", $"2d6", string.Empty, 0, "swarm", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Locust_Swarm].Add(helper.BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Magmin].Add(helper.BuildData("Burning Touch", $"1d8 fire", "Combustion", 0, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Magmin].Add(helper.BuildData("Slam", $"1d3", "Combustion", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Magmin].Add(helper.BuildData("Combustion", $"1d8 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Magmin].Add(helper.BuildData("Fiery Aura", $"1d6 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.MantaRay].Add(helper.BuildData("Ram", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Manticore].Add(helper.BuildData("Claw", $"2d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Manticore].Add(helper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Manticore].Add(helper.BuildData("Spikes", string.Empty, "Tail Spikes", 0, "ranged", 6, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Manticore].Add(helper.BuildData("Tail Spikes", $"1d8", string.Empty, 0.5, "extraordinary ability", 24, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Marilith].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Marilith].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 5, FeatConstants.Frequencies.Round, true, false, false, false));
                testCases[CreatureConstants.Marilith].Add(helper.BuildData("Tail Slap", $"4d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Marilith].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1, "melee", 6, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Marilith].Add(helper.BuildData("Constrict", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Strength));
                testCases[CreatureConstants.Marilith].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Marilith].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Marilith].Add(helper.BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Marut].Add(helper.BuildData("Slam", $"2d6", "Fist of Thunder", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Marut].Add(helper.BuildData("Slam", $"2d6", "Fist of Lightning", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Marut].Add(helper.BuildData("Fist of Thunder", $"3d6 sonic", "deafened 2d6 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Marut].Add(helper.BuildData("Fist of Lightning", $"3d6 electricity", "blinded 2d6 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Marut].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Medusa].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Medusa].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Medusa].Add(helper.BuildData("Snakes", $"1d4", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Medusa].Add(helper.BuildData("Petrifying Gaze", string.Empty, "Permanent petrification", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.Medusa].Add(helper.BuildData("Poison", string.Empty, "Initial damage 1d6 Str, Secondary damage 2d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Megaraptor].Add(helper.BuildData("Talons", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Megaraptor].Add(helper.BuildData("Foreclaw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Megaraptor].Add(helper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Megaraptor].Add(helper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Mephit_Air].Add(helper.BuildData("Claw", $"1d3", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Air].Add(helper.BuildData("Breath weapon", $"1d8", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Air].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Air].Add(helper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Dust].Add(helper.BuildData("Claw", $"1d3", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Dust].Add(helper.BuildData("Breath weapon", $"1d4", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Dust].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Dust].Add(helper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Earth].Add(helper.BuildData("Claw", $"1d3", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Earth].Add(helper.BuildData("Breath weapon", $"1d8", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Earth].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Earth].Add(helper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Fire].Add(helper.BuildData("Claw", $"1d3 + 1d4 fire", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Fire].Add(helper.BuildData("Breath weapon", $"1d8 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Fire].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Fire].Add(helper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Ice].Add(helper.BuildData("Claw", $"1d3 + 1d4 cold", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Ice].Add(helper.BuildData("Breath weapon", $"1d4 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Ice].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Ice].Add(helper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Magma].Add(helper.BuildData("Claw", $"1d3 + 1d4 fire", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Magma].Add(helper.BuildData("Breath weapon", $"1d4 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Magma].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Magma].Add(helper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Ooze].Add(helper.BuildData("Claw", $"1d3", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Ooze].Add(helper.BuildData("Breath weapon", $"1d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Ooze].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Ooze].Add(helper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Salt].Add(helper.BuildData("Claw", $"1d3", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Salt].Add(helper.BuildData("Breath weapon", $"1d4", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Salt].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Salt].Add(helper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Steam].Add(helper.BuildData("Claw", $"1d3 + 1d4 fire", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Steam].Add(helper.BuildData("Breath weapon", $"1d4 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Steam].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Steam].Add(helper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Water].Add(helper.BuildData("Claw", $"1d3", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Water].Add(helper.BuildData("Breath weapon", $"1d8 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Water].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Water].Add(helper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Merfolk].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Merfolk].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Merfolk].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Mimic].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mimic].Add(helper.BuildData("Adhesive", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Mimic].Add(helper.BuildData("Crush", $"1d8", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.MindFlayer].Add(helper.BuildData("Tentacle", $"1d4", string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.MindFlayer].Add(helper.BuildData("Mind Blast", string.Empty, "3d4 rounds stunned", 1, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.MindFlayer].Add(helper.BuildData(FeatConstants.SpecialQualities.Psionic, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.MindFlayer].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.MindFlayer].Add(helper.BuildData("Extract", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Minotaur].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Minotaur].Add(helper.BuildData("Gore", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Minotaur].Add(helper.BuildData("Powerful Charge", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Mohrg].Add(helper.BuildData("Slam", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mohrg].Add(helper.BuildData("Tongue", string.Empty, "Paralyzing Touch", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mohrg].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Mohrg].Add(helper.BuildData("Paralyzing Touch", string.Empty, "1d4 minutes paralyzed", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Mohrg].Add(helper.BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));

                testCases[CreatureConstants.Monkey].Add(helper.BuildData("Bite", $"1d3", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Mule].Add(helper.BuildData("Hoof", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Mummy].Add(helper.BuildData("Slam", $"1d6", "Disease", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mummy].Add(helper.BuildData("Despair", string.Empty, "1d4 rounds fear", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.Mummy].Add(helper.BuildData("Disease", string.Empty, "Mummy Rot", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Mummy].Add(helper.BuildData("Mummy Rot", string.Empty, "incubation period 1 minute; damage 1d6 Con and 1d6 Cha", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Naga_Dark].Add(helper.BuildData("Sting", $"2d4", "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Naga_Dark].Add(helper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Naga_Dark].Add(helper.BuildData("Poison", string.Empty, "lapse into a nightmare-haunted sleep for 2d4 minutes", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Naga_Dark].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Naga_Guardian].Add(helper.BuildData("Bite", $"2d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Naga_Guardian].Add(helper.BuildData("Spit", string.Empty, "Poison", 0, "ranged touch", 1, FeatConstants.Frequencies.Round, false, true, false, false));
                testCases[CreatureConstants.Naga_Guardian].Add(helper.BuildData("Poison", string.Empty, "initial and secondary damage 1d10 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Naga_Guardian].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Naga_Spirit].Add(helper.BuildData("Bite", $"2d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Naga_Spirit].Add(helper.BuildData("Charming Gaze", string.Empty, SpellConstants.CharmPerson, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.Naga_Spirit].Add(helper.BuildData("Poison", string.Empty, "initial and secondary damage 1d8 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Naga_Spirit].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Naga_Water].Add(helper.BuildData("Bite", $"2d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Naga_Water].Add(helper.BuildData("Poison", string.Empty, "initial and secondary damage 1d8 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Naga_Water].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Nalfeshnee].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Nalfeshnee].Add(helper.BuildData("Claw", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Nalfeshnee].Add(helper.BuildData("Smite", string.Empty, string.Empty, 1, "supernatural ability", 3, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Nalfeshnee].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Nalfeshnee].Add(helper.BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.NightHag].Add(helper.BuildData("Bite", $"2d6", "Disease", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.NightHag].Add(helper.BuildData("Disease", string.Empty, "incubation period 1 day, damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.NightHag].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.NightHag].Add(helper.BuildData("Dream Haunting", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Nightcrawler].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Nightcrawler].Add(helper.BuildData("Sting", $"2d8", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Nightcrawler].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Nightcrawler].Add(helper.BuildData("Desecrating Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));
                testCases[CreatureConstants.Nightcrawler].Add(helper.BuildData("Energy Drain", string.Empty, "1 negative level", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Nightcrawler].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Nightcrawler].Add(helper.BuildData("Poison", string.Empty, "initial and secondary damage 2d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Nightcrawler].Add(helper.BuildData("Summon Undead", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.Nightcrawler].Add(helper.BuildData("Swallow Whole", "2d8+12 bludgeoning + 12 acid", "Energy Drain", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Nightmare].Add(helper.BuildData("Hoof", $"1d8", "1d4 fire", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Nightmare].Add(helper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Nightmare].Add(helper.BuildData("Flaming Hooves", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Nightmare].Add(helper.BuildData("Smoke", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Nightmare].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Nightmare_Cauchemar].Add(helper.BuildData("Hoof", $"2d6", "1d4 fire", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Nightmare_Cauchemar].Add(helper.BuildData("Bite", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Nightmare_Cauchemar].Add(helper.BuildData("Flaming Hooves", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Nightmare_Cauchemar].Add(helper.BuildData("Smoke", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Nightmare_Cauchemar].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Nightwalker].Add(helper.BuildData("Slam", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Nightwalker].Add(helper.BuildData("Crush Item", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Nightwalker].Add(helper.BuildData("Desecrating Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));
                testCases[CreatureConstants.Nightwalker].Add(helper.BuildData("Evil Gaze", string.Empty, "1d8 rounds paralyzed with fear", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.Nightwalker].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Nightwalker].Add(helper.BuildData("Summon Undead", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Nightwing].Add(helper.BuildData("Bite", $"2d6", "Magic Drain", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Nightwing].Add(helper.BuildData("Desecrating Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));
                testCases[CreatureConstants.Nightwing].Add(helper.BuildData("Magic Drain", string.Empty, "1 point enhancement bonus", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Nightwing].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Nightwing].Add(helper.BuildData("Summon Undead", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Nixie].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Nixie].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Nixie].Add(helper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Nixie].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Nymph].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Nymph].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Nymph].Add(helper.BuildData("Blinding Beauty", string.Empty, "Blinded permanently", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Nymph].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Nymph].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Nymph].Add(helper.BuildData("Stunning Glance", string.Empty, "stunned 2d4 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.OchreJelly].Add(helper.BuildData("Slam", $"2d4 + 1d4 acid", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.OchreJelly].Add(helper.BuildData("Acid", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
                testCases[CreatureConstants.OchreJelly].Add(helper.BuildData("Constrict", $"2d4 + 1d4 acid", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.OchreJelly].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Octopus].Add(helper.BuildData("Arms", $"0", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Octopus].Add(helper.BuildData("Bite", $"1d3", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Octopus].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Octopus_Giant].Add(helper.BuildData("Tentacle", $"1d4", string.Empty, 1, "melee", 8, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Octopus_Giant].Add(helper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Octopus_Giant].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Octopus_Giant].Add(helper.BuildData("Constrict", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Ogre].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Ogre].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Ogre].Add(helper.BuildData("Unarmed Strike", "1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Ogre_Merrow].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Ogre_Merrow].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Ogre_Merrow].Add(helper.BuildData("Unarmed Strike", "1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.OgreMage].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.OgreMage].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.OgreMage].Add(helper.BuildData("Unarmed Strike", "1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.OgreMage].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Orc].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Orc].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Orc].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Orc_Half].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Orc_Half].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Orc_Half].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Otyugh].Add(helper.BuildData("Tentacle", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Otyugh].Add(helper.BuildData("Bite", $"1d4", "Disease", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Otyugh].Add(helper.BuildData("Constrict", $"1d6", string.Empty, 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Otyugh].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Otyugh].Add(helper.BuildData("Disease", string.Empty, "Filth Fever", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Otyugh].Add(helper.BuildData("Filth Fever", string.Empty, "incubation period 1d3 days; damage 1d3 Dex and 1d3 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Owl].Add(helper.BuildData("Talons", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Owl_Giant].Add(helper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Owl_Giant].Add(helper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Owlbear].Add(helper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Owlbear].Add(helper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Owlbear].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Pegasus].Add(helper.BuildData("Hoof", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pegasus].Add(helper.BuildData("Bite", $"1d3", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Pegasus].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.PhantomFungus].Add(helper.BuildData("Bite", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.PhaseSpider].Add(helper.BuildData("Bite", $"1d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.PhaseSpider].Add(helper.BuildData("Poison", string.Empty, "initial and secondary damage 1d8 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.PhaseSpider].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Phasm].Add(helper.BuildData("Slam", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.PitFiend].Add(helper.BuildData("Claw", $"2d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.PitFiend].Add(helper.BuildData("Wing", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.PitFiend].Add(helper.BuildData("Bite", $"4d6", "poison, disease", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.PitFiend].Add(helper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.PitFiend].Add(helper.BuildData("Constrict", $"2d8", string.Empty, 2, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.PitFiend].Add(helper.BuildData("Fear Aura", string.Empty, string.Empty, 0, "supernatural ability", 2, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.PitFiend].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.PitFiend].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.PitFiend].Add(helper.BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.PitFiend].Add(helper.BuildData("Poison", string.Empty, "Initial damage 1d6 Con, Secondary damage death", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.PitFiend].Add(helper.BuildData("Disease", string.Empty, "Devil Chills", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.PitFiend].Add(helper.BuildData("Devil Chills", string.Empty, "incubation period 1d4 days, damage 1d4 Str", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Pixie].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Pixie].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Pixie].Add(helper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pixie].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Pixie].Add(helper.BuildData("Special Arrow (Memory Loss)", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, false, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveDcBonus: 2));
                testCases[CreatureConstants.Pixie].Add(helper.BuildData("Special Arrow (Sleep)", string.Empty, SpellConstants.Sleep, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, false, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveDcBonus: 2));

                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(helper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(helper.BuildData("Special Arrow (Memory Loss)", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, false, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveDcBonus: 2));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(helper.BuildData("Special Arrow (Sleep)", string.Empty, SpellConstants.Sleep, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, false, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveDcBonus: 2));

                testCases[CreatureConstants.Pony].Add(helper.BuildData("Hoof", $"1d3", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Pony_War].Add(helper.BuildData("Hoof", $"1d3", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Porpoise].Add(helper.BuildData("Slam", $"2d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.PrayingMantis_Giant].Add(helper.BuildData("Claws", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.PrayingMantis_Giant].Add(helper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.PrayingMantis_Giant].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Pseudodragon].Add(helper.BuildData("Sting", $"1d3", "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pseudodragon].Add(helper.BuildData("Bite", $"1", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Pseudodragon].Add(helper.BuildData("Poison", string.Empty, "initial damage sleep for 1 minute, secondary damage sleep for 1d3 hours", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 2));

                testCases[CreatureConstants.PurpleWorm].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.PurpleWorm].Add(helper.BuildData("Sting", $"2d6", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.PurpleWorm].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.PurpleWorm].Add(helper.BuildData("Poison", string.Empty, "initial damage 1d6 Str, secondary damage 2d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.PurpleWorm].Add(helper.BuildData("Swallow Whole", "2d8+12 bludgeoning", "8 acid", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Pyrohydra_5Heads].Add(helper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 5, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pyrohydra_5Heads].Add(helper.BuildData("Breath weapon", $"3d6 fire per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Pyrohydra_6Heads].Add(helper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 6, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pyrohydra_6Heads].Add(helper.BuildData("Breath weapon", $"3d6 fire per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Pyrohydra_7Heads].Add(helper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 7, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pyrohydra_7Heads].Add(helper.BuildData("Breath weapon", $"3d6 fire per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Pyrohydra_8Heads].Add(helper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 8, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pyrohydra_8Heads].Add(helper.BuildData("Breath weapon", $"3d6 fire per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Pyrohydra_9Heads].Add(helper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 9, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pyrohydra_9Heads].Add(helper.BuildData("Breath weapon", $"3d6 fire per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Pyrohydra_10Heads].Add(helper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 10, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pyrohydra_10Heads].Add(helper.BuildData("Breath weapon", $"3d6 fire per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Pyrohydra_11Heads].Add(helper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 11, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pyrohydra_11Heads].Add(helper.BuildData("Breath weapon", $"3d6 fire per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Pyrohydra_12Heads].Add(helper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 12, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pyrohydra_12Heads].Add(helper.BuildData("Breath weapon", $"3d6 fire per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Quasit].Add(helper.BuildData("Claw", $"1d3", "poison", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Quasit].Add(helper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Quasit].Add(helper.BuildData("Poison", string.Empty, $"Initial damage 1d4 Dex, Secondary damage 2d4 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution, 2));
                testCases[CreatureConstants.Quasit].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Rakshasa].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Rakshasa].Add(helper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Rakshasa].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Rast].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Rast].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Rast].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Rast].Add(helper.BuildData("Paralyzing Gaze", string.Empty, "Paralysis for 1d6 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Rast].Add(helper.BuildData("Blood Drain", string.Empty, "1 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Rat].Add(helper.BuildData("Bite", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Rat_Dire].Add(helper.BuildData("Bite", $"1d3", "Disease", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Rat_Dire].Add(helper.BuildData("Disease", string.Empty, "Filth Fever", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
                testCases[CreatureConstants.Rat_Dire].Add(helper.BuildData("Filth Fever", string.Empty, "incubation period 1d3 days, damage 1d3 Dex and 1d3 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Rat_Swarm].Add(helper.BuildData("Swarm", $"1d6", "Disease", 0, "swarm", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Rat_Swarm].Add(helper.BuildData("Disease", string.Empty, "Filth Fever", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
                testCases[CreatureConstants.Rat_Swarm].Add(helper.BuildData("Filth Fever", string.Empty, "incubation period 1d3 days, damage 1d3 Dex and 1d3 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Rat_Swarm].Add(helper.BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Raven].Add(helper.BuildData("Claws", $"1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Ravid].Add(helper.BuildData("Tail Slap", $"1d6", "Positive Energy", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ravid].Add(helper.BuildData("Claw", $"1d4", "Positive Energy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Ravid].Add(helper.BuildData("Tail Touch", string.Empty, "Positive Energy", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ravid].Add(helper.BuildData("Claw Touch", string.Empty, "Positive Energy", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Ravid].Add(helper.BuildData("Positive Energy", "2d10 positive energy", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Ravid].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.RazorBoar].Add(helper.BuildData("Tusk Slash", $"1d8", "Vorpal Tusk", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.RazorBoar].Add(helper.BuildData("Hoof", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.RazorBoar].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.RazorBoar].Add(helper.BuildData("Vorpal Tusk", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.RazorBoar].Add(helper.BuildData("Trample", "2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Remorhaz].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Remorhaz].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Remorhaz].Add(helper.BuildData("Swallow Whole", "2d8+12 bludgeoning", "8d6 fire", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Retriever].Add(helper.BuildData("Claw", $"2d6", string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Retriever].Add(helper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Retriever].Add(helper.BuildData("Eye Ray", string.Empty, string.Empty, 0, "ranged touch", 1, FeatConstants.Frequencies.Round, false, true, false, true, string.Empty, AbilityConstants.Dexterity));
                testCases[CreatureConstants.Retriever].Add(helper.BuildData("Find Target", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));
                testCases[CreatureConstants.Retriever].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Rhinoceras].Add(helper.BuildData("Gore", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Rhinoceras].Add(helper.BuildData("Powerful Charge", $"4d6", string.Empty, 3, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Roc].Add(helper.BuildData("Talon", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Roc].Add(helper.BuildData("Bite", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Roper].Add(helper.BuildData("Strand", string.Empty, "Drag", 0, "ranged touch", 6, FeatConstants.Frequencies.Round, false, true, false, false));
                testCases[CreatureConstants.Roper].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Roper].Add(helper.BuildData("Drag", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Strength));
                testCases[CreatureConstants.Roper].Add(helper.BuildData("Weakness", string.Empty, "2d8 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.RustMonster].Add(helper.BuildData("Antennae", string.Empty, "Rust", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.RustMonster].Add(helper.BuildData("Bite", "1d3", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.RustMonster].Add(helper.BuildData("Rust", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 4));

                testCases[CreatureConstants.Sahuagin].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Sahuagin].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Sahuagin].Add(helper.BuildData("Talon", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Sahuagin].Add(helper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Sahuagin].Add(helper.BuildData("Blood Frenzy", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.Sahuagin].Add(helper.BuildData("Rake", $"1d4", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Sahuagin_Mutant].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Sahuagin_Mutant].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 2, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Sahuagin_Mutant].Add(helper.BuildData("Talon", $"1d4", string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Sahuagin_Mutant].Add(helper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Sahuagin_Mutant].Add(helper.BuildData("Blood Frenzy", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.Sahuagin_Mutant].Add(helper.BuildData("Rake", $"1d4", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Sahuagin_Malenti].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Sahuagin_Malenti].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Sahuagin_Malenti].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Sahuagin_Malenti].Add(helper.BuildData("Blood Frenzy", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Salamander_Flamebrother].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Salamander_Flamebrother].Add(helper.BuildData("Tail Slap", $"1d4", "Heat", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Salamander_Flamebrother].Add(helper.BuildData("Constrict", $"1d4", "Heat", 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Salamander_Flamebrother].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Salamander_Flamebrother].Add(helper.BuildData("Heat", "1d6 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));

                testCases[CreatureConstants.Salamander_Average].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Salamander_Average].Add(helper.BuildData("Tail Slap", $"2d6", "Heat", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Salamander_Average].Add(helper.BuildData("Constrict", $"2d6", "Heat", 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Salamander_Average].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Salamander_Average].Add(helper.BuildData("Heat", "1d6 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));

                testCases[CreatureConstants.Salamander_Noble].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Salamander_Noble].Add(helper.BuildData("Tail Slap", $"2d8", "Heat", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Salamander_Noble].Add(helper.BuildData("Constrict", $"2d8", "Heat", 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Salamander_Noble].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Salamander_Noble].Add(helper.BuildData("Heat", "1d8 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Salamander_Noble].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Satyr].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, false, false));
                testCases[CreatureConstants.Satyr].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 0, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Satyr].Add(helper.BuildData("Head butt", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Satyr_WithPipes].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, false, false));
                testCases[CreatureConstants.Satyr_WithPipes].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 0, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Satyr_WithPipes].Add(helper.BuildData("Head butt", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Satyr_WithPipes].Add(helper.BuildData("Pipes", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.Scorpion_Monstrous_Tiny].Add(helper.BuildData("Claw", $"1d2", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Tiny].Add(helper.BuildData("Sting", $"1d2", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Tiny].Add(helper.BuildData("Constrict", $"1d2", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Tiny].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Tiny].Add(helper.BuildData("Poison", string.Empty, "initial and secondary damage 1 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Scorpion_Monstrous_Small].Add(helper.BuildData("Claw", $"1d3", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Small].Add(helper.BuildData("Sting", $"1d3", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Small].Add(helper.BuildData("Constrict", $"1d3", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Small].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Small].Add(helper.BuildData("Poison", string.Empty, "initial and secondary damage 1d2 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Scorpion_Monstrous_Medium].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Medium].Add(helper.BuildData("Sting", $"1d4", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Medium].Add(helper.BuildData("Constrict", $"1d4", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Medium].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Medium].Add(helper.BuildData("Poison", string.Empty, "initial and secondary damage 1d3 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Scorpion_Monstrous_Large].Add(helper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Large].Add(helper.BuildData("Sting", $"1d6", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Large].Add(helper.BuildData("Constrict", $"1d6", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Large].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Large].Add(helper.BuildData("Poison", string.Empty, "initial and secondary damage 1d4 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Scorpion_Monstrous_Huge].Add(helper.BuildData("Claw", $"1d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Huge].Add(helper.BuildData("Sting", $"1d8", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Huge].Add(helper.BuildData("Constrict", $"1d8", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Huge].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Huge].Add(helper.BuildData("Poison", string.Empty, "initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan].Add(helper.BuildData("Claw", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan].Add(helper.BuildData("Sting", $"2d6", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan].Add(helper.BuildData("Constrict", $"2d6", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan].Add(helper.BuildData("Poison", string.Empty, "initial and secondary damage 1d8 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Scorpion_Monstrous_Colossal].Add(helper.BuildData("Claw", $"2d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Colossal].Add(helper.BuildData("Sting", $"2d8", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Colossal].Add(helper.BuildData("Constrict", $"2d8", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Colossal].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Colossal].Add(helper.BuildData("Poison", string.Empty, "initial and secondary damage 1d10 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Scorpionfolk].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Scorpionfolk].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 0, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Scorpionfolk].Add(helper.BuildData("Sting", $"1d8", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpionfolk].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpionfolk].Add(helper.BuildData("Poison", string.Empty, "initial and secondary damage 1d4 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Scorpionfolk].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Scorpionfolk].Add(helper.BuildData("Trample", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.SeaCat].Add(helper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.SeaCat].Add(helper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.SeaCat].Add(helper.BuildData("Rend", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.SeaHag].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.SeaHag].Add(helper.BuildData("Horrific Appearance", string.Empty, "2d6 Strength damage", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.SeaHag].Add(helper.BuildData("Evil Eye", string.Empty, string.Empty, 0, "supernatural ability", 3, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));

                testCases[CreatureConstants.Shadow].Add(helper.BuildData("Incorporeal touch", string.Empty, "Strength Damage", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Shadow].Add(helper.BuildData("Strength Damage", string.Empty, "1d6 Str", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Shadow].Add(helper.BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Shadow_Greater].Add(helper.BuildData("Incorporeal touch", string.Empty, "Strength Damage", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Shadow_Greater].Add(helper.BuildData("Strength Damage", string.Empty, "1d8 Str", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Shadow_Greater].Add(helper.BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.ShadowMastiff].Add(helper.BuildData("Bite", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.ShadowMastiff].Add(helper.BuildData("Bay", string.Empty, "panicked 2d4 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.ShadowMastiff].Add(helper.BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.ShamblingMound].Add(helper.BuildData("Slam", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.ShamblingMound].Add(helper.BuildData("Constrict", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.ShamblingMound].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Shark_Dire].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Shark_Dire].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Shark_Dire].Add(helper.BuildData("Swallow Whole", "2d6+6 bludgeoning", "1d8+4 acid", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Shark_Huge].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Shark_Large].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Shark_Medium].Add(helper.BuildData("Bite", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.ShieldGuardian].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.ShieldGuardian].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.ShockerLizard].Add(helper.BuildData("Bite", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.ShockerLizard].Add(helper.BuildData("Stunning Shock", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.ShockerLizard].Add(helper.BuildData("Lethal Shock", $"2d8 electricity per shocker lizard", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, false, true, true, true, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Shrieker].Add(helper.BuildData("Shriek", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Skum].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Skum].Add(helper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Skum].Add(helper.BuildData("Rake", $"1d6", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Slaad_Red].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Slaad_Red].Add(helper.BuildData("Claw", $"1d4", "Implant", 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Slaad_Red].Add(helper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Slaad_Red].Add(helper.BuildData("Implant", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Slaad_Red].Add(helper.BuildData("Stunning Croak", string.Empty, "Stunned 1d3 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Slaad_Red].Add(helper.BuildData("Summon Slaad", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Slaad_Blue].Add(helper.BuildData("Claw", $"2d6", string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Slaad_Blue].Add(helper.BuildData("Bite", $"2d8", "Disease", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Slaad_Blue].Add(helper.BuildData("Disease", string.Empty, "Slaad Fever", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Slaad_Blue].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Slaad_Blue].Add(helper.BuildData("Slaad Fever", string.Empty, "incubation period 1 day, damage 1d3 Dex and 1d3 Cha", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Slaad_Blue].Add(helper.BuildData("Summon Slaad", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Slaad_Green].Add(helper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Slaad_Green].Add(helper.BuildData("Bite", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Slaad_Green].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Slaad_Green].Add(helper.BuildData("Summon Slaad", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Slaad_Gray].Add(helper.BuildData("Claw", $"2d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Slaad_Gray].Add(helper.BuildData("Bite", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Slaad_Gray].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Slaad_Gray].Add(helper.BuildData("Summon Slaad", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Slaad_Death].Add(helper.BuildData("Claw", $"3d6", "Stun", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Slaad_Death].Add(helper.BuildData("Bite", $"2d10", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Slaad_Death].Add(helper.BuildData("Stun", string.Empty, "Stunned 1 round", 0, "extraordinary ability", 3, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Wisdom, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Slaad_Death].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Slaad_Death].Add(helper.BuildData("Summon Slaad", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Snake_Constrictor].Add(helper.BuildData("Bite", $"1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Snake_Constrictor].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Snake_Constrictor].Add(helper.BuildData("Constrict", $"1d3", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Snake_Constrictor_Giant].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Snake_Constrictor_Giant].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Snake_Constrictor_Giant].Add(helper.BuildData("Constrict", $"1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Snake_Viper_Tiny].Add(helper.BuildData("Bite", $"1", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Snake_Viper_Tiny].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Snake_Viper_Small].Add(helper.BuildData("Bite", $"1d2", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Snake_Viper_Small].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Snake_Viper_Medium].Add(helper.BuildData("Bite", $"1d4", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Snake_Viper_Medium].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Snake_Viper_Large].Add(helper.BuildData("Bite", $"1d4", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Snake_Viper_Large].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Snake_Viper_Huge].Add(helper.BuildData("Bite", $"1d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Snake_Viper_Huge].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spectre].Add(helper.BuildData("Incorporeal touch", $"1d8", "Energy Drain", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spectre].Add(helper.BuildData("Energy Drain", string.Empty, "2 negative levels", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Spectre].Add(helper.BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny].Add(helper.BuildData("Bite", $"1d3", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d2 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Small].Add(helper.BuildData("Bite", $"1d4", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Small].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d3 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium].Add(helper.BuildData("Bite", $"1d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d4 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Large].Add(helper.BuildData("Bite", $"1d8", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Large].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge].Add(helper.BuildData("Bite", $"2d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d8 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan].Add(helper.BuildData("Bite", $"2d8", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 2d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal].Add(helper.BuildData("Bite", $"4d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 2d8 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny].Add(helper.BuildData("Bite", $"1d3", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d2 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny].Add(helper.BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small].Add(helper.BuildData("Bite", $"1d4", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d3 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small].Add(helper.BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium].Add(helper.BuildData("Bite", $"1d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d4 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium].Add(helper.BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large].Add(helper.BuildData("Bite", $"1d8", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large].Add(helper.BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge].Add(helper.BuildData("Bite", $"2d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d8 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge].Add(helper.BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan].Add(helper.BuildData("Bite", $"2d8", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 2d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan].Add(helper.BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal].Add(helper.BuildData("Bite", $"4d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 2d8 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal].Add(helper.BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, AbilityConstants.Constitution));

                testCases[CreatureConstants.SpiderEater].Add(helper.BuildData("Sting", $"1d8", "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.SpiderEater].Add(helper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.SpiderEater].Add(helper.BuildData("Poison", string.Empty, "Initial damage none, secondary damage paralysis for 1d8+5 weeks", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.SpiderEater].Add(helper.BuildData("Implant", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Spider_Swarm].Add(helper.BuildData("Swarm", $"1d6", "poison", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Swarm].Add(helper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d3 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Spider_Swarm].Add(helper.BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Squid].Add(helper.BuildData("Arms", $"0", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Squid].Add(helper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Squid].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Squid_Giant].Add(helper.BuildData("Tentacle", $"1d6", string.Empty, 1, "melee", 10, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Squid_Giant].Add(helper.BuildData("Bite", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Squid_Giant].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Squid_Giant].Add(helper.BuildData("Constrict", $"1d6", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.StagBeetle_Giant].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.StagBeetle_Giant].Add(helper.BuildData("Trample", $"2d8", string.Empty, 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Stirge].Add(helper.BuildData("Touch", string.Empty, "Attach", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Stirge].Add(helper.BuildData("Attach", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Stirge].Add(helper.BuildData("Blood Drain", string.Empty, "1d4 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Succubus].Add(helper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Succubus].Add(helper.BuildData("Energy Drain", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, string.Empty, AbilityConstants.Charisma));
                testCases[CreatureConstants.Succubus].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Succubus].Add(helper.BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Tarrasque].Add(helper.BuildData("Bite", $"4d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Tarrasque].Add(helper.BuildData("Horn", $"1d10", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tarrasque].Add(helper.BuildData("Claw", $"1d12", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tarrasque].Add(helper.BuildData("Tail Slap", $"3d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tarrasque].Add(helper.BuildData("Augmented Critical", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tarrasque].Add(helper.BuildData("Frightful Presence", string.Empty, "Shaken", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.Tarrasque].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tarrasque].Add(helper.BuildData("Rush", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true));
                testCases[CreatureConstants.Tarrasque].Add(helper.BuildData("Swallow Whole", "2d8+8 bludgeoning + 2d8+6 acid", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Tendriculos].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Tendriculos].Add(helper.BuildData("Tendril", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tendriculos].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tendriculos].Add(helper.BuildData("Swallow Whole", "2d6 acid", "Paralysis", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tendriculos].Add(helper.BuildData("Paralysis", string.Empty, "paralyzed for 3d6 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Thoqqua].Add(helper.BuildData("Slam", $"1d6", "Heat, Burn", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Thoqqua].Add(helper.BuildData("Heat", $"2d6 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Thoqqua].Add(helper.BuildData("Burn", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Tiefling].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Tiefling].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Tiefling].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Tiefling].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Tiger].Add(helper.BuildData("Claw", $"1d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Tiger].Add(helper.BuildData("Bite", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tiger].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tiger].Add(helper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Tiger].Add(helper.BuildData("Rake", $"1d8", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Tiger_Dire].Add(helper.BuildData("Claw", $"2d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Tiger_Dire].Add(helper.BuildData("Bite", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tiger_Dire].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tiger_Dire].Add(helper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Tiger_Dire].Add(helper.BuildData("Rake", $"2d4", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Titan].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Titan].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Titan].Add(helper.BuildData("Slam", $"1d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Titan].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Toad].Add(new[] { None });

                testCases[CreatureConstants.Tojanida_Juvenile].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Tojanida_Juvenile].Add(helper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tojanida_Juvenile].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tojanida_Juvenile].Add(helper.BuildData("Ink Cloud", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Tojanida_Adult].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Tojanida_Adult].Add(helper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tojanida_Adult].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tojanida_Adult].Add(helper.BuildData("Ink Cloud", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Tojanida_Elder].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Tojanida_Elder].Add(helper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tojanida_Elder].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tojanida_Elder].Add(helper.BuildData("Ink Cloud", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Treant].Add(helper.BuildData("Slam", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Treant].Add(helper.BuildData("Animate Trees", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Treant].Add(helper.BuildData("Double Damage Against Objects", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Treant].Add(helper.BuildData("Trample", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Triceratops].Add(helper.BuildData("Gore", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Triceratops].Add(helper.BuildData("Powerful charge", $"4d8", string.Empty, 2, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Triceratops].Add(helper.BuildData("Trample", $"2d12", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Triton].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Triton].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Triton].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Triton].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Troglodyte].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Troglodyte].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Troglodyte].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Troglodyte].Add(helper.BuildData("Bite", $"1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Troglodyte].Add(helper.BuildData("Stench", string.Empty, "Sickened 10 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Troll].Add(helper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Troll].Add(helper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Troll].Add(helper.BuildData("Rend", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Troll_Scrag].Add(helper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Troll_Scrag].Add(helper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Troll_Scrag].Add(helper.BuildData("Rend", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.TrumpetArchon].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.TrumpetArchon].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.TrumpetArchon].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.TrumpetArchon].Add(helper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.TrumpetArchon].Add(helper.BuildData("Trumpet", string.Empty, "1d4 rounds paralyzed", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Tyrannosaurus].Add(helper.BuildData("Bite", $"3d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Tyrannosaurus].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tyrannosaurus].Add(helper.BuildData("Swallow Whole", "2d8 bludgeoning + 8 acid", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.UmberHulk].Add(helper.BuildData("Claw", $"2d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.UmberHulk].Add(helper.BuildData("Bite", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.UmberHulk].Add(helper.BuildData("Confusing Gaze", string.Empty, SpellConstants.Confusion, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.UmberHulk_TrulyHorrid].Add(helper.BuildData("Claw", $"3d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.UmberHulk_TrulyHorrid].Add(helper.BuildData("Bite", $"4d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.UmberHulk_TrulyHorrid].Add(helper.BuildData("Confusing Gaze", string.Empty, SpellConstants.Confusion, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.Unicorn].Add(helper.BuildData("Horn", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Unicorn].Add(helper.BuildData("Hoof", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Unicorn].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.VampireSpawn].Add(helper.BuildData("Slam", $"1d6", "Energy Drain", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.VampireSpawn].Add(helper.BuildData("Blood Drain", string.Empty, "1d4 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.VampireSpawn].Add(helper.BuildData("Domination", string.Empty, SpellConstants.DominatePerson, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.VampireSpawn].Add(helper.BuildData("Energy Drain", string.Empty, "1 negative level", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.VampireSpawn].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Vargouille].Add(helper.BuildData("Bite", $"1d4", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Vargouille].Add(helper.BuildData("Shriek", string.Empty, "paralyzed 2d4 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 1));
                testCases[CreatureConstants.Vargouille].Add(helper.BuildData("Kiss", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 4));
                testCases[CreatureConstants.Vargouille].Add(helper.BuildData("Poison", string.Empty, "unable to heal the vargouille’s bite damage naturally or magically", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 1));

                testCases[CreatureConstants.VioletFungus].Add(helper.BuildData("Tentacle", $"1d6", "Poison", 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.VioletFungus].Add(helper.BuildData("Poison", string.Empty, "initial and secondary damage 1d4 Str and 1d4 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Vrock].Add(helper.BuildData("Claw", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Vrock].Add(helper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Vrock].Add(helper.BuildData("Talon", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Vrock].Add(helper.BuildData("Dance of Ruin", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true, SaveConstants.Reflex, AbilityConstants.Charisma));
                testCases[CreatureConstants.Vrock].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Vrock].Add(helper.BuildData("Spores", "1d8", string.Empty, 0, "melee", 1, FeatConstants.Frequencies.Minute, false, true, true, true));
                testCases[CreatureConstants.Vrock].Add(helper.BuildData("Stunning Screech", string.Empty, string.Empty, 0, "melee", 1, FeatConstants.Frequencies.Hour, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Vrock].Add(helper.BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Wasp_Giant].Add(helper.BuildData("Sting", $"1d3", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wasp_Giant].Add(helper.BuildData("Poison", string.Empty, "initial and secondary damage 1d6 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Weasel].Add(helper.BuildData("Bite", $"1d3", "Attach", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Weasel].Add(helper.BuildData("Attach", $"1d3", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Weasel_Dire].Add(helper.BuildData("Bite", $"1d6", "Attach", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Weasel_Dire].Add(helper.BuildData("Attach", string.Empty, "Blood Drain", 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Weasel_Dire].Add(helper.BuildData("Blood Drain", string.Empty, "1d4 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Whale_Baleen].Add(helper.BuildData("Tail Slap", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Whale_Cachalot].Add(helper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Whale_Cachalot].Add(helper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Whale_Orca].Add(helper.BuildData("Bite", $"2d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Wight].Add(helper.BuildData("Slam", $"1d4", "Energy Drain", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wight].Add(helper.BuildData("Energy Drain", string.Empty, "1 negative level", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Wight].Add(helper.BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.WillOWisp].Add(helper.BuildData("Shock", string.Empty, "2d8 electricity", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.WinterWolf].Add(helper.BuildData("Bite", $"1d8", "Freezing Bite", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.WinterWolf].Add(helper.BuildData("Breath Weapon", "4d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
                testCases[CreatureConstants.WinterWolf].Add(helper.BuildData("Freezing Bite", "1d6 cold", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
                testCases[CreatureConstants.WinterWolf].Add(helper.BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Wolf].Add(helper.BuildData("Bite", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wolf].Add(helper.BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Wolf_Dire].Add(helper.BuildData("Bite", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wolf_Dire].Add(helper.BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Wolverine].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wolverine].Add(helper.BuildData("Bite", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Wolverine].Add(helper.BuildData("Rage", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));

                testCases[CreatureConstants.Wolverine_Dire].Add(helper.BuildData("Claw", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wolverine_Dire].Add(helper.BuildData("Bite", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Wolverine_Dire].Add(helper.BuildData("Rage", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));

                testCases[CreatureConstants.Worg].Add(helper.BuildData("Bite", $"1d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Worg].Add(helper.BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Wraith].Add(helper.BuildData("Incorporeal touch", "1d4", $"Constitution Drain", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wraith].Add(helper.BuildData("Constitution Drain", string.Empty, $"1d4 Con", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Wraith].Add(helper.BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Wraith_Dread].Add(helper.BuildData("Incorporeal touch", "2d6", $"Constitution Drain", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wraith_Dread].Add(helper.BuildData("Constitution Drain", string.Empty, $"1d8 Con", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Wraith_Dread].Add(helper.BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Wyvern].Add(helper.BuildData("Sting", $"1d6", "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wyvern].Add(helper.BuildData("Bite", $"2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Wyvern].Add(helper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Wyvern].Add(helper.BuildData("Talon", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Wyvern].Add(helper.BuildData("Poison", string.Empty, "initial and secondary damage 2d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Wyvern].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Xill].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 2, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Xill].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 2, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Xill].Add(helper.BuildData("Claw", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Xill].Add(helper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Xill].Add(helper.BuildData("Bite", string.Empty, "Paralysis", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Xill].Add(helper.BuildData("Implant", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Xill].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Xill].Add(helper.BuildData("Paralysis", string.Empty, "paralyzed for 1d4 hours", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Xorn_Minor].Add(helper.BuildData("Bite", "2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Xorn_Minor].Add(helper.BuildData("Claw", $"1d3", string.Empty, 0.5, "melee", 3, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Xorn_Average].Add(helper.BuildData("Bite", "4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Xorn_Average].Add(helper.BuildData("Claw", $"1d4", string.Empty, 0.5, "melee", 3, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Xorn_Elder].Add(helper.BuildData("Bite", "4d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Xorn_Elder].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 3, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.YethHound].Add(helper.BuildData("Bite", "1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.YethHound].Add(helper.BuildData("Bay", string.Empty, "panicked 2d4 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.YethHound].Add(helper.BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Yrthak].Add(helper.BuildData("Bite", "2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Yrthak].Add(helper.BuildData("Claw", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Yrthak].Add(helper.BuildData("Sonic Lance", "6d6 sonic", string.Empty, 0, "ranged touch", 1, $"2 {FeatConstants.Frequencies.Round}", false, true, true, false));
                testCases[CreatureConstants.Yrthak].Add(helper.BuildData("Explosion", "2d6 piercing", string.Empty, 0, "supernatural ability", 1, $"2 {FeatConstants.Frequencies.Round}", true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.YuanTi_Pureblood].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.YuanTi_Pureblood].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.YuanTi_Pureblood].Add(helper.BuildData("Unarmed Strike", "1d3", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.YuanTi_Pureblood].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(helper.BuildData("Bite", "1d6", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(helper.BuildData("Poison", string.Empty, "initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(helper.BuildData("Produce Acid", "3d6 acid", "initial and secondary damage 1d6 Con", 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(helper.BuildData("Bite", "1d4", "Poison", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(helper.BuildData("Poison", string.Empty, "initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(helper.BuildData("Produce Acid", "3d6 acid", "initial and secondary damage 1d6 Con", 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(helper.BuildData("Bite", "1d6", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(helper.BuildData("Poison", string.Empty, "initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(helper.BuildData("Produce Acid", "3d6 acid", "initial and secondary damage 1d6 Con", 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(helper.BuildData("Constrict", $"1d4", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(helper.BuildData("Bite", "1d6", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(helper.BuildData("Poison", string.Empty, "initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(helper.BuildData("Produce Acid", "3d6 acid", "initial and secondary damage 1d6 Con", 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(helper.BuildData("Constrict", $"1d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.YuanTi_Abomination].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.YuanTi_Abomination].Add(helper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.YuanTi_Abomination].Add(helper.BuildData("Bite", "2d6", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.YuanTi_Abomination].Add(helper.BuildData("Poison", string.Empty, "initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Abomination].Add(helper.BuildData("Aversion", string.Empty, "aversion 10 minutes", 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.YuanTi_Abomination].Add(helper.BuildData("Produce Acid", "3d6 acid", "initial and secondary damage 1d6 Con", 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Abomination].Add(helper.BuildData("Constrict", $"1d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.YuanTi_Abomination].Add(helper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.YuanTi_Abomination].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Zelekhut].Add(helper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Zelekhut].Add(helper.BuildData("Unarmed Strike", "1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Zelekhut].Add(helper.BuildData("Electrified Weapon", "1d6 electricity", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Zelekhut].Add(helper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

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
