﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.SpecialAttack;

namespace Game.LivingCreatures
{
    public class Creature : BaseNotificationClass
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
        private Magic currentMagic;
        private WeaponSkills currentSkill;

        public string Name 
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged(nameof(name));
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
                level = value;
                OnPropertyChanged(nameof(level));
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
                currentHP = value;
                if (currentHP > maxHP)
                {
                    currentHP = maxHP;
                }
                OnPropertyChanged(nameof(currentHP));
            }
        }
        public int MaxHP
        {
            get
            {
                return maxHP;
            }
            set { }
        }
        public int CurrentMP
        {
            get
            {
                return currentMP;
            }
            set
            {
                currentMP = value;
                if (currentMP > maxMP)
                {
                    currentMP = maxMP;
                }
                OnPropertyChanged(nameof(currentMP));
            }
        }
        public int MaxMP
        {
            get
            {
                return maxMP;
            }
            set { }
        }
        public int Strength
        {
            get
            {
                return strength;
            }
            set
            {
                strength = value;
                OnPropertyChanged(nameof(strength));
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
                agility = value;
                OnPropertyChanged(nameof(agility));
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
                vitality = value;
                OnPropertyChanged(nameof(vitality));

                maxHP = vitality * 10;
                OnPropertyChanged(nameof(maxHP));
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
                intelligence = value;
                OnPropertyChanged(nameof(intelligence));
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
                mind = value;
                OnPropertyChanged(nameof(mind));

                maxMP = mind * 10;
                OnPropertyChanged(nameof(maxMP));
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
                luck = value;
                OnPropertyChanged(nameof(luck));
            }
        } // Increase crit chance

        public float Damage { get; set; }
        public float Defence { get; set; }
        public float Evasion { get; set; }
        public bool IsCriticalHit { get; set; }

        public Magic CurrentSpell
        {
            get
            {
                return currentMagic;
            }
            set
            {
                currentMagic = value;
                OnPropertyChanged(nameof(currentMagic));
            }
        }

        public WeaponSkills CurrentSkill
        {
            get
            {
                return currentSkill;
            }
            set
            {
                currentSkill = value;
                OnPropertyChanged(nameof(currentSkill));
            }
        }

        public ObservableCollection<Magic> SpellBook { get; set; }
        public ObservableCollection<WeaponSkills> SkillBook { get; set; }
        public ObservableCollection<StatusEffect> Effects { get; set; }

        public Creature(string name, int level, int strength, int agility, int vitality, int intelligence, int mind, int luck)
        {
            Name = name;
            Level = level;

            Strength = strength;
            Agility = agility;
            Vitality = vitality;
            Intelligence = intelligence;
            Mind = mind;
            Luck = luck;

            SpellBook = new ObservableCollection<Magic>();
            SkillBook = new ObservableCollection<WeaponSkills>();
            Effects = new ObservableCollection<StatusEffect>();

            RestoreHPMP();
        }

        // Empty Constructor for Race class
        public Creature()
        {
        }

        public float AddDamageModificator()
        {
            if(CurrentSkill != null)
            {
                switch (CurrentSkill.Modificator)
                {
                    case SkillsAndMagic.Attribute.Strength:
                        return Strength * CurrentSkill.AttributeModificator;
                    case SkillsAndMagic.Attribute.Agility:
                        return Agility * CurrentSkill.AttributeModificator;
                    case SkillsAndMagic.Attribute.Vitality:
                        return Vitality * CurrentSkill.AttributeModificator;
                    case SkillsAndMagic.Attribute.Intelligence:
                        return Intelligence * CurrentSkill.AttributeModificator;
                    case SkillsAndMagic.Attribute.Mind:
                        return Mind * CurrentSkill.AttributeModificator;
                    case SkillsAndMagic.Attribute.Luck:
                        return Luck * CurrentSkill.AttributeModificator;
                }
            }
            else
            {
                switch (CurrentSpell.Modificator)
                {
                    case SkillsAndMagic.Attribute.Strength:
                        return Strength * CurrentSpell.AttributeModificator;
                    case SkillsAndMagic.Attribute.Agility:
                        return Agility * CurrentSpell.AttributeModificator;
                    case SkillsAndMagic.Attribute.Vitality:
                        return Vitality * CurrentSpell.AttributeModificator;
                    case SkillsAndMagic.Attribute.Intelligence:
                        return Intelligence * CurrentSpell.AttributeModificator;
                    case SkillsAndMagic.Attribute.Mind:
                        return Mind * CurrentSpell.AttributeModificator;
                    case SkillsAndMagic.Attribute.Luck:
                        return Luck * CurrentSpell.AttributeModificator;
                }
            }  
            return 1;
        }

        public virtual float PhysicalDamageCalculation()
        {
            return Dice.GetRandomModificator() * Strength;
        }

        public virtual float MagicDamageCalculation()
        {
            return Dice.GetRandomModificator() * (CurrentSpell.BaseDamage + AddDamageModificator());
        }

        public virtual float SkillDamageCalculation()
        {
            if(CurrentSkill.NumberOfHits > 1)
            {
                float damage = 0;
                int numberOfHits = Dice.rng.Next(CurrentSkill.NumberOfHits) + 1;
                for(int i = 0; i < numberOfHits; i++)
                {
                    damage += (CurrentSkill.BaseDamage + AddDamageModificator());
                }
                return Dice.GetRandomModificator() * damage;
            }
            return Dice.GetRandomModificator() * (CurrentSkill.BaseDamage + AddDamageModificator());
        }

        public bool CalculateCriticalHitChance()
        {
            if(Dice.rng.Next(100) + 1 <= Dice.rng.Next(Luck))
            {
                return IsCriticalHit = true;
            }

            return IsCriticalHit = false;
        }

        public void ApplyStatusEffect(string name, int id, string description, float affectHP, float affectMP, int duration, Enum type)
        {
            foreach(var effect in Effects)
            {
                if(effect.ID.Equals(id))
                {
                    effect.Duration = duration; // Update time of debuff
                    return;
                }
            }
            Effects.Add(new StatusEffect(name, id, description, affectHP, affectMP, duration, type));
        }
        public void StatusEffectsDamageCalculation()
        {
            if (Effects.Count == 0) return;
            foreach (var effect in Effects)
            {
                if (effect.Type.Equals(StatusEffect.StatusType.HealOverTime))
                {
                    CurrentHP += (int)((MaxHP * effect.AffectHP) / 100);
                }
                if (effect.Type.Equals(StatusEffect.StatusType.DamageOverTime))
                {
                    CurrentHP -= (int)((MaxHP * effect.AffectHP) / 100);
                }
                effect.Duration--;
            }
            RemoveEffects();
        }

        // For now, using this to remove expired status effects one by one
        private void RemoveEffects()
        {
            for(int i = 0; i < Effects.Count; i++)
            {
                if (Effects[i].Duration == 0)
                {
                    Effects.Remove(Effects[i]);
                    i = 0;
                }
            }
        }

        public void RestoreHPMP()
        {
            CurrentHP = MaxHP;
            CurrentMP = MaxMP;
        }
    }
}
