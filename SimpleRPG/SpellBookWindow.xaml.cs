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
        private bool isSpellCasted;
        private bool isSkillUsed;
        public bool IsSpellCasted
        {
            get { return isSpellCasted; }
        }

        public bool IsSkillUsed
        {
            get { return isSkillUsed; }
        }
        public SpellBookWindow(GameSession _gameSession)
        {
            InitializeComponent();

            gameSession = _gameSession;
            DataContext = gameSession;

            isSpellCasted = false;
            isSkillUsed = false;
        }

        private void SpellCast(object sender, MouseButtonEventArgs e)
        {
            if (dgSpellBook.SelectedItem == null) return;

            Magic selectedSpell = (Magic)dgSpellBook.SelectedItem;
            if (gameSession.Hero.CurrentMP >= selectedSpell.ManaCost)
            {
                gameSession.Hero.CurrentSpell = selectedSpell;
                isSpellCasted = true;
                Close();
            }
        }

        private void SkillCast(object sender, MouseButtonEventArgs e)
        {
            if (dgSkillBook.SelectedItem == null) return;

            WeaponSkills selectedSkill = (WeaponSkills)dgSkillBook.SelectedItem;
            if (gameSession.Hero.CurrentMP >= selectedSkill.ManaCost)
            {
                gameSession.Hero.CurrentSkill = selectedSkill;
                isSkillUsed = true;
                Close();
            }
        }
    }
}
