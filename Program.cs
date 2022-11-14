using System.Runtime.CompilerServices;

namespace MonsterFightSimulator2._0
{
    internal class Program
    {
        private static List<MonsterType> monsterTypes = new List<MonsterType>() { MonsterType.Orc, MonsterType.Troll, MonsterType.Goblin };
        static void Main(string[] args)
        {
            Monster monsterOne;
            Monster monsterTwo;
            int rounds = 0;
            int roundLimit = 100;
            bool gameLoop = true;

            #region gameLoop
            PrintText(new List<string>() { "Dear Traveler, welcome to the Arena of Beasts.", 
                "You will be able to choose two monsters which will fight against each other.",
                "You assign each monster its attributes.",
                "The monster with the higher speed will attack first and each attack counts as one round.",
                "The fight will stop when one monster is dead or the round limit is reached."});
            Thread.Sleep(500);
            Console.Clear();
            while (gameLoop)
            {
                CreateCharacter("first", out monsterOne);
                CreateCharacter("second", out monsterTwo);
                while(CheckState(monsterOne, monsterTwo, rounds, roundLimit))
                {
                    Fight(monsterOne, monsterTwo, ref rounds);
                }
                PrintText("Do you want to play another round? y/n");
                gameLoop = CheckInputYN(Console.ReadLine());
            }
            PrintText(new List<string>() { "Traveler, good luck on your journey.", "We'll meet again when fate decides so."});
            #endregion
        }
        internal static void CreateCharacter(string character, out Monster monster)
        {
            (MonsterType type, float hp, float ap, float dp, float s) monsterValues;

            PrintText($"Choose your {character} monster:");
            for (int i = 0; i < monsterTypes.Count; i++) PrintText($"{i + 1}. {monsterTypes[i]}");
            CheckInput(Console.ReadLine(), (1, monsterTypes.Count), out float type);
            monsterValues.type = (MonsterType)type;
            monsterTypes.Remove((MonsterType)type);

            PrintText(new List<string>() { "Please set the value of its attributes. ","Please use only values between 1 and 100:" });
            PrintText("Health: ", false);
            CheckInput(Console.ReadLine(), (1, 100), out monsterValues.hp);
            PrintText("Attack: ", false);
            CheckInput(Console.ReadLine(), (1, 100), out monsterValues.ap);
            PrintText("Defense: ", false);
            CheckInput(Console.ReadLine(), (1, 100), out monsterValues.dp);
            PrintText("Speed: ", false);
            CheckInput(Console.ReadLine(), (1, 100), out monsterValues.s);
            monster = new Monster(monsterValues);
            Thread.Sleep(500);
            Console.Clear();
        }
        internal static void Fight(Monster mOne, Monster mTwo, ref int rounds)
        {
            if(rounds == 0)
            {
                if(mOne.Speed < mTwo.Speed)
                {
                    mOne.turnState = 1;
                    mTwo.turnState = 0;
                }
                else
                {
                    mOne.turnState = 0;
                    mTwo.turnState = 1;
                }
            }
            if ((rounds % 2) == mOne.turnState) mOne.Attack(mTwo);
            else if ((rounds % 2) == mTwo.turnState) mTwo.Attack(mOne);
            rounds++;
        }
        internal static bool CheckState(Monster mOne, Monster mTwo, int rounds, int roundLimit)
        {
            // Check if one of the monster's is dead, if not return true so the gameloop continue's
            // If they still fight after a set amount of rounds it will be a draw
            // If one is dead the other will be declared as the winner
            if (mOne.Health <= 0 || mTwo.Health <= 0) 
            {
                if (rounds == roundLimit) PrintText(new List<string>() { "It was a brutal fight.", "But sadly we can't annoucne a winner.", "Both monsters were on equal terms." });
                else if (mOne.Health > 0 && mTwo.Health <= 0) PrintText(new List<string>() { $"After {rounds} rounds a winner was decide.", $"The glorious {mOne.Type} is the winner of this match.", $"The {mOne.Type} won with {mOne.Health} health left."});
                else if (mOne.Health <= 0 && mTwo.Health > 0) PrintText(new List<string>() { $"After {rounds} rounds a winner was decide.", $"The glorious {mTwo.Type} is the winner of this match.", $"The {mTwo.Type} won with {mTwo.Health} health left."});
                return false;
            }
            return true;
        }
        internal static bool CheckInputYN(string Input)
        {
            List<string> yes = new List<string>();
            List<string> no = new List<string>();
            while (true)
            {
                if (yes.Contains(Input)) return true;
                else if (no.Contains(Input)) return false;
                PrintText(new List<string>() { "That wasn't an acceptable answer.", "Please try again." });
                Input = Console.ReadLine();
            }
            return false;
        }
        internal static void CheckInput(string Input, (float min, float max) range, out float output)
        {
            while (true)
            {
                if (float.TryParse(Input, out float input))
                {
                    if (range.min <= input || input <= range.max)
                    {
                        output = input;
                        return;
                    }
                }
                PrintText(new List<string>() { "Your input was not acceptable.", "Please enter a valid number."});
                Input = Console.ReadLine();
            }
        }
        internal static void PrintText(List<string> text)
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
        internal static void PrintText(string text, bool newLine = true)
        {
            // This method just prints each letter of a text seperatly with a small break time in between
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            foreach (char letter in text)
            {
                Console.Write(letter);
                Thread.Sleep(100);
            }
            Thread.Sleep(600);
            if(newLine) Console.WriteLine();
        }
    }
}