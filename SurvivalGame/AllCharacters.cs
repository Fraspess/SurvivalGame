using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurvivalGame
{
    public class Hero
    {

        public static int headX { get; set; } = 0;
        public static int headY { get; set; } = 0;

        public int Health { get; set; } = 100;
        public int Strength { get; set; } = 100;

        public int Gold { get; set; } = 5;
    }
    




    /*        public static void KeyboardMoveUpdate()
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey().Key;
                    switch (key)
                    {
                        case ConsoleKey.LeftArrow:
                            headX--;
                            break;
                        case ConsoleKey.UpArrow:
                            headY--;
                            break;
                        case ConsoleKey.RightArrow:
                            headX++;
                            break;
                        case ConsoleKey.DownArrow:
                            headY++;
                            break;

                    }
                }
            }*/


    public class Enemy
    {
        public string Name { get; set; }
        public int Health { get; set; }

        public int AttackPower { get; set; }

        public Enemy()
        {
            // Enemy Monster = new Enemy() { Name = "Monster", Health = 100, AttackPower = 15 };
            // Enemy Animal = new Enemy() { Name = "Animal", Health = 50, AttackPower = 5 };
            // Enemy FinalBoss = new Enemy() { Name = "Dragon of End", Health = 500, AttackPower = 50 };
        }

    }



        public class Recource
        {
            public string Type { get; set; }
            public int Amount { get; set; }

            // public Recource(int amount = 0)
            // {
            //     // Recource Gold = new Recource(){ Type = "Gold", Amount = amount};
            //     // Recource Wood = new Recource() { Type = "Iron", Amount = amount };
            // }
            public Recource()
            {
                // Recource Gold = new Recource(){Type = "Gold", Amount = 0};
                // Recource Wood = new Recource(){Type = "Wood", Amount = 0};
            }
        }

        

        

 
    }

