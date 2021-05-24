using Game;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SimpleRPG
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        GameSession gameSession;
        public GameWindow(GameSession _gameSession)
        {
            InitializeComponent();

            gameSession = _gameSession;
            gameSession.Hero.SkillPoints = 5;
            DataContext = gameSession;

            ChangeImages();
        }

        private void ChangeImages()
        {
            if (gameSession.CurrentLocation.ShopOnLocation != null)
            {
                btnEnterShop.Visibility = Visibility.Visible;
            }
            else btnEnterShop.Visibility = Visibility.Hidden;

            if (gameSession.CurrentLocation.NPCOnLocation != null)
            {
                tbNPC.Text = gameSession.CurrentLocation.NPCOnLocation.Name;
            }
            else tbNPC.Text = null;

            if (gameSession.Hero.SkillPoints > 0) imgCharacter.Source = new BitmapImage(new Uri(@"/Images/Icons/characterLevelUPIcon.png", UriKind.Relative));
            else imgCharacter.Source = new BitmapImage(new Uri(@"/Images/Icons/characterIcon.png", UriKind.Relative));
        }

        private void OpenCharacterScreen(object sender, MouseButtonEventArgs e)
        {
            CharacterWindow characterWindow = new CharacterWindow(gameSession);
            characterWindow.ShowDialog();
            ChangeImages();
        }

        private void btnNorth_Click(object sender, RoutedEventArgs e)
        {
            gameSession.MoveNorth();
            ChangeImages();
        }

        private void btnWest_Click(object sender, RoutedEventArgs e)
        {
            gameSession.MoveWest();
            ChangeImages();
        }

        private void btnSouth_Click(object sender, RoutedEventArgs e)
        {
            gameSession.MoveSouth();
            ChangeImages();
        }

        private void btnEast_Click(object sender, RoutedEventArgs e)
        {
            gameSession.MoveEast();
            ChangeImages();
        }

        private void OpenInventory(object sender, MouseButtonEventArgs e)
        {
            Inventory inventory = new Inventory(gameSession, false);
            inventory.Show();
        }

        private void AttackEnemy(object sender, MouseButtonEventArgs e)
        {
            if (gameSession.CurrentEnemy == null) return;
            BattleWindow battle = new BattleWindow(gameSession);
            battle.ShowDialog();

            if (battle.BattleStatus.Equals(BattleWindow.Status.Victory))
            {
                tbLog.Text += $"{gameSession.Hero.Name} kill {gameSession.CurrentEnemy.Name}." + Environment.NewLine;
                tbLog.Text += $"{gameSession.Hero.Name} got {gameSession.CurrentEnemy.RewardEXP} exp and {gameSession.CurrentEnemy.RewardGold} gold." + Environment.NewLine;
                gameSession.Hero.CurrentEXP += gameSession.CurrentEnemy.RewardEXP;
                gameSession.Hero.Gold += gameSession.CurrentEnemy.RewardGold;
                LootEnemy();

                gameSession.Hero.LevelUP(); // Check for LevelUP
                ChangeImages();
            }
            else if (battle.BattleStatus.Equals(BattleWindow.Status.Defeat))
            {
                tbLog.Text += $"{gameSession.CurrentEnemy.Name} kill {gameSession.Hero.Name}." + Environment.NewLine;
                gameSession.CurrentLocation = gameSession.Checkpoint;
                gameSession.Hero.HealingAfterDeath();
            }
            else
            {
                tbLog.Text += $"You successfully escaped from battle." + Environment.NewLine;
            }

            gameSession.CurrentEnemy = null;
        }

        private void LootEnemy()
        {
            int roll;
            foreach (var item in gameSession.CurrentEnemy.Inventory)
            {
                roll = Dice.rng.Next(0, 101);
                if (roll <= item.DropChance)
                {
                    tbLog.Text += $"{gameSession.Hero.Name} searched the enemie's body and found {item.Name}!" + Environment.NewLine;
                    gameSession.Hero.AddItemToInventory(item);
                }
            }
        }
        private void SpecialAttack(object sender, MouseButtonEventArgs e)
        {
            SpellBookWindow spellbook = new SpellBookWindow(gameSession, false);
            spellbook.ShowDialog();
            if (spellbook.IsSkillUsed && gameSession.Hero.CurrentHP != gameSession.Hero.MaxHP)
            {
                gameSession.Hero.CurrentMP -= gameSession.Hero.CurrentSkill.ManaCost;
                gameSession.Hero.Damage = gameSession.Hero.SkillDamageCalculation();
                gameSession.Hero.CurrentHP += (int)gameSession.Hero.Damage;
                if (gameSession.Hero.CurrentHP > gameSession.Hero.MaxHP) gameSession.Hero.CurrentHP = gameSession.Hero.MaxHP;
                tbLog.Text += $"{gameSession.Hero.Name} casting {gameSession.Hero.CurrentSkill.Name} and heal for {(int)gameSession.Hero.Damage} HP." + Environment.NewLine;
            }
        }

        private void btnEnterShop_Click(object sender, RoutedEventArgs e)
        {
            ShopWindow window = new ShopWindow(gameSession);
            window.ShowDialog();
        }

        private void TalkToNPC(object sender, MouseButtonEventArgs e)
        {
            if (gameSession.CurrentLocation.NPCOnLocation != null)
            {
                for (int i = 0; i < gameSession.Hero.QuestJournal.Count; i++)
                {
                    if (gameSession.Hero.QuestJournal[i].ID.Equals(gameSession.CurrentLocation.NPCOnLocation.AvailableQuest.ID))
                    {
                        if (gameSession.Hero.QuestJournal[i].QuestStatus.Equals(Game.GameLocations.Quest.Status.InProgress))
                        {
                            if (CheckQuestForCompletition())
                            {
                                tbLog.Text += gameSession.CurrentLocation.NPCOnLocation.AvailableQuest.CompleteMessage + Environment.NewLine;
                                tbLog.Text += $"Quest: {gameSession.Hero.QuestJournal[i].Name} Complete! Reward: {gameSession.Hero.QuestJournal[i].RewardEXP} EXP and {gameSession.Hero.QuestJournal[i].RewardGold} gold!" + Environment.NewLine;

                                gameSession.Hero.CurrentEXP += gameSession.Hero.QuestJournal[i].RewardEXP;
                                gameSession.Hero.Gold += gameSession.Hero.QuestJournal[i].RewardGold;
                                gameSession.Hero.LevelUP();

                                gameSession.Hero.QuestJournal.Remove(gameSession.CurrentLocation.NPCOnLocation.AvailableQuest);
                                gameSession.CurrentLocation.NPCOnLocation.AvailableQuest.QuestStatus = Game.GameLocations.Quest.Status.Completed;
                            }
                            else
                            {
                                tbLog.Text += gameSession.CurrentLocation.NPCOnLocation.AvailableQuest.InProgressMessage + Environment.NewLine;
                            }
                        }
                        else
                        {
                            tbLog.Text += gameSession.CurrentLocation.NPCOnLocation.AvailableQuest.StartMessage + Environment.NewLine;
                            gameSession.Hero.QuestJournal.Add(gameSession.CurrentLocation.NPCOnLocation.AvailableQuest);
                            gameSession.Hero.QuestJournal[++i].QuestStatus = Game.GameLocations.Quest.Status.InProgress;
                        }
                    }
                }

                if (gameSession.Hero.QuestJournal.Count == 0)
                {
                    if (gameSession.CurrentLocation.NPCOnLocation.AvailableQuest.QuestStatus.Equals(Game.GameLocations.Quest.Status.Completed))
                    {
                        tbLog.Text += gameSession.CurrentLocation.NPCOnLocation.HelloMessage + Environment.NewLine;
                    }
                    else
                    {
                        tbLog.Text += gameSession.CurrentLocation.NPCOnLocation.AvailableQuest.StartMessage + Environment.NewLine;
                        gameSession.Hero.QuestJournal.Add(gameSession.CurrentLocation.NPCOnLocation.AvailableQuest);
                        gameSession.Hero.QuestJournal[0].QuestStatus = Game.GameLocations.Quest.Status.InProgress;
                    }
                }
            }
        }

        private bool CheckQuestForCompletition()
        {
            for (int i = 0; i < gameSession.Hero.Inventory.Count; i++)
            {
                if (gameSession.Hero.Inventory[i].ID.Equals(gameSession.CurrentLocation.NPCOnLocation.AvailableQuest.RequiredItems.ID))
                {
                    if (gameSession.Hero.Inventory[i].Quantity >= gameSession.CurrentLocation.NPCOnLocation.AvailableQuest.RequiredCount)
                    {
                        gameSession.Hero.RemoveItemToInventory(gameSession.Hero.Inventory[i], gameSession.CurrentLocation.NPCOnLocation.AvailableQuest.RequiredCount);
                        return true;
                    }
                    else
                    {
                        tbLog.Text += gameSession.CurrentLocation.NPCOnLocation.AvailableQuest.InProgressMessage;
                    }
                }
            }
            return false;
        }
    }
}
