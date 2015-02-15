using System;
using System.Collections.Generic;
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

        private static void PrintLogo()
        {
            string shift = "    ";
            Console.WriteLine(shift + "       ___ __ ");
            Console.WriteLine(shift + "     _{___{__}\\");
            Console.WriteLine(shift + "   {_}      `\\)");
            Console.WriteLine(shift + "  {_}        `            _.-''''--.._");
            Console.WriteLine(shift + "  {_}                    //'.--.  \\___`.");
            Console.WriteLine(shift + "   { }__,_.--~~~-~~~-~~-::.---. `-.\\  `.)");
            Console.WriteLine(shift + "    `-.{_{_{_{_{_{_{_{_//  -- 8;=- `");
            Console.WriteLine(shift + "     `-:,_.:,_:,_:,.`\\._ ..'=- , ");
            Console.WriteLine(shift + "         // // // //`-.`\\`   .-'/");
            Console.WriteLine(shift + "        << << << <<    \\ `--'  /----)");
            Console.WriteLine(shift + "         ^  ^  ^  ^     `-.....--'''");

            Console.WriteLine("      ____                  _ ");
            Console.WriteLine("     / __/______  _______  (_)__  ___  ___ ____");
            Console.WriteLine("    _\\ \\/ __/ _ \\/ __/ _ \\/ / _ \\/ _ \\/ -_) __/");
            Console.WriteLine("   /___/\\__/\\___/_/ / .__/_/\\___/_//_/\\__/_/");
            Console.WriteLine("                   /_/");

            Thread.Sleep(1000);
            Console.Clear();
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
