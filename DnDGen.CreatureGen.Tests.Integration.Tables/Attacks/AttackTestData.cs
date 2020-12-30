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
                var attackHelper = new AttackHelper();
                var damageHelper = new DamageHelper();
                var templates = CreatureConstants.Templates.GetAll();

                foreach (var template in templates)
                {
                    testCases[template] = new List<string[]>();
                }

                testCases[CreatureConstants.Templates.CelestialCreature].Add(attackHelper.BuildData("Smite Evil",
                    string.Empty,
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Templates.FiendishCreature].Add(attackHelper.BuildData("Smite Good",
                    string.Empty,
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Templates.Ghost].Add(attackHelper.BuildData("Corrupting Gaze",
                    damageHelper.BuildEntries(
                        "2d10", string.Empty, string.Empty,
                        "1d4", AbilityConstants.Charisma),
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true,
                    SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.Templates.Ghost].Add(attackHelper.BuildData("Corrupting Touch",
                    damageHelper.BuildEntries("1d6"),
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Templates.Ghost].Add(attackHelper.BuildData("Draining Touch",
                    damageHelper.BuildEntries("1d4", "Ability points (of ghost's choosing)"),
                    "Ghost heals 5 points of damage", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Templates.Ghost].Add(attackHelper.BuildData("Frightful Moan",
                    string.Empty,
                    "Panic for 2d4 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true,
                    SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Templates.Ghost].Add(attackHelper.BuildData("Horrific Appearance",
                    damageHelper.BuildEntries(
                        "1d4", AbilityConstants.Strength, string.Empty,
                        "1d4", AbilityConstants.Dexterity, string.Empty,
                        "1d4", AbilityConstants.Constitution),
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true,
                    SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.Templates.Ghost].Add(attackHelper.BuildData("Malevolence",
                    string.Empty,
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true,
                    SaveConstants.Will, AbilityConstants.Charisma, 5));
                testCases[CreatureConstants.Templates.Ghost].Add(attackHelper.BuildData("Manifestation",
                    string.Empty,
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Templates.Ghost].Add(attackHelper.BuildData(SpellConstants.Telekinesis,
                    string.Empty,
                    string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true,
                    SaveConstants.Will, AbilityConstants.Charisma));

                testCases[CreatureConstants.Templates.HalfCelestial].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                    string.Empty,
                    string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Templates.HalfCelestial].Add(attackHelper.BuildData("Smite Evil",
                    string.Empty,
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Templates.HalfDragon_Black].Add(attackHelper.BuildData("Claw",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.HalfDragon_Black].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.HalfDragon_Black].Add(attackHelper.BuildData("Breath Weapon",
                    damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Acid),
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                    SaveConstants.Reflex, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.HalfDragon_Blue].Add(attackHelper.BuildData("Claw",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.HalfDragon_Blue].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.HalfDragon_Blue].Add(attackHelper.BuildData("Breath Weapon",
                    damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Electricity),
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                    SaveConstants.Reflex, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.HalfDragon_Brass].Add(attackHelper.BuildData("Claw",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.HalfDragon_Brass].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.HalfDragon_Brass].Add(attackHelper.BuildData("Breath Weapon",
                    damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Fire),
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                    SaveConstants.Reflex, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.HalfDragon_Bronze].Add(attackHelper.BuildData("Claw",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.HalfDragon_Bronze].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.HalfDragon_Bronze].Add(attackHelper.BuildData("Breath Weapon",
                    damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Electricity),
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                    SaveConstants.Reflex, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.HalfDragon_Copper].Add(attackHelper.BuildData("Claw",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.HalfDragon_Copper].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.HalfDragon_Copper].Add(attackHelper.BuildData("Breath Weapon",
                    damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Acid),
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                    SaveConstants.Reflex, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.HalfDragon_Gold].Add(attackHelper.BuildData("Claw",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.HalfDragon_Gold].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.HalfDragon_Gold].Add(attackHelper.BuildData("Breath Weapon",
                    damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Fire),
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                    SaveConstants.Reflex, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.HalfDragon_Green].Add(attackHelper.BuildData("Claw",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.HalfDragon_Green].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.HalfDragon_Green].Add(attackHelper.BuildData("Breath Weapon",
                    damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Acid + " Gas"),
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                    SaveConstants.Reflex, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.HalfDragon_Red].Add(attackHelper.BuildData("Claw",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.HalfDragon_Red].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.HalfDragon_Red].Add(attackHelper.BuildData("Breath Weapon",
                    damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Fire),
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                    SaveConstants.Reflex, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.HalfDragon_Silver].Add(attackHelper.BuildData("Claw",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.HalfDragon_Silver].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.HalfDragon_Silver].Add(attackHelper.BuildData("Breath Weapon",
                    damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Cold),
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                    SaveConstants.Reflex, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.HalfDragon_White].Add(attackHelper.BuildData("Claw",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.HalfDragon_White].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.HalfDragon_White].Add(attackHelper.BuildData("Breath Weapon",
                    damageHelper.BuildEntries("6d8", FeatConstants.Foci.Elements.Cold),
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true,
                    SaveConstants.Reflex, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.HalfFiend].Add(attackHelper.BuildData("Claw",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.HalfFiend].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.HalfFiend].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                    string.Empty,
                    string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Templates.HalfFiend].Add(attackHelper.BuildData("Smite Good",
                    string.Empty,
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Templates.Lich].Add(attackHelper.BuildData("Touch",
                    damageHelper.BuildEntries("1d8+5"),
                    "Paralyzing Touch", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Templates.Lich].Add(attackHelper.BuildData("Fear Aura",
                    string.Empty,
                    "Fear", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Templates.Lich].Add(attackHelper.BuildData("Paralyzing Touch",
                    string.Empty,
                    "Paralyzed", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Charisma));

                testCases[CreatureConstants.Templates.None].Add(new[] { None });

                testCases[CreatureConstants.Templates.Skeleton].Add(attackHelper.BuildData("Claw",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Templates.Vampire].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Vampire].Add(attackHelper.BuildData("Blood Drain",
                    damageHelper.BuildEntries("1d4", AbilityConstants.Constitution),
                    "Vampire gains 5 HP", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Templates.Vampire].Add(attackHelper.BuildData("Children of the Night",
                    string.Empty,
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Templates.Vampire].Add(attackHelper.BuildData("Dominate",
                    string.Empty,
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Templates.Vampire].Add(attackHelper.BuildData("Create Spawn",
                    string.Empty,
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Templates.Vampire].Add(attackHelper.BuildData("Energy Drain",
                    damageHelper.BuildEntries("2", "Negative Levels"),
                    "Vampire gains 5 HP", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural].Add(attackHelper.BuildData("Claw (in Hybrid form)",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural].Add(attackHelper.BuildData("Bite (in Hybrid form)",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural].Add(attackHelper.BuildData("Curse of Lycanthropy",
                    string.Empty,
                    "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true,
                    SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.Lycanthrope_Boar_Natural].Add(attackHelper.BuildData("Claw (in Hybrid form)",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Boar_Natural].Add(attackHelper.BuildData("Gore (in Hybrid form)",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Boar_Natural].Add(attackHelper.BuildData("Curse of Lycanthropy",
                    string.Empty,
                    "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural].Add(attackHelper.BuildData("Claw (in Hybrid form)",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural].Add(attackHelper.BuildData("Gore (in Hybrid form)",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural].Add(attackHelper.BuildData("Curse of Lycanthropy",
                    string.Empty,
                    "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural].Add(attackHelper.BuildData("Claw (in Hybrid form)",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural].Add(attackHelper.BuildData("Bite (in Hybrid form)",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural].Add(attackHelper.BuildData("Curse of Lycanthropy",
                    string.Empty,
                    "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Natural].Add(attackHelper.BuildData("Claw (in Hybrid form)",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Natural].Add(attackHelper.BuildData("Bite (in Hybrid form)",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Natural].Add(attackHelper.BuildData("Curse of Lycanthropy",
                    string.Empty,
                    "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Natural].Add(attackHelper.BuildData("Claw (in Hybrid form)",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Natural].Add(attackHelper.BuildData("Bite (in Hybrid form)",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Natural].Add(attackHelper.BuildData("Curse of Lycanthropy",
                    string.Empty,
                    "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural].Add(attackHelper.BuildData("Claw (in Hybrid form)",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural].Add(attackHelper.BuildData("Bite (in Hybrid form)",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    "Curse of Lycanthropy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural].Add(attackHelper.BuildData("Curse of Lycanthropy",
                    string.Empty,
                    "Contract lycanthropy", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted].Add(attackHelper.BuildData("Claw (in Hybrid form)",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted].Add(attackHelper.BuildData("Bite (in Hybrid form)",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Templates.Lycanthrope_Boar_Afflicted].Add(attackHelper.BuildData("Claw (in Hybrid form)",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Boar_Afflicted].Add(attackHelper.BuildData("Gore (in Hybrid form)",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted].Add(attackHelper.BuildData("Claw (in Hybrid form)",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted].Add(attackHelper.BuildData("Gore (in Hybrid form)",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted].Add(attackHelper.BuildData("Claw (in Hybrid form)",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted].Add(attackHelper.BuildData("Bite (in Hybrid form)",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted].Add(attackHelper.BuildData("Claw (in Hybrid form)",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted].Add(attackHelper.BuildData("Bite (in Hybrid form)",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted].Add(attackHelper.BuildData("Claw (in Hybrid form)",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted].Add(attackHelper.BuildData("Bite (in Hybrid form)",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted].Add(attackHelper.BuildData("Claw (in Hybrid form)",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted].Add(attackHelper.BuildData("Bite (in Hybrid form)",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Templates.Zombie].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                return TestDataHelper.EnumerateTestCases(testCases);
            }
        }

        public static IEnumerable Creatures
        {
            get
            {
                var testCases = new Dictionary<string, List<string[]>>();
                var attackHelper = new AttackHelper();
                var damageHelper = new DamageHelper();
                var creatures = CreatureConstants.GetAll();

                foreach (var creature in creatures)
                {
                    testCases[creature] = new List<string[]>();
                }

                testCases[CreatureConstants.Aasimar].Add(attackHelper.BuildData(AttributeConstants.Melee,
                    string.Empty,
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Aasimar].Add(attackHelper.BuildData(AttributeConstants.Ranged,
                    string.Empty,
                    string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Aasimar].Add(attackHelper.BuildData("Unarmed Strike",
                    damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Aasimar].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                    string.Empty,
                    string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Aboleth].Add(attackHelper.BuildData("Tentacle",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                    "Slime", 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Aboleth].Add(attackHelper.BuildData("Enslave",
                    string.Empty,
                    string.Empty, 0, "supernatural ability", 3, FeatConstants.Frequencies.Day, false, true, true, true,
                    SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Aboleth].Add(attackHelper.BuildData("Slime",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true,
                    SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Aboleth].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.Psionic,
                    string.Empty,
                    string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Achaierai].Add(attackHelper.BuildData("Claw",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Achaierai].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Achaierai].Add(attackHelper.BuildData("Black cloud",
                    damageHelper.BuildEntries("2d6", "Toxic black cloud"),
                    "insanity", 0, "extraordinary ability", 3, FeatConstants.Frequencies.Day, false, true, true, true,
                    SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Allip].Add(attackHelper.BuildData("Incorporeal touch",
                    string.Empty,
                    "Wisdom drain", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Allip].Add(attackHelper.BuildData("Babble",
                    string.Empty,
                    SpellConstants.Hypnotism, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true,
                    SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Allip].Add(attackHelper.BuildData("Madness",
                    damageHelper.BuildEntries("1d4", AbilityConstants.Wisdom),
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));
                testCases[CreatureConstants.Allip].Add(attackHelper.BuildData("Wisdom drain",
                    damageHelper.BuildEntries("1d4", AbilityConstants.Wisdom),
                    "Allip gains 5 temporary hit points", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));

                testCases[CreatureConstants.Androsphinx].Add(attackHelper.BuildData("Claw",
                    damageHelper.BuildEntries("2d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Androsphinx].Add(attackHelper.BuildData("Pounce",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Androsphinx].Add(attackHelper.BuildData("Rake",
                    damageHelper.BuildEntries("2d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Androsphinx].Add(attackHelper.BuildData("Roar",
                    string.Empty,
                    string.Empty, 0, "supernatural ability", 3, FeatConstants.Frequencies.Day, false, true, true, true,
                    SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Androsphinx].Add(attackHelper.BuildData("Spells",
                    string.Empty,
                    string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Angel_AstralDeva].Add(attackHelper.BuildData(AttributeConstants.Melee,
                    string.Empty,
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Angel_AstralDeva].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Angel_AstralDeva].Add(attackHelper.BuildData("Stun",
                    string.Empty,
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Angel_AstralDeva].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                    string.Empty,
                    string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Angel_Planetar].Add(attackHelper.BuildData(AttributeConstants.Melee,
                    string.Empty,
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Angel_Planetar].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Angel_Planetar].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                    string.Empty,
                    string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Angel_Planetar].Add(attackHelper.BuildData("Spells",
                    string.Empty,
                    string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Angel_Solar].Add(attackHelper.BuildData(AttributeConstants.Melee,
                    string.Empty,
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Angel_Solar].Add(attackHelper.BuildData(AttributeConstants.Ranged,
                    string.Empty,
                    string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Angel_Solar].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Angel_Solar].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                    string.Empty,
                    string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Angel_Solar].Add(attackHelper.BuildData("Spells",
                    string.Empty,
                    string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Anvil_Tiny].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Anvil_Small].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Anvil_Medium].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Anvil_Large].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Anvil_Large].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Anvil_Huge].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Anvil_Huge].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Anvil_Gargantuan].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Anvil_Gargantuan].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Anvil_Colossal].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Anvil_Colossal].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Tiny].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Small].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Medium].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Large].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Stone_Large].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Huge].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Stone_Huge].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Gargantuan].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Stone_Gargantuan].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Stone_Colossal].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Stone_Colossal].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Tiny].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Small].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Medium].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Large].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Wood_Large].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Huge].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Wood_Huge].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Gargantuan].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Wood_Gargantuan].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Block_Wood_Colossal].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Block_Wood_Colossal].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Box_Tiny].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Box_Small].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Box_Medium].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Box_Large].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Box_Large].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Box_Huge].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Box_Huge].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Box_Gargantuan].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Box_Gargantuan].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Box_Colossal].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Box_Colossal].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Tiny].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Tiny].Add(attackHelper.BuildData("Blind",
                    string.Empty,
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Tiny].Add(attackHelper.BuildData("Constrict",
                    damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Small].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Small].Add(attackHelper.BuildData("Blind",
                    string.Empty,
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Small].Add(attackHelper.BuildData("Constrict",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Medium].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Medium].Add(attackHelper.BuildData("Blind",
                    string.Empty,
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Medium].Add(attackHelper.BuildData("Constrict",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Large].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Large].Add(attackHelper.BuildData("Blind",
                    string.Empty,
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Large].Add(attackHelper.BuildData("Constrict",
                    damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Huge].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Huge].Add(attackHelper.BuildData("Blind",
                    string.Empty,
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Huge].Add(attackHelper.BuildData("Constrict",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Gargantuan].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Gargantuan].Add(attackHelper.BuildData("Blind",
                    string.Empty,
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Gargantuan].Add(attackHelper.BuildData("Constrict",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carpet_Colossal].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carpet_Colossal].Add(attackHelper.BuildData("Blind",
                    string.Empty,
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Carpet_Colossal].Add(attackHelper.BuildData("Constrict",
                    damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carriage_Tiny].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Carriage_Small].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Carriage_Medium].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Carriage_Large].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carriage_Large].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carriage_Huge].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carriage_Huge].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carriage_Gargantuan].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carriage_Gargantuan].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Carriage_Colossal].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Carriage_Colossal].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Tiny].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Tiny].Add(attackHelper.BuildData("Constrict",
                    damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Small].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Small].Add(attackHelper.BuildData("Constrict",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Medium].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Medium].Add(attackHelper.BuildData("Constrict",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Large].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Large].Add(attackHelper.BuildData("Constrict",
                    damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Chain_Large].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Huge].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Huge].Add(attackHelper.BuildData("Constrict",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Chain_Huge].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Gargantuan].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Gargantuan].Add(attackHelper.BuildData("Constrict",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Chain_Gargantuan].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chain_Colossal].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chain_Colossal].Add(attackHelper.BuildData("Constrict",
                    damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Chain_Colossal].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chair_Tiny].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Chair_Small].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Chair_Medium].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Chair_Large].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chair_Large].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chair_Huge].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chair_Huge].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chair_Gargantuan].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chair_Gargantuan].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Chair_Colossal].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Chair_Colossal].Add(attackHelper.BuildData("Trample",
                    damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Tiny].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Tiny].Add(attackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Tiny].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Small].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Small].Add(attackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Small].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Medium].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Medium].Add(attackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Medium].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Large].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Large].Add(attackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Large].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Huge].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Huge].Add(attackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Huge].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Gargantuan].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Gargantuan].Add(attackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Gargantuan].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Clothes_Colossal].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Clothes_Colossal].Add(attackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Clothes_Colossal].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Ladder_Tiny].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Ladder_Small].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Ladder_Medium].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Ladder_Large].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Ladder_Large].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Ladder_Huge].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Ladder_Huge].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Ladder_Gargantuan].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Ladder_Gargantuan].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Ladder_Colossal].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Ladder_Colossal].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Tiny].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Tiny].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Small].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Small].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Medium].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Medium].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Large].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Large].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Huge].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Huge].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Gargantuan].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Gargantuan].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rope_Colossal].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rope_Colossal].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Tiny].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Tiny].Add(attackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Tiny].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Small].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Small].Add(attackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Small].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Medium].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Medium].Add(attackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Medium].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Large].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Large].Add(attackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Large].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Huge].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Huge].Add(attackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Huge].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Gargantuan].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Gargantuan].Add(attackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Gargantuan].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Rug_Colossal].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Rug_Colossal].Add(attackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Rug_Colossal].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Sled_Tiny].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Sled_Small].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Sled_Medium].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Sled_Large].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Sled_Large].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Sled_Huge].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Sled_Huge].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Sled_Gargantuan].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Sled_Gargantuan].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Sled_Colossal].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Sled_Colossal].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Tiny].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Small].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Medium].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Large].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Large].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Huge].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Huge].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Gargantuan].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Gargantuan].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Colossal].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Animal_Colossal].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Tiny].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Small].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Medium].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Large].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Large].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Huge].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Huge].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Gargantuan].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Gargantuan].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Colossal].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Statue_Humanoid_Colossal].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Stool_Tiny].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Stool_Small].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Stool_Medium].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Stool_Large].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Stool_Large].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Stool_Huge].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Stool_Huge].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Stool_Gargantuan].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Stool_Gargantuan].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Stool_Colossal].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Stool_Colossal].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Table_Tiny].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Table_Small].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Table_Medium].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Table_Large].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Table_Large].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Table_Huge].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Table_Huge].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Table_Gargantuan].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Table_Gargantuan].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Table_Colossal].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Table_Colossal].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Tiny].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Tiny].Add(attackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Tiny].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Small].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Small].Add(attackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Small].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Medium].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Medium].Add(attackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Medium].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Large].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Large].Add(attackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Large].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Huge].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Huge].Add(attackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Huge].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Gargantuan].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Gargantuan].Add(attackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Gargantuan].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Tapestry_Colossal].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Colossal].Add(attackHelper.BuildData("Blind", string.Empty, string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AnimatedObject_Tapestry_Colossal].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Wagon_Tiny].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Wagon_Small].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Wagon_Medium].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.AnimatedObject_Wagon_Large].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Wagon_Large].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Wagon_Huge].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Wagon_Huge].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Wagon_Gargantuan].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Wagon_Gargantuan].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.AnimatedObject_Wagon_Colossal].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AnimatedObject_Wagon_Colossal].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Ankheg].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing, "1d4", FeatConstants.Foci.Elements.Acid),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ankheg].Add(attackHelper.BuildData("Improved Grab",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Ankheg].Add(attackHelper.BuildData("Spit Acid",
                    damageHelper.BuildEntries("4d4", FeatConstants.Foci.Elements.Acid),
                    string.Empty, 0, "extraordinary ability", 1, $"6 {FeatConstants.Frequencies.Hour}", false, true, true, true));

                testCases[CreatureConstants.Annis].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Annis].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Annis].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Annis].Add(attackHelper.BuildData("Rake", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Annis].Add(attackHelper.BuildData("Rend", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Annis].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Ant_Giant_Worker].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ant_Giant_Worker].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Ant_Giant_Soldier].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("2d4", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ant_Giant_Soldier].Add(attackHelper.BuildData("Improved Grab",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Ant_Giant_Soldier].Add(attackHelper.BuildData("Acid Sting",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing, "1d4", FeatConstants.Foci.Elements.Acid),
                    string.Empty, 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Ant_Giant_Queen].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ant_Giant_Queen].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Ape].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ape].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Ape_Dire].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ape_Dire].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Ape_Dire].Add(attackHelper.BuildData("Rend", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Aranea].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), "poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Aranea].Add(attackHelper.BuildData("Poison", string.Empty, "Initial damage 1d6 Str, Secondary damage 2d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, false, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Aranea].Add(attackHelper.BuildData("Web", string.Empty, string.Empty, 0, "ranged, extraordinary ability", 6, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.Aranea].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Arrowhawk_Juvenile].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Arrowhawk_Juvenile].Add(attackHelper.BuildData("Electricity ray",
                    damageHelper.BuildEntries("2d6", FeatConstants.Foci.Elements.Electricity),
                    string.Empty, 0, "ranged touch, supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Arrowhawk_Adult].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Arrowhawk_Adult].Add(attackHelper.BuildData("Electricity ray",
                    damageHelper.BuildEntries("2d8", FeatConstants.Foci.Elements.Electricity),
                    string.Empty, 0, "ranged touch, supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Arrowhawk_Elder].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Arrowhawk_Elder].Add(attackHelper.BuildData("Electricity ray",
                    damageHelper.BuildEntries("2d8", FeatConstants.Foci.Elements.Electricity),
                    string.Empty, 0, "ranged touch, supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.AssassinVine].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.AssassinVine].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.AssassinVine].Add(attackHelper.BuildData("Entangle", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.AssassinVine].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Athach].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Athach].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, false, false, false));
                testCases[CreatureConstants.Athach].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), "poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Athach].Add(attackHelper.BuildData("Rock", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Athach].Add(attackHelper.BuildData("Rock", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "ranged", 2, FeatConstants.Frequencies.Round, false, true, false, false));
                testCases[CreatureConstants.Athach].Add(attackHelper.BuildData("Poison", string.Empty, $"Initial damage 1d6 Str, Secondary damage 2d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Avoral].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Avoral].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Avoral].Add(attackHelper.BuildData("Fear Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Avoral].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Azer].Add(attackHelper.BuildData(AttributeConstants.Melee,
                    string.Empty,
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Azer].Add(attackHelper.BuildData(AttributeConstants.Ranged,
                    string.Empty,
                    string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Azer].Add(attackHelper.BuildData("Unarmed Strike",
                    damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Azer].Add(attackHelper.BuildData("Heat",
                    damageHelper.BuildEntries("1", FeatConstants.Foci.Elements.Fire),
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));

                testCases[CreatureConstants.Babau].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Babau].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Babau].Add(attackHelper.BuildData("Sneak Attack", damageHelper.BuildEntries("2d6", string.Empty), string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Babau].Add(attackHelper.BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.Babau].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Baboon].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Badger].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Badger].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Badger].Add(attackHelper.BuildData("Rage", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Badger_Dire].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Badger_Dire].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Badger_Dire].Add(attackHelper.BuildData("Rage", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Balor].Add(attackHelper.BuildData(AttributeConstants.Melee,
                    string.Empty,
                    string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Balor].Add(attackHelper.BuildData(AttributeConstants.Melee,
                    string.Empty,
                    string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, false, false));
                testCases[CreatureConstants.Balor].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d10", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Balor].Add(attackHelper.BuildData("Death Throes",
                    damageHelper.BuildEntries("100", string.Empty),
                    string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Life, false, true, false, true,
                    SaveConstants.Reflex, AbilityConstants.Constitution));
                testCases[CreatureConstants.Balor].Add(attackHelper.BuildData("Entangle",
                    string.Empty,
                    string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, false, false, true));
                testCases[CreatureConstants.Balor].Add(attackHelper.BuildData("Summon Demon",
                    string.Empty,
                    string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.Balor].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                    string.Empty,
                    string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(attackHelper.BuildData("Claw",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing),
                    "fear", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(attackHelper.BuildData("Fear",
                    string.Empty,
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(attackHelper.BuildData("Improved Grab",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(attackHelper.BuildData("Impale",
                    damageHelper.BuildEntries("3d8", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(attackHelper.BuildData("Summon Devil",
                    string.Empty,
                    string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.BarbedDevil_Hamatula].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                    string.Empty,
                    string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Barghest].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Barghest].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Barghest].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Barghest].Add(attackHelper.BuildData("Feed", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Barghest_Greater].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Barghest_Greater].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Barghest_Greater].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Barghest_Greater].Add(attackHelper.BuildData("Feed", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Basilisk].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Basilisk].Add(attackHelper.BuildData("Petrifying Gaze", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));

                testCases[CreatureConstants.Basilisk_Greater].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Basilisk_Greater].Add(attackHelper.BuildData("Petrifying Gaze", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));

                testCases[CreatureConstants.Bat].Add(new[] { None });

                testCases[CreatureConstants.Bat_Dire].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Bat_Swarm].Add(attackHelper.BuildData("Swarm",
                    damageHelper.BuildEntries("1d6", string.Empty),
                    string.Empty, 0, "swarm", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bat_Swarm].Add(attackHelper.BuildData("Distraction",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Bat_Swarm].Add(attackHelper.BuildData("Wounding",
                    damageHelper.BuildEntries("1", string.Empty),
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));

                testCases[CreatureConstants.Bear_Black].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bear_Black].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Bear_Brown].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bear_Brown].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Bear_Brown].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Bear_Dire].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bear_Dire].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Bear_Dire].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Bear_Polar].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bear_Polar].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Bear_Polar].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(attackHelper.BuildData(AttributeConstants.Melee,
                    string.Empty,
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(attackHelper.BuildData("Claw",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing),
                    "Infernal Wound", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(attackHelper.BuildData("Infernal Wound",
                    damageHelper.BuildEntries("2", string.Empty),
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, string.Empty, AbilityConstants.Constitution));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(attackHelper.BuildData("Beard",
                    damageHelper.BuildEntries("1d8", string.Empty),
                    "Disease", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(attackHelper.BuildData("Battle Frenzy",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 2, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(attackHelper.BuildData("Summon Devil",
                    string.Empty,
                    string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(attackHelper.BuildData("Disease",
                    string.Empty,
                    "Devil Chills", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(attackHelper.BuildData("Devil Chills",
                    string.Empty,
                    "incubation period 1d4 days, damage 1d4 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.BeardedDevil_Barbazu].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                    string.Empty,
                    string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Bebilith].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing),
                    "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bebilith].Add(attackHelper.BuildData("Claw",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Bebilith].Add(attackHelper.BuildData("Web",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 4, FeatConstants.Frequencies.Day, false, true, true, true, string.Empty, AbilityConstants.Constitution));
                testCases[CreatureConstants.Bebilith].Add(attackHelper.BuildData("Poison",
                    string.Empty,
                    "Initial damage 1d6 Con, Secondary damage 2d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Bebilith].Add(attackHelper.BuildData("Rend Armor",
                    damageHelper.BuildEntries("4d6", string.Empty),
                    string.Empty, 2, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Bebilith].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                    string.Empty,
                    string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Bee_Giant].Add(attackHelper.BuildData("Sting", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Life, true, true, true, false));
                testCases[CreatureConstants.Bee_Giant].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d4 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Behir].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("2d4", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Behir].Add(attackHelper.BuildData("Breath Weapon",
                    damageHelper.BuildEntries("7d6", FeatConstants.Foci.Elements.Electricity),
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
                testCases[CreatureConstants.Behir].Add(attackHelper.BuildData("Constrict",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Behir].Add(attackHelper.BuildData("Improved Grab",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Behir].Add(attackHelper.BuildData("Rake",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing),
                    string.Empty, 0.5, "extraordinary ability", 6, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Behir].Add(attackHelper.BuildData("Swallow Whole",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning, "8", FeatConstants.Foci.Elements.Acid),
                    string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Beholder].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d4", AttributeConstants.DamageTypes.Piercing), string.Empty, .5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Beholder].Add(attackHelper.BuildData("Eye ray", string.Empty, SpellConstants.CharmMonster, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(attackHelper.BuildData("Eye ray", string.Empty, SpellConstants.CharmPerson, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(attackHelper.BuildData("Eye ray", string.Empty, SpellConstants.InflictModerateWounds, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(attackHelper.BuildData("Eye ray", string.Empty, SpellConstants.Disintegrate, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(attackHelper.BuildData("Eye ray", string.Empty, SpellConstants.Fear, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(attackHelper.BuildData("Eye ray", string.Empty, SpellConstants.FingerOfDeath, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(attackHelper.BuildData("Eye ray", string.Empty, SpellConstants.FleshToStone, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(attackHelper.BuildData("Eye ray", string.Empty, SpellConstants.Sleep, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(attackHelper.BuildData("Eye ray", string.Empty, SpellConstants.Slow, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder].Add(attackHelper.BuildData("Eye ray", string.Empty, SpellConstants.Telekinesis, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));

                testCases[CreatureConstants.Beholder_Gauth].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, .5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Beholder_Gauth].Add(attackHelper.BuildData("Eye ray", string.Empty, SpellConstants.Sleep, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder_Gauth].Add(attackHelper.BuildData("Eye ray", string.Empty, SpellConstants.InflictModerateWounds, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder_Gauth].Add(attackHelper.BuildData("Eye ray", string.Empty, SpellConstants.DispelMagic, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Beholder_Gauth].Add(attackHelper.BuildData("Eye ray", string.Empty, SpellConstants.ScorchingRay, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Beholder_Gauth].Add(attackHelper.BuildData("Eye ray", string.Empty, "Paralysis", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.Beholder_Gauth].Add(attackHelper.BuildData("Eye ray", string.Empty, SpellConstants.RayOfExhaustion, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Belker].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Belker].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Belker].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Belker].Add(attackHelper.BuildData("Smoke Claw", damageHelper.BuildEntries("3d4"), string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Bison].Add(attackHelper.BuildData("Gore", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bison].Add(attackHelper.BuildData("Stampede", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, SaveConstants.Reflex, AbilityConstants.Strength));

                testCases[CreatureConstants.BlackPudding].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning, string.Empty, "2d6", FeatConstants.Foci.Elements.Acid),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.BlackPudding].Add(attackHelper.BuildData("Acid", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
                testCases[CreatureConstants.BlackPudding].Add(attackHelper.BuildData("Constrict",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning, string.Empty, "2d6", FeatConstants.Foci.Elements.Acid),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.BlackPudding].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.BlackPudding_Elder].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("3d6", AttributeConstants.DamageTypes.Bludgeoning, string.Empty, "3d6", FeatConstants.Foci.Elements.Acid),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.BlackPudding_Elder].Add(attackHelper.BuildData("Acid", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
                testCases[CreatureConstants.BlackPudding_Elder].Add(attackHelper.BuildData("Constrict",
                    damageHelper.BuildEntries("3d6", AttributeConstants.DamageTypes.Bludgeoning, string.Empty, "3d6", FeatConstants.Foci.Elements.Acid),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.BlackPudding_Elder].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.BlinkDog].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.BlinkDog].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Boar].Add(attackHelper.BuildData("Gore", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Boar].Add(attackHelper.BuildData("Ferocity", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Boar_Dire].Add(attackHelper.BuildData("Gore", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Boar_Dire].Add(attackHelper.BuildData("Ferocity", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Bodak].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bodak].Add(attackHelper.BuildData("Death Gaze", string.Empty, "Death", 1.5, "melee", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));

                testCases[CreatureConstants.BombardierBeetle_Giant].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.BombardierBeetle_Giant].Add(attackHelper.BuildData("Acid Spray",
                    damageHelper.BuildEntries("1d4", FeatConstants.Foci.Elements.Acid),
                    string.Empty, 2, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, false, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.BoneDevil_Osyluth].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(attackHelper.BuildData("Sting", damageHelper.BuildEntries("3d4", AttributeConstants.DamageTypes.Piercing), "Poison", 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(attackHelper.BuildData("Fear Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(attackHelper.BuildData("Poison", string.Empty, "Initial damage 1d6 Str, Secondary damage 2d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.BoneDevil_Osyluth].Add(attackHelper.BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Bralani].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Bralani].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Bralani].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bralani].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Bralani].Add(attackHelper.BuildData("Whirlwind blast",
                    damageHelper.BuildEntries("3d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Bugbear].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Bugbear].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Bugbear].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Bulette].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Bulette].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Bulette].Add(attackHelper.BuildData("Leap", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Camel_Bactrian].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Camel_Dromedary].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.CarrionCrawler].Add(attackHelper.BuildData("Tentacle", string.Empty, "Paralysis", 0, "melee", 8, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.CarrionCrawler].Add(attackHelper.BuildData("Bite", "1d4", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.CarrionCrawler].Add(attackHelper.BuildData("Paralysis", string.Empty, "paralyzed for 2d4 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Cat].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cat].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Centaur].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Centaur].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Centaur].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Centaur].Add(attackHelper.BuildData("Hoof",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Centipede_Monstrous_Tiny].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Centipede_Monstrous_Tiny].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Centipede_Monstrous_Small].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Centipede_Monstrous_Small].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d2 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Centipede_Monstrous_Medium].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Centipede_Monstrous_Medium].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d3 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Centipede_Monstrous_Large].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Centipede_Monstrous_Large].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d4 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Centipede_Monstrous_Huge].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Centipede_Monstrous_Huge].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Centipede_Monstrous_Gargantuan].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Centipede_Monstrous_Gargantuan].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d8 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Centipede_Monstrous_Colossal].Add(attackHelper.BuildData("Bite", $"4d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Centipede_Monstrous_Colossal].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 2d6 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Centipede_Swarm].Add(attackHelper.BuildData("Swarm", $"2d6", "Poison", 0, "swarm", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Centipede_Swarm].Add(attackHelper.BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Centipede_Swarm].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d4 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.ChainDevil_Kyton].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.ChainDevil_Kyton].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.ChainDevil_Kyton].Add(attackHelper.BuildData("Dancing Chains", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.ChainDevil_Kyton].Add(attackHelper.BuildData("Unnerving Gaze", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));

                testCases[CreatureConstants.ChaosBeast].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Slashing), "Corporeal Instability", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.ChaosBeast].Add(attackHelper.BuildData("Corporeal Instability", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Cheetah].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Cheetah].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cheetah].Add(attackHelper.BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Chimera_Black].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Black].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Black].Add(attackHelper.BuildData("Gore", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Black].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Chimera_Black].Add(attackHelper.BuildData("Breath weapon", $"3d8 acid", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Chimera_Blue].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Blue].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Blue].Add(attackHelper.BuildData("Gore", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Blue].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Chimera_Blue].Add(attackHelper.BuildData("Breath weapon", $"3d8 electricity", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Chimera_Green].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Green].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Green].Add(attackHelper.BuildData("Gore", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Green].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Chimera_Green].Add(attackHelper.BuildData("Breath weapon", $"3d8 acid", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Chimera_Red].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Red].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Red].Add(attackHelper.BuildData("Gore", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_Red].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Chimera_Red].Add(attackHelper.BuildData("Breath weapon", $"3d8 fire", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Chimera_White].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_White].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_White].Add(attackHelper.BuildData("Gore", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chimera_White].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Chimera_White].Add(attackHelper.BuildData("Breath weapon", $"3d8 cold", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Choker].Add(attackHelper.BuildData("Tentacle", $"1d3", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Choker].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Choker].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Chuul].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Chuul].Add(attackHelper.BuildData("Constrict", $"3d6", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Chuul].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Chuul].Add(attackHelper.BuildData("Paralytic Tentacles", "1d8", "6 round paralysis", 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Cloaker].Add(attackHelper.BuildData("Tail slap", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cloaker].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cloaker].Add(attackHelper.BuildData("Moan", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma));
                testCases[CreatureConstants.Cloaker].Add(attackHelper.BuildData("Engulf", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Cockatrice].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), "Petrification", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cockatrice].Add(attackHelper.BuildData("Petrification", string.Empty, string.Empty, 1, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Couatl].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Couatl].Add(attackHelper.BuildData("Poison", string.Empty, "Injury, initial damage 2d4 Str, secondary damage 4d4 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, false, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Couatl].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Couatl].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Couatl].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.Psionic, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Couatl].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Couatl].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Criosphinx].Add(attackHelper.BuildData("Gore", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Criosphinx].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Criosphinx].Add(attackHelper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Criosphinx].Add(attackHelper.BuildData("Rake", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Crocodile].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Crocodile].Add(attackHelper.BuildData("Tail slap", $"1d12", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Crocodile].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Crocodile_Giant].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Crocodile_Giant].Add(attackHelper.BuildData("Tail slap", $"1d12", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Crocodile_Giant].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Cryohydra_5Heads].Add(attackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 5, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cryohydra_5Heads].Add(attackHelper.BuildData("Breath weapon", $"3d6 cold per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Cryohydra_6Heads].Add(attackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 6, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cryohydra_6Heads].Add(attackHelper.BuildData("Breath weapon", $"3d6 cold per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Cryohydra_7Heads].Add(attackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 7, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cryohydra_7Heads].Add(attackHelper.BuildData("Breath weapon", $"3d6 cold per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Cryohydra_8Heads].Add(attackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 8, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cryohydra_8Heads].Add(attackHelper.BuildData("Breath weapon", $"3d6 cold per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Cryohydra_9Heads].Add(attackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 9, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cryohydra_9Heads].Add(attackHelper.BuildData("Breath weapon", $"3d6 cold per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Cryohydra_10Heads].Add(attackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 10, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cryohydra_10Heads].Add(attackHelper.BuildData("Breath weapon", $"3d6 cold per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Cryohydra_11Heads].Add(attackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 11, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cryohydra_11Heads].Add(attackHelper.BuildData("Breath weapon", $"3d6 cold per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Cryohydra_12Heads].Add(attackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 12, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Cryohydra_12Heads].Add(attackHelper.BuildData("Breath weapon", $"3d6 cold per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Darkmantle].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Darkmantle].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Darkmantle].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Darkmantle].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Deinonychus].Add(attackHelper.BuildData("Talons", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Deinonychus].Add(attackHelper.BuildData("Foreclaw", $"1d3", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Deinonychus].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d4", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Deinonychus].Add(attackHelper.BuildData("Pounce", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Delver].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), "2d6 acid", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Delver].Add(attackHelper.BuildData("Corrosive Slime", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Delver].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Derro].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Derro].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Derro].Add(attackHelper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Derro].Add(attackHelper.BuildData("Poison use", string.Empty, "Greenblood Oil or Monstrous Spider Venom", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Derro].Add(attackHelper.BuildData("Greenblood Oil", string.Empty, "Injury DC 13, Initial 1 Con, Secondary 1d2 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Derro].Add(attackHelper.BuildData("Monstrous Spider Venom", string.Empty, "Injury DC 12, Initial and secondary 1d4 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Derro].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Derro].Add(attackHelper.BuildData("Sneak Attack", $"1d6", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Derro_Sane].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Derro_Sane].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Derro_Sane].Add(attackHelper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Derro_Sane].Add(attackHelper.BuildData("Poison use", string.Empty, "Greenblood Oil or Monstrous Spider Venom", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Derro_Sane].Add(attackHelper.BuildData("Greenblood Oil", string.Empty, "Injury DC 13, Initial 1 Con, Secondary 1d2 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Derro_Sane].Add(attackHelper.BuildData("Monstrous Spider Venom", string.Empty, "Injury DC 12, Initial and secondary 1d4 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Derro_Sane].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Derro_Sane].Add(attackHelper.BuildData("Sneak Attack", $"1d6", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Destrachan].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Destrachan].Add(attackHelper.BuildData("Destructive harmonics", string.Empty, string.Empty, 1, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Charisma));

                testCases[CreatureConstants.Devourer].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Devourer].Add(attackHelper.BuildData("Energy Drain", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Charisma));
                testCases[CreatureConstants.Devourer].Add(attackHelper.BuildData("Trap Essence", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Devourer].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Digester].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Digester].Add(attackHelper.BuildData("Acid Spray", string.Empty, string.Empty, 0, "extraordinary ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.DisplacerBeast].Add(attackHelper.BuildData("Tentacle", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.DisplacerBeast].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.DisplacerBeast_PackLord].Add(attackHelper.BuildData("Tentacle", $"1d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.DisplacerBeast_PackLord].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Djinni].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Djinni].Add(attackHelper.BuildData("Air mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Djinni].Add(attackHelper.BuildData("Whirlwind", string.Empty, string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex, saveDcBonus: 3));
                testCases[CreatureConstants.Djinni].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Djinni_Noble].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Djinni_Noble].Add(attackHelper.BuildData("Air mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Djinni_Noble].Add(attackHelper.BuildData("Whirlwind", string.Empty, string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex, saveDcBonus: 3));
                testCases[CreatureConstants.Djinni_Noble].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Dog].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Dog_Riding].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Donkey].Add(attackHelper.BuildData("Bite", $"1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Doppelganger].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Doppelganger].Add(attackHelper.BuildData("Detect Thoughts", string.Empty, string.Empty, 1, "supernatural ability", 0, FeatConstants.Frequencies.Constant, false, true, true, true));
                testCases[CreatureConstants.Doppelganger].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.DragonTurtle].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.DragonTurtle].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.DragonTurtle].Add(attackHelper.BuildData("Breath Weapon", "12d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
                testCases[CreatureConstants.DragonTurtle].Add(attackHelper.BuildData("Capsize", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //Tiny
                testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Wyrmling].Add(attackHelper.BuildData("Breath Weapon", $"2d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //small
                testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_VeryYoung].Add(attackHelper.BuildData("Breath Weapon", $"4d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //medium
                testCases[CreatureConstants.Dragon_Black_Young].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_Young].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Young].Add(attackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Young].Add(attackHelper.BuildData("Breath Weapon", $"6d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //medium
                testCases[CreatureConstants.Dragon_Black_Juvenile].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_Juvenile].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Juvenile].Add(attackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Juvenile].Add(attackHelper.BuildData("Breath Weapon", $"8d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Juvenile].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(attackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(attackHelper.BuildData("Breath Weapon", $"10d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_YoungAdult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //large
                testCases[CreatureConstants.Dragon_Black_Adult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(attackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(attackHelper.BuildData("Breath Weapon", $"12d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_Adult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(attackHelper.BuildData("Breath Weapon", $"14d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_MatureAdult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Black_Old].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_Old].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Old].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Old].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Old].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Old].Add(attackHelper.BuildData("Breath Weapon", $"16d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Old].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_Old].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_Old].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(attackHelper.BuildData("Breath Weapon", $"18d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_VeryOld].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(attackHelper.BuildData("Breath Weapon", $"20d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_Ancient].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(attackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(attackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(attackHelper.BuildData("Breath Weapon", $"22d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_Wyrm].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(attackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(attackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(attackHelper.BuildData("Breath Weapon", $"24d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Black_GreatWyrm].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //small
                testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Wyrmling].Add(attackHelper.BuildData("Breath Weapon", $"2d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //medium
                testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(attackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_VeryYoung].Add(attackHelper.BuildData("Breath Weapon", $"4d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //medium
                testCases[CreatureConstants.Dragon_Blue_Young].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_Young].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Young].Add(attackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Young].Add(attackHelper.BuildData("Breath Weapon", $"6d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //large
                testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(attackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(attackHelper.BuildData("Breath Weapon", $"8d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Juvenile].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(attackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(attackHelper.BuildData("Breath Weapon", $"10d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_YoungAdult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(attackHelper.BuildData("Breath Weapon", $"12d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_Adult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(attackHelper.BuildData("Breath Weapon", $"14d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_MatureAdult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Blue_Old].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(attackHelper.BuildData("Breath Weapon", $"16d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_Old].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(attackHelper.BuildData("Breath Weapon", $"18d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_VeryOld].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(attackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(attackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(attackHelper.BuildData("Breath Weapon", $"20d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_Ancient].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(attackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(attackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(attackHelper.BuildData("Breath Weapon", $"22d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_Wyrm].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(attackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(attackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(attackHelper.BuildData("Breath Weapon", $"24d8 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Blue_GreatWyrm].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //small
                testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Wyrmling].Add(attackHelper.BuildData("Breath Weapon", $"2d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //medium
                testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(attackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_VeryYoung].Add(attackHelper.BuildData("Breath Weapon", $"4d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //medium
                testCases[CreatureConstants.Dragon_Green_Young].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_Young].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Young].Add(attackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Young].Add(attackHelper.BuildData("Breath Weapon", $"6d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //large
                testCases[CreatureConstants.Dragon_Green_Juvenile].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_Juvenile].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Juvenile].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Juvenile].Add(attackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Juvenile].Add(attackHelper.BuildData("Breath Weapon", $"8d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Juvenile].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(attackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(attackHelper.BuildData("Breath Weapon", $"10d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_YoungAdult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Green_Adult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(attackHelper.BuildData("Breath Weapon", $"12d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_Adult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(attackHelper.BuildData("Breath Weapon", $"14d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_MatureAdult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Green_Old].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_Old].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Old].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Old].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Old].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Old].Add(attackHelper.BuildData("Breath Weapon", $"16d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Old].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_Old].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_Old].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(attackHelper.BuildData("Breath Weapon", $"18d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_VeryOld].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(attackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(attackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(attackHelper.BuildData("Breath Weapon", $"20d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_Ancient].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(attackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(attackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(attackHelper.BuildData("Breath Weapon", $"22d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_Wyrm].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(attackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(attackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(attackHelper.BuildData("Breath Weapon", $"24d6 gas (acid)", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Green_GreatWyrm].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //medium
                testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(attackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Wyrmling].Add(attackHelper.BuildData("Breath Weapon", $"2d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //large
                testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(attackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_VeryYoung].Add(attackHelper.BuildData("Breath Weapon", $"4d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //large
                testCases[CreatureConstants.Dragon_Red_Young].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_Young].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Young].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Young].Add(attackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Young].Add(attackHelper.BuildData("Breath Weapon", $"6d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Young].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(attackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(attackHelper.BuildData("Breath Weapon", $"8d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Juvenile].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //huge
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(attackHelper.BuildData("Breath Weapon", $"10d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_YoungAdult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Red_Adult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(attackHelper.BuildData("Breath Weapon", $"12d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Adult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(attackHelper.BuildData("Breath Weapon", $"14d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_MatureAdult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Red_Old].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_Old].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Old].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Old].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Old].Add(attackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Old].Add(attackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Old].Add(attackHelper.BuildData("Breath Weapon", $"16d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Old].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Old].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Old].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(attackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(attackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(attackHelper.BuildData("Breath Weapon", $"18d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_VeryOld].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(attackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(attackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(attackHelper.BuildData("Breath Weapon", $"20d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Ancient].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(attackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(attackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(attackHelper.BuildData("Breath Weapon", $"22d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_Wyrm].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //colossal
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(attackHelper.BuildData("Bite", $"4d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(attackHelper.BuildData("Claw", $"4d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(attackHelper.BuildData("Tail Slap", $"4d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(attackHelper.BuildData("Crush", $"4d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(attackHelper.BuildData("Tail Sweep", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(attackHelper.BuildData("Breath Weapon", $"24d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Red_GreatWyrm].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //tiny
                testCases[CreatureConstants.Dragon_White_Wyrmling].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_Wyrmling].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Wyrmling].Add(attackHelper.BuildData("Breath Weapon", $"1d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //small
                testCases[CreatureConstants.Dragon_White_VeryYoung].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_VeryYoung].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_VeryYoung].Add(attackHelper.BuildData("Breath Weapon", $"2d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //medium
                testCases[CreatureConstants.Dragon_White_Young].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_Young].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Young].Add(attackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Young].Add(attackHelper.BuildData("Breath Weapon", $"3d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                //medium
                testCases[CreatureConstants.Dragon_White_Juvenile].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_Juvenile].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Juvenile].Add(attackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Juvenile].Add(attackHelper.BuildData("Breath Weapon", $"4d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Juvenile].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(attackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(attackHelper.BuildData("Breath Weapon", $"5d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_YoungAdult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //large
                testCases[CreatureConstants.Dragon_White_Adult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_Adult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Adult].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Adult].Add(attackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Adult].Add(attackHelper.BuildData("Breath Weapon", $"6d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Adult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_Adult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_Adult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(attackHelper.BuildData("Breath Weapon", $"7d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_MatureAdult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_White_Old].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_Old].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Old].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Old].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Old].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Old].Add(attackHelper.BuildData("Breath Weapon", $"8d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Old].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_Old].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_Old].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(attackHelper.BuildData("Breath Weapon", $"9d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_VeryOld].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_White_Ancient].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(attackHelper.BuildData("Breath Weapon", $"10d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_Ancient].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(attackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(attackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(attackHelper.BuildData("Breath Weapon", $"11d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_Wyrm].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(attackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(attackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(attackHelper.BuildData("Breath Weapon", $"12d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_White_GreatWyrm].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //tiny
                testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(attackHelper.BuildData("Breath Weapon (fire)", $"1d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(attackHelper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+1 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_Wyrmling].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //small
                testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(attackHelper.BuildData("Breath Weapon (fire)", $"2d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(attackHelper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+2 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_VeryYoung].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //medium
                testCases[CreatureConstants.Dragon_Brass_Young].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_Young].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Young].Add(attackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Young].Add(attackHelper.BuildData("Breath Weapon (fire)", $"3d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Young].Add(attackHelper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+3 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_Young].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Young].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //medium
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(attackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(attackHelper.BuildData("Breath Weapon (fire)", $"4d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(attackHelper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+4 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Juvenile].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(attackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(attackHelper.BuildData("Breath Weapon (fire)", $"5d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(attackHelper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+5 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_YoungAdult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //large
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(attackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(attackHelper.BuildData("Breath Weapon (fire)", $"6d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(attackHelper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+6 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Adult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(attackHelper.BuildData("Breath Weapon (fire)", $"7d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(attackHelper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+7 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_MatureAdult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Brass_Old].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(attackHelper.BuildData("Breath Weapon (fire)", $"8d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(attackHelper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+8 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Old].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(attackHelper.BuildData("Breath Weapon (fire)", $"9d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(attackHelper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+9 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_VeryOld].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(attackHelper.BuildData("Breath Weapon (fire)", $"10d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(attackHelper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+10 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Ancient].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(attackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(attackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(attackHelper.BuildData("Breath Weapon (fire)", $"11d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(attackHelper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+11 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_Wyrm].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(attackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(attackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(attackHelper.BuildData("Breath Weapon (fire)", $"12d6 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(attackHelper.BuildData("Breath Weapon (sleep)", string.Empty, "Sleep for 1d6+12 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Brass_GreatWyrm].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //small
                testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(attackHelper.BuildData("Breath Weapon (electricity)", $"2d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(attackHelper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+1 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_Wyrmling].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //medium
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(attackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(attackHelper.BuildData("Breath Weapon (electricity)", $"4d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(attackHelper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+2 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_VeryYoung].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //medium
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(attackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(attackHelper.BuildData("Breath Weapon (electricity)", $"6d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(attackHelper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+3 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Young].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(attackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(attackHelper.BuildData("Breath Weapon (electricity)", $"8d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(attackHelper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+4 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Juvenile].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(attackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(attackHelper.BuildData("Breath Weapon (electricity)", $"10d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(attackHelper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+5 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_YoungAdult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(attackHelper.BuildData("Breath Weapon (electricity)", $"12d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(attackHelper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+6 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Adult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(attackHelper.BuildData("Breath Weapon (electricity)", $"14d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(attackHelper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+7 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_MatureAdult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(attackHelper.BuildData("Breath Weapon (electricity)", $"16d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(attackHelper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+8 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Old].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(attackHelper.BuildData("Breath Weapon (electricity)", $"18d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(attackHelper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+9 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_VeryOld].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(attackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(attackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(attackHelper.BuildData("Breath Weapon (electricity)", $"20d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(attackHelper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+10 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Ancient].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(attackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(attackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(attackHelper.BuildData("Breath Weapon (electricity)", $"22d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(attackHelper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+11 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_Wyrm].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(attackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(attackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(attackHelper.BuildData("Breath Weapon (electricity)", $"24d6 electricity", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(attackHelper.BuildData("Breath Weapon (repulsion gas)", string.Empty, "Compelled for 1d6+12 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Bronze_GreatWyrm].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //tiny
                testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(attackHelper.BuildData("Breath Weapon (acid)", $"2d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Wyrmling].Add(attackHelper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+1 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                //small
                testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(attackHelper.BuildData("Breath Weapon (acid)", $"4d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_VeryYoung].Add(attackHelper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+2 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                //medium
                testCases[CreatureConstants.Dragon_Copper_Young].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_Young].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Young].Add(attackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Young].Add(attackHelper.BuildData("Breath Weapon (acid)", $"6d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Young].Add(attackHelper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+3 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_Young].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //medium
                testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(attackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(attackHelper.BuildData("Breath Weapon (acid)", $"8d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(attackHelper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+4 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_Juvenile].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(attackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(attackHelper.BuildData("Breath Weapon (acid)", $"10d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(attackHelper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+5 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_YoungAdult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(attackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(attackHelper.BuildData("Breath Weapon (acid)", $"12d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(attackHelper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+6 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_Adult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(attackHelper.BuildData("Breath Weapon (acid)", $"14d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(attackHelper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+7 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_MatureAdult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Copper_Old].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(attackHelper.BuildData("Breath Weapon (acid)", $"16d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(attackHelper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+8 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_Old].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(attackHelper.BuildData("Breath Weapon (acid)", $"18d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(attackHelper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+9 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_VeryOld].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(attackHelper.BuildData("Breath Weapon (acid)", $"20d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(attackHelper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+10 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_Ancient].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(attackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(attackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(attackHelper.BuildData("Breath Weapon (acid)", $"22d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(attackHelper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+11 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_Wyrm].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(attackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(attackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(attackHelper.BuildData("Breath Weapon (acid)", $"24d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(attackHelper.BuildData("Breath Weapon (slow gas)", string.Empty, "Slowed for 1d6+12 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Copper_GreatWyrm].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //medium
                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(attackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(attackHelper.BuildData("Breath Weapon (fire)", $"2d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Wyrmling].Add(attackHelper.BuildData("Breath Weapon (weakening gas)", string.Empty, "1 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                //large
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(attackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(attackHelper.BuildData("Breath Weapon (fire)", $"4d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_VeryYoung].Add(attackHelper.BuildData("Breath Weapon (weakening gas)", string.Empty, "2 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                //large
                testCases[CreatureConstants.Dragon_Gold_Young].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(attackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(attackHelper.BuildData("Breath Weapon (fire)", $"6d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(attackHelper.BuildData("Breath Weapon (weakening gas)", string.Empty, "3 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_Young].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(attackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(attackHelper.BuildData("Breath Weapon (fire)", $"8d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(attackHelper.BuildData("Breath Weapon (weakening gas)", string.Empty, "4 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Juvenile].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //huge
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(attackHelper.BuildData("Breath Weapon (fire)", $"10d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(attackHelper.BuildData("Breath Weapon (weakening gas)", string.Empty, "5 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_YoungAdult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(attackHelper.BuildData("Breath Weapon (fire)", $"12d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(attackHelper.BuildData("Breath Weapon (weakening gas)", string.Empty, "6 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Adult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(attackHelper.BuildData("Breath Weapon (fire)", $"14d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(attackHelper.BuildData("Breath Weapon (weakening gas)", string.Empty, "7 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_MatureAdult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Gold_Old].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(attackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(attackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(attackHelper.BuildData("Breath Weapon (fire)", $"16d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(attackHelper.BuildData("Breath Weapon (weakening gas)", string.Empty, "8 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Old].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(attackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(attackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(attackHelper.BuildData("Breath Weapon (fire)", $"18d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(attackHelper.BuildData("Breath Weapon (weakening gas)", string.Empty, "9 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_VeryOld].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(attackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(attackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(attackHelper.BuildData("Breath Weapon (fire)", $"20d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(attackHelper.BuildData("Breath Weapon (weakening gas)", string.Empty, "10 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Ancient].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //colossal
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(attackHelper.BuildData("Bite", $"4d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(attackHelper.BuildData("Claw", $"4d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(attackHelper.BuildData("Tail Slap", $"4d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(attackHelper.BuildData("Crush", $"4d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(attackHelper.BuildData("Tail Sweep", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(attackHelper.BuildData("Breath Weapon (fire)", $"22d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(attackHelper.BuildData("Breath Weapon (weakening gas)", string.Empty, "11 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_Wyrm].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //colossal
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(attackHelper.BuildData("Bite", $"4d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(attackHelper.BuildData("Claw", $"4d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(attackHelper.BuildData("Tail Slap", $"4d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(attackHelper.BuildData("Crush", $"4d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(attackHelper.BuildData("Tail Sweep", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(attackHelper.BuildData("Breath Weapon (fire)", $"24d10 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(attackHelper.BuildData("Breath Weapon (weakening gas)", string.Empty, "12 Strength", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Gold_GreatWyrm].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //small
                testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(attackHelper.BuildData("Breath Weapon (cold)", $"2d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Wyrmling].Add(attackHelper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+1 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                //medium
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(attackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(attackHelper.BuildData("Breath Weapon (cold)", $"4d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_VeryYoung].Add(attackHelper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+2 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                //medium
                testCases[CreatureConstants.Dragon_Silver_Young].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(attackHelper.BuildData("Wing", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(attackHelper.BuildData("Breath Weapon (cold)", $"6d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(attackHelper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+3 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_Young].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(attackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(attackHelper.BuildData("Breath Weapon (cold)", $"8d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(attackHelper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+4 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Juvenile].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                //large
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(attackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(attackHelper.BuildData("Breath Weapon (cold)", $"10d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(attackHelper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+5 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_YoungAdult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(attackHelper.BuildData("Breath Weapon (cold)", $"12d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(attackHelper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+6 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Adult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(attackHelper.BuildData("Breath Weapon (cold)", $"14d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(attackHelper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+7 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_MatureAdult].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Silver_Old].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(attackHelper.BuildData("Breath Weapon (cold)", $"16d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(attackHelper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+8 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Old].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //huge
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(attackHelper.BuildData("Tail Slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(attackHelper.BuildData("Crush", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(attackHelper.BuildData("Breath Weapon (cold)", $"18d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(attackHelper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+9 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_VeryOld].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(attackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(attackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(attackHelper.BuildData("Breath Weapon (cold)", $"20d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(attackHelper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+10 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Ancient].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //gargantuan
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(attackHelper.BuildData("Crush", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(attackHelper.BuildData("Tail Sweep", $"2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(attackHelper.BuildData("Breath Weapon (cold)", $"22d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(attackHelper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+11 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_Wyrm].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                //colossal
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(attackHelper.BuildData("Bite", $"4d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(attackHelper.BuildData("Claw", $"4d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(attackHelper.BuildData("Tail Slap", $"4d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(attackHelper.BuildData("Crush", $"4d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(attackHelper.BuildData("Tail Sweep", $"2d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(attackHelper.BuildData("Breath Weapon (cold)", $"24d8 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(attackHelper.BuildData("Breath Weapon (paralyzing gas)", string.Empty, "Paralyzed for 1d6+12 rounds", 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Dragon_Silver_GreatWyrm].Add(attackHelper.BuildData("Frightful Presence", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.Dragonne].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dragonne].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dragonne].Add(attackHelper.BuildData("Pounce", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Dragonne].Add(attackHelper.BuildData("Roar", string.Empty, string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));

                testCases[CreatureConstants.Dretch].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dretch].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Dretch].Add(attackHelper.BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.Dretch].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Drider].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Drider].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Drider].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Drider].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Drider].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Drider].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Dryad].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Dryad].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Dryad].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dryad].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Dwarf_Deep].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Dwarf_Deep].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Dwarf_Deep].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Dwarf_Duergar].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Dwarf_Duergar].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Dwarf_Duergar].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Dwarf_Duergar].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Dwarf_Hill].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Dwarf_Hill].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Dwarf_Hill].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Dwarf_Mountain].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Dwarf_Mountain].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Dwarf_Mountain].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Eagle].Add(attackHelper.BuildData("Talons", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Eagle].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Eagle_Giant].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Eagle_Giant].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Efreeti].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), "Heat", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Efreeti].Add(attackHelper.BuildData("Change Size", string.Empty, string.Empty, 0, "spell-like ability", 2, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Efreeti].Add(attackHelper.BuildData("Heat", "1d6 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, false, true, true, true));
                testCases[CreatureConstants.Efreeti].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Elasmosaurus].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Elemental_Air_Small].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Air_Small].Add(attackHelper.BuildData("Air mastery",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Air_Small].Add(attackHelper.BuildData("Whirlwind",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Air_Medium].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Air_Medium].Add(attackHelper.BuildData("Air mastery",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Air_Medium].Add(attackHelper.BuildData("Whirlwind",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Air_Large].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Air_Large].Add(attackHelper.BuildData("Air mastery",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Air_Large].Add(attackHelper.BuildData("Whirlwind",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Air_Huge].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Air_Huge].Add(attackHelper.BuildData("Air mastery",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Air_Huge].Add(attackHelper.BuildData("Whirlwind",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Air_Greater].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Air_Greater].Add(attackHelper.BuildData("Air mastery",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Air_Greater].Add(attackHelper.BuildData("Whirlwind",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Air_Elder].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Air_Elder].Add(attackHelper.BuildData("Air mastery",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Air_Elder].Add(attackHelper.BuildData("Whirlwind",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Earth_Small].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Earth_Small].Add(attackHelper.BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Earth_Small].Add(attackHelper.BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Elemental_Earth_Medium].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Earth_Medium].Add(attackHelper.BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Earth_Medium].Add(attackHelper.BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Elemental_Earth_Large].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Earth_Large].Add(attackHelper.BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Earth_Large].Add(attackHelper.BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Elemental_Earth_Huge].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d10", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Earth_Huge].Add(attackHelper.BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Earth_Huge].Add(attackHelper.BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Elemental_Earth_Greater].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d10", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Earth_Greater].Add(attackHelper.BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Earth_Greater].Add(attackHelper.BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Elemental_Earth_Elder].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d10", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Earth_Elder].Add(attackHelper.BuildData("Earth mastery", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Earth_Elder].Add(attackHelper.BuildData("Push", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Elemental_Fire_Small].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                    "Burn", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Fire_Small].Add(attackHelper.BuildData("Burn",
                    damageHelper.BuildEntries("1d4", FeatConstants.Foci.Elements.Fire),
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Fire_Medium].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                    "Burn", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Fire_Medium].Add(attackHelper.BuildData("Burn",
                    damageHelper.BuildEntries("1d6", FeatConstants.Foci.Elements.Fire),
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Fire_Large].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                    "Burn", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Fire_Large].Add(attackHelper.BuildData("Burn",
                    damageHelper.BuildEntries("2d6", FeatConstants.Foci.Elements.Fire),
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Fire_Huge].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    "Burn", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Fire_Huge].Add(attackHelper.BuildData("Burn",
                    damageHelper.BuildEntries("2d8", FeatConstants.Foci.Elements.Fire),
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Fire_Greater].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    "Burn", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Fire_Greater].Add(attackHelper.BuildData("Burn",
                    damageHelper.BuildEntries("2d8", FeatConstants.Foci.Elements.Fire),
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Fire_Elder].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    "Burn", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Fire_Elder].Add(attackHelper.BuildData("Burn",
                    damageHelper.BuildEntries("2d8", FeatConstants.Foci.Elements.Fire),
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Water_Small].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Water_Small].Add(attackHelper.BuildData("Water mastery",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Small].Add(attackHelper.BuildData("Drench",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Small].Add(attackHelper.BuildData("Vortex",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Water_Medium].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Water_Medium].Add(attackHelper.BuildData("Water mastery",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Medium].Add(attackHelper.BuildData("Drench",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Medium].Add(attackHelper.BuildData("Vortex",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Water_Large].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Water_Large].Add(attackHelper.BuildData("Water mastery",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Large].Add(attackHelper.BuildData("Drench",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Large].Add(attackHelper.BuildData("Vortex",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Water_Huge].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d10", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Water_Huge].Add(attackHelper.BuildData("Water mastery",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Huge].Add(attackHelper.BuildData("Drench",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Huge].Add(attackHelper.BuildData("Vortex",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Water_Greater].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d10", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Water_Greater].Add(attackHelper.BuildData("Water mastery",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Greater].Add(attackHelper.BuildData("Drench",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Greater].Add(attackHelper.BuildData("Vortex",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elemental_Water_Elder].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("2d10", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elemental_Water_Elder].Add(attackHelper.BuildData("Water mastery",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Elder].Add(attackHelper.BuildData("Drench",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Elemental_Water_Elder].Add(attackHelper.BuildData("Vortex",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 0, "supernatural ability", 1, $"10 {FeatConstants.Frequencies.Minute}", false, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elephant].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elephant].Add(attackHelper.BuildData("Stamp", $"2d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Elephant].Add(attackHelper.BuildData("Gore", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elephant].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Elf_Aquatic].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Elf_Aquatic].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Elf_Aquatic].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Elf_Drow].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Elf_Drow].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Elf_Drow].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Elf_Drow].Add(attackHelper.BuildData("Poison", string.Empty, "DC 13 Fort, Initial 1 minute unconscious, Secondary 2d4 hours unconscious", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
                testCases[CreatureConstants.Elf_Drow].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Elf_Gray].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Elf_Gray].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Elf_Gray].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Elf_Half].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Elf_Half].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Elf_Half].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Elf_High].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Elf_High].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Elf_High].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Elf_Wild].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Elf_Wild].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Elf_Wild].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Elf_Wood].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Elf_Wood].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Elf_Wood].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Erinyes].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Erinyes].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Erinyes].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Erinyes].Add(attackHelper.BuildData("Rope", string.Empty, "Entangle", 0, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Erinyes].Add(attackHelper.BuildData("Entangle", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Erinyes].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Erinyes].Add(attackHelper.BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.EtherealFilcher].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.EtherealFilcher].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.EtherealMarauder].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.EtherealMarauder].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Ettercap].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ettercap].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Ettercap].Add(attackHelper.BuildData("Poison", string.Empty, "Initial damage 1d6 Dex, secondary damage 2d6 Dex", 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 2));
                testCases[CreatureConstants.Ettercap].Add(attackHelper.BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, true, true, false, true, saveAbility: AbilityConstants.Constitution));

                testCases[CreatureConstants.Ettin].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 2, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Ettin].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 2, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Ettin].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.FireBeetle_Giant].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d4", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.FormianWorker].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.FormianWorker].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.FormianWarrior].Add(attackHelper.BuildData("Sting", $"2d4", "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.FormianWarrior].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.FormianWarrior].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.FormianWarrior].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.FormianTaskmaster].Add(attackHelper.BuildData("Sting", $"2d4", "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.FormianTaskmaster].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.FormianTaskmaster].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.FormianTaskmaster].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.FormianTaskmaster].Add(attackHelper.BuildData("Dominated creature", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.FormianMyrmarch].Add(attackHelper.BuildData("Sting",
                    damageHelper.BuildEntries("2d4", AttributeConstants.DamageTypes.Piercing),
                    "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.FormianMyrmarch].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.FormianMyrmarch].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.FormianMyrmarch].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.FormianMyrmarch].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.FormianQueen].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.FormianQueen].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.FrostWorm].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), "Cold", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.FrostWorm].Add(attackHelper.BuildData("Trill", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.FrostWorm].Add(attackHelper.BuildData("Cold",
                    damageHelper.BuildEntries("1d8", FeatConstants.Foci.Elements.Cold),
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.FrostWorm].Add(attackHelper.BuildData("Breath weapon",
                    damageHelper.BuildEntries("15d6", FeatConstants.Foci.Elements.Cold),
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Gargoyle].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Gargoyle].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Gargoyle].Add(attackHelper.BuildData("Gore", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Gargoyle_Kapoacinth].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Gargoyle_Kapoacinth].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Gargoyle_Kapoacinth].Add(attackHelper.BuildData("Gore", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.GelatinousCube].Add(attackHelper.BuildData("Slam",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning, "1d4", FeatConstants.Foci.Elements.Acid),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.GelatinousCube].Add(attackHelper.BuildData("Acid", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.GelatinousCube].Add(attackHelper.BuildData("Engulf", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, SaveConstants.Reflex, AbilityConstants.Strength, 1));
                testCases[CreatureConstants.GelatinousCube].Add(attackHelper.BuildData("Paralysis", string.Empty, "3d6 rounds of paralysis", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Ghaele].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Ghaele].Add(attackHelper.BuildData("Light Ray",
                    damageHelper.BuildEntries("2d12"),
                    string.Empty, 0, "ranged touch", 2, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Ghaele].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Ghaele].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Ghaele].Add(attackHelper.BuildData("Gaze", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.Ghoul].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), "Disease, Paralysis", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ghoul].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Slashing), "Paralysis", 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Ghoul].Add(attackHelper.BuildData("Disease", string.Empty, "Ghoul Fever", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Ghoul].Add(attackHelper.BuildData("Ghoul Fever", string.Empty, "incubation period 1 day, damage 1d3 Con and 1d3 Dex", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Ghoul].Add(attackHelper.BuildData("Paralysis", string.Empty, "1d4+1 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Ghoul_Ghast].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), "Disease, Paralysis", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ghoul_Ghast].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Slashing), "Paralysis", 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Ghoul_Ghast].Add(attackHelper.BuildData("Disease", string.Empty, "Ghoul Fever", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Ghoul_Ghast].Add(attackHelper.BuildData("Ghoul Fever", string.Empty, "incubation period 1 day, damage 1d3 Con and 1d3 Dex", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Ghoul_Ghast].Add(attackHelper.BuildData("Paralysis", string.Empty, "1d4+1 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Ghoul_Ghast].Add(attackHelper.BuildData("Stench", string.Empty, "1d6+4 rounds sickened", 0, "melee", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Ghoul_Lacedon].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), "Disease, Paralysis", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ghoul_Lacedon].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Slashing), "Paralysis", 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Ghoul_Lacedon].Add(attackHelper.BuildData("Disease", string.Empty, "Ghoul Fever", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Ghoul_Lacedon].Add(attackHelper.BuildData("Ghoul Fever", string.Empty, "incubation period 1 day, damage 1d3 Con and 1d3 Dex", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Ghoul_Lacedon].Add(attackHelper.BuildData("Paralysis", string.Empty, "1d4+1 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Giant_Cloud].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Giant_Cloud].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Giant_Cloud].Add(attackHelper.BuildData("Rock", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Giant_Cloud].Add(attackHelper.BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Giant_Cloud].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Giant_Fire].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Giant_Fire].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Giant_Fire].Add(attackHelper.BuildData("Rock", $"2d6", string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Giant_Fire].Add(attackHelper.BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Giant_Frost].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Giant_Frost].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Giant_Frost].Add(attackHelper.BuildData("Rock", $"2d6", string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Giant_Frost].Add(attackHelper.BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Giant_Hill].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Giant_Hill].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Giant_Hill].Add(attackHelper.BuildData("Rock", $"2d6", string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Giant_Hill].Add(attackHelper.BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Giant_Stone].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Giant_Stone].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Giant_Stone].Add(attackHelper.BuildData("Rock", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Giant_Stone].Add(attackHelper.BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Giant_Stone_Elder].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Giant_Stone_Elder].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Giant_Stone_Elder].Add(attackHelper.BuildData("Rock", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Giant_Stone_Elder].Add(attackHelper.BuildData("Rock Throwing", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Giant_Stone_Elder].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Giant_Storm].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Giant_Storm].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Giant_Storm].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Giant_Storm].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.GibberingMouther].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("1", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 1, "melee", 6, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.GibberingMouther].Add(attackHelper.BuildData("Spittle",
                    damageHelper.BuildEntries("1d4", FeatConstants.Foci.Elements.Acid),
                    "Blindness", 0, "ranged touch", 1, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.GibberingMouther].Add(attackHelper.BuildData("Blindness",
                    string.Empty,
                    "1d4 rounds blinded", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.GibberingMouther].Add(attackHelper.BuildData("Gibbering",
                    string.Empty,
                    "1d2 rounds Confusion", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.GibberingMouther].Add(attackHelper.BuildData("Improved Grab",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.GibberingMouther].Add(attackHelper.BuildData("Swallow Whole",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.GibberingMouther].Add(attackHelper.BuildData("Blood Drain",
                    damageHelper.BuildEntries("1d4", AbilityConstants.Constitution),
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.GibberingMouther].Add(attackHelper.BuildData("Ground Manipulation",
                    string.Empty,
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Girallon].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Girallon].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Girallon].Add(attackHelper.BuildData("Rend", $"2d4", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Githyanki].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Githyanki].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Githyanki].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Githyanki].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.Psionic, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Githzerai].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Githzerai].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Githzerai].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Githzerai].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.Psionic, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Glabrezu].Add(attackHelper.BuildData("Pincer", $"2d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Glabrezu].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Glabrezu].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Glabrezu].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Glabrezu].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Glabrezu].Add(attackHelper.BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Gnoll].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Gnoll].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Gnoll].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Gnome_Forest].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Gnome_Forest].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Gnome_Forest].Add(attackHelper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Gnome_Forest].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Gnome_Rock].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Gnome_Rock].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Gnome_Rock].Add(attackHelper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Gnome_Rock].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Gnome_Svirfneblin].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Gnome_Svirfneblin].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Gnome_Svirfneblin].Add(attackHelper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Gnome_Svirfneblin].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Goblin].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Goblin].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Goblin].Add(attackHelper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Golem_Clay].Add(attackHelper.BuildData("Slam", $"2d10", "Cursed Wound", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Golem_Clay].Add(attackHelper.BuildData("Berserk", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Golem_Clay].Add(attackHelper.BuildData("Cursed Wound", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
                testCases[CreatureConstants.Golem_Clay].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Golem_Flesh].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Golem_Flesh].Add(attackHelper.BuildData("Berserk", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Golem_Iron].Add(attackHelper.BuildData("Slam", $"2d10", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Golem_Iron].Add(attackHelper.BuildData("Breath weapon", string.Empty, "Poisonous Gas", 0, "supernatural ability", 1, $"1d4+1 {FeatConstants.Frequencies.Round}", false, true, true, true));
                testCases[CreatureConstants.Golem_Iron].Add(attackHelper.BuildData("Poisonous Gas", string.Empty, "Initial damage 1d4 Con, secondary damage 3d4 Con", 0, "melee", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Golem_Stone].Add(attackHelper.BuildData("Slam", $"2d10", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Golem_Stone].Add(attackHelper.BuildData("Slow", string.Empty, string.Empty, 0, "supernatural ability", 1, $"2 {FeatConstants.Frequencies.Round}", true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));

                testCases[CreatureConstants.Golem_Stone_Greater].Add(attackHelper.BuildData("Slam", $"4d8", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Golem_Stone_Greater].Add(attackHelper.BuildData("Slow", string.Empty, string.Empty, 0, "supernatural ability", 1, $"2 {FeatConstants.Frequencies.Round}", true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Will));

                testCases[CreatureConstants.Gorgon].Add(attackHelper.BuildData("Gore", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Gorgon].Add(attackHelper.BuildData("Breath weapon", string.Empty, "Turn to stone", 0, "supernatural ability", 5, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Gorgon].Add(attackHelper.BuildData("Trample", "1d8", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.GrayOoze].Add(attackHelper.BuildData("Slam", $"1d6 + 1d6 acid", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.GrayOoze].Add(attackHelper.BuildData("Acid", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
                testCases[CreatureConstants.GrayOoze].Add(attackHelper.BuildData("Constrict", $"1d6 + 1d6 acid", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.GrayOoze].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.GrayRender].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.GrayRender].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.GrayRender].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.GrayRender].Add(attackHelper.BuildData("Rend", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.GreenHag].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.GreenHag].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.GreenHag].Add(attackHelper.BuildData("Weakness", string.Empty, "2d4 Strength damage", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.GreenHag].Add(attackHelper.BuildData("Mimicry", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Grick].Add(attackHelper.BuildData("Tentacle", $"1d4", string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Grick].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Griffon].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Griffon].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Griffon].Add(attackHelper.BuildData("Pounce", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Griffon].Add(attackHelper.BuildData("Rake", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Grig].Add(attackHelper.BuildData(AttributeConstants.Melee,
                    string.Empty,
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Grig].Add(attackHelper.BuildData(AttributeConstants.Ranged,
                    string.Empty,
                    string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Grig].Add(attackHelper.BuildData("Unarmed Strike",
                    damageHelper.BuildEntries("1", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Grig].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                    string.Empty,
                    string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Grig_WithFiddle].Add(attackHelper.BuildData(AttributeConstants.Melee,
                    string.Empty,
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Grig_WithFiddle].Add(attackHelper.BuildData(AttributeConstants.Ranged,
                    string.Empty,
                    string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Grig_WithFiddle].Add(attackHelper.BuildData("Unarmed Strike",
                    damageHelper.BuildEntries("1", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Grig_WithFiddle].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                    string.Empty,
                    string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Grig_WithFiddle].Add(attackHelper.BuildData("Fiddle",
                    string.Empty,
                    SpellConstants.IrresistibleDance, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.Grimlock].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Grimlock].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Gynosphinx].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Gynosphinx].Add(attackHelper.BuildData("Pounce", string.Empty, string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Gynosphinx].Add(attackHelper.BuildData("Rake", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Gynosphinx].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Halfling_Deep].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Halfling_Deep].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Halfling_Deep].Add(attackHelper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Halfling_Lightfoot].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Halfling_Lightfoot].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Halfling_Lightfoot].Add(attackHelper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Halfling_Tallfellow].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Halfling_Tallfellow].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Halfling_Tallfellow].Add(attackHelper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Harpy].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Harpy].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Harpy].Add(attackHelper.BuildData("Captivating Song", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.Hawk].Add(attackHelper.BuildData("Talons", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.HellHound].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), "Fiery Bite", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.HellHound].Add(attackHelper.BuildData("Fiery Bite", $"1d6 fire", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.HellHound].Add(attackHelper.BuildData("Breath weapon", "2d6 fire", string.Empty, 0, "supernatural ability", 1, $"2d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.HellHound_NessianWarhound].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), "Fiery Bite", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.HellHound_NessianWarhound].Add(attackHelper.BuildData("Fiery Bite", $"1d8 fire", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.HellHound_NessianWarhound].Add(attackHelper.BuildData("Breath weapon", "3d6 fire", string.Empty, 0, "supernatural ability", 1, $"2d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Hellcat_Bezekira].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(attackHelper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Hellcat_Bezekira].Add(attackHelper.BuildData("Rake", $"1d8", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Hellwasp_Swarm].Add(attackHelper.BuildData("Swarm", $"3d6", "poison", 0, "swarm", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Hellwasp_Swarm].Add(attackHelper.BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Hellwasp_Swarm].Add(attackHelper.BuildData("Inhabit", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, true, true, true, true));
                testCases[CreatureConstants.Hellwasp_Swarm].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d4 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));

                testCases[CreatureConstants.Hezrou].Add(attackHelper.BuildData("Bite", $"4d4", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Hezrou].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Hezrou].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Hezrou].Add(attackHelper.BuildData("Stench", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Hezrou].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Hezrou].Add(attackHelper.BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Hieracosphinx].Add(attackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Hieracosphinx].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Hieracosphinx].Add(attackHelper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Hieracosphinx].Add(attackHelper.BuildData("Rake", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Hippogriff].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Hippogriff].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hobgoblin].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Hobgoblin].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Hobgoblin].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Homunculus].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing),
                    "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Homunculus].Add(attackHelper.BuildData("Poison", string.Empty, "Initial damage sleep for 1 minute, secondary damage sleep for another 5d6 minutes", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 2));

                testCases[CreatureConstants.HornedDevil_Cornugon].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(attackHelper.BuildData("Tail",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning),
                    "infernal wound", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(attackHelper.BuildData("Fear Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(attackHelper.BuildData("Infernal Wound",
                    damageHelper.BuildEntries("2"),
                    string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, string.Empty, AbilityConstants.Constitution));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(attackHelper.BuildData("Stun", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Strength));
                testCases[CreatureConstants.HornedDevil_Cornugon].Add(attackHelper.BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Horse_Heavy].Add(attackHelper.BuildData("Hoof",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Horse_Heavy_War].Add(attackHelper.BuildData("Hoof",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Horse_Heavy_War].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Horse_Light].Add(attackHelper.BuildData("Hoof",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Horse_Light_War].Add(attackHelper.BuildData("Hoof",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Horse_Light_War].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.HoundArchon].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.HoundArchon].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.HoundArchon].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.HoundArchon].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Howler].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing),
                    "1d4 Quills", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Howler].Add(attackHelper.BuildData("Quill",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Dexterity, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Howler].Add(attackHelper.BuildData("Howl",
                    damageHelper.BuildEntries("1", AbilityConstants.Wisdom),
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hour, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.Human].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Human].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Human].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hydra_5Heads].Add(attackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 5, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hydra_6Heads].Add(attackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 6, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hydra_7Heads].Add(attackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 7, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hydra_8Heads].Add(attackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 8, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hydra_9Heads].Add(attackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 9, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hydra_10Heads].Add(attackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 10, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hydra_11Heads].Add(attackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 11, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hydra_12Heads].Add(attackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 12, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Hyena].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Hyena].Add(attackHelper.BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.IceDevil_Gelugon].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(attackHelper.BuildData("Claw", $"1d10", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(attackHelper.BuildData("Tail", $"3d6", "slow", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(attackHelper.BuildData("Fear Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(attackHelper.BuildData("Slow", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.IceDevil_Gelugon].Add(attackHelper.BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Imp].Add(attackHelper.BuildData("Sting", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), "poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Imp].Add(attackHelper.BuildData("Poison", string.Empty, $"Initial damage 1d4 Dex, Secondary damage 2d4 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution, 2));
                testCases[CreatureConstants.Imp].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.InvisibleStalker].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Janni].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Janni].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Janni].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Janni].Add(attackHelper.BuildData("Change Size", string.Empty, string.Empty, 0, "spell-like ability", 2, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Janni].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Kobold].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Kobold].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Kobold].Add(attackHelper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Kolyarut].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Kolyarut].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Kolyarut].Add(attackHelper.BuildData("Vampiric Touch",
                    $"5d6",
                    string.Empty, 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Kolyarut].Add(attackHelper.BuildData("Enervation Ray", string.Empty, "1d4 negative levels", 0, "ranged touch", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Kolyarut].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Kraken].Add(attackHelper.BuildData("Tentacle",
                    $"2d8",
                    string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Kraken].Add(attackHelper.BuildData("Arm",
                    $"1d6",
                    string.Empty, 0.5, "melee", 6, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Kraken].Add(attackHelper.BuildData("Bite",
                    $"4d6",
                    string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Kraken].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Kraken].Add(attackHelper.BuildData("Constrict (Tentacle)",
                    $"2d8",
                    string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Kraken].Add(attackHelper.BuildData("Constrict (Arm)",
                    $"1d6",
                    string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Kraken].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Krenshar].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Krenshar].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Krenshar].Add(attackHelper.BuildData("Scare", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Krenshar].Add(attackHelper.BuildData("Scare with Screech", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.KuoToa].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.KuoToa].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.KuoToa].Add(attackHelper.BuildData("Lightning Bolt",
                    damageHelper.BuildEntries("1d6", FeatConstants.Foci.Elements.Electricity, "per Kuo-Toa Cleric"),
                    string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", true, true, true, false, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Lamia].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Lamia].Add(attackHelper.BuildData("Touch", string.Empty, "Wisdom Drain", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Lamia].Add(attackHelper.BuildData("Wisdom Drain", string.Empty, "1d4 Wisdom Drain", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Lamia].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Lamia].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Lammasu].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Lammasu].Add(attackHelper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Lammasu].Add(attackHelper.BuildData("Rake", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Lammasu].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Lammasu].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.LanternArchon].Add(attackHelper.BuildData("Light Ray", $"1d6", string.Empty, 0, "ranged touch", 2, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.LanternArchon].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Lemure].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Leonal].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Leonal].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Leonal].Add(attackHelper.BuildData("Roar", "2d6 sonic", string.Empty, 0, "supernatural ability", 3, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.Leonal].Add(attackHelper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Leonal].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Leonal].Add(attackHelper.BuildData("Rake", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Leonal].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Leopard].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Leopard].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Leopard].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Leopard].Add(attackHelper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Leopard].Add(attackHelper.BuildData("Rake", $"1d3", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Lillend].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Lillend].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Lillend].Add(attackHelper.BuildData("Tail slap", $"2d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Lillend].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Lillend].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Lillend].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Lillend].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Lion].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Lion].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Lion].Add(attackHelper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Lion].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Lion].Add(attackHelper.BuildData("Rake", $"1d4", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Lion_Dire].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Lion_Dire].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Lion_Dire].Add(attackHelper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Lion_Dire].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Lion_Dire].Add(attackHelper.BuildData("Rake", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Lizard].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Lizard_Monitor].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Lizardfolk].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Lizardfolk].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Lizardfolk].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Lizardfolk].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Locathah].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Locathah].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Locathah].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Locust_Swarm].Add(attackHelper.BuildData("Swarm", $"2d6", string.Empty, 0, "swarm", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Locust_Swarm].Add(attackHelper.BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Magmin].Add(attackHelper.BuildData("Burning Touch", $"1d8 fire", "Combustion", 0, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Magmin].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), "Combustion", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Magmin].Add(attackHelper.BuildData("Combustion", $"1d8 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.Magmin].Add(attackHelper.BuildData("Fiery Aura", $"1d6 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.MantaRay].Add(attackHelper.BuildData("Ram", $"1d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Manticore].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Manticore].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Manticore].Add(attackHelper.BuildData("Spikes", string.Empty, "Tail Spikes", 0, "ranged", 6, FeatConstants.Frequencies.Round, false, true, true, false));
                testCases[CreatureConstants.Manticore].Add(attackHelper.BuildData("Tail Spikes", $"1d8", string.Empty, 0.5, "extraordinary ability", 24, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Marilith].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Marilith].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 5, FeatConstants.Frequencies.Round, true, false, false, false));
                testCases[CreatureConstants.Marilith].Add(attackHelper.BuildData("Tail Slap", $"4d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Marilith].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 6, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Marilith].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("4d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Strength));
                testCases[CreatureConstants.Marilith].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Marilith].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Marilith].Add(attackHelper.BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Marut].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), "Fist of Thunder", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Marut].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), "Fist of Lightning", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Marut].Add(attackHelper.BuildData("Fist of Thunder", $"3d6 sonic", "deafened 2d6 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Marut].Add(attackHelper.BuildData("Fist of Lightning", $"3d6 electricity", "blinded 2d6 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Marut].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Medusa].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Medusa].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Medusa].Add(attackHelper.BuildData("Snakes", $"1d4", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Medusa].Add(attackHelper.BuildData("Petrifying Gaze", string.Empty, "Permanent petrification", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.Medusa].Add(attackHelper.BuildData("Poison", string.Empty, "Initial damage 1d6 Str, Secondary damage 2d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Megaraptor].Add(attackHelper.BuildData("Talons", $"2d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Megaraptor].Add(attackHelper.BuildData("Foreclaw", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Megaraptor].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Megaraptor].Add(attackHelper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Mephit_Air].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Air].Add(attackHelper.BuildData("Breath weapon", $"1d8", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Air].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Air].Add(attackHelper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Dust].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Dust].Add(attackHelper.BuildData("Breath weapon", $"1d4", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Dust].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Dust].Add(attackHelper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Earth].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Earth].Add(attackHelper.BuildData("Breath weapon", $"1d8", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Earth].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Earth].Add(attackHelper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Fire].Add(attackHelper.BuildData("Claw", $"1d3 + 1d4 fire", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Fire].Add(attackHelper.BuildData("Breath weapon", $"1d8 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Fire].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Fire].Add(attackHelper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Ice].Add(attackHelper.BuildData("Claw", $"1d3 + 1d4 cold", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Ice].Add(attackHelper.BuildData("Breath weapon", $"1d4 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Ice].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Ice].Add(attackHelper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Magma].Add(attackHelper.BuildData("Claw", $"1d3 + 1d4 fire", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Magma].Add(attackHelper.BuildData("Breath weapon", $"1d4 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Magma].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Magma].Add(attackHelper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Ooze].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Ooze].Add(attackHelper.BuildData("Breath weapon", $"1d4 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Ooze].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Ooze].Add(attackHelper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Salt].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Salt].Add(attackHelper.BuildData("Breath weapon", $"1d4", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Salt].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Salt].Add(attackHelper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Steam].Add(attackHelper.BuildData("Claw", $"1d3 + 1d4 fire", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Steam].Add(attackHelper.BuildData("Breath weapon", $"1d4 fire", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Steam].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Steam].Add(attackHelper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Mephit_Water].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mephit_Water].Add(attackHelper.BuildData("Breath weapon", $"1d8 acid", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 1));
                testCases[CreatureConstants.Mephit_Water].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Mephit_Water].Add(attackHelper.BuildData("Summon Mephit", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Merfolk].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Merfolk].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Merfolk].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Mimic].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mimic].Add(attackHelper.BuildData("Adhesive", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Mimic].Add(attackHelper.BuildData("Crush", $"1d8", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.MindFlayer].Add(attackHelper.BuildData("Tentacle", $"1d4", string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.MindFlayer].Add(attackHelper.BuildData("Mind Blast", string.Empty, "3d4 rounds stunned", 1, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.MindFlayer].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.Psionic, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.MindFlayer].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.MindFlayer].Add(attackHelper.BuildData("Extract", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Minotaur].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Minotaur].Add(attackHelper.BuildData("Gore", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Minotaur].Add(attackHelper.BuildData("Powerful Charge", $"4d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Mohrg].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mohrg].Add(attackHelper.BuildData("Tongue", string.Empty, "Paralyzing Touch", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mohrg].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Mohrg].Add(attackHelper.BuildData("Paralyzing Touch", string.Empty, "1d4 minutes paralyzed", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Mohrg].Add(attackHelper.BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));

                testCases[CreatureConstants.Monkey].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Mule].Add(attackHelper.BuildData("Hoof", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Mummy].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), "Disease", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Mummy].Add(attackHelper.BuildData("Despair", string.Empty, "1d4 rounds fear", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.Mummy].Add(attackHelper.BuildData("Disease", string.Empty, "Mummy Rot", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Mummy].Add(attackHelper.BuildData("Mummy Rot", string.Empty, "incubation period 1 minute; damage 1d6 Con and 1d6 Cha", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Naga_Dark].Add(attackHelper.BuildData("Sting", $"2d4", "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Naga_Dark].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Naga_Dark].Add(attackHelper.BuildData("Poison", string.Empty, "lapse into a nightmare-haunted sleep for 2d4 minutes", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Naga_Dark].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Naga_Guardian].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Naga_Guardian].Add(attackHelper.BuildData("Spit", string.Empty, "Poison", 0, "ranged touch", 1, FeatConstants.Frequencies.Round, false, true, false, false));
                testCases[CreatureConstants.Naga_Guardian].Add(attackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d10 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Naga_Guardian].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Naga_Spirit].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Naga_Spirit].Add(attackHelper.BuildData("Charming Gaze", string.Empty, SpellConstants.CharmPerson, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.Naga_Spirit].Add(attackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d8 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Naga_Spirit].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Naga_Water].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Naga_Water].Add(attackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d8 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Naga_Water].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Nalfeshnee].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Nalfeshnee].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Nalfeshnee].Add(attackHelper.BuildData("Smite", string.Empty, string.Empty, 1, "supernatural ability", 3, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.Nalfeshnee].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Nalfeshnee].Add(attackHelper.BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.NightHag].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), "Disease", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.NightHag].Add(attackHelper.BuildData("Disease", string.Empty, "incubation period 1 day, damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.NightHag].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.NightHag].Add(attackHelper.BuildData("Dream Haunting", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, true, true, true, true));

                testCases[CreatureConstants.Nightcrawler].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Nightcrawler].Add(attackHelper.BuildData("Sting", $"2d8", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Nightcrawler].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Nightcrawler].Add(attackHelper.BuildData("Desecrating Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));
                testCases[CreatureConstants.Nightcrawler].Add(attackHelper.BuildData("Energy Drain", string.Empty, "1 negative level", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Nightcrawler].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Nightcrawler].Add(attackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 2d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Nightcrawler].Add(attackHelper.BuildData("Summon Undead", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.Nightcrawler].Add(attackHelper.BuildData("Swallow Whole", "2d8+12 bludgeoning + 12 acid", "Energy Drain", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Nightmare].Add(attackHelper.BuildData("Hoof", $"1d8", "1d4 fire", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Nightmare].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Nightmare].Add(attackHelper.BuildData("Flaming Hooves", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Nightmare].Add(attackHelper.BuildData("Smoke", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Nightmare].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Nightmare_Cauchemar].Add(attackHelper.BuildData("Hoof", $"2d6", "1d4 fire", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Nightmare_Cauchemar].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Nightmare_Cauchemar].Add(attackHelper.BuildData("Flaming Hooves", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Nightmare_Cauchemar].Add(attackHelper.BuildData("Smoke", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Nightmare_Cauchemar].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Nightwalker].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Nightwalker].Add(attackHelper.BuildData("Crush Item", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Nightwalker].Add(attackHelper.BuildData("Desecrating Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));
                testCases[CreatureConstants.Nightwalker].Add(attackHelper.BuildData("Evil Gaze", string.Empty, "1d8 rounds paralyzed with fear", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.Nightwalker].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Nightwalker].Add(attackHelper.BuildData("Summon Undead", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Nightwing].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), "Magic Drain", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Nightwing].Add(attackHelper.BuildData("Desecrating Aura", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));
                testCases[CreatureConstants.Nightwing].Add(attackHelper.BuildData("Magic Drain", string.Empty, "1 point enhancement bonus", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Nightwing].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Nightwing].Add(attackHelper.BuildData("Summon Undead", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Nixie].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Nixie].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Nixie].Add(attackHelper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Nixie].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Nymph].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Nymph].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Nymph].Add(attackHelper.BuildData("Blinding Beauty", string.Empty, "Blinded permanently", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Nymph].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Nymph].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Nymph].Add(attackHelper.BuildData("Stunning Glance", string.Empty, "stunned 2d4 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.OchreJelly].Add(attackHelper.BuildData("Slam", "2d4", "1d4 acid", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.OchreJelly].Add(attackHelper.BuildData("Acid", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
                testCases[CreatureConstants.OchreJelly].Add(attackHelper.BuildData("Constrict", "2d4", "1d4 acid", 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.OchreJelly].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Octopus].Add(attackHelper.BuildData("Arms", $"0", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Octopus].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Octopus].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Octopus_Giant].Add(attackHelper.BuildData("Tentacle", $"1d4", string.Empty, 1, "melee", 8, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Octopus_Giant].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Octopus_Giant].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Octopus_Giant].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Ogre].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Ogre].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Ogre].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Ogre_Merrow].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Ogre_Merrow].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Ogre_Merrow].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.OgreMage].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.OgreMage].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.OgreMage].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.OgreMage].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Orc].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Orc].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Orc].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Orc_Half].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Orc_Half].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Orc_Half].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Otyugh].Add(attackHelper.BuildData("Tentacle", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Otyugh].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), "Disease", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Otyugh].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Otyugh].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Otyugh].Add(attackHelper.BuildData("Disease", string.Empty, "Filth Fever", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Otyugh].Add(attackHelper.BuildData("Filth Fever", string.Empty, "incubation period 1d3 days; damage 1d3 Dex and 1d3 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Owl].Add(attackHelper.BuildData("Talons", $"1d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Owl_Giant].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Owl_Giant].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Owlbear].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Owlbear].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Owlbear].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Pegasus].Add(attackHelper.BuildData("Hoof", $"1d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pegasus].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Pegasus].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.PhantomFungus].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.PhaseSpider].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.PhaseSpider].Add(attackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d8 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.PhaseSpider].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Phasm].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.PitFiend].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.PitFiend].Add(attackHelper.BuildData("Wing", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.PitFiend].Add(attackHelper.BuildData("Bite", $"4d6", "poison, disease", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.PitFiend].Add(attackHelper.BuildData("Tail Slap", $"2d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.PitFiend].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 2, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.PitFiend].Add(attackHelper.BuildData("Fear Aura", string.Empty, string.Empty, 0, "supernatural ability", 2, FeatConstants.Frequencies.Round, false, true, false, true, SaveConstants.Will, AbilityConstants.Charisma));
                testCases[CreatureConstants.PitFiend].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.PitFiend].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.PitFiend].Add(attackHelper.BuildData("Summon Devil", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.PitFiend].Add(attackHelper.BuildData("Poison", string.Empty, "Initial damage 1d6 Con, Secondary damage death", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.PitFiend].Add(attackHelper.BuildData("Disease", string.Empty, "Devil Chills", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.PitFiend].Add(attackHelper.BuildData("Devil Chills", string.Empty, "incubation period 1d4 days, damage 1d4 Str", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Pixie].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Pixie].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Pixie].Add(attackHelper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pixie].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Pixie].Add(attackHelper.BuildData("Special Arrow (Memory Loss)", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, false, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveDcBonus: 2));
                testCases[CreatureConstants.Pixie].Add(attackHelper.BuildData("Special Arrow (Sleep)", string.Empty, SpellConstants.Sleep, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, false, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveDcBonus: 2));

                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(attackHelper.BuildData("Unarmed Strike", "1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(attackHelper.BuildData("Special Arrow (Memory Loss)", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, false, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will, saveDcBonus: 2));
                testCases[CreatureConstants.Pixie_WithIrresistibleDance].Add(attackHelper.BuildData("Special Arrow (Sleep)", string.Empty, SpellConstants.Sleep, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, false, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude, saveDcBonus: 2));

                testCases[CreatureConstants.Pony].Add(attackHelper.BuildData("Hoof", $"1d3", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Pony_War].Add(attackHelper.BuildData("Hoof", $"1d3", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Porpoise].Add(attackHelper.BuildData("Slam", $"2d4", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.PrayingMantis_Giant].Add(attackHelper.BuildData("Claws", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.PrayingMantis_Giant].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.PrayingMantis_Giant].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Pseudodragon].Add(attackHelper.BuildData("Sting",
                    damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Piercing),
                    "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pseudodragon].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("1", AttributeConstants.DamageTypes.Piercing),
                    string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Pseudodragon].Add(attackHelper.BuildData("Poison",
                    string.Empty,
                    "initial damage sleep for 1 minute, secondary damage sleep for 1d3 hours", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 2));

                testCases[CreatureConstants.PurpleWorm].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.PurpleWorm].Add(attackHelper.BuildData("Sting", $"2d6", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.PurpleWorm].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.PurpleWorm].Add(attackHelper.BuildData("Poison", string.Empty, "initial damage 1d6 Str, secondary damage 2d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.PurpleWorm].Add(attackHelper.BuildData("Swallow Whole", "2d8+12 bludgeoning", "8 acid", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Pyrohydra_5Heads].Add(attackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 5, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pyrohydra_5Heads].Add(attackHelper.BuildData("Breath weapon", $"3d6 fire per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Pyrohydra_6Heads].Add(attackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 6, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pyrohydra_6Heads].Add(attackHelper.BuildData("Breath weapon", $"3d6 fire per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Pyrohydra_7Heads].Add(attackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 7, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pyrohydra_7Heads].Add(attackHelper.BuildData("Breath weapon", $"3d6 fire per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Pyrohydra_8Heads].Add(attackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 8, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pyrohydra_8Heads].Add(attackHelper.BuildData("Breath weapon", $"3d6 fire per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Pyrohydra_9Heads].Add(attackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 9, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pyrohydra_9Heads].Add(attackHelper.BuildData("Breath weapon", $"3d6 fire per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Pyrohydra_10Heads].Add(attackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 10, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pyrohydra_10Heads].Add(attackHelper.BuildData("Breath weapon", $"3d6 fire per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Pyrohydra_11Heads].Add(attackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 11, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pyrohydra_11Heads].Add(attackHelper.BuildData("Breath weapon", $"3d6 fire per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Pyrohydra_12Heads].Add(attackHelper.BuildData("Bite", $"1d10", string.Empty, 1, "melee", 12, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Pyrohydra_12Heads].Add(attackHelper.BuildData("Breath weapon", $"3d6 fire per head", string.Empty, 1, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Quasit].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Slashing), "poison", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Quasit].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Quasit].Add(attackHelper.BuildData("Poison", string.Empty, $"Initial damage 1d4 Dex, Secondary damage 2d4 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution, 2));
                testCases[CreatureConstants.Quasit].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Rakshasa].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Rakshasa].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Rakshasa].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Rast].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Rast].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Rast].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Rast].Add(attackHelper.BuildData("Paralyzing Gaze", string.Empty, "Paralysis for 1d6 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Rast].Add(attackHelper.BuildData("Blood Drain", string.Empty, "1 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Rat].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Rat_Dire].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Piercing), "Disease", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Rat_Dire].Add(attackHelper.BuildData("Disease", string.Empty, "Filth Fever", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
                testCases[CreatureConstants.Rat_Dire].Add(attackHelper.BuildData("Filth Fever", string.Empty, "incubation period 1d3 days, damage 1d3 Dex and 1d3 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Rat_Swarm].Add(attackHelper.BuildData("Swarm", damageHelper.BuildEntries("1d6", string.Empty), "Disease", 0, "swarm", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Rat_Swarm].Add(attackHelper.BuildData("Disease", string.Empty, "Filth Fever", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
                testCases[CreatureConstants.Rat_Swarm].Add(attackHelper.BuildData("Filth Fever", string.Empty, "incubation period 1d3 days, damage 1d3 Dex and 1d3 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Rat_Swarm].Add(attackHelper.BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Raven].Add(attackHelper.BuildData("Claws", $"1d2", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Ravid].Add(attackHelper.BuildData("Tail Slap", $"1d6", "Positive Energy", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ravid].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), "Positive Energy", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Ravid].Add(attackHelper.BuildData("Tail Touch", string.Empty, "Positive Energy", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Ravid].Add(attackHelper.BuildData("Claw Touch", string.Empty, "Positive Energy", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Ravid].Add(attackHelper.BuildData("Positive Energy", "2d10 positive energy", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Ravid].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.RazorBoar].Add(attackHelper.BuildData("Tusk Slash", $"1d8", "Vorpal Tusk", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.RazorBoar].Add(attackHelper.BuildData("Hoof", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.RazorBoar].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.RazorBoar].Add(attackHelper.BuildData("Vorpal Tusk", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.RazorBoar].Add(attackHelper.BuildData("Trample", "2d6", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Remorhaz].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Remorhaz].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Remorhaz].Add(attackHelper.BuildData("Swallow Whole", "2d8+12 bludgeoning", "8d6 fire", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Retriever].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Retriever].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Retriever].Add(attackHelper.BuildData("Eye Ray", string.Empty, string.Empty, 0, "ranged touch", 1, FeatConstants.Frequencies.Round, false, true, false, true, string.Empty, AbilityConstants.Dexterity));
                testCases[CreatureConstants.Retriever].Add(attackHelper.BuildData("Find Target", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));
                testCases[CreatureConstants.Retriever].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Rhinoceras].Add(attackHelper.BuildData("Gore", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Rhinoceras].Add(attackHelper.BuildData("Powerful Charge", $"4d6", string.Empty, 3, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Roc].Add(attackHelper.BuildData("Talon", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Roc].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Roper].Add(attackHelper.BuildData("Strand", string.Empty, "Drag", 0, "ranged touch", 6, FeatConstants.Frequencies.Round, false, true, false, false));
                testCases[CreatureConstants.Roper].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Roper].Add(attackHelper.BuildData("Drag", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Strength));
                testCases[CreatureConstants.Roper].Add(attackHelper.BuildData("Weakness", string.Empty, "2d8 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.RustMonster].Add(attackHelper.BuildData("Antennae", string.Empty, "Rust", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.RustMonster].Add(attackHelper.BuildData("Bite", "1d3", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.RustMonster].Add(attackHelper.BuildData("Rust", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex, saveDcBonus: 4));

                testCases[CreatureConstants.Sahuagin].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Sahuagin].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Sahuagin].Add(attackHelper.BuildData("Talon", $"1d4", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Sahuagin].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Sahuagin].Add(attackHelper.BuildData("Blood Frenzy", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.Sahuagin].Add(attackHelper.BuildData("Rake", $"1d4", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Sahuagin_Mutant].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Sahuagin_Mutant].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1, "ranged", 2, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Sahuagin_Mutant].Add(attackHelper.BuildData("Talon", $"1d4", string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Sahuagin_Mutant].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Sahuagin_Mutant].Add(attackHelper.BuildData("Blood Frenzy", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));
                testCases[CreatureConstants.Sahuagin_Mutant].Add(attackHelper.BuildData("Rake", $"1d4", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Sahuagin_Malenti].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Sahuagin_Malenti].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Sahuagin_Malenti].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Sahuagin_Malenti].Add(attackHelper.BuildData("Blood Frenzy", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Salamander_Flamebrother].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Salamander_Flamebrother].Add(attackHelper.BuildData("Tail Slap", $"1d4", "Heat", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Salamander_Flamebrother].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), "Heat", 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Salamander_Flamebrother].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Salamander_Flamebrother].Add(attackHelper.BuildData("Heat", "1d6 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));

                testCases[CreatureConstants.Salamander_Average].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Salamander_Average].Add(attackHelper.BuildData("Tail Slap", $"2d6", "Heat", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Salamander_Average].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), "Heat", 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Salamander_Average].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Salamander_Average].Add(attackHelper.BuildData("Heat", "1d6 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));

                testCases[CreatureConstants.Salamander_Noble].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Salamander_Noble].Add(attackHelper.BuildData("Tail Slap", $"2d8", "Heat", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Salamander_Noble].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), "Heat", 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Salamander_Noble].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Salamander_Noble].Add(attackHelper.BuildData("Heat", "1d8 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Salamander_Noble].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Satyr].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, false, false));
                testCases[CreatureConstants.Satyr].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 0, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Satyr].Add(attackHelper.BuildData("Head butt", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Satyr_WithPipes].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, false, false));
                testCases[CreatureConstants.Satyr_WithPipes].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 0, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Satyr_WithPipes].Add(attackHelper.BuildData("Head butt", $"1d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Satyr_WithPipes].Add(attackHelper.BuildData("Pipes", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.Scorpion_Monstrous_Tiny].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d2", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Tiny].Add(attackHelper.BuildData("Sting", $"1d2", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Tiny].Add(attackHelper.BuildData("Constrict", $"1d2", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Tiny].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Tiny].Add(attackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Scorpion_Monstrous_Small].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Small].Add(attackHelper.BuildData("Sting", $"1d3", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Small].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Small].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Small].Add(attackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d2 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Scorpion_Monstrous_Medium].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Medium].Add(attackHelper.BuildData("Sting", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Medium].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Medium].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Medium].Add(attackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d3 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Scorpion_Monstrous_Large].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Large].Add(attackHelper.BuildData("Sting", $"1d6", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Large].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Large].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Large].Add(attackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d4 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Scorpion_Monstrous_Huge].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Huge].Add(attackHelper.BuildData("Sting", $"1d8", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Huge].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Huge].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Huge].Add(attackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan].Add(attackHelper.BuildData("Sting", $"2d6", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Gargantuan].Add(attackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d8 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Scorpion_Monstrous_Colossal].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Colossal].Add(attackHelper.BuildData("Sting", $"2d8", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpion_Monstrous_Colossal].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Colossal].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Scorpion_Monstrous_Colossal].Add(attackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d10 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Scorpionfolk].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Scorpionfolk].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 0, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Scorpionfolk].Add(attackHelper.BuildData("Sting", $"1d8", "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpionfolk].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Scorpionfolk].Add(attackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d4 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Scorpionfolk].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Scorpionfolk].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.SeaCat].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.SeaCat].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.SeaCat].Add(attackHelper.BuildData("Rend", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.SeaHag].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.SeaHag].Add(attackHelper.BuildData("Horrific Appearance", string.Empty, "2d6 Strength damage", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Charisma));
                testCases[CreatureConstants.SeaHag].Add(attackHelper.BuildData("Evil Eye", string.Empty, string.Empty, 0, "supernatural ability", 3, FeatConstants.Frequencies.Day, false, true, true, true, SaveConstants.Will, AbilityConstants.Charisma));

                testCases[CreatureConstants.Shadow].Add(attackHelper.BuildData("Incorporeal touch", string.Empty, "Strength Damage", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Shadow].Add(attackHelper.BuildData("Strength Damage", string.Empty, "1d6 Str", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Shadow].Add(attackHelper.BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Shadow_Greater].Add(attackHelper.BuildData("Incorporeal touch", string.Empty, "Strength Damage", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Shadow_Greater].Add(attackHelper.BuildData("Strength Damage", string.Empty, "1d8 Str", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Shadow_Greater].Add(attackHelper.BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.ShadowMastiff].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.ShadowMastiff].Add(attackHelper.BuildData("Bay", string.Empty, "panicked 2d4 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.ShadowMastiff].Add(attackHelper.BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.ShamblingMound].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.ShamblingMound].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.ShamblingMound].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Shark_Dire].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Shark_Dire].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Shark_Dire].Add(attackHelper.BuildData("Swallow Whole", "2d6+6 bludgeoning", "1d8+4 acid", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Shark_Huge].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Shark_Large].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Shark_Medium].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.ShieldGuardian].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.ShieldGuardian].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.ShockerLizard].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.ShockerLizard].Add(attackHelper.BuildData("Stunning Shock", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));
                testCases[CreatureConstants.ShockerLizard].Add(attackHelper.BuildData("Lethal Shock", $"2d8 electricity per shocker lizard", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, false, true, true, true, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Shrieker].Add(attackHelper.BuildData("Shriek", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Skum].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Skum].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Skum].Add(attackHelper.BuildData("Rake", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Slaad_Red].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Slaad_Red].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), "Implant", 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Slaad_Red].Add(attackHelper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Slaad_Red].Add(attackHelper.BuildData("Implant", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Slaad_Red].Add(attackHelper.BuildData("Stunning Croak", string.Empty, "Stunned 1d3 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Slaad_Red].Add(attackHelper.BuildData("Summon Slaad", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Slaad_Blue].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Slaad_Blue].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), "Disease", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Slaad_Blue].Add(attackHelper.BuildData("Disease", string.Empty, "Slaad Fever", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Slaad_Blue].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Slaad_Blue].Add(attackHelper.BuildData("Slaad Fever", string.Empty, "incubation period 1 day, damage 1d3 Dex and 1d3 Cha", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Slaad_Blue].Add(attackHelper.BuildData("Summon Slaad", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Slaad_Green].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Slaad_Green].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Slaad_Green].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Slaad_Green].Add(attackHelper.BuildData("Summon Slaad", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Slaad_Gray].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Slaad_Gray].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Slaad_Gray].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Slaad_Gray].Add(attackHelper.BuildData("Summon Slaad", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Slaad_Death].Add(attackHelper.BuildData("Claw", $"3d6", "Stun", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Slaad_Death].Add(attackHelper.BuildData("Bite", $"2d10", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Slaad_Death].Add(attackHelper.BuildData("Stun", string.Empty, "Stunned 1 round", 0, "extraordinary ability", 3, FeatConstants.Frequencies.Day, false, true, true, true, saveAbility: AbilityConstants.Wisdom, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Slaad_Death].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Slaad_Death].Add(attackHelper.BuildData("Summon Slaad", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Snake_Constrictor].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Snake_Constrictor].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Snake_Constrictor].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Snake_Constrictor_Giant].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Snake_Constrictor_Giant].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Snake_Constrictor_Giant].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Snake_Viper_Tiny].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("1", AttributeConstants.DamageTypes.Piercing),
                    "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Snake_Viper_Tiny].Add(attackHelper.BuildData("Poison",
                    string.Empty,
                    "Initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Snake_Viper_Small].Add(attackHelper.BuildData("Bite", $"1d2", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Snake_Viper_Small].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Snake_Viper_Medium].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Snake_Viper_Medium].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Snake_Viper_Large].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Snake_Viper_Large].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Snake_Viper_Huge].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Snake_Viper_Huge].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spectre].Add(attackHelper.BuildData("Incorporeal touch", $"1d8", "Energy Drain", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spectre].Add(attackHelper.BuildData("Energy Drain", string.Empty, "2 negative levels", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Spectre].Add(attackHelper.BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Tiny].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d2 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Small].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Small].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d3 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Medium].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d4 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Large].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Large].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Huge].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d8 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 2d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal].Add(attackHelper.BuildData("Bite", $"4d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_Hunter_Colossal].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 2d8 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d2 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny].Add(attackHelper.BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d3 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Small].Add(attackHelper.BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d4 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Medium].Add(attackHelper.BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Large].Add(attackHelper.BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d8 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Huge].Add(attackHelper.BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 2d6 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan].Add(attackHelper.BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, AbilityConstants.Constitution));

                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal].Add(attackHelper.BuildData("Bite", $"4d6", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 2d8 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal].Add(attackHelper.BuildData("Web", string.Empty, string.Empty, 0, "extraordinary ability", 8, FeatConstants.Frequencies.Day, false, true, true, true, AbilityConstants.Constitution));

                testCases[CreatureConstants.SpiderEater].Add(attackHelper.BuildData("Sting", $"1d8", "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.SpiderEater].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.SpiderEater].Add(attackHelper.BuildData("Poison", string.Empty, "Initial damage none, secondary damage paralysis for 1d8+5 weeks", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.SpiderEater].Add(attackHelper.BuildData("Implant", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Spider_Swarm].Add(attackHelper.BuildData("Swarm", damageHelper.BuildEntries("1d6", string.Empty), "poison", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Spider_Swarm].Add(attackHelper.BuildData("Poison", string.Empty, "Initial and secondary damage 1d3 Str", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Spider_Swarm].Add(attackHelper.BuildData("Distraction", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, true, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));

                testCases[CreatureConstants.Squid].Add(attackHelper.BuildData("Arms", $"0", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Squid].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Squid].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Squid_Giant].Add(attackHelper.BuildData("Tentacle", $"1d6", string.Empty, 1, "melee", 10, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Squid_Giant].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Squid_Giant].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Squid_Giant].Add(attackHelper.BuildData("Constrict", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.StagBeetle_Giant].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.StagBeetle_Giant].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 0.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Stirge].Add(attackHelper.BuildData("Touch", string.Empty, "Attach", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Stirge].Add(attackHelper.BuildData("Attach", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Stirge].Add(attackHelper.BuildData("Blood Drain", string.Empty, "1d4 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Succubus].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Succubus].Add(attackHelper.BuildData("Energy Drain", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, string.Empty, AbilityConstants.Charisma));
                testCases[CreatureConstants.Succubus].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Succubus].Add(attackHelper.BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Tarrasque].Add(attackHelper.BuildData("Bite", $"4d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Tarrasque].Add(attackHelper.BuildData("Horn", $"1d10", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tarrasque].Add(attackHelper.BuildData("Claw", $"1d12", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tarrasque].Add(attackHelper.BuildData("Tail Slap", $"3d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tarrasque].Add(attackHelper.BuildData("Augmented Critical", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tarrasque].Add(attackHelper.BuildData("Frightful Presence", string.Empty, "Shaken", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.Tarrasque].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tarrasque].Add(attackHelper.BuildData("Rush", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true));
                testCases[CreatureConstants.Tarrasque].Add(attackHelper.BuildData("Swallow Whole", "2d8+8 bludgeoning + 2d8+6 acid", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Tendriculos].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Tendriculos].Add(attackHelper.BuildData("Tendril", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tendriculos].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tendriculos].Add(attackHelper.BuildData("Swallow Whole", "2d6 acid", "Paralysis", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tendriculos].Add(attackHelper.BuildData("Paralysis", string.Empty, "paralyzed for 3d6 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, false, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Thoqqua].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), "Heat, Burn", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Thoqqua].Add(attackHelper.BuildData("Heat", $"2d6 fire", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Thoqqua].Add(attackHelper.BuildData("Burn", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Tiefling].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Tiefling].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Tiefling].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Tiefling].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Tiger].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Tiger].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tiger].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tiger].Add(attackHelper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Tiger].Add(attackHelper.BuildData("Rake", $"1d8", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Tiger_Dire].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Tiger_Dire].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tiger_Dire].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tiger_Dire].Add(attackHelper.BuildData("Pounce", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.Tiger_Dire].Add(attackHelper.BuildData("Rake", $"2d4", string.Empty, 0.5, "extraordinary ability", 2, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Titan].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Titan].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Titan].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Titan].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Toad].Add(new[] { None });

                testCases[CreatureConstants.Tojanida_Juvenile].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Tojanida_Juvenile].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tojanida_Juvenile].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tojanida_Juvenile].Add(attackHelper.BuildData("Ink Cloud", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Tojanida_Adult].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Tojanida_Adult].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tojanida_Adult].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tojanida_Adult].Add(attackHelper.BuildData("Ink Cloud", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Tojanida_Elder].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Tojanida_Elder].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Tojanida_Elder].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tojanida_Elder].Add(attackHelper.BuildData("Ink Cloud", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Treant].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Treant].Add(attackHelper.BuildData("Animate Trees", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Treant].Add(attackHelper.BuildData("Double Damage Against Objects", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Treant].Add(attackHelper.BuildData("Trample", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Triceratops].Add(attackHelper.BuildData("Gore", $"2d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Triceratops].Add(attackHelper.BuildData("Powerful charge", $"4d8", string.Empty, 2, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Triceratops].Add(attackHelper.BuildData("Trample", $"2d12", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Strength, save: SaveConstants.Reflex));

                testCases[CreatureConstants.Triton].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Triton].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Triton].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Triton].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Troglodyte].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Troglodyte].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Troglodyte].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Troglodyte].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Troglodyte].Add(attackHelper.BuildData("Stench", string.Empty, "Sickened 10 rounds", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Troll].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Troll].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Troll].Add(attackHelper.BuildData("Rend", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Troll_Scrag].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Troll_Scrag].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Troll_Scrag].Add(attackHelper.BuildData("Rend", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.TrumpetArchon].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.TrumpetArchon].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.TrumpetArchon].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.TrumpetArchon].Add(attackHelper.BuildData("Spells", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.TrumpetArchon].Add(attackHelper.BuildData("Trumpet", string.Empty, "1d4 rounds paralyzed", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Tyrannosaurus].Add(attackHelper.BuildData("Bite", $"3d6", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Tyrannosaurus].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Tyrannosaurus].Add(attackHelper.BuildData("Swallow Whole", "2d8 bludgeoning + 8 acid", string.Empty, 1, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.UmberHulk].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.UmberHulk].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.UmberHulk].Add(attackHelper.BuildData("Confusing Gaze", string.Empty, SpellConstants.Confusion, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.UmberHulk_TrulyHorrid].Add(attackHelper.BuildData("Claw", $"3d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.UmberHulk_TrulyHorrid].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.UmberHulk_TrulyHorrid].Add(attackHelper.BuildData("Confusing Gaze", string.Empty, SpellConstants.Confusion, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));

                testCases[CreatureConstants.Unicorn].Add(attackHelper.BuildData("Horn", $"1d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Unicorn].Add(attackHelper.BuildData("Hoof", $"1d4", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Unicorn].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.VampireSpawn].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning), "Energy Drain", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.VampireSpawn].Add(attackHelper.BuildData("Blood Drain", string.Empty, "1d4 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.VampireSpawn].Add(attackHelper.BuildData("Domination", string.Empty, SpellConstants.DominatePerson, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.VampireSpawn].Add(attackHelper.BuildData("Energy Drain", string.Empty, "1 negative level", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.VampireSpawn].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Vargouille].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing), "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Vargouille].Add(attackHelper.BuildData("Shriek", string.Empty, "paralyzed 2d4 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 1));
                testCases[CreatureConstants.Vargouille].Add(attackHelper.BuildData("Kiss", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 4));
                testCases[CreatureConstants.Vargouille].Add(attackHelper.BuildData("Poison", string.Empty, "unable to heal the vargouille’s bite damage naturally or magically", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude, saveDcBonus: 1));

                testCases[CreatureConstants.VioletFungus].Add(attackHelper.BuildData("Tentacle", $"1d6", "Poison", 1, "melee", 4, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.VioletFungus].Add(attackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d4 Str and 1d4 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Vrock].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Vrock].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Vrock].Add(attackHelper.BuildData("Talon", $"1d6", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Vrock].Add(attackHelper.BuildData("Dance of Ruin", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Minute, false, true, true, true, SaveConstants.Reflex, AbilityConstants.Charisma));
                testCases[CreatureConstants.Vrock].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));
                testCases[CreatureConstants.Vrock].Add(attackHelper.BuildData("Spores", "1d8", string.Empty, 0, "melee", 1, FeatConstants.Frequencies.Minute, false, true, true, true));
                testCases[CreatureConstants.Vrock].Add(attackHelper.BuildData("Stunning Screech", string.Empty, string.Empty, 0, "melee", 1, FeatConstants.Frequencies.Hour, false, true, true, true, SaveConstants.Fortitude, AbilityConstants.Constitution));
                testCases[CreatureConstants.Vrock].Add(attackHelper.BuildData("Summon Demon", string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Day, false, true, true, true));

                testCases[CreatureConstants.Wasp_Giant].Add(attackHelper.BuildData("Sting", $"1d3", "Poison", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wasp_Giant].Add(attackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 1d6 Dex", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Weasel].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Piercing), "Attach", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Weasel].Add(attackHelper.BuildData("Attach", $"1d3", string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Weasel_Dire].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), "Attach", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Weasel_Dire].Add(attackHelper.BuildData("Attach", string.Empty, "Blood Drain", 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Weasel_Dire].Add(attackHelper.BuildData("Blood Drain", string.Empty, "1d4 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Whale_Baleen].Add(attackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Whale_Cachalot].Add(attackHelper.BuildData("Bite", $"4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Whale_Cachalot].Add(attackHelper.BuildData("Tail Slap", $"1d8", string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Whale_Orca].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.Wight].Add(attackHelper.BuildData("Slam", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), "Energy Drain", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wight].Add(attackHelper.BuildData("Energy Drain", string.Empty, "1 negative level", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Wight].Add(attackHelper.BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.WillOWisp].Add(attackHelper.BuildData("Shock", string.Empty, "2d8 electricity", 0, "melee touch", 1, FeatConstants.Frequencies.Round, true, true, true, false));

                testCases[CreatureConstants.WinterWolf].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), "Freezing Bite", 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.WinterWolf].Add(attackHelper.BuildData("Breath Weapon", "4d6 cold", string.Empty, 0, "supernatural ability", 1, $"1d4 {FeatConstants.Frequencies.Round}", false, true, true, true, SaveConstants.Reflex, AbilityConstants.Constitution));
                testCases[CreatureConstants.WinterWolf].Add(attackHelper.BuildData("Freezing Bite", "1d6 cold", string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, true, true));
                testCases[CreatureConstants.WinterWolf].Add(attackHelper.BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Wolf].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wolf].Add(attackHelper.BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Wolf_Dire].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wolf_Dire].Add(attackHelper.BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Wolverine].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wolverine].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Wolverine].Add(attackHelper.BuildData("Rage", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));

                testCases[CreatureConstants.Wolverine_Dire].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wolverine_Dire].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Wolverine_Dire].Add(attackHelper.BuildData("Rage", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, false, true, false, true));

                testCases[CreatureConstants.Worg].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Worg].Add(attackHelper.BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Wraith].Add(attackHelper.BuildData("Incorporeal touch", "1d4", $"Constitution Drain", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wraith].Add(attackHelper.BuildData("Constitution Drain", string.Empty, $"1d4 Con", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Wraith].Add(attackHelper.BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Wraith_Dread].Add(attackHelper.BuildData("Incorporeal touch", "2d6", $"Constitution Drain", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wraith_Dread].Add(attackHelper.BuildData("Constitution Drain", string.Empty, $"1d8 Con", 0, "supernatural ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Wraith_Dread].Add(attackHelper.BuildData("Create Spawn", string.Empty, string.Empty, 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Wyvern].Add(attackHelper.BuildData("Sting", $"1d6", "Poison", 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Wyvern].Add(attackHelper.BuildData("Bite", damageHelper.BuildEntries("2d8", AttributeConstants.DamageTypes.Piercing), string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Wyvern].Add(attackHelper.BuildData("Wing", $"1d8", string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Wyvern].Add(attackHelper.BuildData("Talon", $"2d6", string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Wyvern].Add(attackHelper.BuildData("Poison", string.Empty, "initial and secondary damage 2d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.Wyvern].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));

                testCases[CreatureConstants.Xill].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 2, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Xill].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 2, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.Xill].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Xill].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Xill].Add(attackHelper.BuildData("Bite", string.Empty, "Paralysis", 0, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Xill].Add(attackHelper.BuildData("Implant", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Xill].Add(attackHelper.BuildData("Improved Grab", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.Xill].Add(attackHelper.BuildData("Paralysis", string.Empty, "paralyzed for 1d4 hours", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.Xorn_Minor].Add(attackHelper.BuildData("Bite", "2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Xorn_Minor].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 3, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Xorn_Average].Add(attackHelper.BuildData("Bite", "4d6", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Xorn_Average].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 3, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.Xorn_Elder].Add(attackHelper.BuildData("Bite", "4d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Xorn_Elder].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 3, FeatConstants.Frequencies.Round, true, true, false, false));

                testCases[CreatureConstants.YethHound].Add(attackHelper.BuildData("Bite", "1d8", string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.YethHound].Add(attackHelper.BuildData("Bay", string.Empty, "panicked 2d4 rounds", 0, "supernatural ability", 1, FeatConstants.Frequencies.Round, true, true, true, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.YethHound].Add(attackHelper.BuildData("Trip", string.Empty, string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));

                testCases[CreatureConstants.Yrthak].Add(attackHelper.BuildData("Bite", "2d8", string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Yrthak].Add(attackHelper.BuildData("Claw", damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Slashing), string.Empty, 0.5, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.Yrthak].Add(attackHelper.BuildData("Sonic Lance", "6d6 sonic", string.Empty, 0, "ranged touch", 1, $"2 {FeatConstants.Frequencies.Round}", false, true, true, false));
                testCases[CreatureConstants.Yrthak].Add(attackHelper.BuildData("Explosion", "2d6 piercing", string.Empty, 0, "supernatural ability", 1, $"2 {FeatConstants.Frequencies.Round}", true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));

                testCases[CreatureConstants.YuanTi_Pureblood].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.YuanTi_Pureblood].Add(attackHelper.BuildData(AttributeConstants.Ranged, string.Empty, string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.YuanTi_Pureblood].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d3", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.YuanTi_Pureblood].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(attackHelper.BuildData(AttributeConstants.Melee,
                    string.Empty,
                    string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(attackHelper.BuildData(AttributeConstants.Ranged,
                    string.Empty,
                    string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(attackHelper.BuildData("Poison",
                    string.Empty,
                    "initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(attackHelper.BuildData("Produce Acid",
                    damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Acid),
                    "initial and secondary damage 1d6 Con", 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeHead].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                    string.Empty,
                    string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Piercing),
                    "Poison", 1, "melee", 2, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(attackHelper.BuildData("Poison",
                    string.Empty,
                    "initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(attackHelper.BuildData("Produce Acid",
                    damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Acid),
                    "initial and secondary damage 1d6 Con", 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeArms].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                    string.Empty,
                    string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(attackHelper.BuildData(AttributeConstants.Melee,
                    string.Empty,
                    string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(attackHelper.BuildData(AttributeConstants.Ranged,
                    string.Empty,
                    string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(attackHelper.BuildData("Poison",
                    string.Empty,
                    "initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(attackHelper.BuildData("Produce Acid",
                    damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Acid),
                    "initial and secondary damage 1d6 Con", 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(attackHelper.BuildData("Constrict",
                    damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                    string.Empty,
                    string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(attackHelper.BuildData(AttributeConstants.Melee,
                    string.Empty,
                    string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(attackHelper.BuildData(AttributeConstants.Ranged,
                    string.Empty,
                    string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Piercing),
                    "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(attackHelper.BuildData("Poison",
                    string.Empty,
                    "initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(attackHelper.BuildData("Produce Acid",
                    damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Acid),
                    "initial and secondary damage 1d6 Con", 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(attackHelper.BuildData("Constrict",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.YuanTi_Halfblood_SnakeTail].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                    string.Empty,
                    string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.YuanTi_Abomination].Add(attackHelper.BuildData(AttributeConstants.Melee,
                    string.Empty,
                    string.Empty, 1, "melee", 1, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.YuanTi_Abomination].Add(attackHelper.BuildData(AttributeConstants.Ranged,
                    string.Empty,
                    string.Empty, 1.5, "ranged", 1, FeatConstants.Frequencies.Round, false, false, true, false));
                testCases[CreatureConstants.YuanTi_Abomination].Add(attackHelper.BuildData("Bite",
                    damageHelper.BuildEntries("2d6", AttributeConstants.DamageTypes.Piercing),
                    "Poison", 0.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, false, false));
                testCases[CreatureConstants.YuanTi_Abomination].Add(attackHelper.BuildData("Poison",
                    string.Empty,
                    "initial and secondary damage 1d6 Con", 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Abomination].Add(attackHelper.BuildData("Aversion",
                    string.Empty,
                    "aversion 10 minutes", 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, false, true, saveAbility: AbilityConstants.Charisma, save: SaveConstants.Will));
                testCases[CreatureConstants.YuanTi_Abomination].Add(attackHelper.BuildData("Produce Acid",
                    damageHelper.BuildEntries("3d6", FeatConstants.Foci.Elements.Acid),
                    "initial and secondary damage 1d6 Con", 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, true, true, false, true, saveAbility: AbilityConstants.Constitution, save: SaveConstants.Fortitude));
                testCases[CreatureConstants.YuanTi_Abomination].Add(attackHelper.BuildData("Constrict",
                    damageHelper.BuildEntries("1d6", AttributeConstants.DamageTypes.Bludgeoning),
                    string.Empty, 1.5, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, true, true));
                testCases[CreatureConstants.YuanTi_Abomination].Add(attackHelper.BuildData("Improved Grab",
                    string.Empty,
                    string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Round, true, true, false, true));
                testCases[CreatureConstants.YuanTi_Abomination].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility,
                    string.Empty,
                    string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

                testCases[CreatureConstants.Zelekhut].Add(attackHelper.BuildData(AttributeConstants.Melee, string.Empty, string.Empty, 1, "melee", 2, FeatConstants.Frequencies.Round, true, false, true, false));
                testCases[CreatureConstants.Zelekhut].Add(attackHelper.BuildData("Unarmed Strike", damageHelper.BuildEntries("1d4", AttributeConstants.DamageTypes.Bludgeoning), string.Empty, 1.5, "melee", 1, FeatConstants.Frequencies.Round, true, true, true, false));
                testCases[CreatureConstants.Zelekhut].Add(attackHelper.BuildData("Electrified Weapon", "1d6 electricity", string.Empty, 0, "extraordinary ability", 1, FeatConstants.Frequencies.Hit, true, true, false, true));
                testCases[CreatureConstants.Zelekhut].Add(attackHelper.BuildData(FeatConstants.SpecialQualities.SpellLikeAbility, string.Empty, string.Empty, 0, "spell-like ability", 1, FeatConstants.Frequencies.Round, false, true, true, true));

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
