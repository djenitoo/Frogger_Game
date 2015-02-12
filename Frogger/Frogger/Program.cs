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

        private static void DrawCars(List<Vehicle> trucks, List<Vehicle> carTwo, List<Vehicle> carThree, List<Vehicle> carFour, List<Vehicle> carFive)
        {

            

            #region MoveTruck
            for (int i = 0; i < trucks.Count; i++)
            {
                PrintVehicles(trucks[i].x, trucks[i].y, trucks[i].color, trucks[i].speed, trucks[i].ch);
                trucks[i] = Temp(trucks[i], i, '-');
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
                carTwo[i] = Temp(carTwo[i], i, '+');
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
                carThree[i] = Temp(carThree[i], i, '-');
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
                carFour[i] = Temp(carFour[i], i, '+');
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
                carFive[i] = Temp(carFive[i], i, '-');
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

        private static Vehicle Temp(Vehicle item, int i, char ch)
        {
            Vehicle temp = item;
            if (ch == '-')
            {
                temp.x = item.x - 1;
                if (temp.x < 0)
                {
                    temp.x = width - 1;
                }
            }
            else
            {
                temp.x = item.x + 1;
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

            #region Inicialize Trucks
            List<Vehicle> trucks = new List<Vehicle>();

            Vehicle truckOne = new Vehicle();
            truckOne.x = xOfTruckOne;
            truckOne.y = y;
            truckOne.ch = vehicles[0];
            truckOne.color = ConsoleColor.Yellow;
            truckOne.speed = 20;
            trucks.Add(truckOne);

            Vehicle truckTwo = new Vehicle();
            truckTwo.x = xOfTruckOne + 15;
            truckTwo.y = truckOne.y;
            truckTwo.ch = truckOne.ch;
            truckTwo.speed = truckOne.speed;
            truckTwo.color = ConsoleColor.Red;
            trucks.Add(truckTwo);
            #endregion

            #region Inicialize CarTwo
            List<Vehicle> carTwo = new List<Vehicle>();

            Vehicle tempOne = new Vehicle();
            tempOne.x = xOfCarTwo;
            tempOne.y = y + 3;
            tempOne.ch = vehicles[1];
            tempOne.color = ConsoleColor.Green;
            tempOne.speed = 20;
            carTwo.Add(tempOne);

            Vehicle tempTwo = new Vehicle();
            tempTwo.x = xOfCarTwo - 10;
            tempTwo.y = tempOne.y;
            tempTwo.ch = tempOne.ch;
            tempTwo.color = ConsoleColor.DarkMagenta;
            tempTwo.speed = tempOne.speed;
            carTwo.Add(tempTwo);

            Vehicle tempThree = new Vehicle();
            tempThree.x = xOfCarTwo - 20;
            tempThree.y = tempOne.y;
            tempThree.ch = tempOne.ch;
            tempThree.color = ConsoleColor.DarkCyan;
            tempThree.speed = tempOne.speed;
            carTwo.Add(tempThree);
            #endregion

            #region Inicialize CarThree
            List<Vehicle> carThree = new List<Vehicle>();

            Vehicle temp1 = new Vehicle();
            temp1.x = xOfcarThree;
            temp1.y = y + 6;
            temp1.ch = vehicles[2];
            temp1.color = ConsoleColor.DarkBlue;
            temp1.speed = 20;
            carThree.Add(temp1);

            Vehicle temp2 = new Vehicle();
            temp2.x = xOfcarThree + 10;
            temp2.y = temp1.y;
            temp2.ch = temp1.ch;
            temp2.color = ConsoleColor.DarkGray;
            temp2.speed = temp1.speed;
            carThree.Add(temp2);

            Vehicle temp3 = new Vehicle();
            temp3.x = xOfcarThree + 20;
            temp3.y = temp1.y;
            temp3.ch = temp1.ch;
            temp3.color = ConsoleColor.DarkYellow;
            temp3.speed = temp1.speed;
            carThree.Add(temp3);
            #endregion

            #region Inicialize CarFour
            List<Vehicle> carFour = new List<Vehicle>();

            Vehicle temp11 = new Vehicle();
            temp11.x = xOfcarFour;
            temp11.y = y + 9;
            temp11.ch = vehicles[3];
            temp11.color = ConsoleColor.Blue;
            temp11.speed = 20;
            carFour.Add(temp11);

            Vehicle temp22 = new Vehicle();
            temp22.x = xOfcarFour - 10;
            temp22.y = temp11.y;
            temp22.ch = temp11.ch;
            temp22.color = ConsoleColor.DarkRed;
            temp22.speed = temp11.speed;
            carFour.Add(temp22);

            Vehicle temp33 = new Vehicle();
            temp33.x = xOfcarFour - 20;
            temp33.y = temp11.y;
            temp33.ch = temp11.ch;
            temp33.color = ConsoleColor.Gray;
            temp33.speed = temp11.speed;
            carFour.Add(temp33);
            #endregion

            #region Inicialize CarFive
            List<Vehicle> carFive = new List<Vehicle>();

            Vehicle tempf1 = new Vehicle();
            tempf1.x = xOfcarFive;
            tempf1.y = y + 12;
            tempf1.ch = vehicles[4];
            tempf1.color = ConsoleColor.Magenta;
            tempf1.speed = 20;
            carFive.Add(tempf1);

            Vehicle tempf2 = new Vehicle();
            tempf2.x = xOfcarFive + 10;
            tempf2.y = tempf1.y;
            tempf2.ch = tempf1.ch;
            tempf2.color = ConsoleColor.Green;
            tempf2.speed = tempf1.speed;
            carFive.Add(tempf2);

            Vehicle tempf3 = new Vehicle();
            tempf3.x = xOfcarFive + 20;
            tempf3.y = tempf1.y;
            tempf3.ch = tempf1.ch;
            tempf3.color = ConsoleColor.Cyan;
            tempf3.speed = tempf1.speed;
            carFive.Add(tempf3);
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

                

                Thread.Sleep(100);
            }
        }

        

    }
}
