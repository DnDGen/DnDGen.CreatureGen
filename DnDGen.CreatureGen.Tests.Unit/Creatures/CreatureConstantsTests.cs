using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Magics;
using NUnit.Framework;
using System.Linq;
using System.Reflection;

namespace DnDGen.CreatureGen.Tests.Unit.Creatures
{
    [TestFixture]
    public class CreatureConstantsTests
    {
        [TestCase(CreatureConstants.Aasimar, "Aasimar")]
        [TestCase(CreatureConstants.Aboleth, "Aboleth")]
        [TestCase(CreatureConstants.Achaierai, "Achaierai")]
        [TestCase(CreatureConstants.Allip, "Allip")]
        [TestCase(CreatureConstants.Androsphinx, "Androsphinx")]
        [TestCase(CreatureConstants.Ankheg, "Ankheg")]
        [TestCase(CreatureConstants.Annis, "Annis")]
        [TestCase(CreatureConstants.Ape, "Ape")]
        [TestCase(CreatureConstants.Ape_Dire, "Dire Ape")]
        [TestCase(CreatureConstants.Aranea, "Aranea")]
        [TestCase(CreatureConstants.AssassinVine, "Assassin Vine")]
        [TestCase(CreatureConstants.Angel_AstralDeva, "Angel, Astral Deva")]
        [TestCase(CreatureConstants.Athach, "Athach")]
        [TestCase(CreatureConstants.Avoral, "Avoral")]
        [TestCase(CreatureConstants.Azer, "Azer")]
        [TestCase(CreatureConstants.Babau, "Babau")]
        [TestCase(CreatureConstants.Baboon, "Baboon")]
        [TestCase(CreatureConstants.Badger, "Badger")]
        [TestCase(CreatureConstants.Badger_Dire, "Dire Badger")]
        [TestCase(CreatureConstants.Balor, "Balor")]
        [TestCase(CreatureConstants.Barghest, "Barghest")]
        [TestCase(CreatureConstants.Basilisk, "Basilisk")]
        [TestCase(CreatureConstants.Bat, "Bat")]
        [TestCase(CreatureConstants.Bat_Dire, "Dire Bat")]
        [TestCase(CreatureConstants.Bat_Swarm, "Bat Swarm")]
        [TestCase(CreatureConstants.Bear_Black, "Black Bear")]
        [TestCase(CreatureConstants.Bear_Brown, "Brown Bear")]
        [TestCase(CreatureConstants.Bear_Dire, "Dire Bear")]
        [TestCase(CreatureConstants.Bear_Polar, "Polar Bear")]
        [TestCase(CreatureConstants.Bebilith, "Bebilith")]
        [TestCase(CreatureConstants.Bee_Giant, "Giant Bee")]
        [TestCase(CreatureConstants.Behir, "Behir")]
        [TestCase(CreatureConstants.Beholder, "Beholder")]
        [TestCase(CreatureConstants.Beholder_Gauth, "Gauth (Lesser Beholder)")]
        [TestCase(CreatureConstants.Belker, "Belker")]
        [TestCase(CreatureConstants.Bison, "Bison")]
        [TestCase(CreatureConstants.BlackPudding, "Black Pudding")]
        [TestCase(CreatureConstants.BlinkDog, "Blink Dog")]
        [TestCase(CreatureConstants.Boar, "Boar")]
        [TestCase(CreatureConstants.Boar_Dire, "Dire Boar")]
        [TestCase(CreatureConstants.Bodak, "Bodak")]
        [TestCase(CreatureConstants.BombardierBeetle_Giant, "Giant Bombardier Beetle")]
        [TestCase(CreatureConstants.Bralani, "Bralani")]
        [TestCase(CreatureConstants.Bugbear, "Bugbear")]
        [TestCase(CreatureConstants.Bulette, "Bulette")]
        [TestCase(CreatureConstants.Camel_Bactrian, "Camel, Bactrian (Two-Humped)")]
        [TestCase(CreatureConstants.Camel_Dromedary, "Camel, Dromedary (One-Humped)")]
        [TestCase(CreatureConstants.CarrionCrawler, "Carrion Crawler")]
        [TestCase(CreatureConstants.Cat, "Cat")]
        [TestCase(CreatureConstants.Centaur, "Centaur")]
        [TestCase(CreatureConstants.Centipede_Swarm, "Centipede Swarm")]
        [TestCase(CreatureConstants.ChaosBeast, "Chaos Beast")]
        [TestCase(CreatureConstants.Cheetah, "Cheetah")]
        [TestCase(CreatureConstants.Chimera_Black, "Chimera (Black Dragon head)")]
        [TestCase(CreatureConstants.Chimera_Blue, "Chimera (Blue Dragon head)")]
        [TestCase(CreatureConstants.Chimera_Green, "Chimera (Green Dragon head)")]
        [TestCase(CreatureConstants.Chimera_Red, "Chimera (Red Dragon head)")]
        [TestCase(CreatureConstants.Chimera_White, "Chimera (White Dragon head)")]
        [TestCase(CreatureConstants.Choker, "Choker")]
        [TestCase(CreatureConstants.Chuul, "Chuul")]
        [TestCase(CreatureConstants.Cloaker, "Cloaker")]
        [TestCase(CreatureConstants.Cockatrice, "Cockatrice")]
        [TestCase(CreatureConstants.Couatl, "Couatl")]
        [TestCase(CreatureConstants.Criosphinx, "Criosphinx")]
        [TestCase(CreatureConstants.Crocodile, "Crocodile")]
        [TestCase(CreatureConstants.Crocodile_Giant, "Giant Crocodile")]
        [TestCase(CreatureConstants.Darkmantle, "Darkmantle")]
        [TestCase(CreatureConstants.Deinonychus, "Deinonychus")]
        [TestCase(CreatureConstants.Derro, "Derro")]
        [TestCase(CreatureConstants.Destrachan, "Destrachan")]
        [TestCase(CreatureConstants.Devourer, "Devourer")]
        [TestCase(CreatureConstants.Delver, "Delver")]
        [TestCase(CreatureConstants.Digester, "Digester")]
        [TestCase(CreatureConstants.DisplacerBeast, "Displacer Beast")]
        [TestCase(CreatureConstants.Djinni, "Djinni")]
        [TestCase(CreatureConstants.Dog, "Dog")]
        [TestCase(CreatureConstants.Donkey, "Donkey")]
        [TestCase(CreatureConstants.Doppelganger, "Doppelganger")]
        [TestCase(CreatureConstants.DragonTurtle, "Dragon Turtle")]
        [TestCase(CreatureConstants.Dragonne, "Dragonne")]
        [TestCase(CreatureConstants.Dretch, "Dretch")]
        [TestCase(CreatureConstants.Drider, "Drider")]
        [TestCase(CreatureConstants.Dryad, "Dryad")]
        [TestCase(CreatureConstants.Dwarf_Duergar, "Duergar")]
        [TestCase(CreatureConstants.Dwarf_Deep, "Deep Dwarf")]
        [TestCase(CreatureConstants.Dwarf_Hill, "Hill Dwarf")]
        [TestCase(CreatureConstants.Dwarf_Mountain, "Mountain Dwarf")]
        [TestCase(CreatureConstants.Eagle, "Eagle")]
        [TestCase(CreatureConstants.Eagle_Giant, "Giant Eagle")]
        [TestCase(CreatureConstants.Efreeti, "Efreeti")]
        [TestCase(CreatureConstants.Elasmosaurus, "Elasmosaurus")]
        [TestCase(CreatureConstants.Elephant, "Elephant")]
        [TestCase(CreatureConstants.Elf_Aquatic, "Aquatic Elf")]
        [TestCase(CreatureConstants.Elf_Drow, "Drow")]
        [TestCase(CreatureConstants.Elf_Gray, "Gray Elf")]
        [TestCase(CreatureConstants.Elf_High, "High Elf")]
        [TestCase(CreatureConstants.Elf_Half, "Half-Elf")]
        [TestCase(CreatureConstants.Elf_Wood, "Wood Elf")]
        [TestCase(CreatureConstants.Elf_Wild, "Wild Elf")]
        [TestCase(CreatureConstants.Erinyes, "Erinyes")]
        [TestCase(CreatureConstants.EtherealFilcher, "Ethereal Filcher")]
        [TestCase(CreatureConstants.EtherealMarauder, "Ethereal Marauder")]
        [TestCase(CreatureConstants.Ettercap, "Ettercap")]
        [TestCase(CreatureConstants.Ettin, "Ettin")]
        [TestCase(CreatureConstants.FireBeetle_Giant, "Giant Fire Beetle")]
        [TestCase(CreatureConstants.FrostWorm, "Frost Worm")]
        [TestCase(CreatureConstants.Gargoyle, "Gargoyle")]
        [TestCase(CreatureConstants.GelatinousCube, "Gelatinous Cube")]
        [TestCase(CreatureConstants.Ghaele, "Ghaele")]
        [TestCase(CreatureConstants.Ghoul, "Ghoul")]
        [TestCase(CreatureConstants.Giant_Cloud, "Cloud Giant")]
        [TestCase(CreatureConstants.Giant_Fire, "Fire Giant")]
        [TestCase(CreatureConstants.Giant_Frost, "Frost Giant")]
        [TestCase(CreatureConstants.Giant_Hill, "Hill Giant")]
        [TestCase(CreatureConstants.Giant_Stone, "Stone Giant")]
        [TestCase(CreatureConstants.Giant_Storm, "Storm Giant")]
        [TestCase(CreatureConstants.GibberingMouther, "Gibbering Mouther")]
        [TestCase(CreatureConstants.Girallon, "Girallon")]
        [TestCase(CreatureConstants.Githyanki, "Githyanki")]
        [TestCase(CreatureConstants.Githzerai, "Githzerai")]
        [TestCase(CreatureConstants.Glabrezu, "Glabrezu")]
        [TestCase(CreatureConstants.Gnoll, "Gnoll")]
        [TestCase(CreatureConstants.Gnome_Forest, "Forest Gnome")]
        [TestCase(CreatureConstants.Gnome_Rock, "Rock Gnome")]
        [TestCase(CreatureConstants.Goblin, "Goblin")]
        [TestCase(CreatureConstants.Gorgon, "Gorgon")]
        [TestCase(CreatureConstants.GrayRender, "Gray Render")]
        [TestCase(CreatureConstants.GreenHag, "Green Hag")]
        [TestCase(CreatureConstants.Grick, "Grick")]
        [TestCase(CreatureConstants.Griffon, "Griffon")]
        [TestCase(CreatureConstants.Grig, "Grig")]
        [TestCase(CreatureConstants.Grig_WithFiddle, "Grig with fiddle")]
        [TestCase(CreatureConstants.Grimlock, "Grimlock")]
        [TestCase(CreatureConstants.Gynosphinx, "Gynosphinx")]
        [TestCase(CreatureConstants.Halfling_Deep, "Deep Halfling")]
        [TestCase(CreatureConstants.Halfling_Lightfoot, "Lightfoot Halfling")]
        [TestCase(CreatureConstants.Halfling_Tallfellow, "Tallfellow Halfling")]
        [TestCase(CreatureConstants.Harpy, "Harpy")]
        [TestCase(CreatureConstants.Hawk, "Hawk")]
        [TestCase(CreatureConstants.HellHound, "Hell Hound")]
        [TestCase(CreatureConstants.Hellwasp_Swarm, "Hellwasp Swarm")]
        [TestCase(CreatureConstants.Hezrou, "Hezrou")]
        [TestCase(CreatureConstants.Hieracosphinx, "Hieracosphinx")]
        [TestCase(CreatureConstants.Hippogriff, "Hippogriff")]
        [TestCase(CreatureConstants.Hobgoblin, "Hobgoblin")]
        [TestCase(CreatureConstants.Homunculus, "Homunculus")]
        [TestCase(CreatureConstants.Horse_Heavy, "Heavy Horse")]
        [TestCase(CreatureConstants.Horse_Heavy_War, "Heavy Warhorse")]
        [TestCase(CreatureConstants.Horse_Light, "Light Horse")]
        [TestCase(CreatureConstants.Horse_Light_War, "Light Warhorse")]
        [TestCase(CreatureConstants.HoundArchon, "Hound Archon")]
        [TestCase(CreatureConstants.Howler, "Howler")]
        [TestCase(CreatureConstants.Human, "Human")]
        [TestCase(CreatureConstants.Hyena, "Hyena")]
        [TestCase(CreatureConstants.Imp, "Imp")]
        [TestCase(CreatureConstants.InvisibleStalker, "Invisible Stalker")]
        [TestCase(CreatureConstants.Janni, "Janni")]
        [TestCase(CreatureConstants.Kobold, "Kobold")]
        [TestCase(CreatureConstants.Kolyarut, "Kolyarut")]
        [TestCase(CreatureConstants.Kraken, "Kraken")]
        [TestCase(CreatureConstants.Krenshar, "Krenshar")]
        [TestCase(CreatureConstants.KuoToa, "Kuo-toa")]
        [TestCase(CreatureConstants.Lamia, "Lamia")]
        [TestCase(CreatureConstants.Lammasu, "Lammasu")]
        [TestCase(CreatureConstants.LanternArchon, "Lantern Archon")]
        [TestCase(CreatureConstants.Lemure, "Lemure")]
        [TestCase(CreatureConstants.Leonal, "Leonal")]
        [TestCase(CreatureConstants.Leopard, "Leopard")]
        [TestCase(CreatureConstants.Lillend, "Lillend")]
        [TestCase(CreatureConstants.Lion, "Lion")]
        [TestCase(CreatureConstants.Lion_Dire, "Dire Lion")]
        [TestCase(CreatureConstants.Lizard, "Lizard")]
        [TestCase(CreatureConstants.Lizard_Monitor, "Monitor Lizard")]
        [TestCase(CreatureConstants.Lizardfolk, "Lizardfolk")]
        [TestCase(CreatureConstants.Locathah, "Locathah")]
        [TestCase(CreatureConstants.Locust_Swarm, "Locust Swarm")]
        [TestCase(CreatureConstants.Magmin, "Magmin")]
        [TestCase(CreatureConstants.MantaRay, "Manta Ray")]
        [TestCase(CreatureConstants.Manticore, "Manticore")]
        [TestCase(CreatureConstants.Marilith, "Marilith")]
        [TestCase(CreatureConstants.Marut, "Marut")]
        [TestCase(CreatureConstants.Medusa, "Medusa")]
        [TestCase(CreatureConstants.Megaraptor, "Megaraptor")]
        [TestCase(CreatureConstants.Merfolk, "Merfolk")]
        [TestCase(CreatureConstants.Mimic, "Mimic")]
        [TestCase(CreatureConstants.MindFlayer, "Mind Flayer (Illithid)")]
        [TestCase(CreatureConstants.Minotaur, "Minotaur")]
        [TestCase(CreatureConstants.Mohrg, "Mohrg")]
        [TestCase(CreatureConstants.Monkey, "Monkey")]
        [TestCase(CreatureConstants.Mule, "Mule")]
        [TestCase(CreatureConstants.Mummy, "Mummy")]
        [TestCase(CreatureConstants.Nalfeshnee, "Nalfeshnee")]
        [TestCase(CreatureConstants.HellHound_NessianWarhound, "Nessian Warhound")]
        [TestCase(CreatureConstants.Nightcrawler, "Nightcrawler")]
        [TestCase(CreatureConstants.NightHag, "Night Hag")]
        [TestCase(CreatureConstants.Nightmare, "Nightmare")]
        [TestCase(CreatureConstants.Nightwalker, "Nightwalker")]
        [TestCase(CreatureConstants.Nightwing, "Nightwing")]
        [TestCase(CreatureConstants.Nixie, "Nixie")]
        [TestCase(CreatureConstants.Nymph, "Nymph")]
        [TestCase(CreatureConstants.Octopus, "Octopus")]
        [TestCase(CreatureConstants.Octopus_Giant, "Giant Octopus")]
        [TestCase(CreatureConstants.Ogre, "Ogre")]
        [TestCase(CreatureConstants.OgreMage, "Ogre Mage")]
        [TestCase(CreatureConstants.GrayOoze, "Gray Ooze")]
        [TestCase(CreatureConstants.OchreJelly, "Ochre Jelly")]
        [TestCase(CreatureConstants.Orc, "Orc")]
        [TestCase(CreatureConstants.Orc_Half, "Half-Orc")]
        [TestCase(CreatureConstants.Otyugh, "Otyugh")]
        [TestCase(CreatureConstants.Owl, "Owl")]
        [TestCase(CreatureConstants.Owl_Giant, "Giant Owl")]
        [TestCase(CreatureConstants.Owlbear, "Owlbear")]
        [TestCase(CreatureConstants.Pegasus, "Pegasus")]
        [TestCase(CreatureConstants.PhantomFungus, "Phantom Fungus")]
        [TestCase(CreatureConstants.PhaseSpider, "Phase Spider")]
        [TestCase(CreatureConstants.Phasm, "Phasm")]
        [TestCase(CreatureConstants.PitFiend, "Pit Fiend")]
        [TestCase(CreatureConstants.Pixie, "Pixie")]
        [TestCase(CreatureConstants.Angel_Planetar, "Angel, Planetar")]
        [TestCase(CreatureConstants.Pony, "Pony")]
        [TestCase(CreatureConstants.Pony_War, "Warpony")]
        [TestCase(CreatureConstants.Porpoise, "Porpoise")]
        [TestCase(CreatureConstants.PrayingMantis_Giant, "Giant Praying Mantis")]
        [TestCase(CreatureConstants.Pseudodragon, "Pseudodragon")]
        [TestCase(CreatureConstants.PurpleWorm, "Purple Worm")]
        [TestCase(CreatureConstants.Quasit, "Quasit")]
        [TestCase(CreatureConstants.Rakshasa, "Rakshasa")]
        [TestCase(CreatureConstants.Rast, "Rast")]
        [TestCase(CreatureConstants.Rat, "Rat")]
        [TestCase(CreatureConstants.Rat_Dire, "Dire Rat")]
        [TestCase(CreatureConstants.Rat_Swarm, "Rat Swarm")]
        [TestCase(CreatureConstants.Raven, "Raven")]
        [TestCase(CreatureConstants.Ravid, "Ravid")]
        [TestCase(CreatureConstants.RazorBoar, "Razor Boar")]
        [TestCase(CreatureConstants.Remorhaz, "Remorhaz")]
        [TestCase(CreatureConstants.Retriever, "Retriever")]
        [TestCase(CreatureConstants.Rhinoceras, "Rhinoceras")]
        [TestCase(CreatureConstants.Roc, "Roc")]
        [TestCase(CreatureConstants.Roper, "Roper")]
        [TestCase(CreatureConstants.RustMonster, "Rust Monster")]
        [TestCase(CreatureConstants.Sahuagin, "Sahuagin")]
        [TestCase(CreatureConstants.Sahuagin_Malenti, "Malenti")]
        [TestCase(CreatureConstants.Sahuagin_Mutant, "Sahuagin Mutant")]
        [TestCase(CreatureConstants.Satyr, "Satyr")]
        [TestCase(CreatureConstants.Scorpionfolk, "Scorpionfolk")]
        [TestCase(CreatureConstants.SeaCat, "Sea Cat")]
        [TestCase(CreatureConstants.SeaHag, "Sea Hag")]
        [TestCase(CreatureConstants.Shadow, "Shadow")]
        [TestCase(CreatureConstants.ShadowMastiff, "Shadow Mastiff")]
        [TestCase(CreatureConstants.Shark_Dire, "Dire Shark")]
        [TestCase(CreatureConstants.ShamblingMound, "Shambling Mound")]
        [TestCase(CreatureConstants.ShieldGuardian, "Shield Guardian")]
        [TestCase(CreatureConstants.ShockerLizard, "Shocker Lizard")]
        [TestCase(CreatureConstants.Shrieker, "Shrieker")]
        [TestCase(CreatureConstants.Skum, "Skum")]
        [TestCase(CreatureConstants.Snake_Constrictor, "Constrictor Snake")]
        [TestCase(CreatureConstants.Snake_Constrictor_Giant, "Giant Constrictor Snake")]
        [TestCase(CreatureConstants.Angel_Solar, "Angel, Solar")]
        [TestCase(CreatureConstants.Spectre, "Spectre")]
        [TestCase(CreatureConstants.Spider_Swarm, "Spider Swarm")]
        [TestCase(CreatureConstants.SpiderEater, "Spider Eater")]
        [TestCase(CreatureConstants.Squid, "Squid")]
        [TestCase(CreatureConstants.Squid_Giant, "Giant Squid")]
        [TestCase(CreatureConstants.StagBeetle_Giant, "Giant Stag Beetle")]
        [TestCase(CreatureConstants.Stirge, "Stirge")]
        [TestCase(CreatureConstants.Succubus, "Succubus")]
        [TestCase(CreatureConstants.Gnome_Svirfneblin, "Svirfneblin")]
        [TestCase(CreatureConstants.Tarrasque, "Tarrasque")]
        [TestCase(CreatureConstants.Tendriculos, "Tendriculos")]
        [TestCase(CreatureConstants.Thoqqua, "Thoqqua")]
        [TestCase(CreatureConstants.Tiefling, "Tiefling")]
        [TestCase(CreatureConstants.Tiger, "Tiger")]
        [TestCase(CreatureConstants.Tiger_Dire, "Dire Tiger")]
        [TestCase(CreatureConstants.Titan, "Titan")]
        [TestCase(CreatureConstants.Toad, "Toad")]
        [TestCase(CreatureConstants.Treant, "Treant")]
        [TestCase(CreatureConstants.Triceratops, "Triceratops")]
        [TestCase(CreatureConstants.Triton, "Triton")]
        [TestCase(CreatureConstants.Troglodyte, "Troglodyte")]
        [TestCase(CreatureConstants.Troll, "Troll")]
        [TestCase(CreatureConstants.TrumpetArchon, "Trumpet Archon")]
        [TestCase(CreatureConstants.Tyrannosaurus, "Tyrannosaurus")]
        [TestCase(CreatureConstants.UmberHulk, "Umber Hulk")]
        [TestCase(CreatureConstants.Unicorn, "Unicorn")]
        [TestCase(CreatureConstants.VampireSpawn, "Vampire Spawn")]
        [TestCase(CreatureConstants.Vargouille, "Vargouille")]
        [TestCase(CreatureConstants.VioletFungus, "Violet Fungus")]
        [TestCase(CreatureConstants.Vrock, "Vrock")]
        [TestCase(CreatureConstants.Wasp_Giant, "Giant Wasp")]
        [TestCase(CreatureConstants.Weasel, "Weasel")]
        [TestCase(CreatureConstants.Weasel_Dire, "Dire Weasel")]
        [TestCase(CreatureConstants.Wight, "Wight")]
        [TestCase(CreatureConstants.WillOWisp, "Will-O'-Wisp")]
        [TestCase(CreatureConstants.WinterWolf, "Winter Wolf")]
        [TestCase(CreatureConstants.Wolf, "Wolf")]
        [TestCase(CreatureConstants.Wolf_Dire, "Dire Wolf")]
        [TestCase(CreatureConstants.Wolverine, "Wolverine")]
        [TestCase(CreatureConstants.Wolverine_Dire, "Dire Wolverine")]
        [TestCase(CreatureConstants.Worg, "Worg")]
        [TestCase(CreatureConstants.Wraith, "Wraith")]
        [TestCase(CreatureConstants.Wyvern, "Wyvern")]
        [TestCase(CreatureConstants.Xill, "Xill")]
        [TestCase(CreatureConstants.YethHound, "Yeth Hound")]
        [TestCase(CreatureConstants.Yrthak, "Yrthak")]
        [TestCase(CreatureConstants.Zelekhut, "Zelekhut")]
        [TestCase(CreatureConstants.AnimatedObject_Colossal, "Animated Object, " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_Flexible, "Animated Object, " + SizeConstants.Colossal + " (Flexible)")]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Long, "Animated Object, " + SizeConstants.Colossal + " (More Than Two Legs, Long)")]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Long_Wooden, "Animated Object, " + SizeConstants.Colossal + " (More Than Two Legs, Long, Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Tall, "Animated Object, " + SizeConstants.Colossal + " (More Than Two Legs, Tall)")]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_MultipleLegs_Tall_Wooden, "Animated Object, " + SizeConstants.Colossal + " (More Than Two Legs, Tall, Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_Sheetlike, "Animated Object, " + SizeConstants.Colossal + " (Sheetlike)")]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_TwoLegs, "Animated Object, " + SizeConstants.Colossal + " (Two Legs)")]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_TwoLegs_Wooden, "Animated Object, " + SizeConstants.Colossal + " (Two Legs, Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_Wheels_Wooden, "Animated Object, " + SizeConstants.Colossal + " (Wheels, Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Colossal_Wooden, "Animated Object, " + SizeConstants.Colossal + " (Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan, "Animated Object, " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan_Flexible, "Animated Object, " + SizeConstants.Gargantuan + " (Flexible)")]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Long, "Animated Object, " + SizeConstants.Gargantuan + " (More Than Two Legs, Long)")]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Long_Wooden, "Animated Object, " + SizeConstants.Gargantuan + " (More Than Two Legs, Long, Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Tall, "Animated Object, " + SizeConstants.Gargantuan + " (More Than Two Legs, Tall)")]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan_MultipleLegs_Tall_Wooden, "Animated Object, " + SizeConstants.Gargantuan + " (More Than Two Legs, Tall, Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan_Sheetlike, "Animated Object, " + SizeConstants.Gargantuan + " (Sheetlike)")]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan_TwoLegs, "Animated Object, " + SizeConstants.Gargantuan + " (Two Legs)")]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan_TwoLegs_Wooden, "Animated Object, " + SizeConstants.Gargantuan + " (Two Legs, Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan_Wheels_Wooden, "Animated Object, " + SizeConstants.Gargantuan + " (Wheels, Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Gargantuan_Wooden, "Animated Object, " + SizeConstants.Gargantuan + " (Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Huge, "Animated Object, " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.AnimatedObject_Huge_Flexible, "Animated Object, " + SizeConstants.Huge + " (Flexible)")]
        [TestCase(CreatureConstants.AnimatedObject_Huge_MultipleLegs_Long, "Animated Object, " + SizeConstants.Huge + " (More Than Two Legs, Long)")]
        [TestCase(CreatureConstants.AnimatedObject_Huge_MultipleLegs_Long_Wooden, "Animated Object, " + SizeConstants.Huge + " (More Than Two Legs, Long, Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Huge_MultipleLegs_Tall, "Animated Object, " + SizeConstants.Huge + " (More Than Two Legs, Tall)")]
        [TestCase(CreatureConstants.AnimatedObject_Huge_MultipleLegs_Tall_Wooden, "Animated Object, " + SizeConstants.Huge + " (More Than Two Legs, Tall, Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Huge_Sheetlike, "Animated Object, " + SizeConstants.Huge + " (Sheetlike)")]
        [TestCase(CreatureConstants.AnimatedObject_Huge_TwoLegs, "Animated Object, " + SizeConstants.Huge + " (Two Legs)")]
        [TestCase(CreatureConstants.AnimatedObject_Huge_TwoLegs_Wooden, "Animated Object, " + SizeConstants.Huge + " (Two Legs, Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Huge_Wheels_Wooden, "Animated Object, " + SizeConstants.Huge + " (Wheels, Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Huge_Wooden, "Animated Object, " + SizeConstants.Huge + " (Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Large, "Animated Object, " + SizeConstants.Large)]
        [TestCase(CreatureConstants.AnimatedObject_Large_Flexible, "Animated Object, " + SizeConstants.Large + " (Flexible)")]
        [TestCase(CreatureConstants.AnimatedObject_Large_MultipleLegs_Long, "Animated Object, " + SizeConstants.Large + " (More Than Two Legs, Long)")]
        [TestCase(CreatureConstants.AnimatedObject_Large_MultipleLegs_Long_Wooden, "Animated Object, " + SizeConstants.Large + " (More Than Two Legs, Long, Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Large_MultipleLegs_Tall, "Animated Object, " + SizeConstants.Large + " (More Than Two Legs, Tall)")]
        [TestCase(CreatureConstants.AnimatedObject_Large_MultipleLegs_Tall_Wooden, "Animated Object, " + SizeConstants.Large + " (More Than Two Legs, Tall, Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Large_Sheetlike, "Animated Object, " + SizeConstants.Large + " (Sheetlike)")]
        [TestCase(CreatureConstants.AnimatedObject_Large_TwoLegs, "Animated Object, " + SizeConstants.Large + " (Two Legs)")]
        [TestCase(CreatureConstants.AnimatedObject_Large_TwoLegs_Wooden, "Animated Object, " + SizeConstants.Large + " (Two Legs, Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Large_Wheels_Wooden, "Animated Object, " + SizeConstants.Large + " (Wheels, Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Large_Wooden, "Animated Object, " + SizeConstants.Large + " (Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Medium, "Animated Object, " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.AnimatedObject_Medium_Flexible, "Animated Object, " + SizeConstants.Medium + " (Flexible)")]
        [TestCase(CreatureConstants.AnimatedObject_Medium_MultipleLegs, "Animated Object, " + SizeConstants.Medium + " (More Than Two Legs)")]
        [TestCase(CreatureConstants.AnimatedObject_Medium_MultipleLegs_Wooden, "Animated Object, " + SizeConstants.Medium + " (More Than Two Legs, Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Medium_Sheetlike, "Animated Object, " + SizeConstants.Medium + " (Sheetlike)")]
        [TestCase(CreatureConstants.AnimatedObject_Medium_TwoLegs, "Animated Object, " + SizeConstants.Medium + " (Two Legs)")]
        [TestCase(CreatureConstants.AnimatedObject_Medium_TwoLegs_Wooden, "Animated Object, " + SizeConstants.Medium + " (Two Legs, Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Medium_Wheels_Wooden, "Animated Object, " + SizeConstants.Medium + " (Wheels, Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Medium_Wooden, "Animated Object, " + SizeConstants.Medium + " (Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Small, "Animated Object, " + SizeConstants.Small)]
        [TestCase(CreatureConstants.AnimatedObject_Small_Flexible, "Animated Object, " + SizeConstants.Small + " (Flexible)")]
        [TestCase(CreatureConstants.AnimatedObject_Small_MultipleLegs, "Animated Object, " + SizeConstants.Small + " (More Than Two Legs)")]
        [TestCase(CreatureConstants.AnimatedObject_Small_MultipleLegs_Wooden, "Animated Object, " + SizeConstants.Small + " (More Than Two Legs, Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Small_Sheetlike, "Animated Object, " + SizeConstants.Small + " (Sheetlike)")]
        [TestCase(CreatureConstants.AnimatedObject_Small_TwoLegs, "Animated Object, " + SizeConstants.Small + " (Two Legs)")]
        [TestCase(CreatureConstants.AnimatedObject_Small_TwoLegs_Wooden, "Animated Object, " + SizeConstants.Small + " (Two Legs, Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Small_Wheels_Wooden, "Animated Object, " + SizeConstants.Small + " (Wheels, Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Small_Wooden, "Animated Object, " + SizeConstants.Small + " (Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Tiny, "Animated Object, " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.AnimatedObject_Tiny_Flexible, "Animated Object, " + SizeConstants.Tiny + " (Flexible)")]
        [TestCase(CreatureConstants.AnimatedObject_Tiny_MultipleLegs, "Animated Object, " + SizeConstants.Tiny + " (More Than Two Legs)")]
        [TestCase(CreatureConstants.AnimatedObject_Tiny_MultipleLegs_Wooden, "Animated Object, " + SizeConstants.Tiny + " (More Than Two Legs, Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Tiny_Sheetlike, "Animated Object, " + SizeConstants.Tiny + " (Sheetlike)")]
        [TestCase(CreatureConstants.AnimatedObject_Tiny_TwoLegs, "Animated Object, " + SizeConstants.Tiny + " (Two Legs)")]
        [TestCase(CreatureConstants.AnimatedObject_Tiny_TwoLegs_Wooden, "Animated Object, " + SizeConstants.Tiny + " (Two Legs, Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Tiny_Wheels_Wooden, "Animated Object, " + SizeConstants.Tiny + " (Wheels, Wooden)")]
        [TestCase(CreatureConstants.AnimatedObject_Tiny_Wooden, "Animated Object, " + SizeConstants.Tiny + " (Wooden)")]
        [TestCase(CreatureConstants.Ant_Giant_Soldier, "Giant Ant, Soldier")]
        [TestCase(CreatureConstants.Ant_Giant_Worker, "Giant Ant, Worker")]
        [TestCase(CreatureConstants.Ant_Giant_Queen, "Giant Ant, Queen")]
        [TestCase(CreatureConstants.Arrowhawk_Juvenile, "Juvenile Arrowhawk")]
        [TestCase(CreatureConstants.Arrowhawk_Adult, "Adult Arrowhawk")]
        [TestCase(CreatureConstants.Arrowhawk_Elder, "Elder Arrowhawk")]
        [TestCase(CreatureConstants.BarbedDevil_Hamatula, "Barbed Devil (Hamatula)")]
        [TestCase(CreatureConstants.BeardedDevil_Barbazu, "Bearded Devil (Barbazu)")]
        [TestCase(CreatureConstants.Barghest_Greater, "Greater " + CreatureConstants.Barghest)]
        [TestCase(CreatureConstants.Basilisk_Greater, "Greater " + CreatureConstants.Basilisk)]
        [TestCase(CreatureConstants.BlackPudding_Elder, "Elder " + CreatureConstants.BlackPudding)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Colossal, "Monstrous Centipede, " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Gargantuan, "Monstrous Centipede, " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Huge, "Monstrous Centipede, " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Large, "Monstrous Centipede, " + SizeConstants.Large)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Medium, "Monstrous Centipede, " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Small, "Monstrous Centipede, " + SizeConstants.Small)]
        [TestCase(CreatureConstants.Centipede_Monstrous_Tiny, "Monstrous Centipede, " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.ChainDevil_Kyton, "Chain Devil (Kyton)")]
        [TestCase(CreatureConstants.HornedDevil_Cornugon, "Horned Devil (Cornugon)")]
        [TestCase(CreatureConstants.Cryohydra_5Heads, "Five-Headed Cryohydra")]
        [TestCase(CreatureConstants.Cryohydra_6Heads, "Six-Headed Cryohydra")]
        [TestCase(CreatureConstants.Cryohydra_7Heads, "Seven-Headed Cryohydra")]
        [TestCase(CreatureConstants.Cryohydra_8Heads, "Eight-Headed Cryohydra")]
        [TestCase(CreatureConstants.Cryohydra_9Heads, "Nine-Headed Cryohydra")]
        [TestCase(CreatureConstants.Cryohydra_10Heads, "Ten-Headed Cryohydra")]
        [TestCase(CreatureConstants.Cryohydra_11Heads, "Eleven-Headed Cryohydra")]
        [TestCase(CreatureConstants.Cryohydra_12Heads, "Twelve-Headed Cryohydra")]
        [TestCase(CreatureConstants.DisplacerBeast_PackLord, CreatureConstants.DisplacerBeast + " Pack Lord")]
        [TestCase(CreatureConstants.Djinni_Noble, "Noble " + CreatureConstants.Djinni)]
        [TestCase(CreatureConstants.Dog_Riding, CreatureConstants.Dog + ", Riding")]
        [TestCase(CreatureConstants.Dragon_Black_Wyrmling, "Black Dragon, Wyrmling")]
        [TestCase(CreatureConstants.Dragon_Black_VeryYoung, "Black Dragon, Very Young")]
        [TestCase(CreatureConstants.Dragon_Black_Young, "Black Dragon, Young")]
        [TestCase(CreatureConstants.Dragon_Black_Juvenile, "Black Dragon, Juvenile")]
        [TestCase(CreatureConstants.Dragon_Black_YoungAdult, "Black Dragon, Young Adult")]
        [TestCase(CreatureConstants.Dragon_Black_Adult, "Black Dragon, Adult")]
        [TestCase(CreatureConstants.Dragon_Black_MatureAdult, "Black Dragon, Mature Adult")]
        [TestCase(CreatureConstants.Dragon_Black_Old, "Black Dragon, Old")]
        [TestCase(CreatureConstants.Dragon_Black_VeryOld, "Black Dragon, Very Old")]
        [TestCase(CreatureConstants.Dragon_Black_Ancient, "Black Dragon, Ancient")]
        [TestCase(CreatureConstants.Dragon_Black_Wyrm, "Black Dragon, Wyrm")]
        [TestCase(CreatureConstants.Dragon_Black_GreatWyrm, "Black Dragon, Great Wyrm")]
        [TestCase(CreatureConstants.Dragon_Blue_Wyrmling, "Blue Dragon, Wyrmling")]
        [TestCase(CreatureConstants.Dragon_Blue_VeryYoung, "Blue Dragon, Very Young")]
        [TestCase(CreatureConstants.Dragon_Blue_Young, "Blue Dragon, Young")]
        [TestCase(CreatureConstants.Dragon_Blue_Juvenile, "Blue Dragon, Juvenile")]
        [TestCase(CreatureConstants.Dragon_Blue_YoungAdult, "Blue Dragon, Young Adult")]
        [TestCase(CreatureConstants.Dragon_Blue_Adult, "Blue Dragon, Adult")]
        [TestCase(CreatureConstants.Dragon_Blue_MatureAdult, "Blue Dragon, Mature Adult")]
        [TestCase(CreatureConstants.Dragon_Blue_Old, "Blue Dragon, Old")]
        [TestCase(CreatureConstants.Dragon_Blue_VeryOld, "Blue Dragon, Very Old")]
        [TestCase(CreatureConstants.Dragon_Blue_Ancient, "Blue Dragon, Ancient")]
        [TestCase(CreatureConstants.Dragon_Blue_Wyrm, "Blue Dragon, Wyrm")]
        [TestCase(CreatureConstants.Dragon_Blue_GreatWyrm, "Blue Dragon, Great Wyrm")]
        [TestCase(CreatureConstants.Dragon_Brass_Wyrmling, "Brass Dragon, Wyrmling")]
        [TestCase(CreatureConstants.Dragon_Brass_VeryYoung, "Brass Dragon, Very Young")]
        [TestCase(CreatureConstants.Dragon_Brass_Young, "Brass Dragon, Young")]
        [TestCase(CreatureConstants.Dragon_Brass_Juvenile, "Brass Dragon, Juvenile")]
        [TestCase(CreatureConstants.Dragon_Brass_YoungAdult, "Brass Dragon, Young Adult")]
        [TestCase(CreatureConstants.Dragon_Brass_Adult, "Brass Dragon, Adult")]
        [TestCase(CreatureConstants.Dragon_Brass_MatureAdult, "Brass Dragon, Mature Adult")]
        [TestCase(CreatureConstants.Dragon_Brass_Old, "Brass Dragon, Old")]
        [TestCase(CreatureConstants.Dragon_Brass_VeryOld, "Brass Dragon, Very Old")]
        [TestCase(CreatureConstants.Dragon_Brass_Ancient, "Brass Dragon, Ancient")]
        [TestCase(CreatureConstants.Dragon_Brass_Wyrm, "Brass Dragon, Wyrm")]
        [TestCase(CreatureConstants.Dragon_Brass_GreatWyrm, "Brass Dragon, Great Wyrm")]
        [TestCase(CreatureConstants.Dragon_Bronze_Wyrmling, "Bronze Dragon, Wyrmling")]
        [TestCase(CreatureConstants.Dragon_Bronze_VeryYoung, "Bronze Dragon, Very Young")]
        [TestCase(CreatureConstants.Dragon_Bronze_Young, "Bronze Dragon, Young")]
        [TestCase(CreatureConstants.Dragon_Bronze_Juvenile, "Bronze Dragon, Juvenile")]
        [TestCase(CreatureConstants.Dragon_Bronze_YoungAdult, "Bronze Dragon, Young Adult")]
        [TestCase(CreatureConstants.Dragon_Bronze_Adult, "Bronze Dragon, Adult")]
        [TestCase(CreatureConstants.Dragon_Bronze_MatureAdult, "Bronze Dragon, Mature Adult")]
        [TestCase(CreatureConstants.Dragon_Bronze_Old, "Bronze Dragon, Old")]
        [TestCase(CreatureConstants.Dragon_Bronze_VeryOld, "Bronze Dragon, Very Old")]
        [TestCase(CreatureConstants.Dragon_Bronze_Ancient, "Bronze Dragon, Ancient")]
        [TestCase(CreatureConstants.Dragon_Bronze_Wyrm, "Bronze Dragon, Wyrm")]
        [TestCase(CreatureConstants.Dragon_Bronze_GreatWyrm, "Bronze Dragon, Great Wyrm")]
        [TestCase(CreatureConstants.Dragon_Copper_Wyrmling, "Copper Dragon, Wyrmling")]
        [TestCase(CreatureConstants.Dragon_Copper_VeryYoung, "Copper Dragon, Very Young")]
        [TestCase(CreatureConstants.Dragon_Copper_Young, "Copper Dragon, Young")]
        [TestCase(CreatureConstants.Dragon_Copper_Juvenile, "Copper Dragon, Juvenile")]
        [TestCase(CreatureConstants.Dragon_Copper_YoungAdult, "Copper Dragon, Young Adult")]
        [TestCase(CreatureConstants.Dragon_Copper_Adult, "Copper Dragon, Adult")]
        [TestCase(CreatureConstants.Dragon_Copper_MatureAdult, "Copper Dragon, Mature Adult")]
        [TestCase(CreatureConstants.Dragon_Copper_Old, "Copper Dragon, Old")]
        [TestCase(CreatureConstants.Dragon_Copper_VeryOld, "Copper Dragon, Very Old")]
        [TestCase(CreatureConstants.Dragon_Copper_Ancient, "Copper Dragon, Ancient")]
        [TestCase(CreatureConstants.Dragon_Copper_Wyrm, "Copper Dragon, Wyrm")]
        [TestCase(CreatureConstants.Dragon_Copper_GreatWyrm, "Copper Dragon, Great Wyrm")]
        [TestCase(CreatureConstants.Dragon_Gold_Wyrmling, "Gold Dragon, Wyrmling")]
        [TestCase(CreatureConstants.Dragon_Gold_VeryYoung, "Gold Dragon, Very Young")]
        [TestCase(CreatureConstants.Dragon_Gold_Young, "Gold Dragon, Young")]
        [TestCase(CreatureConstants.Dragon_Gold_Juvenile, "Gold Dragon, Juvenile")]
        [TestCase(CreatureConstants.Dragon_Gold_YoungAdult, "Gold Dragon, Young Adult")]
        [TestCase(CreatureConstants.Dragon_Gold_Adult, "Gold Dragon, Adult")]
        [TestCase(CreatureConstants.Dragon_Gold_MatureAdult, "Gold Dragon, Mature Adult")]
        [TestCase(CreatureConstants.Dragon_Gold_Old, "Gold Dragon, Old")]
        [TestCase(CreatureConstants.Dragon_Gold_VeryOld, "Gold Dragon, Very Old")]
        [TestCase(CreatureConstants.Dragon_Gold_Ancient, "Gold Dragon, Ancient")]
        [TestCase(CreatureConstants.Dragon_Gold_Wyrm, "Gold Dragon, Wyrm")]
        [TestCase(CreatureConstants.Dragon_Gold_GreatWyrm, "Gold Dragon, Great Wyrm")]
        [TestCase(CreatureConstants.Dragon_Green_Wyrmling, "Green Dragon, Wyrmling")]
        [TestCase(CreatureConstants.Dragon_Green_VeryYoung, "Green Dragon, Very Young")]
        [TestCase(CreatureConstants.Dragon_Green_Young, "Green Dragon, Young")]
        [TestCase(CreatureConstants.Dragon_Green_Juvenile, "Green Dragon, Juvenile")]
        [TestCase(CreatureConstants.Dragon_Green_YoungAdult, "Green Dragon, Young Adult")]
        [TestCase(CreatureConstants.Dragon_Green_Adult, "Green Dragon, Adult")]
        [TestCase(CreatureConstants.Dragon_Green_MatureAdult, "Green Dragon, Mature Adult")]
        [TestCase(CreatureConstants.Dragon_Green_Old, "Green Dragon, Old")]
        [TestCase(CreatureConstants.Dragon_Green_VeryOld, "Green Dragon, Very Old")]
        [TestCase(CreatureConstants.Dragon_Green_Ancient, "Green Dragon, Ancient")]
        [TestCase(CreatureConstants.Dragon_Green_Wyrm, "Green Dragon, Wyrm")]
        [TestCase(CreatureConstants.Dragon_Green_GreatWyrm, "Green Dragon, Great Wyrm")]
        [TestCase(CreatureConstants.Dragon_Red_Wyrmling, "Red Dragon, Wyrmling")]
        [TestCase(CreatureConstants.Dragon_Red_VeryYoung, "Red Dragon, Very Young")]
        [TestCase(CreatureConstants.Dragon_Red_Young, "Red Dragon, Young")]
        [TestCase(CreatureConstants.Dragon_Red_Juvenile, "Red Dragon, Juvenile")]
        [TestCase(CreatureConstants.Dragon_Red_YoungAdult, "Red Dragon, Young Adult")]
        [TestCase(CreatureConstants.Dragon_Red_Adult, "Red Dragon, Adult")]
        [TestCase(CreatureConstants.Dragon_Red_MatureAdult, "Red Dragon, Mature Adult")]
        [TestCase(CreatureConstants.Dragon_Red_Old, "Red Dragon, Old")]
        [TestCase(CreatureConstants.Dragon_Red_VeryOld, "Red Dragon, Very Old")]
        [TestCase(CreatureConstants.Dragon_Red_Ancient, "Red Dragon, Ancient")]
        [TestCase(CreatureConstants.Dragon_Red_Wyrm, "Red Dragon, Wyrm")]
        [TestCase(CreatureConstants.Dragon_Red_GreatWyrm, "Red Dragon, Great Wyrm")]
        [TestCase(CreatureConstants.Dragon_Silver_Wyrmling, "Silver Dragon, Wyrmling")]
        [TestCase(CreatureConstants.Dragon_Silver_VeryYoung, "Silver Dragon, Very Young")]
        [TestCase(CreatureConstants.Dragon_Silver_Young, "Silver Dragon, Young")]
        [TestCase(CreatureConstants.Dragon_Silver_Juvenile, "Silver Dragon, Juvenile")]
        [TestCase(CreatureConstants.Dragon_Silver_YoungAdult, "Silver Dragon, Young Adult")]
        [TestCase(CreatureConstants.Dragon_Silver_Adult, "Silver Dragon, Adult")]
        [TestCase(CreatureConstants.Dragon_Silver_MatureAdult, "Silver Dragon, Mature Adult")]
        [TestCase(CreatureConstants.Dragon_Silver_Old, "Silver Dragon, Old")]
        [TestCase(CreatureConstants.Dragon_Silver_VeryOld, "Silver Dragon, Very Old")]
        [TestCase(CreatureConstants.Dragon_Silver_Ancient, "Silver Dragon, Ancient")]
        [TestCase(CreatureConstants.Dragon_Silver_Wyrm, "Silver Dragon, Wyrm")]
        [TestCase(CreatureConstants.Dragon_Silver_GreatWyrm, "Silver Dragon, Great Wyrm")]
        [TestCase(CreatureConstants.Dragon_White_Wyrmling, "White Dragon, Wyrmling")]
        [TestCase(CreatureConstants.Dragon_White_VeryYoung, "White Dragon, Very Young")]
        [TestCase(CreatureConstants.Dragon_White_Young, "White Dragon, Young")]
        [TestCase(CreatureConstants.Dragon_White_Juvenile, "White Dragon, Juvenile")]
        [TestCase(CreatureConstants.Dragon_White_YoungAdult, "White Dragon, Young Adult")]
        [TestCase(CreatureConstants.Dragon_White_Adult, "White Dragon, Adult")]
        [TestCase(CreatureConstants.Dragon_White_MatureAdult, "White Dragon, Mature Adult")]
        [TestCase(CreatureConstants.Dragon_White_Old, "White Dragon, Old")]
        [TestCase(CreatureConstants.Dragon_White_VeryOld, "White Dragon, Very Old")]
        [TestCase(CreatureConstants.Dragon_White_Ancient, "White Dragon, Ancient")]
        [TestCase(CreatureConstants.Dragon_White_Wyrm, "White Dragon, Wyrm")]
        [TestCase(CreatureConstants.Dragon_White_GreatWyrm, "White Dragon, Great Wyrm")]
        [TestCase(CreatureConstants.Elemental_Air_Elder, "Air Elemental, Elder")]
        [TestCase(CreatureConstants.Elemental_Air_Greater, "Air Elemental, Greater")]
        [TestCase(CreatureConstants.Elemental_Air_Huge, "Air Elemental, Huge")]
        [TestCase(CreatureConstants.Elemental_Air_Large, "Air Elemental, Large")]
        [TestCase(CreatureConstants.Elemental_Air_Medium, "Air Elemental, Medium")]
        [TestCase(CreatureConstants.Elemental_Air_Small, "Air Elemental, Small")]
        [TestCase(CreatureConstants.Elemental_Earth_Elder, "Earth Elemental, Elder")]
        [TestCase(CreatureConstants.Elemental_Earth_Greater, "Earth Elemental, Greater")]
        [TestCase(CreatureConstants.Elemental_Earth_Huge, "Earth Elemental, Huge")]
        [TestCase(CreatureConstants.Elemental_Earth_Large, "Earth Elemental, Large")]
        [TestCase(CreatureConstants.Elemental_Earth_Medium, "Earth Elemental, Medium")]
        [TestCase(CreatureConstants.Elemental_Earth_Small, "Earth Elemental, Small")]
        [TestCase(CreatureConstants.Elemental_Fire_Elder, "Fire Elemental, Elder")]
        [TestCase(CreatureConstants.Elemental_Fire_Greater, "Fire Elemental, Greater")]
        [TestCase(CreatureConstants.Elemental_Fire_Huge, "Fire Elemental, Huge")]
        [TestCase(CreatureConstants.Elemental_Fire_Large, "Fire Elemental, Large")]
        [TestCase(CreatureConstants.Elemental_Fire_Medium, "Fire Elemental, Medium")]
        [TestCase(CreatureConstants.Elemental_Fire_Small, "Fire Elemental, Small")]
        [TestCase(CreatureConstants.Elemental_Water_Elder, "Water Elemental, Elder")]
        [TestCase(CreatureConstants.Elemental_Water_Greater, "Water Elemental, Greater")]
        [TestCase(CreatureConstants.Elemental_Water_Huge, "Water Elemental, Huge")]
        [TestCase(CreatureConstants.Elemental_Water_Large, "Water Elemental, Large")]
        [TestCase(CreatureConstants.Elemental_Water_Medium, "Water Elemental, Medium")]
        [TestCase(CreatureConstants.Elemental_Water_Small, "Water Elemental, Small")]
        [TestCase(CreatureConstants.FormianMyrmarch, "Formian Myrmarch")]
        [TestCase(CreatureConstants.FormianQueen, "Formian Queen")]
        [TestCase(CreatureConstants.FormianTaskmaster, "Formian Taskmaster")]
        [TestCase(CreatureConstants.FormianWarrior, "Formian Warrior")]
        [TestCase(CreatureConstants.FormianWorker, "Formian Worker")]
        [TestCase(CreatureConstants.IceDevil_Gelugon, "Ice Devil (Gelugon)")]
        [TestCase(CreatureConstants.Gargoyle_Kapoacinth, "Kapoacinth")]
        [TestCase(CreatureConstants.Ghoul_Ghast, "Ghast")]
        [TestCase(CreatureConstants.Ghoul_Lacedon, "Lacedon")]
        [TestCase(CreatureConstants.Giant_Stone_Elder, CreatureConstants.Giant_Stone + " Elder")]
        [TestCase(CreatureConstants.Golem_Clay, "Clay Golem")]
        [TestCase(CreatureConstants.Golem_Flesh, "Flesh Golem")]
        [TestCase(CreatureConstants.Golem_Iron, "Iron Golem")]
        [TestCase(CreatureConstants.Golem_Stone, "Stone Golem")]
        [TestCase(CreatureConstants.Golem_Stone_Greater, "Greater Stone Golem")]
        [TestCase(CreatureConstants.Hellcat_Bezekira, "Hellcat (Bezekira)")]
        [TestCase(CreatureConstants.Hydra_5Heads, "Five-Headed Hydra")]
        [TestCase(CreatureConstants.Hydra_6Heads, "Six-Headed Hydra")]
        [TestCase(CreatureConstants.Hydra_7Heads, "Seven-Headed Hydra")]
        [TestCase(CreatureConstants.Hydra_8Heads, "Eight-Headed Hydra")]
        [TestCase(CreatureConstants.Hydra_9Heads, "Nine-Headed Hydra")]
        [TestCase(CreatureConstants.Hydra_10Heads, "Ten-Headed Hydra")]
        [TestCase(CreatureConstants.Hydra_11Heads, "Eleven-Headed Hydra")]
        [TestCase(CreatureConstants.Hydra_12Heads, "Twelve-Headed Hydra")]
        [TestCase(CreatureConstants.Mephit_Air, "Air Mephit")]
        [TestCase(CreatureConstants.Mephit_Dust, "Dust Mephit")]
        [TestCase(CreatureConstants.Mephit_Earth, "Earth Mephit")]
        [TestCase(CreatureConstants.Mephit_Fire, "Fire Mephit")]
        [TestCase(CreatureConstants.Mephit_Ice, "Ice Mephit")]
        [TestCase(CreatureConstants.Mephit_Magma, "Magma Mephit")]
        [TestCase(CreatureConstants.Mephit_Ooze, "Ooze Mephit")]
        [TestCase(CreatureConstants.Mephit_Salt, "Salt Mephit")]
        [TestCase(CreatureConstants.Mephit_Steam, "Steam Mephit")]
        [TestCase(CreatureConstants.Mephit_Water, "Water Mephit")]
        [TestCase(CreatureConstants.Naga_Dark, "Dark Naga")]
        [TestCase(CreatureConstants.Naga_Guardian, "Guardian Naga")]
        [TestCase(CreatureConstants.Naga_Spirit, "Spirit Naga")]
        [TestCase(CreatureConstants.Naga_Water, "Water Naga")]
        [TestCase(CreatureConstants.Nightmare_Cauchemar, CreatureConstants.Nightmare + ", Cauchemar")]
        [TestCase(CreatureConstants.Ogre_Merrow, "Merrow")]
        [TestCase(CreatureConstants.BoneDevil_Osyluth, "Bone Devil (Osyluth)")]
        [TestCase(CreatureConstants.Pixie_WithIrresistibleDance, CreatureConstants.Pixie + " with " + SpellConstants.IrresistibleDance)]
        [TestCase(CreatureConstants.Pyrohydra_5Heads, "Five-Headed Pyrohydra")]
        [TestCase(CreatureConstants.Pyrohydra_6Heads, "Six-Headed Pyrohydra")]
        [TestCase(CreatureConstants.Pyrohydra_7Heads, "Seven-Headed Pyrohydra")]
        [TestCase(CreatureConstants.Pyrohydra_8Heads, "Eight-Headed Pyrohydra")]
        [TestCase(CreatureConstants.Pyrohydra_9Heads, "Nine-Headed Pyrohydra")]
        [TestCase(CreatureConstants.Pyrohydra_10Heads, "Ten-Headed Pyrohydra")]
        [TestCase(CreatureConstants.Pyrohydra_11Heads, "Eleven-Headed Pyrohydra")]
        [TestCase(CreatureConstants.Pyrohydra_12Heads, "Twelve-Headed Pyrohydra")]
        [TestCase(CreatureConstants.Salamander_Average, "Average Salamander")]
        [TestCase(CreatureConstants.Salamander_Flamebrother, "Flamebrother Salamander")]
        [TestCase(CreatureConstants.Salamander_Noble, "Noble Salamander")]
        [TestCase(CreatureConstants.Satyr_WithPipes, CreatureConstants.Satyr + " with pipes")]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Colossal, "Monstrous Scorpion, " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Gargantuan, "Monstrous Scorpion, " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Huge, "Monstrous Scorpion, " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Large, "Monstrous Scorpion, " + SizeConstants.Large)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Medium, "Monstrous Scorpion, " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Small, "Monstrous Scorpion, " + SizeConstants.Small)]
        [TestCase(CreatureConstants.Scorpion_Monstrous_Tiny, "Monstrous Scorpion, " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.Shadow_Greater, "Greater " + CreatureConstants.Shadow)]
        [TestCase(CreatureConstants.Shark_Medium, "Shark, " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.Shark_Large, "Shark, " + SizeConstants.Large)]
        [TestCase(CreatureConstants.Shark_Huge, "Shark, " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.Slaad_Blue, "Blue Slaad")]
        [TestCase(CreatureConstants.Slaad_Death, "Death Slaad")]
        [TestCase(CreatureConstants.Slaad_Gray, "Gray Slaad")]
        [TestCase(CreatureConstants.Slaad_Green, "Green Slaad")]
        [TestCase(CreatureConstants.Slaad_Red, "Red Slaad")]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Colossal, "Monstrous Spider, Hunter, " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Gargantuan, "Monstrous Spider, Hunter, " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Huge, "Monstrous Spider, Hunter, " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Large, "Monstrous Spider, Hunter, " + SizeConstants.Large)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Medium, "Monstrous Spider, Hunter, " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Small, "Monstrous Spider, Hunter, " + SizeConstants.Small)]
        [TestCase(CreatureConstants.Spider_Monstrous_Hunter_Tiny, "Monstrous Spider, Hunter, " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Colossal, "Monstrous Spider, Web-Spinner, " + SizeConstants.Colossal)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Gargantuan, "Monstrous Spider, Web-Spinner, " + SizeConstants.Gargantuan)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Huge, "Monstrous Spider, Web-Spinner, " + SizeConstants.Huge)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Large, "Monstrous Spider, Web-Spinner, " + SizeConstants.Large)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Medium, "Monstrous Spider, Web-Spinner, " + SizeConstants.Medium)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Small, "Monstrous Spider, Web-Spinner, " + SizeConstants.Small)]
        [TestCase(CreatureConstants.Spider_Monstrous_WebSpinner_Tiny, "Monstrous Spider, Web-Spinner, " + SizeConstants.Tiny)]
        [TestCase(CreatureConstants.Tojanida_Adult, "Adult Tojanida")]
        [TestCase(CreatureConstants.Tojanida_Elder, "Elder Tojanida")]
        [TestCase(CreatureConstants.Tojanida_Juvenile, "Juvenile Tojanida")]
        [TestCase(CreatureConstants.Troll_Scrag, "Scrag")]
        [TestCase(CreatureConstants.UmberHulk_TrulyHorrid, "Truly Horrid " + CreatureConstants.UmberHulk)]
        [TestCase(CreatureConstants.Whale_Baleen, "Baleen Whale")]
        [TestCase(CreatureConstants.Whale_Cachalot, "Cachalot Whale")]
        [TestCase(CreatureConstants.Whale_Orca, "Orca Whale")]
        [TestCase(CreatureConstants.Wraith_Dread, "Dread " + CreatureConstants.Wraith)]
        [TestCase(CreatureConstants.Xorn_Average, "Average Xorn")]
        [TestCase(CreatureConstants.Xorn_Elder, "Elder Xorn")]
        [TestCase(CreatureConstants.Xorn_Minor, "Minor Xorn")]
        [TestCase(CreatureConstants.YuanTi_Abomination, "Yuan-ti Abomination")]
        [TestCase(CreatureConstants.YuanTi_Halfblood_SnakeArms, "Yuan-ti Halfblood (Human Head, Arms are Snakes)")]
        [TestCase(CreatureConstants.YuanTi_Halfblood_SnakeHead, "Yuan-ti Halfblood (Snake Head)")]
        [TestCase(CreatureConstants.YuanTi_Halfblood_SnakeTail, "Yuan-ti Halfblood (Snake Head, Snake Tail instead of Human Legs)")]
        [TestCase(CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs, "Yuan-ti Halfblood (Snake Head, Snake Tail and Human Legs)")]
        [TestCase(CreatureConstants.YuanTi_Pureblood, "Yuan-ti Pureblood")]
        public void CreatureConstant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [TestCase(CreatureConstants.Templates.CelestialCreature, "Celestial Creature")]
        [TestCase(CreatureConstants.Templates.FiendishCreature, "Fiendish Creature")]
        [TestCase(CreatureConstants.Templates.Ghost, "Ghost")]
        [TestCase(CreatureConstants.Templates.HalfCelestial, "Half-Celestial")]
        [TestCase(CreatureConstants.Templates.HalfDragon_Black, "Half-Dragon (Black)")]
        [TestCase(CreatureConstants.Templates.HalfDragon_Blue, "Half-Dragon (Blue)")]
        [TestCase(CreatureConstants.Templates.HalfDragon_Green, "Half-Dragon (Green)")]
        [TestCase(CreatureConstants.Templates.HalfDragon_Red, "Half-Dragon (Red)")]
        [TestCase(CreatureConstants.Templates.HalfDragon_White, "Half-Dragon (White)")]
        [TestCase(CreatureConstants.Templates.HalfDragon_Brass, "Half-Dragon (Brass)")]
        [TestCase(CreatureConstants.Templates.HalfDragon_Bronze, "Half-Dragon (Bronze)")]
        [TestCase(CreatureConstants.Templates.HalfDragon_Copper, "Half-Dragon (Copper)")]
        [TestCase(CreatureConstants.Templates.HalfDragon_Gold, "Half-Dragon (Gold)")]
        [TestCase(CreatureConstants.Templates.HalfDragon_Silver, "Half-Dragon (Silver)")]
        [TestCase(CreatureConstants.Templates.HalfFiend, "Half-Fiend")]
        [TestCase(CreatureConstants.Templates.Lich, "Lich")]
        [TestCase(CreatureConstants.Templates.None, "None")]
        [TestCase(CreatureConstants.Templates.Skeleton, "Skeleton")]
        [TestCase(CreatureConstants.Templates.Vampire, "Vampire")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Bear_Black_Afflicted, "Lycanthrope, Black Bear (Werebear, Afflicted)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Bear_Black_Natural, "Lycanthrope, Black Bear (Werebear, Natural)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Afflicted, "Lycanthrope, Brown Bear (Werebear, Afflicted)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Bear_Brown_Natural, "Lycanthrope, Brown Bear (Werebear, Natural)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Bear_Dire_Afflicted, "Lycanthrope, Dire Bear (Dire Werebear, Afflicted)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Bear_Dire_Natural, "Lycanthrope, Dire Bear (Dire Werebear, Natural)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Bear_Polar_Afflicted, "Lycanthrope, Polar Bear (Werebear, Afflicted)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Bear_Polar_Natural, "Lycanthrope, Polar Bear (Werebear, Natural)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Boar_Afflicted, "Lycanthrope, Boar (Wereboar, Afflicted)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Boar_Natural, "Lycanthrope, Boar (Wereboar, Natural)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Afflicted, "Lycanthrope, Dire Boar (Dire Wereboar, Afflicted)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Boar_Dire_Natural, "Lycanthrope, Dire Boar (Dire Wereboar, Natural)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Afflicted, "Lycanthrope, Dire Rat (Wererat, Afflicted)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Rat_Dire_Natural, "Lycanthrope, Dire Rat (Wererat, Natural)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Tiger_Afflicted, "Lycanthrope, Tiger (Weretiger, Afflicted)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Tiger_Natural, "Lycanthrope, Tiger (Weretiger, Natural)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Afflicted, "Lycanthrope, Dire Tiger (Dire Weretiger, Afflicted)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Tiger_Dire_Natural, "Lycanthrope, Dire Tiger (Dire Weretiger, Natural)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Wolf_Afflicted, "Lycanthrope, Wolf (Werewolf, Afflicted)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Wolf_Natural, "Lycanthrope, Wolf (Werewolf, Natural)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Afflicted, "Lycanthrope, Dire Wolf (Dire Werewolf, Afflicted)")]
        [TestCase(CreatureConstants.Templates.Lycanthrope_Wolf_Dire_Natural, "Lycanthrope, Dire Wolf (Dire Werewolf, Natural)")]
        [TestCase(CreatureConstants.Templates.Zombie, "Zombie")]
        public void CreatureTemplateConstant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [TestCase(CreatureConstants.Types.Aberration, "Aberration")]
        [TestCase(CreatureConstants.Types.Animal, "Animal")]
        [TestCase(CreatureConstants.Types.Construct, "Construct")]
        [TestCase(CreatureConstants.Types.Dragon, "Dragon")]
        [TestCase(CreatureConstants.Types.Elemental, "Elemental")]
        [TestCase(CreatureConstants.Types.Fey, "Fey")]
        [TestCase(CreatureConstants.Types.Giant, "Giant")]
        [TestCase(CreatureConstants.Types.Humanoid, "Humanoid")]
        [TestCase(CreatureConstants.Types.MagicalBeast, "Magical Beast")]
        [TestCase(CreatureConstants.Types.MonstrousHumanoid, "Monstrous Humanoid")]
        [TestCase(CreatureConstants.Types.Ooze, "Ooze")]
        [TestCase(CreatureConstants.Types.Outsider, "Outsider")]
        [TestCase(CreatureConstants.Types.Plant, "Plant")]
        [TestCase(CreatureConstants.Types.Undead, "Undead")]
        [TestCase(CreatureConstants.Types.Vermin, "Vermin")]
        [TestCase(CreatureConstants.Types.Subtypes.Air, "Air")]
        [TestCase(CreatureConstants.Types.Subtypes.Angel, "Angel")]
        [TestCase(CreatureConstants.Types.Subtypes.Aquatic, "Aquatic")]
        [TestCase(CreatureConstants.Types.Subtypes.Archon, "Archon")]
        [TestCase(CreatureConstants.Types.Subtypes.Augmented, "Augmented")]
        [TestCase(CreatureConstants.Types.Subtypes.Chaotic, "Chaotic")]
        [TestCase(CreatureConstants.Types.Subtypes.Cold, "Cold")]
        [TestCase(CreatureConstants.Types.Subtypes.Dwarf, "Dwarf")]
        [TestCase(CreatureConstants.Types.Subtypes.Earth, "Earth")]
        [TestCase(CreatureConstants.Types.Subtypes.Elf, "Elf")]
        [TestCase(CreatureConstants.Types.Subtypes.Evil, "Evil")]
        [TestCase(CreatureConstants.Types.Subtypes.Extraplanar, "Extraplanar")]
        [TestCase(CreatureConstants.Types.Subtypes.Fire, "Fire")]
        [TestCase(CreatureConstants.Types.Subtypes.Gnoll, "Gnoll")]
        [TestCase(CreatureConstants.Types.Subtypes.Gnome, "Gnome")]
        [TestCase(CreatureConstants.Types.Subtypes.Goblinoid, "Goblinoid")]
        [TestCase(CreatureConstants.Types.Subtypes.Good, "Good")]
        [TestCase(CreatureConstants.Types.Subtypes.Halfling, "Halfling")]
        [TestCase(CreatureConstants.Types.Subtypes.Human, "Human")]
        [TestCase(CreatureConstants.Types.Subtypes.Incorporeal, "Incorporeal")]
        [TestCase(CreatureConstants.Types.Subtypes.Lawful, "Lawful")]
        [TestCase(CreatureConstants.Types.Subtypes.Native, "Native")]
        [TestCase(CreatureConstants.Types.Subtypes.Orc, "Orc")]
        [TestCase(CreatureConstants.Types.Subtypes.Reptilian, "Reptilian")]
        [TestCase(CreatureConstants.Types.Subtypes.Shapechanger, "Shapechanger")]
        [TestCase(CreatureConstants.Types.Subtypes.Swarm, "Swarm")]
        [TestCase(CreatureConstants.Types.Subtypes.Water, "Water")]
        public void CreatureTypeConstant(string constant, string value)
        {
            Assert.That(constant, Is.EqualTo(value));
        }

        [Test]
        public void AllCreatureConstants()
        {
            var creatures = CreatureConstants.GetAll();
            var creatureConstants = typeof(CreatureConstants);
            var fields = creatureConstants.GetFields(BindingFlags.Public | BindingFlags.Static);
            var constantFields = fields.Where(f => f.IsLiteral && !f.IsInitOnly);
            var constants = constantFields.Select(f => f.GetValue(null) as string);

            //HACK: The failure message for Is.Equivalent is truncated because of the size of the collection
            //So, we will alter the assertions
            Assert.That(creatures, Is.Unique, "From GetAll");
            Assert.That(constants, Is.Unique, "From Reflection");
            Assert.That(creatures.Except(constants), Is.Empty, "From GetAll");
            Assert.That(constants.Except(creatures), Is.Empty, "From Reflection");
        }

        [Test]
        public void AllTemplates()
        {
            var templates = CreatureConstants.Templates.GetAll();
            var templateConstants = typeof(CreatureConstants.Templates);
            var fields = templateConstants.GetFields(BindingFlags.Public | BindingFlags.Static);
            var constantFields = fields.Where(f => f.IsLiteral && !f.IsInitOnly);
            var constants = constantFields.Select(f => f.GetValue(null) as string);

            Assert.That(templates, Is.EquivalentTo(constants));
        }

        [Test]
        public void AllTypes()
        {
            var types = CreatureConstants.Types.GetAll();
            var typeConstants = typeof(CreatureConstants.Types);
            var fields = typeConstants.GetFields(BindingFlags.Public | BindingFlags.Static);
            var constantFields = fields.Where(f => f.IsLiteral && !f.IsInitOnly);
            var constants = constantFields.Select(f => f.GetValue(null) as string);

            Assert.That(types, Is.EquivalentTo(constants));
        }

        [Test]
        public void AllSubtypes()
        {
            var subtypes = CreatureConstants.Types.Subtypes.GetAll();
            var subtypeConstants = typeof(CreatureConstants.Types.Subtypes);
            var fields = subtypeConstants.GetFields(BindingFlags.Public | BindingFlags.Static);
            var constantFields = fields.Where(f => f.IsLiteral && !f.IsInitOnly);
            var constants = constantFields.Select(f => f.GetValue(null) as string);

            Assert.That(subtypes, Is.EquivalentTo(constants));
        }

        [Test]
        public void AllCharacters()
        {
            var characters = CreatureConstants.GetAllCharacters();
            Assert.That(characters, Is.EquivalentTo(new[]
            {
                CreatureConstants.Aasimar,
                CreatureConstants.Aboleth,
                CreatureConstants.Androsphinx,
                CreatureConstants.Angel_AstralDeva,
                CreatureConstants.Annis,
                CreatureConstants.Aranea,
                CreatureConstants.Athach,
                CreatureConstants.Azer,
                CreatureConstants.BeardedDevil_Barbazu,
                CreatureConstants.BlinkDog,
                CreatureConstants.Bralani,
                CreatureConstants.Bugbear,
                CreatureConstants.Centaur,
                CreatureConstants.ChainDevil_Kyton,
                CreatureConstants.Chimera_Black,
                CreatureConstants.Chimera_Blue,
                CreatureConstants.Chimera_Green,
                CreatureConstants.Chimera_Red,
                CreatureConstants.Chimera_White,
                CreatureConstants.Couatl,
                CreatureConstants.Criosphinx,
                CreatureConstants.Derro,
                CreatureConstants.Derro_Sane,
                CreatureConstants.DisplacerBeast,
                CreatureConstants.Djinni,
                CreatureConstants.Doppelganger,
                CreatureConstants.Dragon_Black_Wyrmling,
                CreatureConstants.Dragon_Black_VeryYoung,
                CreatureConstants.Dragon_Black_Young,
                CreatureConstants.Dragon_Black_Juvenile,
                CreatureConstants.Dragon_Blue_Wyrmling,
                CreatureConstants.Dragon_Blue_VeryYoung,
                CreatureConstants.Dragon_Blue_Young,
                CreatureConstants.Dragon_Green_Wyrmling,
                CreatureConstants.Dragon_Green_VeryYoung,
                CreatureConstants.Dragon_Green_Young,
                CreatureConstants.Dragon_Green_Juvenile,
                CreatureConstants.Dragon_Red_Wyrmling,
                CreatureConstants.Dragon_Red_VeryYoung,
                CreatureConstants.Dragon_Red_Young,
                CreatureConstants.Dragon_White_Wyrmling,
                CreatureConstants.Dragon_White_VeryYoung,
                CreatureConstants.Dragon_White_Young,
                CreatureConstants.Dragon_White_Juvenile,
                CreatureConstants.Dragon_Brass_Wyrmling,
                CreatureConstants.Dragon_Brass_VeryYoung,
                CreatureConstants.Dragon_Brass_Young,
                CreatureConstants.Dragon_Brass_Juvenile,
                CreatureConstants.Dragon_Bronze_Wyrmling,
                CreatureConstants.Dragon_Bronze_VeryYoung,
                CreatureConstants.Dragon_Bronze_Young,
                CreatureConstants.Dragon_Copper_Wyrmling,
                CreatureConstants.Dragon_Copper_VeryYoung,
                CreatureConstants.Dragon_Copper_Young,
                CreatureConstants.Dragon_Copper_Juvenile,
                CreatureConstants.Dragon_Gold_Wyrmling,
                CreatureConstants.Dragon_Gold_VeryYoung,
                CreatureConstants.Dragon_Gold_Young,
                CreatureConstants.Dragon_Silver_Wyrmling,
                CreatureConstants.Dragon_Silver_VeryYoung,
                CreatureConstants.Dragon_Silver_Young,
                CreatureConstants.Dragonne,
                CreatureConstants.Dretch,
                CreatureConstants.Drider,
                CreatureConstants.Dryad,
                CreatureConstants.Dwarf_Deep,
                CreatureConstants.Dwarf_Duergar,
                CreatureConstants.Dwarf_Hill,
                CreatureConstants.Dwarf_Mountain,
                CreatureConstants.Eagle_Giant,
                CreatureConstants.Elf_Aquatic,
                CreatureConstants.Elf_Drow,
                CreatureConstants.Elf_Gray,
                CreatureConstants.Elf_Half,
                CreatureConstants.Elf_High,
                CreatureConstants.Elf_Wild,
                CreatureConstants.Elf_Wood,
                CreatureConstants.Erinyes,
                CreatureConstants.Ettercap,
                CreatureConstants.Ettin,
                CreatureConstants.Gargoyle,
                CreatureConstants.Gargoyle_Kapoacinth,
                CreatureConstants.Giant_Cloud,
                CreatureConstants.Giant_Fire,
                CreatureConstants.Giant_Frost,
                CreatureConstants.Giant_Hill,
                CreatureConstants.Giant_Stone,
                CreatureConstants.Giant_Stone_Elder,
                CreatureConstants.Giant_Storm,
                CreatureConstants.Githyanki,
                CreatureConstants.Githzerai,
                CreatureConstants.Gnoll,
                CreatureConstants.Gnome_Forest,
                CreatureConstants.Gnome_Rock,
                CreatureConstants.Gnome_Svirfneblin,
                CreatureConstants.Goblin,
                CreatureConstants.GrayRender,
                CreatureConstants.GreenHag,
                CreatureConstants.Griffon,
                CreatureConstants.Grig,
                CreatureConstants.Grig_WithFiddle,
                CreatureConstants.Grimlock,
                CreatureConstants.Gynosphinx,
                CreatureConstants.Halfling_Deep,
                CreatureConstants.Halfling_Lightfoot,
                CreatureConstants.Halfling_Tallfellow,
                CreatureConstants.Harpy,
                CreatureConstants.HellHound,
                CreatureConstants.HellHound_NessianWarhound,
                CreatureConstants.Hezrou,
                CreatureConstants.Hieracosphinx,
                CreatureConstants.Hobgoblin,
                CreatureConstants.HoundArchon,
                CreatureConstants.Howler,
                CreatureConstants.Human,
                CreatureConstants.Janni,
                CreatureConstants.Kobold,
                CreatureConstants.Krenshar,
                CreatureConstants.KuoToa,
                CreatureConstants.Lamia,
                CreatureConstants.Lammasu,
                CreatureConstants.Lillend,
                CreatureConstants.Lizardfolk,
                CreatureConstants.Locathah,
                CreatureConstants.Manticore,
                CreatureConstants.Medusa,
                CreatureConstants.Mephit_Air,
                CreatureConstants.Mephit_Dust,
                CreatureConstants.Mephit_Earth,
                CreatureConstants.Mephit_Fire,
                CreatureConstants.Mephit_Ice,
                CreatureConstants.Mephit_Magma,
                CreatureConstants.Mephit_Ooze,
                CreatureConstants.Mephit_Salt,
                CreatureConstants.Mephit_Steam,
                CreatureConstants.Mephit_Water,
                CreatureConstants.Merfolk,
                CreatureConstants.MindFlayer,
                CreatureConstants.Minotaur,
                CreatureConstants.Mummy,
                CreatureConstants.Nightmare,
                CreatureConstants.Nightmare_Cauchemar,
                CreatureConstants.Nixie,
                CreatureConstants.Nymph,
                CreatureConstants.Ogre,
                CreatureConstants.OgreMage,
                CreatureConstants.Ogre_Merrow,
                CreatureConstants.Orc,
                CreatureConstants.Orc_Half,
                CreatureConstants.Owl_Giant,
                CreatureConstants.Pegasus,
                CreatureConstants.Pixie,
                CreatureConstants.Pixie_WithIrresistibleDance,
                CreatureConstants.Pseudodragon,
                CreatureConstants.Rakshasa,
                CreatureConstants.Sahuagin,
                CreatureConstants.Sahuagin_Malenti,
                CreatureConstants.Sahuagin_Mutant,
                CreatureConstants.Salamander_Average,
                CreatureConstants.Salamander_Flamebrother,
                CreatureConstants.Satyr,
                CreatureConstants.Satyr_WithPipes,
                CreatureConstants.Scorpionfolk,
                CreatureConstants.SeaHag,
                CreatureConstants.ShadowMastiff,
                CreatureConstants.ShamblingMound,
                CreatureConstants.Skum,
                CreatureConstants.Slaad_Blue,
                CreatureConstants.Slaad_Gray,
                CreatureConstants.Slaad_Green,
                CreatureConstants.Slaad_Red,
                CreatureConstants.Succubus,
                CreatureConstants.Tiefling,
                CreatureConstants.Treant,
                CreatureConstants.Triton,
                CreatureConstants.Troglodyte,
                CreatureConstants.Troll,
                CreatureConstants.Troll_Scrag,
                CreatureConstants.TrumpetArchon,
                CreatureConstants.Unicorn,
                CreatureConstants.Vrock,
                CreatureConstants.WinterWolf,
                CreatureConstants.Worg,
                CreatureConstants.Xill,
                CreatureConstants.YethHound,
                CreatureConstants.YuanTi_Abomination,
                CreatureConstants.YuanTi_Halfblood_SnakeArms,
                CreatureConstants.YuanTi_Halfblood_SnakeHead,
                CreatureConstants.YuanTi_Halfblood_SnakeTail,
                CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs,
                CreatureConstants.YuanTi_Pureblood,
                CreatureConstants.Zelekhut,
            }));
        }

        [Test]
        public void GetAllNonCharacters_ReturnsNonCharacters()
        {
            var nonCharacters = CreatureConstants.GetAllNonCharacters();
            var creatures = CreatureConstants.GetAll();
            var characters = CreatureConstants.GetAllCharacters();

            Assert.That(nonCharacters, Is.EqualTo(creatures.Except(characters)));
        }
    }
}
