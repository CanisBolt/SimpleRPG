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
        public Enum BattleStatus { get; set; }

        public BattleWindow(GameSession _gameSession)
        {
            InitializeComponent();

            gameSession = _gameSession;
            DataContext = gameSession;

            imgEnemy.Source = new BitmapImage(new Uri(gameSession.CurrentEnemy.Avatar, UriKind.RelativeOrAbsolute));

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
            // TODO change enemy stats to avoid low damage
            if (gameSession.CurrentEnemy.SkillBook.Count != 0)  gameSession.CurrentEnemy.ChooseRandomSkill();
            
            gameSession.CurrentEnemy.CalculateCriticalHitChance();

            if(gameSession.CurrentEnemy.CurrentSkill == null)
            {
                EnemyBasicAttack(); // Basic Attack
            }
            else
            {
                switch (Dice.rng.Next(2))
                {
                    case 0:
                        EnemyBasicAttack();
                        break;
                    case 1:
                        gameSession.CurrentEnemy.Damage = gameSession.CurrentEnemy.SkillDamageCalculation();
                        gameSession.CurrentEnemy.CurrentMP -= gameSession.CurrentEnemy.CurrentSkill.ManaCost;
                        if (gameSession.CurrentEnemy.IsCriticalHit)
                        {
                            gameSession.CurrentEnemy.Damage *= 2;
                            tbBattleLog.Text += $"{gameSession.CurrentEnemy.Name} attack {gameSession.Hero.Name} with {gameSession.CurrentEnemy.CurrentSkill.Name} and deals {(int)(gameSession.CurrentEnemy.Damage - gameSession.Hero.Defence)} damage. CRITICAL HIT!" + Environment.NewLine;
                        }
                        else tbBattleLog.Text += $"{gameSession.CurrentEnemy.Name} attack {gameSession.Hero.Name} with {gameSession.CurrentEnemy.CurrentSkill.Name} and deals {(int)(gameSession.CurrentEnemy.Damage - gameSession.Hero.Defence)} damage." + Environment.NewLine;
                        break;
                }
            }
            
            gameSession.Hero.CurrentHP -= (int)(gameSession.CurrentEnemy.Damage - gameSession.Hero.Defence);
            CheckHPStatus();
        }

        private void EnemyBasicAttack()
        {
            gameSession.CurrentEnemy.Damage = gameSession.CurrentEnemy.PhysicalDamageCalculation();

            if (gameSession.CurrentEnemy.IsCriticalHit)
            {
                gameSession.CurrentEnemy.Damage *= 2;
                tbBattleLog.Text += $"{gameSession.CurrentEnemy.Name} attack {gameSession.Hero.Name} and deals {(int)(gameSession.CurrentEnemy.Damage - gameSession.Hero.Defence)} damage. CRITICAL HIT!" + Environment.NewLine;
            }
            else tbBattleLog.Text += $"{gameSession.CurrentEnemy.Name} attack {gameSession.Hero.Name} and deals {(int)(gameSession.CurrentEnemy.Damage - gameSession.Hero.Defence)} damage." + Environment.NewLine;
        }

        

        private void CheckHPStatus()
        {
            gameSession.Hero.CalculateDefence();
            gameSession.Hero.StatusEffectsDamageCalculation();
            gameSession.CurrentEnemy.StatusEffectsDamageCalculation();

            if (gameSession.Hero.CurrentHP <= 0)
            {
                BattleStatus = Status.Defeat;
                Close();
            }
            else if(gameSession.CurrentEnemy.CurrentHP <= 0)
            {
                BattleStatus = Status.Victory;
                Close();
            }

            // Null current skill and magic to avoid miscalculating modificator
            gameSession.Hero.CurrentSkill = null;
            gameSession.CurrentEnemy.CurrentSkill = null;

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
            SpellBookWindow spellbook = new SpellBookWindow(gameSession, true);
            spellbook.ShowDialog();
            if(spellbook.IsSkillUsed)
            {
                gameSession.Hero.CurrentMP -= gameSession.Hero.CurrentSkill.ManaCost;
                gameSession.Hero.Damage = gameSession.Hero.SkillDamageCalculation();
                if (gameSession.Hero.CalculateCriticalHitChance()) gameSession.Hero.Damage *= 2;

                if (gameSession.Hero.CurrentSkill.AffectedTarger.Equals(Game.SpecialAttack.Skills.Target.Self))
                {
                    gameSession.Hero.CurrentHP += (int)gameSession.Hero.Damage;
                    if (gameSession.Hero.CurrentHP > gameSession.Hero.MaxHP) gameSession.Hero.CurrentHP = gameSession.Hero.MaxHP;
                    tbBattleLog.Text += $"{gameSession.Hero.Name} casting {gameSession.Hero.CurrentSkill.Name} and heal for {(int)gameSession.Hero.Damage} HP." + Environment.NewLine;
                }
                else
                {
                    if (gameSession.Hero.IsCriticalHit)
                    {
                        tbBattleLog.Text += $"{gameSession.Hero.Name} attack {gameSession.CurrentEnemy.Name} with {gameSession.Hero.CurrentSkill.Name} and deals {(int)(gameSession.Hero.Damage - gameSession.CurrentEnemy.Defence)} damage. CRITICAL HIT!" + Environment.NewLine;
                    }
                    else tbBattleLog.Text += $"{gameSession.Hero.Name} attack {gameSession.CurrentEnemy.Name} with {gameSession.Hero.CurrentSkill.Name} and deals {(int)(gameSession.Hero.Damage - gameSession.CurrentEnemy.Defence)} damage." + Environment.NewLine;
                    gameSession.CurrentEnemy.CurrentHP -= (int)(gameSession.Hero.Damage - gameSession.CurrentEnemy.Defence);
                }

                // Apply Status Effect
                if (gameSession.Hero.CurrentSkill.Effect != null)
                {
                    gameSession.CurrentEnemy.ApplyStatusEffect(gameSession.Hero.CurrentSkill.Effect.Name, gameSession.Hero.CurrentSkill.Effect.ID, gameSession.Hero.CurrentSkill.Effect.Description, gameSession.Hero.CurrentSkill.Effect.AffectHP, gameSession.Hero.CurrentSkill.Effect.AffectMP, gameSession.Hero.CurrentSkill.Effect.Duration, gameSession.Hero.CurrentSkill.Effect.Type);
                    tbBattleLog.Text += $"{gameSession.Hero.Name} applied {gameSession.Hero.CurrentSkill.Effect.Name} on {gameSession.CurrentEnemy.Name}" + Environment.NewLine;
                }
                EnemyAttack();
            }
        }

        private void OpenInventory(object sender, MouseButtonEventArgs e)
        {
            Inventory inventory = new Inventory(gameSession, true);
            inventory.ShowDialog();

            if(inventory.IsItemUsed)
            {
                EnemyAttack();
            }
        }

        private void Escape(object sender, MouseButtonEventArgs e)
        {
            if(Dice.rng.Next(21) > 10)
            {
                BattleStatus = Status.Escape;
                Close();
            }
            tbBattleLog.Text += "You tried to escape from battle and failed." + Environment.NewLine;
            EnemyAttack();
        }

        public enum Status
        {
            Victory,
            Defeat,
            Escape
        }
    }
}
