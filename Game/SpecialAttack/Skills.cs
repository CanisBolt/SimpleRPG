using System;

namespace Game.SpecialAttack
{
    public class Skills
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string Description { get; set; }
        public float BaseDamage { get; set; }
        public int ManaCost { get; set; }
        public float AttributeModificator { get; set; }
        public StatusEffect Effect { get; set; }
        public int NumberOfHits { get; set; }
        public Enum AffectedTarger { get; set; }
        public Enum Modificator { get; set; }
        public Enum RequiredWeapon { get; set; }
        public Enum Type { get; set; }

        // TODO add weapon damage to skill damage
        // TODO add damage based on different modificators (like agility)

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

        public enum SpecialAttackType
        {
            Skill,
            Magic
        }
    }
}
