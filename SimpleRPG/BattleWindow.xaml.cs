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
        private bool isBattleWon;
        public bool IsBattleWon
        {
            get { return isBattleWon; }
        }

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
            gameSession.CurrentEnemy.Damage = gameSession.CurrentEnemy.PhysicalDamageCalculation();
            if (gameSession.CurrentEnemy.CalculateCriticalHitChance())
            {
                gameSession.CurrentEnemy.Damage *= 2;
            }
            gameSession.Hero.CurrentHP -= (int)(gameSession.CurrentEnemy.Damage - gameSession.Hero.Defence);
            if(gameSession.CurrentEnemy.IsCriticalHit)
            {
                tbBattleLog.Text += $"{gameSession.CurrentEnemy.Name} attack {gameSession.Hero.Name} and deals {(int)(gameSession.CurrentEnemy.Damage - gameSession.Hero.Defence)} damage. CRITICAL HIT!" + Environment.NewLine;
            }
            else tbBattleLog.Text += $"{gameSession.CurrentEnemy.Name} attack {gameSession.Hero.Name} and deals {(int)(gameSession.CurrentEnemy.Damage - gameSession.Hero.Defence)} damage." + Environment.NewLine;
            turn++;
            CheckHPStatus();
        }

        private void CheckHPStatus()
        {
            if (gameSession.Hero.CurrentHP <= 0)
            {
                isBattleWon = false;
                Close();
            }
            else if(gameSession.CurrentEnemy.CurrentHP <= 0)
            {
                isBattleWon = true;
                Close();
            }
        }

        private void BasicAttack(object sender, MouseButtonEventArgs e)
        {
            gameSession.Hero.Damage = gameSession.Hero.PhysicalDamageCalculation();
            if (gameSession.Hero.CalculateCriticalHitChance()) gameSession.Hero.Damage *= 2;
            gameSession.CurrentEnemy.CurrentHP -= (int)(gameSession.Hero.Damage - gameSession.CurrentEnemy.Defence);
            if(gameSession.Hero.IsCriticalHit)
            {
                tbBattleLog.Text += $"{gameSession.Hero.Name} attack {gameSession.CurrentEnemy.Name} with {gameSession.Hero.CurrentWeapon.Name} and deals {(int)(gameSession.Hero.Damage - gameSession.CurrentEnemy.Defence)} damage. CRITICAL HIT!" + Environment.NewLine;

            }
            else tbBattleLog.Text += $"{gameSession.Hero.Name} attack {gameSession.CurrentEnemy.Name} with {gameSession.Hero.CurrentWeapon.Name} and deals {(int)(gameSession.Hero.Damage - gameSession.CurrentEnemy.Defence)} damage." + Environment.NewLine; 
            EnemyAttack();
        }

        private void SpecialAttack(object sender, MouseButtonEventArgs e)
        {
            SpellBookWindow spellbook = new SpellBookWindow(gameSession);
            spellbook.ShowDialog();

            if(spellbook.IsSpellCasted)
            {
                gameSession.Hero.CurrentMP -= gameSession.Hero.CurrentSpell.ManaCost;
                gameSession.Hero.Damage = gameSession.Hero.MagicDamageCalculation();
                if (gameSession.Hero.CalculateCriticalHitChance()) gameSession.Hero.Damage *= 2;

                if (gameSession.Hero.CurrentSpell.AffectedTarger.Equals(Game.SpecialAttack.SkillsAndMagic.Target.Self))
                {
                    gameSession.Hero.CurrentHP += (int)gameSession.Hero.Damage;
                    if (gameSession.Hero.CurrentHP > gameSession.Hero.MaxHP) gameSession.Hero.CurrentHP = gameSession.Hero.MaxHP;
                    tbBattleLog.Text += $"{gameSession.Hero.Name} casting {gameSession.Hero.CurrentSpell.Name} and heal for {(int)gameSession.Hero.Damage} HP." + Environment.NewLine;
                }
                else
                {
                    gameSession.CurrentEnemy.CurrentHP -= (int)(gameSession.Hero.Damage - gameSession.CurrentEnemy.Defence); if (gameSession.Hero.IsCriticalHit)
                    {
                        tbBattleLog.Text += $"{gameSession.Hero.Name} attack {gameSession.CurrentEnemy.Name} with {gameSession.Hero.CurrentSpell.Name} and deals {(int)(gameSession.Hero.Damage - gameSession.CurrentEnemy.Defence)} damage. CRITICAL HIT!" + Environment.NewLine;

                    }
                    else tbBattleLog.Text += $"{gameSession.Hero.Name} attack {gameSession.CurrentEnemy.Name} with {gameSession.Hero.CurrentSpell.Name} and deals {(int)(gameSession.Hero.Damage - gameSession.CurrentEnemy.Defence)} damage." + Environment.NewLine;
                }
                EnemyAttack();
            }


            else if (spellbook.IsSkillUsed)
            {
                gameSession.Hero.CurrentMP -= gameSession.Hero.CurrentSkill.ManaCost;
                gameSession.Hero.Damage = gameSession.Hero.SkillDamageCalculation();
                if (gameSession.Hero.CalculateCriticalHitChance()) gameSession.Hero.Damage *= 2;
                gameSession.CurrentEnemy.CurrentHP -= (int)(gameSession.Hero.Damage - gameSession.CurrentEnemy.Defence);
                if (gameSession.Hero.IsCriticalHit)
                {
                    tbBattleLog.Text += $"{gameSession.Hero.Name} attack {gameSession.CurrentEnemy.Name} with {gameSession.Hero.CurrentSkill.Name} and deals {(int)(gameSession.Hero.Damage - gameSession.CurrentEnemy.Defence)} damage. CRITICAL HIT!" + Environment.NewLine;

                }
                else tbBattleLog.Text += $"{gameSession.Hero.Name} attack {gameSession.CurrentEnemy.Name} with {gameSession.Hero.CurrentSkill.Name} and deals {(int)(gameSession.Hero.Damage - gameSession.CurrentEnemy.Defence)} damage." + Environment.NewLine;
                EnemyAttack();
            }
        }
    }
}
