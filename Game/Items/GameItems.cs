using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Items
{
    public class GameItems
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public int Price { get; set; }

        public GameItems(string name, int id, int price)
        {
            Name = name;
            ID = id;
            Price = price;
        }
        public GameItems Clone()
        {
            return new GameItems(Name, ID, Price);
        }
    }
}
