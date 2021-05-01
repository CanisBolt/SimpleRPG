using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.SpecialAttack
{
    public class StatusEffect
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string Description { get; set; }
        public float AffectHP { get; set; }
        public float AffectMP { get; set; }
        public int Duration { get; set; }
        public Enum Type { get; set; }

        public StatusEffect(string name, int id, string description, float affectHP, float affectMP, int duration, Enum type)
        {
            Name = name;
            ID = id;
            Description = description;
            AffectHP = affectHP;
            AffectMP = affectMP;
            Duration = duration;
            Type = type;
        }

        public StatusEffect()
        {
        }

        public enum StatusType
        {
            DamageOverTime,
            HealOverTime
        }

    }
}
