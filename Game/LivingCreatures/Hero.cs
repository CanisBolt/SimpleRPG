using System.Collections.ObjectModel;

namespace Game.LivingCreatures
{
    public class Hero : Creature
    {
        private int currentEXP;
        private int expToLevel;
        private int gold;
        private int skillPoints;
        private Items.GameItems currentWeapon;
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
        public Gender HeroGender { get; set; }
        public Race HeroRace { get; set; }

        public Items.GameItems CurrentWeapon
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
            CurrentEXP = 0;
            EXPToLevel = Level * (100 * (Level * 2));
            Gold = 0;
            QuestJournal = new ObservableCollection<GameLocations.Quest>();
            RecipeList = new ObservableCollection<Items.AlchemyRecipe>();
        }

        public void LevelUP()
        {
            if (CurrentEXP >= EXPToLevel)
            {
                CurrentEXP -= EXPToLevel;

                Level++;
                SkillPoints += 5;

                EXPToLevel = Level * (100 * (Level * 2));
            }
        }

        public enum Gender
        {
            Male,
            Female,
            Other
        }

        public override float PhysicalDamageCalculation()
        {
            return Dice.GetRandomModificator() * (Strength + (Dice.rng.Next(CurrentWeapon.MinimumDamage, CurrentWeapon.MaximumDamage) + 1));
        }

        public override float SkillDamageCalculation()
        {
            float damage = 0;
            if (CurrentSkill == null) return 0;

            if (CurrentSkill.NumberOfHits > 1)
            {
                int numberOfHits = Dice.rng.Next(CurrentSkill.NumberOfHits) + 1;
                for (int i = 0; i < numberOfHits; i++)
                {
                    damage += (CurrentSkill.BaseDamage + AddDamageModificator());
                }
                return Dice.GetRandomModificator() * damage;
            }
            else damage = Dice.GetRandomModificator() * (CurrentSkill.BaseDamage + AddDamageModificator());

            if (CurrentWeapon.TypeOfWeapon.Equals(Items.GameItems.WeaponType.Staff) && CurrentSkill.Type.Equals(SpecialAttack.Skills.SpecialAttackType.Magic))
            {
                damage *= 1.2f; // Increase magic damage by 20% if Hero is using a Staff Weapon
            }

            return damage;
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
    }
}
