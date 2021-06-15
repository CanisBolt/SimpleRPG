using Game.GameLocations;
using Game.Items;
using Game.LivingCreatures;
using System;

namespace Game
{
    public class GameSession : BaseNotificationClass
    {
        private Location currentLocation;
        private Enemy currentEnemy;

        public event EventHandler<GameMessageEventArgs> OnMessageRaised;

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
                if(currentEnemy != null) RaiseMessage($"You see a {currentEnemy.Name} here!");
            }
        }

        public GameSession()
        {
            Hero = new Hero("", 1, 5, 5, 5, 5, 5, 5);

            WorldFactory factory = new WorldFactory();
            CurrentWorld = factory.CreateWorld();
            CurrentLocation = CurrentWorld.LocationAt(0, 0); // Starting position (home)
            Checkpoint = CurrentWorld.LocationAt(0, 0); // Starting checkpoint

            Hero.AddItemToInventory(ItemsFactory.CreateGameItem(World.WeaponIDWoodStaff));
            Hero.AddItemToInventory(ItemsFactory.CreateGameItem(World.WeaponIDWoodSword));

            Hero.AddItemToInventory(ItemsFactory.CreateGameItem(World.ArmorIDNoHeadArmor));
            Hero.AddItemToInventory(ItemsFactory.CreateGameItem(World.ArmorIDNoBodyArmor));
            Hero.AddItemToInventory(ItemsFactory.CreateGameItem(World.ArmorIDNoLegsArmor));
            Hero.AddItemToInventory(ItemsFactory.CreateGameItem(World.ArmorIDNoFeetArmor));
            Hero.AddItemToInventory(ItemsFactory.CreateGameItem(World.ArmorIDSilkHat));
            Hero.AddItemToInventory(ItemsFactory.CreateGameItem(World.ArmorIDSilkRobe));
            Hero.AddItemToInventory(ItemsFactory.CreateGameItem(World.ArmorIDSilkPants));
            Hero.AddItemToInventory(ItemsFactory.CreateGameItem(World.ArmorIDSilkSandals));

            Hero.AddItemToInventory(ItemsFactory.CreateGameItem(World.ItemIDSmallHealingPotion));
            Hero.AddItemToInventory(ItemsFactory.CreateGameItem(World.ItemIDSmallHealingPotion));
            Hero.AddItemToInventory(ItemsFactory.CreateGameItem(World.ItemIDSmallManaPotion));
            Hero.AddItemToInventory(ItemsFactory.CreateGameItem(World.ItemIDSmallManaPotion));

            Hero.AddItemToInventory(ItemsFactory.CreateGameItem(World.MaterialIDHealingGrass));
            Hero.AddItemToInventory(ItemsFactory.CreateGameItem(World.MaterialIDHealingGrass));
            Hero.AddItemToInventory(ItemsFactory.CreateGameItem(World.MaterialIDHealingGrass));
            Hero.AddItemToInventory(ItemsFactory.CreateGameItem(World.MaterialIDHealingGrass));

            Hero.AddItemToInventory(ItemsFactory.CreateGameItem(World.EnemyLootIDRatTail));
            Hero.AddItemToInventory(ItemsFactory.CreateGameItem(World.EnemyLootIDRatTail));
            Hero.AddItemToInventory(ItemsFactory.CreateGameItem(World.EnemyLootIDSnakeFang));

            Hero.PlayersGarden.AvailableSeeds.Add(ItemsFactory.CreateGameItem(World.SeedIDHealingGrass));

            Hero.RecipeList.Add(RecipeFactory.RecipeByID(World.AlchemyRecipeIDSmallHealingPotion));
            Hero.RecipeList.Add(RecipeFactory.RecipeByID(World.AlchemyRecipeIDSmallManaPotion));

            Hero.SkillBook.Add(World.SkillByID(World.MagicIDFireball));
            Hero.SkillBook.Add(World.SkillByID(World.MagicIDSmallHeal));

            Hero.SkillBook.Add(World.SkillByID(World.SwordSKillIDFastStrike));

            // Equip starting gear
            Hero.CurrentWeapon = Hero.Inventory[World.WeaponIDWoodStaff];
            Hero.CurrentHeadArmor = Hero.Inventory[World.ArmorIDNoHeadArmor];
            Hero.CurrentBodyArmor = Hero.Inventory[World.ArmorIDNoBodyArmor];
            Hero.CurrentLegsArmor = Hero.Inventory[World.ArmorIDNoLegsArmor];
            Hero.CurrentFeetArmor = Hero.Inventory[World.ArmorIDNoFeetArmor];

            // Bad way to remove equiped items from inventory, but works for now...
            Hero.Inventory.Remove(Hero.CurrentWeapon);
            Hero.Inventory.Remove(Hero.CurrentHeadArmor);
            Hero.Inventory.Remove(Hero.CurrentBodyArmor);
            Hero.Inventory.Remove(Hero.CurrentLegsArmor);
            Hero.Inventory.Remove(Hero.CurrentFeetArmor);


            for (int i = 0; i < Hero.PlayersGarden.Size; i++)
            {
                if (Hero.PlayersGarden.Slots[i] == null)
                {
                    Hero.PlayersGarden.Slots[i] = new GameItems("Empty Slot", -1, 0, 0, GameItems.TypeOfItem.Seed);
                }
            }

            GetEnemyAtRegion();
        }

        public void MoveNorth()
        {
            if (CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1) != null)
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
        private void RaiseMessage(string message)
        {
            OnMessageRaised?.Invoke(this, new GameMessageEventArgs(message));
        }
    }
}
