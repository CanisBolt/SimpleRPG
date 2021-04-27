using System;
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
                OnPropertyChanged(nameof(currentHP));
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
                maxHP = value;
                OnPropertyChanged(nameof(maxHP));
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
                currentMP = value;
                OnPropertyChanged(nameof(currentMP));
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
                maxMP = value;
                OnPropertyChanged(nameof(maxMP));
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

            RestoreHPMP();
        }

        // Empty Constructor for Race class
        public Creature()
        {
        }


        public virtual float PhysicalDamageCalculation()
        {
            return Dice.GetRandomModificator() * Strength;
        }

        public virtual float MagicDamageCalculation()
        {
            return Dice.GetRandomModificator() * (CurrentSpell.BaseDamage + (Intelligence * CurrentSpell.IntelligenceModificator));
        }

        public virtual float SkillDamageCalculation()
        {
            if(CurrentSkill.NumberOfHits > 1)
            {
                float damage = 0;
                int numberOfHits = Dice.rng.Next(CurrentSkill.NumberOfHits) + 1;
                for(int i = 0; i < numberOfHits; i++)
                {
                    damage += (CurrentSkill.BaseDamage + (Strength * CurrentSkill.StrengthModificator));
                }
                return Dice.GetRandomModificator() * damage;
            }
            return Dice.GetRandomModificator() * (CurrentSkill.BaseDamage + (Strength * CurrentSkill.StrengthModificator));
        }

        public bool CalculateCriticalHitChance()
        {
            if(Dice.rng.Next(100) + 1 <= Dice.rng.Next(Luck))
            {
                return IsCriticalHit = true;
            }

            return IsCriticalHit = false;
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
