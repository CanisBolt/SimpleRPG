using Game;
using System.Windows;

namespace SimpleRPG
{
    /// <summary>
    /// Логика взаимодействия для CharacterWindow.xaml
    /// </summary>
    public partial class CharacterWindow : Window
    {
        GameSession gameSession;
        public CharacterWindow(GameSession _gameSession)
        {
            InitializeComponent();
            gameSession = _gameSession;
            DataContext = gameSession;

            UpdateInfo();
        }
        private void UpdateInfo()
        {
            if (gameSession.Hero.SkillPoints > 0)
            {
                btnStrengthUP.Visibility = Visibility.Visible;
                btnAgilityUP.Visibility = Visibility.Visible;
                btnVitalityUP.Visibility = Visibility.Visible;
                btnIntelligenceUP.Visibility = Visibility.Visible;
                btnMindUP.Visibility = Visibility.Visible;
                btnLuckUP.Visibility = Visibility.Visible;
                tbSkillPoints.Visibility = Visibility.Visible;
            }
            else
            {
                btnStrengthUP.Visibility = Visibility.Hidden;
                btnAgilityUP.Visibility = Visibility.Hidden;
                btnVitalityUP.Visibility = Visibility.Hidden;
                btnIntelligenceUP.Visibility = Visibility.Hidden;
                btnMindUP.Visibility = Visibility.Hidden;
                btnLuckUP.Visibility = Visibility.Hidden;
                tbSkillPoints.Visibility = Visibility.Hidden;
            }
        }

        private void btnStrengthUP_Click(object sender, RoutedEventArgs e)
        {
            gameSession.Hero.Strength++;
            gameSession.Hero.SkillPoints--;
            UpdateInfo();
        }

        private void btnAgilityUP_Click(object sender, RoutedEventArgs e)
        {
            gameSession.Hero.Agility++;
            gameSession.Hero.SkillPoints--;
            UpdateInfo();
        }

        private void btnVitalityUP_Click(object sender, RoutedEventArgs e)
        {
            gameSession.Hero.Vitality++;
            gameSession.Hero.SkillPoints--;
            UpdateInfo();
        }

        private void btnIntelligenceUP_Click(object sender, RoutedEventArgs e)
        {
            gameSession.Hero.Intelligence++;
            gameSession.Hero.SkillPoints--;
            UpdateInfo();
        }

        private void btnMindUP_Click(object sender, RoutedEventArgs e)
        {
            gameSession.Hero.Mind++;
            gameSession.Hero.SkillPoints--;
            UpdateInfo();
        }

        private void btnLuckUP_Click(object sender, RoutedEventArgs e)
        {
            gameSession.Hero.Luck++;
            gameSession.Hero.SkillPoints--;
            UpdateInfo();
        }
    }
}
