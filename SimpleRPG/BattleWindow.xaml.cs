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
            // TODO looks not so good. Change it later
            gameSession.CurrentEnemy.CalculateCriticalHitChance();
            switch (Dice.rng.Next(3))
            {
                case 0:
                    EnemyBasicAttack(); // Basic Attack
                    break;
                case 1:
                    if(gameSession.CurrentEnemy.SpellBook.Count != 0 && gameSession.CurrentEnemy.CheckMPForSpells())
                    {
                        gameSession.CurrentEnemy.ChooseRandomSpell();
                        gameSession.CurrentEnemy.Damage = gameSession.CurrentEnemy.MagicDamageCalculation(); // Magic Attack
                        gameSession.CurrentEnemy.CurrentMP -= gameSession.CurrentEnemy.CurrentSpell.ManaCost;
                        if (gameSession.CurrentEnemy.IsCriticalHit)
                        {
                            gameSession.CurrentEnemy.Damage *= 2;
                            tbBattleLog.Text += $"{gameSession.CurrentEnemy.Name} attack {gameSession.Hero.Name} with {gameSession.CurrentEnemy.CurrentSpell.Name} and deals {(int)(gameSession.CurrentEnemy.Damage - gameSession.Hero.Defence)} damage. CRITICAL HIT!" + Environment.NewLine;
                        }
                        else tbBattleLog.Text += $"{gameSession.CurrentEnemy.Name} attack {gameSession.Hero.Name} with {gameSession.CurrentEnemy.CurrentSpell.Name} and deals {(int)(gameSession.CurrentEnemy.Damage - gameSession.Hero.Defence)} damage." + Environment.NewLine;
                        break;
                    }
                    else
                    {
                        EnemyBasicAttack();
                    }
                    break;
                case 2:
                    if(gameSession.CurrentEnemy.SkillBook.Count != 0 && gameSession.CurrentEnemy.CheckMPForSkill())
                    {
                        gameSession.CurrentEnemy.ChooseRandomSkill();
                        gameSession.CurrentEnemy.Damage = gameSession.CurrentEnemy.SkillDamageCalculation(); // Skill Attack
                        gameSession.CurrentEnemy.CurrentMP -= gameSession.CurrentEnemy.CurrentSkill.ManaCost;
                        if (gameSession.CurrentEnemy.IsCriticalHit)
                        {
                            gameSession.CurrentEnemy.Damage *= 2;
                            tbBattleLog.Text += $"{gameSession.CurrentEnemy.Name} attack {gameSession.Hero.Name} with {gameSession.CurrentEnemy.CurrentSkill.Name} and deals {(int)(gameSession.CurrentEnemy.Damage - gameSession.Hero.Defence)} damage. CRITICAL HIT!" + Environment.NewLine;
                        }
                        else tbBattleLog.Text += $"{gameSession.CurrentEnemy.Name} attack {gameSession.Hero.Name} with {gameSession.CurrentEnemy.CurrentSkill.Name} and deals {(int)(gameSession.CurrentEnemy.Damage - gameSession.Hero.Defence)} damage." + Environment.NewLine;
                        break;
                    }
                    else
                    {
                        EnemyBasicAttack();
                    }
                    break;
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
            gameSession.Hero.CurrentSpell = null;
            gameSession.CurrentEnemy.CurrentSkill = null;
            gameSession.CurrentEnemy.CurrentSpell = null;

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

                // Apply Status Effect
                if (gameSession.Hero.CurrentSpell.Effect != null)
                {
                    gameSession.CurrentEnemy.ApplyStatusEffect(gameSession.Hero.CurrentSpell.Effect.Name, gameSession.Hero.CurrentSpell.Effect.ID, gameSession.Hero.CurrentSpell.Effect.Description, gameSession.Hero.CurrentSpell.Effect.AffectHP, gameSession.Hero.CurrentSpell.Effect.AffectMP, gameSession.Hero.CurrentSpell.Effect.Duration, gameSession.Hero.CurrentSpell.Effect.Type);
                    tbBattleLog.Text += $"{gameSession.Hero.Name} applied {gameSession.Hero.CurrentSpell.Effect.Name} on {gameSession.CurrentEnemy.Name}" + Environment.NewLine;
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
