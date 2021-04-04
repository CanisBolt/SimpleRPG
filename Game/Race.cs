using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Race : Creatures
    {
        public int ID { get; set; }
        public string Description { get; set; }
        // TODO add some passives for each race
        public Race(string name, int strength, int agility, int vitality, int intelligence, int mind, int luck, int id) : base(name, strength, agility, vitality, intelligence, mind, luck)
        {
            ID = id;
        }

    }
}
