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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Game;

namespace SimpleRPG
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Hero hero;
        public MainWindow()
        {
            InitializeComponent();
            hero = new Hero("", 1, 5, 5, 5, 5, 5, 5);
            rbHuman.IsChecked = true;

            UpdateInfo();
        }

        private void UpdateInfo()
        {
            tBoxStats.Clear();
            tBoxStats.Text += $"HP: {hero.CurrentHP}/{hero.MaxHP}{Environment.NewLine}";
            tBoxStats.Text += $"MP: {hero.CurrentMP}/{hero.MaxMP}{Environment.NewLine}";
            tBoxStats.Text += $"Strength: {hero.Strength + hero.HeroRace.Strength}{Environment.NewLine}";
            tBoxStats.Text += $"Agility: {hero.Agility + hero.HeroRace.Agility}{Environment.NewLine}";
            tBoxStats.Text += $"Vitality: {hero.Vitality + hero.HeroRace.Vitality}{Environment.NewLine}";
            tBoxStats.Text += $"Intelligence: {hero.Intelligence + hero.HeroRace.Intelligence}{Environment.NewLine}";
            tBoxStats.Text += $"Mind: {hero.Mind + hero.HeroRace.Mind}{Environment.NewLine}";
            tBoxStats.Text += $"Luck: {hero.Luck + hero.HeroRace.Luck}{Environment.NewLine}";
        }


        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            hero.Name = tbName.Text;

            hero.Strength += hero.HeroRace.Strength;
            hero.Agility += hero.HeroRace.Agility;
            hero.Vitality += hero.HeroRace.Vitality;
            hero.Intelligence += hero.HeroRace.Intelligence;
            hero.Mind += hero.HeroRace.Mind;
            hero.Luck += hero.HeroRace.Luck;

            hero.RestoreHPMP();
        }

        private void rbHuman_Checked(object sender, RoutedEventArgs e)
        {
            hero.HeroRace = World.RaceByID(World.RaceHumanID);
            UpdateInfo();
        }

        private void rbElf_Checked(object sender, RoutedEventArgs e)
        {
            hero.HeroRace = World.RaceByID(World.RaceElfID);
            UpdateInfo();
        }

        private void rbDwarf_Checked(object sender, RoutedEventArgs e)
        {
            hero.HeroRace = World.RaceByID(World.RaceDwarfID);
            UpdateInfo();
        }

        private void rbDogFolk_Checked(object sender, RoutedEventArgs e)
        {
            hero.HeroRace = World.RaceByID(World.RaceDogFolkID);
            UpdateInfo();
        }

        private void rbCatFolk_Checked(object sender, RoutedEventArgs e)
        {
            hero.HeroRace = World.RaceByID(World.RaceCatFolkID);
            UpdateInfo();
        }
    }
}
