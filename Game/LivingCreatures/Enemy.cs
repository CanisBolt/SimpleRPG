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
        public int RewardGold { get; set; }
        public int EncounterChance { get; set; }
        public bool IsAgressive { get; set; }
        public Enemy(string name, int level, int strength, int agility, int vitality, int intelligence, int mind, int luck, int rewardEXP, int rewardGold, int encounterChance, int id, bool isAgressive) : base(name, level, strength, agility, vitality, intelligence, mind, luck)
        {
            ID = id;
            RewardEXP = rewardEXP;
            RewardGold = rewardGold;
            EncounterChance = encounterChance;
            IsAgressive = isAgressive;
        }

        // At least 1 skill should be able to cast
        /*
        public bool CheckMPForSkill()
        {
            foreach (var skill in SkillBook)
            {
                if (skill.ManaCost <= CurrentMP)
                {
                    return true;
                }
            }
            return false;
        }
        */

        public void ChooseRandomSkill()
        {
            List<SpecialAttack.Skills> possibleSkill = new List<SpecialAttack.Skills>();
            // Add possible skills for separate list
            foreach (var skill in SkillBook)
            {
                if (skill.ManaCost <= CurrentMP)
                {
                    possibleSkill.Add(skill);
                }
            }

            // Choose a skill from this list
            int randomSkill = Dice.rng.Next(possibleSkill.Count);
            CurrentSkill = possibleSkill[randomSkill];
        }
    }
}
