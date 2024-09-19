using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HeroesLibrary
{
    public class Hero
    {
        public string Name { get; set; }
        public string Main_attribute { get; set; }
        public int Damage { get; set; }
        public string Attack_type { get; set; }
        public int Move_speed { get; set; }
        public string Difficulty { get; set; }

        public Hero(string name, string mainAttribute, int damage, string attackType, int moveSpeed, string difficulty)
        {
            Name = name;
            Main_attribute = mainAttribute;
            Damage = damage;
            Attack_type = attackType;
            Move_speed = moveSpeed;
            Difficulty = difficulty;
        }

        public override string ToString()
        {
            return $"Name: {Name}; Main_attribute: {Main_attribute}; Damage: {Damage}; Attack_type: {Attack_type}; Move_speed: {Move_speed}; Difficulty: {Difficulty}";
        }
    }


}
