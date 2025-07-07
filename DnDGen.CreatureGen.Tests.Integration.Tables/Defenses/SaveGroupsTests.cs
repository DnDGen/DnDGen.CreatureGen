using DnDGen.CreatureGen.Creatures;
using DnDGen.CreatureGen.Defenses;
using DnDGen.CreatureGen.Selectors.Selections;
using DnDGen.CreatureGen.Tables;
using DnDGen.CreatureGen.Tests.Integration.TestData;
using DnDGen.Infrastructure.Selectors.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DnDGen.CreatureGen.Tests.Integration.Tables.Defenses
{
    [TestFixture]
    public class SaveGroupsTests : CollectionTests
    {
        protected override string tableName => TableNameConstants.Collection.SaveGroups;

        private Dictionary<string, IEnumerable<string>> saveGroups;
        private ICollectionDataSelector<CreatureDataSelection> creatureDataSelector;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            saveGroups = GetSaveGroups();
        }

        [SetUp]
        public void Setup()
        {
            creatureDataSelector = GetNewInstanceOf<ICollectionDataSelector<CreatureDataSelection>>();
        }

        private Dictionary<string, IEnumerable<string>> GetSaveGroups()
        {
            var groups = new Dictionary<string, IEnumerable<string>>();
            var creatures = CreatureConstants.GetAll();
            var templates = CreatureConstants.Templates.GetAll();

            foreach (var creature in creatures)
                groups[creature] = [];

            foreach (var template in templates)
                groups[template] = [];

            groups[CreatureConstants.Aasimar] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Aboleth] = [SaveConstants.Will];
            groups[CreatureConstants.Achaierai] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Androsphinx] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Angel_AstralDeva] = [SaveConstants.Will];
            groups[CreatureConstants.Angel_Planetar] = [SaveConstants.Will];
            groups[CreatureConstants.Angel_Solar] = [SaveConstants.Will];
            groups[CreatureConstants.Ankheg] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Annis] = [SaveConstants.Will];
            groups[CreatureConstants.Ape] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Ape_Dire] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Aranea] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Arrowhawk_Juvenile] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Arrowhawk_Adult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Arrowhawk_Elder] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Athach] = [SaveConstants.Fortitude, SaveConstants.Will];
            groups[CreatureConstants.Baboon] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Badger] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Badger_Dire] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Basilisk] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Basilisk_Greater] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Bat] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Bat_Dire] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Bat_Swarm] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Bear_Black] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Bear_Brown] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Bear_Dire] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Bear_Polar] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Behir] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Beholder] = [SaveConstants.Will];
            groups[CreatureConstants.Beholder_Gauth] = [SaveConstants.Will];
            groups[CreatureConstants.Belker] = [SaveConstants.Reflex];
            groups[CreatureConstants.Bison] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.BlinkDog] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Boar] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Boar_Dire] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Bugbear] = [SaveConstants.Reflex];
            groups[CreatureConstants.Bulette] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Camel_Bactrian] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Camel_Dromedary] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.CarrionCrawler] = [SaveConstants.Will];
            groups[CreatureConstants.Cat] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Centaur] = [SaveConstants.Will];
            groups[CreatureConstants.Cheetah] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Chimera_Black] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Chimera_Blue] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Chimera_Green] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Chimera_Red] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Chimera_White] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Choker] = [SaveConstants.Will];
            groups[CreatureConstants.Chuul] = [SaveConstants.Will];
            groups[CreatureConstants.Cloaker] = [SaveConstants.Will];
            groups[CreatureConstants.Cockatrice] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Criosphinx] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Crocodile] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Crocodile_Giant] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Cryohydra_5Heads] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Cryohydra_6Heads] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Cryohydra_7Heads] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Cryohydra_8Heads] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Cryohydra_9Heads] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Cryohydra_10Heads] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Cryohydra_11Heads] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Cryohydra_12Heads] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Darkmantle] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Deinonychus] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Delver] = [SaveConstants.Will];
            groups[CreatureConstants.Derro] = [SaveConstants.Will];
            groups[CreatureConstants.Derro_Sane] = [SaveConstants.Will];
            groups[CreatureConstants.Destrachan] = [SaveConstants.Will];
            groups[CreatureConstants.Digester] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.DisplacerBeast] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.DisplacerBeast_PackLord] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Dog] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Dog_Riding] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Donkey] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Doppelganger] = [SaveConstants.Will];
            groups[CreatureConstants.Dragon_Black_Wyrmling] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Black_VeryYoung] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Black_Young] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Black_Juvenile] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Black_YoungAdult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Black_Adult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Black_MatureAdult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Black_Old] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Black_VeryOld] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Black_Ancient] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Black_Wyrm] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Black_GreatWyrm] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Blue_Wyrmling] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Blue_VeryYoung] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Blue_Young] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Blue_Juvenile] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Blue_YoungAdult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Blue_Adult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Blue_MatureAdult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Blue_Old] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Blue_VeryOld] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Blue_Ancient] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Blue_Wyrm] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Blue_GreatWyrm] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Brass_Wyrmling] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Brass_VeryYoung] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Brass_Young] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Brass_Juvenile] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Brass_YoungAdult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Brass_Adult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Brass_MatureAdult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Brass_Old] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Brass_VeryOld] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Brass_Ancient] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Brass_Wyrm] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Brass_GreatWyrm] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Bronze_Wyrmling] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Bronze_VeryYoung] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Bronze_Young] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Bronze_Juvenile] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Bronze_YoungAdult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Bronze_Adult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Bronze_MatureAdult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Bronze_Old] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Bronze_VeryOld] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Bronze_Ancient] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Bronze_Wyrm] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Bronze_GreatWyrm] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Copper_Wyrmling] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Copper_VeryYoung] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Copper_Young] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Copper_Juvenile] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Copper_YoungAdult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Copper_Adult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Copper_MatureAdult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Copper_Old] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Copper_VeryOld] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Copper_Ancient] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Copper_Wyrm] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Copper_GreatWyrm] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Gold_Wyrmling] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Gold_VeryYoung] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Gold_Young] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Gold_Juvenile] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Gold_YoungAdult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Gold_Adult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Gold_MatureAdult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Gold_Old] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Gold_VeryOld] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Gold_Ancient] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Gold_Wyrm] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Gold_GreatWyrm] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Green_Wyrmling] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Green_VeryYoung] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Green_Young] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Green_Juvenile] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Green_YoungAdult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Green_Adult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Green_MatureAdult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Green_Old] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Green_VeryOld] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Green_Ancient] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Green_Wyrm] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Green_GreatWyrm] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Red_Wyrmling] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Red_VeryYoung] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Red_Young] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Red_Juvenile] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Red_YoungAdult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Red_Adult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Red_MatureAdult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Red_Old] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Red_VeryOld] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Red_Ancient] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Red_Wyrm] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Red_GreatWyrm] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Silver_Wyrmling] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Silver_VeryYoung] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Silver_Young] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Silver_Juvenile] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Silver_YoungAdult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Silver_Adult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Silver_MatureAdult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Silver_Old] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Silver_VeryOld] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Silver_Ancient] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Silver_Wyrm] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_Silver_GreatWyrm] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_White_Wyrmling] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_White_VeryYoung] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_White_Young] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_White_Juvenile] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_White_YoungAdult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_White_Adult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_White_MatureAdult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_White_Old] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_White_VeryOld] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_White_Ancient] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_White_Wyrm] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragon_White_GreatWyrm] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.DragonTurtle] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dragonne] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Drider] = [SaveConstants.Will];
            groups[CreatureConstants.Dryad] = [SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Dwarf_Deep] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Dwarf_Duergar] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Dwarf_Hill] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Dwarf_Mountain] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Eagle] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Eagle_Giant] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Elasmosaurus] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Elephant] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Air_Elder] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Air_Greater] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Air_Huge] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Air_Large] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Air_Medium] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Air_Small] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Earth_Elder] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elemental_Earth_Greater] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elemental_Earth_Huge] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elemental_Earth_Large] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elemental_Earth_Medium] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elemental_Earth_Small] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elemental_Fire_Elder] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Fire_Greater] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Fire_Huge] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Fire_Large] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Fire_Medium] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Fire_Small] = [SaveConstants.Reflex];
            groups[CreatureConstants.Elemental_Water_Elder] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elemental_Water_Greater] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elemental_Water_Huge] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elemental_Water_Large] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elemental_Water_Medium] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elemental_Water_Small] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elf_Aquatic] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elf_Drow] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elf_Gray] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elf_Half] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elf_High] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elf_Wild] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Elf_Wood] = [SaveConstants.Fortitude];
            groups[CreatureConstants.EtherealFilcher] = [SaveConstants.Will];
            groups[CreatureConstants.EtherealMarauder] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Ettercap] = [SaveConstants.Will];
            groups[CreatureConstants.Ettin] = [SaveConstants.Fortitude];
            groups[CreatureConstants.FrostWorm] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Gargoyle] = [SaveConstants.Will];
            groups[CreatureConstants.Gargoyle_Kapoacinth] = [SaveConstants.Will];
            groups[CreatureConstants.Giant_Cloud] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Giant_Fire] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Giant_Frost] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Giant_Hill] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Giant_Stone] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Giant_Stone_Elder] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Giant_Storm] = [SaveConstants.Fortitude];
            groups[CreatureConstants.GibberingMouther] = [SaveConstants.Will];
            groups[CreatureConstants.Girallon] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Githyanki] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Githzerai] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Gnoll] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Gnome_Forest] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Gnome_Rock] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Gnome_Svirfneblin] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Goblin] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Gorgon] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.GrayRender] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.GreenHag] = [SaveConstants.Will];
            groups[CreatureConstants.Grick] = [SaveConstants.Will];
            groups[CreatureConstants.Griffon] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Grig] = [SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Grig_WithFiddle] = [SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Grimlock] = [SaveConstants.Will];
            groups[CreatureConstants.Gynosphinx] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Halfling_Deep] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Halfling_Lightfoot] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Halfling_Tallfellow] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Harpy] = [SaveConstants.Will];
            groups[CreatureConstants.Hawk] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Hellwasp_Swarm] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Hieracosphinx] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Hippogriff] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Hobgoblin] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Horse_Heavy] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Horse_Heavy_War] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Horse_Light] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Horse_Light_War] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Human] = [SaveConstants.Reflex];
            groups[CreatureConstants.Hydra_5Heads] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Hydra_6Heads] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Hydra_7Heads] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Hydra_8Heads] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Hydra_9Heads] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Hydra_10Heads] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Hydra_11Heads] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Hydra_12Heads] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Hyena] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.InvisibleStalker] = [SaveConstants.Reflex];
            groups[CreatureConstants.Kobold] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Kraken] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Krenshar] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.KuoToa] = [SaveConstants.Will];
            groups[CreatureConstants.Lamia] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Lammasu] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Leopard] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Lion] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Lion_Dire] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Lizard] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Lizard_Monitor] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Lizardfolk] = [SaveConstants.Reflex];
            groups[CreatureConstants.Locathah] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Magmin] = [SaveConstants.Reflex];
            groups[CreatureConstants.MantaRay] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Manticore] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Medusa] = [SaveConstants.Will];
            groups[CreatureConstants.Megaraptor] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Mephit_Air] = [SaveConstants.Will];
            groups[CreatureConstants.Mephit_Dust] = [SaveConstants.Will];
            groups[CreatureConstants.Mephit_Earth] = [SaveConstants.Will];
            groups[CreatureConstants.Mephit_Fire] = [SaveConstants.Will];
            groups[CreatureConstants.Mephit_Ice] = [SaveConstants.Will];
            groups[CreatureConstants.Mephit_Magma] = [SaveConstants.Will];
            groups[CreatureConstants.Mephit_Ooze] = [SaveConstants.Will];
            groups[CreatureConstants.Mephit_Salt] = [SaveConstants.Will];
            groups[CreatureConstants.Mephit_Steam] = [SaveConstants.Will];
            groups[CreatureConstants.Mephit_Water] = [SaveConstants.Will];
            groups[CreatureConstants.Merfolk] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Mimic] = [SaveConstants.Will];
            groups[CreatureConstants.MindFlayer] = [SaveConstants.Will];
            groups[CreatureConstants.Minotaur] = [SaveConstants.Will];
            groups[CreatureConstants.Monkey] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Mule] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Naga_Dark] = [SaveConstants.Will];
            groups[CreatureConstants.Naga_Guardian] = [SaveConstants.Will];
            groups[CreatureConstants.Naga_Spirit] = [SaveConstants.Will];
            groups[CreatureConstants.Naga_Water] = [SaveConstants.Will];
            groups[CreatureConstants.Nixie] = [SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Nymph] = [SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Octopus] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Octopus_Giant] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Ogre] = [SaveConstants.Fortitude];
            groups[CreatureConstants.OgreMage] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Ogre_Merrow] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Orc] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Orc_Half] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Otyugh] = [SaveConstants.Will];
            groups[CreatureConstants.Owl] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Owl_Giant] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Owlbear] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Pegasus] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.PhaseSpider] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Phasm] = [SaveConstants.Will];
            groups[CreatureConstants.Pixie] = [SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Pixie_WithIrresistibleDance] = [SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Pony] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Pony_War] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Porpoise] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Pseudodragon] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.PurpleWorm] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Pyrohydra_5Heads] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Pyrohydra_6Heads] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Pyrohydra_7Heads] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Pyrohydra_8Heads] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Pyrohydra_9Heads] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Pyrohydra_10Heads] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Pyrohydra_11Heads] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Pyrohydra_12Heads] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Rat] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Rat_Dire] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Rat_Swarm] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Raven] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.RazorBoar] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Remorhaz] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Rhinoceras] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Roc] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Roper] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.RustMonster] = [SaveConstants.Will];
            groups[CreatureConstants.Sahuagin] = [SaveConstants.Will];
            groups[CreatureConstants.Sahuagin_Malenti] = [SaveConstants.Will];
            groups[CreatureConstants.Sahuagin_Mutant] = [SaveConstants.Will];
            groups[CreatureConstants.Salamander_Average] = [SaveConstants.Will];
            groups[CreatureConstants.Salamander_Flamebrother] = [SaveConstants.Will];
            groups[CreatureConstants.Salamander_Noble] = [SaveConstants.Will];
            groups[CreatureConstants.Satyr] = [SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Satyr_WithPipes] = [SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Scorpionfolk] = [SaveConstants.Will];
            groups[CreatureConstants.SeaCat] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.SeaHag] = [SaveConstants.Will];
            groups[CreatureConstants.Shark_Dire] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Shark_Huge] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Shark_Large] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Shark_Medium] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.ShockerLizard] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Skum] = [SaveConstants.Will];
            groups[CreatureConstants.Snake_Constrictor] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Snake_Constrictor_Giant] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Snake_Viper_Huge] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Snake_Viper_Large] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Snake_Viper_Medium] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Snake_Viper_Small] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Snake_Viper_Tiny] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.SpiderEater] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Squid] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Squid_Giant] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Stirge] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Tarrasque] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Thoqqua] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Tiger] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Tiger_Dire] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Toad] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Tojanida_Juvenile] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Tojanida_Adult] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Tojanida_Elder] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Triceratops] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Troglodyte] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Troll] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Troll_Scrag] = [SaveConstants.Fortitude];
            groups[CreatureConstants.Tyrannosaurus] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.UmberHulk] = [SaveConstants.Will];
            groups[CreatureConstants.UmberHulk_TrulyHorrid] = [SaveConstants.Will];
            groups[CreatureConstants.Unicorn] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Weasel] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Weasel_Dire] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Whale_Baleen] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Whale_Cachalot] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Whale_Orca] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.WillOWisp] = [SaveConstants.Will];
            groups[CreatureConstants.WinterWolf] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Wolf] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Wolf_Dire] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Wolverine] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Wolverine_Dire] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Worg] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.Wyvern] = [SaveConstants.Fortitude, SaveConstants.Reflex, SaveConstants.Will];
            groups[CreatureConstants.Xorn_Average] = [SaveConstants.Will];
            groups[CreatureConstants.Xorn_Elder] = [SaveConstants.Will];
            groups[CreatureConstants.Xorn_Minor] = [SaveConstants.Will];
            groups[CreatureConstants.Yrthak] = [SaveConstants.Fortitude, SaveConstants.Reflex];
            groups[CreatureConstants.YuanTi_Abomination] = [SaveConstants.Will];
            groups[CreatureConstants.YuanTi_Halfblood_SnakeArms] = [SaveConstants.Will];
            groups[CreatureConstants.YuanTi_Halfblood_SnakeHead] = [SaveConstants.Will];
            groups[CreatureConstants.YuanTi_Halfblood_SnakeTail] = [SaveConstants.Will];
            groups[CreatureConstants.YuanTi_Halfblood_SnakeTailAndHumanLegs] = [SaveConstants.Will];
            groups[CreatureConstants.YuanTi_Pureblood] = [SaveConstants.Will];

            groups[CreatureConstants.Templates.Skeleton] = [SaveConstants.Will];
            groups[CreatureConstants.Templates.Zombie] = [SaveConstants.Will];

            return groups;
        }

        [Test]
        public void SaveGroupsNames()
        {
            var creatures = CreatureConstants.GetAll();
            var templates = CreatureConstants.Templates.GetAll();

            var names = creatures.Union(templates);
            Assert.That(saveGroups.Keys, Is.EquivalentTo(names));
            AssertCollectionNames(names);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Creatures))]
        public void CreatureSaveGroupHasStrongSaves(string creature)
        {
            AssertDistinctCollection(creature, [.. saveGroups[creature]]);
        }

        [TestCaseSource(typeof(CreatureTestData), nameof(CreatureTestData.Templates))]
        public void TemplateSaveGroupHasStrongSaves(string template)
        {
            AssertDistinctCollection(template, [.. saveGroups[template]]);
        }

        [TestCase(SaveConstants.Fortitude,
            CreatureConstants.Githyanki,
            CreatureConstants.Githzerai,
            CreatureConstants.Gnoll,
            CreatureConstants.Goblin,
            CreatureConstants.Hobgoblin,
            CreatureConstants.Kobold,
            CreatureConstants.Locathah,
            CreatureConstants.Merfolk,
            CreatureConstants.Thoqqua,
            CreatureConstants.Troglodyte,
            CreatureConstants.Dwarf_Deep,
            CreatureConstants.Dwarf_Duergar,
            CreatureConstants.Dwarf_Hill,
            CreatureConstants.Dwarf_Mountain,
            CreatureConstants.Elemental_Earth_Elder,
            CreatureConstants.Elemental_Earth_Greater,
            CreatureConstants.Elemental_Earth_Huge,
            CreatureConstants.Elemental_Earth_Large,
            CreatureConstants.Elemental_Earth_Medium,
            CreatureConstants.Elemental_Earth_Small,
            CreatureConstants.Elemental_Water_Elder,
            CreatureConstants.Elemental_Water_Greater,
            CreatureConstants.Elemental_Water_Huge,
            CreatureConstants.Elemental_Water_Large,
            CreatureConstants.Elemental_Water_Medium,
            CreatureConstants.Elemental_Water_Small,
            CreatureConstants.Elf_Aquatic,
            CreatureConstants.Elf_Drow,
            CreatureConstants.Elf_Gray,
            CreatureConstants.Elf_Half,
            CreatureConstants.Elf_High,
            CreatureConstants.Elf_Wild,
            CreatureConstants.Elf_Wood,
            CreatureConstants.Gnome_Forest,
            CreatureConstants.Gnome_Rock,
            CreatureConstants.Gnome_Svirfneblin,
            CreatureConstants.Halfling_Deep,
            CreatureConstants.Halfling_Lightfoot,
            CreatureConstants.Halfling_Tallfellow,
            CreatureConstants.Orc,
            CreatureConstants.Orc_Half,
            CreatureConstants.Types.Animal,
            CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Giant,
            CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Plant,
            CreatureConstants.Types.Vermin)]
        [TestCase(SaveConstants.Reflex,
            CreatureConstants.Belker,
            CreatureConstants.Bugbear,
            CreatureConstants.Human,
            CreatureConstants.InvisibleStalker,
            CreatureConstants.Lizardfolk,
            CreatureConstants.Magmin,
            CreatureConstants.Elemental_Air_Elder,
            CreatureConstants.Elemental_Air_Greater,
            CreatureConstants.Elemental_Air_Huge,
            CreatureConstants.Elemental_Air_Large,
            CreatureConstants.Elemental_Air_Medium,
            CreatureConstants.Elemental_Air_Small,
            CreatureConstants.Elemental_Fire_Elder,
            CreatureConstants.Elemental_Fire_Greater,
            CreatureConstants.Elemental_Fire_Huge,
            CreatureConstants.Elemental_Fire_Large,
            CreatureConstants.Elemental_Fire_Medium,
            CreatureConstants.Elemental_Fire_Small,
            CreatureConstants.Types.Animal,
            CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Fey,
            CreatureConstants.Types.MagicalBeast,
            CreatureConstants.Types.MonstrousHumanoid,
            CreatureConstants.Types.Outsider)]
        [TestCase(SaveConstants.Will,
            CreatureConstants.Ape_Dire,
            CreatureConstants.Badger_Dire,
            CreatureConstants.Bat_Dire,
            CreatureConstants.Bear_Dire,
            CreatureConstants.Boar_Dire,
            CreatureConstants.Lion_Dire,
            CreatureConstants.Rat_Dire,
            CreatureConstants.Shark_Dire,
            CreatureConstants.Tiger_Dire,
            CreatureConstants.Weasel_Dire,
            CreatureConstants.Wolf_Dire,
            CreatureConstants.Wolverine_Dire,
            CreatureConstants.Types.Aberration,
            CreatureConstants.Types.Dragon,
            CreatureConstants.Types.Fey,
            CreatureConstants.Types.MonstrousHumanoid,
            CreatureConstants.Types.Outsider,
            CreatureConstants.Types.Undead,
            CreatureConstants.Templates.Skeleton,
            CreatureConstants.Templates.Zombie)]
        public void LEGACY_CreatureSaveGroup(string save, params string[] group)
        {
            var creatures = CreatureConstants.GetAll();
            var types = CreatureConstants.Types.GetAll();

            foreach (var creature in group.Intersect(creatures))
            {
                Assert.That(saveGroups, Contains.Key(creature), creature);
                Assert.That(saveGroups[creature], Contains.Item(save), creature);
            }

            var creatureData = creatureDataSelector.SelectAllFrom(Config.Name, TableNameConstants.Collection.CreatureData);

            foreach (var creatureType in group.Intersect(types))
            {
                var typeCreatures = creatureData
                    .Where(d => d.Value.Single().Types.Contains(creatureType))
                    .Select(d => d.Key);
                var creaturesWithSave = saveGroups
                    .Where(sg => sg.Value.Contains(save))
                    .Select(sg => sg.Key);

                Assert.That(saveGroups.Keys, Is.SupersetOf(typeCreatures), creatureType);
                Assert.That(creaturesWithSave, Is.SupersetOf(typeCreatures), creatureType);
            }
        }
    }
}
