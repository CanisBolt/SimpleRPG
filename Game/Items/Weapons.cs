using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Items
{
    public class Weapons : GameItems
    {
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }
        public Weapons(string name, int id, int price, int minDamage, int maxDamage) : base(name, id, price)
        {
            MinimumDamage = minDamage;
            MaximumDamage = maxDamage;
        }

        public new Weapons Clone()
        {
            return new Weapons(Name, ID, Price, MinimumDamage, MaximumDamage);
        }
    }
}
