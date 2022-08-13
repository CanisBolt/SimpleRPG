using Game.Items;
using Game.SpecialAttack;
using System;
using System.Collections.ObjectModel;

namespace Game.LivingCreatures
{
    public abstract class Creature : BaseNotificationClass
    {
        public static event EventHandler<GameMessageEventArgs> OnMessageRaised;
        public static event EventHandler<GameMessageEventArgs> OnBattleMessageRaised;

        #region Properties
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
        protected float defence;
        private GameItems currentWeapon;
        private Skills currentSkill;
        #endregion

        #region Stats
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
        #endregion

        public float Damage { get; set; }
        public float Defence
        {
            get
            {
                return defence;
            }
            set
            {
                defence = value;
                OnPropertyChanged(nameof(defence));
            }
        }
        public float Evasion { get; set; }
        public bool IsCriticalHit { get; set; }

        public string Avatar { get; set; }

        public GameItems CurrentWeapon
        {
            get
            {
                return currentWeapon;
            }
            set
            {
                currentWeapon = value;
                OnPropertyChanged(nameof(currentWeapon));
            }
        }
        public Skills CurrentSkill
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

        public ObservableCollection<GameItems> Inventory { get; set; }
        public ObservableCollection<Skills> SkillBook { get; set; }
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

            Inventory = new ObservableCollection<GameItems>();
            SkillBook = new ObservableCollection<Skills>();
            Effects = new ObservableCollection<StatusEffect>();

            RestoreHPMP();
        }

        // Empty Constructor for Race class
        public Creature()
        {
        }

        public void AddItemToInventory(Items.GameItems item)
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                if (Inventory[i].ID.Equals(item.ID))
                {
                    Inventory[i].Quantity++;
                    return;
                }
            }
            Inventory.Add(item);
        }

        public float AddDamageModificator()
        {
            if (CurrentSkill != null)
            {
                switch (CurrentSkill.Modificator)
                {
                    case Skills.Attribute.Strength:
                        return Strength * CurrentSkill.AttributeModificator;
                    case Skills.Attribute.Agility:
                        return Agility * CurrentSkill.AttributeModificator;
                    case Skills.Attribute.Vitality:
                        return Vitality * CurrentSkill.AttributeModificator;
                    case Skills.Attribute.Intelligence:
                        return Intelligence * CurrentSkill.AttributeModificator;
                    case Skills.Attribute.Mind:
                        return Mind * CurrentSkill.AttributeModificator;
                    case Skills.Attribute.Luck:
                        return Luck * CurrentSkill.AttributeModificator;
                }
            }
            return 1;
        }

        public virtual float PhysicalDamageCalculation() => Dice.GetRandomModificator() * Strength * CalculateCriticalHitChance();

        public virtual void SkillDamageCalculation()
        {
            float damage = 0;
            if (CurrentSkill == null) return;

            if (CurrentSkill.NumberOfHits > 1)
            {
                int numberOfHits = Dice.rng.Next(CurrentSkill.NumberOfHits) + 1;
                for (int i = 0; i < numberOfHits; i++)
                {
                    damage += CurrentSkill.BaseDamage + AddDamageModificator();
                }
            }
            else damage = CurrentSkill.BaseDamage + AddDamageModificator();

            Damage = Dice.GetRandomModificator() * damage * CalculateCriticalHitChance();
            MagicDamageText();
        }

        protected void MagicDamageText()
        {
            if (CurrentSkill.AffectedTarger.Equals(Skills.Target.Self))
            {
                RaiseMessage($"{Name} casted {CurrentSkill.Name} and healed {(int)Damage} HP");
            }
            else
            {
                if (IsCriticalHit)
                {
                    RaiseBattleMessage($"{Name} used {CurrentSkill.Name} and dealed {(int)Damage} CRITICAL damage to enemy");
                }
                else RaiseBattleMessage($"{Name} used {CurrentSkill.Name} and dealed {(int)Damage} damage to enemy");
            }
        }

        public int CalculateCriticalHitChance()
        {
            if (Dice.Roll20() + (Luck / 5) >= 20)
            {
                IsCriticalHit = true;
                return 2;
            }

            IsCriticalHit = false;
            return 1;
        }

        public void ApplyStatusEffect(string name, int id, string description, float affectHP, float affectMP, int duration, Enum type)
        {
            foreach (var effect in Effects)
            {
                if (effect.ID.Equals(id))
                {
                    effect.Duration = duration; // Update time of debuff
                    return;
                }
            }
            Effects.Add(new StatusEffect(name, id, description, affectHP, affectMP, duration, type));
            RaiseMessage($"{Name} is {name}ing");
        }

        public void StatusEffectsDamageCalculation()
        {
            if (Effects.Count == 0) return;

            foreach (var effect in Effects)
            {
                if (effect.Type.Equals(StatusEffect.StatusType.HealOverTime))
                {
                    CurrentHP += (int)(MaxHP * effect.AffectHP / 100);
                    RaiseMessage($"{Name}: {effect.Name} restore {(int)(MaxHP * effect.AffectHP / 100)} HP");
                }
                if (effect.Type.Equals(StatusEffect.StatusType.DamageOverTime))
                {
                    CurrentHP -= (int)(MaxHP * effect.AffectHP / 100);
                    RaiseMessage($"{Name}: {effect.Name} damaged {(int)(MaxHP * effect.AffectHP / 100)} HP");
                }
                effect.Duration--;
            }
            RemoveEffects();
        }

        // Remove expired status effects one by one
        private void RemoveEffects()
        {
            for (int i = 0; i < Effects.Count; i++)
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
        public void DecreaseHP(int value) => CurrentHP -= value;
        public void DecreaseMP(int value) => CurrentMP -= value;
        public void RestoreHP(int value) => CurrentHP += value;
        public void RestoreMP(int value) => CurrentMP += value;
        protected void RaiseMessage(string message)
        {
            OnMessageRaised?.Invoke(this, new GameMessageEventArgs(message));
        }
        // Separate event to avoid texting both in game window and battle window when casting a spell. Not appling to healing
        protected void RaiseBattleMessage(string message)
        {
            OnBattleMessageRaised?.Invoke(this, new GameMessageEventArgs(message));
        }
    }
}
