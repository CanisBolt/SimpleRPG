using Game.Items;
using Game.SpecialAttack;

namespace Game.GameLocations
{
    internal class WorldFactory
    {
        internal World CreateWorld()
        {
            World newWorld = new World();

            newWorld.AddQuest("Welcome to the Village", World.QuestIDWelcomeToVillage, "Elder wants me to kill rat and bring 5 rat skins", "Bring me 5 rat skins", "I'm still waiting for 5 rat skins", "Finally, now rats will stop bothering this village, for now.", ItemsFactory.ItemByID(World.EnemyLootIDRatSkin), 5, 100, 50);

            NPC villageElder = new NPC("Village Elder", World.NPCIDVillageElder, "Welcome to our humble village, traveller.", World.QuestByID(World.QuestIDWelcomeToVillage));

            Shop villageShop = new Shop(World.ShopIDVillageShop);
            villageShop.Inventory.Add(ItemsFactory.CreateGameItem(World.ItemIDSmallHealingPotion));
            villageShop.Inventory.Add(ItemsFactory.CreateGameItem(World.ItemIDSmallManaPotion));

            newWorld.AddRegion("Village", World.RegionIDVillage);
            newWorld.RegionByID(World.RegionIDVillage).AddEnemy(EnemyFactory.GetMonster(World.EnemyIDRat));
            newWorld.RegionByID(World.RegionIDVillage).AddEnemy(EnemyFactory.GetMonster(World.EnemyIDSnake));

            newWorld.AddRegion("Forest", World.RegionIDForest);
            newWorld.RegionByID(World.RegionIDForest).AddEnemy(EnemyFactory.GetMonster(World.EnemyIDGoblin));
            newWorld.RegionByID(World.RegionIDForest).AddEnemy(EnemyFactory.GetMonster(World.EnemyIDWolf));
            newWorld.RegionByID(World.RegionIDForest).AddEnemy(EnemyFactory.GetMonster(World.EnemyIDRogue));
            newWorld.RegionByID(World.RegionIDForest).AddEnemy(EnemyFactory.GetMonster(World.EnemyIDForestWisp));

            newWorld.AddRace("Human", 0, 0, 0, 0, 0, 0, World.RaceIDHuman);
            newWorld.AddRace("Elf", -2, 2, -2, 1, 0, 1, World.RaceIDElf);
            newWorld.AddRace("Dwarf", 2, -2, 2, -2, 0, 0, World.RaceIDDwarf);
            newWorld.AddRace("DogFolk", -1, -2, 0, 2, 2, -1, World.RaceIDDogFolk);
            newWorld.AddRace("CatFolk", -2, 2, -1, 0, -1, 2, World.RaceIDCatFolk);

            newWorld.AddStatusEffect("Burn", World.StatusEffectIDBurn, "Fire covered your body and dealing Damage: 5% of maxHP. Duration: 3 turns.", 5f, 0, 3, StatusEffect.StatusType.DamageOverTime);
            newWorld.AddStatusEffect("Bleed", World.StatusEffectIDBleed, "Opened wound is bleeding, dealing Damage: 3% of maxHP. Duration: 5 turns.", 3f, 0, 5, StatusEffect.StatusType.DamageOverTime);
            newWorld.AddStatusEffect("Regeneration", World.StatusEffectIDRegeneration, "Your wounds is curing over time. Healing: 3% of maxHP. Duration: 3 turns.", 3f, 0, 3, StatusEffect.StatusType.HealOverTime);

            newWorld.AddSpecialAttack("Fireball", World.MagicIDFireball, "Throw a fireball to enemy. Base Damage: 3 + 0.8 for each Intelligence point. Mana cost: 5", 3f, 5, 0.8f, World.StatusEffectByID(World.StatusEffectIDBurn), Skills.Target.Enemy, Skills.Attribute.Intelligence, Skills.SpecialAttackType.Magic, GameItems.WeaponType.None);
            newWorld.AddSpecialAttack("IceArrow", World.MagicIDIceArrow, "Create an arrow of ice and shoot it to the enemy. Base Damage: 2 + 1.2 for each Intelligence point. Mana cost: 7", 1f, 7, 1.2f, null, Skills.Target.Enemy, Skills.Attribute.Intelligence, Skills.SpecialAttackType.Magic, GameItems.WeaponType.None);
            newWorld.AddSpecialAttack("ThunderStrike", World.MagicIDThunderStrike, "Summon a thunder than strike the enemy. Base Damage: 5 + 1 for each Intelligence point. Mana cost: 10", 5f, 10, 1f, null, Skills.Target.Enemy, Skills.Attribute.Intelligence, Skills.SpecialAttackType.Magic, GameItems.WeaponType.None);
            newWorld.AddSpecialAttack("Small Heal", World.MagicIDSmallHeal, "Cure yourself with magic. Base healing: 5 + 1.5 for Mind point. Mana cost: 10", 5, 10, 1.5f, null, SpecialAttack.Skills.Target.Self, Skills.Attribute.Mind, Skills.SpecialAttackType.Magic, GameItems.WeaponType.None);
            newWorld.AddSpecialAttack("Regeneration", World.MagicIDSmallHeal, "Casting regeneration, that heal your wounds over time. Base healing: 5 + 1.5 for Mind point. Mana cost: 10", 5, 10, 1.5f, World.StatusEffectByID(World.StatusEffectIDRegeneration), Skills.Target.Self, Skills.Attribute.Mind, Skills.SpecialAttackType.Magic, GameItems.WeaponType.None);


            newWorld.AddSpecialAttack("Sword: Fast Strike", World.SwordSKillIDFastStrike, "Deal fast strike with sword. Base Damage: 3 + 0.8 for each Agility point. Mana Cost: 5", 3, 5, 0.8f, null, Skills.Target.Enemy, Skills.Attribute.Agility, Skills.SpecialAttackType.Skill, GameItems.WeaponType.Sword);
            newWorld.AddSpecialAttack("Sword: Heavy Strike", World.SwordSKillIDHeavyStrike, "Deal heavy strike with sword. Base Damage: 5 + 1.5 for each Strength point. Mana Cost 15", 5, 15, 1.5f, World.StatusEffectByID(World.StatusEffectIDBleed), Skills.Target.Enemy, Skills.Attribute.Strength, Skills.SpecialAttackType.Skill, GameItems.WeaponType.Sword);
            newWorld.AddSpecialAttack("Sword: MultiHit", World.SwordSKillIDMultiHit, "Deals multiple hits (from 2 to 5). Base Damage from 1 hit: 2 + 0.5 for each Agility point. Mana Cost 10", 2, 10, 0.5f, null, Skills.Target.Enemy, Skills.Attribute.Agility, Skills.SpecialAttackType.Skill, GameItems.WeaponType.Sword);

            newWorld.AddLocation(0, -1, "Garden",
                "Your Garden", newWorld.RegionByID(World.RegionIDVillage), true);

            newWorld.AddLocation(0, 0, "Home",
                "Home sweet home", newWorld.RegionByID(World.RegionIDVillage), true);

            newWorld.AddLocation(0, 1, "House garden",
                "This is your garden", newWorld.RegionByID(World.RegionIDVillage), false);

            newWorld.AddLocation(0, 2, "Road to the House",
                "Road to your house", newWorld.RegionByID(World.RegionIDVillage), false);

            newWorld.AddLocation(0, 3, "North Street",
                "North Street. Most popular street in village. Here you can buy items.", newWorld.RegionByID(World.RegionIDVillage), false, null, villageElder);

            newWorld.AddLocation(-1, 3, "Village Shop",
                "Local shop", newWorld.RegionByID(World.RegionIDVillage), false, villageShop);

            newWorld.AddLocation(0, 4, "North Road to the Forest",
                "Road to the forest", newWorld.RegionByID(World.RegionIDVillage), false);

            newWorld.AddLocation(0, 5, "Forest South Entrance",
                "South forest entrance", newWorld.RegionByID(World.RegionIDForest), false);

            newWorld.AddLocation(0, 6, "Forest Crossroad",
                "South forest crossroad", newWorld.RegionByID(World.RegionIDForest), false);

            return newWorld;
        }
    }
}