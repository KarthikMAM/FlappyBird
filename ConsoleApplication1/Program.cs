using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication24
{
    class Program
    {
        static int scores;

        void pipes(int x, int y)
        {
            if (x <= 75 && x > 0)
            {
                for (int i = 0; i <= y; i++)
                {
                    Console.SetCursorPosition(x, i);
                    Console.Write("|");
                }

                for (int i = y + 9; i <= 40; i++)
                {
                    Console.SetCursorPosition(x, i);
                    Console.Write("|");
                }
            }
            for (int j = 0; j < 6; j++)
            {
                if (x <= 74 - j && x > -(j + 1))
                {
                    for (int i = 0; i < y; i++)
                    {
                        Console.SetCursorPosition(x + j + 1, i);
                        Console.Write(" ");
                    }
                    Console.SetCursorPosition(x + j + 1, y + 1);
                    Console.Write("-");
                    Console.SetCursorPosition(x + j + 1, y + 8);
                    Console.Write("-");
                    for (int i = y + 9; i <= 40; i++)
                    {
                        Console.SetCursorPosition(x + j + 1, i);
                        Console.Write(" ");
                    }
                }
            }
            if (x <= 69 && x > -7)
            {
                for (int i = 0; i <= y; i++)
                {
                    Console.SetCursorPosition(x + 7, i);
                    Console.Write("|");
                }

                for (int i = y + 9; i <= 40; i++)
                {
                    Console.SetCursorPosition(x + 7, i);
                    Console.Write("|");
                }
            }
            for (int i = 0; i < 100; i++)
            {
                Console.SetCursorPosition(i, 41);
                {
                    Console.WriteLine("=");
                }
            }

            Console.SetCursorPosition(10, 43);
            Console.Write("Score : " + scores);
        }

        void displayer(ref int x, ref int y, ref int flag)
        {
            if (x > -7)
            {
                new Program().pipes(x, y);
                x--;
                flag = 1;
            }
            else
            {
                x = 75;
                flag = 0;
            }
        }

        static int game_over(int a, int b, int x, int y)
        {
            for (int i = 0; i <= y + 1; i++)
            {
                for (int j = x - 1; j < x + 8; j++)
                {
                    if (b == i && a == j)
                    {
                        Console.WriteLine("GAME OVER");
                        return 0;
                    }
                }
            }
            for (int i = y + 8; i < 40; i++)
            {
                for (int j = x - 1; j < x + 8; j++)
                {
                    if (b == i && a == j)
                    {
                        Console.WriteLine("GAME OVER");
                        return 0;
                    }
                }
            }
            if (b == 40)
            {
                Console.WriteLine("GAME OVER");
                return 0;
            }
            return 1;
        }

        static void window_maker()
        {
            Console.Title = "Flappy Smiley by KARTHIK M A M";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.Clear();
            Console.SetWindowSize(100, 50);
        }

        static void Main(string[] args)
        {
            window_maker();
            scores = 0;
            int[] x = { 75, 100, 125 };
            int[] y = { 0, 0, 0 };
            int[] flag = { 0, 0, 0 };
            int a = 10;
            Random randomizer = new Random();
            Console.SetCursorPosition(20, a);
            Console.WriteLine("{0}\t PRESS ANY SPACE TO FLY UP.....", (char)2);
        y:
            {
                char c = Console.ReadKey(true).KeyChar;
                if (c != ' ')
                {
                    goto y;
                }
                for (int i = 0; true; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (flag[j] == 0)
                        {
                            y[j] = randomizer.Next(15, 30);
                        }
                        new Program().displayer(ref x[j], ref y[j], ref flag[j]);
                        if (game_over(20, a, x[j], y[j]) == 0)
                        {
                            for (int d = 0; d < 3; d++)
                            {
                                {
                                    Console.Clear();
                                    Console.BackgroundColor = ConsoleColor.Yellow;
                                    Console.Clear();
                                    for (int k = 0; k < 3; k++)
                                    {
                                        new Program().pipes(x[k], y[k]);
                                    }
                                    Console.Beep(500, 230);
                                    //System.Threading.Thread.Sleep(100);
                                }
                                {
                                    Console.Clear();
                                    Console.BackgroundColor = ConsoleColor.Cyan;
                                    Console.Clear();
                                    for (int k = 0; k < 3; k++)
                                    {
                                        new Program().pipes(x[k], y[k]);
                                    }
                                    Console.Beep(1600, 230);
                                    //System.Threading.Thread.Sleep(100);
                                }
                                {
                                    Console.Clear();
                                    Console.BackgroundColor = ConsoleColor.Gray;
                                    Console.Clear();
                                    for (int k = 0; k < 3; k++)
                                    {
                                        new Program().pipes(x[k], y[k]);
                                    }
                                    Console.Beep(900, 230);
                                    //System.Threading.Thread.Sleep(100);
                                }
                                {
                                    Console.Clear();
                                    Console.BackgroundColor = ConsoleColor.Red;
                                    Console.Clear();
                                    for (int k = 0; k < 3; k++)
                                    {
                                        new Program().pipes(x[k], y[k]);
                                    }
                                    Console.Beep(1000, 230);
                                    //System.Threading.Thread.Sleep(100);
                                }
                            }
                            Console.SetCursorPosition(42, 43);
                            Console.WriteLine("GAME OVER !!! YOU LOST");
                            goto x;
                        }
                        else
                        {
                            if (x[j] == 14)
                            {
                                scores++;
                            }
                        }
                        Console.SetCursorPosition(20, a);
                        Console.Write((char)2);
                        c = 'a';
                    }
                    Task.Factory.StartNew(() => c = Console.ReadKey(true).KeyChar).Wait(TimeSpan.FromMilliseconds(200));
                    if (c == ' ')
                    {
                        if (a > 2)
                            a = a - 1;
                    }
                    else
                    {
                        if (a < 40)
                            a = a + 1;
                    }
                    Console.Clear();
                }
            }
        x:
            {
                System.Threading.Thread.Sleep(10000);
            }
        }
    }
}