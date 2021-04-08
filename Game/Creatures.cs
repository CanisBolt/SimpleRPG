﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Creatures : BaseNotificationClass
    {
        private string name;
        private int level;
        private int currentHP;
        private int maxHP;
        private int currentMP;
        private int maxMP;
        private int strength;
        private int agility;
        private int vitality;
        private int intelligence;
        private int mind;
        private int luck;
        public string Name 
        {
            get
            {
                return name;
            }
            set
            {
                OnPropertyChanged(nameof(name));
                name = value;
            }
        }
        public int Level 
        { 
            get 
            {
                return level;
            } 
            set 
            {
                OnPropertyChanged(nameof(level));
                level = value; 
            }
        }
        public int CurrentHP
        {
            get
            {
                return currentHP;
            }
            set
            {
                OnPropertyChanged(nameof(currentHP));
                currentHP = value;
            }
        }
        public int MaxHP
        {
            get
            {
                return maxHP;
            }
            set
            {
                OnPropertyChanged(nameof(maxHP));
                maxHP = value;
            }
        }
        public int CurrentMP
        {
            get
            {
                return currentMP;
            }
            set
            {
                OnPropertyChanged(nameof(currentMP));
                currentMP = value;
            }
        }
        public int MaxMP
        {
            get
            {
                return maxMP;
            }
            set
            {
                OnPropertyChanged(nameof(maxMP));
                maxMP = value;
            }
        }
        public int Strength
        {
            get
            {
                return strength;
            }
            set
            {
                OnPropertyChanged(nameof(strength));
                strength = value;
            }
        } // Increase phys. damage
        public int Agility
        {
            get
            {
                return agility;
            }
            set
            {
                OnPropertyChanged(nameof(agility));
                agility = value;
            }
        } // Increase evasion
        public int Vitality
        {
            get
            {
                return vitality;
            }
            set
            {
                OnPropertyChanged(nameof(vitality));
                vitality = value;
            }
        } // Increase MaxHP
        public int Intelligence
        {
            get
            {
                return intelligence;
            }
            set
            {
                OnPropertyChanged(nameof(intelligence));
                intelligence = value;
            }
        } // Increase magic damage
        public int Mind
        {
            get
            {
                return mind;
            }
            set
            {
                OnPropertyChanged(nameof(mind));
                mind = value;
            }
        } // Increase MaxMP
        public int Luck
        {
            get
            {
                return luck;
            }
            set
            {
                OnPropertyChanged(nameof(luck));
                luck = value;
            }
        } // Increase crit chance

        public int Damage { get; set; }
        public int Defence { get; set; }
        public float Evasion { get; set; }

        public Creatures(string name, int level, int strength, int agility, int vitality, int intelligence, int mind, int luck)
        {
            Name = name;
            Level = level;

            Strength = strength;
            Agility = agility;
            Vitality = vitality;
            Intelligence = intelligence;
            Mind = mind;
            Luck = luck;

            RestoreHPMP();
        }

        // Constructor for Race class
        public Creatures(string name, int strength, int agility, int vitality, int intelligence, int mind, int luck)
        {
            Name = name;

            Strength = strength;
            Agility = agility;
            Vitality = vitality;
            Intelligence = intelligence;
            Mind = mind;
            Luck = luck;
        }

        public void RestoreHPMP()
        {
            MaxHP = Vitality * 10;
            CurrentHP = MaxHP;

            MaxMP = Mind * 10;
            CurrentMP = MaxMP;
        }
    }
}
