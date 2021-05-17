using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameLocations
{
    public class NPC
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string HelloMessage { get; set; } // message on click (if no quest)
        public Quest AvailableQuest { get; set; }

        public NPC(string name, int id, string helloMessage, Quest availableQuest)
        {
            Name = name;
            ID = id;
            HelloMessage = helloMessage;
            AvailableQuest = availableQuest;
        }
    }
}
