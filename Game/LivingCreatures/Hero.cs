using Game.Items;
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
        private GameItems currentWeapon;
        public int CurrentEXP
        {
            get
            {
                return currentEXP;
            }
            set
            {
                OnPropertyChanged(nameof(currentEXP));
                currentEXP = value;
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
                OnPropertyChanged(nameof(expToLevel));
                expToLevel = value;
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
                OnPropertyChanged(nameof(money));
                money = value;
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
                OnPropertyChanged(nameof(skillPoints));
                skillPoints = value;
            }
        }
        public Gender HeroGender { get; set; }
        public Race HeroRace { get; set; }

        public GameItems CurrentWeapon
        {
            get
            {
                return currentWeapon;
            }
            set
            {
                OnPropertyChanged(nameof(currentWeapon));
                currentWeapon = value;
            }
        }
        public ObservableCollection<GameItems> Inventory { get; set; }

        public Hero(string name, int level, int strength, int agility, int vitality, int intelligence, int mind, int luck) : base(name, level, strength, agility, vitality, intelligence, mind, luck)
        {
            CurrentEXP = 0;
            EXPToLevel = Level * (100 * (Level * 2));
            Money = 0;
            Inventory = new ObservableCollection<GameItems>();
        }

        public enum Gender
        {
            Male,
            Female,
            Other
        }

        public override float PhysicalDamageCalculation()
        {
            float randomModificator = Dice.rng.Next(2, 4) * 0.4f; // TODO chance this to RNG Float
            return randomModificator * (Strength + (Dice.rng.Next(CurrentWeapon.MinimumDamage, CurrentWeapon.MaximumDamage) + 1));
        }
    }
}
