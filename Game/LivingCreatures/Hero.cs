using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.LivingCreatures
{
    public class Hero : Creature
    {
        private int currentEXP;
        private int expToLevel;
        private int money;
        private int skillPoints;
        private Items.GameItems currentWeapon;
        public int CurrentEXP
        {
            get
            {
                return currentEXP;
            }
            set
            {
                currentEXP = value;
                OnPropertyChanged(nameof(currentEXP));
            }
        }
        public int EXPToLevel
        {
            get
            {
                return expToLevel;
            }
            set
            {
                expToLevel = value;
                OnPropertyChanged(nameof(expToLevel));
            }
        }
        public int Money
        {
            get
            {
                return money;
            }
            set
            {
                money = value;
                OnPropertyChanged(nameof(money));
            }
        }
        public int SkillPoints
        {
            get
            {
                return skillPoints;
            }
            set
            {
                skillPoints = value;
                OnPropertyChanged(nameof(skillPoints));
            }
        }
        public Gender HeroGender { get; set; }
        public Race HeroRace { get; set; }

        public Items.GameItems CurrentWeapon
        {
            get
            {
                return currentWeapon;
            }
            set
            {
                currentWeapon = value;
                OnPropertyChanged(nameof(currentWeapon));
            }
        }


        public Hero(string name, int level, int strength, int agility, int vitality, int intelligence, int mind, int luck) : base(name, level, strength, agility, vitality, intelligence, mind, luck)
        {
            CurrentEXP = 0;
            EXPToLevel = Level * (100 * (Level * 2));
            Money = 0;
        }

        public void LevelUP()
        {
            if(CurrentEXP >= EXPToLevel)
            {
                CurrentEXP -= EXPToLevel;

                Level++;
                SkillPoints += 5;

                EXPToLevel = Level * (100 * (Level * 2));
            }
        }

        public enum Gender
        {
            Male,
            Female,
            Other
        }

        public override float PhysicalDamageCalculation()
        {
            return Dice.GetRandomModificator() * (Strength + (Dice.rng.Next(CurrentWeapon.MinimumDamage, CurrentWeapon.MaximumDamage) + 1));
        }

        public override float MagicDamageCalculation()
        {
            float damage = Dice.GetRandomModificator() * (CurrentSpell.BaseDamage + AddDamageModificator());
            if(CurrentWeapon.TypeOfWeapon.Equals(Items.GameItems.WeaponType.Staff))
            {
                damage *= 1.2f; // Increase magic damage by 20% if Hero is using a Staff Weapon
            }
            return damage;
        }

        public void HealingAfterDeath()
        {
            CurrentHP = MaxHP / 2;
            CurrentMP = MaxMP / 2;
        }
    }
}
