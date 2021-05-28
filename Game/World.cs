using Game.GameLocations;
using Game.LivingCreatures;
using Game.SpecialAttack;
using System;
using System.Collections.Generic;

namespace Game
{
    public class World
    {
        private static List<Race> allRaces = new List<Race>();
        private static List<Quest> allQuests = new List<Quest>();
        private List<Location> allLocations = new List<Location>();
        private List<Region> allRegions = new List<Region>();
        private static List<Skills> allSpecialAttacks = new List<Skills>();
        private static List<StatusEffect> allStatusEffects = new List<StatusEffect>();

        // ID's

        // Races
        public static int RaceIDHuman = 0;
        public static int RaceIDElf = 1;
        public static int RaceIDDwarf = 2;
        public static int RaceIDDogFolk = 3;
        public static int RaceIDCatFolk = 4;

        // Regions
        public static int RegionIDVillage = 0;
        public static int RegionIDForest = 1;

        // Enemies
        public static int EnemyIDSnake = 0;
        public static int EnemyIDRat = 1;
        public static int EnemyIDGoblin = 2;
        public static int EnemyIDWolf = 3;
        public static int EnemyIDRogue = 4;
        public static int EnemyIDForestWisp = 5;

        // Magic
        public static int MagicIDFireball = 0;
        public static int MagicIDIceArrow = 1;
        public static int MagicIDThunderStrike = 2;
        public static int MagicIDSmallHeal = 3;

        // Skills
        public static int SwordSKillIDFastStrike = 30;
        public static int SwordSKillIDHeavyStrike = 31;
        public static int SwordSKillIDMultiHit = 32;

        // Status Effects
        public static int StatusEffectIDBurn = 0;
        public static int StatusEffectIDBleed = 1;
        public static int StatusEffectIDRegeneration = 2;

        // Quests
        public static int QuestIDWelcomeToVillage = 0;

        // NPC
        public static int NPCIDVillageElder = 0;

        // Shop
        public static int ShopIDVillageShop = 0;

        // Items
        // Starting Items
        public static int WeaponIDWoodStaff = 0;
        public static int WeaponIDWoodSword = 1;
        public static int ArmorIDNoHeadArmor = 2;
        public static int ArmorIDNoBodyArmor = 3;
        public static int ArmorIDNoLegsArmor = 4;
        public static int ArmorIDNoFeetArmor = 5;
        public static int ArmorIDSilkHat = 6;
        public static int ArmorIDSilkRobe = 7;
        public static int ArmorIDSilkPants = 8;
        public static int ArmorIDSilkSandals = 9;

        // Weapons (start from 100)

        // Armors (start from 200)

        // Healing/MP recovery Items (start from 10)
        public static int ItemIDSmallHealingPotion = 10;
        public static int ItemIDMediumHealingPotion = 11;
        public static int ItemIDBigHealingPotion = 12;
        public static int ItemIDMaxHealingPotion = 13;
        public static int ItemIDSmallManaPotion = 14;
        public static int ItemIDMediumManaPotion = 15;
        public static int ItemIDBigManaPotion = 16;
        public static int ItemIDMaxManaPotion = 17;

        // Material (start from 300)
        public static int MaterialIDHealingGrass = 300;

        // Enemy Loot (start from 1000)
        public static int EnemyLootIDSnakeSkin = 1000;
        public static int EnemyLootIDSnakeFang = 1001;
        public static int EnemyLootIDSnakeEye = 1002;
        public static int EnemyLootIDRatSkin = 1003;
        public static int EnemyLootIDRatTail = 1004;
        public static int EnemyLootIDGoblinSkin = 1005;
        public static int EnemyLootIDGoblinFang = 1006;
        public static int EnemyLootIDWolfSkin = 1007;
        public static int EnemyLootIDWolfFang = 1008;
        public static int EnemyLootIDWispDust = 1009;

        // Alchemy (start from 2000)
        public static int AlchemyRecipeIDSmallHealingPotion = 2000;

