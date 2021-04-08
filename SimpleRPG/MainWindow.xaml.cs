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
        GameSession gameSession;
        public MainWindow()
        {
            InitializeComponent();
            gameSession = new GameSession();
            DataContext = gameSession;

            rbHuman.IsChecked = true;

            UpdateInfo();
        }

        private void UpdateInfo()
        {
            tBoxStats.Clear();
            tBoxStats.Text += $"HP: {gameSession.Hero.CurrentHP}/{gameSession.Hero.MaxHP}{Environment.NewLine}";
            tBoxStats.Text += $"MP: {gameSession.Hero.CurrentMP}/{gameSession.Hero.MaxMP}{Environment.NewLine}";
            tBoxStats.Text += $"Strength: {gameSession.Hero.Strength + gameSession.Hero.HeroRace.Strength}{Environment.NewLine}";
            tBoxStats.Text += $"Agility: {gameSession.Hero.Agility + gameSession.Hero.HeroRace.Agility}{Environment.NewLine}";
            tBoxStats.Text += $"Vitality: {gameSession.Hero.Vitality + gameSession.Hero.HeroRace.Vitality}{Environment.NewLine}";
            tBoxStats.Text += $"Intelligence: {gameSession.Hero.Intelligence + gameSession.Hero.HeroRace.Intelligence}{Environment.NewLine}";
            tBoxStats.Text += $"Mind: {gameSession.Hero.Mind + gameSession.Hero.HeroRace.Mind}{Environment.NewLine}";
            tBoxStats.Text += $"Luck: {gameSession.Hero.Luck + gameSession.Hero.HeroRace.Luck}{Environment.NewLine}";
        }


        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if(tbName.Text == "" || tbName.Text == "Enter Name (Max 20 chars)") { MessageBox.Show("Please enter the name"); return; }
            if(tbName.Text.Length > 15) { MessageBox.Show("Name is too long (max 15 characters)"); return; }
            if(tbName.Text.Length < 3) { MessageBox.Show("Name is too short (min 3 character)");  return; }

            gameSession.Hero.Name = tbName.Text;
            gameSession.Hero.Strength += gameSession.Hero.HeroRace.Strength;
            gameSession.Hero.Agility += gameSession.Hero.HeroRace.Agility;
            gameSession.Hero.Vitality += gameSession.Hero.HeroRace.Vitality;
            gameSession.Hero.Intelligence += gameSession.Hero.HeroRace.Intelligence;
            gameSession.Hero.Mind += gameSession.Hero.HeroRace.Mind;
            gameSession.Hero.Luck += gameSession.Hero.HeroRace.Luck;

            gameSession.Hero.RestoreHPMP();

            GameWindow gameWindow = new GameWindow(gameSession);
            Close();
            gameWindow.ShowDialog();
        }

        

        private void rbHuman_Checked(object sender, RoutedEventArgs e)
        {
            gameSession.Hero.HeroRace = World.RaceByID(World.RaceHumanID);
            UpdateInfo();
        }

        private void rbElf_Checked(object sender, RoutedEventArgs e)
        {
            gameSession.Hero.HeroRace = World.RaceByID(World.RaceElfID);
            UpdateInfo();
        }

        private void rbDwarf_Checked(object sender, RoutedEventArgs e)
        {
            gameSession.Hero.HeroRace = World.RaceByID(World.RaceDwarfID);
            UpdateInfo();
        }

        private void rbDogFolk_Checked(object sender, RoutedEventArgs e)
        {
            gameSession.Hero.HeroRace = World.RaceByID(World.RaceDogFolkID);
            UpdateInfo();
        }

        private void rbCatFolk_Checked(object sender, RoutedEventArgs e)
        {
            gameSession.Hero.HeroRace = World.RaceByID(World.RaceCatFolkID);
            UpdateInfo();
        }
    }
}
