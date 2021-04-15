using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.LivingCreatures
{
    public class Enemy : Creature
    {
        public int ID { get; set; }
        public int RewardEXP { get; set; }
        public int RewardMoney { get; set; }
        public int EncounterChance { get; set; }
        public Enemy(string name, int level, int strength, int agility, int vitality, int intelligence, int mind, int luck, int rewardEXP, int rewardMoney, int encounterChance, int id) : base(name, level, strength, agility, vitality, intelligence, mind, luck)
        {
            ID = id;
            RewardEXP = rewardEXP;
            RewardMoney = rewardMoney;
            EncounterChance = encounterChance;
        }
    }
}
