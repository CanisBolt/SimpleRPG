using Game.Items;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameLocations
{
    public class Shop
    {
        public int ID { get; set; }
        public ObservableCollection<GameItems> Inventory { get; set; }

        public Shop(int id)
        {
            ID = id;
            Inventory = new ObservableCollection<GameItems>();
        }
    }
}
