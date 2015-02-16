using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Frogger
{

    class Program
    {
        static int width = 50;
        public int points = 0;
        public int time = 0;
        static int y = 15;
        static int xOfTruckOne = width - 1;
        static int xOfcarThree = width - 10;
        static int xOfcarFour = 10;
        static int xOfCarTwo = width / 2 + 5;
        static int xOfcarFive = width - 5;

        private static void CustomizeConsole()
        {
            Console.BufferWidth = Console.WindowWidth = width;
            Console.BufferHeight = Console.WindowHeight;
            Console.OutputEncoding = Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;

        }

        private static string[] logo = 
            {
                "     ___ __                            ",
                "   _{___{__}\\                          ",
                " {_}      `\\)                          ",
                "{_}        `            _.-''''--.._   ",
                "{_}                    //'.--.  \\___`. ",
                " { }__,_.--~~~-~~~-~~-::.---. `-.\\  `.)",
                "  `-.{_{_{_{_{_{_{_{_//  -- 8;=- `     ",
                "   `-:,_.:,_:,_:,.`\\._ ..'=- ,         ",
                "       // // // //`-.`\\`   .-'/        ",
                "      << << << <<    \\ `--'  /----)    ",
                "       ^  ^  ^  ^     `-.....--'''     ",
            };

        private static string[] title = 
        {
            "   ____                  _                  ",
            "  / __/______  _______  (_)__  ___  ___ ____",
            " _\\ \\/ __/ _ \\/ __/ _ \\/ / _ \\/ _ \\/ -_) __/",
            "/___/\\__/\\___/_/ / .__/_/\\___/_//_/\\__/_/   ",
            "                /_/                         ",
        };
        private const ConsoleColor TITLE_COLOR = ConsoleColor.White;
        private const ConsoleColor LOGO_COLOR = ConsoleColor.Red;
        private static void PrintName()
        {
            Console.ForegroundColor = TITLE_COLOR;

            if (title[0].Length <= Console.WindowWidth)
            {
                foreach (var str in title)
                {
                    Console.WriteLine(new string(' ', ((Console.WindowWidth - title[0].Length) / 2)) + str);
                }

            }
        }
        private static void PrintLogo()
        {
            Console.ForegroundColor = LOGO_COLOR;


            for (int col = 0; col <= logo[0].Length + ((Console.WindowWidth - logo[0].Length) / 2); col++)
            {
                Console.Clear();
                foreach (var str in logo)
                {
                    if (col < logo[0].Length)
                    {
                        Console.WriteLine(str.Substring(logo[0].Length - col - 1));
                    }
                    else
                    {
                        Console.WriteLine(new string(' ', col - logo[0].Length) + str);
                    }
                }
                Thread.Sleep(25);
            }
        }

        private static void hideLogo()
        {
            for (int row = 1; row <= logo.Length; row++)
            {
                Console.Clear();
                Console.ForegroundColor = LOGO_COLOR;
                for (int row2 = row; row2 < logo.Length; row2++)
                {
                    Console.WriteLine(new string(' ', ((Console.WindowWidth - title[0].Length) / 2)) + logo[row2]);
                }
                Console.ForegroundColor = TITLE_COLOR;
                foreach (var str in title)
                {
                    Console.WriteLine(new string(' ', ((Console.WindowWidth - title[0].Length) / 2)) + str);
                }
                Thread.Sleep(25);
            }
        }

        private static void MainMenu()
        {
            Console.WriteLine("\n\n");
            Console.SetCursorPosition(20, 9);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("MAIN MENU:\n");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\tNEW Game - press [1]\n");
            Console.WriteLine("\tHigh SCORES - press [2]\n");
            Console.WriteLine("\tPoint Table - press [3]\n\n\n");

            int inputChoice = int.Parse(Console.ReadLine());
            if (inputChoice == 1)
            {
                //TODO - logic for starting the game
            }
            else if (inputChoice == 2)
            {
                //TODO - read file with player score and sort it decreasing
                try
                {
                    StreamReader readHighScores = new StreamReader(@"..\..\HighScores.txt");
                    using (readHighScores)
                    {
                        PrintName();
                        Console.SetCursorPosition(17, 9);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("- HIGH SCORES -\n");

                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        string highScores = readHighScores.ReadToEnd();
                        int[] scores = highScores.Split(' ').Select(int.Parse).ToArray(); // sync by what to Split
                        Array.Sort(scores);
                        foreach (var score in scores)
                        {
                            Console.WriteLine(score);
                        }

                        //int lineNumber = 0;
                        //string line = readHighScores.ReadLine();
                        //while (line != null)
                        //{
                        //    lineNumber++;
                        //    Console.WriteLine("{0}",line);
                        //    line = readHighScores.ReadLine();
                        //}

                    }
                }
                catch (FileNotFoundException)
                {
                    Console.Error.WriteLine("Cannot find 'HighScores.txt'.");
                }
            }
            else if (inputChoice == 3)
            {
                // print table with ponts / instructions
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("\n\n");
                PrintName();
                Console.SetCursorPosition(17, 9);
                Console.WriteLine("- POINT TABLE -\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\t10 PTS FOR EACH STEP\n\n" +
                    "\t50 PTS FOR EVERY SCORPION");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tARRIVED HOME SAFELY\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\t1000 PTS FOR SAVING SCOTPIONS");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\tINTO FIVE HOMES\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\tPLUS BONUS");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\t10 PTS X REMAINING SECOND\n\n");

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Press [enter] to go BACK");
                Console.ReadLine();
                Console.Clear();
                PrintName();
                MainMenu();
            }
        }

        private static void DrawCars(List<Vehicle> trucks, List<Vehicle> carTwo, List<Vehicle> carThree, List<Vehicle> carFour, List<Vehicle> carFive)
        {

            

            #region MoveTruck
            for (int i = 0; i < trucks.Count; i++)
            {
                PrintVehicles(trucks[i].x, trucks[i].y, trucks[i].color, trucks[i].speed, trucks[i].ch);
                trucks[i] = Temp(trucks[i], i, '-', trucks[i].speed);
                if (i == 0)
                {
                    xOfTruckOne = trucks[i].x;
                }
                //else
                //{
                //    xOfTruckTwo = trucks[i].x;
                //}

            } 
            #endregion

            #region MoveCarTwo
            for (int i = 0; i < carTwo.Count; i++)
            {
                PrintVehiclesRight(carTwo[i].x, carTwo[i].y, carTwo[i].color, carTwo[i].speed, carTwo[i].ch);
                carTwo[i] = Temp(carTwo[i], i, '+', carTwo[i].speed);
                if (i == 0)
                {
                    xOfCarTwo = carTwo[i].x;
                }
            } 
            #endregion

            #region MoveCarThree
            for (int i = 0; i < carThree.Count; i++)
            {
                PrintVehicles(carThree[i].x, carThree[i].y, carThree[i].color, carThree[i].speed, carThree[i].ch);
                carThree[i] = Temp(carThree[i], i, '-', carThree[i].speed);
                if (i == 0)
                {
                    xOfcarThree = carThree[i].x;
                }

            }
            #endregion

            #region MoveCarFour
            for (int i = 0; i < carFour.Count; i++)
            {
                PrintVehiclesRight(carFour[i].x, carFour[i].y, carFour[i].color, carFour[i].speed, carFour[i].ch);
                carFour[i] = Temp(carFour[i], i, '+', carFour[i].speed);
                if (i == 0)
                {
                    xOfcarFour = carFour[i].x;
                }
            }
            #endregion

            #region MoveCarFive
            for (int i = 0; i < carFive.Count; i++)
            {
                PrintVehicles(carFive[i].x, carFive[i].y, carFive[i].color, carFive[i].speed, carFive[i].ch);
                carFive[i] = Temp(carFive[i], i, '-', carFive[i].speed);
                if (i == 0)
                {
                    xOfcarFive = carFive[i].x;
                }

            }
            #endregion

        }

        private static void PrintVehicles(int x, int y, ConsoleColor color, int speed, char[,] ch)
        {
            for (int j = 0; j < ch.GetLength(0); j++)
            {
                for (int i = 0; i < ch.GetLength(1); i++)
                {

                    //if (x + i < Console.WindowWidth)
                    //{
                    Console.SetCursorPosition((x + i) % width, y + j);
                    Console.ForegroundColor = color;
                    Console.WriteLine(ch[j, i]);
                    //}

                }
            }
        }

        private static void PrintVehiclesRight(int x, int y, ConsoleColor color, int speed, char[,] ch)
        {

            for (int j = 0, p = 0; j < ch.GetLength(0); j++, p++)
            {

                for (int i = ch.GetLength(1) - 1, q = 0; i >= 0; i--, q++)
                {

                    //if (x + i < Console.WindowWidth)
                    //{
                    Console.SetCursorPosition(((x - q) + width) % width, y + p);
                    Console.ForegroundColor = color;
                    Console.WriteLine(ch[j, i]);
                    //}

                }
            }
        }

        private static Vehicle Temp(Vehicle item, int i, char ch, int speed)
        {
            Vehicle temp = item;
            if (ch == '-')
            {
                temp.x = item.x - speed;
                if (temp.x < 0)
                {
                    temp.x = width - 1;
                }
            }
            else
            {
                temp.x = item.x + speed;
                if (temp.x >= width)
                {
                    temp.x = 0;
                }
            }

            return temp;
        }



        static void Main(string[] args)
        {
            CustomizeConsole();

            PrintLogo();
            PrintName();

            Thread.Sleep(1000);
            hideLogo();
            MainMenu();

            #region Inicialize Vehicles

            #region Vehicles List of Forms
            List<char[,]> vehicles = new List<char[,]>();
            char[,] cha1 = {
                            {'\u258C', '\u2588', '\u2588', '\u2588', '\u2588'}, 
                            {'\u258C', '\u2588', '\u2588', '\u2588', '\u2588'}
                         };
            vehicles.Add(cha1);

            char[,] cha2 = {
                            {'\u2592', '\u2592', '\u2580'}, 
                            {'\u2592', '\u2592', '\u2584'}
                          };
            vehicles.Add(cha2);

            char[,] cha3 = {
                        {'\u258C', '\u2593', '\u2592'}, 
                        {'\u258C', '\u2593', '\u2592'}
                  };
            vehicles.Add(cha3);

            char[,] cha4 = {
                            {'\u2588', '\u2588', '\u2584'}, 
                            {'\u2588', '\u2588', '\u2580'}
                          };
            vehicles.Add(cha4);

            char[,] cha5 = {
                        {'\u2584', '\u2590', '\u2591', '\u2591'}, 
                        {'\u2580', '\u2590', '\u2591', '\u2591'}
                  };
            vehicles.Add(cha5);

            #endregion

            Vehicle temp1 = new Vehicle();
            Vehicle temp2 = new Vehicle();
            Vehicle temp3 = new Vehicle();

            #region Inicialize Trucks
            List<Vehicle> trucks = new List<Vehicle>();

            temp1 = new Vehicle();
            temp1.x = xOfTruckOne;
            temp1.y = y;
            temp1.ch = vehicles[0];
            temp1.color = ConsoleColor.Yellow;
            temp1.speed = 2;
            trucks.Add(temp1);

            temp2 = new Vehicle();
            temp2.x = xOfTruckOne + 15;
            temp2.y = temp1.y;
            temp2.ch = temp1.ch;
            temp2.speed = temp1.speed;
            temp2.color = ConsoleColor.Red;
            trucks.Add(temp2);
            #endregion

            #region Inicialize CarTwo
            List<Vehicle> carTwo = new List<Vehicle>();

            temp1 = new Vehicle();
            temp1.x = xOfCarTwo;
            temp1.y = y + 3;
            temp1.ch = vehicles[1];
            temp1.color = ConsoleColor.Green;
            temp1.speed = 2;
            carTwo.Add(temp1);

            temp2 = new Vehicle();
            temp2.x = xOfCarTwo - 10;
            temp2.y = temp1.y;
            temp2.ch = temp1.ch;
            temp2.color = ConsoleColor.DarkMagenta;
            temp2.speed = temp1.speed;
            carTwo.Add(temp2);

            temp3 = new Vehicle();
            temp3.x = xOfCarTwo - 20;
            temp3.y = temp1.y;
            temp3.ch = temp1.ch;
            temp3.color = ConsoleColor.DarkCyan;
            temp3.speed = temp1.speed;
            carTwo.Add(temp3);
            #endregion

            #region Inicialize CarThree
            List<Vehicle> carThree = new List<Vehicle>();

            temp1 = new Vehicle();
            temp1.x = xOfcarThree;
            temp1.y = y + 6;
            temp1.ch = vehicles[2];
            temp1.color = ConsoleColor.DarkBlue;
            temp1.speed = 2;
            carThree.Add(temp1);

            temp2 = new Vehicle();
            temp2.x = xOfcarThree + 10;
            temp2.y = temp1.y;
            temp2.ch = temp1.ch;
            temp2.color = ConsoleColor.DarkGray;
            temp2.speed = temp1.speed;
            carThree.Add(temp2);

            temp3 = new Vehicle();
            temp3.x = xOfcarThree + 20;
            temp3.y = temp1.y;
            temp3.ch = temp1.ch;
            temp3.color = ConsoleColor.DarkYellow;
            temp3.speed = temp1.speed;
            carThree.Add(temp3);
            #endregion
            
            #region Inicialize CarFour
            List<Vehicle> carFour = new List<Vehicle>();

            temp1 = new Vehicle();
            temp1.x = xOfcarFour;
            temp1.y = y + 9;
            temp1.ch = vehicles[3];
            temp1.color = ConsoleColor.Blue;
            temp1.speed = 2;
            carFour.Add(temp1);

            temp2 = new Vehicle();
            temp2.x = xOfcarFour - 10;
            temp2.y = temp1.y;
            temp2.ch = temp1.ch;
            temp2.color = ConsoleColor.DarkRed;
            temp2.speed = temp1.speed;
            carFour.Add(temp2);

            temp3 = new Vehicle();
            temp3.x = xOfcarFour - 20;
            temp3.y = temp1.y;
            temp3.ch = temp1.ch;
            temp3.color = ConsoleColor.Gray;
            temp3.speed = temp1.speed;
            carFour.Add(temp3);
            #endregion

            #region Inicialize CarFive
            List<Vehicle> carFive = new List<Vehicle>();

            temp1 = new Vehicle();
            temp1.x = xOfcarFive;
            temp1.y = y + 12;
            temp1.ch = vehicles[4];
            temp1.color = ConsoleColor.Magenta;
            temp1.speed = 1;
            carFive.Add(temp1);

            temp2 = new Vehicle();
            temp2.x = xOfcarFive + 10;
            temp2.y = temp1.y;
            temp2.ch = temp1.ch;
            temp2.color = ConsoleColor.Green;
            temp2.speed = temp1.speed;
            carFive.Add(temp2);

            temp3 = new Vehicle();
            temp3.x = xOfcarFive + 20;
            temp3.y = temp1.y;
            temp3.ch = temp1.ch;
            temp3.color = ConsoleColor.Cyan;
            temp3.speed = temp1.speed;
            carFive.Add(temp3);
            #endregion
            
            #endregion

            while (true)
            {
                Console.Clear();
                //Front page
                //+Logo
                //+Name
                //Options (new game, sound, etc.)
                DrawCars(trucks, carTwo, carThree, carFour, carFive);
                //Frog movement
                //Points
                //Time
                //Reading name of player
                //Print name and score in txt file
                //~ colors and some bg forms with unicode chars?
                //exception handling
                //~sound effects



                Thread.Sleep(200);
            }
        }

        

    }
}
