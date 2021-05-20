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
using Game.GameLocations;

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

            rbHuman.IsChecked = true; // Default Race

            UpdateInfo();
        }

        private void UpdateInfo()
        {
            tbStrength.Text = $"{gameSession.Hero.Strength + gameSession.Hero.HeroRace.Strength}";
            tbAgility.Text = $"{gameSession.Hero.Agility + gameSession.Hero.HeroRace.Agility}";
            tbVitality.Text = $"{gameSession.Hero.Vitality + gameSession.Hero.HeroRace.Vitality}";
            tbIntelligence.Text = $"{gameSession.Hero.Intelligence + gameSession.Hero.HeroRace.Intelligence}";
            tbMind.Text = $"{gameSession.Hero.Mind + gameSession.Hero.HeroRace.Mind}";
            tbLuck.Text = $"{gameSession.Hero.Luck + gameSession.Hero.HeroRace.Luck}";

            tbHP.Text = $"{(gameSession.Hero.Vitality + gameSession.Hero.HeroRace.Vitality) * 10}";
            tbMP.Text = $"{(gameSession.Hero.Mind + gameSession.Hero.HeroRace.Mind) * 10}";
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
            gameSession.Hero.HeroRace = World.RaceByID(0);
            UpdateInfo();
        }

        private void rbElf_Checked(object sender, RoutedEventArgs e)
        {
            gameSession.Hero.HeroRace = World.RaceByID(1);
            UpdateInfo();
        }

        private void rbDwarf_Checked(object sender, RoutedEventArgs e)
        {
            gameSession.Hero.HeroRace = World.RaceByID(2);
            UpdateInfo();
        }

        private void rbDogFolk_Checked(object sender, RoutedEventArgs e)
        {
            gameSession.Hero.HeroRace = World.RaceByID(3);
            UpdateInfo();
        }

        private void rbCatFolk_Checked(object sender, RoutedEventArgs e)
        {
            gameSession.Hero.HeroRace = World.RaceByID(4);
            UpdateInfo();
        }

        private void tbName_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= tbName_GotFocus;
        }
    }
}
