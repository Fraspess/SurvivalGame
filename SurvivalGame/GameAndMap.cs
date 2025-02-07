using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SurvivalGame.Hero;

namespace SurvivalGame
{
    public class Game
    {
        int w = 160, h = 40;
        public int level;
        public int[,] gameField;
        int kills;
        Hero hero = new Hero();
        int enemyCount;
        List<Enemy> enemyList = new List<Enemy>();
        List<Recource> recources = new List<Recource>();
        private char[,] backgroundPattern;


        int headX, headY;

        bool quit = false;

        public Game()
        {
            gameField = new int[w + 1, h + 1];
            headX = w / 2;
            headY = h / 2;
            recources.Add(new Recource() { Type = "Gold", Amount = 0 });
            recources.Add(new Recource() { Type = "Wood", Amount = 0 });
            enemyList.Add(new Enemy() { Name = "Monster", Health = 100, AttackPower = 15 });
            enemyList.Add(new Enemy() { Name = "Animal", Health = 50, AttackPower = 5 });
            enemyList.Add((Enemy)new Enemy() { Name = "Dragon of End", Health = 500, AttackPower = 50 });
        }
        public void LoadScreen()


        {
            string[] ss = new string[11];
            ss[0] = " G G G G     A A A A     M       M     E E E E E";
            ss[1] = " G           A       A    M M   M M     E        ";
            ss[2] = " G           A       A    M  M M  M     E        ";
            ss[3] = " G           A       A    M   M   M     E        ";
            ss[4] = " G  G G G    A A A A A    M       M     E E E    ";
            ss[5] = " G      G    A       A    M       M     E        ";
            ss[6] = " G      G    A       A    M       M     E        ";
            ss[7] = " G G G G     A       A    M       M     E E E E E";
            ss[8] = "";
            ss[9] = "                                        BY FRASP";

            Console.ForegroundColor = ConsoleColor.Green;
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(ss[i]);
            }
            Console.ResetColor();

            Console.WriteLine("Press any key to start");
        }

