using Game;
using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace SimpleRPG
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        GameSession gameSession;
        DispatcherTimer aggroTimer;
        public GameWindow(GameSession _gameSession)
        {
            InitializeComponent();

            gameSession = _gameSession;
            Game.LivingCreatures.Creature.OnMessageRaised += OnGameMessageRaised;
            gameSession.Hero.SkillPoints = 5;
            DataContext = gameSession;

            UpdateLocationData();
        }

        // Timer starts as soon as player encounter aggressive enemy on location
        private void aggroTimer_Tick(object sender, EventArgs e)
        {
            gameSession.CurrentEnemy.HasAdvantage = true;
            BattleWindow battle = new BattleWindow(gameSession);
            battle.ShowDialog();

            BattleResult(battle);

            gameSession.CurrentEnemy = null;

            aggroTimer.Stop();
        }

        private void UpdateLocationData()
        {
            if(aggroTimer != null)
            {
                if (aggroTimer.IsEnabled) aggroTimer.Stop();
            }
            if (gameSession.CurrentEnemy != null)
            {
                if (gameSession.CurrentEnemy.IsAgressive)
                {
                    aggroTimer = new DispatcherTimer();
                    aggroTimer.Tick += new EventHandler(aggroTimer_Tick);
                    aggroTimer.Interval = new TimeSpan(0, 0, 3);
                    aggroTimer.Start();
                }
            }

            if (gameSession.CurrentLocation.NPCOnLocation != null) tbNPC.Text = gameSession.CurrentLocation.NPCOnLocation.Name;
            else tbNPC.Text = null;

            if (gameSession.CurrentLocation.ShopOnLocation != null) btnEnter.Visibility = Visibility.Visible;
            else btnEnter.Visibility = Visibility.Hidden;

            if (gameSession.Hero.SkillPoints > 0) imgCharacter.Source = new BitmapImage(new Uri(@"/Images/Icons/characterLevelUPIcon.png", UriKind.Relative));
            else imgCharacter.Source = new BitmapImage(new Uri(@"/Images/Icons/characterIcon.png", UriKind.Relative));
        }

        private void AttackEnemy(object sender, MouseButtonEventArgs e)
        {
            if (aggroTimer != null)
            {
                if (aggroTimer.IsEnabled) aggroTimer.Stop();
            }
            if (gameSession.CurrentEnemy == null) return;
            BattleWindow battle = new BattleWindow(gameSession);
            battle.ShowDialog();

            BattleResult(battle);

            gameSession.CurrentEnemy = null;
        }

        private void BattleResult(BattleWindow battle)
        {
            switch(battle.BattleStatus)
            {
                case BattleWindow.Status.Victory:
                    tbLog.Document.Blocks.Add(new Paragraph(new Run($"{gameSession.Hero.Name} kill {gameSession.CurrentEnemy.Name}.")));
                    tbLog.Document.Blocks.Add(new Paragraph(new Run($"{gameSession.Hero.Name} got {gameSession.CurrentEnemy.RewardEXP} exp and {gameSession.CurrentEnemy.RewardGold} gold.")));
                    gameSession.Hero.CurrentEXP += gameSession.CurrentEnemy.RewardEXP;
                    gameSession.Hero.Gold += gameSession.CurrentEnemy.RewardGold;
                    LootEnemy();

                    gameSession.Hero.LevelUP(); // Check for LevelUP
                    UpdateLocationData();
                    break;
                case BattleWindow.Status.Defeat:
                    tbLog.Document.Blocks.Add(new Paragraph(new Run($"{gameSession.CurrentEnemy.Name} kill {gameSession.Hero.Name}.")));
                    gameSession.CurrentLocation = gameSession.Checkpoint;
                    gameSession.Hero.HealingAfterDeath();
                    break;
                case BattleWindow.Status.Escape:
                    tbLog.Document.Blocks.Add(new Paragraph(new Run($"You successfully escaped from battle.")));
                    break;
                default:
                    tbLog.Document.Blocks.Add(new Paragraph(new Run($"You coward! You closed the battle!")));
                    break;
            }
        }

        private void LootEnemy()
        {
            int roll;
            foreach (Game.Items.GameItems item in gameSession.CurrentEnemy.Inventory)
            {
                roll = Dice.rng.Next(0, 101);
                if (roll <= item.DropChance)
                {
                    tbLog.Document.Blocks.Add(new Paragraph(new Run($"{gameSession.Hero.Name} searched the enemie's body and found {item.Name}!")));
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
                gameSession.Hero.SkillDamageCalculation();
                gameSession.Hero.CurrentHP += (int)gameSession.Hero.Damage;
                if (gameSession.Hero.CurrentHP > gameSession.Hero.MaxHP) gameSession.Hero.CurrentHP = gameSession.Hero.MaxHP;
                tbLog.Document.Blocks.Add(new Paragraph(new Run($"{gameSession.Hero.Name} casting {gameSession.Hero.CurrentSkill.Name} and heal for {(int)gameSession.Hero.Damage} HP.")));
            }
        }

        // TODO rewrite
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
                                tbLog.Document.Blocks.Add(new Paragraph(new Run(gameSession.CurrentLocation.NPCOnLocation.AvailableQuest.CompleteMessage)));
                                tbLog.Document.Blocks.Add(new Paragraph(new Run($"Quest: {gameSession.Hero.QuestJournal[i].Name} Complete! Reward: {gameSession.Hero.QuestJournal[i].RewardEXP} EXP and {gameSession.Hero.QuestJournal[i].RewardGold} gold!")));

                                gameSession.Hero.CurrentEXP += gameSession.Hero.QuestJournal[i].RewardEXP;
                                gameSession.Hero.Gold += gameSession.Hero.QuestJournal[i].RewardGold;
                                gameSession.Hero.LevelUP();

                                gameSession.Hero.QuestJournal.Remove(gameSession.CurrentLocation.NPCOnLocation.AvailableQuest);
                                gameSession.CurrentLocation.NPCOnLocation.AvailableQuest.QuestStatus = Game.GameLocations.Quest.Status.Completed;
                            }
                            else
                            {
                                tbLog.Document.Blocks.Add(new Paragraph(new Run(gameSession.CurrentLocation.NPCOnLocation.AvailableQuest.InProgressMessage)));
                            }
                        }
                        else
                        {
                            tbLog.Document.Blocks.Add(new Paragraph(new Run(gameSession.CurrentLocation.NPCOnLocation.AvailableQuest.StartMessage)));
                            gameSession.Hero.QuestJournal.Add(gameSession.CurrentLocation.NPCOnLocation.AvailableQuest);
                            gameSession.Hero.QuestJournal[++i].QuestStatus = Game.GameLocations.Quest.Status.InProgress;
                        }
                    }
                }

                if (gameSession.Hero.QuestJournal.Count == 0)
                {
                    if (gameSession.CurrentLocation.NPCOnLocation.AvailableQuest.QuestStatus.Equals(Game.GameLocations.Quest.Status.Completed))
                    {
                        tbLog.Document.Blocks.Add(new Paragraph(new Run(gameSession.CurrentLocation.NPCOnLocation.HelloMessage)));
                    }
                    else
                    {
                        tbLog.Document.Blocks.Add(new Paragraph(new Run(gameSession.CurrentLocation.NPCOnLocation.AvailableQuest.StartMessage)));
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
                        tbLog.Document.Blocks.Add(new Paragraph(new Run(gameSession.CurrentLocation.NPCOnLocation.AvailableQuest.InProgressMessage)));
                    }
                }
            }
            return false;
        }

        private void OnGameMessageRaised(object sender, GameMessageEventArgs e)
        {
            tbLog.Document.Blocks.Add(new Paragraph(new Run(e.Message)));
            tbLog.ScrollToEnd();
        }

        // Other Windows
        private void OpenCharacterScreen(object sender, MouseButtonEventArgs e)
        {
            CharacterWindow characterWindow = new CharacterWindow(gameSession);
            characterWindow.ShowDialog();
            UpdateLocationData();
        }

        private void OpenAlchemyWindow(object sender, MouseButtonEventArgs e)
        {
            AlchemyWindow alcWindow = new AlchemyWindow(gameSession);
            alcWindow.ShowDialog();
        }

        private void OpenGardenWindow(object sender, MouseButtonEventArgs e)
        {
            // Garden currently not working. Sorry.
        }

        private void OpenInventory(object sender, MouseButtonEventArgs e)
        {
            Inventory inventory = new Inventory(gameSession, false);
            inventory.Show();
        }

        // Buttons
        private void btnNorth_Click(object sender, RoutedEventArgs e)
        {
            gameSession.MoveNorth();
            UpdateLocationData();
        }

        private void btnWest_Click(object sender, RoutedEventArgs e)
        {
            gameSession.MoveWest();
            UpdateLocationData();
        }

        private void btnSouth_Click(object sender, RoutedEventArgs e)
        {
            gameSession.MoveSouth();
            UpdateLocationData();
        }

        private void btnEast_Click(object sender, RoutedEventArgs e)
        {
            gameSession.MoveEast();
            UpdateLocationData();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            switch (gameSession.CurrentLocation.Name)
            {
                case "Village Shop":
                    ShopWindow window = new ShopWindow(gameSession);
                    window.ShowDialog();
                    break;
                case "Garden":
                    break;
            }
        }
    }
}
