using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.GameLocations;
using Game.LivingCreatures;
using Game.SpecialAttack;

namespace Game
{
    public class World
    {
        private static List<Race> allRaces = new List<Race>();
        private List<Location> allLocations = new List<Location>();
        private List<Region> allRegions = new List<Region>();
        private static List<SpecialAttack.Skills> allSpecialAttacks = new List<SpecialAttack.Skills>();
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

        // Shop
        public static int ShopIDVillageShop = 0;

        // Items
        // Weapons
        public static int WeaponIDWoodStaff = 100;
        public static int WeaponIDWoodSword = 101;

        // Armors
        public static int WeaponIDNoHeadArmor = 200;
        public static int WeaponIDNoBodyArmor = 201;
        public static int WeaponIDNoLegsArmor = 202;
        public static int WeaponIDNoFeetArmor = 203;
        public static int WeaponIDSilkHat = 204;
        public static int WeaponIDSilkRobe = 205;
        public static int WeaponIDSilkPants = 206;
        public static int WeaponIDSilkSandals = 207;

        // Healing/MP recovery Items
        public static int ItemIDSmallHealingPotion = 0;
        public static int ItemIDMediumHealingPotion = 1;
        public static int ItemIDBigHealingPotion = 2;
        public static int ItemIDMaxHealingPotion = 3;
        public static int ItemIDSmallManaPotion = 4;
        public static int ItemIDMediumManaPotion = 5;
        public static int ItemIDBigManaPotion = 6;
        public static int ItemIDMaxManaPotion = 7;

        // Enemy Loot
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

        internal void AddRegion(string name, int id)
        {
            Region region = new Region
            {
                Name = name,
                ID = id
            };

            allRegions.Add(region);
        }

        internal void AddLocation(int xCoordinate, int yCoordinate, string name, string description, Region region, bool isCheckpoint, Shop shopOnLocation = null)
        {
            Location loc = new Location
            {
                XCoordinate = xCoordinate,
                YCoordinate = yCoordinate,
                Name = name,
                Description = description,
                Region = region,
                IsCheckpoint = isCheckpoint,
                ShopOnLocation = shopOnLocation
            };

            allLocations.Add(loc);
        }

        internal void AddSpecialAttack(string name, int id, string description, float baseDamage, int manaCost, float attributeModificator, StatusEffect effect, Enum target, Enum modificator, Enum type, Enum requiredWeapon, int numberOfHits = 1)
        {
            SpecialAttack.Skills specialAttack = new SpecialAttack.Skills()
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

        public static SpecialAttack.Skills SkillByID(int id)
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