        public void Init()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(w + 1, h + 1);
        }

        public void Next(int x, int y, int n, int p = 0)
        {
            gameField[x, y] = n + p;

            if (gameField[x + 1, y] == n + p)
                Next(x + 1, y, n + 1, p);
            else if (gameField[x - 1, y] == n + p)
                Next(x - 1, y, n + 1, p);
            else if (gameField[x, y - 1] == n + p)
                Next(x, y - 1, n + 1, p);
            else if (gameField[x, y + 1] == n + p)
                Next(x, y + 1, n + 1, p);
            else if (p == 0)
                gameField[x, y] = 0;
        }

        public void KeyboardMoveUpdate()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey().Key;
                int newX = headX;
                int newY = headY;

                switch (key)
                {
                    case ConsoleKey.LeftArrow:
                        newX--;
                        break;
                    case ConsoleKey.RightArrow:
                        newX++;
                        break;
                    case ConsoleKey.UpArrow:
                        newY--;
                        break;
                    case ConsoleKey.DownArrow:
                        newY++;
                        break;
                    case ConsoleKey.Spacebar:
                        Attack();
                        return;
                    case ConsoleKey.Escape:
                        quit = true;
                        break;
                }

                if (newX > 0 && newX < w && newY > 0 && newY < h)
                {
                    switch (gameField[newX, newY])
                    {
                        case 20000:
                            Console.Write('#');
                            break;
                        case -3: 
                            recources.First().Amount += 10;
                            gameField[newX, newY] = 0;
                            headX = newX;
                            headY = newY;
                            break;
                        case 0: 
                            headX = newX;
                            headY = newY;
                            break;
                            

                    }
                }
            }
        }


        private void Attack()
        {
            Random rnd = new Random();
            for (int dx = -1; dx <= 1; dx++)
            {
                for (int dy = -1; dy <= 1; dy++)
                {
                    int targetX = headX + dx;
                    int targetY = headY + dy;

                    if (targetX > 0 && targetX < w && targetY > 0 && targetY < h)
                    {
                        if (gameField[targetX, targetY] == -1)
                        {
                            if (hero.Strength > enemyList.First().Health)
                            {
                                gameField[targetX, targetY] = 0;
                                kills++;
                                recources.First().Amount += 5;
                            }
                            else
                            {
                                hero.Health -= 15;
                                enemyList.First().Health = enemyList.First().Health - hero.Strength;
                                hero.Health = hero.Health + 50;
                            }
                        }
                        else if (gameField[targetX, targetY] == -2)
                        {
                            int health = 0;
                            foreach (var enemy in enemyList)
                            {
                                if (enemy.Name == "Animal")
                                {
                                    health = enemy.Health;
                                }
                            }
                            if (hero.Strength > health)
                            {
                                gameField[targetX, targetY] = 0;
                                kills++;
                                recources.First().Amount += 2;
                                enemyCount--;
                                hero.Health = hero.Health + 50;
                            }
                            else
                            {
                                hero.Health -= 5;
                                health = health - hero.Strength;
                            }
                        }
                        else if (gameField[targetX,targetY] == -4)
                        {
                            int health = 0;
                                foreach(var enemy in enemyList)
                            {
                                if(enemy.Name == "Dragon of End")
                                {
                                    health = enemy.Health;
                                }
                            }
                            if (hero.Strength >health )
                            {
                                gameField[targetX, targetY] = 0;
                                kills++;
                                recources.First().Amount += 5;
                                hero.Health += 50;
                            }
                            else
                            {
                                hero.Health -= 15;
                                health = health - hero.Strength;
                            }
                        }
                    }
                }
            }
        }


                    // void PrintGameField()
                    // {
                    //     StringBuilder frame = new StringBuilder();

                    //     // Add status line at the top
                    //     frame.AppendLine($"Level:{level} HP:{hero.Health} Gold:{recources.First().Amount}");

                    //     // Draw the game field
                    //     for (int i = 0; i <= h; i++)
                    //     {
                    //         for(int j = 0; j <= w; j++)
                    //         {
                    //             char displayChar = ' ';

                    //             if (j == headX && i == headY)
                    //             {
                    //                 displayChar = '@';  // Hero position
                    //             }
                    //             else
                    //             {
                    //                 switch (gameField[j,i])
                    //                 {
                    //                     case 0: displayChar = ' '; break;
                    //                     case -1: displayChar = '$'; break;
                    //                     case -2: displayChar = 'A'; break;  // Animal
                    //                     case 20000: displayChar = '#'; break;  // Wall
                    //                     default: displayChar = ' '; break;
                    //                 }
                    //             }
                    //             frame.Append(displayChar);
                    //         }
                    //         frame.AppendLine();
                    //     }

                    public void PrintGameField()
        {
            Random rnd = new Random();
            StringBuilder frame = new StringBuilder();

            frame.AppendLine($"Level:{level} HP:{hero.Health} Gold:{recources.First().Amount}");

            for (int i = 0; i <= h; i++)
            {
                for (int j = 0; j <= w; j++)
                {
                    char displayChar;

                    if (j == headX && i == headY)
                    {
                        displayChar = 'H';  
                    }
                    else if (gameField[j, i] == 20000)
                    {
                        displayChar = '#';  
                    }
                    else if (gameField[j, i] == -1)
                    {
                        displayChar = 'M';  
                    }
                    else if (gameField[j, i] == -2)
                    {
                        displayChar = 'A';  
                    }
                    else if (gameField[j,i] == -4)
                    {
                        displayChar = 'D';
                    }
                    else
                    {
                        displayChar = '.';
                    }
                    frame.Append(displayChar);
                }
                frame.AppendLine();
            }

            // Print the entire frame at once
            Console.SetCursorPosition(0, 0);
            Console.Write(frame.ToString());
        }










        public void Load(int level)
        {
            headX = w / 2;
            headY = h / 2;
            gameField = new int[w + 1, h + 1];
            gameField[headX, headY] = 1;
            Random rnd = new Random();
            if(level == 10)
            {
                int x = rnd.Next(2, w - 2);
                int y = rnd.Next(2, h - 2);
                gameField[x, y] = -4;  
            }
            enemyCount = level + 1;
            for (int i = 0; i < enemyCount; i++)
            {
                int x = rnd.Next(2, w - 2);
                int y = rnd.Next(2, h - 2);
                if (gameField[x, y] == 0)
                {
                    gameField[x, y] = -1;
                }
            }
            for (int i = 0; i < enemyCount; i++)
            {
                int x = rnd.Next(2, w - 2);
                int y = rnd.Next(2, h - 2);
                if (gameField[x, y] == 0)
                {
                    gameField[x, y] = -2;
                }
            }


            for (int i = 0; i < level * 2; i++)
            {
                int x = rnd.Next(2, w - 2);
                int y = rnd.Next(2, h - 2);
                if (gameField[x, y] == 0)
                {
                    gameField[x, y] = -3;
                }
            }
                
            for (int i = 0; i <= w; i++)
            {
                gameField[i, 0] = 20000;
                gameField[i, h] = 20000;
            }
            for (int i = 0; i < h; i++)
            {
                gameField[0, i] = 20000;
                gameField[w, i] = 20000;
            }
        }


        public void StartGame()
        {
            while (true)
            {
                Init();
                LoadScreen();
                Console.ReadKey();
                Console.Clear();

                level = 10;
                Load(level);

                while (!quit)
                {
                    if (hero.Health <= 0)
                    {
                        Console.Clear();
                        Console.WriteLine("Game Over! Your hero has fallen!");
                        Console.WriteLine($"Final Score: Level {level}, Gold: {recources.First().Amount}");
                        Console.WriteLine("Press any key to start a new game...");
                        Console.ReadKey();
                        break; 
                    }

                    KeyboardMoveUpdate();
                    PrintGameField();

                    bool allEnemiesDefeated = true;
                    for (int i = 0; i <= w; i++)
                    {
                        for (int j = 0; j <= h; j++)
                        {
                            if (gameField[i, j] == -1 || gameField[i, j] == -2 || gameField[i, j] == -4)
                            {
                                allEnemiesDefeated = false;
                                break;
                            }
                        }
                    }

                    if (allEnemiesDefeated)
                    {
                        level++;
                        Console.Clear();
                        Console.WriteLine($"Level {level - 1} Complete!");
                        Console.WriteLine("Press any key to continue...");
                        Console.ReadKey();
                        Console.Clear();

                        if (level > 10)
                        {
                            Console.WriteLine("Congratulations! You've defeated the Dragon and won the game!");
                            Thread.Sleep(3000);
                            quit = true;
                        }
                        else
                        {
                            Load(level);
                        }
                    }

                    Thread.Sleep(100);
                }
                quit = false;
            }
        }
    





            

           
        }
        
        
    }