        internal void AddRegion(string name, int id)
        {
            Region region = new Region
            {
                Name = name,
                ID = id
            };

            allRegions.Add(region);
        }

        internal void AddLocation(int xCoordinate, int yCoordinate, string name, string description, Region region, bool isCheckpoint, Shop shopOnLocation = null, NPC npcOnLocation = null)
        {
            Location loc = new Location
            {
                XCoordinate = xCoordinate,
                YCoordinate = yCoordinate,
                Name = name,
                Description = description,
                Region = region,
                IsCheckpoint = isCheckpoint,
                ShopOnLocation = shopOnLocation,
                NPCOnLocation = npcOnLocation,
            };

            allLocations.Add(loc);
        }

        internal void AddQuest(string name, int id, string description, string startMessage, string inProgressMessage, string completeMessage, Items.GameItems requiredItems, int requiredCount, int rewardEXP, int rewardGold)
        {
            Quest quest = new Quest
            {
                Name = name,
                ID = id,
                Description = description,
                StartMessage = startMessage,
                InProgressMessage = inProgressMessage,
                CompleteMessage = completeMessage,
                RequiredItems = requiredItems,
                RequiredCount = requiredCount,
                RewardEXP = rewardEXP,
                RewardGold = rewardGold,
                QuestStatus = Quest.Status.Available,
            };

            allQuests.Add(quest);
        }

        internal void AddSpecialAttack(string name, int id, string description, float baseDamage, int manaCost, float attributeModificator, StatusEffect effect, Enum target, Enum modificator, Enum type, Enum requiredWeapon, int numberOfHits = 1)
        {
            Skills specialAttack = new Skills()
            {
                Name = name,
                ID = id,
                Description = description,
                BaseDamage = baseDamage,
                ManaCost = manaCost,
                AttributeModificator = attributeModificator,
                NumberOfHits = numberOfHits,
                Effect = effect,
                AffectedTarger = target,
                Modificator = modificator,
                Type = type,
                RequiredWeapon = requiredWeapon,
            };

            allSpecialAttacks.Add(specialAttack);
        }

        internal void AddStatusEffect(string name, int id, string description, float affectHP, float affectMP, int duration, Enum type)
        {
            StatusEffect status = new StatusEffect
            {
                Name = name,
                ID = id,
                Description = description,
                AffectHP = affectHP,
                AffectMP = affectMP,
                Duration = duration,
                Type = type
            };

            allStatusEffects.Add(status);
        }
        internal void AddRace(string name, int strength, int agility, int vitality, int intelligence, int mind, int luck, int id)
        {
            Race race = new Race
            {
                Name = name,
                Strength = strength,
                Agility = agility,
                Vitality = vitality,
                Intelligence = intelligence,
                Mind = mind,
                Luck = luck,
                ID = id,

            };

            allRaces.Add(race);
        }

        public Region RegionByID(int id)
        {
            foreach (var region in allRegions)
            {
                if (region.ID == id)
                {
                    return region;
                }
            }
            return null;
        }

        public static StatusEffect StatusEffectByID(int id)
        {
            foreach (var status in allStatusEffects)
            {
                if (status.ID == id)
                {
                    return status;
                }
            }
            return null;
        }
        public static Race RaceByID(int id)
        {
            foreach (var race in allRaces)
            {
                if (race.ID == id)
                {
                    return race;
                }
            }
            return null;
        }

        public static Quest QuestByID(int id)
        {
            foreach (var quest in allQuests)
            {
                if (quest.ID == id)
                {
                    return quest;
                }
            }
            return null;
        }

        public static Skills SkillByID(int id)
        {
            foreach (var skill in allSpecialAttacks)
            {
                if (skill.ID == id)
                {
                    return skill;
                }
            }
            return null;
        }

        public Location LocationAt(int xCoordinate, int yCoordinate)
        {
            foreach (Location loc in allLocations)
            {
                if (loc.XCoordinate == xCoordinate && loc.YCoordinate == yCoordinate)
                {
                    return loc;
                }
            }
            return null;
        }
    }
}