using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.SpecialAttack
{
    public class SkillsAndMagic
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string Description { get; set; }
        public float BaseDamage { get; set; }
        public int ManaCost { get; set; }
        public float AttributeModificator { get; set; }
        public Enum AffectedTarger { get; set; }
        public Enum Modificator { get; set; }

        

        public enum Target
        {
            Self,
            Enemy
        }


        public enum Attribute
        {
            Strength,
            Agility,
            Vitality,
            Intelligence,
            Mind,
            Luck
        }
    }
}
