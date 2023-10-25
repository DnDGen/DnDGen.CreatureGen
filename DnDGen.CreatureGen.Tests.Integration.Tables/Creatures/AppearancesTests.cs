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
            Assert.Fail("Need to fill out test cases");

            Assert.That(creatureAppearances, Contains.Key(creature));
            Assert.That(creatureAppearances[creature], Is.Not.Empty);
            Assert.That(creatureAppearances[creature].Where(a => a.Contains("TODO")), Is.Empty);

            AssertCollection(creature, creatureAppearances[creature].ToArray());
        }


        private Dictionary<string, IEnumerable<string>> GetCreatureAppearances()
        {
            var creatures = CreatureConstants.GetAll();
            var appearances = new Dictionary<string, IEnumerable<string>>();

            foreach (var creature in creatures)
            {
                appearances[creature] = new string[0];
            }

            //Source: https://forgottenrealms.fandom.com/wiki/Aasimar
            appearances[CreatureConstants.Aasimar] = GetWeightedAppearances(
                commonSkin: new[] { "Black skin", "Brown skin", "Olive skin", "White skin", "Pink skin",
                    "Deep Black skin", "Deep Brown skin", "Deep Olive skin", "Deep White skin", "Deep Pink skin",
                    "Pale Black skin", "Pale Brown skin", "Pale Olive skin", "Pale White skin", "Pale Pink skin" },
                commonHair: new[] { "Straight Red hair", "Straight Blond hair", "Straight Brown hair", "Straight Black hair", "Straight Silver hair",
                    "Curly Red hair", "Curly Blond hair", "Curly Brown hair", "Curly Black hair", "Curly Silver hair",
                    "Kinky Red hair", "Kinky Blond hair", "Kinky Brown hair", "Kinky Black hair", "Kinky Silver hair" },
                commonEyes: new[] { "Pupil-less pale white eyes", "Pupil-less golden eyes", "Pupil-less gray eyes" },
                uncommonSkin: new[] { "Emerald skin", "Gold skin", "Silver skin",
                    "Black skin with small iridescent scales", "Brown skin with small iridescent scales", "Olive skin with small iridescent scales",
                        "White skin with small iridescent scales", "Pink skin with small iridescent scales",
                    "Deep Black skin with small iridescent scales", "Deep Brown skin with small iridescent scales", "Deep Olive skin with small iridescent scales",
                        "Deep White skin with small iridescent scales", "Deep Pink skin with small iridescent scales",
                    "Pale Black skin with small iridescent scales", "Pale Brown skin with small iridescent scales", "Pale Olive skin with small iridescent scales",
                        "Pale White skin with small iridescent scales", "Pale Pink skin with small iridescent scales" },
                uncommonEyes: new[] { "Pupil-less topaz eyes", "Pupil-less pearly opalescent eyes" },
                uncommonHair: new[] { "Straight Red hair with feathers mixed in", "Straight Blond hair with feathers mixed in",
                        "Straight Brown hair with feathers mixed in", "Straight Black hair with feathers mixed in", "Straight Silver hair with feathers mixed in",
                    "Curly Red hair with feathers mixed in", "Curly Blond hair with feathers mixed in", "Curly Brown hair with feathers mixed in",
                        "Curly Black hair with feathers mixed in", "Curly Silver hair with feathers mixed in",
                    "Kinky Red hair with feathers mixed in", "Kinky Blond hair with feathers mixed in", "Kinky Brown hair with feathers mixed in",
                        "Kinky Black hair with feathers mixed in", "Kinky Silver hair with feathers mixed in" },
                rareSkin: new[] { "Emerald skin with small iridescent scales", "Gold skin with small iridescent scales", "Silver skin with small iridescent scales" },
                uncommonOther: new[] { "A light covering of feathers on the shoulders, where an angel's wings might sprout" });
            //Source: https://forgottenrealms.fandom.com/wiki/Aboleth
            appearances[CreatureConstants.Aboleth] = GetWeightedAppearances(
                allEyes: new[] { "Red eyes" },
                commonSkin: new[] { "Orange-pink underbelly, sea-green skin topside" },
                uncommonSkin: new[] { "Orange underbelly, sea-green skin topside", "Pink underbelly, sea-green skin topside",
                    "Orange-pink underbelly, green skin topside", "Orange-pink underbelly, blue skin topside" },
                rareSkin: new[] { "Orange underbelly, green skin topside", "Pink underbelly, green skin topside",
                    "Orange underbelly, blue skin topside", "Pink underbelly, blue skin topside" },
                allOther: new[] { "Resembles a bizarre eel, with a long, tubular body, as well as a tail at one end and two fins near the head and another along the back. Its mouth is lamprey-like, filled with serrated, jawless teeth. A little bit back from the head is four long tentacles, two sprouting from across each other on the top, and two more of the same on the underbelly. Its head is roughly triangular-shaped, with a spherical, somewhat beak-like nose. Above the nose is its three eyes, each one set atop the other. Tendrils and a few shorter tentacles dangle from the bottom of the head. Four blue-black slime-secreting orifices line the bottom of its body." });
            //Source: https://forgottenrealms.fandom.com/wiki/Achaierai
            appearances[CreatureConstants.Achaierai] = GetWeightedAppearances(
                commonHair: new[] { "Bright red, soft feathers on its crest, brown soft feathers on its body" },
                rareHair: new[] { "Dim teal, soft feathers on its crest, brown soft feathers on its body",
                    "Shadowed gold, soft feathers on its crest, brown soft feathers on its body",
                    "Burnt russet, soft feathers on its crest, brown soft feathers on its body" },
                allOther: new[] { "Resembles a plump quail. A quadruped with stilt-like legs that emerge from its underside, ending in wicked talons. Withered, rudimentary wings rest either side of the spherical, pony-sized head that make up its main body. Legs are a metallic blue-gray, and along with its claws and beak, shine like burnished metal." });
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
                commonHair: new[] { "Bald; Opalescent white feathers", "Bald; White feathers" },
                uncommonSkin: new[] { "Pearly white skin" },
                uncommonHair: new[] { "Long, flowing blue hair" },
                allEyes: new[] { "Glowing blue eyes" },
                allOther: new[] { "White wings tinted with gold" });
            //Source: https://forgottenrealms.fandom.com/wiki/Solar
            appearances[CreatureConstants.Angel_Solar] = GetWeightedAppearances(
                commonSkin: new[] { "Copper skin", "Silver skin", "Golden skin" },
                commonHair: new[] { "Bronze hair; White-feathered wings", "Bronze hair; Coppery-golden-feathered wings" },
                uncommonSkin: new[] { "Bronze skin", "Brass skin" },
                uncommonHair: new[] { "Copper hair; White-feathered wings", "Copper hair; Coppery-golden-feathered wings",
                    "Silver hair; White-feathered wings", "Silver hair; Coppery-golden-feathered wings",
                    "Golden hair; White-feathered wings", "Golden hair; Coppery-golden-feathered wings",
                    "Brass hair; White-feathered wings", "Brass hair; Coppery-golden-feathered wings" },
                allEyes: new[] { "Radiant topaz eyes" });
            //Source: https://www.d20srd.org/srd/monsters/animatedObject.htm
            //https://forgottenrealms.fandom.com/wiki/Animated_object
            appearances[CreatureConstants.AnimatedObject_Colossal] = new[] { "Candlestick", "Candelabra", "Plate", "Cup", "Tea Pot", "Bath Tub", "Iron Maiden" };
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
                commonSkin: new[] { "Black skin", "Brown skin", "Olive skin", "White skin", "Pink skin",
                    "Deep Black skin", "Deep Brown skin", "Deep Olive skin", "Deep White skin", "Deep Pink skin",
                    "Pale Black skin", "Pale Brown skin", "Pale Olive skin", "Pale White skin", "Pale Pink skin" },
                commonHair: new[] { "Straight Red hair", "Straight Blond hair", "Straight Brown hair", "Straight Black hair",
                    "Curly Red hair", "Curly Blond hair", "Curly Brown hair", "Curly Black hair",
                    "Kinky Red hair", "Kinky Blond hair", "Kinky Brown hair", "Kinky Black hair" },
                commonEyes: new[] { "Blue eyes", "Brown eyes", "Gray eyes", "Green eyes", "Hazel eyes" },
                uncommonSkin: new[] { "TODO Half-Elf skin" },
                uncommonHair: new[] { "TODO Half-Elf hair" },
                uncommonEyes: new[] { "TODO Half-Elf eyes" },
                rareSkin: new[] { "TODO Drow skin" },
                rareHair: new[] { "TODO Drow hair" },
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
                allSkin: new[] { "Black skin", "Brown skin", "Olive skin", "White skin", "Pink skin",
                    "Deep Black skin", "Deep Brown skin", "Deep Olive skin", "Deep White skin", "Deep Pink skin",
                    "Pale Black skin", "Pale Brown skin", "Pale Olive skin", "Pale White skin", "Pale Pink skin" },
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
            appearances[CreatureConstants.Cat] = new[] { "Siamese", "British Shorthair", "Maine Coon", "Persian", "Ragdoll", "Sphynx", "American Shorthair", "Abyssinian",
                "Exotic Shorthair", "Scottish Fold", "Burmese", "Birman", "Bombay", "Siberian", "Norwegian Forest", "Russian Blue", "American Curl", "Munchkin",
                "American Bobtail", "Devon Rex", "Balinese", "Oriental Shorthair", "Chartreux", "Turkish Angora", "Manx", "Japanese Bobtail", "American Wirehair",
                "Ragamuffin", "Egyptian Mau", "Cornish Rex", "Somali", "Himalayan", "Selkirk Rex", "Korat", "Singapura", "Ocicat", "Tonkinese", "Turkish Van",
                "British Longhair", "LaPerm", "Havana Brown", "Chausie", "Burmilla", "Toyger", "Sokoke", "Colorpoint Shorthair", "Javanese", "Snowshoe", "Australian Mist",
                "Lykoi", "Khao Manee" };
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
                "A writhing mass of teeth and barbed tentacles, grey and rotting. Random clawed hands hide under some of the tentacles." };
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
                rareSkin: new[] { "Fair purple skin", "FAir beige skin", "Fair red skin" },
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
                allSkin: new[] { "TODO Drow" },
                allHair: new[] { "TODO Drow" },
                allEyes: new[] { "TODO Drow" });
            //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/dryad-species
            appearances[CreatureConstants.Dryad] = new[] { "Delicate features seemingly made from soft wood. Hair looks as if made of leaves and foliage that changes color with the seasons." };
            //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
            appearances[CreatureConstants.Dwarf_Deep][GenderConstants.Female] = "3*12+7";
            appearances[CreatureConstants.Dwarf_Deep][GenderConstants.Male] = "3*12+9";
            appearances[CreatureConstants.Dwarf_Deep][CreatureConstants.Dwarf_Deep] = "2d4";
            appearances[CreatureConstants.Dwarf_Duergar][GenderConstants.Female] = "3*12+7";
            appearances[CreatureConstants.Dwarf_Duergar][GenderConstants.Male] = "3*12+9";
            appearances[CreatureConstants.Dwarf_Duergar][CreatureConstants.Dwarf_Duergar] = "2d4";
            appearances[CreatureConstants.Dwarf_Hill][GenderConstants.Female] = "3*12+7";
            appearances[CreatureConstants.Dwarf_Hill][GenderConstants.Male] = "3*12+9";
            appearances[CreatureConstants.Dwarf_Hill][CreatureConstants.Dwarf_Hill] = "2d4";
            appearances[CreatureConstants.Dwarf_Mountain][GenderConstants.Female] = "3*12+7";
            appearances[CreatureConstants.Dwarf_Mountain][GenderConstants.Male] = "3*12+9";
            appearances[CreatureConstants.Dwarf_Mountain][CreatureConstants.Dwarf_Mountain] = "2d4";
            //Source: https://www.dimensions.com/element/bald-eagle-haliaeetus-leucocephalus
            appearances[CreatureConstants.Eagle][GenderConstants.Female] = GetBaseFromRange(17, 24);
            appearances[CreatureConstants.Eagle][GenderConstants.Male] = GetBaseFromRange(17, 24);
            appearances[CreatureConstants.Eagle][CreatureConstants.Eagle] = GetMultiplierFromRange(17, 24);
            //Source: https://www.d20srd.org/srd/monsters/eagleGiant.htm
            appearances[CreatureConstants.Eagle_Giant][GenderConstants.Female] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.Eagle_Giant][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.Eagle_Giant][CreatureConstants.Eagle_Giant] = GetMultiplierFromAverage(10 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Efreeti
            appearances[CreatureConstants.Efreeti][GenderConstants.Agender] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Efreeti][GenderConstants.Female] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Efreeti][GenderConstants.Male] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Efreeti][CreatureConstants.Efreeti] = GetMultiplierFromAverage(12 * 12);
            //Source: https://jurassicworld-evolution.fandom.com/wiki/Elasmosaurus
            appearances[CreatureConstants.Elasmosaurus][GenderConstants.Female] = GetBaseFromAverage(40);
            appearances[CreatureConstants.Elasmosaurus][GenderConstants.Male] = GetBaseFromAverage(40);
            appearances[CreatureConstants.Elasmosaurus][CreatureConstants.Elasmosaurus] = GetMultiplierFromAverage(40);
            //Source: https://www.d20srd.org/srd/monsters/elemental.htm
            appearances[CreatureConstants.Elemental_Air_Small][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Elemental_Air_Small][CreatureConstants.Elemental_Air_Small] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Elemental_Air_Medium][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Elemental_Air_Medium][CreatureConstants.Elemental_Air_Medium] = GetMultiplierFromAverage(8 * 12);
            appearances[CreatureConstants.Elemental_Air_Large][GenderConstants.Agender] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Elemental_Air_Large][CreatureConstants.Elemental_Air_Large] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Elemental_Air_Huge][GenderConstants.Agender] = GetBaseFromAverage(32 * 12);
            appearances[CreatureConstants.Elemental_Air_Huge][CreatureConstants.Elemental_Air_Huge] = GetMultiplierFromAverage(32 * 12);
            appearances[CreatureConstants.Elemental_Air_Greater][GenderConstants.Agender] = GetBaseFromAverage(36 * 12);
            appearances[CreatureConstants.Elemental_Air_Greater][CreatureConstants.Elemental_Air_Greater] = GetMultiplierFromAverage(36 * 12);
            appearances[CreatureConstants.Elemental_Air_Elder][GenderConstants.Agender] = GetBaseFromAverage(40 * 12);
            appearances[CreatureConstants.Elemental_Air_Elder][CreatureConstants.Elemental_Air_Elder] = GetMultiplierFromAverage(40 * 12);
            appearances[CreatureConstants.Elemental_Earth_Small][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Elemental_Earth_Small][CreatureConstants.Elemental_Earth_Small] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Elemental_Earth_Medium][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Elemental_Earth_Medium][CreatureConstants.Elemental_Earth_Medium] = GetMultiplierFromAverage(8 * 12);
            appearances[CreatureConstants.Elemental_Earth_Large][GenderConstants.Agender] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Elemental_Earth_Large][CreatureConstants.Elemental_Earth_Large] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Elemental_Earth_Huge][GenderConstants.Agender] = GetBaseFromAverage(32 * 12);
            appearances[CreatureConstants.Elemental_Earth_Huge][CreatureConstants.Elemental_Earth_Huge] = GetMultiplierFromAverage(32 * 12);
            appearances[CreatureConstants.Elemental_Earth_Greater][GenderConstants.Agender] = GetBaseFromAverage(36 * 12);
            appearances[CreatureConstants.Elemental_Earth_Greater][CreatureConstants.Elemental_Earth_Greater] = GetMultiplierFromAverage(36 * 12);
            appearances[CreatureConstants.Elemental_Earth_Elder][GenderConstants.Agender] = GetBaseFromAverage(40 * 12);
            appearances[CreatureConstants.Elemental_Earth_Elder][CreatureConstants.Elemental_Earth_Elder] = GetMultiplierFromAverage(40 * 12);
            appearances[CreatureConstants.Elemental_Fire_Small][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Elemental_Fire_Small][CreatureConstants.Elemental_Fire_Small] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Elemental_Fire_Medium][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Elemental_Fire_Medium][CreatureConstants.Elemental_Fire_Medium] = GetMultiplierFromAverage(8 * 12);
            appearances[CreatureConstants.Elemental_Fire_Large][GenderConstants.Agender] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Elemental_Fire_Large][CreatureConstants.Elemental_Fire_Large] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Elemental_Fire_Huge][GenderConstants.Agender] = GetBaseFromAverage(32 * 12);
            appearances[CreatureConstants.Elemental_Fire_Huge][CreatureConstants.Elemental_Fire_Huge] = GetMultiplierFromAverage(32 * 12);
            appearances[CreatureConstants.Elemental_Fire_Greater][GenderConstants.Agender] = GetBaseFromAverage(36 * 12);
            appearances[CreatureConstants.Elemental_Fire_Greater][CreatureConstants.Elemental_Fire_Greater] = GetMultiplierFromAverage(36 * 12);
            appearances[CreatureConstants.Elemental_Fire_Elder][GenderConstants.Agender] = GetBaseFromAverage(40 * 12);
            appearances[CreatureConstants.Elemental_Fire_Elder][CreatureConstants.Elemental_Fire_Elder] = GetMultiplierFromAverage(40 * 12);
            appearances[CreatureConstants.Elemental_Water_Small][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Elemental_Water_Small][CreatureConstants.Elemental_Water_Small] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Elemental_Water_Medium][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Elemental_Water_Medium][CreatureConstants.Elemental_Water_Medium] = GetMultiplierFromAverage(8 * 12);
            appearances[CreatureConstants.Elemental_Water_Large][GenderConstants.Agender] = GetBaseFromAverage(16 * 12);
            appearances[CreatureConstants.Elemental_Water_Large][CreatureConstants.Elemental_Water_Large] = GetMultiplierFromAverage(16 * 12);
            appearances[CreatureConstants.Elemental_Water_Huge][GenderConstants.Agender] = GetBaseFromAverage(32 * 12);
            appearances[CreatureConstants.Elemental_Water_Huge][CreatureConstants.Elemental_Water_Huge] = GetMultiplierFromAverage(32 * 12);
            appearances[CreatureConstants.Elemental_Water_Greater][GenderConstants.Agender] = GetBaseFromAverage(36 * 12);
            appearances[CreatureConstants.Elemental_Water_Greater][CreatureConstants.Elemental_Water_Greater] = GetMultiplierFromAverage(36 * 12);
            appearances[CreatureConstants.Elemental_Water_Elder][GenderConstants.Agender] = GetBaseFromAverage(40 * 12);
            appearances[CreatureConstants.Elemental_Water_Elder][CreatureConstants.Elemental_Water_Elder] = GetMultiplierFromAverage(40 * 12);
            //Source: https://www.dimensions.com/element/african-bush-elephant-loxodonta-africana
            appearances[CreatureConstants.Elephant][GenderConstants.Female] = GetBaseFromRange(8 * 12 + 6, 13 * 12);
            appearances[CreatureConstants.Elephant][GenderConstants.Male] = GetBaseFromRange(8 * 12 + 6, 13 * 12);
            appearances[CreatureConstants.Elephant][CreatureConstants.Elephant] = GetMultiplierFromRange(8 * 12 + 6, 13 * 12);
            //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
            appearances[CreatureConstants.Elf_Aquatic][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Elf_Aquatic][GenderConstants.Male] = "4*12+5";
            appearances[CreatureConstants.Elf_Aquatic][CreatureConstants.Elf_Aquatic] = "2d6";
            appearances[CreatureConstants.Elf_Drow][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Elf_Drow][GenderConstants.Male] = "4*12+5";
            appearances[CreatureConstants.Elf_Drow][CreatureConstants.Elf_Drow] = "2d6";
            appearances[CreatureConstants.Elf_Gray][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Elf_Gray][GenderConstants.Male] = "4*12+5";
            appearances[CreatureConstants.Elf_Gray][CreatureConstants.Elf_Gray] = "2d6";
            appearances[CreatureConstants.Elf_Half] = GetWeightedAppearances(
                commonSkin: new[] { "Black skin", "Brown skin", "Olive skin", "White skin", "Pink skin",
                    "Deep Black skin", "Deep Brown skin", "Deep Olive skin", "Deep White skin", "Deep Pink skin",
                    "Pale Black skin", "Pale Brown skin", "Pale Olive skin", "Pale White skin", "Pale Pink skin",
                    "TODO High Elf Skin" },
                commonHair: new[] { "Straight Red hair", "Straight Blond hair", "Straight Brown hair", "Straight Black hair",
                    "Curly Red hair", "Curly Blond hair", "Curly Brown hair", "Curly Black hair",
                    "Kinky Red hair", "Kinky Blond hair", "Kinky Brown hair", "Kinky Black hair",
                    "TODO High Elf Hair" },
                commonEyes: new[] { "Blue eyes", "Brown eyes", "Gray eyes", "Green eyes", "Hazel eyes",
                    "TODO High Elf Eyes" },
                uncommonSkin: new[] { "TODO Grey Elf Skin",
                    "TODO Wood Elf Skin",
                    "TODO Wild Elf Skin",
                    "TODO Drow Skin" },
                uncommonHair: new[] { "TODO Grey Elf Hair",
                    "TODO Wood Elf Hair",
                    "TODO Wild Elf Hair",
                    "TODO Drow Hair" },
                uncommonEyes: new[] { "TODO Grey Elf Eyes",
                    "TODO Wood Elf Eyes",
                    "TODO Wild Elf Eyes",
                    "TODO Drow Eyes" });
            appearances[CreatureConstants.Elf_High][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Elf_High][GenderConstants.Male] = "4*12+5";
            appearances[CreatureConstants.Elf_High][CreatureConstants.Elf_High] = "2d6";
            appearances[CreatureConstants.Elf_Wild][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Elf_Wild][GenderConstants.Male] = "4*12+5";
            appearances[CreatureConstants.Elf_Wild][CreatureConstants.Elf_Wild] = "2d6";
            appearances[CreatureConstants.Elf_Wood][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Elf_Wood][GenderConstants.Male] = "4*12+5";
            appearances[CreatureConstants.Elf_Wood][CreatureConstants.Elf_Wood] = "2d6";
            //Source: https://www.d20srd.org/srd/monsters/devil.htm#erinyes
            appearances[CreatureConstants.Erinyes][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Erinyes][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Erinyes][CreatureConstants.Erinyes] = GetMultiplierFromAverage(6 * 12);
            //Source: https://aminoapps.com/c/officialdd/page/item/ethereal-filcher/5B63_a5vi5Ia7NM1djj0V1QYVoxpXweGW1z
            appearances[CreatureConstants.EtherealFilcher][GenderConstants.Agender] = GetBaseFromAverage(4 * 12 + 6);
            appearances[CreatureConstants.EtherealFilcher][CreatureConstants.EtherealFilcher] = GetMultiplierFromAverage(4 * 12 + 6);
            //Source: https://www.d20srd.org/srd/monsters/etherealMarauder.htm
            appearances[CreatureConstants.EtherealMarauder][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.EtherealMarauder][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.EtherealMarauder][CreatureConstants.EtherealMarauder] = GetMultiplierFromAverage(4 * 12);
            //Source: https://syrikdarkenedskies.obsidianportal.com/wikis/ettercap-race
            appearances[CreatureConstants.Ettercap][GenderConstants.Female] = "5*12+7";
            appearances[CreatureConstants.Ettercap][GenderConstants.Male] = "5*12+2";
            appearances[CreatureConstants.Ettercap][CreatureConstants.Ettercap] = "2d10";
            //Source: https://forgottenrealms.fandom.com/wiki/Ettin
            appearances[CreatureConstants.Ettin][GenderConstants.Female] = "12*12+2";
            appearances[CreatureConstants.Ettin][GenderConstants.Male] = "12*12+10";
            appearances[CreatureConstants.Ettin][CreatureConstants.Ettin] = "2d6";
            appearances[CreatureConstants.FireBeetle_Giant][GenderConstants.Female] = "0";
            appearances[CreatureConstants.FireBeetle_Giant][GenderConstants.Male] = "0";
            appearances[CreatureConstants.FireBeetle_Giant][CreatureConstants.FireBeetle_Giant] = "0";
            //Source: https://www.d20srd.org/srd/monsters/formian.htm
            appearances[CreatureConstants.FormianWorker][GenderConstants.Male] = GetBaseFromAverage(2 * 12 + 6);
            appearances[CreatureConstants.FormianWorker][CreatureConstants.FormianWorker] = GetMultiplierFromAverage(2 * 12 + 6);
            appearances[CreatureConstants.FormianWarrior][GenderConstants.Male] = GetBaseFromAverage(4 * 12 + 6);
            appearances[CreatureConstants.FormianWarrior][CreatureConstants.FormianWarrior] = GetMultiplierFromAverage(4 * 12 + 6);
            appearances[CreatureConstants.FormianTaskmaster][GenderConstants.Male] = GetBaseFromAverage(4 * 12 + 6);
            appearances[CreatureConstants.FormianTaskmaster][CreatureConstants.FormianTaskmaster] = GetMultiplierFromAverage(4 * 12 + 6);
            appearances[CreatureConstants.FormianMyrmarch][GenderConstants.Male] = GetBaseFromAverage(5 * 12 + 6);
            appearances[CreatureConstants.FormianMyrmarch][CreatureConstants.FormianMyrmarch] = GetMultiplierFromAverage(5 * 12 + 6);
            appearances[CreatureConstants.FormianQueen][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.FormianQueen][CreatureConstants.FormianQueen] = GetMultiplierFromAverage(4 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Frost_worm
            appearances[CreatureConstants.FrostWorm][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.FrostWorm][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.FrostWorm][CreatureConstants.FrostWorm] = GetMultiplierFromAverage(5 * 12);
            //Source: https://dungeonsdragons.fandom.com/wiki/Gargoyle
            appearances[CreatureConstants.Gargoyle][GenderConstants.Agender] = "5*12";
            appearances[CreatureConstants.Gargoyle][CreatureConstants.Gargoyle] = "2d10";
            appearances[CreatureConstants.Gargoyle_Kapoacinth][GenderConstants.Agender] = "5*12";
            appearances[CreatureConstants.Gargoyle_Kapoacinth][CreatureConstants.Gargoyle_Kapoacinth] = "2d10";
            //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/gelatinous-cube-species
            appearances[CreatureConstants.GelatinousCube][GenderConstants.Agender] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.GelatinousCube][CreatureConstants.GelatinousCube] = GetMultiplierFromAverage(10 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Ghaele
            //Adjusting female -2" to match range
            appearances[CreatureConstants.Ghaele][GenderConstants.Female] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 7 - 2);
            appearances[CreatureConstants.Ghaele][GenderConstants.Male] = GetBaseFromRange(5 * 12 + 2, 7 * 12);
            appearances[CreatureConstants.Ghaele][CreatureConstants.Ghaele] = GetMultiplierFromRange(5 * 12 + 2, 7 * 12);
            //Source: https://www.dandwiki.com/wiki/Ghoul_(5e_Race)
            appearances[CreatureConstants.Ghoul][GenderConstants.Female] = "4*12";
            appearances[CreatureConstants.Ghoul][GenderConstants.Male] = "4*12";
            appearances[CreatureConstants.Ghoul][CreatureConstants.Ghoul] = "2d12";
            appearances[CreatureConstants.Ghoul_Ghast][GenderConstants.Female] = "4*12";
            appearances[CreatureConstants.Ghoul_Ghast][GenderConstants.Male] = "4*12";
            appearances[CreatureConstants.Ghoul_Ghast][CreatureConstants.Ghoul_Ghast] = "2d12";
            appearances[CreatureConstants.Ghoul_Lacedon][GenderConstants.Female] = "4*12";
            appearances[CreatureConstants.Ghoul_Lacedon][GenderConstants.Male] = "4*12";
            appearances[CreatureConstants.Ghoul_Lacedon][CreatureConstants.Ghoul_Lacedon] = "2d12";
            //Source: https://forgottenrealms.fandom.com/wiki/Cloud_giant
            appearances[CreatureConstants.Giant_Cloud][GenderConstants.Female] = GetBaseFromRange(22 * 12 + 8, 25 * 12);
            appearances[CreatureConstants.Giant_Cloud][GenderConstants.Male] = GetBaseFromRange(24 * 12 + 4, 26 * 12 + 8);
            appearances[CreatureConstants.Giant_Cloud][CreatureConstants.Giant_Cloud] = GetMultiplierFromRange(22 * 12 + 8, 25 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Fire_giant
            //Adjusting female max -1" to match range
            appearances[CreatureConstants.Giant_Fire][GenderConstants.Female] = GetBaseFromRange(17 * 12 + 5, 19 * 12 - 1);
            appearances[CreatureConstants.Giant_Fire][GenderConstants.Male] = GetBaseFromRange(18 * 12 + 2, 19 * 12 + 8);
            appearances[CreatureConstants.Giant_Fire][CreatureConstants.Giant_Fire] = GetMultiplierFromRange(18 * 12 + 2, 19 * 12 + 8);
            //Source: https://forgottenrealms.fandom.com/wiki/Frost_giant
            appearances[CreatureConstants.Giant_Frost][GenderConstants.Female] = GetBaseFromRange(20 * 12 + 1, 22 * 12 + 4);
            appearances[CreatureConstants.Giant_Frost][GenderConstants.Male] = GetBaseFromRange(21 * 12 + 3, 23 * 12 + 6);
            appearances[CreatureConstants.Giant_Frost][CreatureConstants.Giant_Frost] = GetMultiplierFromRange(21 * 12 + 3, 23 * 12 + 6);
            //Source: https://forgottenrealms.fandom.com/wiki/Hill_giant
            appearances[CreatureConstants.Giant_Hill][GenderConstants.Female] = GetBaseFromRange(15 * 12 + 5, 16 * 12 + 4);
            appearances[CreatureConstants.Giant_Hill][GenderConstants.Male] = GetBaseFromRange(16 * 12 + 1, 17 * 12);
            appearances[CreatureConstants.Giant_Hill][CreatureConstants.Giant_Hill] = GetMultiplierFromRange(15 * 12 + 5, 16 * 12 + 4);
            //Source: https://forgottenrealms.fandom.com/wiki/Stone_giant
            //Adjusting female max -1" to match range
            appearances[CreatureConstants.Giant_Stone][GenderConstants.Female] = GetBaseFromRange(17 * 12 + 5, 19 * 12 - 1);
            appearances[CreatureConstants.Giant_Stone][GenderConstants.Male] = GetBaseFromRange(18 * 12 + 2, 19 * 12 + 8);
            appearances[CreatureConstants.Giant_Stone][CreatureConstants.Giant_Stone] = GetMultiplierFromRange(18 * 12 + 2, 19 * 12 + 8);
            appearances[CreatureConstants.Giant_Stone_Elder][GenderConstants.Female] = GetBaseFromRange(17 * 12 + 5, 19 * 12 - 1);
            appearances[CreatureConstants.Giant_Stone_Elder][GenderConstants.Male] = GetBaseFromRange(18 * 12 + 2, 19 * 12 + 8);
            appearances[CreatureConstants.Giant_Stone_Elder][CreatureConstants.Giant_Stone_Elder] = GetMultiplierFromRange(18 * 12 + 2, 19 * 12 + 8);
            //Source: https://forgottenrealms.fandom.com/wiki/Storm_giant
            appearances[CreatureConstants.Giant_Storm][GenderConstants.Female] = GetBaseFromRange(23 * 12 + 8, 26 * 12 + 8);
            appearances[CreatureConstants.Giant_Storm][GenderConstants.Male] = GetBaseFromRange(26 * 12 + 4, 29 * 12 + 4);
            appearances[CreatureConstants.Giant_Storm][CreatureConstants.Giant_Storm] = GetMultiplierFromRange(26 * 12 + 4, 29 * 12 + 4);
            //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/gibbering-mouther-species
            appearances[CreatureConstants.GibberingMouther][GenderConstants.Agender] = GetBaseFromRange(3 * 12, 7 * 12);
            appearances[CreatureConstants.GibberingMouther][CreatureConstants.GibberingMouther] = GetMultiplierFromRange(3 * 12, 7 * 12);
            //Source: https://www.d20srd.org/srd/monsters/girallon.htm
            appearances[CreatureConstants.Girallon][GenderConstants.Female] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Girallon][GenderConstants.Male] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Girallon][CreatureConstants.Girallon] = GetMultiplierFromAverage(8 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Githyanki
            appearances[CreatureConstants.Githyanki][GenderConstants.Female] = GetBaseFromRange(5 * 12 + 4, 6 * 12 + 10);
            appearances[CreatureConstants.Githyanki][GenderConstants.Male] = GetBaseFromRange(5 * 12 + 5, 6 * 12 + 11);
            appearances[CreatureConstants.Githyanki][CreatureConstants.Githyanki] = GetMultiplierFromRange(5 * 12 + 5, 6 * 12 + 11);
            //Source: https://forgottenrealms.fandom.com/wiki/Githzerai
            appearances[CreatureConstants.Githzerai][GenderConstants.Female] = GetBaseFromRange(5 * 12 + 1, 7 * 12);
            appearances[CreatureConstants.Githzerai][GenderConstants.Male] = GetBaseFromRange(5 * 12 + 1, 7 * 12);
            appearances[CreatureConstants.Githzerai][CreatureConstants.Githzerai] = GetMultiplierFromRange(5 * 12 + 1, 7 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Glabrezu
            appearances[CreatureConstants.Glabrezu][GenderConstants.Agender] = GetBaseFromRange(9 * 12, 15 * 12);
            appearances[CreatureConstants.Glabrezu][CreatureConstants.Glabrezu] = GetMultiplierFromRange(9 * 12, 15 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Gnoll
            appearances[CreatureConstants.Gnoll][GenderConstants.Female] = GetBaseFromRange(7 * 12, 7 * 12 + 6);
            appearances[CreatureConstants.Gnoll][GenderConstants.Male] = GetBaseFromRange(7 * 12, 7 * 12 + 6);
            appearances[CreatureConstants.Gnoll][CreatureConstants.Gnoll] = GetMultiplierFromRange(7 * 12, 7 * 12 + 6);
            //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
            appearances[CreatureConstants.Gnome_Forest][GenderConstants.Female] = "2*12+10";
            appearances[CreatureConstants.Gnome_Forest][GenderConstants.Male] = "3*12+0";
            appearances[CreatureConstants.Gnome_Forest][CreatureConstants.Gnome_Forest] = "2d4";
            appearances[CreatureConstants.Gnome_Rock][GenderConstants.Female] = "2*12+10";
            appearances[CreatureConstants.Gnome_Rock][GenderConstants.Male] = "3*12+0";
            appearances[CreatureConstants.Gnome_Rock][CreatureConstants.Gnome_Rock] = "2d4";
            appearances[CreatureConstants.Gnome_Svirfneblin][GenderConstants.Female] = "2*12+10";
            appearances[CreatureConstants.Gnome_Svirfneblin][GenderConstants.Male] = "3*12+0";
            appearances[CreatureConstants.Gnome_Svirfneblin][CreatureConstants.Gnome_Svirfneblin] = "2d4";
            //Source: https://forgottenrealms.fandom.com/wiki/Goblin
            appearances[CreatureConstants.Goblin][GenderConstants.Female] = GetBaseFromRange(3 * 12 + 4, 3 * 12 + 8);
            appearances[CreatureConstants.Goblin][GenderConstants.Male] = GetBaseFromRange(3 * 12 + 4, 3 * 12 + 8);
            appearances[CreatureConstants.Goblin][CreatureConstants.Goblin] = GetMultiplierFromRange(3 * 12 + 4, 3 * 12 + 8);
            //Source: https://pathfinderwiki.com/wiki/Clay_golem
            appearances[CreatureConstants.Golem_Clay][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Golem_Clay][CreatureConstants.Golem_Clay] = GetMultiplierFromAverage(8 * 12);
            //Source: https://www.d20srd.org/srd/monsters/golem.htm
            appearances[CreatureConstants.Golem_Flesh][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Golem_Flesh][CreatureConstants.Golem_Flesh] = GetMultiplierFromAverage(8 * 12);
            //Source: https://www.d20srd.org/srd/monsters/golem.htm
            appearances[CreatureConstants.Golem_Iron][GenderConstants.Agender] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Golem_Iron][CreatureConstants.Golem_Iron] = GetMultiplierFromAverage(12 * 12);
            //Source: https://www.d20srd.org/srd/monsters/golem.htm
            appearances[CreatureConstants.Golem_Stone][GenderConstants.Agender] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.Golem_Stone][CreatureConstants.Golem_Stone] = GetMultiplierFromAverage(9 * 12);
            //Source: https://www.d20srd.org/srd/monsters/golem.htm
            appearances[CreatureConstants.Golem_Stone_Greater][GenderConstants.Agender] = GetBaseFromAverage(18 * 12);
            appearances[CreatureConstants.Golem_Stone_Greater][CreatureConstants.Golem_Stone_Greater] = GetMultiplierFromAverage(18 * 12);
            //Source: https://www.d20srd.org/srd/monsters/gorgon.htm
            appearances[CreatureConstants.Gorgon][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Gorgon][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Gorgon][CreatureConstants.Gorgon] = GetMultiplierFromAverage(6 * 12);
            //Source: https://www.d20srd.org/srd/monsters/ooze.htm#grayOoze
            appearances[CreatureConstants.GrayOoze][GenderConstants.Agender] = GetBaseFromAverage(6);
            appearances[CreatureConstants.GrayOoze][CreatureConstants.GrayOoze] = GetMultiplierFromAverage(6);
            //Source: https://www.d20srd.org/srd/monsters/grayRender.htm
            appearances[CreatureConstants.GrayRender][GenderConstants.Agender] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.GrayRender][CreatureConstants.GrayRender] = GetMultiplierFromAverage(9 * 12);
            //Source: https://www.d20srd.org/srd/monsters/hag.htm#greenHag Copy from Human
            appearances[CreatureConstants.GreenHag][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.GreenHag][CreatureConstants.GreenHag] = "2d10";
            //Source: https://www.d20srd.org/srd/monsters/grick.htm
            appearances[CreatureConstants.Grick][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Grick][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Grick][CreatureConstants.Grick] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Griffon
            appearances[CreatureConstants.Griffon][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Griffon][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Griffon][CreatureConstants.Griffon] = GetMultiplierFromAverage(5 * 12);
            //Source: https://www.d20srd.org/srd/monsters/sprite.htm#grig
            appearances[CreatureConstants.Grig][GenderConstants.Female] = GetBaseFromAverage(18);
            appearances[CreatureConstants.Grig][GenderConstants.Male] = GetBaseFromAverage(18);
            appearances[CreatureConstants.Grig][CreatureConstants.Grig] = GetMultiplierFromAverage(18);
            appearances[CreatureConstants.Grig_WithFiddle][GenderConstants.Female] = GetBaseFromAverage(18);
            appearances[CreatureConstants.Grig_WithFiddle][GenderConstants.Male] = GetBaseFromAverage(18);
            appearances[CreatureConstants.Grig_WithFiddle][CreatureConstants.Grig_WithFiddle] = GetMultiplierFromAverage(18);
            //Source: https://forgottenrealms.fandom.com/wiki/Grimlock
            appearances[CreatureConstants.Grimlock][GenderConstants.Female] = GetBaseFromRange(5 * 12, 5 * 12 + 6);
            appearances[CreatureConstants.Grimlock][GenderConstants.Male] = GetBaseFromRange(5 * 12, 5 * 12 + 6);
            appearances[CreatureConstants.Grimlock][CreatureConstants.Grimlock] = GetMultiplierFromRange(5 * 12, 5 * 12 + 6);
            //Source: https://www.mojobob.com/roleplay/monstrousmanual/s/sphinx.html
            appearances[CreatureConstants.Gynosphinx][GenderConstants.Female] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Gynosphinx][CreatureConstants.Gynosphinx] = GetMultiplierFromAverage(7 * 12);
            //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
            appearances[CreatureConstants.Halfling_Deep][GenderConstants.Female] = "2*12+6";
            appearances[CreatureConstants.Halfling_Deep][GenderConstants.Male] = "2*12+8";
            appearances[CreatureConstants.Halfling_Deep][CreatureConstants.Halfling_Deep] = "2d4";
            appearances[CreatureConstants.Halfling_Lightfoot][GenderConstants.Female] = "2*12+6";
            appearances[CreatureConstants.Halfling_Lightfoot][GenderConstants.Male] = "2*12+8";
            appearances[CreatureConstants.Halfling_Lightfoot][CreatureConstants.Halfling_Lightfoot] = "2d4";
            appearances[CreatureConstants.Halfling_Tallfellow][GenderConstants.Female] = "3*12+6";
            appearances[CreatureConstants.Halfling_Tallfellow][GenderConstants.Male] = "3*12+8";
            appearances[CreatureConstants.Halfling_Tallfellow][CreatureConstants.Halfling_Tallfellow] = "2d4";
            //Source: https://www.5esrd.com/database/race/harpy/
            appearances[CreatureConstants.Harpy][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Harpy][GenderConstants.Male] = "4*12+10";
            appearances[CreatureConstants.Harpy][CreatureConstants.Harpy] = "2d10";
            //Source: https://www.dimensions.com/element/osprey-pandion-haliaetus
            appearances[CreatureConstants.Hawk][GenderConstants.Female] = GetBaseFromRange(11, 16);
            appearances[CreatureConstants.Hawk][GenderConstants.Male] = GetBaseFromRange(11, 16);
            appearances[CreatureConstants.Hawk][CreatureConstants.Hawk] = GetMultiplierFromRange(11, 16);
            appearances[CreatureConstants.Hellcat_Bezekira][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Hellcat_Bezekira][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Hellcat_Bezekira][CreatureConstants.Hellcat_Bezekira] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Hell_hound
            appearances[CreatureConstants.HellHound][GenderConstants.Female] = GetBaseFromRange(24, 48 + 6);
            appearances[CreatureConstants.HellHound][GenderConstants.Male] = GetBaseFromRange(24, 48 + 6);
            appearances[CreatureConstants.HellHound][CreatureConstants.HellHound] = GetMultiplierFromRange(24, 48 + 6);
            appearances[CreatureConstants.HellHound_NessianWarhound][GenderConstants.Female] = GetBaseFromRange(64, 72);
            appearances[CreatureConstants.HellHound_NessianWarhound][GenderConstants.Male] = GetBaseFromRange(64, 72);
            appearances[CreatureConstants.HellHound_NessianWarhound][CreatureConstants.HellHound_NessianWarhound] = GetMultiplierFromRange(64, 72);
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            appearances[CreatureConstants.Hellwasp_Swarm] = new[] { "5,000 Hellwasps" };
            //Source: https://www.d20srd.org/srd/monsters/demon.htm#hezrou
            appearances[CreatureConstants.Hezrou][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Hezrou][CreatureConstants.Hezrou] = GetMultiplierFromAverage(8 * 12);
            //Source: https://www.mojobob.com/roleplay/monstrousmanual/s/sphinx.html
            appearances[CreatureConstants.Hieracosphinx][GenderConstants.Male] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Hieracosphinx][CreatureConstants.Hieracosphinx] = GetMultiplierFromAverage(7 * 12);
            appearances[CreatureConstants.Hippogriff][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Hippogriff][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Hippogriff][CreatureConstants.Hippogriff] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Hobgoblin
            appearances[CreatureConstants.Hobgoblin][GenderConstants.Female] = GetBaseFromRange(5 * 12, 6 * 12);
            appearances[CreatureConstants.Hobgoblin][GenderConstants.Male] = GetBaseFromRange(5 * 12, 6 * 12);
            appearances[CreatureConstants.Hobgoblin][CreatureConstants.Hobgoblin] = GetMultiplierFromRange(5 * 12, 6 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Homunculus
            //https://www.dimensions.com/element/eastern-gray-squirrel
            appearances[CreatureConstants.Homunculus][GenderConstants.Agender] = GetBaseFromRange(4, 6);
            appearances[CreatureConstants.Homunculus][CreatureConstants.Homunculus] = GetMultiplierFromRange(4, 6);
            //Source: https://www.d20srd.org/srd/monsters/devil.htm#hornedDevilCornugon
            appearances[CreatureConstants.HornedDevil_Cornugon][GenderConstants.Agender] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.HornedDevil_Cornugon][CreatureConstants.HornedDevil_Cornugon] = GetMultiplierFromAverage(9 * 12);
            //Source: https://www.dimensions.com/element/clydesdale-horse
            appearances[CreatureConstants.Horse_Heavy][GenderConstants.Female] = GetBaseFromRange(64, 72);
            appearances[CreatureConstants.Horse_Heavy][GenderConstants.Male] = GetBaseFromRange(64, 72);
            appearances[CreatureConstants.Horse_Heavy][CreatureConstants.Horse_Heavy] = GetMultiplierFromRange(64, 72);
            //Source: https://www.dimensions.com/element/arabian-horse
            appearances[CreatureConstants.Horse_Light][GenderConstants.Female] = GetBaseFromRange(57, 61);
            appearances[CreatureConstants.Horse_Light][GenderConstants.Male] = GetBaseFromRange(57, 61);
            appearances[CreatureConstants.Horse_Light][CreatureConstants.Horse_Light] = GetMultiplierFromRange(57, 61);
            //Source: https://www.dimensions.com/element/clydesdale-horse
            appearances[CreatureConstants.Horse_Heavy_War][GenderConstants.Female] = GetBaseFromRange(64, 72);
            appearances[CreatureConstants.Horse_Heavy_War][GenderConstants.Male] = GetBaseFromRange(64, 72);
            appearances[CreatureConstants.Horse_Heavy_War][CreatureConstants.Horse_Heavy_War] = GetMultiplierFromRange(64, 72);
            //Source: https://www.dimensions.com/element/arabian-horse
            appearances[CreatureConstants.Horse_Light_War][GenderConstants.Female] = GetBaseFromRange(57, 61);
            appearances[CreatureConstants.Horse_Light_War][GenderConstants.Male] = GetBaseFromRange(57, 61);
            appearances[CreatureConstants.Horse_Light_War][CreatureConstants.Horse_Light_War] = GetMultiplierFromRange(57, 61);
            //Source: https://forgottenrealms.fandom.com/wiki/Hound_archon
            appearances[CreatureConstants.HoundArchon][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.HoundArchon][CreatureConstants.HoundArchon] = GetMultiplierFromAverage(6 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Howler
            appearances[CreatureConstants.Howler][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Howler][CreatureConstants.Howler] = GetMultiplierFromAverage(8 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Human
            appearances[CreatureConstants.Human] = GetWeightedAppearances(
                allSkin: new[] { "Black skin", "Brown skin", "Olive skin", "White skin", "Pink skin",
                    "Deep Black skin", "Deep Brown skin", "Deep Olive skin", "Deep White skin", "Deep Pink skin",
                    "Pale Black skin", "Pale Brown skin", "Pale Olive skin", "Pale White skin", "Pale Pink skin" },
                allHair: new[] { "Straight Red hair", "Straight Blond hair", "Straight Brown hair", "Straight Black hair",
                    "Curly Red hair", "Curly Blond hair", "Curly Brown hair", "Curly Black hair",
                    "Kinky Red hair", "Kinky Blond hair", "Kinky Brown hair", "Kinky Black hair" },
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
            appearances[CreatureConstants.Hyena][GenderConstants.Female] = GetBaseFromRange(25, 30);
            appearances[CreatureConstants.Hyena][GenderConstants.Male] = GetBaseFromRange(25, 30);
            appearances[CreatureConstants.Hyena][CreatureConstants.Hyena] = GetMultiplierFromRange(25, 30);
            //Source: https://forgottenrealms.fandom.com/wiki/Gelugon
            appearances[CreatureConstants.IceDevil_Gelugon][GenderConstants.Agender] = GetBaseFromRange(10 * 12 + 6, 12 * 12);
            appearances[CreatureConstants.IceDevil_Gelugon][CreatureConstants.IceDevil_Gelugon] = GetMultiplierFromRange(10 * 12 + 6, 12 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Imp
            appearances[CreatureConstants.Imp][GenderConstants.Agender] = GetBaseFromAverage(2 * 12);
            appearances[CreatureConstants.Imp][GenderConstants.Female] = GetBaseFromAverage(2 * 12);
            appearances[CreatureConstants.Imp][GenderConstants.Male] = GetBaseFromAverage(2 * 12);
            appearances[CreatureConstants.Imp][CreatureConstants.Imp] = GetMultiplierFromAverage(2 * 12);
            //Source: https://www.d20srd.org/srd/monsters/invisibleStalker.htm
            //https://www.d20srd.org/srd/combat/movementPositionAndDistance.htm using generic Large, since actual form is unknown
            appearances[CreatureConstants.InvisibleStalker][GenderConstants.Agender] = GetBaseFromRange(8 * 12, 16 * 12);
            appearances[CreatureConstants.InvisibleStalker][CreatureConstants.InvisibleStalker] = GetMultiplierFromRange(8 * 12, 16 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Janni
            appearances[CreatureConstants.Janni][GenderConstants.Agender] = GetBaseFromRange(6 * 12, 7 * 12);
            appearances[CreatureConstants.Janni][GenderConstants.Female] = GetBaseFromRange(6 * 12, 7 * 12);
            appearances[CreatureConstants.Janni][GenderConstants.Male] = GetBaseFromRange(6 * 12, 7 * 12);
            appearances[CreatureConstants.Janni][CreatureConstants.Janni] = GetMultiplierFromRange(6 * 12, 7 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Kobold
            appearances[CreatureConstants.Kobold][GenderConstants.Female] = GetBaseFromRange(2 * 12, 2 * 12 + 6);
            appearances[CreatureConstants.Kobold][GenderConstants.Male] = GetBaseFromRange(2 * 12, 2 * 12 + 6);
            appearances[CreatureConstants.Kobold][CreatureConstants.Kobold] = GetMultiplierFromRange(2 * 12, 2 * 12 + 6);
            //Source: https://pathfinderwiki.com/wiki/Kolyarut
            //Can't find definitive height, but "size of a tall humanoid", so using human + some
            appearances[CreatureConstants.Kolyarut][GenderConstants.Agender] = "5*12";
            appearances[CreatureConstants.Kolyarut][CreatureConstants.Kolyarut] = "2d12";
            //Source: https://forgottenrealms.fandom.com/wiki/Kraken
            appearances[CreatureConstants.Kraken][GenderConstants.Female] = GetBaseFromAverage(30 * 12);
            appearances[CreatureConstants.Kraken][GenderConstants.Male] = GetBaseFromAverage(30 * 12);
            appearances[CreatureConstants.Kraken][CreatureConstants.Kraken] = GetMultiplierFromAverage(30 * 12);
            //Source: https://www.d20srd.org/srd/monsters/krenshar.htm
            appearances[CreatureConstants.Krenshar][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Krenshar][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Krenshar][CreatureConstants.Krenshar] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Kuo-toa
            appearances[CreatureConstants.KuoToa][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.KuoToa][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.KuoToa][CreatureConstants.KuoToa] = GetMultiplierFromAverage(5 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Lamia
            appearances[CreatureConstants.Lamia][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Lamia][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Lamia][CreatureConstants.Lamia] = GetMultiplierFromAverage(6 * 12);
            //Source: https://www.d20srd.org/srd/monsters/lammasu.htm
            //https://www.dimensions.com/element/african-lion scale up from lion: [44,50]*8*12/[54,78] = [78,62]
            appearances[CreatureConstants.Lammasu][GenderConstants.Female] = GetBaseFromRange(62, 78);
            appearances[CreatureConstants.Lammasu][GenderConstants.Male] = GetBaseFromRange(62, 78);
            appearances[CreatureConstants.Lammasu][CreatureConstants.Lammasu] = GetMultiplierFromRange(62, 78);
            //Source: https://forgottenrealms.fandom.com/wiki/Lantern_archon
            appearances[CreatureConstants.LanternArchon][GenderConstants.Agender] = GetBaseFromRange(12, 36);
            appearances[CreatureConstants.LanternArchon][CreatureConstants.LanternArchon] = GetMultiplierFromRange(12, 36);
            //Source: https://forgottenrealms.fandom.com/wiki/Lemure
            appearances[CreatureConstants.Lemure][GenderConstants.Agender] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Lemure][CreatureConstants.Lemure] = GetMultiplierFromAverage(5 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Leonal
            appearances[CreatureConstants.Leonal][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Leonal][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Leonal][CreatureConstants.Leonal] = GetMultiplierFromAverage(6 * 12);
            //Source: https://www.dimensions.com/element/cougar
            appearances[CreatureConstants.Leopard][GenderConstants.Female] = GetBaseFromRange(21, 28);
            appearances[CreatureConstants.Leopard][GenderConstants.Male] = GetBaseFromRange(21, 28);
            appearances[CreatureConstants.Leopard][CreatureConstants.Leopard] = GetMultiplierFromRange(21, 28);
            //Using Human
            appearances[CreatureConstants.Lillend][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Lillend][GenderConstants.Male] = "4*12+10";
            appearances[CreatureConstants.Lillend][CreatureConstants.Lillend] = "2d10";
            //Source: https://www.dimensions.com/element/african-lion
            appearances[CreatureConstants.Lion][GenderConstants.Female] = GetBaseFromAverage(2 * 12 + 10, 3 * 12 + 4);
            appearances[CreatureConstants.Lion][GenderConstants.Male] = GetBaseFromAverage(3 * 12 + 4);
            appearances[CreatureConstants.Lion][CreatureConstants.Lion] = GetMultiplierFromAverage(3 * 12 + 4);
            //Scaling up from lion, x3 based on length. Since length doesn't differ for male/female, only use male
            appearances[CreatureConstants.Lion_Dire][GenderConstants.Female] = GetBaseFromAverage((3 * 12 + 4) * 3);
            appearances[CreatureConstants.Lion_Dire][GenderConstants.Male] = GetBaseFromAverage((3 * 12 + 4) * 3);
            appearances[CreatureConstants.Lion_Dire][CreatureConstants.Lion_Dire] = GetMultiplierFromAverage((3 * 12 + 4) * 3);
            //Source: https://www.dimensions.com/element/green-iguana-iguana-iguana
            appearances[CreatureConstants.Lizard][GenderConstants.Female] = GetBaseFromRange(1, 2);
            appearances[CreatureConstants.Lizard][GenderConstants.Male] = GetBaseFromRange(1, 2);
            appearances[CreatureConstants.Lizard][CreatureConstants.Lizard] = GetMultiplierFromRange(1, 2);
            //Source: https://www.dimensions.com/element/savannah-monitor-varanus-exanthematicus
            appearances[CreatureConstants.Lizard_Monitor][GenderConstants.Female] = GetBaseFromRange(7, 8);
            appearances[CreatureConstants.Lizard_Monitor][GenderConstants.Male] = GetBaseFromRange(7, 8);
            appearances[CreatureConstants.Lizard_Monitor][CreatureConstants.Lizard_Monitor] = GetMultiplierFromRange(7, 8);
            //Source: https://forgottenrealms.fandom.com/wiki/Lizardfolk
            appearances[CreatureConstants.Lizardfolk][GenderConstants.Female] = GetBaseFromRange(6 * 12, 7 * 12);
            appearances[CreatureConstants.Lizardfolk][GenderConstants.Male] = GetBaseFromRange(6 * 12, 7 * 12);
            appearances[CreatureConstants.Lizardfolk][CreatureConstants.Lizardfolk] = GetMultiplierFromRange(6 * 12, 7 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Locathah
            appearances[CreatureConstants.Locathah][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Locathah][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Locathah][CreatureConstants.Locathah] = GetMultiplierFromAverage(5 * 12);
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            appearances[CreatureConstants.Locust_Swarm] = new[] { "5,000 locusts" };
            //Source: https://forgottenrealms.fandom.com/wiki/Magmin
            appearances[CreatureConstants.Magmin][GenderConstants.Agender] = GetBaseFromRange(3 * 12, 4 * 12);
            appearances[CreatureConstants.Magmin][CreatureConstants.Magmin] = GetMultiplierFromRange(3 * 12, 4 * 12);
            //Source: https://www.dimensions.com/element/reef-manta-ray-mobula-alfredi
            appearances[CreatureConstants.MantaRay][GenderConstants.Female] = "0";
            appearances[CreatureConstants.MantaRay][GenderConstants.Male] = "0";
            appearances[CreatureConstants.MantaRay][CreatureConstants.MantaRay] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Manticore Using Lion for height
            appearances[CreatureConstants.Manticore][GenderConstants.Female] = GetBaseFromAverage(2 * 12 + 10, 3 * 12 + 4);
            appearances[CreatureConstants.Manticore][GenderConstants.Male] = GetBaseFromAverage(3 * 12 + 4);
            appearances[CreatureConstants.Manticore][CreatureConstants.Manticore] = GetMultiplierFromAverage(3 * 12 + 4);
            //Source: https://forgottenrealms.fandom.com/wiki/Marilith
            appearances[CreatureConstants.Marilith][GenderConstants.Female] = GetBaseFromRange(7 * 12, 9 * 12);
            appearances[CreatureConstants.Marilith][CreatureConstants.Marilith] = GetMultiplierFromRange(7 * 12, 9 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Marut
            appearances[CreatureConstants.Marut][GenderConstants.Agender] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Marut][CreatureConstants.Marut] = GetMultiplierFromAverage(12 * 12);
            //Source: https://www.d20srd.org/srd/monsters/medusa.htm
            appearances[CreatureConstants.Medusa][GenderConstants.Female] = GetBaseFromRange(5 * 12, 6 * 12);
            appearances[CreatureConstants.Medusa][GenderConstants.Male] = GetBaseFromRange(5 * 12, 6 * 12);
            appearances[CreatureConstants.Medusa][CreatureConstants.Medusa] = GetMultiplierFromRange(5 * 12, 6 * 12);
            //Source: https://jurassicworld-evolution.fandom.com/wiki/Velociraptor
            appearances[CreatureConstants.Megaraptor][GenderConstants.Female] = GetBaseFromAverage(67);
            appearances[CreatureConstants.Megaraptor][GenderConstants.Male] = GetBaseFromAverage(67);
            appearances[CreatureConstants.Megaraptor][CreatureConstants.Megaraptor] = GetMultiplierFromAverage(67);
            //Source: https://forgottenrealms.fandom.com/wiki/Mephit
            appearances[CreatureConstants.Mephit_Air][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Air][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Air][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Air][CreatureConstants.Mephit_Air] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Dust][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Dust][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Dust][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Dust][CreatureConstants.Mephit_Dust] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Earth][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Earth][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Earth][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Earth][CreatureConstants.Mephit_Earth] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Fire][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Fire][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Fire][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Fire][CreatureConstants.Mephit_Fire] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Ice][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Ice][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Ice][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Ice][CreatureConstants.Mephit_Ice] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Magma][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Magma][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Magma][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Magma][CreatureConstants.Mephit_Magma] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Ooze][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Ooze][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Ooze][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Ooze][CreatureConstants.Mephit_Ooze] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Salt][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Salt][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Salt][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Salt][CreatureConstants.Mephit_Salt] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Steam][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Steam][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Steam][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Steam][CreatureConstants.Mephit_Steam] = GetMultiplierFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Water][GenderConstants.Agender] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Water][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Water][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Mephit_Water][CreatureConstants.Mephit_Water] = GetMultiplierFromAverage(4 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Merfolk
            appearances[CreatureConstants.Merfolk][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Merfolk][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Merfolk][CreatureConstants.Merfolk] = "0";
            //Source: https://www.d20srd.org/srd/monsters/mimic.htm
            appearances[CreatureConstants.Mimic][GenderConstants.Agender] = GetBaseFromRange(3 * 12, 10 * 12);
            appearances[CreatureConstants.Mimic][CreatureConstants.Mimic] = GetMultiplierFromRange(3 * 12, 10 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Mind_flayer
            appearances[CreatureConstants.MindFlayer][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.MindFlayer][CreatureConstants.MindFlayer] = GetMultiplierFromAverage(6 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Minotaur
            appearances[CreatureConstants.Minotaur][GenderConstants.Female] = GetBaseFromAverage(7 * 12, 9 * 12);
            appearances[CreatureConstants.Minotaur][GenderConstants.Male] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.Minotaur][CreatureConstants.Minotaur] = GetMultiplierFromAverage(9 * 12);
            //Source: https://www.d20srd.org/srd/monsters/mohrg.htm
            appearances[CreatureConstants.Mohrg][GenderConstants.Agender] = GetBaseFromRange(5 * 12, 6 * 12);
            appearances[CreatureConstants.Mohrg][CreatureConstants.Mohrg] = GetMultiplierFromRange(5 * 12, 6 * 12);
            //Source: https://www.dimensions.com/element/tufted-capuchin-sapajus-apella
            appearances[CreatureConstants.Monkey][GenderConstants.Female] = GetBaseFromRange(10, 16);
            appearances[CreatureConstants.Monkey][GenderConstants.Male] = GetBaseFromRange(10, 16);
            appearances[CreatureConstants.Monkey][CreatureConstants.Monkey] = GetMultiplierFromRange(10, 16);
            //Source: https://www.dimensions.com/element/mule-equus-asinus-x-equus-caballus
            appearances[CreatureConstants.Mule][GenderConstants.Female] = GetBaseFromRange(56, 68);
            appearances[CreatureConstants.Mule][GenderConstants.Male] = GetBaseFromRange(56, 68);
            appearances[CreatureConstants.Mule][CreatureConstants.Mule] = GetMultiplierFromRange(56, 68);
            //Source: https://www.d20srd.org/srd/monsters/mummy.htm
            appearances[CreatureConstants.Mummy][GenderConstants.Female] = GetBaseFromRange(5 * 12, 6 * 12);
            appearances[CreatureConstants.Mummy][GenderConstants.Male] = GetBaseFromRange(5 * 12, 6 * 12);
            appearances[CreatureConstants.Mummy][CreatureConstants.Mummy] = GetMultiplierFromRange(5 * 12, 6 * 12);
            appearances[CreatureConstants.Naga_Dark][GenderConstants.Hermaphrodite] = "0";
            appearances[CreatureConstants.Naga_Dark][CreatureConstants.Naga_Dark] = "0";
            appearances[CreatureConstants.Naga_Guardian][GenderConstants.Hermaphrodite] = "0";
            appearances[CreatureConstants.Naga_Guardian][CreatureConstants.Naga_Guardian] = "0";
            appearances[CreatureConstants.Naga_Spirit][GenderConstants.Hermaphrodite] = "0";
            appearances[CreatureConstants.Naga_Spirit][CreatureConstants.Naga_Spirit] = "0";
            appearances[CreatureConstants.Naga_Water][GenderConstants.Hermaphrodite] = "0";
            appearances[CreatureConstants.Naga_Water][CreatureConstants.Naga_Water] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Nalfeshnee
            appearances[CreatureConstants.Nalfeshnee][GenderConstants.Agender] = GetBaseFromRange(10 * 12, 20 * 12);
            appearances[CreatureConstants.Nalfeshnee][CreatureConstants.Nalfeshnee] = GetMultiplierFromRange(10 * 12, 20 * 12);
            //Source: https://www.d20srd.org/srd/monsters/nightHag.htm Copy from Human
            appearances[CreatureConstants.NightHag][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.NightHag][CreatureConstants.NightHag] = "2d10";
            //Source: https://www.d20srd.org/srd/monsters/nightshade.htm
            appearances[CreatureConstants.Nightcrawler][GenderConstants.Agender] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Nightcrawler][CreatureConstants.Nightcrawler] = GetMultiplierFromAverage(7 * 12);
            //Source: https://www.d20srd.org/srd/monsters/nightmare.htm
            appearances[CreatureConstants.Nightmare][GenderConstants.Agender] = GetBaseFromRange(57, 61);
            appearances[CreatureConstants.Nightmare][CreatureConstants.Nightmare] = GetMultiplierFromRange(57, 61);
            //Scale up x2
            appearances[CreatureConstants.Nightmare_Cauchemar][GenderConstants.Agender] = GetBaseFromRange(57 * 2, 61 * 2);
            appearances[CreatureConstants.Nightmare_Cauchemar][CreatureConstants.Nightmare_Cauchemar] = GetMultiplierFromRange(57 * 2, 61 * 2);
            //Source: https://www.d20srd.org/srd/monsters/nightshade.htm
            appearances[CreatureConstants.Nightwalker][GenderConstants.Agender] = GetBaseFromAverage(20 * 12);
            appearances[CreatureConstants.Nightwalker][CreatureConstants.Nightwalker] = GetMultiplierFromAverage(20 * 12);
            //Source: https://www.d20srd.org/srd/monsters/nightshade.htm
            //https://www.dimensions.com/element/giant-golden-crowned-flying-fox-acerodon-jubatus scaled up: [18,22]*40*12/[59,67] = [146,158]
            appearances[CreatureConstants.Nightwing][GenderConstants.Agender] = GetBaseFromRange(146, 158);
            appearances[CreatureConstants.Nightwing][CreatureConstants.Nightwing] = GetMultiplierFromRange(146, 158);
            //Source: https://www.d20srd.org/srd/monsters/sprite.htm#nixie
            appearances[CreatureConstants.Nixie][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Nixie][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.Nixie][CreatureConstants.Nixie] = GetMultiplierFromAverage(4 * 12);
            //Source: https://www.d20srd.org/srd/monsters/nymph.htm Copy from High Elf
            appearances[CreatureConstants.Nymph][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Nymph][CreatureConstants.Nymph] = "2d6";
            //Source: https://www.d20srd.org/srd/monsters/ooze.htm#ochreJelly
            appearances[CreatureConstants.OchreJelly][GenderConstants.Agender] = GetBaseFromAverage(6);
            appearances[CreatureConstants.OchreJelly][CreatureConstants.OchreJelly] = GetMultiplierFromAverage(6);
            //Source: https://www.dimensions.com/element/common-octopus-octopus-vulgaris (mantle length)
            appearances[CreatureConstants.Octopus][GenderConstants.Female] = GetBaseFromRange(6, 10);
            appearances[CreatureConstants.Octopus][GenderConstants.Male] = GetBaseFromRange(6, 10);
            appearances[CreatureConstants.Octopus][CreatureConstants.Octopus] = GetMultiplierFromRange(6, 10);
            //Source: https://www.dimensions.com/element/giant-pacific-octopus-enteroctopus-dofleini (mantle length)
            appearances[CreatureConstants.Octopus_Giant][GenderConstants.Female] = GetBaseFromRange(20, 24);
            appearances[CreatureConstants.Octopus_Giant][GenderConstants.Male] = GetBaseFromRange(20, 24);
            appearances[CreatureConstants.Octopus_Giant][CreatureConstants.Octopus_Giant] = GetMultiplierFromRange(20, 24);
            //Source: https://forgottenrealms.fandom.com/wiki/Ogre
            appearances[CreatureConstants.Ogre][GenderConstants.Female] = GetBaseFromRange(9 * 12 + 3, 10 * 12);
            appearances[CreatureConstants.Ogre][GenderConstants.Male] = GetBaseFromRange(10 * 12 + 1, 10 * 12 + 10);
            appearances[CreatureConstants.Ogre][CreatureConstants.Ogre] = GetMultiplierFromRange(10 * 12 + 1, 10 * 12 + 10);
            appearances[CreatureConstants.Ogre_Merrow][GenderConstants.Female] = GetBaseFromRange(9 * 12 + 3, 10 * 12);
            appearances[CreatureConstants.Ogre_Merrow][GenderConstants.Male] = GetBaseFromRange(10 * 12 + 1, 10 * 12 + 10);
            appearances[CreatureConstants.Ogre_Merrow][CreatureConstants.Ogre_Merrow] = GetMultiplierFromRange(10 * 12 + 1, 10 * 12 + 10);
            //Source: https://forgottenrealms.fandom.com/wiki/Oni_mage
            appearances[CreatureConstants.OgreMage][GenderConstants.Female] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.OgreMage][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.OgreMage][CreatureConstants.OgreMage] = GetMultiplierFromAverage(10 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Orc
            appearances[CreatureConstants.Orc][GenderConstants.Female] = GetBaseFromAtLeast(6 * 12);
            appearances[CreatureConstants.Orc][GenderConstants.Male] = GetBaseFromAtLeast(6 * 12);
            appearances[CreatureConstants.Orc][CreatureConstants.Orc] = GetMultiplierFromAtLeast(6 * 12);
            //Source: https://www.d20srd.org/srd/description.htm#vitalStatistics
            appearances[CreatureConstants.Orc_Half][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Orc_Half][GenderConstants.Male] = "4*12+10";
            appearances[CreatureConstants.Orc_Half][CreatureConstants.Orc_Half] = "2d12";
            //Source: https://criticalrole.fandom.com/wiki/Otyugh
            appearances[CreatureConstants.Otyugh][GenderConstants.Hermaphrodite] = GetBaseFromAverage(15 * 12);
            appearances[CreatureConstants.Otyugh][CreatureConstants.Otyugh] = GetMultiplierFromAverage(15 * 12);
            //Source: https://www.dimensions.com/element/great-horned-owl-bubo-virginianus
            appearances[CreatureConstants.Owl][GenderConstants.Female] = GetBaseFromRange(9, 14);
            appearances[CreatureConstants.Owl][GenderConstants.Male] = GetBaseFromRange(9, 14);
            appearances[CreatureConstants.Owl][CreatureConstants.Owl] = GetMultiplierFromRange(9, 14);
            //Source: https://www.d20srd.org/srd/monsters/owlGiant.htm 
            appearances[CreatureConstants.Owl_Giant][GenderConstants.Female] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.Owl_Giant][GenderConstants.Male] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.Owl_Giant][CreatureConstants.Owl_Giant] = GetMultiplierFromAverage(9 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Owlbear
            //https://www.dimensions.com/element/polar-bears polar bears are similar length, so using for height
            //Adjusting female max +5" to match range
            appearances[CreatureConstants.Owlbear][GenderConstants.Female] = GetBaseFromRange(2 * 12 + 8, 3 * 12 + 11 + 5);
            appearances[CreatureConstants.Owlbear][GenderConstants.Male] = GetBaseFromRange(3 * 12 + 7, 5 * 12 + 3);
            appearances[CreatureConstants.Owlbear][CreatureConstants.Owlbear] = GetMultiplierFromRange(3 * 12 + 7, 5 * 12 + 3);
            //Source: https://www.d20srd.org/srd/monsters/pegasus.htm
            appearances[CreatureConstants.Pegasus][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Pegasus][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Pegasus][CreatureConstants.Pegasus] = GetMultiplierFromAverage(6 * 12);
            //Source: https://www.d20pfsrd.com/bestiary/monster-listings/plants/fungus-phantom/
            appearances[CreatureConstants.PhantomFungus][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.PhantomFungus][CreatureConstants.PhantomFungus] = GetMultiplierFromAverage(6 * 12);
            //Source: https://www.d20srd.org/srd/monsters/phaseSpider.htm
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Medium
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Large - going between, so 12 inches
            appearances[CreatureConstants.PhaseSpider][GenderConstants.Female] = GetBaseFromAverage(12);
            appearances[CreatureConstants.PhaseSpider][GenderConstants.Male] = GetBaseFromAverage(12);
            appearances[CreatureConstants.PhaseSpider][CreatureConstants.PhaseSpider] = GetMultiplierFromAverage(12);
            //Source: https://www.d20srd.org/srd/monsters/phasm.htm
            appearances[CreatureConstants.Phasm][GenderConstants.Agender] = GetBaseFromAverage(2 * 12);
            appearances[CreatureConstants.Phasm][CreatureConstants.Phasm] = GetMultiplierFromAverage(2 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Pit_fiend
            appearances[CreatureConstants.PitFiend][GenderConstants.Agender] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.PitFiend][CreatureConstants.PitFiend] = GetMultiplierFromAverage(12 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Pixie
            appearances[CreatureConstants.Pixie][GenderConstants.Female] = GetBaseFromRange(12, 30);
            appearances[CreatureConstants.Pixie][GenderConstants.Male] = GetBaseFromRange(12, 30);
            appearances[CreatureConstants.Pixie][CreatureConstants.Pixie] = GetMultiplierFromRange(12, 30);
            appearances[CreatureConstants.Pixie_WithIrresistibleDance][GenderConstants.Female] = GetBaseFromRange(12, 30);
            appearances[CreatureConstants.Pixie_WithIrresistibleDance][GenderConstants.Male] = GetBaseFromRange(12, 30);
            appearances[CreatureConstants.Pixie_WithIrresistibleDance][CreatureConstants.Pixie_WithIrresistibleDance] = GetMultiplierFromRange(12, 30);
            //Source: https://www.dimensions.com/element/shetland-pony
            appearances[CreatureConstants.Pony][GenderConstants.Female] = GetBaseFromRange(28, 44);
            appearances[CreatureConstants.Pony][GenderConstants.Male] = GetBaseFromRange(28, 44);
            appearances[CreatureConstants.Pony][CreatureConstants.Pony] = GetMultiplierFromRange(28, 44);
            appearances[CreatureConstants.Pony_War][GenderConstants.Female] = GetBaseFromRange(28, 44);
            appearances[CreatureConstants.Pony_War][GenderConstants.Male] = GetBaseFromRange(28, 44);
            appearances[CreatureConstants.Pony_War][CreatureConstants.Pony_War] = GetMultiplierFromRange(28, 44);
            //Source: https://www.dimensions.com/element/harbour-porpoise-phocoena-phocoena
            appearances[CreatureConstants.Porpoise][GenderConstants.Female] = GetBaseFromRange(14, 16);
            appearances[CreatureConstants.Porpoise][GenderConstants.Male] = GetBaseFromRange(14, 16);
            appearances[CreatureConstants.Porpoise][CreatureConstants.Porpoise] = GetMultiplierFromRange(14, 16);
            //Source: https://forgottenrealms.fandom.com/wiki/Giant_praying_mantis
            appearances[CreatureConstants.PrayingMantis_Giant][GenderConstants.Female] = GetBaseFromRange(2 * 12, 5 * 12);
            appearances[CreatureConstants.PrayingMantis_Giant][GenderConstants.Male] = GetBaseFromRange(2 * 12, 5 * 12);
            appearances[CreatureConstants.PrayingMantis_Giant][CreatureConstants.PrayingMantis_Giant] = GetMultiplierFromRange(2 * 12, 5 * 12);
            //Source: https://www.d20srd.org/srd/monsters/pseudodragon.htm
            //Scale from Red Dragon Wyrmling: 48*3/16 = 9
            appearances[CreatureConstants.Pseudodragon][GenderConstants.Female] = GetBaseFromAverage(9);
            appearances[CreatureConstants.Pseudodragon][GenderConstants.Male] = GetBaseFromAverage(9);
            appearances[CreatureConstants.Pseudodragon][CreatureConstants.Pseudodragon] = GetMultiplierFromAverage(9);
            //Source: https://www.d20srd.org/srd/monsters/purpleWorm.htm
            appearances[CreatureConstants.PurpleWorm][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.PurpleWorm][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.PurpleWorm][CreatureConstants.PurpleWorm] = GetMultiplierFromAverage(5 * 12);
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
            appearances[CreatureConstants.Quasit][GenderConstants.Agender] = GetBaseFromRange(12, 24);
            appearances[CreatureConstants.Quasit][CreatureConstants.Quasit] = GetMultiplierFromRange(12, 24);
            //Source: https://www.d20srd.org/srd/monsters/rakshasa.htm Copy from Human
            appearances[CreatureConstants.Rakshasa][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Rakshasa][GenderConstants.Male] = "4*12+10";
            appearances[CreatureConstants.Rakshasa][CreatureConstants.Rakshasa] = "2d10";
            //Source: https://www.d20srd.org/srd/monsters/rast.htm - body size of "large dog", head almost as large as body, so Riding Dog height x2
            appearances[CreatureConstants.Rast][GenderConstants.Agender] = GetBaseFromRange(22 * 2, 30 * 2);
            appearances[CreatureConstants.Rast][CreatureConstants.Rast] = GetMultiplierFromRange(22 * 2, 30 * 2);
            //Source: https://www.dimensions.com/element/common-rat
            appearances[CreatureConstants.Rat][GenderConstants.Female] = GetBaseFromRange(2, 4);
            appearances[CreatureConstants.Rat][GenderConstants.Male] = GetBaseFromRange(2, 4);
            appearances[CreatureConstants.Rat][CreatureConstants.Rat] = GetMultiplierFromRange(2, 4);
            //Scaled up from Rat, x6 based on length
            appearances[CreatureConstants.Rat_Dire][GenderConstants.Female] = GetBaseFromRange(12, 24);
            appearances[CreatureConstants.Rat_Dire][GenderConstants.Male] = GetBaseFromRange(12, 24);
            appearances[CreatureConstants.Rat_Dire][CreatureConstants.Rat_Dire] = GetMultiplierFromRange(12, 24);
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            appearances[CreatureConstants.Rat_Swarm] = new[] { "300 rats" };
            //Source: https://www.dimensions.com/element/common-raven-corvus-corax
            appearances[CreatureConstants.Raven][GenderConstants.Female] = GetBaseFromRange(12, 16);
            appearances[CreatureConstants.Raven][GenderConstants.Male] = GetBaseFromRange(12, 16);
            appearances[CreatureConstants.Raven][CreatureConstants.Raven] = GetMultiplierFromRange(12, 16);
            //Source: https://www.d20srd.org/srd/monsters/ravid.htm
            appearances[CreatureConstants.Ravid][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.Ravid][CreatureConstants.Ravid] = "0";
            //Source: https://www.d20srd.org/srd/monsters/razorBoar.htm Copying from Dire Boar
            appearances[CreatureConstants.RazorBoar][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.RazorBoar][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.RazorBoar][CreatureConstants.RazorBoar] = GetMultiplierFromAverage(6 * 12);
            //Source: https://www.d20srd.org/srd/monsters/remorhaz.htm
            appearances[CreatureConstants.Remorhaz][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Remorhaz][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Remorhaz][CreatureConstants.Remorhaz] = GetMultiplierFromAverage(5 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Retriever
            appearances[CreatureConstants.Retriever][GenderConstants.Agender] = GetBaseFromAverage(12 * 12);
            appearances[CreatureConstants.Retriever][CreatureConstants.Retriever] = GetMultiplierFromAverage(12 * 12);
            //Source: https://www.d20srd.org/srd/monsters/rhinoceros.htm
            appearances[CreatureConstants.Rhinoceras][GenderConstants.Female] = GetBaseFromRange(3 * 12, 6 * 12);
            appearances[CreatureConstants.Rhinoceras][GenderConstants.Male] = GetBaseFromRange(3 * 12, 6 * 12);
            appearances[CreatureConstants.Rhinoceras][CreatureConstants.Rhinoceras] = GetMultiplierFromRange(3 * 12, 6 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Roc
            appearances[CreatureConstants.Roc][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Roc][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Roc][CreatureConstants.Roc] = "0";
            //Source: https://www.d20srd.org/srd/monsters/roper.htm
            appearances[CreatureConstants.Roper][GenderConstants.Hermaphrodite] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.Roper][CreatureConstants.Roper] = GetMultiplierFromAverage(9 * 12);
            //Source: https://www.d20srd.org/srd/monsters/rustMonster.htm
            appearances[CreatureConstants.RustMonster][GenderConstants.Female] = GetBaseFromAverage(3 * 12);
            appearances[CreatureConstants.RustMonster][GenderConstants.Male] = GetBaseFromAverage(3 * 12);
            appearances[CreatureConstants.RustMonster][CreatureConstants.RustMonster] = GetMultiplierFromAverage(3 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Sahuagin
            appearances[CreatureConstants.Sahuagin][GenderConstants.Female] = GetBaseFromRange(6 * 12, 9 * 12);
            appearances[CreatureConstants.Sahuagin][GenderConstants.Male] = GetBaseFromRange(6 * 12, 9 * 12);
            appearances[CreatureConstants.Sahuagin][CreatureConstants.Sahuagin] = GetMultiplierFromRange(6 * 12, 9 * 12);
            appearances[CreatureConstants.Sahuagin_Malenti][GenderConstants.Female] = GetBaseFromRange(6 * 12, 9 * 12);
            appearances[CreatureConstants.Sahuagin_Malenti][GenderConstants.Male] = GetBaseFromRange(6 * 12, 9 * 12);
            appearances[CreatureConstants.Sahuagin_Malenti][CreatureConstants.Sahuagin_Malenti] = GetMultiplierFromRange(6 * 12, 9 * 12);
            appearances[CreatureConstants.Sahuagin_Mutant][GenderConstants.Female] = GetBaseFromRange(6 * 12, 9 * 12);
            appearances[CreatureConstants.Sahuagin_Mutant][GenderConstants.Male] = GetBaseFromRange(6 * 12, 9 * 12);
            appearances[CreatureConstants.Sahuagin_Mutant][CreatureConstants.Sahuagin_Mutant] = GetMultiplierFromRange(6 * 12, 9 * 12);
            //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/salamander-article (average)
            //Scaling down by half for flamebrother, Scaling up x2 for noble. Assuming height is half of length
            appearances[CreatureConstants.Salamander_Flamebrother][GenderConstants.Agender] = GetBaseFromAverage(20 * 12 / 4);
            appearances[CreatureConstants.Salamander_Flamebrother][CreatureConstants.Salamander_Flamebrother] = GetMultiplierFromAverage(20 * 12 / 4);
            appearances[CreatureConstants.Salamander_Average][GenderConstants.Agender] = GetBaseFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Salamander_Average][CreatureConstants.Salamander_Average] = GetMultiplierFromAverage(20 * 12 / 2);
            appearances[CreatureConstants.Salamander_Noble][GenderConstants.Agender] = GetBaseFromAverage(20 * 12);
            appearances[CreatureConstants.Salamander_Noble][CreatureConstants.Salamander_Noble] = GetMultiplierFromAverage(20 * 12);
            //Source: https://www.d20srd.org/srd/monsters/satyr.htm - copy from Half-Elf
            appearances[CreatureConstants.Satyr][GenderConstants.Male] = "4*12+7";
            appearances[CreatureConstants.Satyr][CreatureConstants.Satyr] = "2d8";
            appearances[CreatureConstants.Satyr_WithPipes][GenderConstants.Male] = "4*12+7";
            appearances[CreatureConstants.Satyr_WithPipes][CreatureConstants.Satyr_WithPipes] = "2d8";
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Scorpion,_Tiny
            appearances[CreatureConstants.Scorpion_Monstrous_Tiny][GenderConstants.Female] = GetBaseFromRange(1, 2);
            appearances[CreatureConstants.Scorpion_Monstrous_Tiny][GenderConstants.Male] = GetBaseFromRange(1, 2);
            appearances[CreatureConstants.Scorpion_Monstrous_Tiny][CreatureConstants.Scorpion_Monstrous_Tiny] = GetMultiplierFromRange(1, 2);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Scorpion,_Small
            appearances[CreatureConstants.Scorpion_Monstrous_Small][GenderConstants.Female] = GetBaseFromAverage(3);
            appearances[CreatureConstants.Scorpion_Monstrous_Small][GenderConstants.Male] = GetBaseFromAverage(3);
            appearances[CreatureConstants.Scorpion_Monstrous_Small][CreatureConstants.Scorpion_Monstrous_Small] = GetMultiplierFromAverage(3);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Scorpion,_Medium
            appearances[CreatureConstants.Scorpion_Monstrous_Medium][GenderConstants.Female] = GetBaseFromAverage(6);
            appearances[CreatureConstants.Scorpion_Monstrous_Medium][GenderConstants.Male] = GetBaseFromAverage(6);
            appearances[CreatureConstants.Scorpion_Monstrous_Medium][CreatureConstants.Scorpion_Monstrous_Medium] = GetMultiplierFromAverage(6);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Scorpion,_Large
            appearances[CreatureConstants.Scorpion_Monstrous_Large][GenderConstants.Female] = GetBaseFromAverage(18);
            appearances[CreatureConstants.Scorpion_Monstrous_Large][GenderConstants.Male] = GetBaseFromAverage(18);
            appearances[CreatureConstants.Scorpion_Monstrous_Large][CreatureConstants.Scorpion_Monstrous_Large] = GetMultiplierFromAverage(18);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Scorpion,_Huge
            appearances[CreatureConstants.Scorpion_Monstrous_Huge][GenderConstants.Female] = GetBaseFromAverage(2 * 12 + 6);
            appearances[CreatureConstants.Scorpion_Monstrous_Huge][GenderConstants.Male] = GetBaseFromAverage(2 * 12 + 6);
            appearances[CreatureConstants.Scorpion_Monstrous_Huge][CreatureConstants.Scorpion_Monstrous_Huge] = GetMultiplierFromAverage(2 * 12 + 6);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Scorpion,_Gargantuan
            appearances[CreatureConstants.Scorpion_Monstrous_Gargantuan][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Scorpion_Monstrous_Gargantuan][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Scorpion_Monstrous_Gargantuan][CreatureConstants.Scorpion_Monstrous_Gargantuan] = GetMultiplierFromAverage(5 * 12);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Scorpion,_Colossal
            appearances[CreatureConstants.Scorpion_Monstrous_Colossal][GenderConstants.Female] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.Scorpion_Monstrous_Colossal][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.Scorpion_Monstrous_Colossal][CreatureConstants.Scorpion_Monstrous_Colossal] = GetMultiplierFromAverage(10 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Tlincalli
            appearances[CreatureConstants.Scorpionfolk][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Scorpionfolk][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Scorpionfolk][CreatureConstants.Scorpionfolk] = GetMultiplierFromAverage(6 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Sea_cat
            appearances[CreatureConstants.SeaCat][GenderConstants.Female] = "0";
            appearances[CreatureConstants.SeaCat][GenderConstants.Male] = "0";
            appearances[CreatureConstants.SeaCat][CreatureConstants.SeaCat] = "0";
            //Source: https://www.d20srd.org/srd/monsters/hag.htm - copy from Human
            appearances[CreatureConstants.SeaHag][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.SeaHag][CreatureConstants.SeaHag] = "2d10";
            //Source: https://www.d20srd.org/srd/monsters/shadow.htm
            appearances[CreatureConstants.Shadow][GenderConstants.Agender] = GetBaseFromRange(5 * 12, 6 * 12);
            appearances[CreatureConstants.Shadow][CreatureConstants.Shadow] = GetMultiplierFromRange(5 * 12, 6 * 12);
            appearances[CreatureConstants.Shadow_Greater][GenderConstants.Agender] = GetBaseFromRange(5 * 12, 6 * 12);
            appearances[CreatureConstants.Shadow_Greater][CreatureConstants.Shadow_Greater] = GetMultiplierFromRange(5 * 12, 6 * 12);
            //Source: https://www.d20srd.org/srd/monsters/shadowMastiff.htm
            appearances[CreatureConstants.ShadowMastiff][GenderConstants.Female] = GetBaseFromAverage(2 * 12);
            appearances[CreatureConstants.ShadowMastiff][GenderConstants.Male] = GetBaseFromAverage(2 * 12);
            appearances[CreatureConstants.ShadowMastiff][CreatureConstants.ShadowMastiff] = GetMultiplierFromAverage(2 * 12);
            //Source: https://www.d20srd.org/srd/monsters/shamblingMound.htm
            appearances[CreatureConstants.ShamblingMound][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.ShamblingMound][CreatureConstants.ShamblingMound] = GetMultiplierFromAverage(6 * 12);
            //Source: https://www.dimensions.com/element/blacktip-shark-carcharhinus-limbatus
            appearances[CreatureConstants.Shark_Medium][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Shark_Medium][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Shark_Medium][CreatureConstants.Shark_Medium] = "0";
            //Source: https://www.dimensions.com/element/thresher-shark
            appearances[CreatureConstants.Shark_Large][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Shark_Large][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Shark_Large][CreatureConstants.Shark_Large] = "0";
            //Source: https://www.dimensions.com/element/great-white-shark
            appearances[CreatureConstants.Shark_Huge][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Shark_Huge][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Shark_Huge][CreatureConstants.Shark_Huge] = "0";
            appearances[CreatureConstants.Shark_Dire][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Shark_Dire][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Shark_Dire][CreatureConstants.Shark_Dire] = "0";
            //Source: https://www.d20srd.org/srd/monsters/shieldGuardian.htm
            appearances[CreatureConstants.ShieldGuardian][GenderConstants.Agender] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.ShieldGuardian][CreatureConstants.ShieldGuardian] = GetMultiplierFromAverage(9 * 12);
            //Source: https://www.d20srd.org/srd/monsters/shockerLizard.htm
            appearances[CreatureConstants.ShockerLizard][GenderConstants.Female] = GetBaseFromAverage(12);
            appearances[CreatureConstants.ShockerLizard][GenderConstants.Male] = GetBaseFromAverage(12);
            appearances[CreatureConstants.ShockerLizard][CreatureConstants.ShockerLizard] = GetMultiplierFromAverage(12);
            //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/shrieker-species
            appearances[CreatureConstants.Shrieker][GenderConstants.Agender] = GetBaseFromAverage(3 * 12);
            appearances[CreatureConstants.Shrieker][CreatureConstants.Shrieker] = GetMultiplierFromAverage(3 * 12);
            //Source: https://www.d20srd.org/srd/monsters/skum.htm Copying from Human
            appearances[CreatureConstants.Skum][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Skum][GenderConstants.Male] = "4*12+10";
            appearances[CreatureConstants.Skum][CreatureConstants.Skum] = "2d10";
            //Source: https://forgottenrealms.fandom.com/wiki/Blue_slaad
            appearances[CreatureConstants.Slaad_Blue][GenderConstants.Agender] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.Slaad_Blue][CreatureConstants.Slaad_Blue] = GetMultiplierFromAverage(10 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Red_slaad
            appearances[CreatureConstants.Slaad_Red][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Slaad_Red][CreatureConstants.Slaad_Red] = GetMultiplierFromAverage(8 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Green_slaad
            appearances[CreatureConstants.Slaad_Green][GenderConstants.Agender] = GetBaseFromAverage(7 * 12);
            appearances[CreatureConstants.Slaad_Green][CreatureConstants.Slaad_Green] = GetMultiplierFromAverage(7 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Gray_slaad
            appearances[CreatureConstants.Slaad_Gray][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Slaad_Gray][CreatureConstants.Slaad_Gray] = GetMultiplierFromAverage(6 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Gray_slaad
            appearances[CreatureConstants.Slaad_Death][GenderConstants.Agender] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Slaad_Death][CreatureConstants.Slaad_Death] = GetMultiplierFromAverage(6 * 12);
            //Source: https://www.dimensions.com/element/green-tree-python-morelia-viridis
            appearances[CreatureConstants.Snake_Constrictor][GenderConstants.Female] = GetBaseFromRange(1, 2);
            appearances[CreatureConstants.Snake_Constrictor][GenderConstants.Male] = GetBaseFromRange(1, 2);
            appearances[CreatureConstants.Snake_Constrictor][CreatureConstants.Snake_Constrictor] = GetMultiplierFromRange(1, 2);
            //Source: https://www.dimensions.com/element/burmese-python-python-bivittatus
            appearances[CreatureConstants.Snake_Constrictor_Giant][GenderConstants.Female] = GetBaseFromRange(3, 9);
            appearances[CreatureConstants.Snake_Constrictor_Giant][GenderConstants.Male] = GetBaseFromRange(3, 9);
            appearances[CreatureConstants.Snake_Constrictor_Giant][CreatureConstants.Snake_Constrictor_Giant] = GetMultiplierFromRange(3, 9);
            //Source: https://www.dimensions.com/element/ribbon-snake-thamnophis-saurita 
            appearances[CreatureConstants.Snake_Viper_Tiny][GenderConstants.Female] = GetBaseFromAverage(1);
            appearances[CreatureConstants.Snake_Viper_Tiny][GenderConstants.Male] = GetBaseFromAverage(1);
            appearances[CreatureConstants.Snake_Viper_Tiny][CreatureConstants.Snake_Viper_Tiny] = GetMultiplierFromAverage(1);
            //Source: https://www.dimensions.com/element/copperhead-agkistrodon-contortrix 
            appearances[CreatureConstants.Snake_Viper_Small][GenderConstants.Female] = GetBaseFromRange(1, 2);
            appearances[CreatureConstants.Snake_Viper_Small][GenderConstants.Male] = GetBaseFromRange(1, 2);
            appearances[CreatureConstants.Snake_Viper_Small][CreatureConstants.Snake_Viper_Small] = GetMultiplierFromRange(1, 2);
            //Source: https://www.dimensions.com/element/western-diamondback-rattlesnake-crotalus-atrox 
            appearances[CreatureConstants.Snake_Viper_Medium][GenderConstants.Female] = GetBaseFromRange(1, 3);
            appearances[CreatureConstants.Snake_Viper_Medium][GenderConstants.Male] = GetBaseFromRange(1, 3);
            appearances[CreatureConstants.Snake_Viper_Medium][CreatureConstants.Snake_Viper_Medium] = GetMultiplierFromRange(1, 3);
            //Source: https://www.dimensions.com/element/black-mamba-dendroaspis-polylepis 
            appearances[CreatureConstants.Snake_Viper_Large][GenderConstants.Female] = GetBaseFromRange(2, 4);
            appearances[CreatureConstants.Snake_Viper_Large][GenderConstants.Male] = GetBaseFromRange(2, 4);
            appearances[CreatureConstants.Snake_Viper_Large][CreatureConstants.Snake_Viper_Large] = GetMultiplierFromRange(2, 4);
            //Source: https://www.dimensions.com/element/king-cobra-ophiophagus-hannah 
            appearances[CreatureConstants.Snake_Viper_Huge][GenderConstants.Female] = GetBaseFromRange(3, 6);
            appearances[CreatureConstants.Snake_Viper_Huge][GenderConstants.Male] = GetBaseFromRange(3, 6);
            appearances[CreatureConstants.Snake_Viper_Huge][CreatureConstants.Snake_Viper_Huge] = GetMultiplierFromRange(3, 6);
            //Source: https://www.d20srd.org/srd/monsters/spectre.htm Copying from Human
            appearances[CreatureConstants.Spectre][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Spectre][GenderConstants.Male] = "4*12+10";
            appearances[CreatureConstants.Spectre][CreatureConstants.Spectre] = "2d10";
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Tiny
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Tiny][GenderConstants.Female] = GetBaseFromAverage(2);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Tiny][GenderConstants.Male] = GetBaseFromAverage(2);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Tiny][CreatureConstants.Spider_Monstrous_Hunter_Tiny] = GetMultiplierFromAverage(2);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Small
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Small][GenderConstants.Female] = GetBaseFromAverage(3);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Small][GenderConstants.Male] = GetBaseFromAverage(3);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Small][CreatureConstants.Spider_Monstrous_Hunter_Small] = GetMultiplierFromAverage(3);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Medium
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Medium][GenderConstants.Female] = GetBaseFromAverage(6);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Medium][GenderConstants.Male] = GetBaseFromAverage(6);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Medium][CreatureConstants.Spider_Monstrous_Hunter_Medium] = GetMultiplierFromAverage(6);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Large
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Large][GenderConstants.Female] = GetBaseFromAverage(18);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Large][GenderConstants.Male] = GetBaseFromAverage(18);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Large][CreatureConstants.Spider_Monstrous_Hunter_Large] = GetMultiplierFromAverage(18);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Huge
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Huge][GenderConstants.Female] = GetBaseFromAverage(2 * 12 + 6);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Huge][GenderConstants.Male] = GetBaseFromAverage(2 * 12 + 6);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Huge][CreatureConstants.Spider_Monstrous_Hunter_Huge] = GetMultiplierFromAverage(2 * 12 + 6);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Gargantuan
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Gargantuan][CreatureConstants.Spider_Monstrous_Hunter_Gargantuan] = GetMultiplierFromAverage(5 * 12);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Colossal
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Colossal][GenderConstants.Female] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Colossal][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.Spider_Monstrous_Hunter_Colossal][CreatureConstants.Spider_Monstrous_Hunter_Colossal] = GetMultiplierFromAverage(10 * 12);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Tiny
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny][GenderConstants.Female] = GetBaseFromAverage(2);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny][GenderConstants.Male] = GetBaseFromAverage(2);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Tiny][CreatureConstants.Spider_Monstrous_WebSpinner_Tiny] = GetMultiplierFromAverage(2);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Small
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Small][GenderConstants.Female] = GetBaseFromAverage(3);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Small][GenderConstants.Male] = GetBaseFromAverage(3);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Small][CreatureConstants.Spider_Monstrous_WebSpinner_Small] = GetMultiplierFromAverage(3);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Medium
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Medium][GenderConstants.Female] = GetBaseFromAverage(6);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Medium][GenderConstants.Male] = GetBaseFromAverage(6);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Medium][CreatureConstants.Spider_Monstrous_WebSpinner_Medium] = GetMultiplierFromAverage(6);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Large
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Large][GenderConstants.Female] = GetBaseFromAverage(18);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Large][GenderConstants.Male] = GetBaseFromAverage(18);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Large][CreatureConstants.Spider_Monstrous_WebSpinner_Large] = GetMultiplierFromAverage(18);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Huge
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Huge][GenderConstants.Female] = GetBaseFromAverage(2 * 12 + 6);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Huge][GenderConstants.Male] = GetBaseFromAverage(2 * 12 + 6);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Huge][CreatureConstants.Spider_Monstrous_WebSpinner_Huge] = GetMultiplierFromAverage(2 * 12 + 6);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Gargantuan
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan][GenderConstants.Female] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan][CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan] = GetMultiplierFromAverage(5 * 12);
            //Source: https://www.realmshelps.net/monsters/block/Monstrous_Spider,_Colossal
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal][GenderConstants.Female] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal][GenderConstants.Male] = GetBaseFromAverage(10 * 12);
            appearances[CreatureConstants.Spider_Monstrous_WebSpinner_Colossal][CreatureConstants.Spider_Monstrous_WebSpinner_Colossal] = GetMultiplierFromAverage(10 * 12);
            //Source: https://www.d20srd.org/srd/monsters/swarm.htm
            appearances[CreatureConstants.Spider_Swarm] = new[] { "1,500 spiders" };
            //Source: https://www.d20srd.org/srd/monsters/spiderEater.htm
            appearances[CreatureConstants.SpiderEater][GenderConstants.Female] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.SpiderEater][GenderConstants.Male] = GetBaseFromAverage(4 * 12);
            appearances[CreatureConstants.SpiderEater][CreatureConstants.SpiderEater] = GetMultiplierFromAverage(4 * 12);
            //Source: https://www.dimensions.com/element/humboldt-squid-dosidicus-gigas (mantle length)
            appearances[CreatureConstants.Squid][GenderConstants.Female] = GetBaseFromRange(29, 79);
            appearances[CreatureConstants.Squid][GenderConstants.Male] = GetBaseFromRange(29, 79);
            appearances[CreatureConstants.Squid][CreatureConstants.Squid] = GetMultiplierFromRange(29, 79);
            appearances[CreatureConstants.Squid_Giant][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Squid_Giant][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Squid_Giant][CreatureConstants.Squid_Giant] = "0";
            //Source: https://www.d20srd.org/srd/monsters/giantStagBeetle.htm
            //https://www.dimensions.com/element/hercules-beetle-dynastes-hercules scale up: [.47,1.42]*10*12/[2.36,7.09] = [24,24]
            appearances[CreatureConstants.StagBeetle_Giant][GenderConstants.Female] = GetBaseFromAverage(24);
            appearances[CreatureConstants.StagBeetle_Giant][GenderConstants.Male] = GetBaseFromAverage(24);
            appearances[CreatureConstants.StagBeetle_Giant][CreatureConstants.StagBeetle_Giant] = GetMultiplierFromAverage(24);
            appearances[CreatureConstants.Stirge][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Stirge][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Stirge][CreatureConstants.Stirge] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Succubus
            appearances[CreatureConstants.Succubus][GenderConstants.Female] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Succubus][GenderConstants.Male] = GetBaseFromAverage(6 * 12);
            appearances[CreatureConstants.Succubus][CreatureConstants.Succubus] = GetMultiplierFromAverage(6 * 12);
            //Source: https://www.d20srd.org/srd/monsters/tarrasque.htm
            appearances[CreatureConstants.Tarrasque][GenderConstants.Agender] = GetBaseFromAverage(50 * 12);
            appearances[CreatureConstants.Tarrasque][CreatureConstants.Tarrasque] = GetMultiplierFromAverage(50 * 12);
            //Source: https://www.d20srd.org/srd/monsters/tendriculos.htm
            appearances[CreatureConstants.Tendriculos][GenderConstants.Agender] = GetBaseFromAverage(15 * 12);
            appearances[CreatureConstants.Tendriculos][CreatureConstants.Tendriculos] = GetMultiplierFromAverage(15 * 12);
            //Source: https://www.d20srd.org/srd/monsters/thoqqua.htm
            appearances[CreatureConstants.Thoqqua][GenderConstants.Agender] = GetBaseFromAverage(12);
            appearances[CreatureConstants.Thoqqua][CreatureConstants.Thoqqua] = GetMultiplierFromAverage(12);
            //Source: https://forgottenrealms.fandom.com/wiki/Tiefling
            appearances[CreatureConstants.Tiefling][GenderConstants.Female] = GetBaseFromRange(4 * 12 + 11, 6 * 12);
            appearances[CreatureConstants.Tiefling][GenderConstants.Male] = GetBaseFromRange(4 * 12 + 11, 6 * 12);
            appearances[CreatureConstants.Tiefling][CreatureConstants.Tiefling] = GetMultiplierFromRange(4 * 12 + 11, 6 * 12);
            //Source: https://www.dimensions.com/element/bengal-tiger
            appearances[CreatureConstants.Tiger][GenderConstants.Female] = GetBaseFromRange(34, 45);
            appearances[CreatureConstants.Tiger][GenderConstants.Male] = GetBaseFromRange(34, 45);
            appearances[CreatureConstants.Tiger][CreatureConstants.Tiger] = GetMultiplierFromRange(34, 45);
            //Scaled up from Tiger, x2 based on length
            appearances[CreatureConstants.Tiger_Dire][GenderConstants.Female] = GetBaseFromRange(34 * 2, 45 * 2);
            appearances[CreatureConstants.Tiger_Dire][GenderConstants.Male] = GetBaseFromRange(34 * 2, 45 * 2);
            appearances[CreatureConstants.Tiger_Dire][CreatureConstants.Tiger_Dire] = GetMultiplierFromRange(34 * 2, 45 * 2);
            //Source: https://www.d20srd.org/srd/monsters/titan.htm
            appearances[CreatureConstants.Titan][GenderConstants.Female] = GetBaseFromAverage(25 * 12);
            appearances[CreatureConstants.Titan][GenderConstants.Male] = GetBaseFromAverage(25 * 12);
            appearances[CreatureConstants.Titan][CreatureConstants.Titan] = GetMultiplierFromAverage(25 * 12);
            //Source: https://www.dimensions.com/element/common-toad-bufo-bufo
            appearances[CreatureConstants.Toad][GenderConstants.Female] = GetBaseFromRange(2, 3);
            appearances[CreatureConstants.Toad][GenderConstants.Male] = GetBaseFromRange(2, 3);
            appearances[CreatureConstants.Toad][CreatureConstants.Toad] = GetMultiplierFromRange(2, 3);
            appearances[CreatureConstants.Tojanida_Juvenile][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.Tojanida_Juvenile][CreatureConstants.Tojanida_Juvenile] = "0";
            appearances[CreatureConstants.Tojanida_Adult][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.Tojanida_Adult][CreatureConstants.Tojanida_Adult] = "0";
            appearances[CreatureConstants.Tojanida_Elder][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.Tojanida_Elder][CreatureConstants.Tojanida_Elder] = "0";
            //Source: https://www.d20srd.org/srd/monsters/treant.htm
            appearances[CreatureConstants.Treant][GenderConstants.Female] = GetBaseFromAverage(30 * 12);
            appearances[CreatureConstants.Treant][GenderConstants.Male] = GetBaseFromAverage(30 * 12);
            appearances[CreatureConstants.Treant][CreatureConstants.Treant] = GetMultiplierFromAverage(30 * 12);
            //Source: https://jurassicworld-evolution.fandom.com/wiki/Triceratops
            appearances[CreatureConstants.Triceratops][GenderConstants.Female] = GetBaseFromAverage(157);
            appearances[CreatureConstants.Triceratops][GenderConstants.Male] = GetBaseFromAverage(157);
            appearances[CreatureConstants.Triceratops][CreatureConstants.Triceratops] = GetMultiplierFromAverage(157);
            //Source: https://www.d20srd.org/srd/monsters/triton.htm Copying from Human
            appearances[CreatureConstants.Triton][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Triton][GenderConstants.Male] = "4*12+10";
            appearances[CreatureConstants.Triton][CreatureConstants.Triton] = "2d10";
            //Source: https://forgottenrealms.fandom.com/wiki/Troglodyte
            appearances[CreatureConstants.Troglodyte][GenderConstants.Female] = GetBaseFromRange(5 * 12, 6 * 12);
            appearances[CreatureConstants.Troglodyte][GenderConstants.Male] = GetBaseFromRange(5 * 12, 6 * 12);
            appearances[CreatureConstants.Troglodyte][CreatureConstants.Troglodyte] = GetMultiplierFromRange(5 * 12, 6 * 12);
            //Source: https://www.d20srd.org/srd/monsters/troll.htm Female "slightly larger than males", so 110%
            appearances[CreatureConstants.Troll][GenderConstants.Male] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.Troll][GenderConstants.Female] = GetBaseFromAverage(119, 9 * 12);
            appearances[CreatureConstants.Troll][CreatureConstants.Troll] = GetMultiplierFromAverage(9 * 12);
            appearances[CreatureConstants.Troll_Scrag][GenderConstants.Male] = GetBaseFromAverage(9 * 12);
            appearances[CreatureConstants.Troll_Scrag][GenderConstants.Female] = GetBaseFromAverage(119, 9 * 12);
            appearances[CreatureConstants.Troll_Scrag][CreatureConstants.Troll_Scrag] = GetMultiplierFromAverage(9 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Trumpet_archon
            appearances[CreatureConstants.TrumpetArchon][GenderConstants.Female] = GetBaseFromRange(6 * 12 + 1, 7 * 12 + 4);
            appearances[CreatureConstants.TrumpetArchon][GenderConstants.Male] = GetBaseFromRange(6 * 12 + 6, 7 * 12 + 9);
            appearances[CreatureConstants.TrumpetArchon][CreatureConstants.TrumpetArchon] = GetMultiplierFromRange(6 * 12 + 6, 7 * 12 + 9);
            //Source: https://jurassicworld-evolution.fandom.com/wiki/Tyrannosaurus
            appearances[CreatureConstants.Tyrannosaurus][GenderConstants.Male] = GetBaseFromAverage(232);
            appearances[CreatureConstants.Tyrannosaurus][GenderConstants.Female] = GetBaseFromAverage(232);
            appearances[CreatureConstants.Tyrannosaurus][CreatureConstants.Tyrannosaurus] = GetMultiplierFromAverage(232);
            //Source: https://forgottenrealms.fandom.com/wiki/Umber_hulk
            appearances[CreatureConstants.UmberHulk][GenderConstants.Female] = GetBaseFromRange(4 * 12 + 6, 5 * 12);
            appearances[CreatureConstants.UmberHulk][GenderConstants.Male] = GetBaseFromRange(4 * 12 + 6, 5 * 12);
            appearances[CreatureConstants.UmberHulk][CreatureConstants.UmberHulk] = GetMultiplierFromRange(4 * 12 + 6, 5 * 12);
            //Scale up based on length, X2
            appearances[CreatureConstants.UmberHulk_TrulyHorrid][GenderConstants.Female] = GetBaseFromRange(9 * 12, 10 * 12);
            appearances[CreatureConstants.UmberHulk_TrulyHorrid][GenderConstants.Male] = GetBaseFromRange(9 * 12, 10 * 12);
            appearances[CreatureConstants.UmberHulk_TrulyHorrid][CreatureConstants.UmberHulk_TrulyHorrid] = GetMultiplierFromRange(9 * 12, 10 * 12);
            //Source: https://www.d20srd.org/srd/monsters/unicorn.htm Females "slightly smaller", so 90%
            appearances[CreatureConstants.Unicorn][GenderConstants.Female] = GetBaseFromAverage(54, 5 * 12);
            appearances[CreatureConstants.Unicorn][GenderConstants.Male] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Unicorn][CreatureConstants.Unicorn] = GetMultiplierFromAverage(5 * 12);
            //Source: https://www.d20srd.org/srd/monsters/vampire.htm#vampireSpawn Copying from Human
            appearances[CreatureConstants.VampireSpawn][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.VampireSpawn][GenderConstants.Male] = "4*12+10";
            appearances[CreatureConstants.VampireSpawn][CreatureConstants.VampireSpawn] = "2d10";
            //Source: https://www.d20srd.org/srd/monsters/vargouille.htm
            appearances[CreatureConstants.Vargouille][GenderConstants.Agender] = GetBaseFromAverage(18);
            appearances[CreatureConstants.Vargouille][CreatureConstants.Vargouille] = GetMultiplierFromAverage(18);
            //Source: https://www.worldanvil.com/w/faerun-tatortotzke/a/violet-fungus-species
            appearances[CreatureConstants.VioletFungus][GenderConstants.Agender] = GetBaseFromRange(4 * 12, 7 * 12);
            appearances[CreatureConstants.VioletFungus][CreatureConstants.VioletFungus] = GetMultiplierFromRange(4 * 12, 7 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Vrock
            appearances[CreatureConstants.Vrock][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Vrock][CreatureConstants.Vrock] = GetMultiplierFromAverage(8 * 12);
            //Source: https://www.dimensions.com/element/red-paper-wasp-polistes-carolina
            //https://forgottenrealms.fandom.com/wiki/Giant_wasp scale up: [.23,.33]*5*12/[.94,1.26] = [14,16]
            appearances[CreatureConstants.Wasp_Giant][GenderConstants.Male] = GetBaseFromRange(14, 16);
            appearances[CreatureConstants.Wasp_Giant][CreatureConstants.Wasp_Giant] = GetMultiplierFromRange(14, 16);
            //Source: https://www.dimensions.com/element/least-weasel-mustela-nivalis
            appearances[CreatureConstants.Weasel][GenderConstants.Female] = GetBaseFromRange(2, 3);
            appearances[CreatureConstants.Weasel][GenderConstants.Male] = GetBaseFromRange(2, 3);
            appearances[CreatureConstants.Weasel][CreatureConstants.Weasel] = GetMultiplierFromRange(2, 3);
            //Scaled up from weasel, x15 based on length
            appearances[CreatureConstants.Weasel_Dire][GenderConstants.Female] = GetBaseFromRange(30, 45);
            appearances[CreatureConstants.Weasel_Dire][GenderConstants.Male] = GetBaseFromRange(30, 45);
            appearances[CreatureConstants.Weasel_Dire][CreatureConstants.Weasel_Dire] = GetMultiplierFromRange(30, 45);
            //Source: https://www.dimensions.com/element/humpback-whale-megaptera-novaeangliae
            appearances[CreatureConstants.Whale_Baleen][GenderConstants.Female] = GetBaseFromRange(8 * 12, 9 * 12 + 8);
            appearances[CreatureConstants.Whale_Baleen][GenderConstants.Male] = GetBaseFromRange(8 * 12, 9 * 12 + 8);
            appearances[CreatureConstants.Whale_Baleen][CreatureConstants.Whale_Baleen] = GetMultiplierFromRange(8 * 12, 9 * 12 + 8);
            //Source: https://www.dimensions.com/element/sperm-whale-physeter-macrocephalus
            appearances[CreatureConstants.Whale_Cachalot][GenderConstants.Female] = GetBaseFromRange(6 * 12 + 9, 11 * 12);
            appearances[CreatureConstants.Whale_Cachalot][GenderConstants.Male] = GetBaseFromRange(6 * 12 + 9, 11 * 12);
            appearances[CreatureConstants.Whale_Cachalot][CreatureConstants.Whale_Cachalot] = GetMultiplierFromRange(6 * 12 + 9, 11 * 12);
            //Source: https://www.dimensions.com/element/orca-killer-whale-orcinus-orca
            appearances[CreatureConstants.Whale_Orca][GenderConstants.Female] = GetBaseFromRange(5 * 12 + 3, 7 * 12 + 6);
            appearances[CreatureConstants.Whale_Orca][GenderConstants.Male] = GetBaseFromRange(5 * 12 + 3, 7 * 12 + 6);
            appearances[CreatureConstants.Whale_Orca][CreatureConstants.Whale_Orca] = GetMultiplierFromRange(5 * 12 + 3, 7 * 12 + 6);
            //Source: https://www.d20srd.org/srd/monsters/wight.htm Copy from Human
            appearances[CreatureConstants.Wight][GenderConstants.Female] = "4*12+5";
            appearances[CreatureConstants.Wight][GenderConstants.Male] = "4*12+10";
            appearances[CreatureConstants.Wight][CreatureConstants.Wight] = "2d10";
            //Source: https://www.d20srd.org/srd/monsters/willOWisp.htm
            appearances[CreatureConstants.WillOWisp][GenderConstants.Agender] = GetBaseFromAverage(12);
            appearances[CreatureConstants.WillOWisp][CreatureConstants.WillOWisp] = GetMultiplierFromAverage(12);
            //Source: https://www.d20srd.org/srd/monsters/winterWolf.htm
            appearances[CreatureConstants.WinterWolf][GenderConstants.Female] = GetBaseFromAverage(4 * 12 + 6);
            appearances[CreatureConstants.WinterWolf][GenderConstants.Male] = GetBaseFromAverage(4 * 12 + 6);
            appearances[CreatureConstants.WinterWolf][CreatureConstants.WinterWolf] = GetMultiplierFromAverage(4 * 12 + 6);
            //Source: https://www.dimensions.com/element/gray-wolf
            appearances[CreatureConstants.Wolf][GenderConstants.Female] = GetBaseFromRange(26, 33);
            appearances[CreatureConstants.Wolf][GenderConstants.Male] = GetBaseFromRange(26, 33);
            appearances[CreatureConstants.Wolf][CreatureConstants.Wolf] = GetMultiplierFromRange(26, 33);
            //Scaled up from Wolf, x2 based on length
            appearances[CreatureConstants.Wolf_Dire][GenderConstants.Female] = GetBaseFromRange(26 * 2, 33 * 2);
            appearances[CreatureConstants.Wolf_Dire][GenderConstants.Male] = GetBaseFromRange(26 * 2, 33 * 2);
            appearances[CreatureConstants.Wolf_Dire][CreatureConstants.Wolf_Dire] = GetMultiplierFromRange(26 * 2, 33 * 2);
            //Source: https://www.dimensions.com/element/wolverine-gulo-gulo
            appearances[CreatureConstants.Wolverine][GenderConstants.Female] = GetBaseFromRange(14, 21);
            appearances[CreatureConstants.Wolverine][GenderConstants.Male] = GetBaseFromRange(14, 21);
            appearances[CreatureConstants.Wolverine][CreatureConstants.Wolverine] = GetMultiplierFromRange(14, 21);
            //Scaled up from Wolverine, x4 based on length
            appearances[CreatureConstants.Wolverine_Dire][GenderConstants.Female] = GetBaseFromRange(14 * 4, 21 * 4);
            appearances[CreatureConstants.Wolverine_Dire][GenderConstants.Male] = GetBaseFromRange(14 * 4, 21 * 4);
            appearances[CreatureConstants.Wolverine_Dire][CreatureConstants.Wolverine_Dire] = GetMultiplierFromRange(14 * 4, 21 * 4);
            //Source: https://www.d20srd.org/srd/monsters/worg.htm
            appearances[CreatureConstants.Worg][GenderConstants.Female] = GetBaseFromAverage(3 * 12);
            appearances[CreatureConstants.Worg][GenderConstants.Male] = GetBaseFromAverage(3 * 12);
            appearances[CreatureConstants.Worg][CreatureConstants.Worg] = GetMultiplierFromAverage(3 * 12);
            //Source: https://www.d20srd.org/srd/monsters/wraith.htm W:Human, DW:Ogre
            appearances[CreatureConstants.Wraith][GenderConstants.Agender] = "4*12+10";
            appearances[CreatureConstants.Wraith][CreatureConstants.Wraith] = "2d10";
            appearances[CreatureConstants.Wraith_Dread][GenderConstants.Agender] = "96";
            appearances[CreatureConstants.Wraith_Dread][CreatureConstants.Wraith_Dread] = "2d12";
            //Source: https://forgottenrealms.fandom.com/wiki/Wyvern
            appearances[CreatureConstants.Wyvern][GenderConstants.Female] = "0";
            appearances[CreatureConstants.Wyvern][GenderConstants.Male] = "0";
            appearances[CreatureConstants.Wyvern][CreatureConstants.Wyvern] = "0";
            //Source: https://www.d20srd.org/srd/monsters/xill.htm
            appearances[CreatureConstants.Xill][GenderConstants.Agender] = GetBaseFromRange(4 * 12, 5 * 12);
            appearances[CreatureConstants.Xill][CreatureConstants.Xill] = GetMultiplierFromRange(4 * 12, 5 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Xorn
            appearances[CreatureConstants.Xorn_Minor][GenderConstants.Agender] = GetBaseFromAverage(3 * 12);
            appearances[CreatureConstants.Xorn_Minor][CreatureConstants.Xorn_Minor] = GetMultiplierFromAverage(3 * 12);
            appearances[CreatureConstants.Xorn_Average][GenderConstants.Agender] = GetBaseFromAverage(5 * 12);
            appearances[CreatureConstants.Xorn_Average][CreatureConstants.Xorn_Average] = GetMultiplierFromAverage(5 * 12);
            appearances[CreatureConstants.Xorn_Elder][GenderConstants.Agender] = GetBaseFromAverage(8 * 12);
            appearances[CreatureConstants.Xorn_Elder][CreatureConstants.Xorn_Elder] = GetMultiplierFromAverage(8 * 12);
            //Source: https://forgottenrealms.fandom.com/wiki/Yeth_hound
            appearances[CreatureConstants.YethHound][GenderConstants.Agender] = GetBaseFromRange(4 * 12, 5 * 12);
            appearances[CreatureConstants.YethHound][CreatureConstants.YethHound] = GetMultiplierFromRange(4 * 12, 5 * 12);
            //Source: https://www.d20srd.org/srd/monsters/yrthak.htm
            appearances[CreatureConstants.Yrthak][GenderConstants.Agender] = "0";
            appearances[CreatureConstants.Yrthak][CreatureConstants.Yrthak] = "0";
            //Source: https://forgottenrealms.fandom.com/wiki/Yuan-ti_pureblood
            appearances[CreatureConstants.YuanTi_Pureblood][GenderConstants.Female] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Pureblood][GenderConstants.Male] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Pureblood][CreatureConstants.YuanTi_Pureblood] = GetMultiplierFromRange(4 * 12 + 7, 6 * 12 + 6);
            //Source: https://forgottenrealms.fandom.com/wiki/Yuan-ti_malison
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeArms][GenderConstants.Female] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeArms][GenderConstants.Male] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeArms][CreatureConstants.YuanTi_Halfblood_SnakeArms] = GetMultiplierFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeHead][GenderConstants.Female] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeHead][GenderConstants.Male] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeHead][CreatureConstants.YuanTi_Halfblood_SnakeHead] = GetMultiplierFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeTail][GenderConstants.Female] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeTail][GenderConstants.Male] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeTail][CreatureConstants.YuanTi_Halfblood_SnakeTail] = GetMultiplierFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs][GenderConstants.Female] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs][GenderConstants.Male] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs][CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs] =
                GetMultiplierFromRange(4 * 12 + 7, 6 * 12 + 6);
            //Source: https://forgottenrealms.fandom.com/wiki/Yuan-ti_abomination (assuming height based on other yuan-ti)
            appearances[CreatureConstants.YuanTi_Abomination][GenderConstants.Female] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Abomination][GenderConstants.Male] = GetBaseFromRange(4 * 12 + 7, 6 * 12 + 6);
            appearances[CreatureConstants.YuanTi_Abomination][CreatureConstants.YuanTi_Abomination] = GetMultiplierFromRange(4 * 12 + 7, 6 * 12 + 6);
            //Source: https://forgottenrealms.fandom.com/wiki/Zelekhut using centaur stats
            appearances[CreatureConstants.Zelekhut][GenderConstants.Agender] = GetBaseFromRange(7 * 12, 9 * 12);
            appearances[CreatureConstants.Zelekhut][CreatureConstants.Zelekhut] = GetMultiplierFromRange(7 * 12, 9 * 12);

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

            var builtCommon = common.Select(a =>
                (GetAppearancePrototype(prototype.Appearance, a),
                GetRarity(prototype.Rarity, Rarity.Common)));
            var builtUncommon = uncommon.Select(a =>
                (GetAppearancePrototype(prototype.Appearance, a),
                GetRarity(prototype.Rarity, Rarity.Uncommon)));
            var builtRare = rare.Select(a =>
                (GetAppearancePrototype(prototype.Appearance, a),
                GetRarity(prototype.Rarity, Rarity.Rare)));

            return builtCommon.Concat(builtUncommon).Concat(builtRare);
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
