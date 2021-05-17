using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameLocations
{
    public class Quest
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string Description { get; set; }
        public string StartMessage { get; set; }
        public string InProgressMessage { get; set; }
        public string CompleteMessage { get; set; }
        public Items.GameItems RequiredItems { get; set; }
        public int RequiredCount { get; set; }
        public int RewardEXP { get; set; }
        public int RewardGold { get; set; }
        public Enum QuestStatus { get; set; }


        public enum Status
        {
            Available,
            InProgress,
            Completed
        }
    }
}
