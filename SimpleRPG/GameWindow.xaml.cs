using System;
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


            UpdateInfo();
        }

        private void UpdateInfo()
        {
            ChangeImages();

            gameSession.Hero.MaxHP = gameSession.Hero.Vitality * 10;
            gameSession.Hero.MaxMP = gameSession.Hero.Mind * 10;
        }

        private void ChangeImages()
        {
            if (gameSession.Hero.SkillPoints > 0) imgCharacter.Source = new BitmapImage(new Uri(@"/Images/Icons/characterLevelUPIcon.png", UriKind.Relative));
            else imgCharacter.Source = new BitmapImage(new Uri(@"/Images/Icons/characterIcon.png", UriKind.Relative));
        }

        private void OpenCharcterScreen(object sender, MouseButtonEventArgs e)
        {
            CharacterWindow characterWindow = new CharacterWindow(gameSession);
            characterWindow.ShowDialog();
            UpdateInfo();
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
    }
}
