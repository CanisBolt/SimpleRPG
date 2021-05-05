﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Game;

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
        }

        private void btnWest_Click(object sender, RoutedEventArgs e)
        {
            gameSession.MoveWest();
        }

        private void btnSouth_Click(object sender, RoutedEventArgs e)
        {
            gameSession.MoveSouth();
        }

        private void btnEast_Click(object sender, RoutedEventArgs e)
        {
            gameSession.MoveEast();
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
                tbLog.Text += $"{gameSession.Hero.Name} got {gameSession.CurrentEnemy.RewardEXP} exp and {gameSession.CurrentEnemy.RewardMoney} money." + Environment.NewLine;
                gameSession.Hero.CurrentEXP += gameSession.CurrentEnemy.RewardEXP;
                gameSession.Hero.Money += gameSession.CurrentEnemy.RewardMoney;

                gameSession.Hero.LevelUP(); // Check for LevelUP
                ChangeImages();
            }
            else if(battle.BattleStatus.Equals(BattleWindow.Status.Defeat))
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
    }
}
