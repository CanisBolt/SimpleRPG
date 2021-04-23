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
        int turn = 1;
        public BattleWindow(GameSession _gameSession)
        {
            InitializeComponent();

            gameSession = _gameSession;
            DataContext = gameSession;

            Battle();
        }


        private void Battle()
        {
            int heroRoll = Dice.rng.Next(1, 21) + gameSession.Hero.Agility;
            int enemyRoll = Dice.rng.Next(1, 21) + gameSession.CurrentEnemy.Agility;

            if (heroRoll < enemyRoll)
            {
                // Enemy attack first
                EnemyAttack();
            }
        }

        private void EnemyAttack()
        {
            // For now, enemy using same logic as Hero (without weapon).
            // TODO Add choice between normal, skill and magic attack later
            float randomModificator = Dice.rng.Next(2, 4) * 0.4f; // TODO chance this to RNG Float
            gameSession.CurrentEnemy.Damage = randomModificator * gameSession.CurrentEnemy.Strength;
            gameSession.Hero.CurrentHP -= (int)(gameSession.CurrentEnemy.Damage - gameSession.Hero.Defence);
            tbBattleLog.Text += $"{gameSession.CurrentEnemy.Name} attack {gameSession.Hero.Name} and deals {(int)(gameSession.CurrentEnemy.Damage - gameSession.Hero.Defence)} damage." + Environment.NewLine;
            turn++;
            CheckHPStatus();
        }

        private void CheckHPStatus()
        {
            if (gameSession.Hero.CurrentHP <= 0)
            {
                tbBattleLog.Text += $"{gameSession.CurrentEnemy.Name} kill {gameSession.Hero.Name}." + Environment.NewLine;
            }
            else if(gameSession.CurrentEnemy.CurrentHP <= 0)
            {
                tbBattleLog.Text += $"{gameSession.Hero.Name} kill {gameSession.CurrentEnemy.Name}." + Environment.NewLine;
                tbBattleLog.Text += $"{gameSession.Hero.Name} got {gameSession.CurrentEnemy.RewardEXP} exp and {gameSession.CurrentEnemy.RewardMoney} money." + Environment.NewLine;
                gameSession.Hero.CurrentEXP += gameSession.CurrentEnemy.RewardEXP;
                gameSession.Hero.Money += gameSession.CurrentEnemy.RewardMoney;
            }
        }

        private void BasicAttack(object sender, MouseButtonEventArgs e)
        {
            float randomModificator = Dice.rng.Next(2, 4) * 0.4f; // TODO chance this to RNG Float
            gameSession.Hero.Damage = randomModificator * (gameSession.Hero.Strength + (Dice.rng.Next(gameSession.Hero.CurrentWeapon.MinimumDamage, gameSession.Hero.CurrentWeapon.MaximumDamage) + 1));
            gameSession.CurrentEnemy.CurrentHP -= (int)(gameSession.Hero.Damage - gameSession.CurrentEnemy.Defence);
            tbBattleLog.Text += $"{gameSession.Hero.Name} attack {gameSession.CurrentEnemy.Name} with {gameSession.Hero.CurrentWeapon.Name} and deals {(int)(gameSession.Hero.Damage - gameSession.CurrentEnemy.Defence)} damage." + Environment.NewLine; 
            EnemyAttack();
        }

        private void SpecialAttack(object sender, MouseButtonEventArgs e)
        {

        }

        private void MagicAttack(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
