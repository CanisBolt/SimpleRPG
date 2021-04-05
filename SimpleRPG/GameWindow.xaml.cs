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

            UpdateInfo();
        }

        private void UpdateInfo()
        {
            lblName.Content = hero.Name;
            lblLevel.Content = $"Level: {hero.Level}";
            lblHP.Content = $"HP: {hero.CurrentHP}/{hero.MaxHP}";
            lblMP.Content = $"MP: {hero.CurrentMP}/{hero.MaxMP}";
            lblEXP.Content = $"EXP: {hero.CurrentEXP}/{hero.EXPToLevel}";
        }
    }
}
