using System.Collections.Generic;

namespace Game.LivingCreatures
{
    public class Enemy : Creature
    {
        private readonly List<Items.ItemPercentage> lootTable =
               new List<Items.ItemPercentage>();
        public int ID { get; set; }
        public int RewardEXP { get; set; }
        public int RewardGold { get; set; }
        public int EncounterChance { get; set; }
        public bool IsAgressive { get; set; }
        public bool HasAdvantage { get; set; }
        public Enemy(string name, int level, int strength, int agility, int vitality, int intelligence, int mind, int luck, int rewardEXP, int rewardGold, int encounterChance, int id, bool isAgressive, float defence, string avatar) : base(name, level, strength, agility, vitality, intelligence, mind, luck)
        {
            ID = id;
            RewardEXP = rewardEXP;
            RewardGold = rewardGold;
            EncounterChance = encounterChance;
            IsAgressive = isAgressive;
            Defence = defence;
            Avatar = avatar;

            HasAdvantage = false;
        }

        public void AddItemToLootTable(int id, int percentage)
        {
            // Remove the entry from the loot table,
            // if it already contains an entry with this ID
            lootTable.RemoveAll(ip => ip.ID == id);

            lootTable.Add(new Items.ItemPercentage(id, percentage));
        }

        public Enemy GetNewInstance()
        {
            // "Clone" this monster to a new Monster object
            Enemy newEnemy =
                new Enemy(Name, Level, Strength, Agility, Vitality, Intelligence, Mind, Luck, RewardEXP, RewardGold, EncounterChance, ID, IsAgressive, Defence, Avatar);

            foreach (Items.ItemPercentage itemPercentage in lootTable)
            {
                // Clone the loot table - even though we probably won't need it
                newEnemy.AddItemToLootTable(itemPercentage.ID, itemPercentage.Percentage);

                // Populate the new monster's inventory, using the loot table
                if (Dice.rng.Next(1, 100) <= itemPercentage.Percentage)
                {
                    newEnemy.AddItemToInventory(Factory.ItemsFactory.CreateGameItem(itemPercentage.ID));
                }
            }

            return newEnemy;
        }

        public void ChooseRandomSkill()
        {
            List<SpecialAttack.Skills> possibleSkill = new List<SpecialAttack.Skills>();
            // Add possible skills for separate list
            foreach (var skill in SkillBook)
            {
                if (skill.ManaCost <= CurrentMP)
                {
                    possibleSkill.Add(skill);
                }
            }

            if (possibleSkill.Count == 0) return;

            // Choose a skill from this list
            int randomSkill = Dice.rng.Next(possibleSkill.Count);
            CurrentSkill = possibleSkill[randomSkill];
        }
    }
}
