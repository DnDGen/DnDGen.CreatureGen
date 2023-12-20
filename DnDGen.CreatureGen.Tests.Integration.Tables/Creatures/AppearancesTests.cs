using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using DnDGen.Infrastructure.Selectors.Collections;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Creatures
{
    [TestFixture]
    internal class AppearancesTests : CollectionTests
    {
        protected override string tableName => TableNameConstants.Collection.Appearances;

        private Dictionary<string, IEnumerable<string>> creatureAppearances;
        private ICollectionSelector collectionSelector;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            collectionSelector = GetNewInstanceOf<ICollectionSelector>();
            creatureAppearances = GetCreatureAppearances();
        }

        [Test]
        public void AppearancesNames()
        {
            var creatures = CreatureConstants.GetAll();
            AssertCollectionNames(creatures);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void Appearances(string creature)
        {
            Assert.That(creatureAppearances, Contains.Key(creature));
            Assert.That(creatureAppearances[creature], Is.Not.Empty);
            Assert.That(creatureAppearances[creature].Where(a => a.Contains("TODO")), Is.Empty);

            AssertWeightedCollection(creature, creatureAppearances[creature].ToArray());
        }

        private Dictionary<string, IEnumerable<string>> GetCreatureAppearances()
        {
            var creatures = CreatureConstants.GetAll();
            var appearances = new Dictionary<string, IEnumerable<string>>();

            foreach (var creature in creatures)
            {
                appearances[creature] = new string[0];
            }

            //Human:
            //allSkin: new[] { "Dark Brown skin", "Brown skin", "Light brown skin",
            //    "Dark tan skin", "Tan skin", "Light tan skin",
            //    "Pink skin", "White skin", "Pale white skin" },
            //commonHair: new[] { "Straight Red hair", "Straight Blond hair", "Straight Brown hair", "Straight Black hair",
            //    "Curly Red hair", "Curly Blond hair", "Curly Brown hair", "Curly Black hair",
            //    "Kinky Red hair", "Kinky Blond hair", "Kinky Brown hair", "Kinky Black hair" },
            //uncommonHair: new[] { "Bald" },
            //allEyes: new[] { "Blue eyes", "Brown eyes", "Gray eyes", "Green eyes", "Hazel eyes" }

            //Source: https://forgottenrealms.fandom.com/wiki/Aasimar
            appearances[CreatureConstants.Aasimar] = GetWeightedAppearances(
                commonSkin: new[] { "Dark Brown skin", "Brown skin", "Light brown skin",
                    "Dark tan skin", "Tan skin", "Light tan skin",
                    "Pink skin", "White skin", "Pale white skin" },
                uncommonSkin: new[] { "Emerald skin", "Gold skin", "Silver skin",
                    "Dark Brown skin with small iridescent scales", "Brown skin with small iridescent scales", "Light brown skin with small iridescent scales",
                        "Dark tan skin with small iridescent scales", "Tan skin with small iridescent scales", "Light tan skin with small iridescent scales",
                        "Pink skin with small iridescent scales", "White skin with small iridescent scales", "Pale white skin with small iridescent scales",
                },
                rareSkin: new[] { "Emerald skin with small iridescent scales", "Gold skin with small iridescent scales", "Silver skin with small iridescent scales" },
                commonHair: new[] { "Straight Red hair", "Straight Blond hair", "Straight Brown hair", "Straight Black hair",
                    "Curly Red hair", "Curly Blond hair", "Curly Brown hair", "Curly Black hair",
                    "Kinky Red hair", "Kinky Blond hair", "Kinky Brown hair", "Kinky Black hair",
                    "Straight Silver hair", "Curly Silver hair", "Kinky Silver hair" },
                uncommonHair: new[] {
                    "Bald",
                    "Straight Red hair with feathers mixed in", "Straight Blond hair with feathers mixed in", "Straight Brown hair with feathers mixed in",
                        "Straight Black hair with feathers mixed in",
                    "Curly Red hair with feathers mixed in", "Curly Blond hair with feathers mixed in", "Curly Brown hair with feathers mixed in",
                        "Curly Black hair with feathers mixed in",
                    "Kinky Red hair with feathers mixed in", "Kinky Blond hair with feathers mixed in", "Kinky Brown hair with feathers mixed in",
                        "Kinky Black hair with feathers mixed in",
                    "Straight Silver hair with feathers mixed in", "Curly Silver hair with feathers mixed in", "Kinky Silver hair with feathers mixed in" },
                commonEyes: new[] { "Pupil-less pale white eyes", "Pupil-less golden eyes", "Pupil-less gray eyes" },
                uncommonEyes: new[] { "Pupil-less topaz eyes", "Pupil-less pearly opalescent eyes" },
                uncommonOther: new[] { "A light covering of feathers on the shoulders, where an angel's wings might sprout" });
            //Source: https://forgottenrealms.fandom.com/wiki/Aboleth
            appearances[CreatureConstants.Aboleth] = GetWeightedAppearances(
                allEyes: new[] { "Three red eyes" },
                commonSkin: new[] { "Orange-pink underbelly, sea-green skin topside" },
                uncommonSkin: new[] { "Orange underbelly, sea-green skin topside", "Pink underbelly, sea-green skin topside",
                    "Orange-pink underbelly, green skin topside", "Orange-pink underbelly, blue skin topside" },
                rareSkin: new[] { "Orange underbelly, green skin topside", "Pink underbelly, green skin topside",
                    "Orange underbelly, blue skin topside", "Pink underbelly, blue skin topside" },
                allOther: new[] { "Resembles a bizarre eel" });
            //Source: https://forgottenrealms.fandom.com/wiki/Achaierai
            appearances[CreatureConstants.Achaierai] = GetWeightedAppearances(
                allSkin: new[] { "Shining metallic blue-gray legs, claws, and beak" },
                commonHair: new[] { "Bright red, soft feathers on its crest, brown soft feathers on its body" },
                rareHair: new[] { "Dim teal, soft feathers on its crest, brown soft feathers on its body",
                    "Shadowed gold, soft feathers on its crest, brown soft feathers on its body",
                    "Burnt russet, soft feathers on its crest, brown soft feathers on its body" },
                allOther: new[] { "Resembles a quadruped, plump quail" });
            //Source: https://forgottenrealms.fandom.com/wiki/Allip
            appearances[CreatureConstants.Allip] = new[] { "Spectral variations of the person it once was, with nightmarish, warped features befitting of the madness that possessed it. Its lower portions trail away into a dark fog." };
            //Source: https://forgottenrealms.fandom.com/wiki/Androsphinx
            appearances[CreatureConstants.Androsphinx] = new[] { "Tawny hair/fur. Lion body, falcon wings." };
            //Source: https://forgottenrealms.fandom.com/wiki/Astral_deva
            appearances[CreatureConstants.Angel_AstralDeva] = GetWeightedAppearances(
                allSkin: new[] { "Golden skin" },
                allHair: new[] { "Fair hair", "Golden hair" },
                allEyes: new[] { "Amber eyes" },
                allOther: new[] { "White wings tinted with gold" });
            //Source: https://forgottenrealms.fandom.com/wiki/Planetar
            appearances[CreatureConstants.Angel_Planetar] = GetWeightedAppearances(
                commonSkin: new[] { "Opalescent emerald skin" },
                uncommonSkin: new[] { "Pearly white skin" },
                commonHair: new[] { "Bald; Opalescent white feathers tinted with gold on wings", "Bald; White feathers tinted with gold on wings" },
                uncommonHair: new[] { "Long, flowing blue hair; Opalescent white feathers tinted with gold on wings",
                    "Long, flowing blue hair; White feathers tinted with gold on wings" },
                allEyes: new[] { "Glowing blue eyes" });
            //Source: https://forgottenrealms.fandom.com/wiki/Solar
            appearances[CreatureConstants.Angel_Solar] = GetWeightedAppearances(
                commonSkin: new[] { "Copper skin", "Silver skin", "Golden skin" },
                uncommonSkin: new[] { "Bronze skin", "Brass skin" },
                commonHair: new[] { "Bronze hair; White-feathered wings", "Bronze hair; Coppery-golden-feathered wings" },
                uncommonHair: new[] { "Copper hair; White-feathered wings", "Copper hair; Coppery-golden-feathered wings",
                    "Silver hair; White-feathered wings", "Silver hair; Coppery-golden-feathered wings",
                    "Golden hair; White-feathered wings", "Golden hair; Coppery-golden-feathered wings",
                    "Brass hair; White-feathered wings", "Brass hair; Coppery-golden-feathered wings" },
                allEyes: new[] { "Radiant topaz eyes" });
            //Source: https://www.d20srd.org/srd/monsters/animatedObject.htm
            //https://forgottenrealms.fandom.com/wiki/Animated_object
            appearances[CreatureConstants.AnimatedObject_Colossal] = new[] { "Candlestick", "Candelabra", "Plate", "Cup", "Tea Pot", "Bath Tub" };
            appearances[CreatureConstants.AnimatedObject_Colossal_Flexible] = new[] { "Rope", "Vine", "Chain" };
            appearances[CreatureConstants.AnimatedObject_Colossal_MultipleLegs] = new[] { "Stone Table", "Stone Chair", "Stone Dresser" };
            appearances[CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Wooden] = new[] { "Wooden Table", "Wooden Chair", "Wooden Dresser", "Wooden Ottoman",
                "Wooden Stool" };
            appearances[CreatureConstants.AnimatedObject_Colossal_Sheetlike] = new[] { "Carpet", "Tapestry", "Rug", "Blanket" };
            appearances[CreatureConstants.AnimatedObject_Colossal_TwoLegs] = GetWeightedAppearances(
                commonOther: new[] { "Human statue", "High Elf statue", "Lightfoot Halfling statue", "Hill Dwarf statue", "Rock Gnome statue", "Half-Elf statue",
                    "Half-Orc statue", "Suit of plate armor" },
                uncommonOther: new[] { "Wood Elf statue", "Wild Elf statue", "Gray Elf statue", "Aquatic Elf statue", "Drow statue",
                    "Tallfellow Halfling statue", "Deep Halfling statue",
                    "Mountain Dwarf statue", "Deep Dwarf statue", "Duergar statue",
                    "Forest Gnome statue", "Svirfneblin statue",
                    "Aasimar statue", "Tiefling statue" },
                rareOther: new[] { "Orc statue", "Goblin statue", "Hobgoblin statue", "Kobold statue", "Troll statue", "Ogre statue", "Mind Flayer statue",
                    "Hill Giant statue", "Frost Giant statue", "Fire Giant statue", "Stone Giant statue", "Cloud Giant statue", "Storm Giant statue",
                    "Half-Celestial statue", "Half-Fiend statue", "Half-Dragon statue" });
            appearances[CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden] = new[] { "Ladder",
                "Human wooden figurine", "High Elf wooden figurine", "Lightfoot Halfling wooden figurine", "Hill Dwarf wooden figurine", "Rock Gnome wooden figurine",
                    "Half-Elf wooden figurine", "Half-Orc wooden figurine" };
            appearances[CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden] = new[] { "Cart", "Carriage" };
            appearances[CreatureConstants.AnimatedObject_Colossal_Wooden] = new[] { "Clock", "Feather Duster", "Broom", "Bucket", "Barrel" };
            appearances[CreatureConstants.AnimatedObject_Gargantuan] = new[] { "Candlestick", "Candelabra", "Plate", "Cup", "Tea Pot", "Bath Tub" };
            appearances[CreatureConstants.AnimatedObject_Gargantuan_Flexible] = new[] { "Rope", "Vine", "Chain" };
            appearances[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs] = new[] { "Stone Table", "Stone Chair", "Stone Dresser" };
            appearances[CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Wooden] = new[] { "Wooden Table", "Wooden Chair", "Wooden Dresser", "Wooden Ottoman",
                "Wooden Stool" };
            appearances[CreatureConstants.AnimatedObject_Gargantuan_Sheetlike] = new[] { "Carpet", "Tapestry", "Rug", "Blanket" };
            appearances[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs] = GetWeightedAppearances(
                commonOther: new[] { "Human statue", "High Elf statue", "Lightfoot Halfling statue", "Hill Dwarf statue", "Rock Gnome statue", "Half-Elf statue",
                    "Half-Orc statue", "Suit of plate armor" },
                uncommonOther: new[] { "Wood Elf statue", "Wild Elf statue", "Gray Elf statue", "Aquatic Elf statue", "Drow statue",
                    "Tallfellow Halfling statue", "Deep Halfling statue",
                    "Mountain Dwarf statue", "Deep Dwarf statue", "Duergar statue",
                    "Forest Gnome statue", "Svirfneblin statue",
                    "Aasimar statue", "Tiefling statue" },
                rareOther: new[] { "Orc statue", "Goblin statue", "Hobgoblin statue", "Kobold statue", "Troll statue", "Ogre statue", "Mind Flayer statue",
                    "Hill Giant statue", "Frost Giant statue", "Fire Giant statue", "Stone Giant statue", "Cloud Giant statue", "Storm Giant statue",
                    "Half-Celestial statue", "Half-Fiend statue", "Half-Dragon statue" });
            appearances[CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden] = new[] { "Ladder",
                "Human wooden figurine", "High Elf wooden figurine", "Lightfoot Halfling wooden figurine", "Hill Dwarf wooden figurine", "Rock Gnome wooden figurine",
                    "Half-Elf wooden figurine", "Half-Orc wooden figurine" };
            appearances[CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden] = new[] { "Cart", "Carriage" };
            appearances[CreatureConstants.AnimatedObject_Gargantuan_Wooden] = new[] { "Clock", "Feather Duster", "Broom", "Bucket", "Barrel" };
            appearances[CreatureConstants.AnimatedObject_Huge] = new[] { "Candlestick", "Candelabra", "Plate", "Cup", "Tea Pot", "Bath Tub" };
            appearances[CreatureConstants.AnimatedObject_Huge_Flexible] = new[] { "Rope", "Vine", "Chain" };
            appearances[CreatureConstants.AnimatedObject_Huge_MultipleLegs] = new[] { "Stone Table", "Stone Chair", "Stone Dresser" };
            appearances[CreatureConstants.AnimatedObject_Huge_MultipleLegs_Wooden] = new[] { "Wooden Table", "Wooden Chair", "Wooden Dresser", "Wooden Ottoman",
                "Wooden Stool" };
            appearances[CreatureConstants.AnimatedObject_Huge_Sheetlike] = new[] { "Carpet", "Tapestry", "Rug", "Blanket" };
            appearances[CreatureConstants.AnimatedObject_Huge_TwoLegs] = GetWeightedAppearances(
                commonOther: new[] { "Human statue", "High Elf statue", "Lightfoot Halfling statue", "Hill Dwarf statue", "Rock Gnome statue", "Half-Elf statue",
                    "Half-Orc statue", "Suit of plate armor" },
                uncommonOther: new[] { "Wood Elf statue", "Wild Elf statue", "Gray Elf statue", "Aquatic Elf statue", "Drow statue",
                    "Tallfellow Halfling statue", "Deep Halfling statue",
                    "Mountain Dwarf statue", "Deep Dwarf statue", "Duergar statue",
                    "Forest Gnome statue", "Svirfneblin statue",
                    "Aasimar statue", "Tiefling statue" },
                rareOther: new[] { "Orc statue", "Goblin statue", "Hobgoblin statue", "Kobold statue", "Troll statue", "Ogre statue", "Mind Flayer statue",
                    "Hill Giant statue", "Frost Giant statue", "Fire Giant statue", "Stone Giant statue", "Cloud Giant statue", "Storm Giant statue",
                    "Half-Celestial statue", "Half-Fiend statue", "Half-Dragon statue" });
            appearances[CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden] = new[] { "Ladder",
                "Human wooden figurine", "High Elf wooden figurine", "Lightfoot Halfling wooden figurine", "Hill Dwarf wooden figurine", "Rock Gnome wooden figurine",
                    "Half-Elf wooden figurine", "Half-Orc wooden figurine" };
            appearances[CreatureConstants.AnimatedObject_Huge_Wheels_Wooden] = new[] { "Cart", "Carriage" };
            appearances[CreatureConstants.AnimatedObject_Huge_Wooden] = new[] { "Clock", "Feather Duster", "Broom", "Bucket", "Barrel" };
            appearances[CreatureConstants.AnimatedObject_Large] = new[] { "Candlestick", "Candelabra", "Plate", "Cup", "Tea Pot", "Bath Tub" };
            appearances[CreatureConstants.AnimatedObject_Large_Flexible] = new[] { "Rope", "Vine", "Chain" };
            appearances[CreatureConstants.AnimatedObject_Large_MultipleLegs] = new[] { "Stone Table", "Stone Chair", "Stone Dresser" };
            appearances[CreatureConstants.AnimatedObject_Large_MultipleLegs_Wooden] = new[] { "Wooden Table", "Wooden Chair", "Wooden Dresser", "Wooden Ottoman",
                "Wooden Stool" };
            appearances[CreatureConstants.AnimatedObject_Large_Sheetlike] = new[] { "Carpet", "Tapestry", "Rug", "Blanket" };
            appearances[CreatureConstants.AnimatedObject_Large_TwoLegs] = GetWeightedAppearances(
                commonOther: new[] { "Human statue", "High Elf statue", "Lightfoot Halfling statue", "Hill Dwarf statue", "Rock Gnome statue", "Half-Elf statue",
                    "Half-Orc statue", "Suit of plate armor" },
                uncommonOther: new[] { "Wood Elf statue", "Wild Elf statue", "Gray Elf statue", "Aquatic Elf statue", "Drow statue",
                    "Tallfellow Halfling statue", "Deep Halfling statue",
                    "Mountain Dwarf statue", "Deep Dwarf statue", "Duergar statue",
                    "Forest Gnome statue", "Svirfneblin statue",
                    "Aasimar statue", "Tiefling statue" },
                rareOther: new[] { "Orc statue", "Goblin statue", "Hobgoblin statue", "Kobold statue", "Troll statue", "Ogre statue", "Mind Flayer statue",
                    "Hill Giant statue", "Frost Giant statue", "Fire Giant statue", "Stone Giant statue", "Cloud Giant statue", "Storm Giant statue",
                    "Half-Celestial statue", "Half-Fiend statue", "Half-Dragon statue" });
            appearances[CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden] = new[] { "Ladder",
                "Human wooden figurine", "High Elf wooden figurine", "Lightfoot Halfling wooden figurine", "Hill Dwarf wooden figurine", "Rock Gnome wooden figurine",
                    "Half-Elf wooden figurine", "Half-Orc wooden figurine" };
            appearances[CreatureConstants.AnimatedObject_Large_Wheels_Wooden] = new[] { "Cart", "Carriage" };
            appearances[CreatureConstants.AnimatedObject_Large_Wooden] = new[] { "Clock", "Feather Duster", "Broom", "Bucket", "Barrel" };
            appearances[CreatureConstants.AnimatedObject_Medium] = new[] { "Candlestick", "Candelabra", "Plate", "Cup", "Tea Pot", "Bath Tub" };
            appearances[CreatureConstants.AnimatedObject_Medium_Flexible] = new[] { "Rope", "Vine", "Chain" };
            appearances[CreatureConstants.AnimatedObject_Medium_MultipleLegs] = new[] { "Stone Table", "Stone Chair", "Stone Dresser" };
            appearances[CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden] = new[] { "Wooden Table", "Wooden Chair", "Wooden Dresser", "Wooden Ottoman",
                "Wooden Stool" };
            appearances[CreatureConstants.AnimatedObject_Medium_Sheetlike] = new[] { "Carpet", "Tapestry", "Rug", "Blanket" };
            appearances[CreatureConstants.AnimatedObject_Medium_TwoLegs] = GetWeightedAppearances(
                commonOther: new[] { "Human statue", "High Elf statue", "Lightfoot Halfling statue", "Hill Dwarf statue", "Rock Gnome statue", "Half-Elf statue",
                    "Half-Orc statue", "Suit of plate armor" },
                uncommonOther: new[] { "Wood Elf statue", "Wild Elf statue", "Gray Elf statue", "Aquatic Elf statue", "Drow statue",
                    "Tallfellow Halfling statue", "Deep Halfling statue",
                    "Mountain Dwarf statue", "Deep Dwarf statue", "Duergar statue",
                    "Forest Gnome statue", "Svirfneblin statue",
                    "Aasimar statue", "Tiefling statue" },
                rareOther: new[] { "Orc statue", "Goblin statue", "Hobgoblin statue", "Kobold statue", "Troll statue", "Ogre statue", "Mind Flayer statue",
                    "Hill Giant statue", "Frost Giant statue", "Fire Giant statue", "Stone Giant statue", "Cloud Giant statue", "Storm Giant statue",
                    "Half-Celestial statue", "Half-Fiend statue", "Half-Dragon statue" });
            appearances[CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden] = new[] { "Ladder",
                "Human wooden figurine", "High Elf wooden figurine", "Lightfoot Halfling wooden figurine", "Hill Dwarf wooden figurine", "Rock Gnome wooden figurine",
                    "Half-Elf wooden figurine", "Half-Orc wooden figurine" };
            appearances[CreatureConstants.AnimatedObject_Medium_Wheels_Wooden] = new[] { "Cart", "Carriage" };
            appearances[CreatureConstants.AnimatedObject_Medium_Wooden] = new[] { "Clock", "Feather Duster", "Broom", "Bucket", "Barrel" };
            appearances[CreatureConstants.AnimatedObject_Small] = new[] { "Candlestick", "Candelabra", "Plate", "Cup", "Tea Pot", "Bath Tub" };
            appearances[CreatureConstants.AnimatedObject_Small_Flexible] = new[] { "Rope", "Vine", "Chain" };
            appearances[CreatureConstants.AnimatedObject_Small_MultipleLegs] = new[] { "Stone Table", "Stone Chair", "Stone Dresser" };
            appearances[CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden] = new[] { "Wooden Table", "Wooden Chair", "Wooden Dresser", "Wooden Ottoman",
                "Wooden Stool" };
            appearances[CreatureConstants.AnimatedObject_Small_Sheetlike] = new[] { "Carpet", "Tapestry", "Rug", "Blanket" };
            appearances[CreatureConstants.AnimatedObject_Small_TwoLegs] = GetWeightedAppearances(
                commonOther: new[] { "Human statue", "High Elf statue", "Lightfoot Halfling statue", "Hill Dwarf statue", "Rock Gnome statue", "Half-Elf statue",
                    "Half-Orc statue", "Suit of plate armor" },
                uncommonOther: new[] { "Wood Elf statue", "Wild Elf statue", "Gray Elf statue", "Aquatic Elf statue", "Drow statue",
                    "Tallfellow Halfling statue", "Deep Halfling statue",
                    "Mountain Dwarf statue", "Deep Dwarf statue", "Duergar statue",
                    "Forest Gnome statue", "Svirfneblin statue",
                    "Aasimar statue", "Tiefling statue" },
                rareOther: new[] { "Orc statue", "Goblin statue", "Hobgoblin statue", "Kobold statue", "Troll statue", "Ogre statue", "Mind Flayer statue",
                    "Hill Giant statue", "Frost Giant statue", "Fire Giant statue", "Stone Giant statue", "Cloud Giant statue", "Storm Giant statue",
                    "Half-Celestial statue", "Half-Fiend statue", "Half-Dragon statue" });
            appearances[CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden] = new[] { "Ladder",
                "Human wooden figurine", "High Elf wooden figurine", "Lightfoot Halfling wooden figurine", "Hill Dwarf wooden figurine", "Rock Gnome wooden figurine",
                    "Half-Elf wooden figurine", "Half-Orc wooden figurine" };
            appearances[CreatureConstants.AnimatedObject_Small_Wheels_Wooden] = new[] { "Cart", "Carriage" };
            appearances[CreatureConstants.AnimatedObject_Small_Wooden] = new[] { "Clock", "Feather Duster", "Broom", "Bucket", "Barrel" };
            appearances[CreatureConstants.AnimatedObject_Tiny] = new[] { "Candlestick", "Candelabra", "Plate", "Cup", "Tea Pot", "Bath Tub" };
            appearances[CreatureConstants.AnimatedObject_Tiny_Flexible] = new[] { "Rope", "Vine", "Chain" };
            appearances[CreatureConstants.AnimatedObject_Tiny_MultipleLegs] = new[] { "Stone Table", "Stone Chair", "Stone Dresser" };
            appearances[CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden] = new[] { "Wooden Table", "Wooden Chair", "Wooden Dresser", "Wooden Ottoman",
                "Wooden Stool" };
            appearances[CreatureConstants.AnimatedObject_Tiny_Sheetlike] = new[] { "Carpet", "Tapestry", "Rug", "Blanket" };
            appearances[CreatureConstants.AnimatedObject_Tiny_TwoLegs] = GetWeightedAppearances(
                commonOther: new[] { "Human statue", "High Elf statue", "Lightfoot Halfling statue", "Hill Dwarf statue", "Rock Gnome statue", "Half-Elf statue",
                    "Half-Orc statue", "Suit of plate armor" },
                uncommonOther: new[] { "Wood Elf statue", "Wild Elf statue", "Gray Elf statue", "Aquatic Elf statue", "Drow statue",
                    "Tallfellow Halfling statue", "Deep Halfling statue",
                    "Mountain Dwarf statue", "Deep Dwarf statue", "Duergar statue",
                    "Forest Gnome statue", "Svirfneblin statue",
                    "Aasimar statue", "Tiefling statue" },
                rareOther: new[] { "Orc statue", "Goblin statue", "Hobgoblin statue", "Kobold statue", "Troll statue", "Ogre statue", "Mind Flayer statue",
                    "Hill Giant statue", "Frost Giant statue", "Fire Giant statue", "Stone Giant statue", "Cloud Giant statue", "Storm Giant statue",
                    "Half-Celestial statue", "Half-Fiend statue", "Half-Dragon statue" });
            appearances[CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden] = new[] { "Ladder",
                "Human wooden figurine", "High Elf wooden figurine", "Lightfoot Halfling wooden figurine", "Hill Dwarf wooden figurine", "Rock Gnome wooden figurine",
                    "Half-Elf wooden figurine", "Half-Orc wooden figurine" };
            appearances[CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden] = new[] { "Cart", "Carriage" };
            appearances[CreatureConstants.AnimatedObject_Tiny_Wooden] = new[] { "Clock", "Feather Duster", "Broom", "Bucket", "Barrel" };
            //Source: https://forgottenrealms.fandom.com/wiki/Ankheg
            appearances[CreatureConstants.Ankheg] = GetWeightedAppearances(
                allSkin: new[] { "Brown skin", "Yellow skin", "Green skin" },
                allEyes: new[] { "Black eyes" });
            //Source: https://www.d20srd.org/srd/monsters/hag.htm#annis
            appearances[CreatureConstants.Annis] = GetWeightedAppearances(
                allSkin: new[] { "Blue-black skin, as if deeply bruised, covered in scars and blemishes" },
                allHair: new[] { "Black hair" },
                allEyes: new[] { "Dull, greenish-yellow eyes" },
                commonOther: new[] { "Lean with long, lanky limbs. Long, spiraling, stylized, black nails",
                    "Gnarled muscles, jutting bones, humped back, hunched shoulders. Long, spiraling, stylized black nails",
                    "Lean with long, lanky limbs. Long, spiraling, stylized, rust-colored nails",
                    "Gnarled muscles, jutting bones, humped back, hunched shoulders. Long, spiraling, stylized rust-colored nails" },
                uncommonOther: new[] { "Lean with long, lanky limbs. Long, spiraling, stylized, black nails. Chewed-through cheeks, etching a gangrenous grin",
                    "Gnarled muscles, jutting bones, humped back, hunched shoulders. Long, spiraling, stylized black nails. Chewed-through cheeks, etching a gangrenous grin",
                    "Lean with long, lanky limbs. Long, spiraling, stylized, rust-colored nails. Chewed-through cheeks, etching a gangrenous grin",
                    "Gnarled muscles, jutting bones, humped back, hunched shoulders. Long, spiraling, stylized rust-colored nails. Chewed-through cheeks, etching a gangrenous grin",
                    "Lean with long, lanky limbs. Long, spiraling, stylized, black nails. Corpses stitched to her back",
                    "Gnarled muscles, jutting bones, humped back, hunched shoulders. Long, spiraling, stylized black nails. Corpses stitched to her back",
                    "Lean with long, lanky limbs. Long, spiraling, stylized, rust-colored nails. Corpses stitched to her back",
                    "Gnarled muscles, jutting bones, humped back, hunched shoulders. Long, spiraling, stylized rust-colored nails. Corpses stitched to her back",
                    "Lean with long, lanky limbs. Long, spiraling, stylized, black nails. Right hand is a grafted Troll hand",
                    "Gnarled muscles, jutting bones, humped back, hunched shoulders. Long, spiraling, stylized black nails. Right hand is a grafted Troll hand",
                    "Lean with long, lanky limbs. Long, spiraling, stylized, rust-colored nails. Right hand is a grafted Troll hand",
                    "Gnarled muscles, jutting bones, humped back, hunched shoulders. Long, spiraling, stylized rust-colored nails. Right hand is a grafted Troll hand",
                    "Lean with long, lanky limbs. Long, spiraling, stylized, black nails. Left hand is a grafted Troll hand",
                    "Gnarled muscles, jutting bones, humped back, hunched shoulders. Long, spiraling, stylized black nails. Left hand is a grafted Troll hand",
                    "Lean with long, lanky limbs. Long, spiraling, stylized, rust-colored nails. Left hand is a grafted Troll hand",
                    "Gnarled muscles, jutting bones, humped back, hunched shoulders. Long, spiraling, stylized rust-colored nails. Left hand is a grafted Troll hand"  });
            //Source: https://forgottenrealms.fandom.com/wiki/Giant_ant
            appearances[CreatureConstants.Ant_Giant_Worker] = new[] { "Black skin", "Red skin", "Brown skin" };
            appearances[CreatureConstants.Ant_Giant_Soldier] = new[] { "Black skin", "Red skin", "Brown skin" };
            appearances[CreatureConstants.Ant_Giant_Queen] = new[] { "Black skin", "Red skin", "Brown skin" };
            //Source: https://www.dimensions.com/element/eastern-lowland-gorilla-gorilla-beringei-graueri
            appearances[CreatureConstants.Ape] = GetWeightedAppearances(
                commonHair: new[] { "Jet-black fur" },
                uncommonHair: new[] { "Grey fur", "Silver fur" });
            //Source: https://forgottenrealms.fandom.com/wiki/Dire_ape
            appearances[CreatureConstants.Ape_Dire] = GetWeightedAppearances(
                commonHair: new[] { "Jet-black fur" },
                uncommonHair: new[] { "Grey fur", "Silver fur" });
            //Source: https://forgottenrealms.fandom.com/wiki/Aranea
            appearances[CreatureConstants.Aranea] = GetWeightedAppearances(
                commonSkin: new[] { "TODO Human skin" },
                uncommonSkin: new[] { "TODO Half-Elf skin" },
                rareSkin: new[] { "TODO Drow skin" },
                commonHair: new[] { "TODO Human hair" },
                uncommonHair: new[] { "TODO Half-Elf hair" },
                rareHair: new[] { "TODO Drow hair" },
                commonEyes: new[] { "TODO Human eyes" },
                uncommonEyes: new[] { "TODO Half-Elf eyes" },
                rareEyes: new[] { "TODO Drow eyes" });
            //Source: https://www.d20srd.org/srd/monsters/arrowhawk.htm
            appearances[CreatureConstants.Arrowhawk_Juvenile] = new[] { "Covered in blue scales with occasional tufts of yellow feathers" };
            appearances[CreatureConstants.Arrowhawk_Adult] = new[] { "Covered in blue scales with occasional tufts of yellow feathers" };
            appearances[CreatureConstants.Arrowhawk_Elder] = new[] { "Covered in blue scales with occasional tufts of yellow feathers" };
            //Source: https://forgottenrealms.fandom.com/wiki/Assassin_vine
            appearances[CreatureConstants.AssassinVine] = new[] { "Smaller vines extend from the main branch and bear clusters of grape-like berries. Stringy bark. Hand-shaped leaves" };
            //Source: https://www.d20srd.org/srd/monsters/athach.htm
            appearances[CreatureConstants.Athach] = new[] { "Extra arm growing out of chest. Large, curved tusks on either side of a wide mouth. Relatively small eyes and nose for the size of the head. One ear significantly bigger than the other. Quite strong stench." };
            //Source: https://forgottenrealms.fandom.com/wiki/Avoral
            appearances[CreatureConstants.Avoral] = GetWeightedAppearances(
                allSkin: new[] { "TODO Human skin" },
                allHair: new[] { "Red feathers", "Blue feathers", "Brown feathers", "Black feathers", "White feathers" },
                allEyes: new[] { "Bright golden eyes" });
            //Source: https://forgottenrealms.fandom.com/wiki/Azer
            appearances[CreatureConstants.Azer] = GetWeightedAppearances(
                allSkin: new[] { "Brass-colored skin" },
                allHair: new[] { "Brass-colored hair, brass-colored beard", "Brass-colored hair, fiery-colored beard", "Brass-colored hair, literal fire for beard",
                    "Fiery-colored hair, brass-colored beard", "Fiery-colored hair, fiery-colored beard", "Fiery-colored hair, literal fire for beard",
                    "Literal fire for hair, brass-colored beard", "Literal fire for hair, fiery-colored beard", "Literal fire for hair, literal fire for beard" },
                allEyes: new[] { "TODO Dwarven eyes" });
            //Source: https://forgottenrealms.fandom.com/wiki/Babau
            appearances[CreatureConstants.Babau] = new[] { "Black, leathery skin tight to a gaunt, skeletal frame. Terrible stench. Skull is tall and long with pointed ears and jaws filled with jagged teeth. A single long horn juts from the back of the skull, curving forward and downward. White eyes that glow red when glaring." };
            //Source: https://www.dimensions.com/element/rhesus-macaque-macaca-mulatta
            appearances[CreatureConstants.Baboon] = new[] { "Brown hair, red face" };
            //Source: https://animals.sandiegozoo.org/animals/honey-badger-ratel
            appearances[CreatureConstants.Badger] = new[] { "Thick, coarse, black fur, with a wide gray-white stripe that stretches across the back, from the top of the head to the tip of the tail" };
            //Multiplying up from normal badger. Length is about x2 from normal low, 2.5x for high
            appearances[CreatureConstants.Badger_Dire] = GetWeightedAppearances(
                commonHair: new[] { "Thick brown fur" },
                uncommonHair: new[] { "Thick grey fur", "Thick tan fur" });
            //Source: https://forgottenrealms.fandom.com/wiki/Balor
            appearances[CreatureConstants.Balor] = new[] { "Dark red skin wrapped in glaring flames. Venom-dripping fangs. Fearsome claws." };
            //Source: https://forgottenrealms.fandom.com/wiki/Hamatula
            appearances[CreatureConstants.BarbedDevil_Hamatula] = new[] { "Body completely covered in barbs. Horned hands ending in strange, lengthy claws. Long, burly tails coated in spines. Gleaming, vigilant eyes constantly shifting, making it seem anxious" };
            //Source: https://forgottenrealms.fandom.com/wiki/Barghest
            appearances[CreatureConstants.Barghest] = GetWeightedAppearances(
                commonHair: new[] { "Bluish-red fur", "Blue fur" },
                commonEyes: new[] { "TODO Dog eyes. Glowing orange when excited",
                    "TODO Goblin eyes. Glowing orange when excited" },
                uncommonHair: new[] { "Bluish-red fur, shock of white hair along the back", "Blue fur, shock of white hair along the back" },
                uncommonEyes: new[] { "TODO Dog eyes/Goblin eyes mix-match (one eye discolored). Glowing orange when excited." });
            appearances[CreatureConstants.Barghest_Greater] = GetWeightedAppearances(
                commonHair: new[] { "Bluish-red fur", "Blue fur" },
                commonEyes: new[] { "TODO Dog eyes. Glowing orange when excited",
                    "TODO Goblin eyes. Glowing orange when excited" },
                uncommonHair: new[] { "Bluish-red fur, shock of white hair along the back", "Blue fur, shock of white hair along the back" },
                uncommonEyes: new[] { "TODO Dog eyes/Goblin eyes mix-match (one eye discolored). Glowing orange when excited." });
            //Source: https://forgottenrealms.fandom.com/wiki/Basilisk
            appearances[CreatureConstants.Basilisk] = GetWeightedAppearances(
                commonSkin: new[] { "Dull brown skin, yellowish underbelly" },
                allEyes: new[] { "Glowing pale green eyes" },
                uncommonSkin: new[] { "Dark gray skin, yellowish underbelly", "Dark yellow skin, yellowish underbelly", "Dark orange skin, yellowish underbelly" },
                commonOther: new[] { "Single row of bony spikes lining the back" },
                rareOther: new[] { "Single row of bony spikes lining the back. Curved horn atop the nose" });
            appearances[CreatureConstants.Basilisk_Greater] = GetWeightedAppearances(
                commonSkin: new[] { "Dull brown skin, yellowish underbelly" },
                allEyes: new[] { "Glowing pale green eyes" },
                uncommonSkin: new[] { "Dark gray skin, yellowish underbelly", "Dark yellow skin, yellowish underbelly", "Dark orange skin, yellowish underbelly" },
                commonOther: new[] { "Single row of bony spikes lining the back" },
                rareOther: new[] { "Single row of bony spikes lining the back. Curved horn atop the nose" });
            //Source: https://www.dimensions.com/element/little-brown-bat-myotis-lucifugus hanging height
            appearances[CreatureConstants.Bat] = GetWeightedAppearances(
                commonSkin: new[] { "Pale red fur", "Pale tan fur", "Red fur", "Tan fur", "Brown fur", "Dark brown fur" },
                allOther: new[] { "Has a short snout and sloped forehead" });
            //Scaled up from bat, x15 based on wingspan
            appearances[CreatureConstants.Bat_Dire] = new[] { "Shaggy, pale red fur", "Shaggy, pale tan fur", "Shaggy, red fur", "Shaggy, tan fur", "Shaggy, brown fur",
                "Shaggy, dark brown fur" };
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            appearances[CreatureConstants.Bat_Swarm] = new[] { "5,000 bats" };
            //Source: https://forgottenrealms.fandom.com/wiki/Black_bear
            appearances[CreatureConstants.Bear_Black] = new[] { "Black fur", "Blond fur", "Cinnamon fur" };
            //Source: https://forgottenrealms.fandom.com/wiki/Brown_bear
            appearances[CreatureConstants.Bear_Brown] = new[] { "Brown fur" };
            //Source: https://forgottenrealms.fandom.com/wiki/Dire_bear
            appearances[CreatureConstants.Bear_Dire] = new[] { "Brown fur. Bony brow ridges and extra-long claws. A feral face with cold, piecing eyes." };
            //Source: https://forgottenrealms.fandom.com/wiki/Polar_bear
            appearances[CreatureConstants.Bear_Polar] = new[] { "White fur" };
            //Source: https://forgottenrealms.fandom.com/wiki/Barbazu
            appearances[CreatureConstants.BeardedDevil_Barbazu] = GetWeightedAppearances(
                commonSkin: new[] { "Green skin covered in scales" },
                uncommonSkin: new[] { "Red skin covered in scales", "Purple skin covered in scales" },
                commonOther: new[] { "Disgusting, wiry beard. Pointed ears and long tail. Clawed hands and feet." },
                uncommonOther: new[] { "Disgusting, wiry beard infested with maggots. Pointed ears and long tail. Clawed hands and feet." });
            appearances[CreatureConstants.Bebilith] = new[] { "Like a massive, misshapen spider. Tough, chitinous, mottled exoskeleton. Two forelegs end in razor-sharp barbs. Fangs drip globs of odorous liquid." };
            //Source: https://www.d20srd.org/srd/monsters/giantBee.htm
            //https://www.dimensions.com/element/western-honey-bee-apis-mellifera scale up, [.12,.2]*5*12/[.39,.59] = [18,21]
            appearances[CreatureConstants.Bee_Giant] = new[] { "Alternating black and yellow stripes" };
            //Source: https://forgottenrealms.fandom.com/wiki/Behir
            appearances[CreatureConstants.Behir] = new[] { "Deep blue scales, lighter on the underside", "Dark blue scales, lighter on the underside",
                    "Ultramarine scales, lighter on the underside",
                "Deep blue scales with gray-brown bands, lighter on the underside", "Dark blue scales with gray-brown bands, lighter on the underside",
                    "Ultramarine scales with gray-brown bands, lighter on the underside",
                "Deep blue scales with brown bands, lighter on the underside", "Dark blue scales with brown bands, lighter on the underside",
                    "Ultramarine scales with brown bands, lighter on the underside",
                "Deep blue scales with gray bands, lighter on the underside", "Dark blue scales with gray bands, lighter on the underside",
                    "Ultramarine scales with gray bands, lighter on the underside" };
            //Source: https://forgottenrealms.fandom.com/wiki/Beholder
            appearances[CreatureConstants.Beholder] = GetWeightedAppearances(
                commonSkin: new[] { "Purple, pebbly-textured skin on top graduating to earth tones further down",
                    "Blue, pebbly-textured skin on top graduating to earth tones further down",
                    "Brown, pebbly-textured skin on top graduating to earth tones further down",
                    "Blue-purple, pebbly-textured skin on top graduating to earth tones further down" },
                uncommonSkin: new[] { "White, pebbly-textured skin on top graduating to earth tones further down",
                    "Red, pebbly-textured skin on top graduating to earth tones further down",
                    "Grey, pebbly-textured skin on top graduating to earth tones further down",
                    "Green, pebbly-textured skin on top graduating to earth tones further down" },
                commonOther: new[] { "A giant eye surrounded by smaller eye stalks. Has a massive, gaping maw." },
                uncommonOther: new[] { "A giant eye surrounded by smaller eye stalks. Has a massive, gaping maw. Nostrils and jointed, articulated eyestalks." });
            //Source: https://forgottenrealms.fandom.com/wiki/Gauth
            appearances[CreatureConstants.Beholder_Gauth] = GetWeightedAppearances(
                commonSkin: new[] { "Brown skin, mottled with purple and gray", "Brown skin, mottled with purple", "Brown skin, mottled with gray",
                    "Purple skin, mottled with brown and gray", "Purple skin, mottled with brown", "Purple skin, mottled with gray",
                    "Gray skin, mottled with purple and brown", "Gray skin, mottled with purple", "Gray skin, mottled with brown" },
                uncommonSkin: new[] { "Purple, pebbly-textured skin on top graduating to earth tones further down",
                    "Blue, pebbly-textured skin on top graduating to earth tones further down",
                    "Brown, pebbly-textured skin on top graduating to earth tones further down",
                    "Blue-purple, pebbly-textured skin on top graduating to earth tones further down" },
                rareSkin: new[] { "White, pebbly-textured skin on top graduating to earth tones further down",
                    "Red, pebbly-textured skin on top graduating to earth tones further down",
                    "Grey, pebbly-textured skin on top graduating to earth tones further down",
                    "Green, pebbly-textured skin on top graduating to earth tones further down" },
                commonOther: new[] { "A giant eye surrounded by smaller eye stalks, along with feeding tendrils and sprout from the lower half of its body. The central eye is surrounded by a ridge of flesh and smaller eyes used for sight. Has a massive, gaping maw." },
                uncommonOther: new[] { "A giant eye surrounded by smaller eye stalks, along with feeding tendrils and sprout from the lower half of its body. The central eye is surrounded by a ridge of flesh and smaller eyes used for sight. Has a massive, gaping maw. Nostrils and jointed, articulated eyestalks." });
            //Source: https://forgottenrealms.fandom.com/wiki/Belker
            appearances[CreatureConstants.Belker] = new[] { "Primarily composed of smoke, has large, black, bat-like wings, clawed tendrils, and a biting maw. Base form was sort of deomonic, but continually shifts and changes shape due to its semi-gaseous nature." };
            //Source: https://forgottenrealms.fandom.com/wiki/Bison
            appearances[CreatureConstants.Bison] = new[] { "Distinctive broad head that connects to large, humped shoulders with a thick neck. Short, curved, hollow horns growing from the sides of its head. Body covered in fur. Head covered in a mantle of thick fur. Short legs. Small tail ending in a tassel of fur.",
                "TODO: Gender-specific appearance" };
            //Source: https://forgottenrealms.fandom.com/wiki/Black_pudding
            appearances[CreatureConstants.BlackPudding] = GetWeightedAppearances(
                commonSkin: new[] { "Inky black skin" },
                uncommonSkin: new[] { "Brown skin", "Grey skin", "White skin" });
            appearances[CreatureConstants.BlackPudding_Elder] = GetWeightedAppearances(
                commonSkin: new[] { "Inky black skin" },
                uncommonSkin: new[] { "Brown skin", "Grey skin", "White skin" });
            //Source: https://www.d20pfsrd.com/bestiary/monster-listings/magical-beasts/blink-dog
            appearances[CreatureConstants.BlinkDog] = GetWeightedAppearances(
                commonHair: new[] { "Yellow-brown fur" },
                uncommonHair: new[] { "Yellow fur", "Brown fur" },
                allOther: new[] { "Large ears" });
            //Source: https://forgottenrealms.fandom.com/wiki/Boar
            appearances[CreatureConstants.Boar] = GetWeightedAppearances(
                allHair: new[] { "Thick brown fur", "Thick gray fur" },
                allOther: new[] { "Sharp tusks jutting out from beneath the snout" });
            //Source: https://forgottenrealms.fandom.com/wiki/Dire_boar
            appearances[CreatureConstants.Boar_Dire] = GetWeightedAppearances(
                commonHair: new[] { "Thick brown fur", "Thick gray fur" },
                uncommonHair: new[] { "Thick black fur" },
                allOther: new[] { "Sharp tusks jutting out from beneath the snout" });
            //Source: https://forgottenrealms.fandom.com/wiki/Bodak
            appearances[CreatureConstants.Bodak] = GetWeightedAppearances(
                allSkin: new[] { "Deathly pale white skin", "Deathly gray skin", "Deathly pale gray skin" },
                allHair: new[] { "No hair of any kind" },
                allEyes: new[] { "Empty, milky-white eyes outstretched into vertical ovals" },
                allOther: new[] { "Face twisted into an inhuman visage of sheer madness and horror" });
            //Source: https://web.stanford.edu/~cbross/bombbeetle.html
            appearances[CreatureConstants.BombardierBeetle_Giant] = new[] { "Body is dull blue-gray. Head and chest is brownish-orange." };
            //Source: https://forgottenrealms.fandom.com/wiki/Osyluth
            appearances[CreatureConstants.BoneDevil_Osyluth] = new[] { "Dry, sickly skin that seems tautly stretched over every bone in its body. Incredibly gaunt husk of a frame so emaciated that it seems skeletal. Tail of a giant scorpion while head looks like menacing skulls. The putrid stench of rot surrounds it." };
            //Source: https://forgottenrealms.fandom.com/wiki/Bralani
            appearances[CreatureConstants.Bralani] = GetWeightedAppearances(
                commonSkin: new[] { "TODO High elf skin" },
                uncommonSkin: new[] { "TODO Grey elf skin", "TODO Wild elf skin", "TODO Wood elf skin" },
                allHair: new[] { "Silvery-white hair" },
                allEyes: new[] { "Eyes are an ever-changing rainbow of colors, depending on its mood" },
                allOther: new[] { "Broad and stocky" });
            //Source: https://forgottenrealms.fandom.com/wiki/Bugbear
            appearances[CreatureConstants.Bugbear] = GetWeightedAppearances(
                commonSkin: new[] { "Yellow skin", "Reddish-yellow skin", "Yellowish-brown skin", "Reddish-brown skin" },
                uncommonSkin: new[] { "Red skin", "Brown skin" },
                allHair: new[] { "Brown hair", "Red hair" },
                allEyes: new[] { "Yellow eyes", "Orange eyes", "Red eyes", "Brown eyes", "Greenish-white eyes" },
                allOther: new[] { "Large, hairy, wedge-shaped ears; tough hide; claws" });
            //Source: https://forgottenrealms.fandom.com/wiki/Bulette
            appearances[CreatureConstants.Bulette] = GetWeightedAppearances(
                allSkin: new[] { "Around the head and rear, the armor is blue-brown. In between, it is gray-blue. Slightly darker skin around the eyes",
                    "Around the head and rear, the armor is blue-brown. In between, it is blue. Slightly darker skin around the eyes",
                    "Around the head and rear, the armor is blue-brown. In between, it is blue-green. Slightly darker skin around the eyes" },
                allEyes: new[] { "Yellow eyes with blue-green pupils" },
                allOther: new[] { "Covered in thick, layered plates. Hea dis bullet-shaped, similar to a shark's, with a massive mouth. Stumpy but powerful legs." });
            //Source: https://forgottenrealms.fandom.com/wiki/Camel
            appearances[CreatureConstants.Camel_Bactrian] = GetWeightedAppearances(
                allHair: new[] { "White fur", "Pale tan fur", "Deep brown fur", "Tan fur", "Brown fur" },
                allOther: new[] { "Two humps. Broad feet" });
            appearances[CreatureConstants.Camel_Dromedary] = GetWeightedAppearances(
                allHair: new[] { "White fur", "Pale tan fur", "Deep brown fur", "Tan fur", "Brown fur" },
                allOther: new[] { "One hump. Broad feet" });
            //Source: https://forgottenrealms.fandom.com/wiki/Carrion_crawler
            appearances[CreatureConstants.CarrionCrawler] = GetWeightedAppearances(
                allSkin: new[] { "Pale yellow skin", "Green skin, pale yellow underbelly", "Green skin" },
                allEyes: new[] { "Two eye stalks" },
                allOther: new[] { "Eight long tentacles protruding from the side of the head" });
            //Source: https://g.co/kgs/eqa8L1
            //https://www.omlet.us/guide/cats/choosing_the_right_cat_for_you/cat_coat_colors_and_patterns/
            appearances[CreatureConstants.Cat] = new[] { "Siamese", "British Shorthair", "Maine Coon", "Persian", "Ragdoll", "Sphynx", "American Shorthair", "Abyssinian",
                "Exotic Shorthair", "Scottish Fold", "Burmese", "Birman", "Bombay", "Siberian", "Norwegian Forest", "Russian Blue", "American Curl", "Munchkin",
                "American Bobtail", "Devon Rex", "Balinese", "Oriental Shorthair", "Chartreux", "Turkish Angora", "Manx", "Japanese Bobtail", "American Wirehair",
                "Ragamuffin", "Egyptian Mau", "Cornish Rex", "Somali", "Himalayan", "Selkirk Rex", "Korat", "Singapura", "Ocicat", "Tonkinese", "Turkish Van",
                "British Longhair", "LaPerm", "Havana Brown", "Chausie", "Burmilla", "Toyger", "Sokoke", "Colorpoint Shorthair", "Javanese", "Snowshoe", "Australian Mist",
                "Lykoi", "Khao Manee",
                "Solid Black", "Solid White", "Solid Ginger", "Solid Blue/Gray", "Solid Cream", "Solid Brown", "Solid Cinnamon", "Solid Fawn",
                "Black/White Tuxedo", "Black/Gray Tuxedo", "Black/Cream Tuxedo", "Black/Fawn Tuxedo",
                    "Brown/White Tuxedo", "Brown/Gray Tuxedo", "Brown/Cream Tuxedo", "Brown/Fawn Tuxedo",
                    "Cinnamon/White Tuxedo", "Cinnamon/Gray Tuxedo", "Cinnamon/Cream Tuxedo", "Cinnamon/Fawn Tuxedo",
                    "Ginger/White Tuxedo", "Ginger/Gray Tuxedo", "Ginger/Cream Tuxedo", "Ginger/Fawn Tuxedo",
                "Black/White Magpie Bi-color", "Black/Gray Magpie Bi-color", "Black/Cream Magpie Bi-color", "Black/Fawn Magpie Bi-color",
                    "Brown/White Magpie Bi-color", "Brown/Gray Magpie Bi-color", "Brown/Cream Magpie Bi-color", "Brown/Fawn Magpie Bi-color",
                    "Cinnamon/White Magpie Bi-color", "Cinnamon/Gray Magpie Bi-color", "Cinnamon/Cream Magpie Bi-color", "Cinnamon/Fawn Magpie Bi-color",
                    "Ginger/White Magpie Bi-color", "Ginger/Gray Magpie Bi-color", "Ginger/Cream Magpie Bi-color", "Ginger/Fawn Magpie Bi-color",
                "Black/White Harlequin Bi-color", "Black/Gray Harlequin Bi-color", "Black/Cream Harlequin Bi-color", "Black/Fawn Harlequin Bi-color",
                    "Brown/White Harlequin Bi-color", "Brown/Gray Harlequin Bi-color", "Brown/Cream Harlequin Bi-color", "Brown/Fawn Harlequin Bi-color",
                    "Cinnamon/White Harlequin Bi-color", "Cinnamon/Gray Harlequin Bi-color", "Cinnamon/Cream Harlequin Bi-color", "Cinnamon/Fawn Harlequin Bi-color",
                    "Ginger/White Harlequin Bi-color", "Ginger/Gray Harlequin Bi-color", "Ginger/Cream Harlequin Bi-color", "Ginger/Fawn Harlequin Bi-color",
                "Black/White Cap-And-Saddle Bi-color", "Black/Gray Cap-And-Saddle Bi-color", "Black/Cream Cap-And-Saddle Bi-color", "Black/Fawn Cap-And-Saddle Bi-color",
                    "Brown/White Cap-And-Saddle Bi-color", "Brown/Gray Cap-And-Saddle Bi-color", "Brown/Cream Cap-And-Saddle Bi-color", "Brown/Fawn Cap-And-Saddle Bi-color",
                    "Cinnamon/White Cap-And-Saddle Bi-color", "Cinnamon/Gray Cap-And-Saddle Bi-color", "Cinnamon/Cream Cap-And-Saddle Bi-color", "Cinnamon/Fawn Cap-And-Saddle Bi-color",
                    "Ginger/White Cap-And-Saddle Bi-color", "Ginger/Gray Cap-And-Saddle Bi-color", "Ginger/Cream Cap-And-Saddle Bi-color", "Ginger/Fawn Cap-And-Saddle Bi-color",
                "Black/White Van Bi-color", "Black/Gray Van Bi-color", "Black/Cream Van Bi-color", "Black/Fawn Van Bi-color",
                    "Brown/White Van Bi-color", "Brown/Gray Van Bi-color", "Brown/Cream Van Bi-color", "Brown/Fawn Van Bi-color",
                    "Cinnamon/White Van Bi-color", "Cinnamon/Gray Van Bi-color", "Cinnamon/Cream Van Bi-color", "Cinnamon/Fawn Van Bi-color",
                    "Ginger/White Van Bi-color", "Ginger/Gray Van Bi-color", "Ginger/Cream Van Bi-color", "Ginger/Fawn Van Bi-color",
                "Black Striped Tabby", "White Striped Tabby", "Ginger Striped Tabby", "Blue/Gray Striped Tabby", "Cream Striped Tabby", "Brown Striped Tabby",
                    "Cinnamon Striped Tabby", "Fawn Striped Tabby",
                "Black Blotched Tabby", "White Blotched Tabby", "Ginger Blotched Tabby", "Blue/Gray Blotched Tabby", "Cream Blotched Tabby", "Brown Blotched Tabby",
                    "Cinnamon Blotched Tabby", "Fawn Blotched Tabby",
                "Black Spotted Tabby", "White Spotted Tabby", "Ginger Spotted Tabby", "Blue/Gray Spotted Tabby", "Cream Spotted Tabby", "Brown Spotted Tabby",
                    "Cinnamon Spotted Tabby", "Fawn Spotted Tabby",
                "Black Ticked Tabby", "White Ticked Tabby", "Ginger Ticked Tabby", "Blue/Gray Ticked Tabby", "Cream Ticked Tabby", "Brown Ticked Tabby",
                    "Cinnamon Ticked Tabby", "Fawn Ticked Tabby",
                "Black/Ginger Brindled Tortoiseshell", "Blue/Ginger Brindled Tortoiseshell", "Cream/Ginger Brindled Tortoiseshell",
                "Black/Ginger Patchy Tortoiseshell", "Blue/Ginger Patchy Tortoiseshell", "Cream/Ginger Patchy Tortoiseshell",
                "Black/Ginger Brindled Torbie", "Blue/Ginger Brindled Torbie", "Cream/Ginger Brindled Torbie",
                "Black/Ginger Patchy Torbie", "Blue/Ginger Patchy Torbie", "Cream/Ginger Patchy Torbie",
                "Ginger/Black/White Callico", "Ginger/Gray/White Callico", "Ginger/Cream/White Callico",
                "Black Colorpoint", "White Colorpoint", "Ginger Colorpoint", "Blue/Gray Colorpoint", "Cream Colorpoint", "Brown Colorpoint", "Cinnamon Colorpoint",
                    "Fawn Colorpoint" };
            //Source: https://forgottenrealms.fandom.com/wiki/Centaur
            appearances[CreatureConstants.Centaur] = GetWeightedAppearances(
                allSkin: new[] { "TODO Human skin", "TODO Light Horse skin", "TODO Heavy Horse skin" },
                allHair: new[] { "TODO Human hair", "TODO Light Horse hair", "TODO Heavy Horse hair" },
                allEyes: new[] { "TODO Human eyes" });
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Centipede,_Tiny
            appearances[CreatureConstants.Centipede_Monstrous_Tiny] = new[] { "Black skin", "Brown skin", "Grey skin", "Red skin", "Pale grey skin" };
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Centipede,_Small
            appearances[CreatureConstants.Centipede_Monstrous_Small] = new[] { "Black skin", "Brown skin", "Grey skin", "Red skin", "Pale grey skin" };
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Centipede,_Medium
            appearances[CreatureConstants.Centipede_Monstrous_Medium] = new[] { "Black skin", "Brown skin", "Grey skin", "Red skin", "Pale grey skin" };
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Centipede,_Large
            appearances[CreatureConstants.Centipede_Monstrous_Large] = new[] { "Black skin", "Brown skin", "Grey skin", "Red skin", "Pale grey skin" };
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Centipede,_Huge
            appearances[CreatureConstants.Centipede_Monstrous_Huge] = new[] { "Black skin", "Brown skin", "Grey skin", "Red skin", "Pale grey skin" };
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Centipede,_Gargantuan
            appearances[CreatureConstants.Centipede_Monstrous_Gargantuan] = new[] { "Black skin", "Brown skin", "Grey skin", "Red skin", "Pale grey skin" };
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Centipede,_Colossal
            appearances[CreatureConstants.Centipede_Monstrous_Colossal] = new[] { "Black skin", "Brown skin", "Grey skin", "Red skin", "Pale grey skin" };
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            appearances[CreatureConstants.Centipede_Swarm] = new[] { "1,500 centipedes" };
            //Source: https://www.d20srd.org/srd/monsters/devil.htm#chainDevilKyton
            appearances[CreatureConstants.ChainDevil_Kyton] = new[] { "Red skin", "Grey skin", "Grey skin with red tattoos" };
            //Source: https://forgottenrealms.fandom.com/wiki/Chaos_beast
            appearances[CreatureConstants.ChaosBeast] = new[] {
                "A towering thing of pulpy flesh and exposed veins, covered in fangs and hooks. Three legs upon which it walks with unsteady and lurching steps. Face is fractured, with bent eyes and nose hooked three times.",
                "A filmy mass of rope-like tentacles with vermilion tips that slither along. Bulbous body ringed by gnashing mouths and topped by a viscous sac holding ten bobbing eyes. Dozens of vestigial wings that flap too feebly to give it flight.",
                "A powerful and muscular beast, armed with claws and alligator jaws.",
                "A graceful six-legged creature with delicate arms and shining skin. Maned head with three flashing eyes. Walks with a smooth gait and noble bearing.",
                "A man-sized creature with stumpy feet - can only shuffle. Hands like crab's claws. Body like dough falling from broken bones. Head is balding and pitted with empty eye sockets.",
                "A slender raptor-like creature with large talons underneath its body and broad wings, all covered with dark fur. Two hateful, coldly-gleaming eyes.",
                "A sac of intestines, warm and wet and steaming. Both deaf and blind. To move, it rolls and flops like limp sausages falling down stairs, becoming looped and draped around all obstacles and lying as a tangled sprawl.",
                "A hollowed-out carcass, swelling with gases into a balloon-like husk, bobbing along in the air with the ends of its body trailing about it and tangling others.",
                "A plump moth with brilliantly colored wings like the stained glass of temples and compound eyes that sparkle like a myriad of jewels.",
                "The person one least suspects.",
                "A writhing mass of teeth and barbed tentacles, grey and rotting. Random clawed hands hide under some of the tentacles.",
                "DM Discretion - come up with something particularly chaotic" };
            //Source: https://www.dimensions.com/element/cheetahs
            //https://forgottenrealms.fandom.com/wiki/Cheetah
            appearances[CreatureConstants.Cheetah] = new[] { "Slender body, yellow coat with black spots, long thin legs, deep chest, small head",
                "Slender body, sand-colored coat with black spots, long thin legs, deep chest, small head",
                "Slender body, yellow coat with brown spots, long thin legs, deep chest, small head",
                "Slender body, sand-colored coat with brown spots, long thin legs, deep chest, small head" };
            //Source: https://forgottenrealms.fandom.com/wiki/Chimera
            appearances[CreatureConstants.Chimera_Black] = new[] { "Hindquarters of a goat, the forequarters of a lion, and a set of Black Dragon wings. Three heads: that of a horned goat, a lion, and a Black Dragon." };
            appearances[CreatureConstants.Chimera_Blue] = new[] { "Hindquarters of a goat, the forequarters of a lion, and a set of Blue Dragon wings. Three heads: that of a horned goat, a lion, and a Blue Dragon." };
            appearances[CreatureConstants.Chimera_Green] = new[] { "Hindquarters of a goat, the forequarters of a lion, and a set of Green Dragon wings. Three heads: that of a horned goat, a lion, and a Green Dragon." };
            appearances[CreatureConstants.Chimera_Red] = new[] { "Hindquarters of a goat, the forequarters of a lion, and a set of Red Dragon wings. Three heads: that of a horned goat, a lion, and a Red Dragon." };
            appearances[CreatureConstants.Chimera_White] = new[] { "Hindquarters of a goat, the forequarters of a lion, and a set of White Dragon wings. Three heads: that of a horned goat, a lion, and a White Dragon." };
            //Source: https://forgottenrealms.fandom.com/wiki/Choker
            appearances[CreatureConstants.Choker] = GetWeightedAppearances(
                allSkin: new[] { "Gray, rubbery skin", "Spotted gray, rubbery skin", "Earthy-brown, rubbery skin", "Spotted earthy-brown, rubbery skin" },
                allOther: new[] { "Roughly humanoid, with a small torso and head. Arms and legs are long and thin, more like tentacles. Hands are shaped somewhat like a starfish, with a spiny surface—stiff cartilage jutting through the skin. Fingers are enormously long. Limbs move with the fluidity of tentacles." });
            //Source: https://forgottenrealms.fandom.com/wiki/Chuul
            appearances[CreatureConstants.Chuul] = GetWeightedAppearances(
                allSkin: new[] { "Dark yellow-green exoskeleton" },
                allEyes: new[] { "Black eyes" },
                allOther: new[] { "Four long legs and two large crustacean-like claws. Fan-like tail. Mass of tentacles around the mouth." });
            //Source: https://forgottenrealms.fandom.com/wiki/Cloaker
            appearances[CreatureConstants.Cloaker] = GetWeightedAppearances(
                allSkin: new[] { "Black skin, pale belly" },
                allEyes: new[] { "Red eyes" },
                commonOther: new[] { "Long, whiplike tail. Head set within the \"cowl\" of the cloak-like body. Toothy maw. Bony claws adjacent to the head look almost like clasps for the cloak." },
                uncommonOther: new[] { "Long, whiplike tail. Head set within the \"cowl\" of the cloak-like body. Toothy maw. Bony claws adjacent to the head look almost like clasps for the cloak. A series of spots run in two parallel lines down the backs, giving the appearance of buttons." });
            //Source: https://forgottenrealms.fandom.com/wiki/Cockatrice
            appearances[CreatureConstants.Cockatrice] = GetWeightedAppearances(
                allSkin: new[] { "Yellow-green scales on the tail. Grey skin on the wings. Yellow beak and feet." },
                allHair: new[] { "Golden brown feathers" },
                allEyes: new[] { "Glowing crimson eyes" },
                allOther: new[] { "Lizard-like tail. Wings of a bat.", "TODO gender-specific appearance" });
            //Source: https://forgottenrealms.fandom.com/wiki/Couatl
            appearances[CreatureConstants.Couatl] = new[] { "Long feathered serpent with a pair of rainbow-feathered wings." };
            //Source: https://www.mojobob.com/roleplay/monstrousmanual/s/sphinx.html
            appearances[CreatureConstants.Criosphinx] = new[] { "Falcon-like wings, ram-like head. Tawny lion body. Mouth full of sharp teeth." };
            //Source: https://en.wikipedia.org/wiki/Nile_crocodile#Characteristics_and_physiology
            appearances[CreatureConstants.Crocodile] = new[] { "Dark bronze colouration above, with faded blackish spots and stripes variably appearing across the back and a dingy off-yellow on the belly, although mud can often obscure the crocodile's actual colour.[19] The flanks, which are yellowish-green in colour, have dark patches arranged in oblique stripes in highly variable patterns. Some variation occurs relative to environment; specimens from swift-flowing waters tend to be lighter in colour than those dwelling in murkier lakes or swamps, which provides camouflage that suits their environment, an example of clinal variation. Nile crocodiles have green eyes" };
            //Source: https://en.wikipedia.org/wiki/Saltwater_crocodile#Description
            appearances[CreatureConstants.Crocodile_Giant] = GetWeightedAppearances(
                commonSkin: new[] {
                    "Darker greenish-drab skin. The ventral surface is white. Stripes are present on the lower sides of the body, but do not extend onto the belly. Tail is grey with dark bands.",
                    "Darker greenish-drab skin. The ventral surface is yellow. Stripes are present on the lower sides of the body, but do not extend onto the belly. Tail is grey with dark bands.",
                },
                uncommonSkin: new[] {
                    "Darker greenish-drab skin, with a few lighter tan areas. The ventral surface is white. Stripes are present on the lower sides of the body, but do not extend onto the belly. Tail is grey with dark bands.",
                    "Darker greenish-drab skin, with a few lighter tan areas. The ventral surface is yellow. Stripes are present on the lower sides of the body, but do not extend onto the belly. Tail is grey with dark bands.",
                    "Darker greenish-drab skin, with a few lighter grey areas. The ventral surface is white. Stripes are present on the lower sides of the body, but do not extend onto the belly. Tail is grey with dark bands.",
                    "Darker greenish-drab skin, with a few lighter grey areas. The ventral surface is yellow. Stripes are present on the lower sides of the body, but do not extend onto the belly. Tail is grey with dark bands.",
                    "Pale greenish-drab skin. The ventral surface is white. Stripes are present on the lower sides of the body, but do not extend onto the belly. Tail is grey with dark bands.",
                    "Pale greenish-drab skin. The ventral surface is yellow. Stripes are present on the lower sides of the body, but do not extend onto the belly. Tail is grey with dark bands.",
                    "Dark black-green skin. The ventral surface is white. Stripes are present on the lower sides of the body, but do not extend onto the belly. Tail is grey with dark bands.",
                    "Dark black-green skin. The ventral surface is yellow. Stripes are present on the lower sides of the body, but do not extend onto the belly. Tail is grey with dark bands.",
                    "Dark blackish skin. The ventral surface is white. Stripes are present on the lower sides of the body, but do not extend onto the belly. Tail is grey with dark bands.",
                    "Dark blackish skin. The ventral surface is yellow. Stripes are present on the lower sides of the body, but do not extend onto the belly. Tail is grey with dark bands.",
                },
                rareSkin: new[] {
                    "Pale greenish-drab skin, with a few lighter tan areas. The ventral surface is white. Stripes are present on the lower sides of the body, but do not extend onto the belly. Tail is grey with dark bands.",
                    "Pale greenish-drab skin, with a few lighter tan areas. The ventral surface is yellow. Stripes are present on the lower sides of the body, but do not extend onto the belly. Tail is grey with dark bands.",
                    "Pale greenish-drab skin, with a few lighter grey areas. The ventral surface is white. Stripes are present on the lower sides of the body, but do not extend onto the belly. Tail is grey with dark bands.",
                    "Pale greenish-drab skin, with a few lighter grey areas. The ventral surface is yellow. Stripes are present on the lower sides of the body, but do not extend onto the belly. Tail is grey with dark bands.",
                    "Dark black-green skin, with a few lighter tan areas. The ventral surface is white. Stripes are present on the lower sides of the body, but do not extend onto the belly. Tail is grey with dark bands.",
                    "Dark black-green skin, with a few lighter tan areas. The ventral surface is yellow. Stripes are present on the lower sides of the body, but do not extend onto the belly. Tail is grey with dark bands.",
                    "Dark black-green skin, with a few lighter grey areas. The ventral surface is white. Stripes are present on the lower sides of the body, but do not extend onto the belly. Tail is grey with dark bands.",
                    "Dark black-green skin, with a few lighter grey areas. The ventral surface is yellow. Stripes are present on the lower sides of the body, but do not extend onto the belly. Tail is grey with dark bands.",
                    "Dark blackish skin, with a few lighter tan areas. The ventral surface is white. Stripes are present on the lower sides of the body, but do not extend onto the belly. Tail is grey with dark bands.",
                    "Dark blackish skin, with a few lighter tan areas. The ventral surface is yellow. Stripes are present on the lower sides of the body, but do not extend onto the belly. Tail is grey with dark bands.",
                    "Dark blackish skin, with a few lighter grey areas. The ventral surface is white. Stripes are present on the lower sides of the body, but do not extend onto the belly. Tail is grey with dark bands.",
                    "Dark blackish skin, with a few lighter grey areas. The ventral surface is yellow. Stripes are present on the lower sides of the body, but do not extend onto the belly. Tail is grey with dark bands.",
                });
            //Source: https://forgottenrealms.fandom.com/wiki/Cryohydra
            appearances[CreatureConstants.Cryohydra_5Heads] = GetWeightedAppearances(
                allSkin: new[] { "Purplish-brown skin", "Brown skin", "Dark brown skin", "Dark purplish-brown skin" },
                allEyes: new[] { "Amber eyes" });
            appearances[CreatureConstants.Cryohydra_6Heads] = GetWeightedAppearances(
                allSkin: new[] { "Purplish-brown skin", "Brown skin", "Dark brown skin", "Dark purplish-brown skin" },
                allEyes: new[] { "Amber eyes" });
            appearances[CreatureConstants.Cryohydra_7Heads] = GetWeightedAppearances(
                allSkin: new[] { "Purplish-brown skin", "Brown skin", "Dark brown skin", "Dark purplish-brown skin" },
                allEyes: new[] { "Amber eyes" });
            appearances[CreatureConstants.Cryohydra_8Heads] = GetWeightedAppearances(
                allSkin: new[] { "Purplish-brown skin", "Brown skin", "Dark brown skin", "Dark purplish-brown skin" },
                allEyes: new[] { "Amber eyes" });
            appearances[CreatureConstants.Cryohydra_9Heads] = GetWeightedAppearances(
                allSkin: new[] { "Purplish-brown skin", "Brown skin", "Dark brown skin", "Dark purplish-brown skin" },
                allEyes: new[] { "Amber eyes" });
            appearances[CreatureConstants.Cryohydra_10Heads] = GetWeightedAppearances(
                allSkin: new[] { "Purplish-brown skin", "Brown skin", "Dark brown skin", "Dark purplish-brown skin" },
                allEyes: new[] { "Amber eyes" });
            appearances[CreatureConstants.Cryohydra_11Heads] = GetWeightedAppearances(
                allSkin: new[] { "Purplish-brown skin", "Brown skin", "Dark brown skin", "Dark purplish-brown skin" },
                allEyes: new[] { "Amber eyes" });
            appearances[CreatureConstants.Cryohydra_12Heads] = GetWeightedAppearances(
                allSkin: new[] { "Purplish-brown skin", "Brown skin", "Dark brown skin", "Dark purplish-brown skin" },
                allEyes: new[] { "Amber eyes" });
            //Source: https://forgottenrealms.fandom.com/wiki/Darkmantle
            appearances[CreatureConstants.Darkmantle] = GetWeightedAppearances(
                commonSkin: new[] { "Limestone-colored skin" },
                uncommonSkin: new[] { "Dolomite-colored skin", "Gypsum-colored skin", "Marble-colored skin", "Anhydrite-colored skin", "Halite-colored skin" },
                allEyes: new[] { "Eight eyespots, but not actual eyes" });
            //Source: https://www.dimensions.com/element/deinonychus-deinonychus-antirrhopus
            //https://jurassicworld-evolution.fandom.com/wiki/Deinonychus#Skins
            appearances[CreatureConstants.Deinonychus] = GetWeightedAppearances(
                allSkin: new[] { "Red skin, tan underbelly", "Gray skin, tan underbelly", "Orange skin, mottled yellow-orange underbelly",
                    "Turquoise skin, gray underbelly", "Green skin, yellow underbelly", "Purple skin with pink accents, gray underbelly" },
                allOther: new[] { "Small body, sleek horizontal posture, ratite-like spine, and enlarged raptorial claws on the feet. A large, sickle-shaped talon is on the second toe of each hind foot." });
            //Source: https://forgottenrealms.fandom.com/wiki/Delver
            appearances[CreatureConstants.Delver] = new[] { "Huge teardrop-shaped body with rock-like skin, always covered in a mucus-like slime. Almost the entire underside of the body consists of their mouth. Only two flipper-like appendages at the front of the body ending in blunt claws" };
            //Source: https://forgottenrealms.fandom.com/wiki/Derro
            appearances[CreatureConstants.Derro] = GetWeightedAppearances(
                commonSkin: new[] { "Pale blue-white skin", "Blue-gray skin" },
                uncommonSkin: new[] { "Pale blue-gray skin", "Blue-white skin" },
                allHair: new[] { "White hair", "Yellow hair", "Pale tan hair" },
                allEyes: new[] { "Uniformly pale eyes lacking irisies and pupils" });
            appearances[CreatureConstants.Derro_Sane] = GetWeightedAppearances(
                commonSkin: new[] { "Pale blue-white skin", "Blue-gray skin" },
                uncommonSkin: new[] { "Pale blue-gray skin", "Blue-white skin" },
                allHair: new[] { "White hair", "Yellow hair", "Pale tan hair" },
                allEyes: new[] { "Uniformly pale eyes lacking irisies and pupils" });
            //Source: https://forgottenrealms.fandom.com/wiki/Destrachan
            appearances[CreatureConstants.Destrachan] = new[] { "Bipedal, vaguely reptilian. Head is largely featureless except for the circular mouth and the large ear structures." };
            //Source: https://forgottenrealms.fandom.com/wiki/Devourer
            appearances[CreatureConstants.Devourer] = new[] { "Tall, gaunt skeletal figure with a smaller figure trapped within its rib cage" };
            //Source: https://forgottenrealms.fandom.com/wiki/Digester
            appearances[CreatureConstants.Digester] = new[] { "Gray, pebbly skin with dagger-like markings. Two powerful hind legs. Long tail. Head is narrow with a sucking mouth, sporting a tube-like orifice on the forehead." };
            //Source: https://forgottenrealms.fandom.com/wiki/Displacer_beast
            appearances[CreatureConstants.DisplacerBeast] = GetWeightedAppearances(
                commonHair: new[] { "Blue fur", "Blue-black fur", "Black fur" },
                uncommonHair: new[] { "Dark blue fur", "Purple fur" },
                allEyes: new[] { "Striking, glowing emerald-green eyes" },
                commonOther: new[] { "Panther-like with six legs and a pair of tentacles sprouting from their shoulders. Tentacles end in pads with sharp, horny, brownish-yellow edges" },
                uncommonOther: new[] { "Panther-like with six legs and a pair of tentacles sprouting from their shoulders. Tentacles end in pads with sharp, horny, brownish-yellow edges. Emaciated-looking." });
            appearances[CreatureConstants.DisplacerBeast_PackLord] = GetWeightedAppearances(
                commonHair: new[] { "Black fur", "Ebony fur" },
                uncommonHair: new[] { "Dark blue fur", "Purple fur", "Blue fur", "Blue-black fur" },
                allEyes: new[] { "Striking, glowing emerald-green eyes" },
                commonOther: new[] { "Panther-like with six legs and a pair of tentacles sprouting from their shoulders. Tentacles end in pads with sharp, horny, brownish-yellow edges" },
                uncommonOther: new[] { "Panther-like with six legs and a pair of tentacles sprouting from their shoulders. Tentacles end in pads with sharp, horny, brownish-yellow edges. Emaciated-looking." });
            //Source: https://forgottenrealms.fandom.com/wiki/Djinni
            appearances[CreatureConstants.Djinni] = GetWeightedAppearances(
                commonSkin: new[] { "Pale blue skin", "Olive-brown skin", "Blue skin", "Dark blue skin" },
                uncommonSkin: new[] { "Dark tan skin" },
                rareSkin: new[] { "Purple skin", "Dark purple skin", "Beige skin", "Dark red skin" },
                allHair: new[] { "Bald", "Black hair", "TODO Human hair" },
                commonEyes: new[] { "Brown eyes" },
                rareEyes: new[] { "Blue eyes" });
            //Source: https://forgottenrealms.fandom.com/wiki/Noble_djinni
            appearances[CreatureConstants.Djinni_Noble] = GetWeightedAppearances(
                commonSkin: new[] { "Pale blue skin", "Fair brown skin", "Fair Blue skin" },
                uncommonSkin: new[] { "Fair tan skin" },
                rareSkin: new[] { "Fair purple skin", "Fair beige skin", "Fair red skin" },
                allHair: new[] { "Bald", "Black hair", "TODO Human hair" },
                commonEyes: new[] { "Brown eyes" },
                rareEyes: new[] { "Blue eyes" });
            //Source: https://g.co/kgs/q7esXM
            appearances[CreatureConstants.Dog] = new[] { "Bulldog", "French Bulldog", "Beagle", "Standard Poodle", "Chihuahua", "Dachsund", "Bichon Frise", "Maltese",
                "Chow Chow", "English Cocker Spaniel" , "Pomeranian", "Yorkshire Terrier", "Cavalier King Charles Spaniel", "Pembroke Welsh Corgi", "Basenji", "Havanese",
                "Boston Terrier", "Cairn Terrier", "Brittany", "Sheltie", "Shiba Inu", "Jack Russell Terrier", "Borzoi", "Maltipoo", "Papillon", "Pikanese",
                "Miniature Poodle", "Pikapoo", "Coyote", "Dingo", "Miniature Schnauzer", "Mutt" };
            //Source: https://www.google.com/search?q=dog+working+breeds
            appearances[CreatureConstants.Dog_Riding] = new[] { "German Shepherd", "Labrador Retriever", "Golden Retriever", "Siberian Husky", "Beagle", "Alaskan Malamute",
                "Border Collie", "Rottweiler", "Australian Shepherd", "Airedale Terrier", "Affenpinscher", "Afghan Hound", "American Eskimo", "Anatolian Shepherd",
                "Basset Hound", "Belgian Malinois", "Boston Terrier", "Bullmastiff", "Black Russian Terrier", "Bedlington Terrier", "American Pit Bull Terrier", "Dobermann",
                "Sarabi", "Samoyed", "American Bully", "Borzoi", "Goldendoodle", "Dalmation", "Labradoodle", "Bernese Mountain Dog", "Greater Swiss Mountain Dog",
                "Newfoundland", "Boxer", "Cane Corso", "Boerboel", "Chinook", "Dogue de Bordeaux", "Giant Schnauzer", "Neopolitan Mastiff", "Dogo Argentino",
                "Great Pyrenees", "Great Dane", "Tibetan Mastiff", "Kuvasz", "Komondor", "German Pinscher", "Leonberger", "St. Bernard", "Portugese Water Dog",
                "Standard Schnauzer", "English Mastiff", "Karelian Bear Dog", "American Bulldog", "Hovawart", "Bloodhound", "Entlebucher Mountain Dog", "Eurasier",
                "Greenland", "Canadian Eskimo", "Canaan", "Caucasian Shepherd", "Blue Lacy", "American Akita", "Akbash", "Central Asian Shepherd", "Australian Cattle",
                "Yakutian Laika", "Mutt"
            };
            //Source: https://www.britannica.com/animal/donkey
            appearances[CreatureConstants.Donkey] = GetWeightedAppearances(
                allHair: new[] { "White fur with dark stripe from mane to tail and a crosswise stripe on the shoulders",
                    "Light gray fur with dark stripe from mane to tail and a crosswise stripe on the shoulders",
                    "Gray fur with dark stripe from mane to tail and a crosswise stripe on the shoulders",
                    "Dark gray fur with darker stripe from mane to tail and a crosswise stripe on the shoulders",
                    "Black fur with gray stripe from mane to tail and a crosswise stripe on the shoulders" },
                allOther: new[] { "Long ears, surefooted" });
            //Source: https://forgottenrealms.fandom.com/wiki/Doppelganger
            appearances[CreatureConstants.Doppelganger] = GetWeightedAppearances(
                allSkin: new[] { "Gray skin" },
                allHair: new[] { "Hairless" },
                commonEyes: new[] { "Bulging, pale yellow eyes lacking visible pupils", "Bulging, yellow eyes lacking visible pupils" },
                rareEyes: new[] { "Bulging, green eyes lacking visible pupils", "Bulging, white eyes lacking visible pupils" },
                allOther: new[] { "Elven ears. Bulbous head with formless face." });
            //Source: Draconomicon
            appearances[CreatureConstants.Dragon_Black_Wyrmling] = GetWeightedAppearances(
                allSkin: new[] { "Thin, glossy black one-inch scales" },
                allEyes: new[] { "Deep-socketed eyes" },
                allOther: new[] { "Broad nasal openings, making its face look like a skull. Segmented horns the curve forward and down, somewhat like a ram's horns, but not as curly. These horns are bone-colored near their bases, but darken to dead black at the tips. One inch of deteriorated flesh around the horns and cheekbones, as though eaten by acid, leaving only thin layers of hide covering the skull. Most teeth protrude when the mouth is closed. Big spikes stud the lower jaw. Small horns jut from the chin, and a row of hornlets crown the head." });
            appearances[CreatureConstants.Dragon_Black_VeryYoung] = GetWeightedAppearances(
                allSkin: new[] { "Thin, glossy black two-inch scales" },
                allEyes: new[] { "Deep-socketed eyes" },
                allOther: new[] { "Broad nasal openings, making its face look like a skull. Segmented horns the curve forward and down, somewhat like a ram's horns, but not as curly. These horns are bone-colored near their bases, but darken to dead black at the tips. Two inches of deteriorated flesh around the horns and cheekbones, as though eaten by acid, leaving only thin layers of hide covering the skull. Most teeth protrude when the mouth is closed. Big spikes stud the lower jaw. Small horns jut from the chin, and a row of hornlets crown the head." });
            appearances[CreatureConstants.Dragon_Black_Young] = GetWeightedAppearances(
                allSkin: new[] { "Thin, glossy black three-inch scales" },
                allEyes: new[] { "Deep-socketed eyes" },
                allOther: new[] { "Broad nasal openings, making its face look like a skull. Segmented horns the curve forward and down, somewhat like a ram's horns, but not as curly. These horns are bone-colored near their bases, but darken to dead black at the tips. Three inches of deteriorated flesh around the horns and cheekbones, as though eaten by acid, leaving only thin layers of hide covering the skull. Most teeth protrude when the mouth is closed. Big spikes stud the lower jaw. Small horns jut from the chin, and a row of hornlets crown the head." });
            appearances[CreatureConstants.Dragon_Black_Juvenile] = GetWeightedAppearances(
                allSkin: new[] { "Thin, glossy black four-inch scales" },
                allEyes: new[] { "Deep-socketed eyes" },
                allOther: new[] { "Broad nasal openings, making its face look like a skull. Segmented horns the curve forward and down, somewhat like a ram's horns, but not as curly. These horns are bone-colored near their bases, but darken to dead black at the tips. Four inches of deteriorated flesh around the horns and cheekbones, as though eaten by acid, leaving only thin layers of hide covering the skull. Most teeth protrude when the mouth is closed. Big spikes stud the lower jaw. Small horns jut from the chin, and a row of hornlets crown the head." });
            appearances[CreatureConstants.Dragon_Black_YoungAdult] = GetWeightedAppearances(
                allSkin: new[] { "Matte black five-inch scales" },
                allEyes: new[] { "Deep-socketed eyes" },
                allOther: new[] { "Broad nasal openings, making its face look like a skull. Segmented horns the curve forward and down, somewhat like a ram's horns, but not as curly. These horns are bone-colored near their bases, but darken to dead black at the tips. Five inches of deteriorated flesh around the horns and cheekbones, as though eaten by acid, leaving only thin layers of hide covering the skull. Most teeth protrude when the mouth is closed. Big spikes stud the lower jaw. Small horns jut from the chin, and a row of hornlets crown the head." });
            appearances[CreatureConstants.Dragon_Black_Adult] = GetWeightedAppearances(
                allSkin: new[] { "Matte black six-inch scales" },
                allEyes: new[] { "Deep-socketed eyes" },
                allOther: new[] { "Broad nasal openings, making its face look like a skull. Segmented horns the curve forward and down, somewhat like a ram's horns, but not as curly. These horns are bone-colored near their bases, but darken to dead black at the tips. Six inches of deteriorated flesh around the horns and cheekbones, as though eaten by acid, leaving only thin layers of hide covering the skull. Most teeth protrude when the mouth is closed. Big spikes stud the lower jaw. Small horns jut from the chin, and a row of hornlets crown the head." });
            appearances[CreatureConstants.Dragon_Black_MatureAdult] = GetWeightedAppearances(
                allSkin: new[] { "Matte black seven-inch scales" },
                allEyes: new[] { "Deep-socketed eyes" },
                allOther: new[] { "Broad nasal openings, making its face look like a skull. Segmented horns the curve forward and down, somewhat like a ram's horns, but not as curly. These horns are bone-colored near their bases, but darken to dead black at the tips. Seven inches of deteriorated flesh around the horns and cheekbones, as though eaten by acid, leaving only thin layers of hide covering the skull. Most teeth protrude when the mouth is closed. Big spikes stud the lower jaw. Small horns jut from the chin, and a row of hornlets crown the head." });
            appearances[CreatureConstants.Dragon_Black_Old] = GetWeightedAppearances(
                allSkin: new[] { "Matte black eight-inch scales" },
                allEyes: new[] { "Deep-socketed eyes" },
                allOther: new[] { "Broad nasal openings, making its face look like a skull. Segmented horns the curve forward and down, somewhat like a ram's horns, but not as curly. These horns are bone-colored near their bases, but darken to dead black at the tips. Eight inches of deteriorated flesh around the horns and cheekbones, as though eaten by acid, leaving only thin layers of hide covering the skull. Most teeth protrude when the mouth is closed. Big spikes stud the lower jaw. Small horns jut from the chin, and a row of hornlets crown the head." });
            appearances[CreatureConstants.Dragon_Black_VeryOld] = GetWeightedAppearances(
                allSkin: new[] { "Thick, dull black nine-inch scales" },
                allEyes: new[] { "Deep-socketed eyes" },
                allOther: new[] { "Broad nasal openings, making its face look like a skull. Segmented horns the curve forward and down, somewhat like a ram's horns, but not as curly. These horns are bone-colored near their bases, but darken to dead black at the tips. Nine inches of deteriorated flesh around the horns and cheekbones, as though eaten by acid, leaving only thin layers of hide covering the skull. Most teeth protrude when the mouth is closed. Big spikes stud the lower jaw. Small horns jut from the chin, and a row of hornlets crown the head." });
            appearances[CreatureConstants.Dragon_Black_Ancient] = GetWeightedAppearances(
                allSkin: new[] { "Thick, dull black ten-inch scales" },
                allEyes: new[] { "Deep-socketed eyes" },
                allOther: new[] { "Broad nasal openings, making its face look like a skull. Segmented horns the curve forward and down, somewhat like a ram's horns, but not as curly. These horns are bone-colored near their bases, but darken to dead black at the tips. Ten inches of deteriorated flesh around the horns and cheekbones, as though eaten by acid, leaving only thin layers of hide covering the skull. Most teeth protrude when the mouth is closed. Big spikes stud the lower jaw. Small horns jut from the chin, and a row of hornlets crown the head." });
            appearances[CreatureConstants.Dragon_Black_Wyrm] = GetWeightedAppearances(
                allSkin: new[] { "Thick, dull black eleven-inch scales" },
                allEyes: new[] { "Deep-socketed eyes" },
                allOther: new[] { "Broad nasal openings, making its face look like a skull. Segmented horns the curve forward and down, somewhat like a ram's horns, but not as curly. These horns are bone-colored near their bases, but darken to dead black at the tips. Eleven inches of deteriorated flesh around the horns and cheekbones, as though eaten by acid, leaving only thin layers of hide covering the skull. Most teeth protrude when the mouth is closed. Big spikes stud the lower jaw. Small horns jut from the chin, and a row of hornlets crown the head." });
            appearances[CreatureConstants.Dragon_Black_GreatWyrm] = GetWeightedAppearances(
                allSkin: new[] { "Thick, dull black twelve-inch scales" },
                allEyes: new[] { "Deep-socketed eyes" },
                allOther: new[] { "Broad nasal openings, making its face look like a skull. Segmented horns the curve forward and down, somewhat like a ram's horns, but not as curly. These horns are bone-colored near their bases, but darken to dead black at the tips. Twelve inches of deteriorated flesh around the horns and cheekbones, as though eaten by acid, leaving only thin layers of hide covering the skull. Most teeth protrude when the mouth is closed. Big spikes stud the lower jaw. Small horns jut from the chin, and a row of hornlets crown the head." });
            appearances[CreatureConstants.Dragon_Blue_Wyrmling] = GetWeightedAppearances(
                allSkin: new[] { "Thin, iridescent azure scales", "Thin, glossy, deep indigo scales" },
                commonOther: new[] { "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has two points, with the primary slightly curved and reaching well forward, with the smaller secondary point behind. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed." },
                uncommonOther: new[] { "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has one point, slightly curved and reaching well forward. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed.",
                    "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has three points, with the primary slightly curved and reaching well forward, with the smaller second and third points behind in a line. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed."});
            appearances[CreatureConstants.Dragon_Blue_VeryYoung] = GetWeightedAppearances(
                allSkin: new[] { "Thin, iridescent azure scales", "Thin, glossy, deep indigo scales" },
                commonOther: new[] { "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has two points, with the primary slightly curved and reaching well forward, with the smaller secondary point behind. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed." },
                uncommonOther: new[] { "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has one point, slightly curved and reaching well forward. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed.",
                    "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has three points, with the primary slightly curved and reaching well forward, with the smaller second and third points behind in a line. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed."});
            appearances[CreatureConstants.Dragon_Blue_Young] = GetWeightedAppearances(
                allSkin: new[] { "Thin, iridescent azure scales", "Thin, glossy, deep indigo scales" },
                commonOther: new[] { "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has two points, with the primary slightly curved and reaching well forward, with the smaller secondary point behind. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed." },
                uncommonOther: new[] { "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has one point, slightly curved and reaching well forward. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed.",
                    "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has three points, with the primary slightly curved and reaching well forward, with the smaller second and third points behind in a line. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed."});
            appearances[CreatureConstants.Dragon_Blue_Juvenile] = GetWeightedAppearances(
                allSkin: new[] { "Thin, iridescent azure scales", "Thin, glossy, deep indigo scales" },
                commonOther: new[] { "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has two points, with the primary slightly curved and reaching well forward, with the smaller secondary point behind. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed." },
                uncommonOther: new[] { "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has one point, slightly curved and reaching well forward. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed.",
                    "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has three points, with the primary slightly curved and reaching well forward, with the smaller second and third points behind in a line. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed."});
            appearances[CreatureConstants.Dragon_Blue_YoungAdult] = GetWeightedAppearances(
                allSkin: new[] { "Iridescent azure scales", "Glossy, deep indigo scales" },
                commonOther: new[] { "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has two points, with the primary slightly curved and reaching well forward, with the smaller secondary point behind. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed." },
                uncommonOther: new[] { "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has one point, slightly curved and reaching well forward. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed.",
                    "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has three points, with the primary slightly curved and reaching well forward, with the smaller second and third points behind in a line. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed."});
            appearances[CreatureConstants.Dragon_Blue_Adult] = GetWeightedAppearances(
                allSkin: new[] { "Iridescent azure scales", "Glossy, deep indigo scales" },
                commonOther: new[] { "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has two points, with the primary slightly curved and reaching well forward, with the smaller secondary point behind. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed." },
                uncommonOther: new[] { "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has one point, slightly curved and reaching well forward. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed.",
                    "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has three points, with the primary slightly curved and reaching well forward, with the smaller second and third points behind in a line. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed."});
            appearances[CreatureConstants.Dragon_Blue_MatureAdult] = GetWeightedAppearances(
                allSkin: new[] { "Iridescent azure scales", "Glossy, deep indigo scales" },
                commonOther: new[] { "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has two points, with the primary slightly curved and reaching well forward, with the smaller secondary point behind. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed." },
                uncommonOther: new[] { "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has one point, slightly curved and reaching well forward. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed.",
                    "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has three points, with the primary slightly curved and reaching well forward, with the smaller second and third points behind in a line. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed."});
            appearances[CreatureConstants.Dragon_Blue_Old] = GetWeightedAppearances(
                allSkin: new[] { "Iridescent azure scales", "Glossy, deep indigo scales" },
                commonOther: new[] { "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has two points, with the primary slightly curved and reaching well forward, with the smaller secondary point behind. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed." },
                uncommonOther: new[] { "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has one point, slightly curved and reaching well forward. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed.",
                    "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has three points, with the primary slightly curved and reaching well forward, with the smaller second and third points behind in a line. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed."});
            appearances[CreatureConstants.Dragon_Blue_VeryOld] = GetWeightedAppearances(
                allSkin: new[] { "Thick, iridescent azure scales", "Thick, glossy, deep indigo scales" },
                commonOther: new[] { "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has two points, with the primary slightly curved and reaching well forward, with the smaller secondary point behind. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed." },
                uncommonOther: new[] { "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has one point, slightly curved and reaching well forward. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed.",
                    "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has three points, with the primary slightly curved and reaching well forward, with the smaller second and third points behind in a line. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed."});
            appearances[CreatureConstants.Dragon_Blue_Ancient] = GetWeightedAppearances(
                allSkin: new[] { "Thick, iridescent azure scales", "Thick, glossy, deep indigo scales" },
                commonOther: new[] { "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has two points, with the primary slightly curved and reaching well forward, with the smaller secondary point behind. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed." },
                uncommonOther: new[] { "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has one point, slightly curved and reaching well forward. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed.",
                    "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has three points, with the primary slightly curved and reaching well forward, with the smaller second and third points behind in a line. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed."});
            appearances[CreatureConstants.Dragon_Blue_Wyrm] = GetWeightedAppearances(
                allSkin: new[] { "Thick, iridescent azure scales", "Thick, glossy, deep indigo scales" },
                commonOther: new[] { "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has two points, with the primary slightly curved and reaching well forward, with the smaller secondary point behind. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed." },
                uncommonOther: new[] { "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has one point, slightly curved and reaching well forward. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed.",
                    "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has three points, with the primary slightly curved and reaching well forward, with the smaller second and third points behind in a line. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed."});
            appearances[CreatureConstants.Dragon_Blue_GreatWyrm] = GetWeightedAppearances(
                allSkin: new[] { "Thick, iridescent azure scales", "Thick, glossy, deep indigo scales" },
                commonOther: new[] { "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has two points, with the primary slightly curved and reaching well forward, with the smaller secondary point behind. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed." },
                uncommonOther: new[] { "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has one point, slightly curved and reaching well forward. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed.",
                    "Dramatic frilled ears. A single horn atop its short, blunt head. The horn juts forward from a base that takes up most of the top of the head. The horn has three points, with the primary slightly curved and reaching well forward, with the smaller second and third points behind in a line. Rows of hornlets line the brow ridges and run back from the nostrils along the entire length of the head. Short snout with underslung lower jaw. Cluster of bladelike scales under its chin and hornlets on its cheeks. Most teeth protrude when its mouth is closed."});
            appearances[CreatureConstants.Dragon_Brass_Wyrmling] = GetWeightedAppearances(
                allSkin: new[] { "Dull, mottled brown scales. Wings and frills are mottled green where they join the body, and have reddish tints at the outer edges." },
                allEyes: new[] { "Brass-colored eyes" },
                allOther: new[] { "Fluted plate sweeping back from its eye sockets, forehead, and cheeks. The plate is dished like a plowshare. Sharp, bladed chin horns. Supple, expressive lips." });
            appearances[CreatureConstants.Dragon_Brass_VeryYoung] = GetWeightedAppearances(
                allSkin: new[] { "Dull, mottled brown scales. Wings and frills are mottled green where they join the body, and have reddish tints at the outer edges." },
                allEyes: new[] { "Brass-colored eyes" },
                allOther: new[] { "Fluted plate sweeping back from its eye sockets, forehead, and cheeks. The plate is dished like a plowshare. Sharp, bladed chin horns. Supple, expressive lips." });
            appearances[CreatureConstants.Dragon_Brass_Young] = GetWeightedAppearances(
                allSkin: new[] { "Dull brown scales. Wings and frills are mottled green where they join the body, and have reddish tints at the outer edges." },
                allEyes: new[] { "Brass-colored eyes" },
                allOther: new[] { "Fluted plate sweeping back from its eye sockets, forehead, and cheeks. The plate is dished like a plowshare. Sharp, bladed chin horns. Supple, expressive lips." });
            appearances[CreatureConstants.Dragon_Brass_Juvenile] = GetWeightedAppearances(
                allSkin: new[] { "Dull brown scales. Wings and frills are mottled green where they join the body, and have reddish tints at the outer edges." },
                allEyes: new[] { "Brass-colored eyes" },
                allOther: new[] { "Fluted plate sweeping back from its eye sockets, forehead, and cheeks. The plate is dished like a plowshare. Sharp, bladed chin horns. Supple, expressive lips." });
            appearances[CreatureConstants.Dragon_Brass_YoungAdult] = GetWeightedAppearances(
                allSkin: new[] { "Brown scales. Wings and frills are mottled green where they join the body, and have reddish tints at the outer edges." },
                allEyes: new[] { "Brass-colored eyes with small pupils" },
                allOther: new[] { "Fluted plate sweeping back from its eye sockets, forehead, and cheeks. The plate is dished like a plowshare. Very sharp, bladed chin horns. Supple, expressive lips." });
            appearances[CreatureConstants.Dragon_Brass_Adult] = GetWeightedAppearances(
                allSkin: new[] { "Brown scales. Wings and frills are mottled green where they join the body, and have reddish tints at the outer edges." },
                allEyes: new[] { "Brass-colored eyes with small pupils" },
                allOther: new[] { "Fluted plate sweeping back from its eye sockets, forehead, and cheeks. The plate is dished like a plowshare. Very sharp, bladed chin horns. Supple, expressive lips." });
            appearances[CreatureConstants.Dragon_Brass_MatureAdult] = GetWeightedAppearances(
                allSkin: new[] { "Brass-colored scales. Wings and frills are mottled green where they join the body, and have reddish tints at the outer edges." },
                allEyes: new[] { "Brass-colored eyes with small pupils" },
                allOther: new[] { "Fluted plate sweeping back from its eye sockets, forehead, and cheeks. The plate is dished like a plowshare. Very sharp, bladed chin horns. Supple, expressive lips." });
            appearances[CreatureConstants.Dragon_Brass_Old] = GetWeightedAppearances(
                allSkin: new[] { "Brass-colored scales. Wings and frills are mottled green where they join the body, and have reddish tints at the outer edges." },
                allEyes: new[] { "Brass-colored eyes with very small pupils" },
                allOther: new[] { "Fluted plate sweeping back from its eye sockets, forehead, and cheeks. The plate is dished like a plowshare. Very sharp, bladed chin horns. Supple, expressive lips." });
            appearances[CreatureConstants.Dragon_Brass_VeryOld] = GetWeightedAppearances(
                allSkin: new[] { "Warm, brass-colored scales. Wings and frills are mottled green where they join the body, and have reddish tints at the outer edges." },
                allEyes: new[] { "Brass-colored eyes with very small pupils" },
                allOther: new[] { "Fluted plate sweeping back from its eye sockets, forehead, and cheeks. The plate is dished like a plowshare. Extremely sharp, bladed chin horns. Supple, expressive lips." });
            appearances[CreatureConstants.Dragon_Brass_Ancient] = GetWeightedAppearances(
                allSkin: new[] { "Warm, brass-colored scales. Wings and frills are mottled green where they join the body, and have reddish tints at the outer edges." },
                allEyes: new[] { "Brass-colored eyes with very small pupils" },
                allOther: new[] { "Fluted plate sweeping back from its eye sockets, forehead, and cheeks. The plate is dished like a plowshare. Extremely sharp, bladed chin horns. Supple, expressive lips." });
            appearances[CreatureConstants.Dragon_Brass_Wyrm] = GetWeightedAppearances(
                allSkin: new[] { "Brass-colored scales with a warm, burnished appearance. Wings and frills are mottled green where they join the body, and have reddish tints at the outer edges." },
                allEyes: new[] { "Eyes look like pupil-less, molten metal orbs" },
                allOther: new[] { "Fluted plate sweeping back from its eye sockets, forehead, and cheeks. The plate is dished like a plowshare. Extremely sharp, bladed chin horns. Supple, expressive lips." });
            appearances[CreatureConstants.Dragon_Brass_GreatWyrm] = GetWeightedAppearances(
                allSkin: new[] { "Brass-colored scales with a warm, burnished appearance. Wings and frills are mottled green where they join the body, and have reddish tints at the outer edges." },
                allEyes: new[] { "Eyes look like pupil-less, molten metal orbs" },
                allOther: new[] { "Fluted plate sweeping back from its eye sockets, forehead, and cheeks. The plate is dished like a plowshare. Extremely sharp, bladed chin horns. Supple, expressive lips." });
            appearances[CreatureConstants.Dragon_Bronze_Wyrmling] = GetWeightedAppearances(
                allSkin: new[] { "Smooth, flat, yellow scales tinged with green, showing only a hint of bronze" },
                allEyes: new[] { "Green eyes" },
                allOther: new[] { "Ribbed and fluted crest sweeping back from the cheeks and eyes. Ribs in the crest end in curving horns. The horns are smooth, dark, and oval in cross-section, curving slightly inward toward the spine. Large horns on the top of the head. Beaklike snout. Small head frill and tall neck frill. Webbed feet and webbing behind the forelimbs." });
            appearances[CreatureConstants.Dragon_Bronze_VeryYoung] = GetWeightedAppearances(
                allSkin: new[] { "Smooth, flat, yellow scales tinged with green and bronze" },
                allEyes: new[] { "Green eyes" },
                allOther: new[] { "Ribbed and fluted crest sweeping back from the cheeks and eyes. Ribs in the crest end in curving horns. The horns are smooth, dark, and oval in cross-section, curving slightly inward toward the spine. Large horns on the top of the head. Beaklike snout. Small head frill and tall neck frill. Webbed feet and webbing behind the forelimbs." });
            appearances[CreatureConstants.Dragon_Bronze_Young] = GetWeightedAppearances(
                allSkin: new[] { "Smooth, flat, yellow scales tinged with bronze" },
                allEyes: new[] { "Green eyes" },
                allOther: new[] { "Ribbed and fluted crest sweeping back from the cheeks and eyes. Ribs in the crest end in curving horns. The horns are smooth, dark, and oval in cross-section, curving slightly inward toward the spine. Large horns on the top of the head. Beaklike snout. Small head frill and tall neck frill. Webbed feet and webbing behind the forelimbs." });
            appearances[CreatureConstants.Dragon_Bronze_Juvenile] = GetWeightedAppearances(
                allSkin: new[] { "Smooth, flat, yellow scales mottled with bronze" },
                allEyes: new[] { "Green eyes with somewhat-faded pupils" },
                allOther: new[] { "Ribbed and fluted crest sweeping back from the cheeks and eyes. Ribs in the crest end in curving horns. The horns are smooth, dark, and oval in cross-section, curving slightly inward toward the spine. Large horns on the top of the head. Beaklike snout. Small head frill and tall neck frill. Webbed feet and webbing behind the forelimbs." });
            appearances[CreatureConstants.Dragon_Bronze_YoungAdult] = GetWeightedAppearances(
                allSkin: new[] { "Rich, bronze scales" },
                allEyes: new[] { "Green eyes with somewhat-faded pupils" },
                allOther: new[] { "Ribbed and fluted crest sweeping back from the cheeks and eyes. Ribs in the crest end in curving horns. The horns are smooth, dark, and oval in cross-section, curving slightly inward toward the spine. Large horns on the top of the head. Beaklike snout. Small head frill and tall neck frill. Webbed feet and webbing behind the forelimbs." });
            appearances[CreatureConstants.Dragon_Bronze_Adult] = GetWeightedAppearances(
                allSkin: new[] { "Rich, bronze scales" },
                allEyes: new[] { "Green eyes with somewhat-faded pupils" },
                allOther: new[] { "Ribbed and fluted crest sweeping back from the cheeks and eyes. Ribs in the crest end in curving horns. The horns are smooth, dark, and oval in cross-section, curving slightly inward toward the spine. Large horns on the top of the head. Beaklike snout. Small head frill and tall neck frill. Webbed feet and webbing behind the forelimbs." });
            appearances[CreatureConstants.Dragon_Bronze_MatureAdult] = GetWeightedAppearances(
                allSkin: new[] { "Rich, bronze scales" },
                allEyes: new[] { "Green eyes with faded pupils" },
                allOther: new[] { "Ribbed and fluted crest sweeping back from the cheeks and eyes. Ribs in the crest end in curving horns. The horns are smooth, dark, and oval in cross-section, curving slightly inward toward the spine. Large horns on the top of the head. Smaller horns have a secondary point. Beaklike snout. Small head frill and tall neck frill. Webbed feet and webbing behind the forelimbs." });
            appearances[CreatureConstants.Dragon_Bronze_Old] = GetWeightedAppearances(
                allSkin: new[] { "Rich, bronze scales" },
                allEyes: new[] { "Green eyes with faded pupils" },
                allOther: new[] { "Ribbed and fluted crest sweeping back from the cheeks and eyes. Ribs in the crest end in curving horns. The horns are smooth, dark, and oval in cross-section, curving slightly inward toward the spine. Large horns on the top of the head. Smaller horns have a secondary point. Beaklike snout. Small head frill and tall neck frill. Webbed feet and webbing behind the forelimbs." });
            appearances[CreatureConstants.Dragon_Bronze_VeryOld] = GetWeightedAppearances(
                allSkin: new[] { "Rich, bronze scales with a blue-black tint to the edges" },
                allEyes: new[] { "Green eyes with faded pupils" },
                allOther: new[] { "Ribbed and fluted crest sweeping back from the cheeks and eyes. Ribs in the crest end in curving horns. The horns are smooth, dark, and oval in cross-section, curving slightly inward toward the spine. Large horns on the top of the head. Smaller horns have a secondary point. Beaklike snout. Small head frill and tall neck frill. Webbed feet and webbing behind the forelimbs." });
            appearances[CreatureConstants.Dragon_Bronze_Ancient] = GetWeightedAppearances(
                allSkin: new[] { "Rich, bronze scales with a blue-black tint to the edges" },
                allEyes: new[] { "Eyes have no pupils, and resemble glowing green orbs" },
                allOther: new[] { "Ribbed and fluted crest sweeping back from the cheeks and eyes. Ribs in the crest end in curving horns. The horns are smooth, dark, and oval in cross-section, curving slightly inward toward the spine. Large horns on the top of the head. Smaller horns have a secondary point. Beaklike snout. Small head frill and tall neck frill. Webbed feet and webbing behind the forelimbs." });
            appearances[CreatureConstants.Dragon_Bronze_Wyrm] = GetWeightedAppearances(
                allSkin: new[] { "Rich, bronze scales with a blue-black tint to the edges" },
                allEyes: new[] { "Eyes have no pupils, and resemble glowing green orbs" },
                allOther: new[] { "Ribbed and fluted crest sweeping back from the cheeks and eyes. Ribs in the crest end in curving horns. The horns are smooth, dark, and oval in cross-section, curving slightly inward toward the spine. Large horns on the top of the head. Smaller horns have a secondary point. Beaklike snout. Small head frill and tall neck frill. Webbed feet and webbing behind the forelimbs." });
            appearances[CreatureConstants.Dragon_Bronze_GreatWyrm] = GetWeightedAppearances(
                allSkin: new[] { "Rich, bronze scales with a blue-black tint to the edges" },
                allEyes: new[] { "Eyes have no pupils, and resemble glowing green orbs" },
                allOther: new[] { "Ribbed and fluted crest sweeping back from the cheeks and eyes. Ribs in the crest end in curving horns. The horns are smooth, dark, and oval in cross-section, curving slightly inward toward the spine. Large horns on the top of the head. Smaller horns have a secondary point. Beaklike snout. Small head frill and tall neck frill. Webbed feet and webbing behind the forelimbs." });
            appearances[CreatureConstants.Dragon_Copper_Wyrmling] = GetWeightedAppearances(
                allSkin: new[] { "Ruddy brown scales with a metallic tint. Green and red mottling along the trailing edges of the wings." },
                allEyes: new[] { "Turquoise eyes" },
                allOther: new[] { "Thick thighs and shoulders. Short face, no beak. Broad, smooth browplates jut over the eyes. Long, flat, coppery horns extend back from the browplates in a series of segments. Backswept cheek ridges and frills on the back of the lower jaw that sweep forward slightly. One layer of triangular blades point down from the chin." });
            appearances[CreatureConstants.Dragon_Copper_VeryYoung] = GetWeightedAppearances(
                allSkin: new[] { "Ruddy brown scales with a metallic tint. Green and red mottling along the trailing edges of the wings." },
                allEyes: new[] { "Turquoise eyes" },
                allOther: new[] { "Thick thighs and shoulders. Short face, no beak. Broad, smooth browplates jut over the eyes. Long, flat, coppery horns extend back from the browplates in a series of segments. Backswept cheek ridges and frills on the back of the lower jaw that sweep forward slightly. One layer of triangular blades point down from the chin." });
            appearances[CreatureConstants.Dragon_Copper_Young] = GetWeightedAppearances(
                allSkin: new[] { "Ruddy brown scales with a metallic tint. Green and red mottling along the trailing edges of the wings." },
                allEyes: new[] { "Turquoise eyes" },
                allOther: new[] { "Thick thighs and shoulders. Short face, no beak. Broad, smooth browplates jut over the eyes. Long, flat, coppery horns extend back from the browplates in a series of segments. Backswept cheek ridges and frills on the back of the lower jaw that sweep forward slightly. One layer of triangular blades point down from the chin." });
            appearances[CreatureConstants.Dragon_Copper_Juvenile] = GetWeightedAppearances(
                allSkin: new[] { "Ruddy brown scales with a metallic tint. Green and red mottling along the trailing edges of the wings." },
                allEyes: new[] { "Turquoise eyes with somewhat-faded pupils" },
                allOther: new[] { "Thick thighs and shoulders. Short face, no beak. Broad, smooth browplates jut over the eyes. Long, flat, coppery horns extend back from the browplates in a series of segments. Backswept cheek ridges and frills on the back of the lower jaw that sweep forward slightly. One layer of triangular blades point down from the chin." });
            appearances[CreatureConstants.Dragon_Copper_YoungAdult] = GetWeightedAppearances(
                allSkin: new[] { "Coppery scales with a soft, warm gloss. Green and red mottling along the trailing edges of the wings." },
                allEyes: new[] { "Turquoise eyes with somewhat-faded pupils" },
                allOther: new[] { "Thick thighs and shoulders. Short face, no beak. Broad, smooth browplates jut over the eyes. Long, flat, coppery horns extend back from the browplates in a series of segments. Backswept cheek ridges and frills on the back of the lower jaw that sweep forward slightly. Two layers of large, triangular blades point down from the chin." });
            appearances[CreatureConstants.Dragon_Copper_Adult] = GetWeightedAppearances(
                allSkin: new[] { "Coppery scales with a soft, warm gloss. Green and red mottling along the trailing edges of the wings." },
                allEyes: new[] { "Turquoise eyes with somewhat-faded pupils" },
                allOther: new[] { "Thick thighs and shoulders. Short face, no beak. Broad, smooth browplates jut over the eyes. Long, flat, coppery horns extend back from the browplates in a series of segments. Backswept cheek ridges and frills on the back of the lower jaw that sweep forward slightly. Two layers of large, triangular blades point down from the chin." });
            appearances[CreatureConstants.Dragon_Copper_MatureAdult] = GetWeightedAppearances(
                allSkin: new[] { "Coppery scales with a soft, warm gloss. Green and red mottling along the trailing edges of the wings." },
                allEyes: new[] { "Turquoise eyes with faded pupils" },
                allOther: new[] { "Thick thighs and shoulders. Short face, no beak. Broad, smooth browplates jut over the eyes. Long, flat, coppery horns extend back from the browplates in a series of segments. Backswept cheek ridges and frills on the back of the lower jaw that sweep forward slightly. Two layers of large, triangular blades point down from the chin." });
            appearances[CreatureConstants.Dragon_Copper_Old] = GetWeightedAppearances(
                allSkin: new[] { "Coppery scales with a soft, warm gloss. Green and red mottling along the trailing edges of the wings." },
                allEyes: new[] { "Turquoise eyes with faded pupils" },
                allOther: new[] { "Thick thighs and shoulders. Short face, no beak. Broad, smooth browplates jut over the eyes. Long, flat, coppery horns extend back from the browplates in a series of segments. Backswept cheek ridges and frills on the back of the lower jaw that sweep forward slightly. Two layers of large, triangular blades point down from the chin." });
            appearances[CreatureConstants.Dragon_Copper_VeryOld] = GetWeightedAppearances(
                allSkin: new[] { "Coppery scales with a soft, warm gloss and a green tint. Green and red mottling along the trailing edges of the wings." },
                allEyes: new[] { "Turquoise eyes with faded pupils" },
                allOther: new[] { "Thick thighs and shoulders. Short face, no beak. Broad, smooth browplates jut over the eyes. Long, flat, coppery horns extend back from the browplates in a series of segments. Backswept cheek ridges and frills on the back of the lower jaw that sweep forward slightly. Three layers of huge, triangular blades point down from the chin." });
            appearances[CreatureConstants.Dragon_Copper_Ancient] = GetWeightedAppearances(
                allSkin: new[] { "Coppery scales with a soft, warm gloss and a green tint. Green and red mottling along the trailing edges of the wings." },
                allEyes: new[] { "Turquoise eyes with very faded pupils" },
                allOther: new[] { "Thick thighs and shoulders. Short face, no beak. Broad, smooth browplates jut over the eyes. Long, flat, coppery horns extend back from the browplates in a series of segments. Backswept cheek ridges and frills on the back of the lower jaw that sweep forward slightly. Three layers of huge, triangular blades point down from the chin." });
            appearances[CreatureConstants.Dragon_Copper_Wyrm] = GetWeightedAppearances(
                allSkin: new[] { "Coppery scales with a soft, warm gloss and a green tint. Green and red mottling along the trailing edges of the wings." },
                allEyes: new[] { "Turquoise eyes with very faded pupils" },
                allOther: new[] { "Thick thighs and shoulders. Short face, no beak. Broad, smooth browplates jut over the eyes. Long, flat, coppery horns extend back from the browplates in a series of segments. Backswept cheek ridges and frills on the back of the lower jaw that sweep forward slightly. Three layers of huge, triangular blades point down from the chin." });
            appearances[CreatureConstants.Dragon_Copper_GreatWyrm] = GetWeightedAppearances(
                allSkin: new[] { "Coppery scales with a soft, warm gloss and a green tint. Green and red mottling along the trailing edges of the wings." },
                allEyes: new[] { "Eyes have no pupils, and resemble glowing turquoise orbs" },
                allOther: new[] { "Thick thighs and shoulders. Short face, no beak. Broad, smooth browplates jut over the eyes. Long, flat, coppery horns extend back from the browplates in a series of segments. Backswept cheek ridges and frills on the back of the lower jaw that sweep forward slightly. Three layers of huge, triangular blades point down from the chin." });
            appearances[CreatureConstants.Dragon_Gold_Wyrmling] = GetWeightedAppearances(
                allSkin: new[] { "Dark yellow scales with golden metallic flecks" },
                allEyes: new[] { "Slanted, very narrow golden eyes" },
                allOther: new[] { "Twin horns, smooth and metallic, coming off the head. Twin neck frills." });
            appearances[CreatureConstants.Dragon_Gold_VeryYoung] = GetWeightedAppearances(
                allSkin: new[] { "Dark yellow scales with large golden metallic flecks" },
                allEyes: new[] { "Slanted, very narrow golden eyes" },
                allOther: new[] { "Twin horns, smooth and metallic, coming off the head. Twin neck frills. Eight whiskers, four on upper jaw and four below, around the mouth that look like the barbels of a catfish." });
            appearances[CreatureConstants.Dragon_Gold_Young] = GetWeightedAppearances(
                allSkin: new[] { "Dark yellow scales mottled with metallic gold" },
                allEyes: new[] { "Slanted, very narrow golden eyes" },
                allOther: new[] { "Twin horns, smooth and metallic, coming off the head. Twin neck frills. Eight whiskers, four on upper jaw and four below, around the mouth that look like the barbels of a catfish." });
            appearances[CreatureConstants.Dragon_Gold_Juvenile] = GetWeightedAppearances(
                allSkin: new[] { "Metallic golden scales with large dark yellow flecks" },
                allEyes: new[] { "Slanted, very narrow golden eyes with somewhat-faded pupils" },
                allOther: new[] { "Twin horns, smooth and metallic, coming off the head. Twin neck frills. Eight whiskers, four on upper jaw and four below, around the mouth that look like the barbels of a catfish." });
            appearances[CreatureConstants.Dragon_Gold_YoungAdult] = GetWeightedAppearances(
                allSkin: new[] { "Metallic golden scales with dark yellow flecks" },
                allEyes: new[] { "Slanted, very narrow golden eyes with somewhat-faded pupils" },
                allOther: new[] { "Twin horns, smooth and metallic, coming off the head. Twin neck frills. Eight whiskers, four on upper jaw and four below, around the mouth that look like the barbels of a catfish." });
            appearances[CreatureConstants.Dragon_Gold_Adult] = GetWeightedAppearances(
                allSkin: new[] { "Golden metallic scales" },
                allEyes: new[] { "Slanted, very narrow golden eyes with somewhat-faded pupils" },
                allOther: new[] { "Twin horns, smooth and metallic, coming off the head. Twin neck frills. Ten whiskers, five on upper jaw and five below, around the mouth that look like the barbels of a catfish." });
            appearances[CreatureConstants.Dragon_Gold_MatureAdult] = GetWeightedAppearances(
                allSkin: new[] { "Golden metallic scales" },
                allEyes: new[] { "Slanted, very narrow golden eyes with faded pupils" },
                allOther: new[] { "Twin horns, smooth and metallic, coming off the head. Twin neck frills. Ten whiskers, five on upper jaw and five below, around the mouth that look like the barbels of a catfish." });
            appearances[CreatureConstants.Dragon_Gold_Old] = GetWeightedAppearances(
                allSkin: new[] { "Golden metallic scales" },
                allEyes: new[] { "Slanted, very narrow golden eyes with faded pupils" },
                allOther: new[] { "Twin horns, smooth and metallic, coming off the head. Twin neck frills. Ten whiskers, five on upper jaw and five below, around the mouth that look like the barbels of a catfish." });
            appearances[CreatureConstants.Dragon_Gold_VeryOld] = GetWeightedAppearances(
                allSkin: new[] { "Golden metallic scales" },
                allEyes: new[] { "Slanted, very narrow golden eyes with faded pupils" },
                allOther: new[] { "Twin horns, smooth and metallic, coming off the head. Twin neck frills. Ten whiskers, five on upper jaw and five below, around the mouth that look like the barbels of a catfish." });
            appearances[CreatureConstants.Dragon_Gold_Ancient] = GetWeightedAppearances(
                allSkin: new[] { "Golden metallic scales" },
                allEyes: new[] { "Slanted, very narrow eyes that lack pupils, resembling pools of molten gold." },
                allOther: new[] { "Twin horns, smooth and metallic, coming off the head. Twin neck frills. Twelve whiskers, six on upper jaw and six below, around the mouth that look like the barbels of a catfish." });
            appearances[CreatureConstants.Dragon_Gold_Wyrm] = GetWeightedAppearances(
                allSkin: new[] { "Golden metallic scales" },
                allEyes: new[] { "Slanted, very narrow eyes that lack pupils, resembling pools of molten gold." },
                allOther: new[] { "Twin horns, smooth and metallic, coming off the head. Twin neck frills. Twelve whiskers, six on upper jaw and six below, around the mouth that look like the barbels of a catfish." });
            appearances[CreatureConstants.Dragon_Gold_GreatWyrm] = GetWeightedAppearances(
                allSkin: new[] { "Golden metallic scales" },
                allEyes: new[] { "Slanted, very narrow eyes that lack pupils, resembling pools of molten gold." },
                allOther: new[] { "Twin horns, smooth and metallic, coming off the head. Twin neck frills. Twelve whiskers, six on upper jaw and six below, around the mouth that look like the barbels of a catfish." });
            appearances[CreatureConstants.Dragon_Green_Wyrmling] = GetWeightedAppearances(
                allSkin: new[] { "Thin, very small, deep black-green scales. Wings have a dappled pattern, darker near the leading edges and lighter toward the trailing edges." },
                allEyes: new[] { "Green eyes" },
                allOther: new[] { "Heavily curved jawline and a crest that begins near the eyes and continues down most of the spine. The crest reaches its full height just behind the skull. No external ears, just ear openings." });
            appearances[CreatureConstants.Dragon_Green_VeryYoung] = GetWeightedAppearances(
                allSkin: new[] { "Thin, very small, black-green scales. Wings have a dappled pattern, darker near the leading edges and lighter toward the trailing edges." },
                allEyes: new[] { "Green eyes" },
                allOther: new[] { "Heavily curved jawline and a crest that begins near the eyes and continues down most of the spine. The crest reaches its full height just behind the skull. No external ears, just ear openings." });
            appearances[CreatureConstants.Dragon_Green_Young] = GetWeightedAppearances(
                allSkin: new[] { "Thin, small, forest green scales. Wings have a dappled pattern, darker near the leading edges and lighter toward the trailing edges." },
                allEyes: new[] { "Green eyes" },
                allOther: new[] { "Heavily curved jawline and a crest that begins near the eyes and continues down most of the spine. The crest reaches its full height just behind the skull. No external ears, just ear openings." });
            appearances[CreatureConstants.Dragon_Green_Juvenile] = GetWeightedAppearances(
                allSkin: new[] { "Thin, small, forest green scales. Wings have a dappled pattern, darker near the leading edges and lighter toward the trailing edges." },
                allEyes: new[] { "Green eyes" },
                allOther: new[] { "Heavily curved jawline and a crest that begins near the eyes and continues down most of the spine. The crest reaches its full height just behind the skull. No external ears, just ear openings." });
            appearances[CreatureConstants.Dragon_Green_YoungAdult] = GetWeightedAppearances(
                allSkin: new[] { "Forest green scales. Wings have a dappled pattern, darker near the leading edges and lighter toward the trailing edges." },
                allEyes: new[] { "Green eyes" },
                allOther: new[] { "Heavily curved jawline and a crest that begins near the eyes and continues down most of the spine. The crest reaches its full height just behind the skull. No external ears, just ear openings." });
            appearances[CreatureConstants.Dragon_Green_Adult] = GetWeightedAppearances(
                allSkin: new[] { "Forest green scales. Wings have a dappled pattern, darker near the leading edges and lighter toward the trailing edges." },
                allEyes: new[] { "Green eyes" },
                allOther: new[] { "Heavily curved jawline and a crest that begins near the eyes and continues down most of the spine. The crest reaches its full height just behind the skull. No external ears, just ear openings." });
            appearances[CreatureConstants.Dragon_Green_MatureAdult] = GetWeightedAppearances(
                allSkin: new[] { "Large, emerald scales. Wings have a dappled pattern, darker near the leading edges and lighter toward the trailing edges." },
                allEyes: new[] { "Green eyes" },
                allOther: new[] { "Heavily curved jawline and a crest that begins near the eyes and continues down most of the spine. The crest reaches its full height just behind the skull. No external ears, just ear openings." });
            appearances[CreatureConstants.Dragon_Green_Old] = GetWeightedAppearances(
                allSkin: new[] { "Large, emerald scales. Wings have a dappled pattern, darker near the leading edges and lighter toward the trailing edges." },
                allEyes: new[] { "Green eyes" },
                allOther: new[] { "Heavily curved jawline and a crest that begins near the eyes and continues down most of the spine. The crest reaches its full height just behind the skull. No external ears, just ear openings." });
            appearances[CreatureConstants.Dragon_Green_VeryOld] = GetWeightedAppearances(
                allSkin: new[] { "Thick, large, emerald scales. Wings have a dappled pattern, darker near the leading edges and lighter toward the trailing edges." },
                allEyes: new[] { "Green eyes" },
                allOther: new[] { "Heavily curved jawline and a crest that begins near the eyes and continues down most of the spine. The crest reaches its full height just behind the skull. No external ears, just ear openings." });
            appearances[CreatureConstants.Dragon_Green_Ancient] = GetWeightedAppearances(
                allSkin: new[] { "Thick, huge, olive-green scales. Wings have a dappled pattern, darker near the leading edges and lighter toward the trailing edges." },
                allEyes: new[] { "Green eyes" },
                allOther: new[] { "Heavily curved jawline and a crest that begins near the eyes and continues down most of the spine. The crest reaches its full height just behind the skull. No external ears, just ear openings." });
            appearances[CreatureConstants.Dragon_Green_Wyrm] = GetWeightedAppearances(
                allSkin: new[] { "Thick, huge, olive-green scales. Wings have a dappled pattern, darker near the leading edges and lighter toward the trailing edges." },
                allEyes: new[] { "Green eyes" },
                allOther: new[] { "Heavily curved jawline and a crest that begins near the eyes and continues down most of the spine. The crest reaches its full height just behind the skull. No external ears, just ear openings." });
            appearances[CreatureConstants.Dragon_Green_GreatWyrm] = GetWeightedAppearances(
                allSkin: new[] { "Thick, huge, olive-green scales. Wings have a dappled pattern, darker near the leading edges and lighter toward the trailing edges." },
                allEyes: new[] { "Green eyes" },
                allOther: new[] { "Heavily curved jawline and a crest that begins near the eyes and continues down most of the spine. The crest reaches its full height just behind the skull. No external ears, just ear openings." });
            appearances[CreatureConstants.Dragon_Red_Wyrmling] = GetWeightedAppearances(
                allSkin: new[] { "Small, bright, glossy, scarlet scales" },
                allEyes: new[] { "Red eyes" },
                allOther: new[] { "Two straight, bone-white horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, bone-white horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, light gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, light gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, dark gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, dark gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, faded black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, faded black sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, night-black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, night-black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears." });
            appearances[CreatureConstants.Dragon_Red_VeryYoung] = GetWeightedAppearances(
                allSkin: new[] { "Small, glossy, scarlet scales" },
                allEyes: new[] { "Red eyes" },
                allOther: new[] { "Two straight, bone-white horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, bone-white horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, light gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, light gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, dark gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, dark gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, faded black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, faded black sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, night-black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, night-black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears." });
            appearances[CreatureConstants.Dragon_Red_Young] = GetWeightedAppearances(
                allSkin: new[] { "Small scarlet scales" },
                allEyes: new[] { "Red eyes" },
                allOther: new[] { "Two straight, bone-white horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, bone-white horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, light gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, light gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, dark gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, dark gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, faded black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, faded black sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, night-black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, night-black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears." });
            appearances[CreatureConstants.Dragon_Red_Juvenile] = GetWeightedAppearances(
                allSkin: new[] { "Smooth, dull, deep red scales" },
                allEyes: new[] { "Red eyes with somewhat-faded pupils" },
                allOther: new[] { "Two straight, bone-white horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, bone-white horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, light gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, light gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, dark gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, dark gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, faded black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, faded black sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, night-black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, night-black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears." });
            appearances[CreatureConstants.Dragon_Red_YoungAdult] = GetWeightedAppearances(
                allSkin: new[] { "Smooth, dull, deep red scales" },
                allEyes: new[] { "Red eyes with somewhat-faded pupils" },
                allOther: new[] { "Two straight, bone-white horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, bone-white horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, light gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, light gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, dark gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, dark gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, faded black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, faded black sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, night-black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, night-black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears." });
            appearances[CreatureConstants.Dragon_Red_Adult] = GetWeightedAppearances(
                allSkin: new[] { "Smooth, dull, deep red scales" },
                allEyes: new[] { "Red eyes with somewhat-faded pupils" },
                allOther: new[] { "Two straight, bone-white horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, bone-white horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, light gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, light gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, dark gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, dark gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, faded black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, faded black sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two straight, night-black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears.",
                    "Two twisted, night-black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears." });
            appearances[CreatureConstants.Dragon_Red_MatureAdult] = GetWeightedAppearances(
                allSkin: new[] { "Smooth, dull, deep red scales" },
                allEyes: new[] { "Red eyes with faded pupils" },
                allOther: new[] { "Two straight, bone-white horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, bone-white horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, light gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, light gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, dark gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, dark gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, faded black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, faded black sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, night-black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, night-black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns." });
            appearances[CreatureConstants.Dragon_Red_Old] = GetWeightedAppearances(
                allSkin: new[] { "Large, thick red scales" },
                allEyes: new[] { "Red eyes with faded pupils" },
                allOther: new[] { "Two straight, bone-white horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, bone-white horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, light gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, light gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, dark gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, dark gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, faded black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, faded black sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, night-black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, night-black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns." });
            appearances[CreatureConstants.Dragon_Red_VeryOld] = GetWeightedAppearances(
                allSkin: new[] { "Large, thick red scales" },
                allEyes: new[] { "Red eyes with faded pupils" },
                allOther: new[] { "Two straight, bone-white horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, bone-white horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, light gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, light gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, dark gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, dark gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, faded black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, faded black sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, night-black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, night-black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns." });
            appearances[CreatureConstants.Dragon_Red_Ancient] = GetWeightedAppearances(
                allSkin: new[] { "Large, thick red scales" },
                allEyes: new[] { "Red eyes with heavily-faded pupils" },
                allOther: new[] { "Two straight, bone-white horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, bone-white horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, light gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, light gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, dark gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, dark gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, faded black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, faded black sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, night-black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, night-black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns." });
            appearances[CreatureConstants.Dragon_Red_Wyrm] = GetWeightedAppearances(
                allSkin: new[] { "Large, thick red scales" },
                allEyes: new[] { "Red eyes with heavily-faded pupils" },
                allOther: new[] { "Two straight, bone-white horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, bone-white horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, light gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, light gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, dark gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, dark gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, faded black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, faded black sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, night-black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, night-black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns." });
            appearances[CreatureConstants.Dragon_Red_GreatWyrm] = GetWeightedAppearances(
                allSkin: new[] { "Large, thick red scales" },
                allEyes: new[] { "Eyes with no pupils, resembling molten lava orbs" },
                allOther: new[] { "Two straight, bone-white horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, bone-white horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, light gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, light gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, dark gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, dark gray horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, faded black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, faded black sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two straight, night-black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns.",
                    "Two twisted, night-black horns sweep back atop the head. Rows of small horns run along the top of the head. Small horns on the cheeks and lower jaw as well. Fringed ears that merge with the cheek horns." });
            appearances[CreatureConstants.Dragon_Silver_Wyrmling] = GetWeightedAppearances(
                allSkin: new[] { "Blue-gray scales with silver highlights" },
                allEyes: new[] { "Silver eyes" },
                allOther: new[] { "Smooth, shiny plate for a face. A frill rises high over its head and continues down the neck and back to the tip of the tail. The frill is supported by long spines with dark tips. Two smooth, shiny horns with dark tips." });
            appearances[CreatureConstants.Dragon_Silver_VeryYoung] = GetWeightedAppearances(
                allSkin: new[] { "Blue-gray scales with silver highlights" },
                allEyes: new[] { "Silver eyes" },
                allOther: new[] { "Smooth, shiny plate for a face. A frill rises high over its head and continues down the neck and back to the tip of the tail. The frill is supported by long spines with dark tips. Two smooth, shiny horns with dark tips." });
            appearances[CreatureConstants.Dragon_Silver_Young] = GetWeightedAppearances(
                allSkin: new[] { "Gray scales with silver highlights" },
                allEyes: new[] { "Silver eyes" },
                allOther: new[] { "Smooth, shiny plate for a face. A frill rises high over its head and continues down the neck and back to the tip of the tail. The frill is supported by long spines with dark tips. Two smooth, shiny horns with dark tips." });
            appearances[CreatureConstants.Dragon_Silver_Juvenile] = GetWeightedAppearances(
                allSkin: new[] { "Gray scales mottled with metallic silver" },
                allEyes: new[] { "Silver eyes with somewhat-faded pupils" },
                allOther: new[] { "Smooth, shiny plate for a face. A frill rises high over its head and continues down the neck and back to the tip of the tail. The frill is supported by long spines with dark tips. Two smooth, shiny horns with dark tips." });
            appearances[CreatureConstants.Dragon_Silver_YoungAdult] = GetWeightedAppearances(
                allSkin: new[] { "Silver scales" },
                allEyes: new[] { "Silver eyes with somewhat-faded pupils" },
                allOther: new[] { "Smooth, shiny plate for a face. A frill rises high over its head and continues down the neck and back to the tip of the tail. The frill is supported by long spines with dark tips. Two smooth, shiny horns with dark tips." });
            appearances[CreatureConstants.Dragon_Silver_Adult] = GetWeightedAppearances(
                allSkin: new[] { "Silver scales" },
                allEyes: new[] { "Silver eyes with somewhat-faded pupils" },
                allOther: new[] { "Smooth, shiny plate for a face. A frill rises high over its head and continues down the neck and back to the tip of the tail. The frill is supported by long spines with dark tips. Two smooth, shiny horns with dark tips." });
            appearances[CreatureConstants.Dragon_Silver_MatureAdult] = GetWeightedAppearances(
                allSkin: new[] { "Silver scales" },
                allEyes: new[] { "Silver eyes with faded pupils" },
                allOther: new[] { "Smooth, shiny plate for a face. A frill rises high over its head and continues down the neck and back to the tip of the tail. The frill is supported by long spines with dark tips. Two smooth, shiny horns with dark tips." });
            appearances[CreatureConstants.Dragon_Silver_Old] = GetWeightedAppearances(
                allSkin: new[] { "Bright silver scales" },
                allEyes: new[] { "Silver eyes with faded pupils" },
                allOther: new[] { "Smooth, shiny plate for a face. A frill rises high over its head and continues down the neck and back to the tip of the tail. The frill is supported by long spines with dark tips. Two smooth, shiny horns with dark tips." });
            appearances[CreatureConstants.Dragon_Silver_VeryOld] = GetWeightedAppearances(
                allSkin: new[] { "Bright silver scales" },
                allEyes: new[] { "Silver eyes with faded pupils" },
                allOther: new[] { "Smooth, shiny plate for a face. A frill rises high over its head and continues down the neck and back to the tip of the tail. The frill is supported by long spines with dark tips. Two smooth, shiny horns with dark tips." });
            appearances[CreatureConstants.Dragon_Silver_Ancient] = GetWeightedAppearances(
                allSkin: new[] { "Bright silver scales" },
                allEyes: new[] { "Silver eyes with heavily-faded pupils" },
                allOther: new[] { "Smooth, shiny plate for a face. A frill rises high over its head and continues down the neck and back to the tip of the tail. The frill is supported by long spines with dark tips. Two smooth, shiny horns with dark tips." });
            appearances[CreatureConstants.Dragon_Silver_Wyrm] = GetWeightedAppearances(
                allSkin: new[] { "Bright silver scales" },
                allEyes: new[] { "Silver eyes with heavily-faded pupils" },
                allOther: new[] { "Smooth, shiny plate for a face. A frill rises high over its head and continues down the neck and back to the tip of the tail. The frill is supported by long spines with dark tips. Two smooth, shiny horns with dark tips." });
            appearances[CreatureConstants.Dragon_Silver_GreatWyrm] = GetWeightedAppearances(
                allSkin: new[] { "Bright silver scales" },
                allEyes: new[] { "Eyes without pupils, resembling orbs of mercury" },
                allOther: new[] { "Smooth, shiny plate for a face. A frill rises high over its head and continues down the neck and back to the tip of the tail. The frill is supported by long spines with dark tips. Two smooth, shiny horns with dark tips." });
            appearances[CreatureConstants.Dragon_White_Wyrmling] = GetWeightedAppearances(
                allSkin: new[] { "Glistening pure-white scales. Trailing edge of the wings show a pink tinge",
                    "Glistening pure-white scales. Trailing edge of the wings show a blue tinge" },
                allEyes: new[] { "Gray eyes" },
                allOther: new[] { "Small, sharp beak at the nose and a pointed chin. A crest supported by a single backward-curving spine tops the head. Scaled cheeks, spiny dewlaps, and a few protruding teeth when its mouth is closed." });
            appearances[CreatureConstants.Dragon_White_VeryYoung] = GetWeightedAppearances(
                allSkin: new[] { "Glistening pure-white scales. Trailing edge of the wings show a pink tinge",
                    "Glistening pure-white scales. Trailing edge of the wings show a blue tinge" },
                allEyes: new[] { "Gray eyes" },
                allOther: new[] { "Small, sharp beak at the nose and a pointed chin. A crest supported by a single backward-curving spine tops the head. Scaled cheeks, spiny dewlaps, and a few protruding teeth when its mouth is closed." });
            appearances[CreatureConstants.Dragon_White_Young] = GetWeightedAppearances(
                allSkin: new[] { "Glistening pure-white scales. Trailing edge of the wings show a pink tinge",
                    "Glistening pure-white scales. Trailing edge of the wings show a blue tinge" },
                allEyes: new[] { "Gray eyes" },
                allOther: new[] { "Small, sharp beak at the nose and a pointed chin. A crest supported by a single backward-curving spine tops the head. Scaled cheeks, spiny dewlaps, and a few protruding teeth when its mouth is closed." });
            appearances[CreatureConstants.Dragon_White_Juvenile] = GetWeightedAppearances(
                allSkin: new[] { "Glistening pure-white scales. Trailing edge of the wings show a pink tinge",
                    "Glistening pure-white scales. Trailing edge of the wings show a blue tinge" },
                allEyes: new[] { "Gray eyes" },
                allOther: new[] { "Small, sharp beak at the nose and a pointed chin. A crest supported by a single backward-curving spine tops the head. Scaled cheeks, spiny dewlaps, and a few protruding teeth when its mouth is closed." });
            appearances[CreatureConstants.Dragon_White_YoungAdult] = GetWeightedAppearances(
                allSkin: new[] { "Glistening pure-white scales. Trailing edge of the wings show a pink tinge",
                    "Glistening pure-white scales. Trailing edge of the wings show a blue tinge" },
                allEyes: new[] { "Gray eyes" },
                allOther: new[] { "Small, sharp beak at the nose and a pointed chin. A crest supported by a single backward-curving spine tops the head. Scaled cheeks, spiny dewlaps, and a few protruding teeth when its mouth is closed." });
            appearances[CreatureConstants.Dragon_White_Adult] = GetWeightedAppearances(
                allSkin: new[] { "Pure-white scales. Trailing edge of the wings show a pink tinge",
                    "Pure-white scales. Trailing edge of the wings show a blue tinge" },
                allEyes: new[] { "Gray eyes" },
                allOther: new[] { "Small, sharp beak at the nose and a pointed chin. A crest supported by a single backward-curving spine tops the head. Scaled cheeks, spiny dewlaps, and a few protruding teeth when its mouth is closed." });
            appearances[CreatureConstants.Dragon_White_MatureAdult] = GetWeightedAppearances(
                allSkin: new[] { "Pure-white scales. Trailing edge of the wings show a pink tinge",
                    "Pure-white scales. Trailing edge of the wings show a blue tinge" },
                allEyes: new[] { "Gray eyes" },
                allOther: new[] { "Small, sharp beak at the nose and a pointed chin. A crest supported by a single backward-curving spine tops the head. Scaled cheeks, spiny dewlaps, and a few protruding teeth when its mouth is closed." });
            appearances[CreatureConstants.Dragon_White_Old] = GetWeightedAppearances(
                allSkin: new[] { "Pure-white scales. Trailing edge of the wings show a pink tinge",
                    "Pure-white scales. Trailing edge of the wings show a blue tinge" },
                allEyes: new[] { "Gray eyes" },
                allOther: new[] { "Small, sharp beak at the nose and a pointed chin. A crest supported by a single backward-curving spine tops the head. Scaled cheeks, spiny dewlaps, and a few protruding teeth when its mouth is closed." });
            appearances[CreatureConstants.Dragon_White_VeryOld] = GetWeightedAppearances(
                allSkin: new[] { "White scales with scales of pale blue and light gray mixed in. Trailing edge of the wings show a pink tinge",
                    "White scales with scales of pale blue and light gray mixed in. Trailing edge of the wings show a blue tinge" },
                allEyes: new[] { "Gray eyes" },
                allOther: new[] { "Small, sharp beak at the nose and a pointed chin. A crest supported by a single backward-curving spine tops the head. Scaled cheeks, spiny dewlaps, and a few protruding teeth when its mouth is closed." });
            appearances[CreatureConstants.Dragon_White_Ancient] = GetWeightedAppearances(
                allSkin: new[] { "White scales with scales of pale blue and light gray mixed in. Trailing edge of the wings show a pink tinge",
                    "White scales with scales of pale blue and light gray mixed in. Trailing edge of the wings show a blue tinge" },
                allEyes: new[] { "Gray eyes" },
                allOther: new[] { "Small, sharp beak at the nose and a pointed chin. A crest supported by a single backward-curving spine tops the head. Scaled cheeks, spiny dewlaps, and a few protruding teeth when its mouth is closed." });
            appearances[CreatureConstants.Dragon_White_Wyrm] = GetWeightedAppearances(
                allSkin: new[] { "White scales with scales of pale blue and light gray mixed in. Trailing edge of the wings show a pink tinge",
                    "White scales with scales of pale blue and light gray mixed in. Trailing edge of the wings show a blue tinge" },
                allEyes: new[] { "Gray eyes" },
                allOther: new[] { "Small, sharp beak at the nose and a pointed chin. A crest supported by a single backward-curving spine tops the head. Scaled cheeks, spiny dewlaps, and a few protruding teeth when its mouth is closed." });
            appearances[CreatureConstants.Dragon_White_GreatWyrm] = GetWeightedAppearances(
                allSkin: new[] { "White scales with scales of pale blue and light gray mixed in. Trailing edge of the wings show a pink tinge",
                    "White scales with scales of pale blue and light gray mixed in. Trailing edge of the wings show a blue tinge" },
                allEyes: new[] { "Gray eyes" },
                allOther: new[] { "Small, sharp beak at the nose and a pointed chin. A crest supported by a single backward-curving spine tops the head. Scaled cheeks, spiny dewlaps, and a few protruding teeth when its mouth is closed." });
            //Source: https://forgottenrealms.fandom.com/wiki/Dragon_turtle
            appearances[CreatureConstants.DragonTurtle] = new[] { "Green skin, with a golden crest down the center of the head." };
            //Source: https://forgottenrealms.fandom.com/wiki/Dragonne
            appearances[CreatureConstants.Dragonne] = GetWeightedAppearances(
                allSkin: new[] { "Brassy scales" },
                allHair: new[] { "Thick, coarse mane encircling the face. Large feathery eyebrows" },
                commonEyes: new[] { "Big brass-colored eyes" },
                uncommonEyes: new[] { "Big bronze-colored eyes", "Big copper-colored eyes" });
            //Source: https://forgottenrealms.fandom.com/wiki/Dretch
            appearances[CreatureConstants.Dretch] = new[] { "Pale, rubbery, white skin", "Pale, rubbery, beige skin", "Pale, rubbery, blue skin" };
            //Source: https://forgottenrealms.fandom.com/wiki/Drider
            appearances[CreatureConstants.Drider] = GetWeightedAppearances(
                allSkin: new[] { "TODO Drow skin. Bottom half is TODO Spider skin" },
                allHair: new[] { "TODO Drow hair" },
                allEyes: new[] { "TODO Drow eyes" });
            //Source: https://forgottenrealms.fandom.com/wiki/Dryad
            appearances[CreatureConstants.Dryad] = new[] { "Delicate features seemingly made from soft wood. Hair looks as if made of leaves and foliage that changes color with the seasons." };
            //Source: https://www.d20srd.org/srd/monsters/dwarf.htm
            appearances[CreatureConstants.Dwarf_Deep] = GetWeightedAppearances(
                commonSkin: new[] { "Tan skin", "Brown skin", "Dark tan skin", "Dark brown skin", "Very dark tan skin", "Very dark brown skin" },
                uncommonSkin: new[] { "Tan skin with a reddish tinge", "Brown skin with a reddish tinge", "Dark tan skin with a reddish tinge",
                    "Dark brown skin with a reddish tinge", "Very dark tan skin with a reddish tinge", "Very dark brown skin with a reddish tinge" },
                allHair: new[] { "Bright red hair", "Straw blond hair", "Muted red hair", "Yellow hair", "Bright yellow hair", "Muted yellow hair", "Brown hair" },
                commonEyes: new[] { "Washed-out blue eyes" },
                uncommonEyes: new[] { "TODO Human eyes (Washed-out)" });
            appearances[CreatureConstants.Dwarf_Duergar] = GetWeightedAppearances(
                allSkin: new[] { "Tan skin", "Brown skin", "Dark tan skin", "Dark brown skin", "Very dark tan skin", "Very dark brown skin" },
                commonHair: new[] { "Bald" },
                uncommonHair: new[] { "Black hair", "Gray hair", "Brown hair" },
                allEyes: new[] { "TODO Human eyes (Bright)" });
            appearances[CreatureConstants.Dwarf_Hill] = GetWeightedAppearances(
                allSkin: new[] { "Tan skin", "Brown skin", "Dark tan skin", "Dark brown skin", "Very dark tan skin", "Very dark brown skin" },
                allHair: new[] { "Black hair", "Gray hair", "Brown hair" },
                allEyes: new[] { "TODO Human eyes (Bright)" });
            appearances[CreatureConstants.Dwarf_Mountain] = GetWeightedAppearances(
                allSkin: new[] { "Lightly tan skin", "Light Brown skin", "Tan skin", "Brown skin", "Dark tan skin", "Dark brown skin" },
                allHair: new[] { "Dark gray hair", "Light gray hair", "Light brown hair" },
                allEyes: new[] { "TODO Human eyes (Bright)" });
            //Source: https://www.google.com/search?q=species+of+eagle
            appearances[CreatureConstants.Eagle] = new[] { "Golden eagle", "Bald eagle", "Harpy eagle", "White-tailed eagle", "Black kite", "Steller's sea eagle",
                "Philippine eagle", "Haast's eagle", "Wedge-tailed eagle", "Common buzzard", "Black eagle", "Crowned eagle", "Javan hawk-eagle", "Eastern imperial eagle",
                "Indian spotted eagle", "African fish eagle", "Spanish imperial eagle", "Bonelli's eagle", "Bearded vulture", "Black-and-chestnut eagle", "Martial eagle",
                "Black-chested buzzard-eagle", "Eurasian griffon vulture", "Red kite", "Verreaux's eagle", "Crested serpent eagle", "Steppe eagle", "Cinereous vulture",
                "White-bellied sea eagle", "Booted eagle", "Short-toed snake eagle", "Tawny eagle", "Egyptian vulture", "Lesser spotted eagle", "Mountain hawk-eagle",
                "Greater spotted eagle", "Little eagle", "Brahminy kite", "Ornate hawk-eagle", "Black-and-white hawk-eagle", "Changeable hawk-eagle", "Crested eagle",
                "Rough-legged buzzard", "Hen harrier" };
            //Source: https://www.d20srd.org/srd/monsters/eagleGiant.htm
            appearances[CreatureConstants.Eagle_Giant] = new[] { "Golden eagle", "Bald eagle", "Harpy eagle", "White-tailed eagle", "Black kite", "Steller's sea eagle",
                "Philippine eagle", "Haast's eagle", "Wedge-tailed eagle", "Common buzzard", "Black eagle", "Crowned eagle", "Javan hawk-eagle", "Eastern imperial eagle",
                "Indian spotted eagle", "African fish eagle", "Spanish imperial eagle", "Bonelli's eagle", "Bearded vulture", "Black-and-chestnut eagle", "Martial eagle",
                "Black-chested buzzard-eagle", "Eurasian griffon vulture", "Red kite", "Verreaux's eagle", "Crested serpent eagle", "Steppe eagle", "Cinereous vulture",
                "White-bellied sea eagle", "Booted eagle", "Short-toed snake eagle", "Tawny eagle", "Egyptian vulture", "Lesser spotted eagle", "Mountain hawk-eagle",
                "Greater spotted eagle", "Little eagle", "Brahminy kite", "Ornate hawk-eagle", "Black-and-white hawk-eagle", "Changeable hawk-eagle", "Crested eagle",
                "Rough-legged buzzard", "Hen harrier" };
            //Source: https://forgottenrealms.fandom.com/wiki/Efreeti
            appearances[CreatureConstants.Efreeti] = new[] { "Red, burning skin", "Black, burning skin" };
            //Source: https://jurassicworld-evolution.fandom.com/wiki/Elasmosaurus
            appearances[CreatureConstants.Elasmosaurus] = new[] { "Beige skin" };
            //Source: https://forgottenrealms.fandom.com/wiki/Air_elemental
            appearances[CreatureConstants.Elemental_Air_Small] = new[] { "An amorphous, ever-shifting cloud" };
            appearances[CreatureConstants.Elemental_Air_Medium] = new[] { "An amorphous, ever-shifting cloud" };
            appearances[CreatureConstants.Elemental_Air_Large] = new[] { "An amorphous, ever-shifting cloud" };
            appearances[CreatureConstants.Elemental_Air_Huge] = new[] { "An amorphous, ever-shifting cloud" };
            appearances[CreatureConstants.Elemental_Air_Greater] = new[] { "An amorphous, ever-shifting cloud" };
            appearances[CreatureConstants.Elemental_Air_Elder] = new[] { "An amorphous, ever-shifting cloud" };
            //Source: https://forgottenrealms.fandom.com/wiki/Earth_elemental
            appearances[CreatureConstants.Elemental_Earth_Small] = GetWeightedAppearances(
                allSkin: new[] { "Vaguely humanoid with club-like arms made of jagged stone and a head made of both dirt and stone" },
                uncommonOther: new[] { "Chunks of minerals set within the stony body", "Chunks of gems set within the stony body",
                    "Chunks of metals set within the stony body" },
                rareOther: new[] { "Chunks of minerals and gems set within the stony body", "Chunks of gems and metals set within the stony body",
                    "Chunks of metals and minerals set within the stony body", "Chunks of minerals, gems, and metals set within the stony body" });
            appearances[CreatureConstants.Elemental_Earth_Medium] = GetWeightedAppearances(
                allSkin: new[] { "Vaguely humanoid with club-like arms made of jagged stone and a head made of both dirt and stone" },
                uncommonOther: new[] { "Chunks of minerals set within the stony body", "Chunks of gems set within the stony body",
                    "Chunks of metals set within the stony body" },
                rareOther: new[] { "Chunks of minerals and gems set within the stony body", "Chunks of gems and metals set within the stony body",
                    "Chunks of metals and minerals set within the stony body", "Chunks of minerals, gems, and metals set within the stony body" });
            appearances[CreatureConstants.Elemental_Earth_Large] = GetWeightedAppearances(
                allSkin: new[] { "Vaguely humanoid with club-like arms made of jagged stone and a head made of both dirt and stone" },
                uncommonOther: new[] { "Chunks of minerals set within the stony body", "Chunks of gems set within the stony body",
                    "Chunks of metals set within the stony body" },
                rareOther: new[] { "Chunks of minerals and gems set within the stony body", "Chunks of gems and metals set within the stony body",
                    "Chunks of metals and minerals set within the stony body", "Chunks of minerals, gems, and metals set within the stony body" });
            appearances[CreatureConstants.Elemental_Earth_Huge] = GetWeightedAppearances(
                allSkin: new[] { "Vaguely humanoid with club-like arms made of jagged stone and a head made of both dirt and stone" },
                uncommonOther: new[] { "Chunks of minerals set within the stony body", "Chunks of gems set within the stony body",
                    "Chunks of metals set within the stony body" },
                rareOther: new[] { "Chunks of minerals and gems set within the stony body", "Chunks of gems and metals set within the stony body",
                    "Chunks of metals and minerals set within the stony body", "Chunks of minerals, gems, and metals set within the stony body" });
            appearances[CreatureConstants.Elemental_Earth_Greater] = GetWeightedAppearances(
                allSkin: new[] { "Vaguely humanoid with club-like arms made of jagged stone and a head made of both dirt and stone" },
                uncommonOther: new[] { "Chunks of minerals set within the stony body", "Chunks of gems set within the stony body",
                    "Chunks of metals set within the stony body" },
                rareOther: new[] { "Chunks of minerals and gems set within the stony body", "Chunks of gems and metals set within the stony body",
                    "Chunks of metals and minerals set within the stony body", "Chunks of minerals, gems, and metals set within the stony body" });
            appearances[CreatureConstants.Elemental_Earth_Elder] = GetWeightedAppearances(
                allSkin: new[] { "Vaguely humanoid with club-like arms made of jagged stone and a head made of both dirt and stone" },
                uncommonOther: new[] { "Chunks of minerals set within the stony body", "Chunks of gems set within the stony body",
                    "Chunks of metals set within the stony body" },
                rareOther: new[] { "Chunks of minerals and gems set within the stony body", "Chunks of gems and metals set within the stony body",
                    "Chunks of metals and minerals set within the stony body", "Chunks of minerals, gems, and metals set within the stony body" });
            //Source: https://forgottenrealms.fandom.com/wiki/Fire_elemental
            appearances[CreatureConstants.Elemental_Fire_Small] = new[] { "A column of red flames", "A column of yellow flames", "A column of orange flames",
                "A column of red/yellow flames", "A column of yellow/orange flames", "A column of orange/red flames", "A column of red/yellow/orange flames" };
            appearances[CreatureConstants.Elemental_Fire_Medium] = new[] { "A column of red flames", "A column of yellow flames", "A column of orange flames",
                "A column of red/yellow flames", "A column of yellow/orange flames", "A column of orange/red flames", "A column of red/yellow/orange flames" };
            appearances[CreatureConstants.Elemental_Fire_Large] = new[] { "A column of red flames", "A column of yellow flames", "A column of orange flames",
                "A column of red/yellow flames", "A column of yellow/orange flames", "A column of orange/red flames", "A column of red/yellow/orange flames" };
            appearances[CreatureConstants.Elemental_Fire_Huge] = new[] { "A column of red flames", "A column of yellow flames", "A column of orange flames",
                "A column of red/yellow flames", "A column of yellow/orange flames", "A column of orange/red flames", "A column of red/yellow/orange flames" };
            appearances[CreatureConstants.Elemental_Fire_Greater] = new[] { "A column of red flames", "A column of yellow flames", "A column of orange flames",
                "A column of red/yellow flames", "A column of yellow/orange flames", "A column of orange/red flames", "A column of red/yellow/orange flames" };
            appearances[CreatureConstants.Elemental_Fire_Elder] = new[] { "A column of red flames", "A column of yellow flames", "A column of orange flames",
                "A column of red/yellow flames", "A column of yellow/orange flames", "A column of orange/red flames", "A column of red/yellow/orange flames" };
            //Source: https://forgottenrealms.fandom.com/wiki/Water_elemental
            appearances[CreatureConstants.Elemental_Water_Small] = new[] { "A cresting wave of water" };
            appearances[CreatureConstants.Elemental_Water_Medium] = new[] { "A cresting wave of water" };
            appearances[CreatureConstants.Elemental_Water_Large] = new[] { "A cresting wave of water" };
            appearances[CreatureConstants.Elemental_Water_Huge] = new[] { "A cresting wave of water" };
            appearances[CreatureConstants.Elemental_Water_Greater] = new[] { "A cresting wave of water" };
            appearances[CreatureConstants.Elemental_Water_Elder] = new[] { "A cresting wave of water" };
            //Source: https://en.wikipedia.org/wiki/African_bush_elephant#Description
            appearances[CreatureConstants.Elephant] = new[] { "Gray skin with scanty hairs. Large, pointed, triangular ears cover the whole shoulder." };
            //Source: https://forgottenrealms.fandom.com/wiki/Aquatic_elf
            appearances[CreatureConstants.Elf_Aquatic] = GetWeightedAppearances(
                commonSkin: new[] { "Deep green skin, mottled and striped with brown", "Blue skin with white stripes and patches",
                    "Deep blue skin with white stripes and patches", "Light blue skin with white stripes and patches",
                    "Pale silver-green skin" },
                uncommonSkin: new[] { "Pale silver-green skin" },
                commonEyes: new[] { "Turquoise eyes", "White eyes", "Black eyes", "Blue eyes", "Green eyes" },
                rareEyes: new[] { "Silver eyes" },
                commonHair: new[] { "Thick, somewhat-stringy black hair", "Thick, somewhat-stringy blue hair", "Thick, somewhat-stringy silver-white hair",
                        "Thick, somewhat-stringy blue-green hair", "Thick, somewhat-stringy emerald green hair" },
                uncommonHair: new[] { "Thick, somewhat-stringy red hair",
                    "Thick, somewhat-stringy, rough black hair", "Thick, somewhat-stringy, rough blue hair", "Thick, somewhat-stringy, rough silver-white hair",
                        "Thick, somewhat-stringy, rough blue-green hair", "Thick, somewhat-stringy, rough emerald green hair" },
                rareHair: new[] { "Thick, somewhat-stringy, rough red hair" },
                allOther: new[] { "Pointed ears. Long fingers and toes with thick webbing. Gills visible on the neck and ribs" });
            //Source: https://forgottenrealms.fandom.com/wiki/Drow
            //https://www.d20srd.org/srd/monsters/elf.htm
            appearances[CreatureConstants.Elf_Drow] = GetWeightedAppearances(
                commonSkin: new[] { "Black skin", "Dark blue skin", "Gray skin", "Dark gray skin", "Jet-black skin", "Obsidian-colored skin", "Blue skin",
                    "Gray-blue skin", "Black-blue skin" },
                rareSkin: new[] { "White (albino) skin" },
                commonEyes: new[] { "Bright red eyes", "Vivid red eyes" },
                uncommonEyes: new[] { "Pale white-blue eyes", "Pale white-lilac eyes", "Pale white-pink eyes", "Pale white-silver eyes", "Purple eyes", "Blue eyes" },
                rareEyes: new[] { "Green eyes", "Brown eyes", "Black eyes", "Amber eyes", "Rose-hued eyes" },
                commonHair: new[] { "Straight Stark white hair", "Wavy Stark white hair", "Curly Stark white hair", "Kinky Stark white hair",
                    "Straight Pale yellow hair", "Wavy Pale yellow hair", "Curly Pale yellow hair", "Kinky Pale yellow hair" },
                uncommonHair: new[] { "Straight Gray hair", "Wavy Gray hair", "Curly Gray hair", "Kinky Gray hair",
                    "Straight Pale Yellow hair", "Wavy Pale Yellow hair", "Curly Pale Yellow hair", "Kinky Pale Yellow hair",
                    "Straight Silver hair", "Wavy Silver hair", "Curly Silver hair", "Kinky Silver hair",
                    "Straight Red hair", "Straight Blond hair", "Straight Brown hair", "Straight Black hair",
                    "Pale TODO Human hair",
                    "Pale TODO High Elf hair" },
                allOther: new[] { "Pointed ears" });
            //Source: https://www.d20srd.org/srd/monsters/elf.htm
            //https://forgottenrealms.fandom.com/wiki/Grey_elf
            appearances[CreatureConstants.Elf_Gray] = GetWeightedAppearances(
                commonSkin: new[] { "Pale grey skin" },
                commonHair: new[] { "Straight Silver hair", "Wavy Silver hair", "Curly Silver hair", "Kinky Silver hair" },
                uncommonHair: new[] { "Straight pale-golden hair", "Wavy pale-golden hair", "Curly pale-golden hair", "Kinky pale-golden hair" },
                commonEyes: new[] { "Amber eyes" },
                uncommonEyes: new[] { "Violet eyes" },
                allOther: new[] { "Pointed ears" });
            //Source: https://www.d20srd.org/srd/monsters/elf.htm
            appearances[CreatureConstants.Elf_Half] = GetWeightedAppearances(
                commonSkin: new[] { "TODO Human skin",
                    "TODO High Elf Skin" },
                uncommonSkin: new[] { "TODO Grey Elf Skin",
                    "TODO Wood Elf Skin",
                    "TODO Wild Elf Skin",
                    "TODO Drow Skin" },
                commonHair: new[] { "TODO Human hair",
                    "TODO High Elf Hair" },
                uncommonHair: new[] { "TODO Grey Elf Hair",
                    "TODO Wood Elf Hair",
                    "TODO Wild Elf Hair",
                    "TODO Drow Hair" },
                commonEyes: new[] { "TODO Human eyes",
                    "TODO High Elf eyes" },
                uncommonEyes: new[] { "TODO Grey Elf Eyes",
                    "TODO Wood Elf Eyes",
                    "TODO Wild Elf Eyes",
                    "TODO Drow Eyes" },
                allOther: new[] { "Pointed ears" });
            //Source: https://forgottenrealms.fandom.com/wiki/High_elf
            appearances[CreatureConstants.Elf_High] = GetWeightedAppearances(
                commonSkin: new[] { "Brown skin", "Olive skin", "White skin", "Pink skin",
                    "Pale Brown skin", "Pale Olive skin", "Pale White skin", "Pale Pink skin" },
                uncommonSkin: new[] { "Dark brown skin", "Dark Olive skin", "Dark Pink skin" },
                commonHair: new[] { "Straight Black hair", "Wavy Black hair", "Curly Black hair", "Kinky Black hair",
                    "Straight White hair", "Wavy White hair", "Curly White hair", "Kinky White hair",
                    "Straight Silver hair", "Wavy Silver hair", "Curly Silver hair", "Kinky Silver hair",
                    "Straight Pale Gold hair", "Wavy Pale Gold hair", "Curly Pale Gold hair", "Kinky Pale Gold hair" },
                uncommonHair: new[] { "Straight Black hair with silvery hues", "Wavy Black hair with silvery hues",
                        "Curly Black hair with silvery hues", "Kinky Black hair with silvery hues",
                    "Straight Black hair with blond hues", "Wavy Black hair with blond hues", "Curly Black hair with blond hues", "Kinky Black hair with blond hues",
                    "Straight Black hair with copper hues", "Wavy Black hair with copper hues", "Curly Black hair with copper hues", "Kinky Black hair with copper hues",
                    "Straight White hair with silvery hues", "Wavy White hair with silvery hues", "Curly White hair with silvery hues",
                        "Kinky White hair with silvery hues",
                    "Straight White hair with blond hues", "Wavy White hair with blond hues", "Curly White hair with blond hues", "Kinky White hair with blond hues",
                    "Straight White hair with copper hues", "Wavy White hair with copper hues", "Curly White hair with copper hues", "Kinky White hair with copper hues",
                    "Straight Silver hair with silvery hues", "Wavy Silver hair with silvery hues", "Curly Silver hair with silvery hues",
                        "Kinky Silver hair with silvery hues",
                    "Straight Silver hair with blond hues", "Wavy Silver hair with blond hues", "Curly Silver hair with blond hues", "Kinky Silver hair with blond hues",
                    "Straight Silver hair with copper hues", "Wavy Silver hair with copper hues", "Curly Silver hair with copper hues",
                        "Kinky Silver hair with copper hues",
                    "Straight Pale Gold hair with silvery hues", "Wavy Pale Gold hair with silvery hues", "Curly Pale Gold hair with silvery hues",
                        "Kinky Pale Gold hair with silvery hues",
                    "Straight Pale Gold hair with blond hues", "Wavy Pale Gold hair with blond hues", "Curly Pale Gold hair with blond hues",
                        "Kinky Pale Gold hair with blond hues",
                    "Straight Pale Gold hair with copper hues", "Wavy Pale Gold hair with copper hues", "Curly Pale Gold hair with copper hues",
                        "Kinky Pale Gold hair with copper hues" },
                commonEyes: new[] { "Green eyes" },
                uncommonEyes: new[] { "Golden eyes", "Blue eyes", "Light Blue eyes", "Green eyes speckled with gold" },
                rareEyes: new[] { "Violet eyes", "Blue eyes speckled with gold", "Light Blue eyes speckled with gold", "Violet eyes speckled with gold",
                    "Solid green eyes, lacking pupils", "Solid golden eyes, lacking pupils", "Solid blue eyes, lacking pupils", "Solid light blue eyes, lacking pupils",
                    "Solid violet eyes, lacking pupils", "Solid green eyes speckled with gold, lacking pupils", "Solid golden eyes speckled with gold, lacking pupils",
                    "Solid blue eyes speckled with gold, lacking pupils", "Solid light blue eyes speckled with gold, lacking pupils",
                    "Solid violet eyes speckled with gold, lacking pupils" },
                allOther: new[] { "Pointed ears" });
            //Source: https://forgottenrealms.fandom.com/wiki/Wild_elf
            //https://www.d20srd.org/srd/monsters/elf.htm
            appearances[CreatureConstants.Elf_Wild] = GetWeightedAppearances(
                allSkin: new[] { "Light brown skin", "Brown skin", "Dark brown skin" },
                commonHair: new[] { "Straight Black hair", "Wavy Black hair", "Curly Black hair", "Kinky Black hair",
                    "Straight Dark Brown hair", "Wavy Dark Brown hair", "Curly Dark Brown hair", "Kinky Dark Brown hair",
                    "Straight Brown hair", "Wavy Brown hair", "Curly Brown hair", "Kinky Brown hair",
                    "Straight Light Brown hair", "Curly Light Brown hair", "Wavy Light Brown hair", "Kinky Light Brown hair" },
                uncommonHair: new[] { "Straight Silver hair", "Wavy Silver hair", "Curly Silver hair", "Kinky Silver hair",
                    "Straight Silvery-White hair", "Wavy Silvery-White hair", "Curly Silvery-White hair", "Kinky Silvery-White hair",
                    "Straight Gray hair", "Wavy Gray hair", "Curly Gray hair", "Kinky Gray hair",
                    "Straight White hair", "Wavy White hair", "Curly White hair", "Kinky White hair" },
                allEyes: new[] { "TODO High Elf eyes" },
                allOther: new[] { "Pointed ears" });
            //Source: https://forgottenrealms.fandom.com/wiki/Wood_elf
            //https://www.d20srd.org/srd/monsters/elf.htm
            appearances[CreatureConstants.Elf_Wood] = GetWeightedAppearances(
                allSkin: new[] { "Copper skin", "Tan skin" },
                commonHair: new[] { "Straight Black hair", "Straight Light Brown hair", "Straight Brown hair",
                    "Wavy Black hair", "Wavy Light Brown hair", "Wavy Brown hair",
                    "Curly Black hair", "Curly Light Brown hair", "Curly Brown hair",
                    "Kinky Black hair", "Kinky Light Brown hair", "Kinky Brown hair", },
                uncommonHair: new[] { "Straight Yellow hair", "Straight Blond hair", "Straight Copper-Red hair",
                    "Wavy Yellow hair", "Wavy Blond hair", "Wavy Copper-Red hair",
                    "Curly Yellow hair", "Curly Blond hair", "Curly Copper-Red hair",
                    "Kinky Yellow hair", "Kinky Blond hair", "Kinky Copper-Red hair" },
                allEyes: new[] { "Green eyes", "Brown eyes", "Hazel eyes" },
                allOther: new[] { "Pointed ears" });
            //Source: https://www.d20srd.org/srd/monsters/devil.htm#erinyes
            //https://forgottenrealms.fandom.com/wiki/Erinyes
            appearances[CreatureConstants.Erinyes] = GetWeightedAppearances(
                allSkin: new[] { "TODO Human skin" },
                allHair: new[] { "TODO Human hair" },
                allEyes: new[] { "Red eyes" });
            //Source: https://www.5esrd.com/database/creature/ethereal-filcher/
            appearances[CreatureConstants.EtherealFilcher] = new[] { "One foot. Four arms ending in hands with long, spindly fingers. Looks as if it has two heads, one on a long stalk of a neck and another on its abdomen." };
            //Source: https://www.d20srd.org/srd/monsters/etherealMarauder.htm
            appearances[CreatureConstants.EtherealMarauder] = new[] { "Bright blue skin", "Blue skin", "Deep blue skin",
                "Bright violet skin", "Violet skin", "Deep violet skin",
                "Bright blue-violet skin", "Blue-violet skin", "Deep blue-violet skin" };
            //Source: https://forgottenrealms.fandom.com/wiki/Ettercap
            appearances[CreatureConstants.Ettercap] = new[] { "Grey-purplish skin. Distended underbelly. Spider-like face, with fangs and multiple eyes. Two sharp, black, chitinous claws instead of hands and feet." };
            //Source: https://forgottenrealms.fandom.com/wiki/Ettin
            appearances[CreatureConstants.Ettin] = GetWeightedAppearances(
                allSkin: new[] { "Brown skin, crusted over with a thick layer of grime", "Olive skin, crusted over with a thick layer of grime",
                        "Pink skin, crusted over with a thick layer of grime", "Gray skin, crusted over with a thick layer of grime",
                    "Deep Brown skin, crusted over with a thick layer of grime", "Deep Olive skin, crusted over with a thick layer of grime",
                        "Deep Pink skin, crusted over with a thick layer of grime", "Deep Gray skin, crusted over with a thick layer of grime",
                    "Pale Brown skin, crusted over with a thick layer of grime", "Pale Olive skin, crusted over with a thick layer of grime",
                        "Pale Pink skin, crusted over with a thick layer of grime", "Pale Gray skin, crusted over with a thick layer of grime" },
                allHair: new[] { "Long, stringy TODO HILL GIANT hair",
                    "Long, stringy TODO STONE GIANT hair" },
                allEyes: new[] { "Large, watery TODO ORC eyes" },
                allOther: new[] { "Two heads. Horrid stench. Shovel jaws with lower canine teeth that protrude out like boar tusks." });
            //Source: https://beetleidentifications.com/fire-beetle/
            //https://forgottenrealms.fandom.com/wiki/Giant_fire_beetle
            appearances[CreatureConstants.FireBeetle_Giant] = GetWeightedAppearances(
                commonSkin: new[] { "Brown color" },
                uncommonSkin: new[] { "Shimmering green color", "Black color with red markings", "Red color", "Orange color",
                    "Orange color with brown flame-like markings", "Purple color with red markings" },
                allOther: new[] { "Red bioluminescent glands above each eye and near the abdomen" });
            //Source: https://www.d20srd.org/srd/monsters/formian.htm
            appearances[CreatureConstants.FormianWorker] = GetWeightedAppearances(
                allSkin: new[] { "Brownish-red carapace" },
                allOther: new[] { "Hands only suitable for manual labor" });
            appearances[CreatureConstants.FormianWarrior] = GetWeightedAppearances(
                allSkin: new[] { "Brownish-red carapace" },
                allOther: new[] { "Sharp mandibles and poison stingers" });
            appearances[CreatureConstants.FormianTaskmaster] = GetWeightedAppearances(
                allSkin: new[] { "Brownish-red carapace" },
                allOther: new[] { "Neither mandible nor mouth" });
            appearances[CreatureConstants.FormianMyrmarch] = GetWeightedAppearances(
                allSkin: new[] { "Brownish-red carapace" },
                allOther: new[] { "Claws capable of fine manipulation, like human hands" });
            appearances[CreatureConstants.FormianQueen] = GetWeightedAppearances(
                allSkin: new[] { "Brownish-red carapace" },
                allOther: new[] { "Body too large to allow for movement on her weak, atrophied legs" });
            //Source: https://forgottenrealms.fandom.com/wiki/Frost_worm
            appearances[CreatureConstants.FrostWorm] = GetWeightedAppearances(
                commonSkin: new[] { "Blue-white skin", "Very pale blue skin" },
                uncommonSkin: new[] { "Blue-white skin with a purple underbelly", "Very pale blue skin with a purple underbelly" },
                allOther: new[] { "Huge mandibles. Nodule from which it emits a trilling noise." });
            //Source: https://forgottenrealms.fandom.com/wiki/Gargoyle - adding my own info for skin tones
            appearances[CreatureConstants.Gargoyle] = GetWeightedAppearances(
                allSkin: new[] { "Brown stone skin", "Dark Brown stone skin", "Light Brown stone skin",
                    "Gray stone skin", "Dark Gray stone skin","Light Gray stone skin",
                    "White stone skin", "Marble stone skin", "Black stone skin" },
                allOther: new[] { "Demon-like humanoid" });
            appearances[CreatureConstants.Gargoyle_Kapoacinth] = GetWeightedAppearances(
                allSkin: new[] { "Brown stone skin", "Dark Brown stone skin", "Light Brown stone skin",
                    "Gray stone skin", "Dark Gray stone skin","Light Gray stone skin",
                    "White stone skin", "Marble stone skin", "Black stone skin" },
                allOther: new[] { "Demon-like humanoid" });
            //Source: https://forgottenrealms.fandom.com/wiki/Gelatinous_cube
            appearances[CreatureConstants.GelatinousCube] = GetWeightedAppearances(
                allSkin: new[] { "Completely transparent cube, with only a glint on the surface to give away its position",
                    "Completely transparent rhombohedron, with only a glint on the surface to give away its position" },
                uncommonOther: new[] { "An object floats within the cube's body", "Two objects float within the cube's body",
                    "Three objects float within the cube's body" });
            //Source: https://forgottenrealms.fandom.com/wiki/Ghaele
            appearances[CreatureConstants.Ghaele] = GetWeightedAppearances(
                allSkin: new[] { "TODO High Elf skin" },
                allHair: new[] { "TODO High Elf hair" },
                allEyes: new[] { "Opalescent pearl eyes" });
            //Source: https://forgottenrealms.fandom.com/wiki/Ghoul
            appearances[CreatureConstants.Ghoul] = GetWeightedAppearances(
                allSkin: new[] { "Mottled, decaying, gray hide stretched tight over its emaciated body",
                    "Mottled, decaying, purple hide stretched tight over its emaciated body",
                    "Mottled, decaying, pink hide stretched tight over its emaciated body" },
                allHair: new[] { "Almost hairless", "Hairless" },
                allEyes: new[] { "Sunken eyes, burning like hot coals" });
            appearances[CreatureConstants.Ghoul_Ghast] = GetWeightedAppearances(
                allSkin: new[] { "Mottled, decaying, gray hide stretched tight over its emaciated body",
                    "Mottled, decaying, purple hide stretched tight over its emaciated body",
                    "Mottled, decaying, pink hide stretched tight over its emaciated body",
                    "Mottled, decaying, white hide stretched tight over its emaciated body" },
                allHair: new[] { "Almost hairless", "Hairless" },
                allEyes: new[] { "Sunken eyes, burning like hot coals" });
            appearances[CreatureConstants.Ghoul_Lacedon] = GetWeightedAppearances(
                allSkin: new[] { "Mottled, decaying, gray hide stretched tight over its emaciated body",
                    "Mottled, decaying, purple hide stretched tight over its emaciated body",
                    "Mottled, decaying, pink hide stretched tight over its emaciated body",
                    "Mottled, decaying, green hide stretched tight over its emaciated body" },
                allHair: new[] { "Almost hairless", "Hairless" },
                allEyes: new[] { "Sunken eyes, burning like hot coals" });
            //Source: https://forgottenrealms.fandom.com/wiki/Cloud_giant
            appearances[CreatureConstants.Giant_Cloud] = GetWeightedAppearances(
                allSkin: new[] { "Milky white skin", "Light sky-blue skin" },
                commonHair: new[] { "Straight silvery-white hair", "Wavy silvery-white hair", "Curly silvery-white hair", "Kinky silvery-white hair",
                    "Straight brass hair", "Wavy brass hair", "Curly brass hair", "Kinky brass hair"},
                uncommonHair: new[] { "Straight sky-blue hair", "Wavy sky-blue hair", "Curly sky-blue hair", "Kinky sky-blue hair" },
                allEyes: new[] { "Iridescent blue eyes" });
            //Source: https://forgottenrealms.fandom.com/wiki/Fire_giant
            appearances[CreatureConstants.Giant_Fire] = GetWeightedAppearances(
                allSkin: new[] { "Coal-black skin", "Coal-gray skin" },
                allHair: new[] { "Bright orange hair", "Bright red hair", "Flaming orange hair", "Flaming red hair" },
                allEyes: new[] { "Red eyes", "Orange eyes", "Fiery red eyes", "Fiery orange eyes" });
            //Source: https://forgottenrealms.fandom.com/wiki/Frost_giant
            appearances[CreatureConstants.Giant_Frost] = GetWeightedAppearances(
                allSkin: new[] { "Ivory-white skin", "Glacial-blue skin" },
                allHair: new[] { "Blue-white hair", "Light blue hair", "Pale white hair", "Dirty yellow hair", "TODO Males grow beards" },
                allEyes: new[] { "Pale blue eyes", "Yellow eyes" });
            //Source: https://forgottenrealms.fandom.com/wiki/Hill_giant
            appearances[CreatureConstants.Giant_Hill] = GetWeightedAppearances(
                allSkin: new[] { "Tan skin", "Reddish-brown skin", "Brown skin", "Dark tan skin",
                    "Deep ruddy-brown skin", "Light tan skin" },
                allHair: new[] { "Brown hair", "Dark brown hair", "Black hair" },
                allEyes: new[] { "Black, red-rimmed eyes", "Dark brown, red-rimmed eyes", "Brown, red-rimmed eyes" },
                allOther: new[] { "Long arms, stooped shoulders, low forehead" });
            //Source: https://forgottenrealms.fandom.com/wiki/Stone_giant
            appearances[CreatureConstants.Giant_Stone] = GetWeightedAppearances(
                allSkin: new[] { "Gray skin", "Grayish-brown skin" },
                allHair: new[] { "Dark gray hair", "Gray hair", "Bluish-gray hair", "Bald" },
                allEyes: new[] { "Silver eyes", "Steel-colored eyes" });
            appearances[CreatureConstants.Giant_Stone_Elder] = GetWeightedAppearances(
                allSkin: new[] { "Gray skin", "Grayish-brown skin" },
                allHair: new[] { "Dark gray hair", "Gray hair", "Bluish-gray hair", "Bald" },
                allEyes: new[] { "Silver eyes", "Steel-colored eyes" });
            //Source: https://forgottenrealms.fandom.com/wiki/Storm_giant
            appearances[CreatureConstants.Giant_Storm] = GetWeightedAppearances(
                commonSkin: new[] { "Pale light-green skin skin" },
                uncommonSkin: new[] { "Pale purple-gray skin" },
                rareSkin: new[] { "Violet skin" },
                commonHair: new[] { "Dark green hair" },
                uncommonHair: new[] { "Pale purple-gray hair", "Deep violet hair" },
                rareHair: new[] { "Dark blue hair", "Blue-black hair" },
                commonEyes: new[] { "Emerald eyes", "Dark green eyes" },
                uncommonEyes: new[] { "Purple eyes" },
                rareEyes: new[] { "Silvery-gray eyes", "Silver eyes" });
            //Source: https://forgottenrealms.fandom.com/wiki/Gibbering_mouther
            appearances[CreatureConstants.GibberingMouther] = GetWeightedAppearances(
                allSkin: new[] { "Bright red skin", "Pink skin", "White-red skin", "Brown skin", "Brown-green skin" },
                allOther: new[] { "Large blob of mouth and eyes" });
            //Source: https://forgottenrealms.fandom.com/wiki/Girallon
            appearances[CreatureConstants.Girallon] = GetWeightedAppearances(
                allSkin: new[] { "Beige skin", "Light brown skin", "Pink skin" },
                allHair: new[] { "Thick white fur" },
                allOther: new[] { "Four arms. Curved tusks in its mouth." });
            //Source: https://forgottenrealms.fandom.com/wiki/Githyanki
            appearances[CreatureConstants.Githyanki] = GetWeightedAppearances(
                allSkin: new[] { "Rough, leathery yellow skin" },
                allHair: new[] { "Russet hair in a topknot", "Black hair in a topknot", "Red hair in a topknot" },
                allEyes: new[] { "Bright black eyes sunk deep in the orbits" },
                allOther: new[] { "Pointed ears, serrated in the back. Long angular skull, with a small and highly-placed flat nose. Pointed teeth." });
            //Source: https://forgottenrealms.fandom.com/wiki/Githzerai
            appearances[CreatureConstants.Githzerai] = GetWeightedAppearances(
                commonSkin: new[] { "Fair yellow skin", "Pale yellow skin" },
                uncommonSkin: new[] { "Pale yellow skin with green tones", "Pale yellow skin with brown tones" },
                commonHair: new[] { "Russet hair", "Black hair",
                    "TODO Male Bald (shaved), carefully groomed facial hair", "TODO Male braids, carefully groomed facial hair",
                    "TODO Female buns", "TODO Female braids" },
                uncommonHair: new[] { "Gray hair",
                    "TODO Female (but Male styles)" },
                allEyes: new[] { "Deep-set yellow eyes" },
                allOther: new[] { "Long angular skull, flattened nose, long pointed ears" });
            //Source: https://forgottenrealms.fandom.com/wiki/Glabrezu
            appearances[CreatureConstants.Glabrezu] = GetWeightedAppearances(
                allSkin: new[] { "Wrinkly, deep russet skin", "Wrinkly, russet-black skin", "Wrinkly, black skin", "Wrinkly, pitch-black skin" },
                allEyes: new[] { "Cold, purple, piercing eyes" },
                allOther: new[] { "Two pairs of arms: main arms (from the shoulders) are larger and end in pincers, smaller pair (from the stomach) is humanoid with clawed fingers. Goat horns atop the canine head. Numerous fangs in the muzzle." });
            //Source: https://forgottenrealms.fandom.com/wiki/Gnoll
            appearances[CreatureConstants.Gnoll] = GetWeightedAppearances(
                allSkin: new[] { "Greenish-gray skin" },
                commonHair: new[] { "Light brown fur, dirty yellow crest-like mane", "Light brown fur, reddish-gray crest-like mane",
                    "Light brown fur, dirty yellow-gray crest-like mane", "Light brown fur, dirty yellow-red crest-like mane",
                    "Light brown fur, yellow-gray crest-like mane", "Light brown fur, yellow-red crest-like mane",
                    "Dark brown fur, dirty yellow crest-like mane", "Dark brown fur, reddish-gray crest-like mane",
                    "Dark brown fur, dirty yellow-gray crest-like mane", "Dark brown fur, dirty yellow-red crest-like mane",
                    "Dark brown fur, yellow-gray crest-like mane", "Dark brown fur, yellow-red crest-like mane" },
                uncommonHair: new[] { "Light brown fur marked with spots and stripes, dirty yellow crest-like mane",
                    "Light brown fur marked with spots and stripes, reddish-gray crest-like mane",
                    "Light brown fur marked with spots and stripes, dirty yellow-gray crest-like mane",
                    "Light brown fur marked with spots and stripes, dirty yellow-red crest-like mane",
                    "Light brown fur marked with spots and stripes, yellow-gray crest-like mane",
                    "Light brown fur marked with spots and stripes, yellow-red crest-like mane",
                    "Dark brown fur marked with spots and stripes, dirty yellow crest-like mane",
                    "Dark brown fur marked with spots and stripes, reddish-gray crest-like mane",
                    "Dark brown fur marked with spots and stripes, dirty yellow-gray mane",
                    "Dark brown fur marked with spots and stripes, dirty yellow-red crest-like mane",
                    "Dark brown fur marked with spots and stripes, yellow-gray crest-like mane",
                    "Dark brown fur marked with spots and stripes, yellow-red crest-like mane" },
                rareHair: new[] { "Black fur, dirty yellow crest-like mane", "Black fur, reddish-gray crest-like mane",
                    "Black fur, dirty yellow-gray crest-like mane", "Black, dirty yellow-red crest-like mane",
                    "Black fur, yellow-gray crest-like mane", "Black fur, yellow-red crest-like mane",
                    "Black fur marked with fiery orange spots and stripes, dirty yellow crest-like mane",
                    "Black fur marked with fiery orange spots and stripes, reddish-gray crest-like mane",
                    "Black fur marked with fiery orange spots and stripes, dirty yellow-gray mane",
                    "Black fur marked with fiery orange spots and stripes, dirty yellow-red crest-like mane",
                    "Black fur marked with fiery orange spots and stripes, yellow-gray crest-like mane",
                    "Black fur marked with fiery orange spots and stripes, yellow-red crest-like mane" },
                commonEyes: new[] { "Green eyes", "Brown eyes" },
                rareEyes: new[] { "Gleaming red eyes" },
                allOther: new[] { "Hyena-like appearance" });
            //Source: https://forgottenrealms.fandom.com/wiki/Forest_gnome
            appearances[CreatureConstants.Gnome_Forest] = GetWeightedAppearances(
                allSkin: new[] { "Bark-colored skin", "Earthy brown skin" },
                commonHair: new[] { "Long, free black hair", "Long, free brown hair",
                    "Long, free black hair TODO MALE beard trimmed to fine point", "Long, free brown hair TODO MALE beard trimmed to fine point",
                    "Long, free black hair TODO MALE beard trimmed into hornlike spikes", "Long, free brown hair TODO MALE beard trimmed into hornlike spikes"  },
                uncommonHair: new[] { "Long, free gray hair",
                    "Long, free gray hair TODO MALE beard trimmed to fine point",
                    "Long, free gray hair TODO MALE beard trimmed into hornlike spikes",
                    "Long, free brown-gray hair",
                    "Long, free brown-gray hair TODO MALE beard trimmed to fine point",
                    "Long, free brown-gray hair TODO MALE beard trimmed into hornlike spikes" },
                rareHair: new[] { "Long, free white hair",
                    "Long, free white hair TODO MALE beard trimmed to fine point",
                    "Long, free white hair TODO MALE beard trimmed into hornlike spikes" },
                allEyes: new[] { "Blue eyes", "Brown eyes" });
            //Source: https://forgottenrealms.fandom.com/wiki/Rock_gnome
            //https://forgottenrealms.fandom.com/wiki/Gnome
            appearances[CreatureConstants.Gnome_Rock] = GetWeightedAppearances(
                allSkin: new[] { "Light tan skin", "Tan skin", "Light brown skin", "Brown skin" },
                allHair: new[] { "TODO Human style gray hair", "TODO Human style white hair",
                    "TODO Human style gray hair TODO MALE Neatly-trimmed beard", "TODO Human style white hair TODO MALE Neatly-trimmed beard"},
                commonEyes: new[] { "Glitering opaque orbs of black", "Glitering opaque orbs of blue" },
                uncommonEyes: new[] { "TODO Human eyes" });
            //Source: https://forgottenrealms.fandom.com/wiki/Deep_gnome
            //https://www.d20srd.org/srd/monsters/gnome.htm
            appearances[CreatureConstants.Gnome_Svirfneblin] = GetWeightedAppearances(
                allSkin: new[] { "Rocky brown skin", "Rocky brownish-gray skin", "Rocky gray skin" },
                allHair: new[] { "TODO MALE bald", "TODO FEMALE Stringy gray hair" },
                allEyes: new[] { "Dark gray eyes", "Black eyes" },
                allOther: new[] { "Gnarled and wiry" });
            //Source: https://forgottenrealms.fandom.com/wiki/Goblin
            //https://www.d20srd.org/srd/monsters/goblin.htm
            appearances[CreatureConstants.Goblin] = GetWeightedAppearances(
                commonSkin: new[] { "Yellow skin", "Red skin", "Orange skin", "Light orange skin", "Dark orange skin", "Deep red skin" },
                uncommonSkin: new[] { "Green skin" },
                allEyes: new[] { "Dull, glazed, yellow eyes", "Dull, glazed, orange eyes", "Dull, glazed, red eyes" },
                allOther: new[] { "Flat face, small fangs, pointed ears, sloped-back forehead, broad noses" });
            //Source: https://pathfinderwiki.com/wiki/Clay_golem
            //https://forgottenrealms.fandom.com/wiki/Clay_golem
            appearances[CreatureConstants.Golem_Clay] = new[] { "Humanoid body madde from clay" };
            //Source: https://www.d20srd.org/srd/monsters/golem.htm
            //https://forgottenrealms.fandom.com/wiki/Flesh_golem
            appearances[CreatureConstants.Golem_Flesh] = new[] { "Various decaying humanoid body parts stitched and bolted together" };
            //Source: https://www.d20srd.org/srd/monsters/golem.htm
            //https://forgottenrealms.fandom.com/wiki/Iron_golem
            appearances[CreatureConstants.Golem_Iron] = GetWeightedAppearances(
                allSkin: new[] { "Reddish-brown color", "Black color", "Black color with golden markings", "Rusted red color", "Shining steel color" },
                allOther: new[] { "Resembles a suit of armor, smooth features",
                    "Resembles a suit of armor, smooth features, symbol carved in the chest",
                    "Resembles a suit of armor, smooth features, designs carved into the limbs" });
            //Source: https://www.d20srd.org/srd/monsters/golem.htm
            //https://forgottenrealms.fandom.com/wiki/Stone_golem
            appearances[CreatureConstants.Golem_Stone] = GetWeightedAppearances(
                allSkin: new[] { "Stone gray color", "Sandy brown color" },
                allOther: new[] { "Looks like a carved statue", "Looks like a carved statue, appears as if wearing armor",
                    "Looks like a carved statue, appears as if wearing armor and has a symbol carved on the breastplate",
                    "Looks like a carved statue, symbol carved in the chest",
                    "Looks like a carved statue, designs carved into the limbs" });
            //Source: https://www.d20srd.org/srd/monsters/golem.htm
            //https://forgottenrealms.fandom.com/wiki/Stone_golem
            appearances[CreatureConstants.Golem_Stone_Greater] = GetWeightedAppearances(
                allSkin: new[] { "Stone gray color", "Sandy brown color" },
                allOther: new[] { "Looks like a carved statue", "Looks like a carved statue, stylized by its creator" });
            //Source: https://www.d20srd.org/srd/monsters/gorgon.htm
            //https://forgottenrealms.fandom.com/wiki/Gorgon
            appearances[CreatureConstants.Gorgon] = GetWeightedAppearances(
                allSkin: new[] { "Dusky metallic scales" },
                allOther: new[] { "Resembles a bull" });
            //Source: https://www.d20srd.org/srd/monsters/ooze.htm#grayOoze
            appearances[CreatureConstants.GrayOoze] = new[] { "A thick puddle of gray sludge resembling wet stone",
                "A thick puddle of gray sludge resembling an amorphous rock formation" };
            //Source: https://forgottenrealms.fandom.com/wiki/Gray_render
            appearances[CreatureConstants.GrayRender] = new[] { "Gray hairless skin. Six yellow eyes in two rows along the sloped forehead. Extra long arms drag on the ground." };
            //Source: https://forgottenrealms.fandom.com/wiki/Green_hag
            appearances[CreatureConstants.GreenHag] = GetWeightedAppearances(
                commonSkin: new[] { "Rough, bark-like, pallid-green skin with knobby, cancerous protrusions. Warts on the face and exaggerated facial features" },
                uncommonSkin: new[] { "Rough, bark-like, pallid-green skin with knobby, cancerous protrusions. Warts on the face",
                    "Rough, bark-like, pallid-green skin with knobby, cancerous protrusions. Exaggerated facial features",
                    "Rough, bark-like, pallid-green skin with knobby, cancerous protrusions" },
                allHair: new[] { "Tangled, vine-like, black hair", "Tangled, vine-like, gray hair", "Tangled, vine-like, white hair",
                    "Tangled, vine-like, moldy-olive-green hair" },
                allEyes: new[] { "Orange eyes", "Amber eyes" },
                allOther: new[] { "Needle-sharp fangs, black talons covered in filth", "Needle-sharp fangs, yellow talons covered in filth" });
            //Source: https://forgottenrealms.fandom.com/wiki/Grick
            appearances[CreatureConstants.Grick] = GetWeightedAppearances(
                allSkin: new[] { "Rubbery, uniform, dark green skin with a pale underbelly", "Rubbery, uniform, dark blue skin with a pale underbelly",
                    "Rubbery, uniform, blue skin with a pale underbelly" },
                allOther: new[] { "Snapping beak surrounded by large barbed tentacles" });
            //Source: https://forgottenrealms.fandom.com/wiki/Griffon
            appearances[CreatureConstants.Griffon] = GetWeightedAppearances(
                allSkin: new[] { "Dusky yellow fur", "TODO Lion skin" },
                allHair: new[] { "Golden feathers", "TODO Eagle feathers" },
                allEyes: new[] { "TODO Eagle eyes" },
                allOther: new[] { "Body of a lion. Wings, forelegs, and head of an eagle." });
            //Source: https://forgottenrealms.fandom.com/wiki/Grig
            appearances[CreatureConstants.Grig] = GetWeightedAppearances(
                allSkin: new[] { "Light blue skin on the torso, brown cricket-like body from the waist-down" },
                allHair: new[] { "Forest green hair" },
                allOther: new[] { "Gossamer wings, hairy legs" });
            appearances[CreatureConstants.Grig_WithFiddle] = GetWeightedAppearances(
                allSkin: new[] { "Light blue skin on the torso, brown cricket-like body from the waist-down" },
                allHair: new[] { "Forest green hair" },
                allOther: new[] { "Gossamer wings, hairy legs" });
            //Source: https://forgottenrealms.fandom.com/wiki/Grimlock
            appearances[CreatureConstants.Grimlock] = GetWeightedAppearances(
                commonSkin: new[] { "Slightly-scaled, thick, scarred, gray skin" },
                uncommonSkin: new[] { "Slightly-scaled, thick gray skin", "Slightly-scaled, thick gray skin with decorative designs scarred into the skin" },
                allHair: new[] { "Black hair, long and unkempt" },
                allEyes: new[] { "Completely white eyes", "Face devoid of eyes, skin stretching across where its eye sockets should be" },
                allOther: new[] { "Sharp teeth" });
            //Source: https://forgottenrealms.fandom.com/wiki/Gynosphinx
            appearances[CreatureConstants.Gynosphinx] = GetWeightedAppearances(
                allHair: new[] { "Tawny fur" },
                allOther: new[] { "Lion body, falcon wings" });
            //Source: https://forgottenrealms.fandom.com/wiki/Lightfoot_halfling
            //https://www.d20srd.org/srd/monsters/halfling.htm
            appearances[CreatureConstants.Halfling_Deep] = GetWeightedAppearances(
                commonSkin: new[] { "Ruddy skin" },
                uncommonSkin: new[] { "TODO Human skin" },
                commonHair: new[] { "Straight black hair", "Straight black hair TODO MALE Long sideburns", "Straight brown hair", "Straight brown hair TODO MALE Long sideburns" },
                uncommonHair: new[] { "Straight black hair TODO MALE Long sideburns", "Straight brown hair TODO MALE Long sideburns",
                    "Straight black hair TODO MALE beard", "Straight brown hair TODO MALE beard",
                    "Straight black hair TODO FEMALE Short sideburns", "Straight brown hair TODO FEMALE Short sideburns",
                    "TODO Human hair" },
                rareHair: new[] { "Straight black hair TODO MALE Mustache", "Straight brown hair TODO MALE Mustache",
                    "TODO Human hair" },
                commonEyes: new[] { "Brown eyes", "Black eyes", "Hazel eyes" },
                uncommonEyes: new[] { "TODO Human eyes" });
            //Source: https://forgottenrealms.fandom.com/wiki/Lightfoot_halfling
            appearances[CreatureConstants.Halfling_Lightfoot] = GetWeightedAppearances(
                commonSkin: new[] { "Ruddy skin" },
                uncommonSkin: new[] { "TODO Human skin" },
                commonHair: new[] { "Straight black hair", "Straight black hair TODO MALE Long sideburns", "Straight brown hair", "Straight brown hair TODO MALE Long sideburns" },
                uncommonHair: new[] { "Straight black hair TODO MALE Long sideburns", "Straight brown hair TODO MALE Long sideburns",
                    "Straight black hair TODO MALE beard", "Straight brown hair TODO MALE beard",
                    "Straight black hair TODO FEMALE Short sideburns", "Straight brown hair TODO FEMALE Short sideburns",
                    "TODO Human hair" },
                rareHair: new[] { "Straight black hair TODO MALE Mustache", "Straight brown hair TODO MALE Mustache",
                    "TODO Human hair" },
                commonEyes: new[] { "Brown eyes", "Black eyes", "Hazel eyes" },
                uncommonEyes: new[] { "TODO Human eyes" });
            //Source: https://forgottenrealms.fandom.com/wiki/Lightfoot_halfling
            //https://www.d20srd.org/srd/monsters/halfling.htm
            appearances[CreatureConstants.Halfling_Tallfellow] = GetWeightedAppearances(
                commonSkin: new[] { "Ruddy skin" },
                uncommonSkin: new[] { "TODO Human skin" },
                commonHair: new[] { "Straight black hair", "Straight black hair TODO MALE Long sideburns", "Straight brown hair", "Straight brown hair TODO MALE Long sideburns" },
                uncommonHair: new[] { "Straight black hair TODO MALE Long sideburns", "Straight brown hair TODO MALE Long sideburns",
                    "Straight black hair TODO MALE beard", "Straight brown hair TODO MALE beard",
                    "Straight black hair TODO FEMALE Short sideburns", "Straight brown hair TODO FEMALE Short sideburns",
                    "TODO Human hair" },
                rareHair: new[] { "Straight black hair TODO MALE Mustache", "Straight brown hair TODO MALE Mustache",
                    "TODO Human hair" },
                commonEyes: new[] { "Brown eyes", "Black eyes", "Hazel eyes" },
                uncommonEyes: new[] { "TODO Human eyes" });
            //Source: https://www.5esrd.com/database/race/harpy/
            appearances[CreatureConstants.Harpy] = GetWeightedAppearances(
                allSkin: new[] { "TODO Human skin" },
                allHair: new[] { "Filthy, tangled, TODO Human hair crusted with old, dry blood" },
                allEyes: new[] { "Coal black eyes" },
                allOther: new[] { "Scaly legs, clawed feet, and clawed hands with knotty fingers. Leathery wings. Breasts",
                    "Scaly legs, clawed feet, and clawed hands with knotty fingers. Feathery wings. Breasts" });
            //Source: https://www.dimensions.com/element/osprey-pandion-haliaetus
            appearances[CreatureConstants.Hawk] = new[] { "Red-tailed hawk", "Eurasian sparrowhawk", "Sharp-shinned hawk", "Osprey", "Harris' hawk", "Eurasian goshawk",
                "Black kite", "Red-shouldered hawk", "Cooper's hawk", "Broad-winged hawk", "Ferruginous hawk", "Swainson's hawk", "Tiny hawk", "White-tailed hawk",
                "Northern harrier", "Roadside hawk", "Great black hawk", "Common black hawk", "Crested goshawk", "Red kite", "Shikra", "Collared sparrowhawk",
                "Japanese sparrowhawk", "Chinese sparrowhawk", "Hen harrier", "Black-winged kite", "White-tailed kite", "Zone-tailed hawk", "Brahminy kite", "Besra",
                "Western marsh harrier", "Red-backed hawk", "Montagu's harrier", "Swamp harrier"
            };
            //Source: https://forgottenrealms.fandom.com/wiki/Hellcat
            appearances[CreatureConstants.Hellcat_Bezekira] = GetWeightedAppearances(
                allSkin: new[] { "Form of a lion", "Form of a tiger-sized domestic cat" },
                allEyes: new[] { "Crimson eyes", "Crimson eyes, backed by the literal fires of Hell" },
                allOther: new[] { "Faint, wraith-like outlines with bodies composed of bright light and flickers of fire - only visible in total darkness" });
            //Source: https://forgottenrealms.fandom.com/wiki/Hell_hound
            appearances[CreatureConstants.HellHound] = GetWeightedAppearances(
                allHair: new[] { "Rust-red fur with soot-colored markings", "Red-brown fur with soot-colored markings" },
                allEyes: new[] { "Glowing red eyes" },
                allOther: new[] { "Resembles monstrous dog. Soot-colored fangs and tongues. Reek of sulfurous smoke." });
            appearances[CreatureConstants.HellHound_NessianWarhound] = GetWeightedAppearances(
                allHair: new[] { "Coal-black fur with soot-colored markings" },
                allEyes: new[] { "Glowing red eyes" },
                allOther: new[] { "Resembles monstrous dog. Soot-colored fangs and tongues. Reek of sulfurous smoke." });
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            appearances[CreatureConstants.Hellwasp_Swarm] = new[] { "5,000 Hellwasps" };
            //Source: https://forgottenrealms.fandom.com/wiki/Hezrou
            appearances[CreatureConstants.Hezrou] = GetWeightedAppearances(
                allSkin: new[] { "Orange skin", "Yellow-green skin", "Mottled brown skin" },
                allEyes: new[] { "Glowing yellow eyes", "Glowing red eyes" },
                allOther: new[] { "Resemble a humanoid toad, wide maw with rows of blunt teeth, long spikes running down the back" });
            //Source: https://www.mojobob.com/roleplay/monstrousmanual/s/sphinx.html
            appearances[CreatureConstants.Hieracosphinx] = GetWeightedAppearances(
                allHair: new[] { "TODO Lion fur", "TODO Hawk feathers" },
                allOther: new[] { "Body of a lion, great feathery wings, head of a hawk" });
            //Source: https://forgottenrealms.fandom.com/wiki/Hippogriff
            appearances[CreatureConstants.Hippogriff] = GetWeightedAppearances(
                allSkin: new[] { "Ivory beak", "Golden yellow beak" },
                allHair: new[] { "Russet fur, russet feathers", "Golden tan fur, russet feathers", "Brown fur, russet feathers",
                    "Russet fur, golden tan feathers", "Golden tan fur, golden tan feathers", "Brown fur, golden tan feathers",
                    "Russet fur, brown feathers", "Golden tan fur, brown feathers", "Brown fur, brown feathers" },
                allOther: new[] { "Body of a horse, wings and head of a hawk, forelegs ending in sharp talons, hind legs end in hooves",
                    "Body of a horse, wings and head of a eagle, forelegs ending in sharp talons, hind legs end in hooves" });
            //Source: https://forgottenrealms.fandom.com/wiki/Hobgoblin
            //https://www.d20srd.org/srd/monsters/hobgoblin.htm
            appearances[CreatureConstants.Hobgoblin] = GetWeightedAppearances(
                allSkin: new[] { "Orange skin", "Reddish-brown skin", "Orange-brown skin", "Orange-red skin", "Dark orange skin", "Red-orange skin",
                    "TODO MALE Blue nose", "TODO MALE Red nose" },
                allHair: new[] { "Dark brown hair", "Dark gray hair", "Orange hair", "Red hair", "Dark reddish-brown hair", "Reddish-brown hair" },
                allEyes: new[] { "Yellow eyes", "Dark brown eyes" },
                allOther: new[] { "Yellow teeth" });
            //Source: https://forgottenrealms.fandom.com/wiki/Homunculus
            appearances[CreatureConstants.Homunculus] = GetWeightedAppearances(
                allSkin: new[] { "Beige skin", "Dark gray skin", "Pale green skin", "Yellow-green skin" },
                allEyes: new[] { "Yellow eyes", "Green eyes", "Blue eyes", "Blue-green eyes", "Red eyes", "Black eyes" },
                allOther: new[] { "Leathery wings, large bat-like ears" });
            //Source: https://forgottenrealms.fandom.com/wiki/Cornugon
            appearances[CreatureConstants.HornedDevil_Cornugon] = GetWeightedAppearances(
                allSkin: new[] { "Repulsive red scales", "Repulsive yellow scales", "Repulsive orange scales", "Repulsive black scales", "Repulsive blue scales" },
                allEyes: new[] { "Yellow eyes", "Green eyes", "Blue eyes", "Blue-green eyes", "Red eyes", "Black eyes" },
                allOther: new[] { "Sweeping horns, wings, prehensile, serpentine tail" });
            //Source: https://www.google.com/search?q=draft+horse+breeds
            //https://equineworld.co.uk/about-horses/horse-colours-and-markings/horse-coat-colours-and-patterns
            appearances[CreatureConstants.Horse_Heavy] = new[] { "Belgian Draught", "Shire horse", "Clydesdale horse", "Percheron", "Suffolk Punch",
                "American Cream Draft", "Ardennais", "Haflinger", "Irish Draught", "Dutch Draft", "Friesian horse", "Fjord horse", "Russian Heavy Draft",
                "Boulonnais horse", "Galineers Cob", "Australian Draught", "North Swedish Horse", "Noriker", "American Belgian Draft", "Breton horse", "Jutland",
                "Comtois horse", "Vladimir Heavy Draft", "Rhenish German Coldblood", "Auxois", "Cleveland Bay", "Lithuanian heavy draft",
                "North American Spotted Draft Horse", "Bashkir horse", "Soviet Heavy Draft", "Italian Heavy Draft", "South German Coldblood", "Hackney horse",
                "Polish Draft", "Dole Gudbrandsdal", "Spotted Draft", "Groninger", "Orlov Trotter", "Gelderlander", "Kabardian", "Karabair", "Kazakh horse",
                "Camargue horse", "Losino horse", "American Quarter Horse", "Zemaitukas", "Vyatka horse", "Estonian Native", "Kisber Felver", "Shagya Arabian",
                "Byelorussian Harness Horse",
                "Spotted (White coat with black spots; Black/white mane and tail)", "Spotted (White coat with brown spots; Brown/white mane and tail)",
                    "Spotted (White coat with gray spots; Gray/white mane and tail)", "Spotted (White coat with ginger spots; Ginger/white mane and tail)",
                    "Spotted (White coat with golden spots; Golden/white mane and tail)", "Spotted (White coat with cream spots; Cream/white mane and tail)",
                    "Spotted (White coat with light gray spots; Light gray/white mane and tail)",
                    "Spotted (White coat with dark gray spots; Dark gray/white mane and tail)",
                    "Spotted (White coat with creamy golden spots; Creamy golden/white mane and tail)",
                    "Spotted (White coat with light ginger spots; Light ginger/white mane and tail)",
                    "Spotted (White coat with dark ginger spots; Dark ginger/white mane and tail)",
                    "Spotted (Black coat with white spots; White/black mane and tail)",
                    "Spotted (Black coat with brown spots; Brown/black mane and tail)",
                    "Spotted (Black coat with gray spots; Gray/black mane and tail)",
                    "Spotted (Black coat with ginger spots; Ginger/black mane and tail)",
                    "Spotted (Black coat with golden spots; Golden/black mane and tail)",
                    "Spotted (Black coat with cream spots; Cream/black mane and tail)",
                    "Spotted (Black coat with light gray spots; Light gray/black mane and tail)",
                    "Spotted (Black coat with dark gray spots; Dark gray/black mane and tail)",
                    "Spotted (Black coat with creamy golden spots; Creamy golden/black mane and tail)",
                    "Spotted (Black coat with light ginger spots; Light ginger/black mane and tail)",
                    "Spotted (Black coat with dark ginger spots; Dark ginger/black mane and tail)",
                    "Spotted (Brown coat with white spots; White/brown mane and tail)",
                    "Spotted (Brown coat with black spots; Black/brown mane and tail)",
                    "Spotted (Brown coat with gray spots; Gray/brown mane and tail)",
                    "Spotted (Brown coat with ginger spots; Ginger/brown mane and tail)",
                    "Spotted (Brown coat with golden spots; Golden/brown mane and tail)",
                    "Spotted (Brown coat with cream spots; Cream/brown mane and tail)",
                    "Spotted (Brown coat with light gray spots; Light gray/brown mane and tail)",
                    "Spotted (Brown coat with dark gray spots; Dark gray/brown mane and tail)",
                    "Spotted (Brown coat with creamy golden spots; Creamy golden/brown mane and tail)",
                    "Spotted (Brown coat with light ginger spots; Light ginger/brown mane and tail)",
                    "Spotted (Brown coat with dark ginger spots; Dark ginger/brown mane and tail)",
                "Bay (Brown coat; Black legs; Black mane and tail)", "Solid black coat; Black mane and tail",
                "Brown coat; Black lower legs; Light brown mane, tail, and muzzle",
                "Buckskin (Creamy-golden coat with black points; Black mane and tail)", "Buckskin (Rich golden coat with black points; Black mane and tail)",
                "Chestnut (Ginger coat; Ginger mane and tail)", "Chestnut (Ginger coat; Light ginger mane and tail)",
                    "Chestnut (Ginger coat; Dark ginger mane and tail)",
                "Chestnut (Light ginger coat; Ginger mane and tail)", "Chestnut (Light ginger coat; Light ginger mane and tail)",
                    "Chestnut (Light ginger coat; Dark ginger mane and tail)",
                "Chestnut (Dark ginger coat; Ginger mane and tail)", "Chestnut (Dark ginger coat; Light ginger mane and tail)",
                    "Chestnut (Dark ginger coat; Dark ginger mane and tail)",
                "Cremello (Extremely pale white coat, mane, and tail; Blue eyes)", "Cremello (Extremely pale white coat, mane, and tail; Amber eyes)",
                    "Cremello (Extremely pale cream coat, mane, and tail; Blue eyes)", "Cremello (Extremely pale cream coat, mane, and tail; Amber eyes)",
                "Dun (Creamy-golden coat; Black mane and tail; Dark dorsal strip)",
                "Solid white-gray coat, mane, and tail", "Solid light gray coat, mane, and tail", "Solid gray coat, mane, and tail",
                    "Solid dark gray coat, mane, and tail",
                "Overo (Black/white cow-patterned coat; Black mane and tail)", "Overo (Black/brown cow-patterned coat; Black mane and tail)",
                    "Overo (Black/gray cow-patterned coat; Black mane and tail)", "Overo (Black/light gray cow-patterned coat; Black mane and tail)",
                    "Overo (Black/dark gray cow-patterned coat; Black mane and tail)", "Overo (Black/creamy golden cow-patterned coat; Black mane and tail)",
                    "Overo (Black/rich golden cow-patterned coat; Black mane and tail)", "Overo (Black/ginger cow-patterned coat; Black mane and tail)",
                    "Overo (Black/light ginger cow-patterned coat; Black mane and tail)", "Overo (Black/dark ginger cow-patterned coat; Black mane and tail)",
                    "Overo (Black/golden cow-patterned coat; Black mane and tail)", "Overo (Black/dark ginger cow-patterned coat; Black mane and tail)",
                "Palomino (Golden coat; White mane and tail)",
                "Roan (White-brown coat; Brown mane, tail, and legs)", "Roan (White-black coat; Black mane, tail, and legs)",
                    "Roan (White-golden coat; Golden mane, tail, and legs)", "Roan (White-ginger coat; Ginger mane, tail, and legs)",
                    "Roan (White-light-ginger coat; Light ginger mane, tail, and legs)", "Roan (White-dark-ginger coat; Dark ginger mane, tail, and legs)",
                    "Roan (White-creamy-golden coat; Creamy golden mane, tail, and legs)", "Roan (White-rich-golden coat; Rich golden mane, tail, and legs)",
                    "Roan (White-gray coat; Creamy golden mane, tail, and legs)", "Roan (White-light-gray coat; Rich golden mane, tail, and legs)",
                    "Roan (White-dark-gray coat; Creamy golden mane, tail, and legs)",
                "Tobiano (White/black cow-patterned coat; White mane and tail)", "Tobiano (White/brown cow-patterned coat; White mane and tail)",
                    "Tobiano (White/gray cow-patterned coat; White mane and tail)", "Tobiano (White/light gray cow-patterned coat; White mane and tail)",
                    "Tobiano (White/dark gray cow-patterned coat; White mane and tail)", "Tobiano (White/creamy golden cow-patterned coat; White mane and tail)",
                    "Tobiano (White/rich golden cow-patterned coat; White mane and tail)", "Tobiano (White/ginger cow-patterned coat; White mane and tail)",
                    "Tobiano (White/light ginger cow-patterned coat; White mane and tail)", "Tobiano (White/dark ginger cow-patterned coat; White mane and tail)",
                    "Tobiano (White/golden cow-patterned coat; White mane and tail)", "Tobiano (White/dark ginger cow-patterned coat; White mane and tail)" };
            //Source: https://www.google.com/search?q=horse+breeds
            //https://equineworld.co.uk/about-horses/horse-colours-and-markings/horse-coat-colours-and-patterns
            appearances[CreatureConstants.Horse_Light] = new[] { "Arabian", "Friesian", "Mustang", "Thoroughbread", "Appaloosa", "American Quarter Horse",
                "Dutch Warmblood", "American Paint Horse", "Akhal-Teke", "Turkoman horse", "Mangalarga Marchador", "Percheron", "Criollo",
                "Rahvan", "Kandachime", "Morgan horse", "Icelandic horse", "Cob", "Hanoverian", "Andalusian", "Lipizzan", "Lusitano", "Standardbred", "Falabella",
                "Pure Spanish Breed", "Mongolian", "Trakehner", "Knabstupper", "Konik", "Ferghana", "Marwari", "American Saddlebred", "Missouri Fox Trotter",
                "Belgian Warmblood", "Peruvian paso", "Brumby", "Holsteiner", "Welsh Cob", "Black Forest Horse", "Irish Sport Horse",
                "Spotted (White coat with black spots; Black/white mane and tail)", "Spotted (White coat with brown spots; Brown/white mane and tail)",
                    "Spotted (White coat with gray spots; Gray/white mane and tail)", "Spotted (White coat with ginger spots; Ginger/white mane and tail)",
                    "Spotted (White coat with golden spots; Golden/white mane and tail)", "Spotted (White coat with cream spots; Cream/white mane and tail)",
                    "Spotted (White coat with light gray spots; Light gray/white mane and tail)",
                    "Spotted (White coat with dark gray spots; Dark gray/white mane and tail)",
                    "Spotted (White coat with creamy golden spots; Creamy golden/white mane and tail)",
                    "Spotted (White coat with light ginger spots; Light ginger/white mane and tail)",
                    "Spotted (White coat with dark ginger spots; Dark ginger/white mane and tail)",
                    "Spotted (Black coat with white spots; White/black mane and tail)",
                    "Spotted (Black coat with brown spots; Brown/black mane and tail)",
                    "Spotted (Black coat with gray spots; Gray/black mane and tail)",
                    "Spotted (Black coat with ginger spots; Ginger/black mane and tail)",
                    "Spotted (Black coat with golden spots; Golden/black mane and tail)",
                    "Spotted (Black coat with cream spots; Cream/black mane and tail)",
                    "Spotted (Black coat with light gray spots; Light gray/black mane and tail)",
                    "Spotted (Black coat with dark gray spots; Dark gray/black mane and tail)",
                    "Spotted (Black coat with creamy golden spots; Creamy golden/black mane and tail)",
                    "Spotted (Black coat with light ginger spots; Light ginger/black mane and tail)",
                    "Spotted (Black coat with dark ginger spots; Dark ginger/black mane and tail)",
                    "Spotted (Brown coat with white spots; White/brown mane and tail)",
                    "Spotted (Brown coat with black spots; Black/brown mane and tail)",
                    "Spotted (Brown coat with gray spots; Gray/brown mane and tail)",
                    "Spotted (Brown coat with ginger spots; Ginger/brown mane and tail)",
                    "Spotted (Brown coat with golden spots; Golden/brown mane and tail)",
                    "Spotted (Brown coat with cream spots; Cream/brown mane and tail)",
                    "Spotted (Brown coat with light gray spots; Light gray/brown mane and tail)",
                    "Spotted (Brown coat with dark gray spots; Dark gray/brown mane and tail)",
                    "Spotted (Brown coat with creamy golden spots; Creamy golden/brown mane and tail)",
                    "Spotted (Brown coat with light ginger spots; Light ginger/brown mane and tail)",
                    "Spotted (Brown coat with dark ginger spots; Dark ginger/brown mane and tail)",
                "Bay (Brown coat; Black legs; Black mane and tail)", "Solid black coat; Black mane and tail",
                "Brown coat; Black lower legs; Light brown mane, tail, and muzzle",
                "Buckskin (Creamy-golden coat with black points; Black mane and tail)", "Buckskin (Rich golden coat with black points; Black mane and tail)",
                "Chestnut (Ginger coat; Ginger mane and tail)", "Chestnut (Ginger coat; Light ginger mane and tail)",
                    "Chestnut (Ginger coat; Dark ginger mane and tail)",
                "Chestnut (Light ginger coat; Ginger mane and tail)", "Chestnut (Light ginger coat; Light ginger mane and tail)",
                    "Chestnut (Light ginger coat; Dark ginger mane and tail)",
                "Chestnut (Dark ginger coat; Ginger mane and tail)", "Chestnut (Dark ginger coat; Light ginger mane and tail)",
                    "Chestnut (Dark ginger coat; Dark ginger mane and tail)",
                "Cremello (Extremely pale white coat, mane, and tail; Blue eyes)", "Cremello (Extremely pale white coat, mane, and tail; Amber eyes)",
                    "Cremello (Extremely pale cream coat, mane, and tail; Blue eyes)", "Cremello (Extremely pale cream coat, mane, and tail; Amber eyes)",
                "Dun (Creamy-golden coat; Black mane and tail; Dark dorsal strip)",
                "Solid white-gray coat, mane, and tail", "Solid light gray coat, mane, and tail", "Solid gray coat, mane, and tail",
                    "Solid dark gray coat, mane, and tail",
                "Overo (Black/white cow-patterned coat; Black mane and tail)", "Overo (Black/brown cow-patterned coat; Black mane and tail)",
                    "Overo (Black/gray cow-patterned coat; Black mane and tail)", "Overo (Black/light gray cow-patterned coat; Black mane and tail)",
                    "Overo (Black/dark gray cow-patterned coat; Black mane and tail)", "Overo (Black/creamy golden cow-patterned coat; Black mane and tail)",
                    "Overo (Black/rich golden cow-patterned coat; Black mane and tail)", "Overo (Black/ginger cow-patterned coat; Black mane and tail)",
                    "Overo (Black/light ginger cow-patterned coat; Black mane and tail)", "Overo (Black/dark ginger cow-patterned coat; Black mane and tail)",
                    "Overo (Black/golden cow-patterned coat; Black mane and tail)", "Overo (Black/dark ginger cow-patterned coat; Black mane and tail)",
                "Palomino (Golden coat; White mane and tail)",
                "Roan (White-brown coat; Brown mane, tail, and legs)", "Roan (White-black coat; Black mane, tail, and legs)",
                    "Roan (White-golden coat; Golden mane, tail, and legs)", "Roan (White-ginger coat; Ginger mane, tail, and legs)",
                    "Roan (White-light-ginger coat; Light ginger mane, tail, and legs)", "Roan (White-dark-ginger coat; Dark ginger mane, tail, and legs)",
                    "Roan (White-creamy-golden coat; Creamy golden mane, tail, and legs)", "Roan (White-rich-golden coat; Rich golden mane, tail, and legs)",
                    "Roan (White-gray coat; Creamy golden mane, tail, and legs)", "Roan (White-light-gray coat; Rich golden mane, tail, and legs)",
                    "Roan (White-dark-gray coat; Creamy golden mane, tail, and legs)",
                "Tobiano (White/black cow-patterned coat; White mane and tail)", "Tobiano (White/brown cow-patterned coat; White mane and tail)",
                    "Tobiano (White/gray cow-patterned coat; White mane and tail)", "Tobiano (White/light gray cow-patterned coat; White mane and tail)",
                    "Tobiano (White/dark gray cow-patterned coat; White mane and tail)", "Tobiano (White/creamy golden cow-patterned coat; White mane and tail)",
                    "Tobiano (White/rich golden cow-patterned coat; White mane and tail)", "Tobiano (White/ginger cow-patterned coat; White mane and tail)",
                    "Tobiano (White/light ginger cow-patterned coat; White mane and tail)", "Tobiano (White/dark ginger cow-patterned coat; White mane and tail)",
                    "Tobiano (White/golden cow-patterned coat; White mane and tail)", "Tobiano (White/dark ginger cow-patterned coat; White mane and tail)" };
            //Source: https://www.google.com/search?q=draft+horse+breeds
            //https://equineworld.co.uk/about-horses/horse-colours-and-markings/horse-coat-colours-and-patterns
            appearances[CreatureConstants.Horse_Heavy_War] = new[] { "Belgian Draught", "Shire horse", "Clydesdale horse", "Percheron", "Suffolk Punch",
                "American Cream Draft", "Ardennais", "Haflinger", "Irish Draught", "Dutch Draft", "Friesian horse", "Fjord horse", "Russian Heavy Draft",
                "Boulonnais horse", "Galineers Cob", "Australian Draught", "North Swedish Horse", "Noriker", "American Belgian Draft", "Breton horse", "Jutland",
                "Comtois horse", "Vladimir Heavy Draft", "Rhenish German Coldblood", "Auxois", "Cleveland Bay", "Lithuanian heavy draft",
                "North American Spotted Draft Horse", "Bashkir horse", "Soviet Heavy Draft", "Italian Heavy Draft", "South German Coldblood", "Hackney horse",
                "Polish Draft", "Dole Gudbrandsdal", "Spotted Draft", "Groninger", "Orlov Trotter", "Gelderlander", "Kabardian", "Karabair", "Kazakh horse",
                "Camargue horse", "Losino horse", "American Quarter Horse", "Zemaitukas", "Vyatka horse", "Estonian Native", "Kisber Felver", "Shagya Arabian",
                "Byelorussian Harness Horse",
                "Spotted (White coat with black spots; Black/white mane and tail)", "Spotted (White coat with brown spots; Brown/white mane and tail)",
                    "Spotted (White coat with gray spots; Gray/white mane and tail)", "Spotted (White coat with ginger spots; Ginger/white mane and tail)",
                    "Spotted (White coat with golden spots; Golden/white mane and tail)", "Spotted (White coat with cream spots; Cream/white mane and tail)",
                    "Spotted (White coat with light gray spots; Light gray/white mane and tail)",
                    "Spotted (White coat with dark gray spots; Dark gray/white mane and tail)",
                    "Spotted (White coat with creamy golden spots; Creamy golden/white mane and tail)",
                    "Spotted (White coat with light ginger spots; Light ginger/white mane and tail)",
                    "Spotted (White coat with dark ginger spots; Dark ginger/white mane and tail)",
                    "Spotted (Black coat with white spots; White/black mane and tail)",
                    "Spotted (Black coat with brown spots; Brown/black mane and tail)",
                    "Spotted (Black coat with gray spots; Gray/black mane and tail)",
                    "Spotted (Black coat with ginger spots; Ginger/black mane and tail)",
                    "Spotted (Black coat with golden spots; Golden/black mane and tail)",
                    "Spotted (Black coat with cream spots; Cream/black mane and tail)",
                    "Spotted (Black coat with light gray spots; Light gray/black mane and tail)",
                    "Spotted (Black coat with dark gray spots; Dark gray/black mane and tail)",
                    "Spotted (Black coat with creamy golden spots; Creamy golden/black mane and tail)",
                    "Spotted (Black coat with light ginger spots; Light ginger/black mane and tail)",
                    "Spotted (Black coat with dark ginger spots; Dark ginger/black mane and tail)",
                    "Spotted (Brown coat with white spots; White/brown mane and tail)",
                    "Spotted (Brown coat with black spots; Black/brown mane and tail)",
                    "Spotted (Brown coat with gray spots; Gray/brown mane and tail)",
                    "Spotted (Brown coat with ginger spots; Ginger/brown mane and tail)",
                    "Spotted (Brown coat with golden spots; Golden/brown mane and tail)",
                    "Spotted (Brown coat with cream spots; Cream/brown mane and tail)",
                    "Spotted (Brown coat with light gray spots; Light gray/brown mane and tail)",
                    "Spotted (Brown coat with dark gray spots; Dark gray/brown mane and tail)",
                    "Spotted (Brown coat with creamy golden spots; Creamy golden/brown mane and tail)",
                    "Spotted (Brown coat with light ginger spots; Light ginger/brown mane and tail)",
                    "Spotted (Brown coat with dark ginger spots; Dark ginger/brown mane and tail)",
                "Bay (Brown coat; Black legs; Black mane and tail)", "Solid black coat; Black mane and tail",
                "Brown coat; Black lower legs; Light brown mane, tail, and muzzle",
                "Buckskin (Creamy-golden coat with black points; Black mane and tail)", "Buckskin (Rich golden coat with black points; Black mane and tail)",
                "Chestnut (Ginger coat; Ginger mane and tail)", "Chestnut (Ginger coat; Light ginger mane and tail)",
                    "Chestnut (Ginger coat; Dark ginger mane and tail)",
                "Chestnut (Light ginger coat; Ginger mane and tail)", "Chestnut (Light ginger coat; Light ginger mane and tail)",
                    "Chestnut (Light ginger coat; Dark ginger mane and tail)",
                "Chestnut (Dark ginger coat; Ginger mane and tail)", "Chestnut (Dark ginger coat; Light ginger mane and tail)",
                    "Chestnut (Dark ginger coat; Dark ginger mane and tail)",
                "Cremello (Extremely pale white coat, mane, and tail; Blue eyes)", "Cremello (Extremely pale white coat, mane, and tail; Amber eyes)",
                    "Cremello (Extremely pale cream coat, mane, and tail; Blue eyes)", "Cremello (Extremely pale cream coat, mane, and tail; Amber eyes)",
                "Dun (Creamy-golden coat; Black mane and tail; Dark dorsal strip)",
                "Solid white-gray coat, mane, and tail", "Solid light gray coat, mane, and tail", "Solid gray coat, mane, and tail",
                    "Solid dark gray coat, mane, and tail",
                "Overo (Black/white cow-patterned coat; Black mane and tail)", "Overo (Black/brown cow-patterned coat; Black mane and tail)",
                    "Overo (Black/gray cow-patterned coat; Black mane and tail)", "Overo (Black/light gray cow-patterned coat; Black mane and tail)",
                    "Overo (Black/dark gray cow-patterned coat; Black mane and tail)", "Overo (Black/creamy golden cow-patterned coat; Black mane and tail)",
                    "Overo (Black/rich golden cow-patterned coat; Black mane and tail)", "Overo (Black/ginger cow-patterned coat; Black mane and tail)",
                    "Overo (Black/light ginger cow-patterned coat; Black mane and tail)", "Overo (Black/dark ginger cow-patterned coat; Black mane and tail)",
                    "Overo (Black/golden cow-patterned coat; Black mane and tail)", "Overo (Black/dark ginger cow-patterned coat; Black mane and tail)",
                "Palomino (Golden coat; White mane and tail)",
                "Roan (White-brown coat; Brown mane, tail, and legs)", "Roan (White-black coat; Black mane, tail, and legs)",
                    "Roan (White-golden coat; Golden mane, tail, and legs)", "Roan (White-ginger coat; Ginger mane, tail, and legs)",
                    "Roan (White-light-ginger coat; Light ginger mane, tail, and legs)", "Roan (White-dark-ginger coat; Dark ginger mane, tail, and legs)",
                    "Roan (White-creamy-golden coat; Creamy golden mane, tail, and legs)", "Roan (White-rich-golden coat; Rich golden mane, tail, and legs)",
                    "Roan (White-gray coat; Creamy golden mane, tail, and legs)", "Roan (White-light-gray coat; Rich golden mane, tail, and legs)",
                    "Roan (White-dark-gray coat; Creamy golden mane, tail, and legs)",
                "Tobiano (White/black cow-patterned coat; White mane and tail)", "Tobiano (White/brown cow-patterned coat; White mane and tail)",
                    "Tobiano (White/gray cow-patterned coat; White mane and tail)", "Tobiano (White/light gray cow-patterned coat; White mane and tail)",
                    "Tobiano (White/dark gray cow-patterned coat; White mane and tail)", "Tobiano (White/creamy golden cow-patterned coat; White mane and tail)",
                    "Tobiano (White/rich golden cow-patterned coat; White mane and tail)", "Tobiano (White/ginger cow-patterned coat; White mane and tail)",
                    "Tobiano (White/light ginger cow-patterned coat; White mane and tail)", "Tobiano (White/dark ginger cow-patterned coat; White mane and tail)",
                    "Tobiano (White/golden cow-patterned coat; White mane and tail)", "Tobiano (White/dark ginger cow-patterned coat; White mane and tail)" };
            //Source: https://www.google.com/search?q=horse+breeds
            //https://equineworld.co.uk/about-horses/horse-colours-and-markings/horse-coat-colours-and-patterns
            appearances[CreatureConstants.Horse_Light_War] = new[] { "Arabian", "Friesian", "Mustang", "Thoroughbread", "Appaloosa", "American Quarter Horse",
                "Dutch Warmblood", "American Paint Horse", "Akhal-Teke", "Turkoman horse", "Mangalarga Marchador", "Percheron", "Criollo",
                "Rahvan", "Kandachime", "Morgan horse", "Icelandic horse", "Cob", "Hanoverian", "Andalusian", "Lipizzan", "Lusitano", "Standardbred", "Falabella",
                "Pure Spanish Breed", "Mongolian", "Trakehner", "Knabstupper", "Konik", "Ferghana", "Marwari", "American Saddlebred", "Missouri Fox Trotter",
                "Belgian Warmblood", "Peruvian paso", "Brumby", "Holsteiner", "Welsh Cob", "Black Forest Horse", "Irish Sport Horse",
                "Spotted (White coat with black spots; Black/white mane and tail)", "Spotted (White coat with brown spots; Brown/white mane and tail)",
                    "Spotted (White coat with gray spots; Gray/white mane and tail)", "Spotted (White coat with ginger spots; Ginger/white mane and tail)",
                    "Spotted (White coat with golden spots; Golden/white mane and tail)", "Spotted (White coat with cream spots; Cream/white mane and tail)",
                    "Spotted (White coat with light gray spots; Light gray/white mane and tail)",
                    "Spotted (White coat with dark gray spots; Dark gray/white mane and tail)",
                    "Spotted (White coat with creamy golden spots; Creamy golden/white mane and tail)",
                    "Spotted (White coat with light ginger spots; Light ginger/white mane and tail)",
                    "Spotted (White coat with dark ginger spots; Dark ginger/white mane and tail)",
                    "Spotted (Black coat with white spots; White/black mane and tail)",
                    "Spotted (Black coat with brown spots; Brown/black mane and tail)",
                    "Spotted (Black coat with gray spots; Gray/black mane and tail)",
                    "Spotted (Black coat with ginger spots; Ginger/black mane and tail)",
                    "Spotted (Black coat with golden spots; Golden/black mane and tail)",
                    "Spotted (Black coat with cream spots; Cream/black mane and tail)",
                    "Spotted (Black coat with light gray spots; Light gray/black mane and tail)",
                    "Spotted (Black coat with dark gray spots; Dark gray/black mane and tail)",
                    "Spotted (Black coat with creamy golden spots; Creamy golden/black mane and tail)",
                    "Spotted (Black coat with light ginger spots; Light ginger/black mane and tail)",
                    "Spotted (Black coat with dark ginger spots; Dark ginger/black mane and tail)",
                    "Spotted (Brown coat with white spots; White/brown mane and tail)",
                    "Spotted (Brown coat with black spots; Black/brown mane and tail)",
                    "Spotted (Brown coat with gray spots; Gray/brown mane and tail)",
                    "Spotted (Brown coat with ginger spots; Ginger/brown mane and tail)",
                    "Spotted (Brown coat with golden spots; Golden/brown mane and tail)",
                    "Spotted (Brown coat with cream spots; Cream/brown mane and tail)",
                    "Spotted (Brown coat with light gray spots; Light gray/brown mane and tail)",
                    "Spotted (Brown coat with dark gray spots; Dark gray/brown mane and tail)",
                    "Spotted (Brown coat with creamy golden spots; Creamy golden/brown mane and tail)",
                    "Spotted (Brown coat with light ginger spots; Light ginger/brown mane and tail)",
                    "Spotted (Brown coat with dark ginger spots; Dark ginger/brown mane and tail)",
                "Bay (Brown coat; Black legs; Black mane and tail)", "Solid black coat; Black mane and tail",
                "Brown coat; Black lower legs; Light brown mane, tail, and muzzle",
                "Buckskin (Creamy-golden coat with black points; Black mane and tail)", "Buckskin (Rich golden coat with black points; Black mane and tail)",
                "Chestnut (Ginger coat; Ginger mane and tail)", "Chestnut (Ginger coat; Light ginger mane and tail)",
                    "Chestnut (Ginger coat; Dark ginger mane and tail)",
                "Chestnut (Light ginger coat; Ginger mane and tail)", "Chestnut (Light ginger coat; Light ginger mane and tail)",
                    "Chestnut (Light ginger coat; Dark ginger mane and tail)",
                "Chestnut (Dark ginger coat; Ginger mane and tail)", "Chestnut (Dark ginger coat; Light ginger mane and tail)",
                    "Chestnut (Dark ginger coat; Dark ginger mane and tail)",
                "Cremello (Extremely pale white coat, mane, and tail; Blue eyes)", "Cremello (Extremely pale white coat, mane, and tail; Amber eyes)",
                    "Cremello (Extremely pale cream coat, mane, and tail; Blue eyes)", "Cremello (Extremely pale cream coat, mane, and tail; Amber eyes)",
                "Dun (Creamy-golden coat; Black mane and tail; Dark dorsal strip)",
                "Solid white-gray coat, mane, and tail", "Solid light gray coat, mane, and tail", "Solid gray coat, mane, and tail",
                    "Solid dark gray coat, mane, and tail",
                "Overo (Black/white cow-patterned coat; Black mane and tail)", "Overo (Black/brown cow-patterned coat; Black mane and tail)",
                    "Overo (Black/gray cow-patterned coat; Black mane and tail)", "Overo (Black/light gray cow-patterned coat; Black mane and tail)",
                    "Overo (Black/dark gray cow-patterned coat; Black mane and tail)", "Overo (Black/creamy golden cow-patterned coat; Black mane and tail)",
                    "Overo (Black/rich golden cow-patterned coat; Black mane and tail)", "Overo (Black/ginger cow-patterned coat; Black mane and tail)",
                    "Overo (Black/light ginger cow-patterned coat; Black mane and tail)", "Overo (Black/dark ginger cow-patterned coat; Black mane and tail)",
                    "Overo (Black/golden cow-patterned coat; Black mane and tail)", "Overo (Black/dark ginger cow-patterned coat; Black mane and tail)",
                "Palomino (Golden coat; White mane and tail)",
                "Roan (White-brown coat; Brown mane, tail, and legs)", "Roan (White-black coat; Black mane, tail, and legs)",
                    "Roan (White-golden coat; Golden mane, tail, and legs)", "Roan (White-ginger coat; Ginger mane, tail, and legs)",
                    "Roan (White-light-ginger coat; Light ginger mane, tail, and legs)", "Roan (White-dark-ginger coat; Dark ginger mane, tail, and legs)",
                    "Roan (White-creamy-golden coat; Creamy golden mane, tail, and legs)", "Roan (White-rich-golden coat; Rich golden mane, tail, and legs)",
                    "Roan (White-gray coat; Creamy golden mane, tail, and legs)", "Roan (White-light-gray coat; Rich golden mane, tail, and legs)",
                    "Roan (White-dark-gray coat; Creamy golden mane, tail, and legs)",
                "Tobiano (White/black cow-patterned coat; White mane and tail)", "Tobiano (White/brown cow-patterned coat; White mane and tail)",
                    "Tobiano (White/gray cow-patterned coat; White mane and tail)", "Tobiano (White/light gray cow-patterned coat; White mane and tail)",
                    "Tobiano (White/dark gray cow-patterned coat; White mane and tail)", "Tobiano (White/creamy golden cow-patterned coat; White mane and tail)",
                    "Tobiano (White/rich golden cow-patterned coat; White mane and tail)", "Tobiano (White/ginger cow-patterned coat; White mane and tail)",
                    "Tobiano (White/light ginger cow-patterned coat; White mane and tail)", "Tobiano (White/dark ginger cow-patterned coat; White mane and tail)",
                    "Tobiano (White/golden cow-patterned coat; White mane and tail)", "Tobiano (White/dark ginger cow-patterned coat; White mane and tail)" };
            //Source: https://forgottenrealms.fandom.com/wiki/Hound_archon
            appearances[CreatureConstants.HoundArchon] = GetWeightedAppearances(
                allSkin: new[] { "Red skin", "Brown skin", "Light brown skin", "Tan skin", "Cream-colored skin" },
                allEyes: new[] { "Black eyes", "Yellow eyes" },
                allOther: new[] { "Humanoid body; head of a dog" });
            //Source: https://forgottenrealms.fandom.com/wiki/Howler
            appearances[CreatureConstants.Howler] = GetWeightedAppearances(
                allSkin: new[] { "Light brown scales", "Tan scales" },
                allHair: new[] { "Tangled, red fur, spreading into mane of trembling quills on the back of the neck and surrounding the face" },
                allEyes: new[] { "Black eyes", "Yellow eyes" },
                allOther: new[] { "Bruised, crushed digits ending in claws on the front legs. Back legs end in hooves. Back like an ox. Muzzled, simian face." });
            //Source: https://forgottenrealms.fandom.com/wiki/Human
            appearances[CreatureConstants.Human] = GetWeightedAppearances(
                allSkin: new[] { "Dark Brown skin", "Brown skin", "Light brown skin",
                    "Dark tan skin", "Tan skin", "Light tan skin",
                    "Pink skin", "White skin", "Pale white skin" },
                commonHair: new[] { "Straight Red hair", "Straight Blond hair", "Straight Brown hair", "Straight Black hair",
                    "Curly Red hair", "Curly Blond hair", "Curly Brown hair", "Curly Black hair",
                    "Kinky Red hair", "Kinky Blond hair", "Kinky Brown hair", "Kinky Black hair" },
                uncommonHair: new[] { "Bald" },
                allEyes: new[] { "Blue eyes", "Brown eyes", "Gray eyes", "Green eyes", "Hazel eyes" });
            //Source: https://forgottenrealms.fandom.com/wiki/Hydra
            appearances[CreatureConstants.Hydra_5Heads] = GetWeightedAppearances(
                allSkin: new[] { "Gray-brown skin", "Brown skin", "Dark brown skin", "Dark gray-brown skin",
                    "Yellow-green skin", "Green skin, yellow underbelly", "Tan skin" },
                allEyes: new[] { "Amber eyes" });
            appearances[CreatureConstants.Hydra_6Heads] = GetWeightedAppearances(
                allSkin: new[] { "Gray-brown skin", "Brown skin", "Dark brown skin", "Dark gray-brown skin",
                    "Yellow-green skin", "Green skin, yellow underbelly", "Tan skin" },
                allEyes: new[] { "Amber eyes" });
            appearances[CreatureConstants.Hydra_7Heads] = GetWeightedAppearances(
                allSkin: new[] { "Gray-brown skin", "Brown skin", "Dark brown skin", "Dark gray-brown skin",
                    "Yellow-green skin", "Green skin, yellow underbelly", "Tan skin" },
                allEyes: new[] { "Amber eyes" });
            appearances[CreatureConstants.Hydra_8Heads] = GetWeightedAppearances(
                allSkin: new[] { "Gray-brown skin", "Brown skin", "Dark brown skin", "Dark gray-brown skin",
                    "Yellow-green skin", "Green skin, yellow underbelly", "Tan skin" },
                allEyes: new[] { "Amber eyes" });
            appearances[CreatureConstants.Hydra_9Heads] = GetWeightedAppearances(
                allSkin: new[] { "Gray-brown skin", "Brown skin", "Dark brown skin", "Dark gray-brown skin",
                    "Yellow-green skin", "Green skin, yellow underbelly", "Tan skin" },
                allEyes: new[] { "Amber eyes" });
            appearances[CreatureConstants.Hydra_10Heads] = GetWeightedAppearances(
                allSkin: new[] { "Gray-brown skin", "Brown skin", "Dark brown skin", "Dark gray-brown skin",
                    "Yellow-green skin", "Green skin, yellow underbelly", "Tan skin" },
                allEyes: new[] { "Amber eyes" });
            appearances[CreatureConstants.Hydra_11Heads] = GetWeightedAppearances(
                allSkin: new[] { "Gray-brown skin", "Brown skin", "Dark brown skin", "Dark gray-brown skin",
                    "Yellow-green skin", "Green skin, yellow underbelly", "Tan skin" },
                allEyes: new[] { "Amber eyes" });
            appearances[CreatureConstants.Hydra_12Heads] = GetWeightedAppearances(
                allSkin: new[] { "Gray-brown skin", "Brown skin", "Dark brown skin", "Dark gray-brown skin",
                    "Yellow-green skin", "Green skin, yellow underbelly", "Tan skin" },
                allEyes: new[] { "Amber eyes" });
            //Source: https://www.dimensions.com/element/striped-hyena-hyaena-hyaena
            //https://en.wikipedia.org/wiki/Striped_hyena#Description
            appearances[CreatureConstants.Hyena] = GetWeightedAppearances(
                allHair: new[] { "Coarse, bristly, light brown fur with darker brown striping. Mane hair is light gray at the base and black at the tips. Muzzle is dark, grayish brown",
                    "Coarse, bristly, light brown fur with darker brown striping. Mane hair is light gray at the base and dark brown at the tips. Muzzle is dark, grayish brown",
                    "Coarse, bristly, light brown fur with darker brown striping. Mane hair is white at the base and black at the tips. Muzzle is dark, grayish brown",
                    "Coarse, bristly, light brown fur with darker brown striping. Mane hair is white at the base and dark brown at the tips. Muzzle is dark, grayish brown",
                    "Coarse, bristly, light brown fur with darker brown striping. Mane hair is light gray at the base and black at the tips. Muzzle is brownish-gray",
                    "Coarse, bristly, light brown fur with darker brown striping. Mane hair is light gray at the base and dark brown at the tips. Muzzle is brownish-gray",
                    "Coarse, bristly, light brown fur with darker brown striping. Mane hair is white at the base and black at the tips. Muzzle is brownish-gray",
                    "Coarse, bristly, light brown fur with darker brown striping. Mane hair is white at the base and dark brown at the tips. Muzzle is brownish-gray",
                    "Coarse, bristly, light brown fur with darker brown striping. Mane hair is light gray at the base and black at the tips. Muzzle is black",
                    "Coarse, bristly, light brown fur with darker brown striping. Mane hair is light gray at the base and dark brown at the tips. Muzzle is black",
                    "Coarse, bristly, light brown fur with darker brown striping. Mane hair is white at the base and black at the tips. Muzzle is black",
                    "Coarse, bristly, light brown fur with darker brown striping. Mane hair is white at the base and dark brown at the tips. Muzzle is black",
                    "Coarse, bristly, dirty-gray fur with darker gray striping. Mane hair is light gray at the base and black at the tips. Muzzle is dark, grayish brown",
                    "Coarse, bristly, dirty-gray fur with darker gray striping. Mane hair is light gray at the base and dark brown at the tips. Muzzle is dark, grayish brown",
                    "Coarse, bristly, dirty-gray fur with darker gray striping. Mane hair is white at the base and black at the tips. Muzzle is dark, grayish brown",
                    "Coarse, bristly, dirty-gray fur with darker gray striping. Mane hair is white at the base and dark brown at the tips. Muzzle is dark, grayish brown",
                    "Coarse, bristly, dirty-gray fur with darker gray striping. Mane hair is light gray at the base and black at the tips. Muzzle is brownish-gray",
                    "Coarse, bristly, dirty-gray fur with darker gray striping. Mane hair is light gray at the base and dark brown at the tips. Muzzle is brownish-gray",
                    "Coarse, bristly, dirty-gray fur with darker gray striping. Mane hair is white at the base and black at the tips. Muzzle is brownish-gray",
                    "Coarse, bristly, dirty-gray fur with darker gray striping. Mane hair is white at the base and dark brown at the tips. Muzzle is brownish-gray",
                    "Coarse, bristly, dirty-gray fur with darker gray striping. Mane hair is light gray at the base and black at the tips. Muzzle is black",
                    "Coarse, bristly, dirty-gray fur with darker gray striping. Mane hair is light gray at the base and dark brown at the tips. Muzzle is black",
                    "Coarse, bristly, dirty-gray fur with darker gray striping. Mane hair is white at the base and black at the tips. Muzzle is black",
                    "Coarse, bristly, dirty-gray fur with darker gray striping. Mane hair is white at the base and dark brown at the tips. Muzzle is black" },
                allEyes: new[] { "Small eyes" },
                allOther: new[] { "Short torso, long legs, thick and elongated neck, large and high-set ears" });
            //Source: https://forgottenrealms.fandom.com/wiki/Gelugon
            appearances[CreatureConstants.IceDevil_Gelugon] = GetWeightedAppearances(
                allSkin: new[] { "Blue and white skin", "Blue skin", "White skin" },
                allEyes: new[] { "Multifaceted yellow eyes", "Multifaceted green eyes" },
                allOther: new[] { "Insectoid. Hands and feet end in claws. Long thick tail surrounded in spikes. Set of antennae on the head. Sharp mandibles" });
            //Source: https://forgottenrealms.fandom.com/wiki/Imp
            appearances[CreatureConstants.Imp] = GetWeightedAppearances(
                commonSkin: new[] { "Dark red skin" },
                uncommonSkin: new[] { "Red skin", "Light red skin", "Orange skin", "Blue skin", "Brown skin", "Green skin", "Purple skin", "White skin", "Gray skin",
                    "Black skin", "Yellow skin" },
                allEyes: new[] { "Yellow eyes", "White eyes", "Green eyes" },
                allOther: new[] { "Leathery bat-like wings. Prehensile tail ending in stinger. Small, sharp, twisting, gleaming-white horns. White fangs." });
            //Source: https://www.d20srd.org/srd/monsters/invisibleStalker.htm
            appearances[CreatureConstants.InvisibleStalker] = new[] { "Amorphous form. See Invisibility reveals a dim outline of a cloud. True Seeing reveals a roiling cloud of vapor" };
            //Source: https://forgottenrealms.fandom.com/wiki/Janni
            appearances[CreatureConstants.Janni] = GetWeightedAppearances(
                allSkin: new[] { "Skin the color of golden sand", "Earth-colored skin" },
                allHair: new[] { "TODO HUMAN hair", "TODO HALF-ELF hair" },
                allEyes: new[] { "TODO HUMAN eyes with supernatural intensity", "TODO HALF-ELF eyes with supernatural intensity" });
            //Source: https://forgottenrealms.fandom.com/wiki/Kobold
            appearances[CreatureConstants.Kobold] = GetWeightedAppearances(
                allSkin: new[] { "Reddish-brown scaled skin", "Rusty black scaled skin", "Rusty brown scaled skin", "Reddish-black scaled skin",
                    "Brown-black scaled skin", "Black scaled skin" },
                allEyes: new[] { "Red eyes", "Burnt orange eyes", "Orange-red eyes", "Burnt red eyes" },
                allOther: new[] { "Long, clawed fingers. Jaw like a crocodile. Small white horns on the head. Rat-like tail",
                    "Long, clawed fingers. Jaw like a crocodile. Small tan horns on the head. Rat-like tail" });
            //Source: https://forgottenrealms.fandom.com/wiki/Kolyarut
            appearances[CreatureConstants.Kolyarut] = new[] { "Red skin. Golden-hued banded-mail armor. Flowing red robe.",
                "Black skin. Golden-hued banded-mail armor. Flowing red robe." };
            //Source: https://forgottenrealms.fandom.com/wiki/Kraken
            appearances[CreatureConstants.Kraken] = GetWeightedAppearances(
                allSkin: new[] { "Brown skin", "Light brown skin", "Reddish-brown skin", "Blue skin", "Purple skin", "Yellow-green skin",
                    "Yellow-green skin with a black head, black tips on tentacles"},
                allEyes: new[] { "Large yellow eyes", "Large red eyes", "Large blue eyes", "Large green eyes" },
                allOther: new[] { "Squid-like. Two of the ten tentacles are longer and have deadly barbs. Fins protrude from the upper part of the elongated central body" });
            //Source: https://forgottenrealms.fandom.com/wiki/Krenshar
            appearances[CreatureConstants.Krenshar] = GetWeightedAppearances(
                allHair: new[] { "Light brown shaggy fur with dark brown spots, dark-red spiky mane", "Light brown shaggy fur with dark brown spots, dark-brown spiky mane",
                    "Light gray shaggy fur with dark brown spots, brown spiky mane", "Light gray shaggy fur with dark brown spots, dark-brown spiky mane",
                    "Light gray shaggy fur with dark gray spots, brown spiky mane", "Light gray shaggy fur with dark gray spots, dark-brown spiky mane" },
                allOther: new[] { "Long, bushy tail. Skin over the skull is a sheath that can be pulled back to reveal the underlyring muscles and bones" });
            //Source: https://forgottenrealms.fandom.com/wiki/Kuo-toa
            appearances[CreatureConstants.KuoToa] = GetWeightedAppearances(
                allSkin: new[] { "Silver-gray scales", "Gray scales", "Gray scales with yellow undertones", "Gray scales with dark red undertones",
                    "Gray scales with ghostly white undertones" },
                allEyes: new[] { "Bulging silver-black eyes" },
                allOther: new[] { "Broad, distended, partially-webbed hands and feet. Four digits per hand and feet. Bullet-shapred, piscine head. Sharp teeth." });
            //Source: https://forgottenrealms.fandom.com/wiki/Lamia
            appearances[CreatureConstants.Lamia] = GetWeightedAppearances(
                allSkin: new[] { "TODO HUMAN skin" },
                commonHair: new[] { "TODO LION fur" },
                uncommonHair: new[] { "TODO GOAT fur", "TODO DEER fur" },
                allEyes: new[] { "TODO HUMAN eyes" },
                commonOther: new[] { "Human from the waist-up, lion from the waist-down" },
                uncommonOther: new[] { "Human from the waist-up, goat from the waist-down", "Human from the waist-up, deer from the waist-down" });
            //Source: https://forgottenrealms.fandom.com/wiki/Lammasu
            appearances[CreatureConstants.Lammasu] = GetWeightedAppearances(
                allSkin: new[] { "TODO HUMAN skin" },
                allHair: new[] { "TODO LION fur", "TODO EAGLE feathers" },
                allEyes: new[] { "TODO HUMAN eyes" },
                allOther: new[] { "Human from the waist-up, lion from the waist-down, pair of wings on the back" });
            //Source: https://forgottenrealms.fandom.com/wiki/Lantern_archon
            appearances[CreatureConstants.LanternArchon] = new[] { "Sphere of soft, glowing light" };
            //Source: https://forgottenrealms.fandom.com/wiki/Lemure
            appearances[CreatureConstants.Lemure] = GetWeightedAppearances(
                allSkin: new[] { "Orange skin", "Pink skin" },
                allOther: new[] { "Blob of molten, stinking flesh. Formless, shivering mass below the waist. Head and torso is vaguely humanoid." });
            //Source: https://forgottenrealms.fandom.com/wiki/Leonal
            appearances[CreatureConstants.Leonal] = GetWeightedAppearances(
                allSkin: new[] { "TODO HUMAN skin" },
                allHair: new[] { "TODO LION fur" },
                allEyes: new[] { "TODO HUMAN eyes", "TODO LION eyes" });
            //Source: https://forgottenrealms.fandom.com/wiki/Leopard
            appearances[CreatureConstants.Leopard] = new[] { "Yellow fur with rosette-shaped spots" };
            //Source: https://forgottenrealms.fandom.com/wiki/Lillend
            appearances[CreatureConstants.Lillend] = GetWeightedAppearances(
                allSkin: new[] { "TODO HUMAN skin",
                    "Bright blue scales", "Bright purple scales", "Bright red scales", "Bright orange scales", "Bright yellow scales", "Bright green scales" },
                allHair: new[] { "Bright blue hair, bright blue feathers", "Bright blue hair, bright purple feathers", "Bright blue hair, bright red feathers",
                        "Bright blue hair, bright orange feathers", "Bright blue hair, bright yellow feathers", "Bright blue hair, bright green feathers",
                    "Bright purple hair, bright blue feathers", "Bright purple hair, bright purple feathers", "Bright purple hair, bright red feathers",
                        "Bright purple hair, bright orange feathers", "Bright purple hair, bright yellow feathers", "Bright purple hair, bright green feathers",
                    "Bright red hair, bright blue feathers", "Bright red hair, bright purple feathers", "Bright red hair, bright red feathers",
                        "Bright red hair, bright orange feathers", "Bright red hair, bright yellow feathers", "Bright red hair, bright green feathers",
                    "Bright orange hair, bright blue feathers", "Bright orange hair, bright purple feathers", "Bright orange hair, bright red feathers",
                        "Bright orange hair, bright orange feathers", "Bright orange hair, bright yellow feathers", "Bright orange hair, bright green feathers",
                    "Bright yellow hair, bright blue feathers", "Bright yellow hair, bright purple feathers", "Bright yellow hair, bright red feathers",
                        "Bright yellow hair, bright orange feathers", "Bright yellow hair, bright yellow feathers", "Bright yellow hair, bright green feathers",
                    "Bright green hair, bright blue feathers", "Bright green hair, bright purple feathers", "Bright green hair, bright red feathers",
                        "Bright green hair, bright orange feathers", "Bright green hair, bright yellow feathers", "Bright green hair, bright green feathers" },
                allEyes: new[] { "TODO HUMAN eyes" },
                allOther: new[] { "Torso is humanoid, waist-down is serpentine" });
            //Source: https://forgottenrealms.fandom.com/wiki/Lion
            appearances[CreatureConstants.Lion] = GetWeightedAppearances(
                allHair: new[] { "Golden fur", "Tawny fur", "TODO MALE Brown mane" });
            //Source: https://forgottenrealms.fandom.com/wiki/Dire_lion
            appearances[CreatureConstants.Lion_Dire] = GetWeightedAppearances(
                allHair: new[] { "Golden fur", "Tawny fur", "TODO MALE Brown mane" },
                allOther: new[] { "Bony spine and brow ridges" });
            //Source: https://www.d20srd.org/srd/monsters/lizard.htm
            //https://www.petcoach.co/article/green-iguana-color-change-causes/
            appearances[CreatureConstants.Lizard] = GetWeightedAppearances(
                commonSkin: new[] { "Green skin" },
                uncommonSkin: new[] { "Turquoise skin", "Brown skin", "Reddish-green skin" },
                rareSkin: new[] { "White skin (Albino)" });
            //Source: https://azeah.com/lizards/basic-care-tree-monitors
            appearances[CreatureConstants.Lizard_Monitor] = GetWeightedAppearances(
                commonSkin: new[] { "Blue skin", "Black skin", "Green skin", "Yellow skin" });
            //Source: https://forgottenrealms.fandom.com/wiki/Lizardfolk
            appearances[CreatureConstants.Lizardfolk] = GetWeightedAppearances(
                allSkin: new[] { "Green scales", "Black scales", "Gray scales", "Brown scales", "Dark green scales", "Dark brown scales", "Dark gray scales",
                    "Green-brown scales", "Green-gray scales" },
                allEyes: new[] { "Yellow eyes" },
                allOther: new[] { "Tail, sharp claws and teeth" });
            //Source: https://forgottenrealms.fandom.com/wiki/Locathah
            appearances[CreatureConstants.Locathah] = GetWeightedAppearances(
                allSkin: new[] { "Fine yellow-green scales with sea-green scales on the stomach", "Fine ochre scales with sea-green scales on the stomach",
                        "Fine yellow scales with sea-green scales on the stomach", "Fine olive-green scales with sea-green scales on the stomach",
                    "Fine yellow-green scales with pale-yellow scales on the stomach", "Fine ochre scales with pale-yellow scales on the stomach",
                        "Fine yellow scales with pale-yellow scales on the stomach", "Fine olive-green scales with pale-yellow scales on the stomach",
                    "Fine yellow-green scales with yellow-green scales on the stomach", "Fine ochre scales with yellow-green scales on the stomach",
                        "Fine yellow scales with yellow-green scales on the stomach", "Fine olive-green scales with yellow-green scales on the stomach",
                    "Fine yellow-green scales with yellow scales on the stomach", "Fine ochre scales with yellow scales on the stomach",
                        "Fine yellow scales with yellow scales on the stomach", "Fine olive-green scales with yellow scales on the stomach",
                    "Fine yellow-green scales with green scales on the stomach", "Fine ochre scales with green scales on the stomach",
                        "Fine yellow scales with green scales on the stomach", "Fine olive-green scales with green scales on the stomach" },
                allEyes: new[] { "All-black eyes", "All-white eyes" },
                allOther: new[] { "slender, large fins on arms and legs. Toothless mouth" });
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            appearances[CreatureConstants.Locust_Swarm] = new[] { "5,000 locusts" };
            //Source: https://forgottenrealms.fandom.com/wiki/Magmin
            appearances[CreatureConstants.Magmin] = new[] { "Seemingly sculpted from hardened magma. Small bursts of flame constantly erupt from its skin" };
            //Source: https://www.scubalibre-adventures.com/giant-manta-ray-vs-reef-manta/
            appearances[CreatureConstants.MantaRay] = new[] { "Black top with white underbelly" };
            //Source: https://forgottenrealms.fandom.com/wiki/Manticore
            appearances[CreatureConstants.Manticore] = GetWeightedAppearances(
                allSkin: new[] { "TODO HUMAN skin", "TODO DRAGON wings" },
                allHair: new[] { "TODO LION fur", "TODO HUMAN hair" },
                allEyes: new[] { "Yellow eyes" },
                allOther: new[] { "Tail ends in a mass of deadly spikes. Mouth full of rows and rows of razor-sharp teeth, similar to that of a great white shark" });
            //Source: https://forgottenrealms.fandom.com/wiki/Marilith
            appearances[CreatureConstants.Marilith] = GetWeightedAppearances(
                allSkin: new[] { "TODO HUMAN skin, lower half has green scales" },
                allHair: new[] { "TODO HUMAN hair" },
                allEyes: new[] { "TODO HUMAN eyes" },
                allOther: new[] { "Top half is human, bottom half is a serpent" });
            //Source: https://forgottenrealms.fandom.com/wiki/Marut
            appearances[CreatureConstants.Marut] = new[] { "Resembles a massive statue made entirely of onyx, humanoid in form but composed of mechanical components. Clad in golden armor." };
            //Source: https://www.d20srd.org/srd/monsters/medusa.htm
            appearances[CreatureConstants.Medusa] = GetWeightedAppearances(
                allSkin: new[] { "Pale gray, scaly skin", "Earthy-brown, scaly skin", "Pale-green, scaly skin" },
                allHair: new[] { "Hair is mass of 1‑foot-long living venomous serpents" },
                allEyes: new[] { "Intense red eyes" });
            //Source: https://jurassicworld-evolution.fandom.com/wiki/Velociraptor
            appearances[CreatureConstants.Megaraptor] = GetWeightedAppearances(
                commonSkin: new[] { "Dull pale green skin", "Ashy gray skin", "Yellow skin", "Orange skin", "Beige skin", "Stone gray skin", "Dark gray skin" },
                uncommonSkin: new[] { "Earth-brown green skin", "Sandy-yellow skin", "Orange skin with dark orange stripes across the back, yellow underbelly",
                    "White skin with dark gray stripes runnings the length of the body, dark gray hands and feet, dark gray circles around the eyes",
                    "Dark gray skin with a white stripe running the length of the body, yellow underbelly", },
                rareSkin: new[] { "Ashy-gray skin with a blue stripe running the length of the back and down the tail",
                    "Forest-green skin with dark green stripes across the back",
                    "Ashy-gray skin", "Sandy-brown skin with turquoise markings" },
                allOther: new[] { "Large sickle claw on its foot. Large, sharp teeth." });
            //Source: https://forgottenrealms.fandom.com/wiki/Air_mephit
            appearances[CreatureConstants.Mephit_Air] = GetWeightedAppearances(
                allSkin: new[] { "Pale white skin" },
                allOther: new[] { "Wings, whirlwinds instead of wings" });
            //Source: https://forgottenrealms.fandom.com/wiki/Dust_mephit
            appearances[CreatureConstants.Mephit_Dust] = GetWeightedAppearances(
                allSkin: new[] { "Rough gray skin", "Flaky gray skin" },
                allOther: new[] { "Wings, emits dust and grit" });
            //Source: https://forgottenrealms.fandom.com/wiki/Earth_mephit
            appearances[CreatureConstants.Mephit_Earth] = GetWeightedAppearances(
                allSkin: new[] { "Bumpy, rough, earthy skin", "Bumpy, rough, rocky skin", "Bumpy, rough, stony skin" },
                allOther: new[] { "Wings" });
            //Source: https://forgottenrealms.fandom.com/wiki/Fire_mephit
            appearances[CreatureConstants.Mephit_Fire] = GetWeightedAppearances(
                allSkin: new[] { "Red skin", "Red skin streaked with black" },
                uncommonHair: new[] { "Flaming mustache", "Flaming goatee" },
                commonOther: new[] { "Imp-like wings covered in a halo of fire, body covered in flames" },
                uncommonOther: new[] { "Bat-like wings covered in a halo of fire, body covered in flames" });
            //Source: https://forgottenrealms.fandom.com/wiki/Ice_mephit
            appearances[CreatureConstants.Mephit_Ice] = GetWeightedAppearances(
                allSkin: new[] { "Blue skin", "Blue-white skin", "White skin" },
                allOther: new[] { "Wings, body seemingly composed of ice" });
            //Source: https://forgottenrealms.fandom.com/wiki/Magma_mephit
            appearances[CreatureConstants.Mephit_Magma] = GetWeightedAppearances(
                allSkin: new[] { "Dull red skin" },
                allOther: new[] { "Wings, body oozes molten lava (like sweat)" });
            //Source: https://forgottenrealms.fandom.com/wiki/Ooze_mephit
            appearances[CreatureConstants.Mephit_Ooze] = GetWeightedAppearances(
                allSkin: new[] { "Ochre skin", "Green skin" },
                allEyes: new[] { "Dark green eyes", "Red eyes" },
                allOther: new[] { "Wings are green bubble membranes, body is mucky and filthy, dripping in slime" });
            //Source: https://forgottenrealms.fandom.com/wiki/Salt_mephit
            appearances[CreatureConstants.Mephit_Salt] = GetWeightedAppearances(
                allSkin: new[] { "Pale beige crystalline skin", "White, grainy, crystalline skin" },
                allEyes: new[] { "Red eyes" },
                allOther: new[] { "Wings composed of small cubic crystals" });
            //Source: https://forgottenrealms.fandom.com/wiki/Steam_mephit
            appearances[CreatureConstants.Mephit_Steam] = GetWeightedAppearances(
                allSkin: new[] { "Transluscent white skin", "Billowing steamy skin" },
                uncommonHair: new[] { "Flames like hair on the head", "Flames engulfing the head" },
                allEyes: new[] { "White eyes", "Yellow eyes" },
                allOther: new[] { "Wings composed of steam, body dripping with steamy moisture",
                    "Wings composed of steam, body is billowing steam, a flame roars in the stomach", "Wings composed of flames, body dripping with steamy moisture" });
            //Source: https://forgottenrealms.fandom.com/wiki/Water_mephit
            appearances[CreatureConstants.Mephit_Water] = GetWeightedAppearances(
                allSkin: new[] { "Blue scaly skin", "Sea-green scaly skin", "Light-blue scaly skin" },
                allEyes: new[] { "Large, bulbous, black eyes", "Large, bulbous, sea-green eyes", "Large, bulbous, red eyes" },
                allOther: new[] { "Fin-like wings, body dripping with salt water" });
            //Source: https://forgottenrealms.fandom.com/wiki/Merfolk
            appearances[CreatureConstants.Merfolk] = GetWeightedAppearances(
                commonSkin: new[] { "Pale pink skin, deep kelly-green scales", "Pale pink skin, brilliant silver scales", "Pale pink skin, kelly-green scales",
                        "Pale pink skin, dark silver scales", "Pale pink skin, green-silver scales", "Pale pink skin, pale green scales",
                        "Pale pink skin, brilliant green scales",
                    "Pale tan skin, deep kelly-green scales", "Pale tan skin, brilliant silver scales", "Pale tan skin, kelly-green scales",
                        "Pale tan skin, dark silver scales", "Pale tan skin, green-silver scales", "Pale tan skin, pale green scales",
                        "Pale tan skin, brilliant green scales",
                    "Tan skin, deep kelly-green scales", "Tan skin, brilliant silver scales", "Tan skin, kelly-green scales",
                        "Tan skin, dark silver scales", "Tan skin, green-silver scales", "Tan skin, pale green scales",
                        "Tan skin, brilliant green scales" },
                uncommonSkin: new[] { "TODO HUMAN skin",
                    "... deep kelly-green scales", "... brilliant silver scales", "... kelly-green scales",
                        "... dark silver scales", "... green-silver scales", "... pale green scales",
                        "... brilliant green scales",
                    "Pale pink skin, orange scales", "Pale pink skin, yellow scales", "Pale pink skin, red scales",
                        "Pale pink skin, blue scales", "Pale pink skin, purple scales",
                    "Pale tan skin, orange scales", "Pale tan skin, yellow scales", "Pale tan skin, red scales",
                        "Pale tan skin, blue scales", "Pale tan skin, purple scales",
                    "Tan skin, orange scales", "Tan skin, yellow scales", "Tan skin, red scales",
                        "Tan skin, blue scales", "Tan skin, purple scales",
                    "Pale pink skin, deep kelly-green scales with darker stripes", "Pale pink skin, brilliant silver scales with darker stripes",
                        "Pale pink skin, kelly-green scales with darker stripes", "Pale pink skin, dark silver scales with darker stripes",
                        "Pale pink skin, green-silver scales with darker stripes", "Pale pink skin, pale green scales with darker stripes",
                        "Pale pink skin, brilliant green scales with darker stripes",
                    "Pale pink skin, deep kelly-green scales with lighter stripes", "Pale pink skin, brilliant silver scales with lighter stripes",
                        "Pale pink skin, kelly-green scales with lighter stripes", "Pale pink skin, dark silver scales with lighter stripes",
                        "Pale pink skin, green-silver scales with lighter stripes", "Pale pink skin, pale green scales with lighter stripes",
                        "Pale pink skin, brilliant green scales with lighter stripes",
                    "Pale pink skin, deep kelly-green scales with darker speckles", "Pale pink skin, brilliant silver scales with darker speckles",
                        "Pale pink skin, kelly-green scales with darker speckles", "Pale pink skin, dark silver scales with darker speckles",
                        "Pale pink skin, green-silver scales with darker speckles", "Pale pink skin, pale green scales with darker speckles",
                        "Pale pink skin, brilliant green scales with darker speckles",
                    "Pale pink skin, deep kelly-green scales with lighter speckles", "Pale pink skin, brilliant silver scales with lighter speckles",
                        "Pale pink skin, kelly-green scales with lighter speckles", "Pale pink skin, dark silver scales with lighter speckles",
                        "Pale pink skin, green-silver scales with lighter speckles", "Pale pink skin, pale green scales with lighter speckles",
                        "Pale pink skin, brilliant green scales with lighter speckles",
                    "Pale tan skin, deep kelly-green scales with darker stripes", "Pale tan skin, brilliant silver scales with darker stripes",
                        "Pale tan skin, kelly-green scales with darker stripes", "Pale tan skin, dark silver scales with darker stripes",
                        "Pale tan skin, green-silver scales with darker stripes", "Pale tan skin, pale green scales with darker stripes",
                        "Pale tan skin, brilliant green scales with darker stripes",
                    "Pale tan skin, deep kelly-green scales with lighter stripes", "Pale tan skin, brilliant silver scales with lighter stripes",
                        "Pale tan skin, kelly-green scales with lighter stripes", "Pale tan skin, dark silver scales with lighter stripes",
                        "Pale tan skin, green-silver scales with lighter stripes", "Pale tan skin, pale green scales with lighter stripes",
                        "Pale tan skin, brilliant green scales with lighter stripes",
                    "Pale tan skin, deep kelly-green scales with darker speckles", "Pale tan skin, brilliant silver scales with darker speckles",
                        "Pale tan skin, kelly-green scales with darker speckles", "Pale tan skin, dark silver scales with darker speckles",
                        "Pale tan skin, green-silver scales with darker speckles", "Pale tan skin, pale green scales with darker speckles",
                        "Pale tan skin, brilliant green scales with darker speckles",
                    "Pale tan skin, deep kelly-green scales with lighter speckles", "Pale tan skin, brilliant silver scales with lighter speckles",
                        "Pale tan skin, kelly-green scales with lighter speckles", "Pale tan skin, dark silver scales with lighter speckles",
                        "Pale tan skin, green-silver scales with lighter speckles", "Pale tan skin, pale green scales with lighter speckles",
                        "Pale tan skin, brilliant green scales with lighter speckles",
                    "Tan skin, deep kelly-green scales with darker stripes", "Tan skin, brilliant silver scales with darker stripes",
                        "tan skin, kelly-green scales with darker stripes", "Tan skin, dark silver scales with darker stripes",
                        "tan skin, green-silver scales with darker stripes", "Tan skin, pale green scales with darker stripes",
                        "tan skin, brilliant green scales with darker stripes",
                    "Tan skin, deep kelly-green scales with lighter stripes", "Tan skin, brilliant silver scales with lighter stripes",
                        "Tan skin, kelly-green scales with lighter stripes", "Tan skin, dark silver scales with lighter stripes",
                        "Tan skin, green-silver scales with lighter stripes", "Tan skin, pale green scales with lighter stripes",
                        "Tan skin, brilliant green scales with lighter stripes",
                    "Tan skin, deep kelly-green scales with darker speckles", "Tan skin, brilliant silver scales with darker speckles",
                        "Tan skin, kelly-green scales with darker speckles", "Tan skin, dark silver scales with darker speckles",
                        "Tan skin, green-silver scales with darker speckles", "Tan skin, pale green scales with darker speckles",
                        "Tan skin, brilliant green scales with darker speckles",
                    "Tan skin, deep kelly-green scales with lighter speckles", "Tan skin, brilliant silver scales with lighter speckles",
                        "Tan skin, kelly-green scales with lighter speckles", "Tan skin, dark silver scales with lighter speckles",
                        "Tan skin, green-silver scales with lighter speckles", "Tan skin, pale green scales with lighter speckles",
                        "Tan skin, brilliant green scales with lighter speckles",
                    "Pale pink skin, copper scales", "Pale pink skin, bronze scales", "Pale pink skin, golden scales",
                    "Pale tan skin, copper scales", "Pale tan skin, bronze scales", "Pale tan skin, golden scales",
                    "Tan skin, copper scales", "Tan skin, bronze scales", "Tan skin, golden scales", },
                rareSkin: new[] { "TODO HUMAN skin",
                    "... orange scales", "... yellow scales", "... red scales",
                        "... blue scales", "... purple scales",
                        "... copper scales", "... bronze scales", "... golden scales",
                    "... orange scales with darker stripes", "... yellow scales with darker stripes", "... red scales with darker stripes",
                        "... blue scales with darker stripes", "... purple scales with darker stripes",
                        "... copper scales with darker stripes", "... bronze scales with darker stripes", "... golden scales with darker stripes",
                    "... orange scales with lighter stripes", "... yellow scales with lighter stripes", "... red scales with lighter stripes",
                        "... blue scales with lighter stripes", "... purple scales with lighter stripes",
                        "... copper scales with lighter stripes", "... bronze scales with lighter stripes", "... golden scales with lighter stripes",
                    "... orange scales with darker speckles", "... yellow scales with darker speckles", "... red scales with darker speckles",
                        "... blue scales with darker speckles", "... purple scales with darker speckles",
                        "... copper scales with darker speckles", "... bronze scales with darker speckles", "... golden scales with darker speckles",
                    "... orange scales with lighter speckles", "... yellow scales with lighter speckles", "... red scales with lighter speckles",
                        "... blue scales with lighter speckles", "... purple scales with lighter speckles",
                        "... copper scales with lighter speckles", "... bronze scales with lighter speckles", "... golden scales with lighter speckles", },
                commonHair: new[] { "Light brown hair", "Blond hair" },
                uncommonHair: new[] { "TODO HUMAN hair" },
                rareHair: new[] { "Blue hair", "Silver hair" },
                allEyes: new[] { "TODO HUMAN eyes with ice-blue pupils",
                    "TODO HUMAN eyes with pale-silver pupils" },
                commonOther: new[] { "Fish-like tail instead of legs, gill slits along the neck, slight webbing between fingers" },
                uncommonOther: new[] { "Fish-like tail instead of legs, gill slits along the neck, slight webbing between fingers, tattoos on the arms",
                    "Fish-like tail instead of legs, gill slits along the neck, slight webbing between fingers, tattoos on the chest",
                    "Fish-like tail instead of legs, gill slits along the neck, slight webbing between fingers, tattoos on the back",
                    "Fish-like tail instead of legs, gill slits along the neck, slight webbing between fingers, tattoos on the face",
                    "Fish-like tail instead of legs, gill slits along the neck, slight webbing between fingers, tattoos on the arms and chest",
                    "Fish-like tail instead of legs, gill slits along the neck, slight webbing between fingers, tattoos on the arms and back",
                    "Fish-like tail instead of legs, gill slits along the neck, slight webbing between fingers, tattoos on the arms and face",
                    "Fish-like tail instead of legs, gill slits along the neck, slight webbing between fingers, tattoos on the chest and back",
                    "Fish-like tail instead of legs, gill slits along the neck, slight webbing between fingers, tattoos on the chest and face",
                    "Fish-like tail instead of legs, gill slits along the neck, slight webbing between fingers, tattoos on the back and face",
                    "Fish-like tail instead of legs, gill slits along the neck, slight webbing between fingers, covered in tattoos" });
            //Source: https://www.d20srd.org/srd/monsters/mimic.htm
            //https://forgottenrealms.fandom.com/wiki/Mimic
            appearances[CreatureConstants.Mimic] = new[] { "Shaped like a door", "Shaped like a treasure chest, closed and locked", "Shaped like a rug",
                "Shaped like a bookshelf filled with books", "Shaped like a treasure chest, cracked open and glistening with gold",
                "Shaped like a bed", "Shaped like a wooden chair", "Shaped like a table", "Shaped like a trap door in the floor", "Shaped like a raised stone platform",
                "Shaped like a stone pedestal with a glass orb on top", "Shaped like an altar", "Shaped like a barrel filled with gold",
                "Shaped like a barrel filled with gleaming swords", "Shaped like an empty barrel", "Shaped like a bench", "Shaped like a brazier, unlit",
                "Shaped like a candelabra, unlit", "Shaped like a cauldron", "Shaped like a gate with a solid lock", "Shaped like a chest of drawers",
                "Shaped like a storage chest", "Shaped like a coat rack", "Shaped like a cupboard", "Shaped like a crate", "Shaped like a sofa", "Shaped like a divan",
                "Shaped like a wingback, cushioned chair", "Shaped like a fountain without running water, with a long lever on the side", "Shaped like an iron maiden",
                "Shaped like a ladder", "Shaped like a loom", "Shaped like a stove", "Shaped like an oven", "Shaped like a small set of stairs", "Shaped like a ramp",
                "Shaped like a throne", "Shaped like a washbasin", "Shaped like a bathtub", "Shaped like a wardrobe",
                "Shaped like a weapon rack, filled with gleaming blades", "Shaped like a weapon rack, filled with finely-carved staffs and mallets",
                "Shaped like a gleaming suit of armor (full plate)", "Shaped like a gleaming suit of armor (half plate)", "Shaped like a workbench" };
            //Source: https://forgottenrealms.fandom.com/wiki/Mind_flayer
            appearances[CreatureConstants.MindFlayer] = GetWeightedAppearances(
                allSkin: new[] { "Soft, supple, moist, rubbery, mauve skin", "Soft, supple, moist, rubbery, greenish-violet skin",
                    "Soft, supple, moist, rubbery, pale purple skin", "Soft, supple, moist, rubbery, pale violet skin", "Soft, supple, moist, rubbery, violet skin",
                    "Soft, supple, moist, rubbery, purple skin", "Soft, supple, moist, rubbery, greenish-purple skin",
                    "Soft, supple, moist, rubbery, pale greenish-purple skin" },
                allEyes: new[] { "Solid white eyes" },
                allOther: new[] { "Four purplish-black facial tentacles" });
            //Source: https://forgottenrealms.fandom.com/wiki/Minotaur
            appearances[CreatureConstants.Minotaur] = GetWeightedAppearances(
                allSkin: new[] { "Black skin", "Brown skin", "Fair skin", "Pale skin" },
                allHair: new[] { "Black hair and fur", "Brown hair and fur", "Dark brown hair and fur" },
                commonOther: new[] { "Hooves on legs, dark yellow horns", "Hooves on legs, brown horns", "Hooves on legs, yellow horns",
                    "Hooves on legs, yellow-brown horns", "Hooves on legs, black horns", "Hooves on legs, dark brown horns", "Hooves on legs, white horns",
                    "Hooves on legs, beige horns" },
                uncommonOther: new[] { "Hooves on legs, dark yellow horns, cow-like tail", "Hooves on legs, brown horns, cow-like tail",
                    "Hooves on legs, yellow horns, cow-like tail", "Hooves on legs, yellow-brown horns, cow-like tail", "Hooves on legs, black horns, cow-like tail",
                    "Hooves on legs, dark brown horns, cow-like tail", "Hooves on legs, white horns, cow-like tail", "Hooves on legs, beige horns, cow-like tail" });
            //Source: https://forgottenrealms.fandom.com/wiki/Mohrg
            appearances[CreatureConstants.Mohrg] = new[] { "Skeletal corpse filled with writhing organs" };
            //Source: https://www.d20srd.org/srd/monsters/monkey.htm
            //https://www.google.com/search?q=colobus+monkey+species
            //https://www.google.com/search?q=capuchin+monkey+species
            appearances[CreatureConstants.Monkey] = new[] { "Tufted capuchin", "Panamanian white-faced capuchin", "Black-and-white colobus", "Black colobus",
                "Mantled guereza", "Red colobus", "Colombian white-faced capuchin", "White-fronted capuchin", "Black capuchin", "Wedge-capped capuchin",
                "Golden-bellied capuchin", "Kaapori capuchin", "Blond capuchin", "Crested capuchin", "Black-striped capuchin", "Marañón white-fronted capuchin",
                "Azara's capuchin", "Varied white-fronted capuchin", "Spix's white-fronted capuchin", "Santa Marta white-fronted capuchin", "Killikaike", "Acrecebus" };
            //Source: https://www.newworldencyclopedia.org/entry/Mule
            appearances[CreatureConstants.Mule] = GetWeightedAppearances(
                commonHair: new[] { "Bay coloration", "Sorrel coloration", "Black fur", "Gray fur" },
                uncommonHair: new[] { "White fur", "Blue roan coloration", "Red roan coloration", "Palomino coloration", "Dun coloration", "Buckskin coloration" },
                rareHair: new[] { "Paint coloration", "Tobiano coloration" });
            //Source: https://www.d20srd.org/srd/monsters/mummy.htm
            appearances[CreatureConstants.Mummy] = GetWeightedAppearances(
                commonOther: new[] { "Desiccated husk wrapped in linen burial wraps, pungent cinnamon smell",
                    "Desiccated husk wrapped in linen burial wraps, pungent ginger smell",
                    "Desiccated husk wrapped in linen burial wraps, pungent saltpeter smell",
                    "Desiccated husk wrapped in linen burial wraps, pungent roses smell",
                    "Desiccated husk wrapped in linen burial wraps, pungent myrrh smell",
                    "Desiccated husk wrapped in linen burial wraps, pungent frankincense smell",
                    "Desiccated husk wrapped in linen burial wraps, pungent lye smell",
                    "Desiccated husk wrapped in linen burial wraps, pungent wormwood smell",
                    "Desiccated husk wrapped in linen burial wraps, pungent pomegranate smell" },
                uncommonOther: new[] { "Desiccated husk wrapped in linen burial wraps, pungent upturned-earth smell",
                    "Desiccated husk wrapped in linen burial wraps, pungent decay smell",
                    "Desiccated husk wrapped in linen burial wraps, pungent vegetable rot smell" },
                rareOther: new[] { "Desiccated husk wrapped in linen burial wraps, pungent bread smell",
                    "Desiccated husk wrapped in linen burial wraps, pungent yeast smell",
                    "Desiccated husk wrapped in linen burial wraps, pungent wine smell",
                    "Desiccated husk wrapped in linen burial wraps, pungent vinegar smell",
                    "Desiccated husk wrapped in linen burial wraps, pungent roasted meats smell" });
            //Source: https://forgottenrealms.fandom.com/wiki/Dark_naga
            appearances[CreatureConstants.Naga_Dark] = GetWeightedAppearances(
                commonSkin: new[] { "Dark blue scales with black frills", "Dark blue patterned scales", "Dark blue patterned scales with black frills",
                    "Purple scales with black frills", "Purple patterned scales", "Purple patterned scales with black frills" },
                uncommonSkin: new[] { "Dark blue scales", "Purple scales" });
            //Source: https://forgottenrealms.fandom.com/wiki/Guardian_naga
            appearances[CreatureConstants.Naga_Guardian] = GetWeightedAppearances(
                commonSkin: new[] { "Green-gold scales with silvery triangles down the spine" },
                uncommonSkin: new[] { "Green-gold scales with silvery triangles down the spine, golden frill runs down the length of the spine" },
                allEyes: new[] { "Bright golden eyes" },
                uncommonOther: new[] { "Smells of flowers" });
            //Source: https://forgottenrealms.fandom.com/wiki/Spirit_naga
            appearances[CreatureConstants.Naga_Spirit] = GetWeightedAppearances(
                allSkin: new[] { "Black scales with red bands" },
                allHair: new[] { "Stringy black hair" },
                allOther: new[] { "Emits the stench of carrion" });
            //Source: https://forgottenrealms.fandom.com/wiki/Water_naga
            appearances[CreatureConstants.Naga_Water] = GetWeightedAppearances(
                allSkin: new[] { "Emerald-green skin, with reticulated rings of light olive and dark emerald green",
                    "Turquoise skin, with reticulated rings of light olive and dark emerald green",
                    "Blue-green skin, with reticulated rings of light olive and dark emerald green",
                    "Green skin, with reticulated rings of light olive and dark emerald green",
                    "Blue skin, with reticulated rings of light olive and dark emerald green",
                    "Emerald-green skin, with reticulated rings of dark brown and gray",
                    "Turquoise skin, with reticulated rings of dark brown and gray",
                    "Blue-green skin, with reticulated rings of dark brown and gray",
                    "Green skin, with reticulated rings of dark brown and gray",
                    "Blue skin, with reticulated rings of dark brown and gray"},
                allEyes: new[] { "Pale green eyes", "Bright amber eyes" },
                allOther: new[] { "Fiery red and orange spines" });
            //Source: https://forgottenrealms.fandom.com/wiki/Nalfeshnee
            appearances[CreatureConstants.Nalfeshnee] = new[] { "Corpulent, combining the most abysmal features of a boar and ape into a bipedal monstrosity. Small, feathered wings." };
            //Source: https://forgottenrealms.fandom.com/wiki/Night_hag
            appearances[CreatureConstants.NightHag] = GetWeightedAppearances(
                allSkin: new[] { "Blue-violet skin", "Light blue-violet skin", "Dark blue-violet skin", "Blue-black skin", "Purple-black skin" },
                allHair: new[] { "Jet-black hair", "Pitch-black hair" },
                allEyes: new[] { "Red eyes" },
                commonOther: new[] { "Long, night-black nails. Tattoo-like scars. Grotesque warts, open sores, diseased blisters" },
                uncommonOther: new[] { "Long, night-black nails. Tattoo-like scars" });
            //Source: https://forgottenrealms.fandom.com/wiki/Nightcrawler
            appearances[CreatureConstants.Nightcrawler] = GetWeightedAppearances(
                allSkin: new[] { "Solid black skin" },
                allOther: new[] { "Black teeth" });
            //Source: https://forgottenrealms.fandom.com/wiki/Nightmare_(creature)
            appearances[CreatureConstants.Nightmare] = GetWeightedAppearances(
                allSkin: new[] { "Night-black coat" },
                allHair: new[] { "Red and orange flames for mane and tail" },
                allEyes: new[] { "Glowing red eyes", "Dark eyes illuminated with flames" },
                allOther: new[] { "Viper fangs" });
            appearances[CreatureConstants.Nightmare_Cauchemar] = GetWeightedAppearances(
                allSkin: new[] { "Night-black coat" },
                allHair: new[] { "Red and orange flames for mane and tail" },
                allEyes: new[] { "Glowing red eyes", "Dark eyes illuminated with flames" },
                allOther: new[] { "Viper fangs" });
            //Source: https://forgottenrealms.fandom.com/wiki/Nightwalker
            appearances[CreatureConstants.Nightwalker] = new[] { "Body looks as if it is made of shadow-stuff, with a vaguely humanoid form. Smooth and hairless." };
            //Source: https://forgottenrealms.fandom.com/wiki/Nightwing
            appearances[CreatureConstants.Nightwing] = new[] { "Bat-shaped darkness" };
            //Source: https://forgottenrealms.fandom.com/wiki/Nixie
            appearances[CreatureConstants.Nixie] = GetWeightedAppearances(
                allSkin: new[] { "Pale green skin" },
                allHair: new[] { "Dark green hair" },
                allEyes: new[] { "Wide, silver eyes" },
                allOther: new[] { "Webbed fingers and toes, pointed ears" });
            //Source: https://forgottenrealms.fandom.com/wiki/Nymph
            appearances[CreatureConstants.Nymph] = GetWeightedAppearances(
                allSkin: new[] { "Vibrant green skin", "Autumnal orange skin", "Autumnal red skin", "Brown skin", "Yellow skin",
                    "TODO HIGH ELF skin" },
                allHair: new[] { "Green hair" , "Orange hair", "Red hair", "Orange-brown hair", "Red-brown hair",
                    "TODO HIGH ELF hair" },
                allEyes: new[] { "Yellow eyes", "Green eyes",
                    "TODO HIGH ELF eyes" });
            //Source: https://forgottenrealms.fandom.com/wiki/Ochre_jelly
            appearances[CreatureConstants.OchreJelly] = GetWeightedAppearances(
                allSkin: new[] { "Dark yellow skin" },
                allOther: new[] { "Resembles giant amoeba" });
            //Source: https://kids.nationalgeographic.com/animals/invertebrates/facts/octopus
            appearances[CreatureConstants.Octopus] = GetWeightedAppearances(
                allSkin: new[] { "Gray skin", "Brown skin", "Pink skin", "Blue skin", "Green skin", "Reddish-brown skin" });
            //Source: https://www.nationalgeographic.com/animals/invertebrates/facts/giant-pacific-octopus
            appearances[CreatureConstants.Octopus_Giant] = GetWeightedAppearances(
                allSkin: new[] { "Gray skin", "Brown skin", "Pink skin", "Blue skin", "Green skin", "Reddish-brown skin" });
            //Source: https://forgottenrealms.fandom.com/wiki/Ogre
            appearances[CreatureConstants.Ogre] = GetWeightedAppearances(
                allSkin: new[] { "Beige skin", "Pale beige skin", "White skin", "Pale gray skin", "Pale pink skin", "Pink skin", "Red-brown skin" },
                allEyes: new[] { "Yellow eyes", "Green eyes" });
            //Source: https://forgottenrealms.fandom.com/wiki/Merrow_(ogre)
            appearances[CreatureConstants.Ogre_Merrow] = GetWeightedAppearances(
                allSkin: new[] { "Green scales" },
                allHair: new[] { "Seaweed-green, slimy hair" },
                allEyes: new[] { "Dark green eyes", "Green eyes" },
                commonOther: new[] { "Black teeth and nails, webbed hands and feet, underbite, sloping shoulders, covered in tattoos" },
                rareOther: new[] { "Black teeth and nails, webbed hands and feet, underbite, sloping shoulders, ivory horns, covered in tattoos" });
            //Source: https://forgottenrealms.fandom.com/wiki/Oni_mage
            appearances[CreatureConstants.OgreMage] = GetWeightedAppearances(
                allSkin: new[] { "Turquoise skin", "Pale turquoise skin" },
                allHair: new[] { "White hair", "Light gray hair", "Black hair", "Blue-black hair" },
                allEyes: new[] { "Yellow eyes", "Green eyes" },
                allOther: new[] { "Black nails", "Black nails, white horns", "Black nails, black horns" });
            //Source: https://forgottenrealms.fandom.com/wiki/Orc
            appearances[CreatureConstants.Orc] = GetWeightedAppearances(
                allSkin: new[] { "Gray skin" },
                allHair: new[] { "Coarse black hair" },
                allEyes: new[] { "Reddish eyes" },
                allOther: new[] { "Low forehead, tusks" });
            //Source: https://forgottenrealms.fandom.com/wiki/Half-orc
            appearances[CreatureConstants.Orc_Half] = GetWeightedAppearances(
                commonSkin: new[] { "Grayish skin" },
                uncommonSkin: new[] { "TODO HUMAN skin" },
                commonHair: new[] { "Coarse black hair on head and body" },
                uncommonHair: new[] { "Coarse dark gray hair on head and body", "Coarse gray hair on head and body", "TODO HUMAN hair" },
                allEyes: new[] { "TODO HUMAN eyes", "TODO ORC eyes" },
                allOther: new[] { "Jutting jaw, sloping forehead, small tusks" });
            //Source: https://forgottenrealms.fandom.com/wiki/Otyugh
            appearances[CreatureConstants.Otyugh] = GetWeightedAppearances(
                allSkin: new[] { "Disgusting light brown rock-like hide", "Disgusting green rock-like hide", "Disgusting brown rock-like hide" },
                allOther: new[] { "Bloated, oval-shaped body. Three shuffling elephantine legs. Long tentacles bedecked in rough thorny growths ending in leaf-shaped pads with rows of more sharp spikes. Third tentacle sprouts from the top of the body, forming a vine-like stalk, ending in pair of eyes and olfactory organ. Fang-filled mouth." });
            //Source: https://www.dimensions.com/element/great-horned-owl-bubo-virginianus
            //https://www.allaboutbirds.org/guide/Great_Horned_Owl/species-compare/
            //https://carnivora.net/great-horned-owl-v-powerful-owl-t7257.html
            //https://www.allaboutbirds.org/guide/Long-eared_Owl/species-compare/
            appearances[CreatureConstants.Owl] = new[] { "Barn Owl", "Great Horned Owl", "Snowy Owl", "Barred Owl", "Great Gray Owl", "Long-Eared Owl", "Powerful Owl" };
            //Source: https://www.d20srd.org/srd/monsters/owlGiant.htm 
            appearances[CreatureConstants.Owl_Giant] = new[] { "Resembles a Barn Owl", "Resembles a Great Horned Owl", "Resembles a Snowy Owl", "Resembles a Barred Owl",
                "Resembles a Great Gray Owl", "Resembles a Long-Eared Owl", "Resembles a Powerful Owl" };
            //Source: https://forgottenrealms.fandom.com/wiki/Owlbear
            appearances[CreatureConstants.Owlbear] = GetWeightedAppearances(
                allHair: new[] { "Brown-black feathers and fur", "Yellow-brown feathers and fur", "Brown feathers and fur", "Dark brown feathers and fur" },
                allEyes: new[] { "Eyes rimmed in red with limpid pupils" },
                allOther: new[] { "Bear-like body, avian head, hooked yellow beak", "Bear-like body, avian head, serrated yellow beak",
                    "Bear-like body, avian head, hooked yellow-white beak", "Bear-like body, avian head, serrated yellow-white beak",
                    "Bear-like body, avian head, hooked dull ivory beak", "Bear-like body, avian head, serrated dull ivory beak" });
            //Source: https://forgottenrealms.fandom.com/wiki/Pegasus
            appearances[CreatureConstants.Pegasus] = GetWeightedAppearances(
                allHair: new[] { "White hair on the body, white feathers for mane and tail, white feathered legs" },
                allOther: new[] { "Resembles horse with bird-like wings" });
            //Source: https://forgottenrealms.fandom.com/wiki/Phantom_fungus
            appearances[CreatureConstants.PhantomFungus] = GetWeightedAppearances(
                allSkin: new[] { "Brown skin", "Greenish-brown skin" },
                allOther: new[] { "Fungal mass supported by four stumpy legs. Cluster of nodules atop the body. Gaping maw lined with row of teeth." });
            //Source: https://forgottenrealms.fandom.com/wiki/Phase_spider
            appearances[CreatureConstants.PhaseSpider] = GetWeightedAppearances(
                allSkin: new[] { "Dark green/white skin", "Green/white skin", "Blue-green/white skin" },
                allOther: new[] { "Spindly-legged spider" });
            //Source: https://forgottenrealms.fandom.com/wiki/Phasm
            appearances[CreatureConstants.Phasm] = new[] { "In its true form, resembles an amorphous, colorful ooze. However, they were natural shapechangers that could take the form of almost any creature or object." };
            //Source: https://forgottenrealms.fandom.com/wiki/Pit_fiend
            appearances[CreatureConstants.PitFiend] = GetWeightedAppearances(
                allSkin: new[] { "Red scales" },
                allOther: new[] { "Massive bat-like wings. Huge fangs drop with green venom. Prehensile tail." });
            //Source: https://forgottenrealms.fandom.com/wiki/Pixie
            appearances[CreatureConstants.Pixie] = GetWeightedAppearances(
                allSkin: new[] { "TODO HIGH ELF skin", "Green skin" },
                allHair: new[] { "TODO HIGH ELF hair", "Green hair", "Red hair" },
                allEyes: new[] { "TODO HIGH ELF eyes" },
                allOther: new[] { "Silvery, moth-like wings" });
            appearances[CreatureConstants.Pixie_WithIrresistibleDance] = GetWeightedAppearances(
                allSkin: new[] { "TODO HIGH ELF skin", "Green skin" },
                allHair: new[] { "TODO HIGH ELF hair", "Green hair", "Red hair" },
                allEyes: new[] { "TODO HIGH ELF eyes" },
                allOther: new[] { "Silvery, moth-like wings" });
            //Source: https://www.google.com/search?q=breeds+of+pony
            appearances[CreatureConstants.Pony] = new[] { "Shetland Pony", "Connemara Pony", "Pony of the Americas", "New Forest Pony", "Dartmoor Pony", "Exmoor Pony",
                "Fell Pony", "Asturcon", "Burmese Pony" };
            appearances[CreatureConstants.Pony_War] = new[] { "Shetland Pony", "Connemara Pony", "Pony of the Americas", "New Forest Pony", "Dartmoor Pony", "Exmoor Pony",
                "Fell Pony", "Asturcon", "Burmese Pony" };
            //Source: https://www.google.com/search?q=species+of+porpoise
            appearances[CreatureConstants.Porpoise] = new[] { "Harbour porpoise", "Vaquita", "Dall's porpoise", "Spectacles porpoise", "Burmeister's porpoise",
                "Indo-Pacific finless porpoise", "Narrow-ridged finless porpoise" };
            //Source: https://forgottenrealms.fandom.com/wiki/Giant_praying_mantis
            appearances[CreatureConstants.PrayingMantis_Giant] = GetWeightedAppearances(
                allSkin: new[] { "Brown coloration", "Green coloration", "Yellow coloration", "Yellow-brown coloration", "Yellow-green coloration",
                    "Brown-green coloration" });
            //Source: https://forgottenrealms.fandom.com/wiki/Pseudodragon
            appearances[CreatureConstants.Pseudodragon] = GetWeightedAppearances(
                allSkin: new[] { "Brownish-red skin" });
            //Source: https://forgottenrealms.fandom.com/wiki/Purple_worm
            appearances[CreatureConstants.PurpleWorm] = GetWeightedAppearances(
                allSkin: new[] { "Deep purple skin" });
            //Source: https://forgottenrealms.fandom.com/wiki/Pyrohydra
            appearances[CreatureConstants.Pyrohydra_5Heads] = GetWeightedAppearances(
                allSkin: new[] { "Reddish skin, yellow underbelly" },
                allEyes: new[] { "Amber eyes" });
            appearances[CreatureConstants.Pyrohydra_6Heads] = GetWeightedAppearances(
                allSkin: new[] { "Reddish skin, yellow underbelly" },
                allEyes: new[] { "Amber eyes" });
            appearances[CreatureConstants.Pyrohydra_7Heads] = GetWeightedAppearances(
                allSkin: new[] { "Reddish skin, yellow underbelly" },
                allEyes: new[] { "Amber eyes" });
            appearances[CreatureConstants.Pyrohydra_8Heads] = GetWeightedAppearances(
                allSkin: new[] { "Reddish skin, yellow underbelly" },
                allEyes: new[] { "Amber eyes" });
            appearances[CreatureConstants.Pyrohydra_9Heads] = GetWeightedAppearances(
                allSkin: new[] { "Reddish skin, yellow underbelly" },
                allEyes: new[] { "Amber eyes" });
            appearances[CreatureConstants.Pyrohydra_10Heads] = GetWeightedAppearances(
                allSkin: new[] { "Reddish skin, yellow underbelly" },
                allEyes: new[] { "Amber eyes" });
            appearances[CreatureConstants.Pyrohydra_11Heads] = GetWeightedAppearances(
                allSkin: new[] { "Reddish skin, yellow underbelly" },
                allEyes: new[] { "Amber eyes" });
            appearances[CreatureConstants.Pyrohydra_12Heads] = GetWeightedAppearances(
                allSkin: new[] { "Reddish skin, yellow underbelly" },
                allEyes: new[] { "Amber eyes" });
            //Source: https://forgottenrealms.fandom.com/wiki/Quasit
            appearances[CreatureConstants.Quasit] = GetWeightedAppearances(
                allSkin: new[] { "Green skin covered in warts and postules" },
                commonOther: new[] { "Tail covered in barbs, spiky horns" },
                uncommonOther: new[] { "Tail covered in barbs, spiky horns, bat-like wings" });
            //Source: https://forgottenrealms.fandom.com/wiki/Rakshasa
            appearances[CreatureConstants.Rakshasa] = GetWeightedAppearances(
                uncommonSkin: new[] { "TODO CROCODILE skin", "TODO MANTIS skin" },
                commonHair: new[] { "TODO TIGER fur" },
                uncommonHair: new[] { "TODO APE fur" },
                commonOther: new[] { "Reversed hands, tiger head" },
                uncommonOther: new[] { "Reversed hands, ape head", "Reversed hands, crocodile head", "Reversed hands, mantis head" });
            //Source: https://dungeonsdragons.fandom.com/wiki/Rast
            appearances[CreatureConstants.Rast] = new[]
            {
                "Central body about the size of a large dog, with 10 spindly spider's legs coming out of it. Each of these legs is tipped with a claw. The head is almost exactly the same size as the central body, and resembles a cross between a snake, a vulture and a goblin (some would say it is almost monkey-like). The central body and legs of the Rast are brown, while the head and claws are rust red.",
                "Central body about the size of a large dog, with 11 spindly spider's legs coming out of it. Each of these legs is tipped with a claw. The head is almost exactly the same size as the central body, and resembles a cross between a snake, a vulture and a goblin (some would say it is almost monkey-like). The central body and legs of the Rast are brown, while the head and claws are rust red.",
                "Central body about the size of a large dog, with 12 spindly spider's legs coming out of it. Each of these legs is tipped with a claw. The head is almost exactly the same size as the central body, and resembles a cross between a snake, a vulture and a goblin (some would say it is almost monkey-like). The central body and legs of the Rast are brown, while the head and claws are rust red.",
                "Central body about the size of a large dog, with 13 spindly spider's legs coming out of it. Each of these legs is tipped with a claw. The head is almost exactly the same size as the central body, and resembles a cross between a snake, a vulture and a goblin (some would say it is almost monkey-like). The central body and legs of the Rast are brown, while the head and claws are rust red.",
                "Central body about the size of a large dog, with 14 spindly spider's legs coming out of it. Each of these legs is tipped with a claw. The head is almost exactly the same size as the central body, and resembles a cross between a snake, a vulture and a goblin (some would say it is almost monkey-like). The central body and legs of the Rast are brown, while the head and claws are rust red.",
                "Central body about the size of a large dog, with 15 spindly spider's legs coming out of it. Each of these legs is tipped with a claw. The head is almost exactly the same size as the central body, and resembles a cross between a snake, a vulture and a goblin (some would say it is almost monkey-like). The central body and legs of the Rast are brown, while the head and claws are rust red.",
            };
            //Source: https://rockypointrattery.com/rat-colors-%26-markings
            appearances[CreatureConstants.Rat] = new[] { "Standard coat: short, smooth, and glossy black fur",
                "Rex coat: evenly curled black fur, slightly curled white belly", "Rex coat: evenly curled black fur, slightly curled white belly, curled whiskers",
                "Double rex coat: Very short curly white coat", "Double rex coat: peach fuzz white fur", "Double rex coat: mostly hairless, white fur",
                "Silvermane coat: Soft, silvered fur",
                "Velveteen coat: thick, soft black/brown coat, wavy whiskers",
                "Hairless: thin, bright, almost-transluscent skin, very short curly whiskers", "Hairless: thin, bright, almost-transluscent skin, no whiskers",
                "Agouti: rich chestnut fur with dark slate at the base of the hair, silver-gray belly, black eyes",
                "Silver fawn: rich orange fur, white belly, red eyes",
                "Fawn: rich, deep golden orange fur, slightly-lighter belly, dark ruby eyes",
                "American Blue Agouti: Medium-slate fur with a yellow-tan line on top, silver-blue belly, black eyes",
                "Russian Blue Agouti: Dark steel blue fur with a fawn band on top, silver-blue belly, black eyes",
                "Cinnamon: warm russet brown fur with medium slate at the base of the hair, silver-gray belly, black eyes",
                    "Cinnamon: warm russet brown fur with medium slate at the base of the hair, silver-gray belly, ruby eyes",
                "Apricot Agouti: pale apricot fur with dark slate at the base of the hair, pale cream belly, red eyes",
                "Albino: pure clean white fur, red eyes",
                "Cinnamon Red-eyed Marten: warm russet brown fur, light heathering, red eyes",
                "Agouti Silvermane: rich chestnut, soft, silky, sheened fur with silver-white tips, appearing heavily silvered, darker fur around the muzzle and eyes, black eyes",
                "Black: solid black fur, black eyes",
                "Beige: warm grayish-tan fur, dark ruby eyes",
                "Champagne: evenly warm beige fur, red eyes",
                "American Blue: slate blue fur, dark ruby eyes", "American Blue: slate blue fur, black eyes",
                "Russian Blue: dark slate blue fur, black eyes",
                "Mink: even mid gray-brown fur with bluish sheen, ruby eyes", "Mink: even mid gray-brown fur with bluish sheen, black eyes",
                "Silver: off-white fur with a cool ice-blue cast, red eyes",
                "Pink-eyed White: pure clean white fur, red eyes",
                "Himalayan: white fur, red eyes",
                "Black Silvermane: solid black, soft, silky, sheened fur with silver-white tips, looking heavily silvered, darker fur around the muzzle and eyes",
                "Russian Blue Silvermane: Dark steel blue, soft, silky, sheened fur with silver-white tips, looking heavily silvered. Darker fur around the muzzle and eyes.",
                "Black Red-eyed Marten: light gray fur with light heathering, lighter fur around the muzzle and eyes, red eyes",
                "Berkshire: solid black fur on top, completely white belly, white feet and tail",
                "Hooded: solid black fur on head, neck, shoulders, and extending down the spine; white fur on sides, legs, and feet",
                "Bareback: solid cream-colored fur on head, neck, and shoulders; white fur on spine, sides, legs, and feet",
                "Mismarked Bareback: solid cream-colored fur on head, neck, and shoulders; small cream spot on the spine; white fur on sides, legs, and feet",
                "Variegated: solid gray fur on head, neck, shoulders; patvhes and flecks of gray fur on the back side; white fur on sides, legs, and feet; white spot on forehead",
                    "Variegated: solid gray fur on head, neck, shoulders; patvhes and flecks of gray fur on the back side; white fur on sides, legs, and feet; white star on forehead",
                "Blaze: solid gray fur on head and neck; white fur on body; white markings from nose to forehead",
                "English Irish: solid black fur; white equilateral triangle on chest; front feet white; back feet white on lower half",
                "Irish: solid black fur; white marking on belly; white feet; white-tipped tail",
                "English Irish/Irish: solid black fur; white equilateral triangle on chest; white marking on belly; front feet white; back feet white on lower half; white-tipped tail",
                "Spotted Tabby: light black fur with darker dorsal stripe and spots on sides and face",
                    "Spotted Tabby: light brown fur with darker dorsal stripe and spots on sides and face",
                    "Spotted Tabby: light gray fur with darker dorsal stripe and spots on sides and face",
                "Siamese: medium beige fur; darker points on nose, ears, feet, tail, and tail root; red eyes",
                    "Siamese: medium beige fur; darker points on nose, ears, feet, tail, and tail root; black eyes",
                "Himalayan: white fur; rich dark sepia points on nose, ears, feet, tail, and tail root; red eyes",
                "Dalmation: white fur with numerous ragged black splotches",
            };
            //Source: https://forgottenrealms.fandom.com/wiki/Giant_rat
            appearances[CreatureConstants.Rat_Dire] = new[] { "Coarse brown-black fur" };
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            appearances[CreatureConstants.Rat_Swarm] = new[] { "300 rats" };
            //Source: https://forgottenrealms.fandom.com/wiki/Raven
            appearances[CreatureConstants.Raven] = new[] { "Glossy black feathers" };
            //Source: https://eberron.fandom.com/wiki/Ravid
            appearances[CreatureConstants.Ravid] = new[] { "Long and serpentine, floating above the ground. One single claw juts out of its body just beneath its head. Pale white, scaley skin." };
            //Source: https://www.d20srd.org/srd/monsters/razorBoar.htm 
            appearances[CreatureConstants.RazorBoar] = new[] { "Black-bristled hide marked by hundreds of old scars. Its eyes are wild and bloodshot, and its tusks are more than three feet long, gleaming like polished ivory and sharper than many swords." };
            //Source: https://forgottenrealms.fandom.com/wiki/Remorhaz
            appearances[CreatureConstants.Remorhaz] = GetWeightedAppearances(
                allSkin: new[] { "Ice-blue scales" },
                allEyes: new[] { "White eyes" },
                allOther: new[] { "Tough leathery wings, insect-like head, dozens of legs, back glows red, horns along its body, dagger-like teeth" });
            //Source: https://forgottenrealms.fandom.com/wiki/Retriever
            appearances[CreatureConstants.Retriever] = new[] { "Resembles a giant spider with four forelegs ending in large cleaver-like blades, and four rear limbs for walking and carrying most of the weight. Two primary eyes and four smaller eyes that gleam malevolently as they peak out from the carapace." };
            //Source: https://www.d20srd.org/srd/monsters/rhinoceros.htm
            //https://denverzoo.org/animals/black-rhinoceros/
            appearances[CreatureConstants.Rhinoceras] = GetWeightedAppearances(
                allSkin: new[] { "Dark gray skin", "Dark brown skin" },
                allOther: new[] { "Two horns on the nose, with the anterior horn being larger" });
            //Source: https://forgottenrealms.fandom.com/wiki/Roc
            appearances[CreatureConstants.Roc] = GetWeightedAppearances(
                commonHair: new[] { "Dark brown feathers", "Golden feathers", "Golden-brown feathers", "Dark golden-brown feathers", "Dark golden feathers" },
                rareHair: new[] { "Black feathers", "Red feathers", "White feathers" });
            //Source: https://forgottenrealms.fandom.com/wiki/Roper
            appearances[CreatureConstants.Roper] = new[] { "Earthy brown skin, resembling a stalagmite" };
            //Source: https://forgottenrealms.fandom.com/wiki/Rust_monster
            appearances[CreatureConstants.RustMonster] = new[] { "Four insectlike legs and a squat, humped body. Thick, lumpy hide is yellowish-tan on the belly and rust-red on the back. Tail ends in what looks like a double-ended paddle. Two long antennae come out of its head, one under each eye" };
            //Source: https://forgottenrealms.fandom.com/wiki/Sahuagin
            appearances[CreatureConstants.Sahuagin] = GetWeightedAppearances(
                allSkin: new[] { "Green skin, darker on the back and lighter in the front", "Green skin, darker on the back and lighter in the front, with dark stripes",
                    "Green skin, darker on the back and lighter in the front, with dark bands",
                    "Green skin, darker on the back and lighter in the front, with dark spots",
                    "TODO FEMALE speckled yellow tail",
                    "TODO FEMALE spotted yellow tail",
                    "TODO FEMALE mottled yellow tail",
                    "TODO FEMALE mostly yellow tail",
                    "TODO FEMALE fully yellow tail",
                },
                allEyes: new[] { "Silvery-black eyes" },
                allOther: new[] { "Webbed feet and hands; gills; finned tail; additional webbing down the back, on elbows, and where ears would be" });
            appearances[CreatureConstants.Sahuagin_Malenti] = GetWeightedAppearances(
                allSkin: new[] { "Greenish-silver skin" },
                allHair: new[] { "Green hair", "Blue hair" },
                allEyes: new[] { "Silvery-black eyes" },
                allOther: new[] { "Resembles an Aquatic Elf" });
            appearances[CreatureConstants.Sahuagin_Mutant] = GetWeightedAppearances(
                allSkin: new[] { "Black skin on the back and gray in the front", "Black skin on the back and gray in the front, with dark stripes",
                    "Black skin on the back and gray in the front, with dark bands",
                    "Black skin on the back and gray in the front, with dark spots",
                    "TODO FEMALE speckled yellow tail",
                    "TODO FEMALE spotted yellow tail",
                    "TODO FEMALE mottled yellow tail",
                    "TODO FEMALE mostly yellow tail",
                    "TODO FEMALE fully yellow tail",
                },
                allEyes: new[] { "Silvery-black eyes" },
                allOther: new[] { "Webbed feet and hands; gills; finned tail; additional webbing down the back, on elbows, and where ears would be; four arms" });
            //Source: https://forgottenrealms.fandom.com/wiki/Salamander
            appearances[CreatureConstants.Salamander_Flamebrother] = new[] { "Serpentine with a humanoid torso, head, and arms, albeit with spines rising from their head, arms, and spine. Red fiery skin" };
            appearances[CreatureConstants.Salamander_Average] = new[] { "Serpentine with a humanoid torso, head, and arms, albeit with spines rising from their head, arms, and spine. Red fiery skin" };
            appearances[CreatureConstants.Salamander_Noble] = new[] { "Serpentine with a humanoid torso, head, and arms, albeit with spines rising from their head, arms, and spine. Red fiery skin" };
            //Source: https://forgottenrealms.fandom.com/wiki/Satyr
            appearances[CreatureConstants.Satyr] = GetWeightedAppearances(
                allSkin: new[] { "Tan skin", "Light brown skin", "Brown skin", "Lightly tan skin", "TODO GOAT fur" },
                allHair: new[] { "Red hair", "Chestnut brown hair" },
                allOther: new[] { "Small nubs for horns, fur-covered lower bodies and legs and cloven hooves similar to those of a goat",
                    "Large curling ram horns, fur-covered lower bodies and legs and cloven hooves similar to those of a goat",
                    "Small curling ram horns, fur-covered lower bodies and legs and cloven hooves similar to those of a goat",
                    "Small straight horns, fur-covered lower bodies and legs and cloven hooves similar to those of a goat",
                    "Large straight horns, fur-covered lower bodies and legs and cloven hooves similar to those of a goat",
                });
            appearances[CreatureConstants.Satyr_WithPipes] = GetWeightedAppearances(
                allSkin: new[] { "Tan skin", "Light brown skin", "Brown skin", "Lightly tan skin", "TODO GOAT fur" },
                allHair: new[] { "Red hair", "Chestnut brown hair" },
                allOther: new[] { "Small nubs for horns, fur-covered lower bodies and legs and cloven hooves similar to those of a goat",
                    "Large curling ram horns, fur-covered lower bodies and legs and cloven hooves similar to those of a goat",
                    "Small curling ram horns, fur-covered lower bodies and legs and cloven hooves similar to those of a goat",
                    "Small straight horns, fur-covered lower bodies and legs and cloven hooves similar to those of a goat",
                    "Large straight horns, fur-covered lower bodies and legs and cloven hooves similar to those of a goat",
                });
            //Source: https://a-z-animals.com/animals/scorpion/
            appearances[CreatureConstants.Scorpion_Monstrous_Tiny] = GetWeightedAppearances(
                commonSkin: new[] { "Yellow exoskeleton", "Black exoskeleton" },
                uncommonSkin: new[] { "Brown exoskeleton", "Red exoskeleton", "White exoskeleton", "Orange exoskeleton" },
                allOther: new[] { "Front pincers and the tail with a stinger" });
            appearances[CreatureConstants.Scorpion_Monstrous_Small] = GetWeightedAppearances(
                commonSkin: new[] { "Yellow exoskeleton", "Black exoskeleton" },
                uncommonSkin: new[] { "Brown exoskeleton", "Red exoskeleton", "White exoskeleton", "Orange exoskeleton" },
                allOther: new[] { "Front pincers and the tail with a stinger" });
            appearances[CreatureConstants.Scorpion_Monstrous_Medium] = GetWeightedAppearances(
                commonSkin: new[] { "Yellow exoskeleton", "Black exoskeleton" },
                uncommonSkin: new[] { "Brown exoskeleton", "Red exoskeleton", "White exoskeleton", "Orange exoskeleton" },
                allOther: new[] { "Front pincers and the tail with a stinger" });
            appearances[CreatureConstants.Scorpion_Monstrous_Large] = GetWeightedAppearances(
                commonSkin: new[] { "Yellow exoskeleton", "Black exoskeleton" },
                uncommonSkin: new[] { "Brown exoskeleton", "Red exoskeleton", "White exoskeleton", "Orange exoskeleton" },
                allOther: new[] { "Front pincers and the tail with a stinger" });
            appearances[CreatureConstants.Scorpion_Monstrous_Huge] = GetWeightedAppearances(
                commonSkin: new[] { "Yellow exoskeleton", "Black exoskeleton" },
                uncommonSkin: new[] { "Brown exoskeleton", "Red exoskeleton", "White exoskeleton", "Orange exoskeleton" },
                allOther: new[] { "Front pincers and the tail with a stinger" });
            appearances[CreatureConstants.Scorpion_Monstrous_Gargantuan] = GetWeightedAppearances(
                commonSkin: new[] { "Yellow exoskeleton", "Black exoskeleton" },
                uncommonSkin: new[] { "Brown exoskeleton", "Red exoskeleton", "White exoskeleton", "Orange exoskeleton" },
                allOther: new[] { "Front pincers and the tail with a stinger" });
            appearances[CreatureConstants.Scorpion_Monstrous_Colossal] = GetWeightedAppearances(
                commonSkin: new[] { "Yellow exoskeleton", "Black exoskeleton" },
                uncommonSkin: new[] { "Brown exoskeleton", "Red exoskeleton", "White exoskeleton", "Orange exoskeleton" },
                allOther: new[] { "Front pincers and the tail with a stinger" });
            //Source: https://forgottenrealms.fandom.com/wiki/Tlincalli
            appearances[CreatureConstants.Scorpionfolk] = GetWeightedAppearances(
                allSkin: new[] { "... red skin on the scorpion half", "TODO HUMAN skin" },
                allHair: new[] { "Completely hairless" },
                allEyes: new[] { "Red eyes" },
                allOther: new[] { "Upper body of a human, lower body of a six-legged giant scorpion; Plates of armor made from bone cover the body from the upper abdomen down. Two large fingers and a thumb for hands. Human-like facial features except for segmented, insect-like eyes." });
            //Source: https://forgottenrealms.fandom.com/wiki/Sea_cat
            appearances[CreatureConstants.SeaCat] = GetWeightedAppearances(
                allSkin: new[] { "Tan fur and fish-like scales", "Green skin and reptilian scales" },
                allHair: new[] { "Mane of silky hair running from the top of its head and down its back to its tail flukes" },
                allOther: new[]
                {
                    "Head and forepaws of a lion, but a body and tail similar to those of a fish",
                    "Head and forepaws of a lion, but a body and tail similar to those of a porpoise",
                    "Head and forepaws of a lion, but a body and tail similar to those of a small whale",
                });
            //Source: https://forgottenrealms.fandom.com/wiki/Sea_hag
            appearances[CreatureConstants.SeaHag] = GetWeightedAppearances(
                allSkin: new[] { "Complexion of a rotting fish: pallid yellow skin with patches of green, slimy scales and bony protrusions" },
                allHair: new[] { "Long, limp, hair that resembles rancid seaweed" },
                allEyes: new[] { "Piscine eyes, devoid of life, with deep black pupils surrounded by red" },
                allOther: new[] { "Emaciated appearance, flesh was rife with warts and oozing cankers" });
            //Source: https://forgottenrealms.fandom.com/wiki/Shadow
            appearances[CreatureConstants.Shadow] = new[] { "A sentient shadow" };
            appearances[CreatureConstants.Shadow_Greater] = new[] { "A sentient shadow" };
            //Source: https://forgottenrealms.fandom.com/wiki/Shadow_mastiff
            appearances[CreatureConstants.ShadowMastiff] = new[] { "Body of a large dog, head more humanoid in shape but monstrous, with mouth full of vicious teeth, coat is smooth and all black" };
            //Source: https://forgottenrealms.fandom.com/wiki/Shambling_mound
            appearances[CreatureConstants.ShamblingMound] = new[] { "Vaguely man-shaped mound of rotting vegetation" };
            //Source: https://www.dimensions.com/element/blacktip-shark-carcharhinus-limbatus
            //https://www.enchantedlearning.com/subjects/sharks/anatomy/Size.shtml
            appearances[CreatureConstants.Shark_Medium] = new[] { "Blacktip reef shark", "Mako shark", "Pacific Angelshark", "Whitetip reef shark" };
            //Source: https://www.dimensions.com/element/thresher-shark
            appearances[CreatureConstants.Shark_Large] = new[] { "Thresher shark", "Blue shark", "Bull shark", "Galapagos shark", "Goblin shark", "Lemon shark",
                "Nurse shark", "Short-finned mako shark", "Tiger shark" };
            //Source: https://www.dimensions.com/element/great-white-shark
            appearances[CreatureConstants.Shark_Huge] = new[] { "Great white shark", "Great Hammerhead shark" };
            appearances[CreatureConstants.Shark_Dire] = new[] { "Megalodon" };
            //Source: https://forgottenrealms.fandom.com/wiki/Shield_guardian
            appearances[CreatureConstants.ShieldGuardian] = new[] { "Like a large stick figure made of wood with rocky appendages and metal parts" };
            //Source: https://forgottenrealms.fandom.com/wiki/Shocker_lizard
            appearances[CreatureConstants.ShockerLizard] = new[] { "Blue skin, with light blue belly" };
            //Source: https://forgottenrealms.fandom.com/wiki/Shrieker
            appearances[CreatureConstants.Shrieker] = new[] { "TODO VIOLET FUNGI, but did not have tentacles with which to poison prey" };
            //Source: https://forgottenrealms.fandom.com/wiki/Skum
            appearances[CreatureConstants.Skum] = new[] { "Resembles horrific hybrid of human and fish. Spiny frill on the back, bulbous eyes, and a tail." };
            //Source: https://forgottenrealms.fandom.com/wiki/Blue_slaad
            appearances[CreatureConstants.Slaad_Blue] = new[] { "Bipedal, roughly-humanoid toad with light, electric blue skin marked by grey streaks. Long snout. Two long, scimitar-like hooks of bone protruding from the back of both hands." };
            //Source: https://forgottenrealms.fandom.com/wiki/Red_slaad
            appearances[CreatureConstants.Slaad_Red] = new[] { "Bipedal, roughly-humanoid, almost neckless toad with huge, flat head. Skin is mostly dull red with specks of gray, lighter around the underside and darker along the back. Fingers and hands are strangely large and, like the feet, end in claws." };
            //Source: https://forgottenrealms.fandom.com/wiki/Green_slaad
            appearances[CreatureConstants.Slaad_Green] = new[] { "Bipedal, roughly-humanoid, gangly toad. Skin is mostly a pale, mottled green marked with grey streaks. High forehead, wide mouth, and long claws." };
            //Source: https://forgottenrealms.fandom.com/wiki/Gray_slaad
            appearances[CreatureConstants.Slaad_Gray] = new[] {
                "Frog-like humanoid. Long, clawed fingers. Gray skin is spotted.",
                "Frog-like humanoid. Long, clawed fingers. Evenly light-gray skin.",
            };
            //Source: https://forgottenrealms.fandom.com/wiki/Death_slaad
            appearances[CreatureConstants.Slaad_Death] = new[] {
                "Frog-like humanoid. Long, clawed fingers. Gray skin is spotted.",
                "Frog-like humanoid. Long, clawed fingers. Evenly light-gray skin.",
            };
            //Source: https://nationalzoo.si.edu/animals/green-tree-python
            //https://www.britannica.com/animal/boa-constrictor
            appearances[CreatureConstants.Snake_Constrictor] = new[] { "Diamond shaped heads, irregular scales, vibrant green color",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, ovals, and joined ovals, against a pale brown-and-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, ovals, and joined ovals, against a pale brown background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, ovals, and joined ovals, against a pale gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, against a pale brown-and-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, against a pale brown background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, against a pale gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of ovals, against a pale brown-and-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of ovals, against a pale brown background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of ovals, against a pale gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of joined ovals, against a pale brown-and-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of joined ovals, against a pale brown background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of joined ovals, against a pale gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, ovals, and joined ovals, against a pale brown-and-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, ovals, and joined ovals, against a pale brown background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, ovals, and joined ovals, against a pale gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, against a pale brown-and-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, against a pale brown background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, against a pale gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of ovals, against a pale brown-and-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of ovals, against a pale brown background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of ovals, against a pale gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of joined ovals, against a pale brown-and-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of joined ovals, against a pale brown background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of joined ovals, against a pale gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, ovals, and joined ovals, against a pale brown-and-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, ovals, and joined ovals, against a pale brown background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, ovals, and joined ovals, against a pale gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, against a pale brown-and-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, against a pale brown background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, against a pale gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of ovals, against a pale brown-and-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of ovals, against a pale brown background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of ovals, against a pale gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of joined ovals, against a pale brown-and-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of joined ovals, against a pale brown background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of joined ovals, against a pale gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, ovals, and joined ovals, against a pale brown-and-gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, ovals, and joined ovals, against a pale brown background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, ovals, and joined ovals, against a pale gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, against a pale brown-and-gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, against a pale brown background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, against a pale gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of ovals, against a pale brown-and-gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of ovals, against a pale brown background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of ovals, against a pale gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of joined ovals, against a pale brown-and-gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of joined ovals, against a pale brown background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of joined ovals, against a pale gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, ovals, and joined ovals, against a pale brown-and-gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, ovals, and joined ovals, against a pale brown background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, ovals, and joined ovals, against a pale gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, against a pale brown-and-gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, against a pale brown background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, against a pale gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of ovals, against a pale brown-and-gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of ovals, against a pale brown background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of ovals, against a pale gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of joined ovals, against a pale brown-and-gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of joined ovals, against a pale brown background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of joined ovals, against a pale gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, ovals, and joined ovals, against a pale brown-and-gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, ovals, and joined ovals, against a pale brown background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, ovals, and joined ovals, against a pale gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, against a pale brown-and-gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, against a pale brown background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, against a pale gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of ovals, against a pale brown-and-gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of ovals, against a pale brown background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of ovals, against a pale gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of joined ovals, against a pale brown-and-gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of joined ovals, against a pale brown background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of joined ovals, against a pale gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, ovals, and joined ovals, against a silvery-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, against a silvery-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of ovals, against a silvery-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of joined ovals, against a silvery-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, ovals, and joined ovals, against a silvery-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, against a silvery-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of ovals, against a silvery-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of joined ovals, against a silvery-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, ovals, and joined ovals, against a silvery-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, against a silvery-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of ovals, against a silvery-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of joined ovals, against a silvery-gray background.",
            };
            //Source: https://www.dimensions.com/element/burmese-python-python-bivittatus
            //https://myfwc.com/wildlifehabitats/nonnatives/python/identification/
            //https://nationalzoo.si.edu/animals/green-anaconda
            //https://wwf.panda.org/discover/our_focus/wildlife_practice/profiles/reptiles/anaconda/
            appearances[CreatureConstants.Snake_Constrictor_Giant] = new[] {
                "Tan scales with dark blotches along the back and sides. Dark brown blotches are irregularly shaped, fitting together like a puzzle. Dark wedges on top of head, below head, and behind the eye ",
                "Tan scales with dark blotches along the back and sides. Dark brown blotches are irregularly shaped, fitting together like a giraffe pattern. Dark wedges on top of head, below head, and behind the eye ",
                "Olive-green scales with dark oval spots along the spine and similar spots with yellow centers along the sides. The scales on the belly are yellow and black. Two dark stripes stretch from the eyes angling toward the jaw.",
                "Dark green scales with alternating oval black spots. Similar spots with yellow-ochre centres are along the sides of its body. It has a large narrow head. The eyes and nostrils are set on the top of its head.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, ovals, and joined ovals, against a pale brown-and-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, ovals, and joined ovals, against a pale brown background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, ovals, and joined ovals, against a pale gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, against a pale brown-and-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, against a pale brown background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, against a pale gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of ovals, against a pale brown-and-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of ovals, against a pale brown background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of ovals, against a pale gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of joined ovals, against a pale brown-and-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of joined ovals, against a pale brown background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of joined ovals, against a pale gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, ovals, and joined ovals, against a pale brown-and-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, ovals, and joined ovals, against a pale brown background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, ovals, and joined ovals, against a pale gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, against a pale brown-and-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, against a pale brown background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, against a pale gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of ovals, against a pale brown-and-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of ovals, against a pale brown background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of ovals, against a pale gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of joined ovals, against a pale brown-and-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of joined ovals, against a pale brown background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of joined ovals, against a pale gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, ovals, and joined ovals, against a pale brown-and-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, ovals, and joined ovals, against a pale brown background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, ovals, and joined ovals, against a pale gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, against a pale brown-and-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, against a pale brown background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, against a pale gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of ovals, against a pale brown-and-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of ovals, against a pale brown background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of ovals, against a pale gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of joined ovals, against a pale brown-and-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of joined ovals, against a pale brown background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of joined ovals, against a pale gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, ovals, and joined ovals, against a pale brown-and-gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, ovals, and joined ovals, against a pale brown background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, ovals, and joined ovals, against a pale gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, against a pale brown-and-gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, against a pale brown background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, against a pale gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of ovals, against a pale brown-and-gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of ovals, against a pale brown background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of ovals, against a pale gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of joined ovals, against a pale brown-and-gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of joined ovals, against a pale brown background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of joined ovals, against a pale gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, ovals, and joined ovals, against a pale brown-and-gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, ovals, and joined ovals, against a pale brown background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, ovals, and joined ovals, against a pale gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, against a pale brown-and-gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, against a pale brown background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, against a pale gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of ovals, against a pale brown-and-gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of ovals, against a pale brown background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of ovals, against a pale gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of joined ovals, against a pale brown-and-gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of joined ovals, against a pale brown background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of joined ovals, against a pale gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, ovals, and joined ovals, against a pale brown-and-gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, ovals, and joined ovals, against a pale brown background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, ovals, and joined ovals, against a pale gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, against a pale brown-and-gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, against a pale brown background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, against a pale gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of ovals, against a pale brown-and-gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of ovals, against a pale brown background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of ovals, against a pale gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of joined ovals, against a pale brown-and-gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of joined ovals, against a pale brown background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of joined ovals, against a pale gray background. Markings on the tail are red.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, ovals, and joined ovals, against a silvery-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of triangles, against a silvery-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of ovals, against a silvery-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown-and-black markings in the shape of joined ovals, against a silvery-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, ovals, and joined ovals, against a silvery-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of triangles, against a silvery-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of ovals, against a silvery-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep brown markings in the shape of joined ovals, against a silvery-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, ovals, and joined ovals, against a silvery-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of triangles, against a silvery-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of ovals, against a silvery-gray background.",
                "Long triangular head, with dark streaks from the eyes to the back of the jaw and another dark streak along the top. Deep black markings in the shape of joined ovals, against a silvery-gray background.", };
            //Source: https://www.dimensions.com/element/ribbon-snake-thamnophis-saurita
            //https://portal.ct.gov/-/media/DEEP/wildlife/pdf_files/outreach/fact_sheets/ribbonsnakepdf.pdf
            appearances[CreatureConstants.Snake_Viper_Tiny] = new[] {
                "Ribbonsnake: Boldly patterned with three yellow stripes on a reddish-brown background. A distinct dark band separates each side stripe from the belly. One stripe is centered on the body, while the other 2 stripes run down scale rows 3 and 4.",
                "Ribbonsnake: Boldly patterned with three yellow stripes on a dark reddish-brown background. A distinct dark band separates each side stripe from the belly. One stripe is centered on the body, while the other 2 stripes run down scale rows 3 and 4.",
                "Ribbonsnake: Boldly patterned with three yellow stripes on a reddish-black background. A distinct dark band separates each side stripe from the belly. One stripe is centered on the body, while the other 2 stripes run down scale rows 3 and 4.",
                "Ribbonsnake: Boldly patterned with three yellow stripes on a brownish-black background. A distinct dark band separates each side stripe from the belly. One stripe is centered on the body, while the other 2 stripes run down scale rows 3 and 4.",
                "Ribbonsnake: Boldly patterned with three yellow stripes on a black background. A distinct dark band separates each side stripe from the belly. One stripe is centered on the body, while the other 2 stripes run down scale rows 3 and 4.",
            };
            //Source: https://www.dimensions.com/element/copperhead-agkistrodon-contortrix
            //https://www.chesapeakebay.net/discover/field-guide/entry/copperheads
            appearances[CreatureConstants.Snake_Viper_Small] = new[] {
                "Copperhead: Broad, unmarked, copper-colored head and a reddish-tan colored body. Hourglass-shaped darker marks on its back. Underside is a pinkish color."
            };
            //Source: https://www.dimensions.com/element/western-diamondback-rattlesnake-crotalus-atrox 
            //https://www.desertmuseum.org/kids/oz/long-fact-sheets/Diamondback%20Rattlesnake.php
            appearances[CreatureConstants.Snake_Viper_Medium] = new[] {
                "Diamondback Rattlesnake: Triangular shaped head. Two dark diagonal lines on each side of its face run from the eyes to its jaws. Dark diamond-shaped pattern along its back. The tail has black and white bands just above the rattles."
            };
            //Source: https://www.dimensions.com/element/black-mamba-dendroaspis-polylepis 
            //https://en.wikipedia.org/wiki/Black_mamba#Description
            //https://www.pretoriazoo.org/animals/green-mamba/
            appearances[CreatureConstants.Snake_Viper_Large] = GetWeightedAppearances(
                commonSkin: new[] { "Olive-colored scales, greyish-white underbelly", "Yellowish-brown scales, greyish-white underbelly",
                    "Khaki-colored scales, greyish-white underbelly", "Gunmetal-colored scales, greyish-white underbelly",
                    "Bright green scales laid like paving stones, yellow belly",
                    "Bright green scales laid like paving stones, light green belly",
                    "Bright green scales laid like paving stones, yellow-green belly",
                    "Bright green scales laid like paving stones, light yellow-green belly",
                },
                uncommonSkin: new[] { "Olive-colored scales with a purplish sheen, greyish-white underbelly",
                        "Yellowish-brown scales with a purplish sheen, greyish-white underbelly",
                        "Khaki-colored scales with a purplish sheen, greyish-white underbelly",
                        "Gunmetal-colored scales with a purplish sheen, greyish-white underbelly",
                    "Olive-colored scales with dark mottling toward the posterior, greyish-white underbelly",
                        "Yellowish-brown scales with dark mottling toward the posterior, greyish-white underbelly",
                        "Khaki-colored scales with dark mottling toward the posterior, greyish-white underbelly",
                        "Gunmetal-colored scales with dark mottling toward the posterior, greyish-white underbelly",
                    "Olive-colored scales with dark mottling toward the posterior in the form of diagonal crossbands, greyish-white underbelly",
                        "Yellowish-brown scales with dark mottling toward the posterior in the form of diagonal crossbands, greyish-white underbelly",
                        "Khaki-colored scales with dark mottling toward the posterior in the form of diagonal crossbands, greyish-white underbelly",
                        "Gunmetal-colored scales with dark mottling toward the posterior in the form of diagonal crossbands, greyish-white underbelly",
                },
                rareSkin: new[] { "Black scales, greyish-white underbelly", "Black scales with a purplish sheen, greyish-white underbelly",
                    "Olive-colored scales with a purplish sheen and dark mottling toward the posterior, greyish-white underbelly",
                        "Yellowish-brown scales with a purplish sheen and dark mottling toward the posterior, greyish-white underbelly",
                        "Khaki-colored scales with a purplish sheen and dark mottling toward the posterior, greyish-white underbelly",
                        "Gunmetal-colored scales with a purplish sheen and dark mottling toward the posterior, greyish-white underbelly",
                    "Olive-colored scales with a purplish sheen and dark mottling toward the posterior in the form of diagonal crossbands, greyish-white underbelly",
                        "Yellowish-brown scales with a purplish sheen and dark mottling toward the posterior in the form of diagonal crossbands, greyish-white underbelly",
                        "Khaki-colored scales with a purplish sheen and dark mottling toward the posterior in the form of diagonal crossbands, greyish-white underbelly",
                        "Gunmetal-colored scales with a purplish sheen and dark mottling toward the posterior in the form of diagonal crossbands, greyish-white underbelly",
                },
                allEyes: new[] { "Medium-sized eyes, grayish-brown irises surrounded by silvery-white",
                    "Medium-sized eyes, dark black irises surrounded by silvery-white",
                    "Medium-sized eyes, dark gray irises surrounded by silvery-white", "Medium-sized eyes, gray irises surrounded by silvery-white",
                    "Medium-sized eyes, light gray irises surrounded by silvery-white", "Medium-sized eyes, brown irises surrounded by silvery-white",
                    "Medium-sized eyes, dark brown irises surrounded by silvery-white",
                    "Medium-sized eyes, dark grayish-brown irises surrounded by silvery-white",
                    "Medium-sized eyes, grayish-brown irises surrounded by yellow",
                    "Medium-sized eyes, dark black irises surrounded by yellow",
                    "Medium-sized eyes, dark gray irises surrounded by yellow", "Medium-sized eyes, gray irises surrounded by yellow",
                    "Medium-sized eyes, light gray irises surrounded by yellow", "Medium-sized eyes, brown irises surrounded by yellow",
                    "Medium-sized eyes, dark brown irises surrounded by yellow",
                    "Medium-sized eyes, dark grayish-brown irises surrounded by yellow"},
                allOther: new[] { "Mamba: Coffin-shaped head with somewhat-pronounced brow ridge" }
            );
            //Source: https://www.dimensions.com/element/king-cobra-ophiophagus-hannah
            //https://nationalzoo.si.edu/animals/king-cobra
            appearances[CreatureConstants.Snake_Viper_Huge] = new[]
            {
                "King Cobra: Yellow scales. The belly is uniform in color. The throat is light yellow.",
                "King Cobra: Yellow scales. The belly is ornamented with bars. The throat is light yellow.",
                "King Cobra: Yellow scales. The belly is uniform in color. The throat is cream-colored.",
                "King Cobra: Yellow scales. The belly is ornamented with bars. The throat is cream-colored.",
                "King Cobra: Green scales. The belly is uniform in color. The throat is light yellow.",
                "King Cobra: Green scales. The belly is ornamented with bars. The throat is light yellow.",
                "King Cobra: Green scales. The belly is uniform in color. The throat is cream-colored.",
                "King Cobra: Green scales. The belly is ornamented with bars. The throat is cream-colored.",
                "King Cobra: Brown scales. The belly is uniform in color. The throat is light yellow.",
                "King Cobra: Brown scales. The belly is ornamented with bars. The throat is light yellow.",
                "King Cobra: Brown scales. The belly is uniform in color. The throat is cream-colored.",
                "King Cobra: Brown scales. The belly is ornamented with bars. The throat is cream-colored.",
                "King Cobra: Black scales. The belly is uniform in color. The throat is light yellow.",
                "King Cobra: Black scales. The belly is ornamented with bars. The throat is light yellow.",
                "King Cobra: Black scales. The belly is uniform in color. The throat is cream-colored.",
                "King Cobra: Black scales. The belly is ornamented with bars. The throat is cream-colored.",
                "King Cobra: Yellow scales with darker yellowish crossbars. The belly is uniform in color. The throat is light yellow.",
                "King Cobra: Yellow scales with darker yellowish crossbars. The belly is uniform in color. The throat is cream-colored.",
                "King Cobra: Yellow scales with darker yellowish crossbars. The belly is ornamented with bars. The throat is light yellow.",
                "King Cobra: Yellow scales with darker yellowish crossbars. The belly is ornamented with bars. The throat is cream-colored.",
                "King Cobra: Yellow scales with darker yellowish chevrons. The belly is uniform in color. The throat is light yellow.",
                "King Cobra: Yellow scales with darker yellowish chevrons. The belly is uniform in color. The throat is cream-colored.",
                "King Cobra: Yellow scales with darker yellowish chevrons. The belly is ornamented with bars. The throat is light yellow.",
                "King Cobra: Yellow scales with darker yellowish chevrons. The belly is ornamented with bars. The throat is cream-colored.",
                "King Cobra: Yellow scales with lighter yellowish crossbars. The belly is uniform in color. The throat is light yellow.",
                "King Cobra: Yellow scales with lighter yellowish crossbars. The belly is uniform in color. The throat is cream-colored.",
                "King Cobra: Yellow scales with lighter yellowish crossbars. The belly is ornamented with bars. The throat is light yellow.",
                "King Cobra: Yellow scales with lighter yellowish crossbars. The belly is ornamented with bars. The throat is cream-colored.",
                "King Cobra: Yellow scales with lighter yellowish chevrons. The belly is uniform in color. The throat is light yellow.",
                "King Cobra: Yellow scales with lighter yellowish chevrons. The belly is uniform in color. The throat is cream-colored.",
                "King Cobra: Yellow scales with lighter yellowish chevrons. The belly is ornamented with bars. The throat is light yellow.",
                "King Cobra: Yellow scales with lighter yellowish chevrons. The belly is ornamented with bars. The throat is cream-colored.",
                "King Cobra: Yellow scales with white crossbars. The belly is uniform in color. The throat is light yellow.",
                "King Cobra: Yellow scales with white crossbars. The belly is uniform in color. The throat is cream-colored.",
                "King Cobra: Yellow scales with white crossbars. The belly is ornamented with bars. The throat is light yellow.",
                "King Cobra: Yellow scales with white crossbars. The belly is ornamented with bars. The throat is cream-colored.",
                "King Cobra: Yellow scales with white chevrons. The belly is uniform in color. The throat is light yellow.",
                "King Cobra: Yellow scales with white chevrons. The belly is uniform in color. The throat is cream-colored.",
                "King Cobra: Yellow scales with white chevrons. The belly is ornamented with bars. The throat is light yellow.",
                "King Cobra: Yellow scales with white chevrons. The belly is ornamented with bars. The throat is cream-colored.",
                "King Cobra: Green scales with yellowish crossbars. The belly is uniform in color. The throat is light yellow.",
                "King Cobra: Green scales with yellowish crossbars. The belly is uniform in color. The throat is cream-colored.",
                "King Cobra: Green scales with yellowish crossbars. The belly is ornamented with bars. The throat is light yellow.",
                "King Cobra: Green scales with yellowish crossbars. The belly is ornamented with bars. The throat is cream-colored.",
                "King Cobra: Green scales with yellowish chevrons. The belly is uniform in color. The throat is light yellow.",
                "King Cobra: Green scales with yellowish chevrons. The belly is uniform in color. The throat is cream-colored.",
                "King Cobra: Green scales with yellowish chevrons. The belly is ornamented with bars. The throat is light yellow.",
                "King Cobra: Green scales with yellowish chevrons. The belly is ornamented with bars. The throat is cream-colored.",
                "King Cobra: Green scales with white crossbars. The belly is uniform in color. The throat is light yellow.",
                "King Cobra: Green scales with white crossbars. The belly is uniform in color. The throat is cream-colored.",
                "King Cobra: Green scales with white crossbars. The belly is ornamented with bars. The throat is light yellow.",
                "King Cobra: Green scales with white crossbars. The belly is ornamented with bars. The throat is cream-colored.",
                "King Cobra: Green scales with white chevrons. The belly is uniform in color. The throat is light yellow.",
                "King Cobra: Green scales with white chevrons. The belly is uniform in color. The throat is cream-colored.",
                "King Cobra: Green scales with white chevrons. The belly is ornamented with bars. The throat is light yellow.",
                "King Cobra: Green scales with white chevrons. The belly is ornamented with bars. The throat is cream-colored.",
                "King Cobra: Brown scales with yellowish crossbars. The belly is uniform in color. The throat is light yellow.",
                "King Cobra: Brown scales with yellowish crossbars. The belly is uniform in color. The throat is cream-colored.",
                "King Cobra: Brown scales with yellowish crossbars. The belly is ornamented with bars. The throat is light yellow.",
                "King Cobra: Brown scales with yellowish crossbars. The belly is ornamented with bars. The throat is cream-colored.",
                "King Cobra: Brown scales with yellowish chevrons. The belly is uniform in color. The throat is light yellow.",
                "King Cobra: Brown scales with yellowish chevrons. The belly is uniform in color. The throat is cream-colored.",
                "King Cobra: Brown scales with yellowish chevrons. The belly is ornamented with bars. The throat is light yellow.",
                "King Cobra: Brown scales with yellowish chevrons. The belly is ornamented with bars. The throat is cream-colored.",
                "King Cobra: Brown scales with white crossbars. The belly is uniform in color. The throat is light yellow.",
                "King Cobra: Brown scales with white crossbars. The belly is uniform in color. The throat is cream-colored.",
                "King Cobra: Brown scales with white crossbars. The belly is ornamented with bars. The throat is light yellow.",
                "King Cobra: Brown scales with white crossbars. The belly is ornamented with bars. The throat is cream-colored.",
                "King Cobra: Brown scales with white chevrons. The belly is uniform in color. The throat is light yellow.",
                "King Cobra: Brown scales with white chevrons. The belly is uniform in color. The throat is cream-colored.",
                "King Cobra: Brown scales with white chevrons. The belly is ornamented with bars. The throat is light yellow.",
                "King Cobra: Brown scales with white chevrons. The belly is ornamented with bars. The throat is cream-colored.",
                "King Cobra: Black scales with yellowish crossbars. The belly is uniform in color. The throat is light yellow.",
                "King Cobra: Black scales with yellowish crossbars. The belly is uniform in color. The throat is cream-colored.",
                "King Cobra: Black scales with yellowish crossbars. The belly is ornamented with bars. The throat is light yellow.",
                "King Cobra: Black scales with yellowish crossbars. The belly is ornamented with bars. The throat is cream-colored.",
                "King Cobra: Black scales with yellowish chevrons. The belly is uniform in color. The throat is light yellow.",
                "King Cobra: Black scales with yellowish chevrons. The belly is uniform in color. The throat is cream-colored.",
                "King Cobra: Black scales with yellowish chevrons. The belly is ornamented with bars. The throat is light yellow.",
                "King Cobra: Black scales with yellowish chevrons. The belly is ornamented with bars. The throat is cream-colored.",
                "King Cobra: Black scales with white crossbars. The belly is uniform in color. The throat is light yellow.",
                "King Cobra: Black scales with white crossbars. The belly is uniform in color. The throat is cream-colored.",
                "King Cobra: Black scales with white crossbars. The belly is ornamented with bars. The throat is light yellow.",
                "King Cobra: Black scales with white crossbars. The belly is ornamented with bars. The throat is cream-colored.",
                "King Cobra: Black scales with white chevrons. The belly is uniform in color. The throat is light yellow.",
                "King Cobra: Black scales with white chevrons. The belly is uniform in color. The throat is cream-colored.",
                "King Cobra: Black scales with white chevrons. The belly is ornamented with bars. The throat is light yellow.",
                "King Cobra: Black scales with white chevrons. The belly is ornamented with bars. The throat is cream-colored.",
            };
            //Source: https://forgottenrealms.fandom.com/wiki/Specter
            appearances[CreatureConstants.Spectre] = new[]
            {
                "Humanoid, with a mostly transparent and faintly luminous form.",
                "Humanoid, with a mostly transparent and faintly luminous form. The injuries that caused its violent death are visible.",
            };
            //Source: http://www.wlgf.org/hunting_spiders.html
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Tiny] = new[] { "Common house spider", "Brown recluse spider", "Southern black widow",
                "Hobo spider", "Forked pirate spider", "Black house spider", "Western black widow", "Black-footed yellow sac spider",
                "Brown widow", "Woodlouse spider", "Redback spider", "False widow", "Giant house spider", "Rabid wolf spider", "Goliath birdeater", "Noble false widow",
                "Bold jumper"
            };
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Small
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Small] = new[] { "Common house spider", "Brown recluse spider", "Southern black widow",
                "Hobo spider", "Forked pirate spider", "Black house spider", "Western black widow", "Black-footed yellow sac spider",
                "Brown widow", "Woodlouse spider", "Redback spider", "False widow", "Giant house spider", "Rabid wolf spider", "Goliath birdeater", "Noble false widow",
                "Bold jumper"
            };
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Medium
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Medium] = new[] { "Common house spider", "Brown recluse spider", "Southern black widow",
                "Hobo spider", "Forked pirate spider", "Black house spider", "Western black widow", "Black-footed yellow sac spider",
                "Brown widow", "Woodlouse spider", "Redback spider", "False widow", "Giant house spider", "Rabid wolf spider", "Goliath birdeater", "Noble false widow",
                "Bold jumper"
            };
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Large
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Large] = new[] { "Common house spider", "Brown recluse spider", "Southern black widow",
                "Hobo spider", "Forked pirate spider", "Black house spider", "Western black widow", "Black-footed yellow sac spider",
                "Brown widow", "Woodlouse spider", "Redback spider", "False widow", "Giant house spider", "Rabid wolf spider", "Goliath birdeater", "Noble false widow",
                "Bold jumper"
            };
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Huge
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Huge] = new[] { "Common house spider", "Brown recluse spider", "Southern black widow",
                "Hobo spider", "Forked pirate spider", "Black house spider", "Western black widow", "Black-footed yellow sac spider",
                "Brown widow", "Woodlouse spider", "Redback spider", "False widow", "Giant house spider", "Rabid wolf spider", "Goliath birdeater", "Noble false widow",
                "Bold jumper"
            };
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Gargantuan
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan] = new[] { "Common house spider", "Brown recluse spider", "Southern black widow",
                "Hobo spider", "Forked pirate spider", "Black house spider", "Western black widow", "Black-footed yellow sac spider",
                "Brown widow", "Woodlouse spider", "Redback spider", "False widow", "Giant house spider", "Rabid wolf spider", "Goliath birdeater", "Noble false widow",
                "Bold jumper"
            };
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Colossal
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Colossal] = new[] { "Common house spider", "Brown recluse spider", "Southern black widow",
                "Hobo spider", "Forked pirate spider", "Black house spider", "Western black widow", "Black-footed yellow sac spider",
                "Brown widow", "Woodlouse spider", "Redback spider", "False widow", "Giant house spider", "Rabid wolf spider", "Goliath birdeater", "Noble false widow",
                "Bold jumper"
            };
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Tiny
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny] = new[] { "European garden spider", "Yellow garden spider", "Golden silk orb-weaver",
                "Sydney funnel-web spider", "Filmy dome spider", "Diving bell spider", "Wheel spider", "Wasp spider", "Barn spider", "Cat-faced spider" };
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Small
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Small] = new[] { "European garden spider", "Yellow garden spider", "Golden silk orb-weaver",
                "Sydney funnel-web spider", "Filmy dome spider", "Diving bell spider", "Wheel spider", "Wasp spider", "Barn spider", "Cat-faced spider" };
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Medium
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Medium] = new[] { "European garden spider", "Yellow garden spider", "Golden silk orb-weaver",
                "Sydney funnel-web spider", "Filmy dome spider", "Diving bell spider", "Wheel spider", "Wasp spider", "Barn spider", "Cat-faced spider" };
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Large
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Large] = new[] { "European garden spider", "Yellow garden spider", "Golden silk orb-weaver",
                "Sydney funnel-web spider", "Filmy dome spider", "Diving bell spider", "Wheel spider", "Wasp spider", "Barn spider", "Cat-faced spider" };
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Huge
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Huge] = new[] { "European garden spider", "Yellow garden spider", "Golden silk orb-weaver",
                "Sydney funnel-web spider", "Filmy dome spider", "Diving bell spider", "Wheel spider", "Wasp spider", "Barn spider", "Cat-faced spider" };
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Gargantuan
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan] = new[] { "European garden spider", "Yellow garden spider", "Golden silk orb-weaver",
                "Sydney funnel-web spider", "Filmy dome spider", "Diving bell spider", "Wheel spider", "Wasp spider", "Barn spider", "Cat-faced spider" };
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Colossal
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal] = new[] { "European garden spider", "Yellow garden spider", "Golden silk orb-weaver",
                "Sydney funnel-web spider", "Filmy dome spider", "Diving bell spider", "Wheel spider", "Wasp spider", "Barn spider", "Cat-faced spider" };
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            appearances[CreatureConstants.Spider_Swarm] = new[] { "1,500 spiders" };
            //Source: https://www.d20srd.org/srd/monsters/spiderEater.htm
            appearances[CreatureConstants.SpiderEater] = new[] { "Resembles a wasp the size of a horse, but with the head of a spider and two long appendages ending in pincers." };
            //Source: https://www.google.com/search?q=squid+species
            appearances[CreatureConstants.Squid] = new[] { "Humboldt squid", "European squid", "Japanese flying squid", "Caribbean reef squid", "Bigfin reef squid",
                "Vampire squid", "Longfin inshore squid", "Neon flying squid", "Robust clubhook squid", "Angel clubhook squid", "European flying squid"
            };
            appearances[CreatureConstants.Squid_Giant] = new[] { "Colossal squid" };
            //Source: https://www.uky.edu/Ag/CritterFiles/casefile/insects/beetles/hercules/hercules.htm
            appearances[CreatureConstants.StagBeetle_Giant] = GetWeightedAppearances(
                commonSkin: new[] { "Green exoskeleton mottled with black spots", "Gray exoskeleton mottled with black spots",
                    "Tan exoskeleton mottled with black spots", "Black exoskeleton", "Black iridescent exoskeleton" },
                rareSkin: new[] { "Green exoskeleton", "Gray exoskeleton", "Tan exoskeleton" }
            );
            //Source: https://forgottenrealms.fandom.com/wiki/Stirge
            appearances[CreatureConstants.Stirge] = new[] {
                "Resembles a monstrous cross between a large bat and an oversized mosquito. Short and furry body in shades of rusty red. Yellowish eyes. Wings are membranous and bat-like, interlaced with thin-walled blood vessels. Four eight-jointed legs ending in sharp pincers. Long, sharp needle-like proboscis, pink at the tip and fades to grey at the base.",
                "Resembles a monstrous cross between a large bat and an oversized mosquito. Short and furry body in shades of reddish brown. Yellowish eyes. Wings are membranous and bat-like, interlaced with thin-walled blood vessels. Four eight-jointed legs ending in sharp pincers. Long, sharp needle-like proboscis, pink at the tip and fades to grey at the base.",
                "Resembles a monstrous cross between a large bat and an oversized mosquito. Short body covered in feathers in shades of rusty red. Yellowish eyes. Wings are membranous and bat-like, interlaced with thin-walled blood vessels. Four eight-jointed legs ending in sharp pincers. Long, sharp needle-like proboscis, pink at the tip and fades to grey at the base.",
                "Resembles a monstrous cross between a large bat and an oversized mosquito. Short body covered in feathers in shades of reddish brown. Yellowish eyes. Wings are membranous and bat-like, interlaced with thin-walled blood vessels. Four eight-jointed legs ending in sharp pincers. Long, sharp needle-like proboscis, pink at the tip and fades to grey at the base.",
                "Resembles a monstrous cross between an oversized mosquito and a bird. Short and furry body in shades of rusty red. Yellowish eyes. Wings are membranous and bat-like, interlaced with thin-walled blood vessels. Four eight-jointed legs ending in sharp pincers. Long, sharp needle-like proboscis, pink at the tip and fades to grey at the base.",
                "Resembles a monstrous cross between an oversized mosquito and a bird. Short and furry body in shades of reddish brown. Yellowish eyes. Wings are membranous and bat-like, interlaced with thin-walled blood vessels. Four eight-jointed legs ending in sharp pincers. Long, sharp needle-like proboscis, pink at the tip and fades to grey at the base.",
                "Resembles a monstrous cross between an oversized mosquito and a bird. Short body covered in feathers in shades of rusty red. Yellowish eyes. Wings are membranous and bat-like, interlaced with thin-walled blood vessels. Four eight-jointed legs ending in sharp pincers. Long, sharp needle-like proboscis, pink at the tip and fades to grey at the base.",
                "Resembles a monstrous cross between an oversized mosquito and a bird. Short body covered in feathers in shades of reddish brown. Yellowish eyes. Wings are membranous and bat-like, interlaced with thin-walled blood vessels. Four eight-jointed legs ending in sharp pincers. Long, sharp needle-like proboscis, pink at the tip and fades to grey at the base.",
                "Resembles a monstrous cross between a large bat and an oversized mosquito. Short and furry body in shades of rusty red. Yellowish eyes. Wings are membranous and bat-like, interlaced with thin-walled blood vessels. Four eight-jointed legs ending in sharp pincers of a similar yellowish hue to the eyes. Long, sharp needle-like proboscis, pink at the tip and fades to grey at the base.",
                "Resembles a monstrous cross between a large bat and an oversized mosquito. Short and furry body in shades of reddish brown. Yellowish eyes. Wings are membranous and bat-like, interlaced with thin-walled blood vessels. Four eight-jointed legs ending in sharp pincers of a similar yellowish hue to the eyes. Long, sharp needle-like proboscis, pink at the tip and fades to grey at the base.",
                "Resembles a monstrous cross between a large bat and an oversized mosquito. Short body covered in feathers in shades of rusty red. Yellowish eyes. Wings are membranous and bat-like, interlaced with thin-walled blood vessels. Four eight-jointed legs ending in sharp pincers of a similar yellowish hue to the eyes. Long, sharp needle-like proboscis, pink at the tip and fades to grey at the base.",
                "Resembles a monstrous cross between a large bat and an oversized mosquito. Short body covered in feathers in shades of reddish brown. Yellowish eyes. Wings are membranous and bat-like, interlaced with thin-walled blood vessels. Four eight-jointed legs ending in sharp pincers of a similar yellowish hue to the eyes. Long, sharp needle-like proboscis, pink at the tip and fades to grey at the base.",
                "Resembles a monstrous cross between an oversized mosquito and a bird. Short and furry body in shades of rusty red. Yellowish eyes. Wings are membranous and bat-like, interlaced with thin-walled blood vessels. Four eight-jointed legs ending in sharp pincers of a similar yellowish hue to the eyes. Long, sharp needle-like proboscis, pink at the tip and fades to grey at the base.",
                "Resembles a monstrous cross between an oversized mosquito and a bird. Short and furry body in shades of reddish brown. Yellowish eyes. Wings are membranous and bat-like, interlaced with thin-walled blood vessels. Four eight-jointed legs ending in sharp pincers of a similar yellowish hue to the eyes. Long, sharp needle-like proboscis, pink at the tip and fades to grey at the base.",
                "Resembles a monstrous cross between an oversized mosquito and a bird. Short body covered in feathers in shades of rusty red. Yellowish eyes. Wings are membranous and bat-like, interlaced with thin-walled blood vessels. Four eight-jointed legs ending in sharp pincers of a similar yellowish hue to the eyes. Long, sharp needle-like proboscis, pink at the tip and fades to grey at the base.",
                "Resembles a monstrous cross between an oversized mosquito and a bird. Short body covered in feathers in shades of reddish brown. Yellowish eyes. Wings are membranous and bat-like, interlaced with thin-walled blood vessels. Four eight-jointed legs ending in sharp pincers of a similar yellowish hue to the eyes. Long, sharp needle-like proboscis, pink at the tip and fades to grey at the base.",
            };
            //Source: https://forgottenrealms.fandom.com/wiki/Succubus
            appearances[CreatureConstants.Succubus] = GetWeightedAppearances(
                allSkin: new[] { "TODO HUMAN flawless skin" },
                allHair: new[] { "Red hair", "Raven-black hair" },
                allEyes: new[] { "TODO HUMAN smoldering eyes" },
                commonOther: new[] { "Clawed fingers, large black bat-like wings", "Clawed fingers, large dark red bat-like wings",
                    "Clawed fingers, large dark gray bat-like wings", "Clawed fingers, large dark green bat-like wings", "Clawed fingers, large dark blue bat-like wings",
                    "Clawed fingers, large dark purple bat-like wings", "Clawed fingers, large reddish bat-like wings"
                },
                uncommonOther: new[] { "Clawed fingers, large black bat-like wings, small horns", "Clawed fingers, large dark red bat-like wings, small horns",
                    "Clawed fingers, large dark gray bat-like wings, small horns", "Clawed fingers, large dark green bat-like wings, small horns",
                    "Clawed fingers, large dark blue bat-like wings, small horns",
                    "Clawed fingers, large dark purple bat-like wings, small horns", "Clawed fingers, large reddish bat-like wings, small horns",
                    "Clawed fingers, large black bat-like wings, tail", "Clawed fingers, large dark red bat-like wings, tail",
                    "Clawed fingers, large dark gray bat-like wings, tail", "Clawed fingers, large dark green bat-like wings, tail",
                    "Clawed fingers, large dark blue bat-like wings, tail",
                    "Clawed fingers, large dark purple bat-like wings, tail", "Clawed fingers, large reddish bat-like wings, tail",
                    "Clawed fingers, large black bat-like wings, small horns, tail", "Clawed fingers, large dark red bat-like wings, small horns, tail",
                    "Clawed fingers, large dark gray bat-like wings, small horns, tail", "Clawed fingers, large dark green bat-like wings, small horns, tail",
                    "Clawed fingers, large dark blue bat-like wings, small horns, tail",
                    "Clawed fingers, large dark purple bat-like wings, small horns, tail", "Clawed fingers, large reddish bat-like wings, small horns, tail"
                }
            );
            //Source: https://forgottenrealms.fandom.com/wiki/Tarrasque
            appearances[CreatureConstants.Tarrasque] = new[] { "Two long horns extend from its forehead; thick carapace; mighty tail; many spikes cover its large body; wide, toothy maw. Two small eyes." };
            //Source: https://forgottenrealms.fandom.com/wiki/Tendriculos
            appearances[CreatureConstants.Tendriculos] = new[] { "Mound of vegetation, supported by various branches and vines. Large opening filled with sharp branches and horns, acting sort of like teeth." };
            //Source: https://forgottenrealms.fandom.com/wiki/Thoqqua
            appearances[CreatureConstants.Thoqqua] = new[] { "Worm-like, made of earth and fire. Brown and red skin. Glowing, molten-hot beak." };
            //Source: https://forgottenrealms.fandom.com/wiki/Tiefling
            appearances[CreatureConstants.Tiefling] = GetWeightedAppearances(
                commonSkin: new[] { "TODO HUMAN skin" },
                uncommonSkin: new[] { "Light red skin", "Red skin", "Dark red skin",
                    "TODO HUMAN furred skin",
                    "TODO HUMAN leathery skin",
                    "TODO HUMAN scaly skin",
                    "TODO HUMAN skin, unusually warm",
                },
                rareSkin: new[] { "Light red skin, unusually warm", "Red skin, unusually warm", "Dark red skin, unusually warm",
                    "TODO HUMAN furred skin, unusually warm",
                    "TODO HUMAN leathery skin, unusually warm",
                    "TODO HUMAN scaly skin, unusually warm",
                },
                commonHair: new[] { "Red hair", "Brown hair", "Black hair", "Dark blue hair", "Purple hair" },
                uncommonHair: new[] { "Pale white hair" },
                commonEyes: new[] { "TODO HUMAN eyes" },
                uncommonEyes: new[] { "Solid black eyes with no pupils", "Solid gold eyes with no pupils", "Solid red eyes with no pupils",
                    "Solid silver eyes with no pupils", "Solid white eyes with no pupils",
                    "TODO HUMAN feline eyes" },
                commonOther: new[] { "Horns", "Prehensile tail", "Non-prehensile tail", "Pointed teeth" },
                uncommonOther: new[] { "No other demonic qualities",
                    "Sulfurous odor", "Cloven feet",
                    "Horns and prehensile tail", "Horns and non-prehensile tail", "Horns and pointed teeth",
                    "Prehensile tail and pointed teeth", "Non-prehensile tail and pointed teeth",
                    "Forked tongue", "Odor of brimstone", "Goat-like legs", "Hooves",
                    "Antlers",
                },
                rareOther: new[] { "Horns, prehensile tail, and pointed teeth", "Horns, non-prehensile tail, and pointed teeth",
                    "Horns and sulfurous odor", "Horns and cloven feet", "Horns, sulfurous odor, and cloven feet",
                    "Prehensile tail and sulfurous odor", "Prehensile tail and cloven feet", "Prehensile tail, sulfurous odor, and cloven feet",
                    "Non-prehensile tail and sulfurous odor", "Non-prehensile tail and cloven feet", "Non-prehensile tail, sulfurous odor, and cloven feet",
                    "Pointed teeth and sulfurous odor", "Pointed teeth and cloven feet", "Pointed teeth, sulfurous odor, and cloven feet",
                    "Sulfurous oder and cloven feet",
                    "Horns, prehensile tail, and sulfurous odor", "Horns, prehensile tail, and cloven feet", "Horns, prehensile tail, sulfurous odor, and cloven feet",
                    "Horns, non-prehensile tail, and sulfurous odor", "Horns, non-prehensile tail, and cloven feet",
                        "Horns, non-prehensile tail, sulfurous odor, and cloven feet",
                    "Horns, pointed teeth, and sulfurous odor", "Horns, pointed teeth, and cloven feet", "Horns, pointed teeth, sulfurous odor, and cloven feet",
                    "Prehensile tail, pointed teeth, and sulfurous odor", "Prehensile tail, pointed teeth, and cloven feet",
                        "Prehensile tail, pointed teeth, sulfurous odor, and cloven feet",
                    "Non-prehensile tail, pointed teeth, and sulfurous odor", "Non-prehensile tail, pointed teeth, and cloven feet",
                        "Non-prehensile tail, pointed teeth, sulfurous odor, and cloven feet",
                    "Forked tongue and odor of brimstone", "Forked tongue and goat-like legs", "Forked tongue and hooves",
                    "Odor of brimstone and goat-like legs", "Odor of brimstone and hooves",
                    "Forked tongue, odor of brimstone, and goat-like legs", "Forked tongue, odor of brimstone, and hooves",
                    "Antlers and prehensile tail", "Antlers and non-prehensile tail", "Antlers and pointed teeth",
                    "Antlers, prehensile tail, and pointed teeth", "Antlers, non-prehensile tail, and pointed teeth",
                    "Antlers and sulfurous odor", "Antlers and cloven feet", "Antlers, sulfurous odor, and cloven feet",
                    "Antlers, prehensile tail, and sulfurous odor", "Antlers, prehensile tail, and cloven feet",
                        "Antlers, prehensile tail, sulfurous odor, and cloven feet",
                    "Antlers, non-prehensile tail, and sulfurous odor", "Antlers, non-prehensile tail, and cloven feet",
                        "Antlers, non-prehensile tail, sulfurous odor, and cloven feet",
                    "Antlers, pointed teeth, and sulfurous odor", "Antlers, pointed teeth, and cloven feet", "Antlers, pointed teeth, sulfurous odor, and cloven feet",
                }
            );
            //Source: https://nationalzoo.si.edu/animals/tiger
            appearances[CreatureConstants.Tiger] = new[] { "Reddish-orange coat with prominent black stripes, white bellies and white spots on the ears." };
            //https://forgottenrealms.fandom.com/wiki/Dire_tiger
            appearances[CreatureConstants.Tiger_Dire] = new[] { "Reddish-orange coat with prominent black stripes, white bellies and white spots on the ears, pair of huge fangs jutting from powerful jaws" };
            //Source: https://forgottenrealms.fandom.com/wiki/Titan
            appearances[CreatureConstants.Titan] = new[] { "TODO HUMAN all appearances" };
            //Source: https://www.woodlandtrust.org.uk/trees-woods-and-wildlife/animals/reptiles-and-amphibians/common-toad/
            appearances[CreatureConstants.Toad] = GetWeightedAppearances(
                commonSkin: new[] { "Olive brown, dry, warty skin", "Green, dry, warty skin", "Dark brown, dry, warty skin", "Gray, dry, warty skin" },
                uncommonSkin: new[] { "Olive brown, dry, warty skin with dark markings", "Green, dry, warty skin with dark markings",
                    "Dark brown, dry, warty skinn with dark markings", "Gray, dry, warty skin with dark markings" },
                allEyes: new[] { "Copper-colored eyes with horizontal pupils" }
            );
            //Source: https://forgottenrealms.fandom.com/wiki/Tojanida
            //https://www.d20pfsrd.com/bestiary/monster-listings/outsiders/tojanida/
            appearances[CreatureConstants.Tojanida_Juvenile] = new[] { "Resemble a cross between a monstrous crab and an enormous snapping turtles. Light green shell, sea-green skin" };
            appearances[CreatureConstants.Tojanida_Adult] = new[] { "Resemble a cross between a monstrous crab and an enormous snapping turtles. Light green shell, sea-green skin" };
            appearances[CreatureConstants.Tojanida_Elder] = new[] { "Resemble a cross between a monstrous crab and an enormous snapping turtles. Light green shell, sea-green skin" };
            //Source: https://www.d20srd.org/srd/monsters/treant.htm
            appearances[CreatureConstants.Treant] = new[] { "Face-like features on their bark, a division between the trunk that forms legs, and long branches that serve as arms; brown bark-like skin" };
            //Source: https://jurassicworld-evolution.fandom.com/wiki/Triceratops
            appearances[CreatureConstants.Triceratops] = GetWeightedAppearances(
                commonSkin: new[] { "Dark beige skin", "Pale gray skin with slightly-darker stripes across the back",
                    "Sandy-brown skin with slightly-darker stripes across the back",
                    "Dark gray skin with slightly-lighter stripes across the back, slightly-lighter underbelly",
                    "Earthy, mud-colored skin with slightly-lighter stripes across the back, slightly-lighter underbelly",
                    "Green-brown skin with slightly-lighter stripes across the back, slightly-lighter underbelly" },
                uncommonSkin: new[] { "Gray-tan skin, mottled spots down the back", "Bright green skin, yellow underbelly, mottled spots down the back",
                    "Light gray-green skin, lighter underbelly, mottled spots down the back",
                    "Dull red skin, yellow underbelly, mottled spots down the back",
                    "Orange skin, yellow underbelly, mottled spots down the back",
                    "Sandy-yellow skin, lighter underbelly, mottled spots down the back" },
                allOther: new[] { "Large bony neck frill and three horns on its head" });
            //Source: https://forgottenrealms.fandom.com/wiki/Triton
            appearances[CreatureConstants.Triton] = GetWeightedAppearances(
                commonSkin: new[] { "Silver skin", "Silver-blue skin", "Deep blue skin" },
                uncommonSkin: new[] { "Pearl-colored skin", "Light green skin" },
                rareSkin: new[] { "Dark green skin", "Bright green skin", "Gray skin", "Dark gray skin", "Night-black skin", "Red skin", "Orange skin", "Yellow skin" },
                commonHair: new[] { "Deep blue hair", "Green-blue hair", "Dark blue hair", "Dark green hair" },
                uncommonHair: new[] { "Light blue hair", "Light green hair" },
                allEyes: new[] { "Brilliant green eyes with a nictitating membrane", "Brilliant blue-green eyes with a nictitating membrane",
                    "Brilliant blue eyes with a nictitating membrane", "Brilliant dark green eyes with a nictitating membrane",
                    "Brilliant dark blue eyes with a nictitating membrane", "Brilliant light green eyes with a nictitating membrane",
                    "Brilliant light blue eyes with a nictitating membrane" },
                allOther: new[] { "Hands and feet are webbed, minor dorsal fins that run from the mid-calf to the ankle" });
            //Source: https://forgottenrealms.fandom.com/wiki/Troglodyte
            appearances[CreatureConstants.Troglodyte] = GetWeightedAppearances(
                allSkin: new[] { "Grayish-brown leathery scales" },
                allEyes: new[] { "Black beady eyes" },
                allOther: new[] { "Spindly but muscular arms, squat legs, long slender tail, lizard-like head, claws and fangs", "TODO MALE crowned with frills extending from forehead to neck" });
            //Source: https://forgottenrealms.fandom.com/wiki/Troll
            appearances[CreatureConstants.Troll] = GetWeightedAppearances(
                allSkin: new[] { "Thick, rubbery, moss-green scales", "Thick, rubbery, putrid gray scales", "Thick, rubbery, mottled gray and green scales" },
                allHair: new[] { "Iron-gray hair", "Greenish-black hair", "Steel gray hair", "Black hair" },
                allOther: new[] { "Severe hunch, backs of the hands dragging on the ground" });
            //Source: https://forgottenrealms.fandom.com/wiki/Scrag
            appearances[CreatureConstants.Troll_Scrag] = GetWeightedAppearances(
                allSkin: new[] { "Blue-green scales", "Olive-green scales" },
                allHair: new[] { "Iron-gray hair", "Greenish-black hair", "Steel gray hair", "Black hair" },
                allOther: new[] { "Severe hunch, backs of the hands dragging on the ground, gills" });
            //Source: https://forgottenrealms.fandom.com/wiki/Trumpet_archon
            appearances[CreatureConstants.TrumpetArchon] = GetWeightedAppearances(
                commonSkin: new[] { "TODO HIGH ELF" },
                uncommonSkin: new[] { "Green skin", "TODO LIGHTFOOT HALFLING" },
                commonHair: new[] { "TODO HIGH ELF" },
                uncommonHair: new[] { "TODO LIGHTFOOT HALFLING" },
                commonEyes: new[] { "TODO HIGH ELF" },
                uncommonEyes: new[] { "TODO LIGHTFOOT HALFLING" },
                allOther: new[] { "Wings" });
            //Source: https://jurassicworld-evolution.fandom.com/wiki/Tyrannosaurus
            appearances[CreatureConstants.Tyrannosaurus] = GetWeightedAppearances(
                commonSkin: new[] { "Dark brown skin", "Light brown skin", "White skin with dark gray stripes across the back", "Dark brown-gray skin, lighter underbelly",
                    "Dark gray skin, orange underbelly", "Gray-green skin with gray-yellow stripes across the back, gray-yellow underbelly",
                    "Yellow skin with black stripes across the back" },
                uncommonSkin: new[] { "Gray skin, lighter underbelly", "Dark gray skin, slightly-lighter underbelly", "Turquoise-gray skin, yellow underbelly",
                    "Green skin, yellow underbelly", "Orange skin, white underbelly", "Brown-red skin, yellow underbelly" },
                allOther: new[] { "Massive head, powerful jaw, rows of large serrated teeth, small non-functional arms" });
            //Source: https://forgottenrealms.fandom.com/wiki/Umber_hulk
            appearances[CreatureConstants.UmberHulk] = GetWeightedAppearances(
                allSkin: new[] { "Black chitinous armor, front is burnt brown", "Black chitinous armor, front is yellowish-gray" },
                allEyes: new[] { "Two sets of evenly-spaced eyes: compound eyes on the side of the head, forehead eyes that are small and apelike" },
                allOther: new[] { "Looks like a cross between a gorilla and a beetle, huge mandibles, mouth filled with rows of sharp triangular teeth" });
            appearances[CreatureConstants.UmberHulk_TrulyHorrid] = GetWeightedAppearances(
                allSkin: new[] { "Black chitinous armor, front is burnt brown", "Black chitinous armor, front is yellowish-gray" },
                allEyes: new[] { "Two sets of evenly-spaced eyes: compound eyes on the side of the head, forehead eyes that are small and apelike" },
                allOther: new[] { "Looks like a cross between a gorilla and a beetle, huge mandibles, mouth filled with rows of sharp triangular teeth" });
            //Source: https://forgottenrealms.fandom.com/wiki/Unicorn
            appearances[CreatureConstants.Unicorn] = GetWeightedAppearances(
                allHair: new[] { "White coat, mane, and tail", "White coat, mane, and tail TODO MALE white beard" },
                allEyes: new[] { "Deep sea-blue eyes", "Violet eyes", "Brown eyes", "Golden eyes", "Pink eyes" },
                allOther: new[] { "Cloven hooves, long ivory-colored horn protruding from the forehead" });
            //Source: https://forgottenrealms.fandom.com/wiki/Vampire_spawn
            appearances[CreatureConstants.VampireSpawn] = GetWeightedAppearances(
                allSkin: new[] { "TODO HUMAN skin" },
                allHair: new[] { "TODO HUMAN hair" },
                allEyes: new[] { "TODO HUMAN eyes" },
                allOther: new[] { "Hardened features, appearing predatory" });
            //Source: https://forgottenrealms.fandom.com/wiki/Vargouille
            appearances[CreatureConstants.Vargouille] = GetWeightedAppearances(
                allSkin: new[] { "TODO HUMAN skin" },
                allEyes: new[] { "TODO HUMAN eyes with an unnerving green glow" },
                allOther: new[] { "Grotesquely deformed human-like head, leathery bat-like wings instead of ears, flailing tendrils crown and fringe the head in place of hair, jagged teeth" });
            //Source: https://forgottenrealms.fandom.com/wiki/Violet_fungus
            appearances[CreatureConstants.VioletFungus] = GetWeightedAppearances(
                allSkin: new[] { "Deep purple coloration", "Dull gray coloration", "Violet coloration and covered in purple spots" },
                allOther: new[] { "Mass of root-like feelers, four leafy tendrils" });
            //Source: https://forgottenrealms.fandom.com/wiki/Vrock
            appearances[CreatureConstants.Vrock] = new[] { "A cross between a vulture and a human. Twisted and gnarled with a long neck and limbs covered in sinew. Covered in small gray feathers. Stinks of offal and carrion. Long talons and vulture head." };
            //Source: https://activepestcontrol.com/pest-info/bees-and-hornets/red-paper-wasp/
            //https://forgottenrealms.fandom.com/wiki/Giant_wasp 
            appearances[CreatureConstants.Wasp_Giant] = new[] { "Dull tan body with yellow stripes", "Pinched waist, nearly black wings, red coloring, yellow face" };
            //Source: https://www.woodlandtrust.org.uk/trees-woods-and-wildlife/animals/mammals/weasel/
            appearances[CreatureConstants.Weasel] = new[] { "Chestnut-brown fur with white-cream underparts and a long, slim body that ends in a short tail." };
            //https://forgottenrealms.fandom.com/wiki/Giant_weasel
            appearances[CreatureConstants.Weasel_Dire] = GetWeightedAppearances(
                commonHair: new[] { "Chestnut-brown fur with white-cream underparts" },
                uncommonHair: new[] { "White fur with white-cream underparts" },
                rareHair: new[] { "Black fur with black underparts" },
                allOther: new[] { "Long, slim body ending in a short tail" });
            //Source: https://www.d20srd.org/srd/monsters/whale.htm
            //https://www.fisheries.noaa.gov/species/humpback-whale
            //https://www.fisheries.noaa.gov/species/gray-whale
            //https://www.fisheries.noaa.gov/species/north-atlantic-right-whale
            appearances[CreatureConstants.Whale_Baleen] = GetWeightedAppearances(
                commonSkin: new[] {
                    "Humpback Whale: Black skin with white on the underside of the pectoral fins, belly, and underside of the tail",
                    "Gray Whale: Mottled gray skin with small eyes located just above the corners of the mouth. Broad paddle-shaped pectoral flippers, pointed at the tips. Dorsal hump.",
                    "Right Whale: Stocky black body, V-shaped blow spout, broad and deeply-notched tail, all-black belly",
                    "Right Whale: Stocky black body, V-shaped blow spout, broad and deeply-notched tail, black belly with irregularly-shaped white patches",
                },
                uncommonSkin: new[] { "Humpback Whale: Black skin with white on the underside of the pectoral fins, flanks, belly, and underside of the tail" });
            //Source: https://www.fisheries.noaa.gov/species/sperm-whale
            appearances[CreatureConstants.Whale_Cachalot] = GetWeightedAppearances(
                commonSkin: new[] { "Sperm Whale: Dark grey skin" },
                uncommonSkin: new[] { "Sperm Whale: Dark grey skin with white patches on the belly" });
            //Source: https://www.fisheries.noaa.gov/species/killer-whale
            appearances[CreatureConstants.Whale_Orca] = new[] {
                "Mostly black on top with white underside and white patches near the eyes. Gray saddle patch behind the dorsal fin.",
                "Mostly black on top with white underside and white patches near the eyes. White saddle patch behind the dorsal fin.",
            };
            //Source: https://forgottenrealms.fandom.com/wiki/Wight
            appearances[CreatureConstants.Wight] = GetWeightedAppearances(
                allSkin: new[] { "TODO HUMAN mummified skin" },
                allHair: new[] { "TODO HUMAN hair" },
                allEyes: new[] { "Calcified, all-white eyes" },
                allOther: new[] { "Hands end in claws, teeth are sharp and jagged like needles" });
            //Source: https://forgottenrealms.fandom.com/wiki/Will-o'-wisp
            appearances[CreatureConstants.WillOWisp] = GetWeightedAppearances(
                allSkin: new[] { "Hazy ball of white light", "Hazy ball of blue light", "Hazy ball of violet light", "Hazy ball of green light",
                    "Hazy ball of yellow light" });
            //Source: https://forgottenrealms.fandom.com/wiki/Winter_wolf
            appearances[CreatureConstants.WinterWolf] = GetWeightedAppearances(
                allHair: new[] { "Glistening white fur", "Glistening silvery fur" },
                commonEyes: new[] { "Blue eyes", "Very pale blue eyes" },
                uncommonEyes: new[] { "Silvery eyes" });
            //Source: https://seaworld.org/animals/facts/mammals/gray-wolf/
            appearances[CreatureConstants.Wolf] = GetWeightedAppearances(
                commonHair: new[] { "Light gray fur with a thick, dense underfur", "Gray fur with a thick, dense underfur", "Dark gray fur with a thick, dense underfur",
                    "Light gray fur interspersed with black and white with a thick, dense underfur",
                    "Gray fur interspersed with black and white with a thick, dense underfur",
                    "Dark gray fur interspersed with black and white with a thick, dense underfur"
                },
                uncommonHair: new[] { "Solid black fur with a thick, dense underfur", "Solid white fur with a thick, dense underfur" },
                allOther: new[] { "High body, long legs, broad skull tapering to a narrow muzzle, bushy tail" });
            //Source: https://forgottenrealms.fandom.com/wiki/Dire_wolf
            appearances[CreatureConstants.Wolf_Dire] = GetWeightedAppearances(
                allHair: new[] { "Thick mottled gray fur", "Thick black fur", "Thick mottled black fur", "Thick gray fur" },
                allEyes: new[] { "Eyes like fire" });
            //Source: https://www.adfg.alaska.gov/index.cfm?adfg=wolverine.printerfriendly
            appearances[CreatureConstants.Wolverine] = GetWeightedAppearances(
                commonHair: new[] {
                    "Long, dense, dark brown fur with a creamy white stripe running from each shoulder along the flanks to the base of the tail, with a white hair patch on the neck and chest",
                    "Long, dense, dark brown fur with a gold stripe running from each shoulder along the flanks to the base of the tail, with a white hair patch on the neck and chest",
                    "Long, dense, black fur with a creamy white stripe running from each shoulder along the flanks to the base of the tail, with a white hair patch on the neck and chest",
                    "Long, dense, black fur with a gold stripe running from each shoulder along the flanks to the base of the tail, with a white hair patch on the neck and chest",
                },
                uncommonHair: new[] {
                    "Long, dense, dark brown fur with a creamy white stripe running from each shoulder along the flanks to the base of the tail",
                    "Long, dense, dark brown fur with a gold stripe running from each shoulder along the flanks to the base of the tail",
                    "Long, dense, black fur with a creamy white stripe running from each shoulder along the flanks to the base of the tail",
                    "Long, dense, black fur with a gold stripe running from each shoulder along the flanks to the base of the tail",
                },
                allOther: new[] { "Thick body, short legs, short ears, broad flat head" });
            appearances[CreatureConstants.Wolverine_Dire] = GetWeightedAppearances(
                commonHair: new[] {
                    "Long, dense, dark brown fur with a creamy white stripe running from each shoulder along the flanks to the base of the tail, with a white hair patch on the neck and chest",
                    "Long, dense, dark brown fur with a gold stripe running from each shoulder along the flanks to the base of the tail, with a white hair patch on the neck and chest",
                    "Long, dense, black fur with a creamy white stripe running from each shoulder along the flanks to the base of the tail, with a white hair patch on the neck and chest",
                    "Long, dense, black fur with a gold stripe running from each shoulder along the flanks to the base of the tail, with a white hair patch on the neck and chest",
                },
                uncommonHair: new[] {
                    "Long, dense, dark brown fur with a creamy white stripe running from each shoulder along the flanks to the base of the tail",
                    "Long, dense, dark brown fur with a gold stripe running from each shoulder along the flanks to the base of the tail",
                    "Long, dense, black fur with a creamy white stripe running from each shoulder along the flanks to the base of the tail",
                    "Long, dense, black fur with a gold stripe running from each shoulder along the flanks to the base of the tail",
                },
                allOther: new[] { "Thick body, short legs, short ears, broad flat head" });
            //Source: https://forgottenrealms.fandom.com/wiki/Worg
            appearances[CreatureConstants.Worg] = GetWeightedAppearances(
                commonHair: new[] { "Gray fur", "Black fur" },
                uncommonHair: new[] { "Gray, particularly-glossy fur", "Black, particularly-glossy fur" },
                allOther: new[] { "High body, long legs, broad skull tapering to a narrow muzzle, bushy tail" });
            //Source: https://forgottenrealms.fandom.com/wiki/Wraith
            appearances[CreatureConstants.Wraith] = new[] { "A sinister, spectral figure robed in darkness, no visual features or appendages, except for their glowing red eyes" };
            appearances[CreatureConstants.Wraith_Dread] = new[] { "A sinister, spectral figure robed in darkness, no visual features or appendages, except for their glowing red eyes" };
            //Source: https://forgottenrealms.fandom.com/wiki/Wyvern
            appearances[CreatureConstants.Wyvern] = GetWeightedAppearances(
                allSkin: new[] { "Dark brown scales", "Gray scales", "Dark gray scales", "Gray-brown scales" },
                allEyes: new[] { "Orange eyes", "Red eyes" },
                allOther: new[] { "Jaw filled with long, sharp teeth" });
            //Source: https://forgottenrealms.fandom.com/wiki/Xill
            appearances[CreatureConstants.Xill] = new[] { "Vaguely reptilian with flame-red, leathery skin and solid black eyes. Roughly humanoid body with four clawed arms." };
            //Source: https://forgottenrealms.fandom.com/wiki/Xorn
            appearances[CreatureConstants.Xorn_Minor] = GetWeightedAppearances(
                commonSkin: new[] { "Brown stonelike skin", "Brown pebbly, rocky skin", "Gray stonelike skin", "Gray pebbly, rocky skin" },
                uncommonSkin: new[] { "Dark gray, metallic-like skin" },
                allOther: new[] { "Large mouth on top of head, radially-symmetric body, three arms with talons at the end, three eyes, three legs" });
            appearances[CreatureConstants.Xorn_Average] = GetWeightedAppearances(
                commonSkin: new[] { "Brown stonelike skin", "Brown pebbly, rocky skin", "Gray stonelike skin", "Gray pebbly, rocky skin" },
                uncommonSkin: new[] { "Dark gray, metallic-like skin" },
                allOther: new[] { "Large mouth on top of head, radially-symmetric body, three arms with talons at the end, three eyes, three legs" });
            appearances[CreatureConstants.Xorn_Elder] = GetWeightedAppearances(
                commonSkin: new[] { "Brown stonelike skin", "Brown pebbly, rocky skin", "Gray stonelike skin", "Gray pebbly, rocky skin" },
                uncommonSkin: new[] { "Dark gray, metallic-like skin" },
                allOther: new[] { "Large mouth on top of head, radially-symmetric body, three arms with talons at the end, three eyes, three legs" });
            //Source: https://forgottenrealms.fandom.com/wiki/Yeth_hound
            appearances[CreatureConstants.YethHound] = GetWeightedAppearances(
                allHair: new[] { "Dull, nonreflective night-black fur" },
                allEyes: new[] { "Cherry-red eyes" },
                allOther: new[] { "Resembles a greyhound, smells of chilled smoke, oddly human-like visage with protruding nose, short pointed ears that curve upward and away resembling horns" });
            //Source: https://forgottenrealms.fandom.com/wiki/Yrthak
            appearances[CreatureConstants.Yrthak] = GetWeightedAppearances(
                allSkin: new[] { "Tough yellowish-green skin, with yellow leathery wings and yellow back-fin" },
                allOther: new[] {
                    "Akin to a winged crocodile, eyeless head, disproportionately large mouth filled with yellow teeth, distinctive twisting horn",
                    "Akin to a sickly dragon, eyeless head, disproportionately large mouth filled with yellow teeth, distinctive twisting horn",
                });
            //Source: https://forgottenrealms.fandom.com/wiki/Yuan-ti_pureblood
            appearances[CreatureConstants.YuanTi_Pureblood] = GetWeightedAppearances(
                allSkin: new[] { "TODO HUMAN skin", "TODO HUMAN skin with patches of scales" },
                allHair: new[] { "TODO HUMAN hair" },
                allEyes: new[] { "TODO HUMAN eyes", "TODO HUMAN snake-like eyes" },
                commonOther: new[] { "Fangs", "Forked tongue" },
                uncommonOther: new[] { "Fangs and forked tongue" }
            );
            //Source: https://forgottenrealms.fandom.com/wiki/Yuan-ti_malison
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeArms] = GetWeightedAppearances(
                allSkin: new[] { "TODO HUMAN skin, distinctly-patterned scales" },
                allHair: new[] { "TODO HUMAN hair" },
                allEyes: new[] { "TODO HUMAN snake-like eyes" },
                allOther: new[] { "Snake arms, fangs and forked tongue" }
            );
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeHead] = GetWeightedAppearances(
                allSkin: new[] { "TODO HUMAN skin, distinctly-patterned scales" },
                allEyes: new[] { "TODO HUMAN snake-like eyes" },
                commonOther: new[] { "Snake head" },
                uncommonOther: new[] { "Snake head with hood like a cobra" }
            );
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeTail] = GetWeightedAppearances(
                allSkin: new[] { "TODO HUMAN skin, distinctly-patterned scales" },
                allHair: new[] { "TODO HUMAN hair" },
                allEyes: new[] { "TODO HUMAN snake-like eyes" },
                allOther: new[] { "Snake tail instead of human legs, fangs and forked tongue" }
            );
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs] = GetWeightedAppearances(
                allSkin: new[] { "TODO HUMAN skin, distinctly-patterned scales" },
                allHair: new[] { "TODO HUMAN hair" },
                allEyes: new[] { "TODO HUMAN snake-like eyes" },
                allOther: new[] { "Snake tail and human legs, fangs and forked tongue" }
            );
            //Source: https://forgottenrealms.fandom.com/wiki/Yuan-ti_abomination
            appearances[CreatureConstants.YuanTi_Abomination] = GetWeightedAppearances(
                allSkin: new[] { "Turquoise scales with pale yellow underbelly", "Bright green scales with yellow underbelly", "Yellow-green scales",
                    "Gray scales with black-outlined red diamonds along the back" },
                allEyes: new[] { "Yellow snake-like eyes", "Red snake-like eyes", "Green snake-like eyes" },
                allOther: new[] { "Resembles massive snake with scale-covered humanoid arms, wedge-shaped head, fangs, forked tongue" }
            );
            //Source: https://forgottenrealms.fandom.com/wiki/Zelekhut
            appearances[CreatureConstants.Zelekhut] = new[] {
                "Looks like a mechanical centaur, with skin as white as alabaster and clad in golden plate armor. A pair of golden metallic wings unfold from its back when needed.",
                "Looks like a clockwork centaur, with skin as white as alabaster and clad in golden plate armor. A pair of golden metallic wings unfold from its back when needed.",
            };

            return appearances;
        }

        private enum Rarity
        {
            Common,
            Uncommon,
            Rare,
            VeryRare
        }

        private IEnumerable<string> GetWeightedAppearances(
            IEnumerable<string> allSkin = null, IEnumerable<string> commonSkin = null, IEnumerable<string> uncommonSkin = null, IEnumerable<string> rareSkin = null,
            IEnumerable<string> allHair = null, IEnumerable<string> commonHair = null, IEnumerable<string> uncommonHair = null, IEnumerable<string> rareHair = null,
            IEnumerable<string> allEyes = null, IEnumerable<string> commonEyes = null, IEnumerable<string> uncommonEyes = null, IEnumerable<string> rareEyes = null,
            IEnumerable<string> allOther = null, IEnumerable<string> commonOther = null, IEnumerable<string> uncommonOther = null, IEnumerable<string> rareOther = null)
        {
            var appearances = Build(allSkin, commonSkin, uncommonSkin, rareSkin, (string.Empty, Rarity.Common))
                .SelectMany(a => Build(allHair, commonHair, uncommonHair, rareHair, a))
                .SelectMany(a => Build(allEyes, commonEyes, uncommonEyes, rareEyes, a))
                .SelectMany(a => Build(allOther, commonOther, uncommonOther, rareOther, a))
                .Where(a => !string.IsNullOrEmpty(a.Appearance));

            var commonAppearances = appearances.Where(a => a.Rarity == Rarity.Common).Select(a => a.Appearance);
            var uncommonAppearances = appearances.Where(a => a.Rarity == Rarity.Uncommon).Select(a => a.Appearance);
            var rareAppearances = appearances.Where(a => a.Rarity == Rarity.Rare).Select(a => a.Appearance);
            var veryRareAppearances = appearances.Where(a => a.Rarity == Rarity.VeryRare).Select(a => a.Appearance);

            return collectionSelector.CreateWeighted(commonAppearances, uncommonAppearances, rareAppearances, veryRareAppearances);
        }

        private IEnumerable<(string Appearance, Rarity Rarity)> Build(
            IEnumerable<string> all, IEnumerable<string> common, IEnumerable<string> uncommon, IEnumerable<string> rare,
            (string Appearance, Rarity Rarity) prototype)
        {
            if (all?.Any() == true)
                return all.Select(a =>
                    (GetAppearancePrototype(prototype.Appearance, a),
                    GetRarity(prototype.Rarity, Rarity.Common)));

            common ??= new[] { string.Empty };
            uncommon ??= new[] { string.Empty };
            rare ??= new[] { string.Empty };

            if (common.Concat(uncommon).Concat(rare).Any(a => !string.IsNullOrEmpty(a)) == false)
                return new[] { prototype };

            //Whether it is specified or not, common should always be here (even if empty)
            var builtCommon = common.Select(a =>
                (GetAppearancePrototype(prototype.Appearance, a),
                GetRarity(prototype.Rarity, Rarity.Common)));
            var appearances = builtCommon;

            //If we didn't specify uncommon, we should not add it as empty
            if (uncommon.Any(a => !string.IsNullOrEmpty(a)))
            {
                var builtUncommon = uncommon.Select(a =>
                    (GetAppearancePrototype(prototype.Appearance, a),
                    GetRarity(prototype.Rarity, Rarity.Uncommon)));
                appearances = appearances.Concat(builtUncommon);
            }

            //If we didn't specify rare, we should not add it as empty
            if (rare.Any(a => !string.IsNullOrEmpty(a)))
            {
                var builtRare = rare.Select(a =>
                    (GetAppearancePrototype(prototype.Appearance, a),
                    GetRarity(prototype.Rarity, Rarity.Rare)));
                appearances = appearances.Concat(builtRare);
            }

            return appearances;
        }

        private string GetAppearancePrototype(string source, string additional)
        {
            if (string.IsNullOrEmpty(source) && string.IsNullOrEmpty(additional))
                return string.Empty;

            if (string.IsNullOrEmpty(source))
                return additional;

            if (string.IsNullOrEmpty(additional))
                return source;

            return $"{source}; {additional}";
        }

        private Rarity GetRarity(Rarity source, Rarity additional)
        {
            if (source == Rarity.VeryRare || additional == Rarity.VeryRare)
                return Rarity.VeryRare;

            if (source != Rarity.Common && source == additional)
                return (Rarity)((int)source + 1);

            return (Rarity)Math.Max((int)source, (int)additional);
        }
    }
}
