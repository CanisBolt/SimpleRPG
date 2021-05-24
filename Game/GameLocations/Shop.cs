using Game.Items;
using System.Collections.ObjectModel;

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
