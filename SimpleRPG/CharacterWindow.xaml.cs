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
    /// Логика взаимодействия для CharacterWindow.xaml
    /// </summary>
    public partial class CharacterWindow : Window
    {
        Hero hero;
        public CharacterWindow(Hero _hero)
        {
            InitializeComponent();
            hero = _hero;

            UpdateInfo();
        }
        private void UpdateInfo()
        {
            hero.MaxHP = hero.Vitality * 10;
            hero.MaxMP = hero.Mind * 10;

            lblName.Content = hero.Name;
            lblRace.Content = $"Race: {hero.HeroRace.Name}";
            lblLevel.Content = $"Level: {hero.Level}";
            lblHP.Content = $"HP: {hero.CurrentHP}/{hero.MaxHP}";
            lblMP.Content = $"MP: {hero.CurrentMP}/{hero.MaxMP}";
            lblEXP.Content = $"EXP: {hero.CurrentEXP}/{hero.EXPToLevel}";
            lblStrength.Content = $"Strength: {hero.Strength}";
            lblAgility.Content = $"Agility: {hero.Agility}";
            lblVitality.Content = $"Vitality: {hero.Vitality}";
            lblIntelligence.Content = $"Intelligence: {hero.Intelligence}";
            lblMind.Content = $"Mind: {hero.Mind}";
            lblLuck.Content = $"Luck: {hero.Luck}";
            lblSkillPoins.Content = $"Skill Points Left: {hero.SkillPoints}";

            if (hero.SkillPoints > 0)
            {
                btnStrengthUP.Visibility = Visibility.Visible;
                btnAgilityUP.Visibility = Visibility.Visible;
                btnVitalityUP.Visibility = Visibility.Visible;
                btnIntelligenceUP.Visibility = Visibility.Visible;
                btnMindUP.Visibility = Visibility.Visible;
                btnLuckUP.Visibility = Visibility.Visible;
                lblSkillPoins.Visibility = Visibility.Visible;
            }
            else
            {

                btnStrengthUP.Visibility = Visibility.Hidden;
                btnAgilityUP.Visibility = Visibility.Hidden;
                btnVitalityUP.Visibility = Visibility.Hidden;
                btnIntelligenceUP.Visibility = Visibility.Hidden;
                btnMindUP.Visibility = Visibility.Hidden;
                btnLuckUP.Visibility = Visibility.Hidden;
                lblSkillPoins.Visibility = Visibility.Hidden;
            }
        }

        private void btnStrengthUP_Click(object sender, RoutedEventArgs e)
        {
            hero.Strength++;
            hero.SkillPoints--; 
            UpdateInfo();
        }

        private void btnAgilityUP_Click(object sender, RoutedEventArgs e)
        {
            hero.Agility++;
            hero.SkillPoints--; 
            UpdateInfo();
        }

        private void btnVitalityUP_Click(object sender, RoutedEventArgs e)
        {
            hero.Vitality++;
            hero.SkillPoints--;
            UpdateInfo();
        }

        private void btnIntelligenceUP_Click(object sender, RoutedEventArgs e)
        {
            hero.Intelligence++;
            hero.SkillPoints--;
            UpdateInfo();
        }

        private void btnMindUP_Click(object sender, RoutedEventArgs e)
        {
            hero.Mind++;
            hero.SkillPoints--;
            UpdateInfo();
        }

        private void btnLuckUP_Click(object sender, RoutedEventArgs e)
        {
            hero.Luck++;
            hero.SkillPoints--;
            UpdateInfo();
        }
    }
}
