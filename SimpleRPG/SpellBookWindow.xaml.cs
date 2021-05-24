using Game;
using Game.SpecialAttack;
using System.Windows;
using System.Windows.Input;

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

            if (IsInBattle)
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
