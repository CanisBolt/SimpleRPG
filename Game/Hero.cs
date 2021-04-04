using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Hero : Creatures
    {
        public int CurrentEXP { get; set; }
        public int EXPToLevel { get; set; }
        public int Money { get; set; }
        public int SkillPoints { get; set; }
        public Gender HeroGender { get; set; }
        public Race HeroRace { get; set; }

        public Hero(string name, int level, int strength, int agility, int vitality, int intelligence, int mind, int luck) : base(name, level, strength, agility, vitality, intelligence, mind, luck)
        {
            CurrentEXP = 0;
            EXPToLevel = Level * (100 * (Level * 2));
            Money = 0;
        }

        public enum Gender
        {
            Male,
            Female,
            Other
        }
    }
}
