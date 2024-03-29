﻿using Game;
using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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
            Game.LivingCreatures.Creature.OnMessageRaised += OnBattleMessageRaised;
            DataContext = gameSession;

            imgEnemy.Source = new BitmapImage(new Uri(gameSession.CurrentEnemy.Avatar, UriKind.Relative));

            Battle();
        }

        private void Battle()
        {
            int heroRoll = (int)Dice.RollDice(1, 20) + gameSession.Hero.Agility;
            int enemyRoll = (int)Dice.RollDice(1, 20) + gameSession.CurrentEnemy.Agility;

            if(gameSession.CurrentEnemy.HasAdvantage)
            {
                tbBattleLog.Document.Blocks.Add(new Paragraph(new Run($"{gameSession.CurrentEnemy.Name} caught {gameSession.Hero.Name} off guard and deals first strike!")));
                EnemyAttack();
            }
            else if (heroRoll < enemyRoll)
            {
                // Enemy attack first
                EnemyAttack();
            }
        }

        private void EnemyAttack()
        {
            // For now, enemy using same logic as Hero (without weapon).
            // TODO change enemy stats to avoid low damage
            if (gameSession.CurrentEnemy.SkillBook.Count != 0) gameSession.CurrentEnemy.ChooseRandomSkill();

            if (gameSession.CurrentEnemy.CurrentSkill == null)
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
                        gameSession.CurrentEnemy.SkillDamageCalculation();
                        gameSession.CurrentEnemy.DecreaseMP(gameSession.CurrentEnemy.CurrentSkill.ManaCost);
                        break;
                }
            }
            gameSession.Hero.DecreaseHP((int)(gameSession.CurrentEnemy.Damage - gameSession.Hero.Defence));
            CheckHPStatus();
        }

        private void EnemyBasicAttack()
        {
            gameSession.CurrentEnemy.Damage = gameSession.CurrentEnemy.PhysicalDamageCalculation();

            if (gameSession.CurrentEnemy.IsCriticalHit)
            {
                tbBattleLog.Document.Blocks.Add(new Paragraph(new Run($"{gameSession.CurrentEnemy.Name} attack {gameSession.Hero.Name} and deals {(int)(gameSession.CurrentEnemy.Damage - gameSession.Hero.Defence)} damage. CRITICAL HIT!")));
            }
            else tbBattleLog.Document.Blocks.Add(new Paragraph(new Run($"{gameSession.CurrentEnemy.Name} attack {gameSession.Hero.Name} and deals {(int)(gameSession.CurrentEnemy.Damage - gameSession.Hero.Defence)} damage.")));
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
            else if (gameSession.CurrentEnemy.CurrentHP <= 0)
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
            gameSession.CurrentEnemy.DecreaseHP((int)(gameSession.Hero.Damage - gameSession.CurrentEnemy.Defence));
            IncreaseWeaponSkill();
            if (gameSession.Hero.IsCriticalHit)
            {
                tbBattleLog.Document.Blocks.Add(new Paragraph(new Run($"{gameSession.Hero.Name} attack {gameSession.CurrentEnemy.Name} with {gameSession.Hero.CurrentWeapon.Name} and deals {(int)(gameSession.Hero.Damage - gameSession.CurrentEnemy.Defence)} damage. CRITICAL HIT!")));
            }
            else tbBattleLog.Document.Blocks.Add(new Paragraph(new Run($"{gameSession.Hero.Name} attack {gameSession.CurrentEnemy.Name} with {gameSession.Hero.CurrentWeapon.Name} and deals {(int)(gameSession.Hero.Damage - gameSession.CurrentEnemy.Defence)} damage.")));
            EnemyAttack();
        }

        private void SpecialAttack(object sender, MouseButtonEventArgs e)
        {
            SpellBookWindow spellbook = new SpellBookWindow(gameSession, true);
            spellbook.ShowDialog();
            if (spellbook.IsSkillUsed)
            {
                gameSession.Hero.CurrentMP -= gameSession.Hero.CurrentSkill.ManaCost;
                gameSession.Hero.SkillDamageCalculation();

                if (gameSession.Hero.CurrentSkill.AffectedTarger.Equals(Game.SpecialAttack.Skills.Target.Self))
                {
                    gameSession.Hero.RestoreHP((int)gameSession.Hero.Damage);
                    if (gameSession.Hero.CurrentHP > gameSession.Hero.MaxHP) gameSession.Hero.CurrentHP = gameSession.Hero.MaxHP;
                    IncreaseMagicSkill();
                }
                else
                {
                    if(gameSession.Hero.CurrentSkill.Type.Equals(Game.SpecialAttack.Skills.SpecialAttackType.PhysicalSkill)) IncreaseWeaponSkill();
                    else IncreaseMagicSkill();
                    gameSession.CurrentEnemy.DecreaseHP((int)(gameSession.Hero.Damage - gameSession.CurrentEnemy.Defence));
                }

                // Apply Status Effect
                if (gameSession.Hero.CurrentSkill.Effect != null)
                {
                    gameSession.CurrentEnemy.ApplyStatusEffect(gameSession.Hero.CurrentSkill.Effect.Name, gameSession.Hero.CurrentSkill.Effect.ID, gameSession.Hero.CurrentSkill.Effect.Description, gameSession.Hero.CurrentSkill.Effect.AffectHP, gameSession.Hero.CurrentSkill.Effect.AffectMP, gameSession.Hero.CurrentSkill.Effect.Duration, gameSession.Hero.CurrentSkill.Effect.Type);
                }
                EnemyAttack();
            }
        }

        private void IncreaseWeaponSkill()
        {
            float increase = 0.01f * gameSession.CurrentEnemy.Level - ((gameSession.Hero.Level - gameSession.CurrentEnemy.Level) / 100);
            switch (gameSession.Hero.CurrentWeapon.TypeOfWeapon)
            {
                case Game.Items.GameItems.WeaponType.Sword:
                    gameSession.Hero.SwordSkill += increase;
                    break;
                case Game.Items.GameItems.WeaponType.Dagger:
                    gameSession.Hero.DaggerSkill += increase;
                    break;
                case Game.Items.GameItems.WeaponType.Staff:
                    gameSession.Hero.StaffSkill += increase;
                    break;
            }
        }

        private void IncreaseMagicSkill()
        {
            float increase = 0.01f * gameSession.CurrentEnemy.Level - ((gameSession.Hero.Level - gameSession.CurrentEnemy.Level) / 100);
            switch(gameSession.Hero.CurrentSkill.Type)
            {
                case Game.SpecialAttack.Skills.SpecialAttackType.FireMagic:
                    gameSession.Hero.FireMagic += increase;
                    break;
                case Game.SpecialAttack.Skills.SpecialAttackType.IceMagic:
                    gameSession.Hero.IceMagic += increase;
                    break;
                case Game.SpecialAttack.Skills.SpecialAttackType.LightningMagic:
                    gameSession.Hero.LightningMagic += increase;
                    break;
                case Game.SpecialAttack.Skills.SpecialAttackType.HealingMagic:
                    gameSession.Hero.HealingMagic += increase;
                    break;
            }
        }

        private void OpenInventory(object sender, MouseButtonEventArgs e)
        {
            Inventory inventory = new Inventory(gameSession, true);
            inventory.ShowDialog();

            if (inventory.IsItemUsed)
            {
                EnemyAttack();
            }
        }

        private void Escape(object sender, MouseButtonEventArgs e)
        {
            if (Dice.rng.Next(21) > 10)
            {
                BattleStatus = Status.Escape;
                Close();
            }
            tbBattleLog.Document.Blocks.Add(new Paragraph(new Run("You tried to escape from battle and failed.")));
            EnemyAttack();
        }

        private void OnBattleMessageRaised(object sender, GameMessageEventArgs e)
        {
            tbBattleLog.Document.Blocks.Add(new Paragraph(new Run(e.Message)));
            tbBattleLog.ScrollToEnd();
        }

        public enum Status
        {
            Victory,
            Defeat,
            Escape
        }
    }
}
