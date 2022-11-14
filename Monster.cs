using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterFightSimulator2._0
{
    internal class Monster
    {
        private MonsterType type;
        private float health;
        private float attack;
        private float defense;
        private float speed;
        public int turnState; // detemines on which turn it attacks (equal or non equal rounds)

        public float Health { get { return health; } }
        public float Speed { get { return speed; } }
        public MonsterType Type { get { return type; } }

        public Monster((MonsterType t, float h, float a, float d, float s) values)
        {
            type = values.t;
            health = values.h;
            attack = values.a;
            defense = values.d;
            speed = values.s;
        }
        internal void Attack(Monster defender)
        {
            float damage = attack - defender.defense;
            if (damage > 0) defender.health -= damage;
            return;
        }
        internal void AttackPlus(Monster defender)
        {
            float damage = attack - defender.defense;
            if (damage > 0)
            {
                defender.health -= damage;
                PrintText(new List<string>() { $"The {this.type} dealt {damage} damge.", $"The {defender.type} remains with {defender.health} health." });
            }
            PrintText(new List<string>() { $"The {this.type} isn't strong enough to deal damage." });
            return;
        }
        internal void PrintText(List<string> text)
        {
            // This method just prints each letter of a text seperatly with a small break time in between
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            foreach (string line in text)
            {
                foreach (char letter in line)
                {
                    Console.Write(letter);
                    Thread.Sleep(100);
                }
                Thread.Sleep(600);
                Console.WriteLine();
            }
        }
    }
    enum MonsterType
    {
        Orc = 1,
        Troll = 2,
        Goblin = 3
    }
}
