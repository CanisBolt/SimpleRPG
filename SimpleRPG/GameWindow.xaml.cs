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
        Hero hero;
        public GameWindow(Hero _hero)
        {
            InitializeComponent();
            hero = _hero;
            hero.SkillPoints = 5;

            UpdateInfo();
        }

        private void UpdateInfo()
        {
            ChangeImages();

            hero.MaxHP = hero.Vitality * 10;
            hero.MaxMP = hero.Mind * 10;

            lblName.Content = hero.Name;
            lblLevel.Content = $"Level: {hero.Level}";
            lblHP.Content = $"HP: {hero.CurrentHP}/{hero.MaxHP}";
            lblMP.Content = $"MP: {hero.CurrentMP}/{hero.MaxMP}";
            lblEXP.Content = $"EXP: {hero.CurrentEXP}/{hero.EXPToLevel}";
        }

        private void ChangeImages()
        {
            if (hero.SkillPoints > 0) imgCharacter.Source = new BitmapImage(new Uri(@"/Images/Icons/characterLevelUPIcon.png", UriKind.Relative));
            else imgCharacter.Source = new BitmapImage(new Uri(@"/Images/Icons/characterIcon.png", UriKind.Relative));
        }

        private void OpenCharcterScreen(object sender, MouseButtonEventArgs e)
        {
            CharacterWindow characterWindow = new CharacterWindow(hero);
            characterWindow.ShowDialog();
            UpdateInfo();
        }
    }
}
