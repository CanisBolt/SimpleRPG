using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.SpecialAttack
{
    public class WeaponSkills : SkillsAndMagic
    {
        public float StrengthModificator { get; set; }
        public int NumberOfHits { get; set; }
        public Enum RequiredWeapon { get; set; }
        // TODO add weapon damage to skill damage
        // TODO add damage based on different modificators (like agility)
    }
}
