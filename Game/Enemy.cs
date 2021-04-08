using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Enemy : Creatures
    {
        public int ID { get; set; }
        public int RewardEXP { get; set; }
        public int RewardMoney { get; set; }
        public int EncounterChance { get; set; }
        public Enemy(string name, int level, int strength, int agility, int vitality, int intelligence, int mind, int luck, int id, int rewardEXP, int rewardMoney, int encounterChance) : base(name, level, strength, agility, vitality, intelligence, mind, luck)
        {
            ID = id;
            RewardEXP = rewardEXP;
            RewardMoney = rewardMoney;
            EncounterChance = encounterChance;
        }
    }
}
