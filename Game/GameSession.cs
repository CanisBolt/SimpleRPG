using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class GameSession : INotifyPropertyChanged
    {
        public Hero Hero { get; set; }
        public GameSession()
        {
            Hero = new Hero("", 1, 5, 5, 5, 5, 5, 5);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
