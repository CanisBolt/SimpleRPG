using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.LivingCreatures;
using Game.Items;
using Game.GameLocations;

namespace Game
{
    public class GameSession : BaseNotificationClass
    {
        private Location currentLocation; 
        private Enemy currentEnemy;

        public World CurrentWorld { get; set; }
        public Hero Hero { get; set; }
        public Location CurrentLocation
        {
            get { return currentLocation; }
            set
            {
                currentLocation = value;
                OnPropertyChanged(nameof(CurrentLocation));

                GetEnemyAtRegion();
            }
        }
        public Location Checkpoint { get; set; }

        public Enemy CurrentEnemy
        {
            get { return currentEnemy; }
            set
            {
                currentEnemy = value;
                OnPropertyChanged(nameof(HasEnemy));
                OnPropertyChanged(nameof(currentEnemy));
            }
        }

        public GameSession()
        {
            Hero = new Hero("", 1, 5, 5, 5, 5, 5, 5);

            WorldFactory factory = new WorldFactory();
            CurrentWorld = factory.CreateWorld();
            CurrentLocation = CurrentWorld.LocationAt(0, 0); // Starting position (home)
            Checkpoint = CurrentWorld.LocationAt(0, 0); // Starting checkpoint

            Hero.Inventory.Add(ItemsFactory.CreateGameItem(World.WeaponIDWoodStaff));
            Hero.Inventory.Add(ItemsFactory.CreateGameItem(World.WeaponIDWoodSword));

            Hero.Inventory.Add(ItemsFactory.CreateGameItem(World.ArmorIDNoHeadArmor));
            Hero.Inventory.Add(ItemsFactory.CreateGameItem(World.ArmorIDNoBodyArmor));
            Hero.Inventory.Add(ItemsFactory.CreateGameItem(World.ArmorIDNoLegsArmor));
            Hero.Inventory.Add(ItemsFactory.CreateGameItem(World.ArmorIDNoFeetArmor));
            Hero.Inventory.Add(ItemsFactory.CreateGameItem(World.ArmorIDSilkHat));
            Hero.Inventory.Add(ItemsFactory.CreateGameItem(World.ArmorIDSilkRobe));
            Hero.Inventory.Add(ItemsFactory.CreateGameItem(World.ArmorIDSilkPants));
            Hero.Inventory.Add(ItemsFactory.CreateGameItem(World.ArmorIDSilkSandals));

            Hero.Inventory.Add(ItemsFactory.CreateGameItem(World.ItemIDSmallHealingPotion));
            Hero.Inventory.Add(ItemsFactory.CreateGameItem(World.ItemIDSmallHealingPotion));
            Hero.Inventory.Add(ItemsFactory.CreateGameItem(World.ItemIDSmallManaPotion));
            Hero.Inventory.Add(ItemsFactory.CreateGameItem(World.ItemIDSmallManaPotion));

            Hero.SkillBook.Add(World.SkillByID(World.MagicIDFireball));
            Hero.SkillBook.Add(World.SkillByID(World.MagicIDSmallHeal));

            Hero.SkillBook.Add(World.SkillByID(World.SwordSKillIDFastStrike));

            // Equip starting gear
            Hero.CurrentWeapon = Hero.Inventory[World.WeaponIDWoodStaff];
            Hero.CurrentHeadArmor = Hero.Inventory[World.ArmorIDNoHeadArmor];
            Hero.CurrentBodyArmor = Hero.Inventory[World.ArmorIDNoBodyArmor];
            Hero.CurrentLegsArmor = Hero.Inventory[World.ArmorIDNoLegsArmor];
            Hero.CurrentFeetArmor = Hero.Inventory[World.ArmorIDNoFeetArmor];

            GetEnemyAtRegion();
        }

        public void MoveNorth()
        {
            if(CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1) != null)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1);
                if (CurrentLocation.IsCheckpoint)
                {
                    Checkpoint = CurrentLocation;
                }
            }
        }

        public void MoveEast()
        {
            if (CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate) != null)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate);
                if (CurrentLocation.IsCheckpoint)
                {
                    Checkpoint = CurrentLocation;
                }
            } 
        }

        public void MoveSouth()
        {
            if (CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1) != null)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1);
                if (CurrentLocation.IsCheckpoint)
                {
                    Checkpoint = CurrentLocation;
                }
            }
        }

        public void MoveWest()
        {
            if (CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate) != null)
            {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate);
                if (CurrentLocation.IsCheckpoint)
                {
                    Checkpoint = CurrentLocation;
                }
            }
        }

        public bool HasEnemy => CurrentEnemy != null;

        private void GetEnemyAtRegion()
        {
            CurrentEnemy = CurrentLocation.Region.GetEnemy();
        }
    }
}
