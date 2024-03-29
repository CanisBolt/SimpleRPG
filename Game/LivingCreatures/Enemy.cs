﻿using System.Collections.Generic;

namespace Game.LivingCreatures
{
    public class Enemy : Creature
    {
        public int ID { get; set; }
        public int RewardEXP { get; set; }
        public int RewardGold { get; set; }
        public int EncounterChance { get; set; }
        public bool IsAgressive { get; set; }
        public bool HasAdvantage { get; set; }
        public Enemy(string name, int level, int strength, int agility, int vitality, int intelligence, int mind, int luck, int rewardEXP, int rewardGold, int encounterChance, int id, bool isAgressive, float defence) : base(name, level, strength, agility, vitality, intelligence, mind, luck)
        {
            ID = id;
            RewardEXP = rewardEXP;
            RewardGold = rewardGold;
            EncounterChance = encounterChance;
            IsAgressive = isAgressive;
            Defence = defence;

            HasAdvantage = false;
        }

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

            if (possibleSkill.Count == 0) return;

            // Choose a skill from this list
            int randomSkill = Dice.rng.Next(possibleSkill.Count);
            CurrentSkill = possibleSkill[randomSkill];
        }
    }
}
