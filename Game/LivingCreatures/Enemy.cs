﻿using System;
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
        public bool IsAgressive { get; set; }
        public Enemy(string name, int level, int strength, int agility, int vitality, int intelligence, int mind, int luck, int rewardEXP, int rewardMoney, int encounterChance, int id, bool isAgressive) : base(name, level, strength, agility, vitality, intelligence, mind, luck)
        {
            ID = id;
            RewardEXP = rewardEXP;
            RewardMoney = rewardMoney;
            EncounterChance = encounterChance;
            IsAgressive = isAgressive;
        }

        // At least 1 spell should be able to cast
        public bool CheckMPForSpells()
        {
            foreach(var spell in SpellBook)
            {
                if(spell.ManaCost <= CurrentMP)
                {
                    return true;
                }
            }
            return false;
        }

        public void ChooseRandomSpell()
        {
            List<SpecialAttack.Magic> possibleSpells = new List<SpecialAttack.Magic>();
            // Add possible spells for separate list
            foreach(var spell in SpellBook)
            {
                if(spell.ManaCost <= CurrentMP)
                {
                    possibleSpells.Add(spell);
                }
            }

            // Choose a spell from this list
            int randomSpell = Dice.rng.Next(possibleSpells.Count);
            CurrentSpell = possibleSpells[randomSpell];
        }

        // At least 1 skill should be able to cast
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

        public void ChooseRandomSkill()
        {
            List<SpecialAttack.WeaponSkills> possibleSkill = new List<SpecialAttack.WeaponSkills>();
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
