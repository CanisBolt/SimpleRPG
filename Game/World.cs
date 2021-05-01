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
        private static List<Magic> allMagicSpells = new List<Magic>();
        private static List<WeaponSkills> allSkills = new List<WeaponSkills>();
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
        public static int SwordSKillIDFastStrike = 0;
        public static int SwordSKillIDHeavyStrike = 1;
        public static int SwordSKillIDMultiHit = 2;

        // Status Effects
        public static int StatusEffectIDBurn = 0;
        public static int StatusEffectIDBleed = 1;
        public static int StatusEffectIDRegeneration = 2;

        // Items
        // Healing/MP recovery Items
        public static int ItemIDSmallHealingPotion = 0;
        public static int ItemIDMediumHealingPotion = 1;
        public static int ItemIDBigHealingPotion = 2;
        public static int ItemIDMaxHealingPotion = 3;
        public static int ItemIDSmallManaPotion = 4;
        public static int ItemIDMediumManaPotion = 5;
        public static int ItemIDBigManaPotion = 6;
        public static int ItemIDMaxManaPotion = 7;
        // Weapons
        public static int WeaponIDWoodStaff = 100;
        public static int WeaponIDWoodSword = 101;


        internal void AddRegion(string name, int id)
        {
            Region region = new Region
            {
                Name = name,
                ID = id
            };

            allRegions.Add(region);
        }

        internal void AddLocation(int xCoordinate, int yCoordinate, string name, string description, Region region, bool isCheckpoint)
        {
            Location loc = new Location
            {
                XCoordinate = xCoordinate,
                YCoordinate = yCoordinate,
                Name = name,
                Description = description,
                Region = region,
                IsCheckpoint = isCheckpoint
            };

            allLocations.Add(loc);
        }

        internal void AddMagic(string name, int id, string description, float spellDamage, int manaCost, float attributeModificator, Enum target, Enum modificator, StatusEffect effect)
        {
            Magic magic = new Magic
            {
                Name = name,
                ID = id,
                Description = description,
                BaseDamage = spellDamage,
                ManaCost = manaCost,
                AttributeModificator = attributeModificator,
                AffectedTarger = target,
                Modificator = modificator,
                Effect = effect
            };

            allMagicSpells.Add(magic);
        }

        internal void AddSkill(string name, int id, string description, float spellDamage, int manaCost, float attributeModificator, int numberOfHits, Enum target, Enum requiredWeapon, Enum modificator, StatusEffect effect)
        {
            WeaponSkills skill = new WeaponSkills
            {
                Name = name,
                ID = id,
                Description = description,
                BaseDamage = spellDamage,
                ManaCost = manaCost,
                AttributeModificator = attributeModificator,
                NumberOfHits = numberOfHits,
                AffectedTarger = target,
                RequiredWeapon = requiredWeapon,
                Modificator = modificator
            };

            allSkills.Add(skill);
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

        public static Magic SpellByID(int id)
        {
            foreach (var spell in allMagicSpells)
            {
                if (spell.ID == id)
                {
                    return spell;
                }
            }
            return null;
        }

        public static WeaponSkills SkillByID(int id)
        {
            foreach (var skill in allSkills)
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