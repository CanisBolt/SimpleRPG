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
    /// Логика взаимодействия для BattleWindow.xaml
    /// </summary>
    public partial class BattleWindow : Window
    {
        GameSession gameSession;
        public BattleWindow(GameSession _gameSession)
        {
            InitializeComponent();

            gameSession = _gameSession;
            DataContext = gameSession;

            Battle();
        }


        private void Battle()
        {
            int turn = 1;

            int heroRoll = Dice.rng.Next(1, 21) + gameSession.Hero.Agility;
            int enemyRoll = Dice.rng.Next(1, 21) + gameSession.CurrentEnemy.Agility;

            if (heroRoll >= enemyRoll)
            {
                // Hero attack first
            }
            else
            {
                // Enemy attack first
            }
        }
    }
}
