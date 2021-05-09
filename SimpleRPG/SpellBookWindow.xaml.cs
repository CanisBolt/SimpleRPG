using Game;
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
using Game.SpecialAttack;

namespace SimpleRPG
{
    /// <summary>
    /// Логика взаимодействия для SpellBookWindow.xaml
    /// </summary>
    public partial class SpellBookWindow : Window
    {
        GameSession gameSession; 
        public bool IsSkillUsed { get; set; }
        public bool IsInBattle { get; set; }
        public SpellBookWindow(GameSession _gameSession, bool isInBattle)
        {
            InitializeComponent();

            gameSession = _gameSession;
            DataContext = gameSession;
            IsSkillUsed = false;
            IsInBattle = isInBattle;
        }

        private void SkillCast(object sender, MouseButtonEventArgs e)
        {
            if (dgSkillBook.SelectedItem == null) return;

            if(IsInBattle)
            {
                Skills selectedSkill = (Skills)dgSkillBook.SelectedItem;
                if (selectedSkill.RequiredWeapon.Equals(gameSession.Hero.CurrentWeapon.TypeOfWeapon) || selectedSkill.RequiredWeapon.Equals(Game.Items.GameItems.WeaponType.None))
                {
                    if (gameSession.Hero.CurrentMP >= selectedSkill.ManaCost)
                    {
                        gameSession.Hero.CurrentSkill = selectedSkill;
                        IsSkillUsed = true;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show($"Not enough mana points for {selectedSkill.Name}");
                    }
                }
                else
                {
                    MessageBox.Show($"This skill cannot be used with current weapon. Required weapon type: {selectedSkill.RequiredWeapon}");
                }
            }
            else
            {
                Skills selectedSkill = (Skills)dgSkillBook.SelectedItem;
                if (selectedSkill.AffectedTarger.Equals(Skills.Target.Self))
                {
                    if (gameSession.Hero.CurrentMP >= selectedSkill.ManaCost)
                    {
                        gameSession.Hero.CurrentSkill = selectedSkill;
                        IsSkillUsed = true;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show($"Not enough mana points for {selectedSkill.Name}");
                    }
                }
                else
                {
                    MessageBox.Show($"You cannot cast battle skill/magic outside the battle!");
                }
            }
        }
    }
}
