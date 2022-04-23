using System;
using System.Collections.ObjectModel;

namespace Game.LivingCreatures
{
    public class Hero : Creature
    {
        private int currentEXP;
        private int expToLevel;
        private int gold;
        private int skillPoints;
        private float swordSkill;
        private float daggerSkill;
        private float staffSkill;
        private Items.GameItems currentHeadArmor;
        private Items.GameItems currentBodyArmor;
        private Items.GameItems currentLegsArmor;
        private Items.GameItems currentFeetArmor;

        public int CurrentEXP
        {
            get
            {
                return currentEXP;
            }
            set
            {
                currentEXP = value;
                OnPropertyChanged(nameof(currentEXP));
            }
        }
        public int EXPToLevel
        {
            get
            {
                return expToLevel;
            }
            set
            {
                expToLevel = value;
                OnPropertyChanged(nameof(expToLevel));
            }
        }
        public int Gold
        {
            get
            {
                return gold;
            }
            set
            {
                gold = value;
                OnPropertyChanged(nameof(gold));
            }
        }
        public int SkillPoints
        {
            get
            {
                return skillPoints;
            }
            set
            {
                skillPoints = value;
                OnPropertyChanged(nameof(skillPoints));
            }
        }
        public float SwordSkill
        {
            get
            {
                return swordSkill;
            }
            set
            {
                swordSkill = value;
                OnPropertyChanged(nameof(swordSkill));
            }
        }
        public float DaggerSkill
        {
            get
            {
                return daggerSkill;
            }
            set
            {
                daggerSkill = value;
                OnPropertyChanged(nameof(daggerSkill));
            }
        }
        public float StaffSkill
        {
            get
            {
                return staffSkill;
            }
            set
            {
                staffSkill = value;
                OnPropertyChanged(nameof(staffSkill));
            }
        }
        public Gender HeroGender { get; set; }
        public Race HeroRace { get; set; }


        public Items.GameItems CurrentHeadArmor
        {
            get
            {
                return currentHeadArmor;
            }
            set
            {
                currentHeadArmor = value;
                OnPropertyChanged(nameof(currentHeadArmor));
            }
        }

        public Items.GameItems CurrentBodyArmor
        {
            get
            {
                return currentBodyArmor;
            }
            set
            {
                currentBodyArmor = value;
                OnPropertyChanged(nameof(currentBodyArmor));
            }
        }

        public Items.GameItems CurrentLegsArmor
        {
            get
            {
                return currentLegsArmor;
            }
            set
            {
                currentLegsArmor = value;
                OnPropertyChanged(nameof(currentLegsArmor));
            }
        }

        public Items.GameItems CurrentFeetArmor
        {
            get
            {
                return currentFeetArmor;
            }
            set
            {
                currentFeetArmor = value;
                OnPropertyChanged(nameof(currentFeetArmor));
            }
        }

        public ObservableCollection<GameLocations.Quest> QuestJournal { get; set; }

        public ObservableCollection<Items.AlchemyRecipe> RecipeList { get; set; }
        public GameLocations.Garden PlayersGarden { get; set; }

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

        public void RemoveItemToInventory(Items.GameItems item, int quantity = 1)
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                if (Inventory[i].ID.Equals(item.ID))
                {
                    if (Inventory[i].Quantity > quantity)
                    {
                        if (Inventory[i].Quantity == quantity)
                        {
                            Inventory.Remove(Inventory[i]);
                        }
                        Inventory[i].Quantity -= quantity;
                    }
                    else
                    {
                        Inventory.Remove(Inventory[i]);
                    }
                    return;
                }
            }
        }

        public Hero(string name, int level, int strength, int agility, int vitality, int intelligence, int mind, int luck) : base(name, level, strength, agility, vitality, intelligence, mind, luck)
        {
            swordSkill = 1f;
            daggerSkill = 1f;
            staffSkill = 1f;
            CurrentEXP = 0;
            SetNextLevelEXP();
            Gold = 0;
            QuestJournal = new ObservableCollection<GameLocations.Quest>();
            RecipeList = new ObservableCollection<Items.AlchemyRecipe>();
            PlayersGarden = new GameLocations.Garden();
        }

        private void SetNextLevelEXP() => EXPToLevel = Level * (25 * (Level * 2));

        public void LevelUP()
        {
            if (CurrentEXP >= EXPToLevel)
            {
                CurrentEXP -= EXPToLevel;

                Level++; 
                RaiseMessage($"{Name} now Level {Level}!");
                SkillPoints += 5;
                NewSkills();

                SetNextLevelEXP();
            }
        }

        private void NewSkills()
        {
            switch(Level)
            {
                case 2:
                    SkillBook.Add(World.SkillByID(World.MagicIDIceArrow));
                    RaiseMessage($"Learned new Spell: {World.SkillByID(World.MagicIDIceArrow).Name}!");
                    break;
                case 3:
                    SkillBook.Add(World.SkillByID(World.SwordSKillIDHeavyStrike));
                    break;
            }
        }

        private float GetWeaponType()
        {
            switch (CurrentWeapon.TypeOfWeapon)
            {
                case Items.GameItems.WeaponType.Sword:
                    return SwordSkill;
                case Items.GameItems.WeaponType.Dagger:
                    return DaggerSkill;
                case Items.GameItems.WeaponType.Staff:
                    return StaffSkill;
            }
            return 1;
        }

        public override float PhysicalDamageCalculation()
        {
            Damage = Dice.GetRandomModificator() * (Strength + (Dice.RollDice(CurrentWeapon.NumberOfDices, CurrentWeapon.NumberOfSides) * GetWeaponType()) * CalculateCriticalHitChance());
            return Damage;
        }

        public void CalculateDefence()
        {
            Defence = currentHeadArmor.Defence + currentBodyArmor.Defence + currentLegsArmor.Defence + currentFeetArmor.Defence;
        }

        public void HealingAfterDeath()
        {
            CurrentHP = MaxHP / 2;
            CurrentMP = MaxMP / 2;
        }

        public enum Gender
        {
            Male,
            Female,
            Other
        }
    }
}
