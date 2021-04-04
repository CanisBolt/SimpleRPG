using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Creatures
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int CurrentHP { get; set; }
        public int MaxHP { get; set; }
        public int CurrentMP { get; set; }
        public int MaxMP { get; set; }
        public int Strength { get; set; } // Increase phys. damage
        public int Agility { get; set; } // Increase evasion
        public int Vitality { get; set; } // Increase MaxHP
        public int Intelligence { get; set; } // Increase magic damage
        public int Mind { get; set; } // Increase MaxMP
        public int Luck { get; set; } // Increase crit chance

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
