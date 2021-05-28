using Game;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace SimpleRPG
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GameSession gameSession;
        List<string> HumanImages;
        List<string> ElfImages; 
        List<string> DwarfImages;
        List<string> DogfolkImages;
        List<string> CatfolkImages;
        int imageNumber = 0;
        public MainWindow()
        {
            InitializeComponent();
            gameSession = new GameSession();
            DataContext = gameSession;

            HumanImages = new List<string>()
            {
                @"/Images/Creatures/Heroes/Human1.jpg",
                @"/Images/Creatures/Heroes/Human2.jpg",
                @"/Images/Creatures/Heroes/Human3.jpg",
            };

            ElfImages = new List<string>()
            {
                @"/Images/Creatures/Heroes/Elf1.jpg",
                @"/Images/Creatures/Heroes/Elf2.jpg",
                @"/Images/Creatures/Heroes/Elf3.jpg",
            };

            DwarfImages = new List<string>()
            {
                @"/Images/Creatures/Heroes/Dwarf1.png",
                @"/Images/Creatures/Heroes/Dwarf2.png",
                @"/Images/Creatures/Heroes/Dwarf3.png",
            };

            DogfolkImages = new List<string>()
            {
                @"/Images/Creatures/Heroes/Dog1.jpg",
                @"/Images/Creatures/Heroes/Dog2.jpg",
                @"/Images/Creatures/Heroes/Dog3.jpg",
            };


            CatfolkImages = new List<string>()
            {
                @"/Images/Creatures/Heroes/Cat1.jpg",
                @"/Images/Creatures/Heroes/Cat2.jpg",
                @"/Images/Creatures/Heroes/Cat3.jpg",
            };

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

            imageNumber = 0;
        }


        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text == "" || tbName.Text == "Enter Name (Max 20 chars)") { MessageBox.Show("Please enter the name"); return; }
            if (tbName.Text.Length > 15) { MessageBox.Show("Name is too long (max 15 characters)"); return; }
            if (tbName.Text.Length < 3) { MessageBox.Show("Name is too short (min 3 character)"); return; }

            gameSession.Hero.Name = tbName.Text;
            gameSession.Hero.Strength += gameSession.Hero.HeroRace.Strength;
            gameSession.Hero.Agility += gameSession.Hero.HeroRace.Agility;
            gameSession.Hero.Vitality += gameSession.Hero.HeroRace.Vitality;
            gameSession.Hero.Intelligence += gameSession.Hero.HeroRace.Intelligence;
            gameSession.Hero.Mind += gameSession.Hero.HeroRace.Mind;
            gameSession.Hero.Luck += gameSession.Hero.HeroRace.Luck;

            gameSession.Hero.Avatar = imgSelectedAvatar.Source.ToString();

            gameSession.Hero.RestoreHPMP();

            GameWindow gameWindow = new GameWindow(gameSession);
            Close();
            gameWindow.ShowDialog();
        }



        private void rbHuman_Checked(object sender, RoutedEventArgs e)
        {
            gameSession.Hero.HeroRace = World.RaceByID(0);
            UpdateInfo();
            imgSelectedAvatar.Source = new BitmapImage(new Uri(HumanImages[imageNumber], UriKind.Relative));
        }

        private void rbElf_Checked(object sender, RoutedEventArgs e)
        {
            gameSession.Hero.HeroRace = World.RaceByID(1);
            UpdateInfo();
            imgSelectedAvatar.Source = new BitmapImage(new Uri(ElfImages[imageNumber], UriKind.Relative));
        }

        private void rbDwarf_Checked(object sender, RoutedEventArgs e)
        {
            gameSession.Hero.HeroRace = World.RaceByID(2);
            UpdateInfo();
            imgSelectedAvatar.Source = new BitmapImage(new Uri(DwarfImages[imageNumber], UriKind.Relative));
        }

        private void rbDogFolk_Checked(object sender, RoutedEventArgs e)
        {
            gameSession.Hero.HeroRace = World.RaceByID(3);
            UpdateInfo();
            imgSelectedAvatar.Source = new BitmapImage(new Uri(DogfolkImages[imageNumber], UriKind.Relative));
        }

        private void rbCatFolk_Checked(object sender, RoutedEventArgs e)
        {
            gameSession.Hero.HeroRace = World.RaceByID(4);
            UpdateInfo();
            imgSelectedAvatar.Source = new BitmapImage(new Uri(CatfolkImages[imageNumber], UriKind.Relative));
        }

        private void tbName_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= tbName_GotFocus;
        }

        private void btnPreviousImage_Click(object sender, RoutedEventArgs e)
        {
            imageNumber--;
            if (imageNumber < 0) imageNumber = 0;
            switch (gameSession.Hero.HeroRace.Name)
            {
                case "Human":
                    imgSelectedAvatar.Source = new BitmapImage(new Uri(HumanImages[imageNumber], UriKind.Relative));
                    break;
                case "Elf":
                    imgSelectedAvatar.Source = new BitmapImage(new Uri(ElfImages[imageNumber], UriKind.Relative));
                    break;
                case "Dwarf":
                    imgSelectedAvatar.Source = new BitmapImage(new Uri(DwarfImages[imageNumber], UriKind.Relative));
                    break;
                case "DogFolk":
                    imgSelectedAvatar.Source = new BitmapImage(new Uri(DogfolkImages[imageNumber], UriKind.Relative));
                    break;
                case "CatFolk":
                    imgSelectedAvatar.Source = new BitmapImage(new Uri(CatfolkImages[imageNumber], UriKind.Relative));
                    break;
            }
        }

        private void btnNextImage_Click(object sender, RoutedEventArgs e)
        {
            imageNumber++;
            switch (gameSession.Hero.HeroRace.Name)
            {
                case "Human":
                    if (imageNumber >= HumanImages.Count) imageNumber = HumanImages.Count - 1;
                    imgSelectedAvatar.Source = new BitmapImage(new Uri(HumanImages[imageNumber], UriKind.Relative));
                    break;
                case "Elf":
                    if (imageNumber >= ElfImages.Count) imageNumber = ElfImages.Count - 1;
                    imgSelectedAvatar.Source = new BitmapImage(new Uri(ElfImages[imageNumber], UriKind.Relative));
                    break;
                case "Dwarf":
                    imgSelectedAvatar.Source = new BitmapImage(new Uri(DwarfImages[imageNumber], UriKind.Relative));
                    break;
                case "DogFolk":
                    if (imageNumber >= DogfolkImages.Count) imageNumber = DogfolkImages.Count - 1;
                    imgSelectedAvatar.Source = new BitmapImage(new Uri(DogfolkImages[imageNumber], UriKind.Relative));
                    break;
                case "CatFolk":
                    if (imageNumber >= CatfolkImages.Count) imageNumber = CatfolkImages.Count - 1;
                    imgSelectedAvatar.Source = new BitmapImage(new Uri(CatfolkImages[imageNumber], UriKind.Relative));
                    break;
            }
        }
    }
}
