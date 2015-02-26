using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;

namespace Frogger
{
    class Program
    {
        static int width = 69;
        static int height = 55;
        //static bool isCrash = false;
        static int points = 0;
        static int time = 0;
        static int y = 25;
        static int showHide = 0;
        static int xOfTruckOne = width - 1;
        static int xOfcarThree = width - 10;
        static int xOfcarFour = 10;
        static int xOfCarTwo = width / 2 + 5;
        static int xOfcarFive = width - 5;
        static int xOfTreeOne = 10;
        static int yOfTree = 18;
        static int xOfTreeTwo = 20;
        private const ConsoleColor TITLE_COLOR = ConsoleColor.White;
        private const ConsoleColor LOGO_COLOR = ConsoleColor.Red;
        #region Logos
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
        #endregion
        private static string nameOfPlayer = "Jenny";
        static int inputChoice = 0;
        static int secondChoice = 0;
        private static string[] menuItems = { "New Game".ToUpper(), "View High Scores".ToUpper(), "Point Table".ToUpper(), "Exit".ToUpper() };
        private static string[] secMenuItems = { "RETURN", "EXIT" };
        static char[,] turtle2 = {  {'*','*',' ','*','*',' ','*','*'},
                                    {'*','*',' ','*','*',' ','*','*'} };

        static char[,] scorpionForm = { { '╘', '╦', '╛' },
                                        { ' ', '╙', '╜' } };
        static char[,] deadForm = {     { '×', ' ', '×' },
                                        { ' ', '_', ' ' } };
        public static Forms scorpion;
        public static Forms tempCoord;

        private static void CustomizeConsole()
        {
            Console.BufferWidth = Console.WindowWidth = width;
            Console.BufferHeight = Console.WindowHeight = height;
            Console.OutputEncoding = Encoding.Unicode;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.CursorVisible = false;
            Console.Title = "Scorpioner";

        }

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
            int indexForPrint = width / 2 - ("MAIN MENU:".Length / 2);
            Console.SetCursorPosition(indexForPrint, 9);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("MAIN MENU:");

            Console.ForegroundColor = ConsoleColor.Green;
            //Console.WriteLine("\tNEW Game - press [1]\n");
            //Console.WriteLine("\tHigh SCORES - press [2]\n");
            //Console.WriteLine("\tPoint Table - press [3]\n\n\n");
            while (true)
            {
                for (int i = 0; i < 4; i++)
                {
                    Console.SetCursorPosition(width / 2 - (menuItems[i].Length / 2), 10 + i + 1);
                    if (inputChoice == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    }
                    Console.WriteLine(menuItems[i]);
                }
                if (Console.KeyAvailable)
                {
                    for (int i = 0; i < menuItems.Length - 1; i++)
                    {
                        Console.SetCursorPosition(width / 2 - (menuItems[i].Length / 2), 10 + i + 1);
                        if (inputChoice == i)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                        }
                        Console.WriteLine(menuItems[i]);
                    }
                    ConsoleKeyInfo choice = Console.ReadKey();
                    if (choice.Key == ConsoleKey.DownArrow)
                    {
                        inputChoice = (inputChoice + 1) % 4;

                    }
                    if (choice.Key == ConsoleKey.UpArrow)
                    {
                        inputChoice = (inputChoice - 1 + 4) % 4;

                    }
                    if (choice.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            }
            #region Process Input
            //int inputChoice = int.Parse(Console.ReadLine());
            if (inputChoice == 0)
            {
                return;
                //Begin new game
            }
            #region input = 1
            else if (inputChoice == 1)
            {
                try
                {
                    Console.Clear();
                    PrintName();
                    Console.SetCursorPosition((width / 2) - 10, 10);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("- HIGH SCORES -\n");

                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    List<string> playerName = new List<string>();
                    using (StreamReader reader = new StreamReader(@"..\..\HighScores.txt"))
                    {
                        //string highScores = readHighScores.ReadToEnd();
                        //int[] scores = highScores.Split(' ').Select(int.Parse).ToArray(); // sync by what to Split
                        //Array.Sort(scores);
                        //foreach (var score in scores)
                        //{
                        //    Console.WriteLine(score);
                        //}
                        string line = reader.ReadLine();
                        while (line != null)
                        {
                            playerName.Add(line); //Add name and score to the list
                            line = reader.ReadLine();
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
                    while (playerName.Count > 10)
                    {
                        playerName.RemoveAt(playerName.Count - 1);
                    }
                    string newEntry = points + "\t" + nameOfPlayer;
                    playerName.Add(newEntry);
                    string[] playersArr = playerName.ToArray();
                    Array.Sort(playersArr, new AlphanumComparatorFast());
                    for (int i = playersArr.Length - 1, j = 0; i >= 0; i--, j++)
                    {
                        Console.SetCursorPosition((width / 2) - 10, 12 + j);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine(playersArr[i]);
                    }
                    using (var writer = new StreamWriter(@"..\..\HighScores.txt"))
                    {
                        for (int i = playersArr.Length - 1; i >= 0; i--)
                        {
                            writer.WriteLine(playerName[i]); //Writes name and score supposedly
                        }
                    }
                }
                catch (FileNotFoundException)
                {
                    Console.Error.WriteLine("\tCannot find 'HighScores.txt'.");
                }
                //Environment.Exit(0);

                secondChoiseOfMenus();
            }

            #endregion

            #region input 2
            else if (inputChoice == 2)
            {
                // print table with ponts / instructions
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                PrintName();
                Console.SetCursorPosition(20, 9);
                Console.WriteLine("\t- POINT TABLE -\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\t\t10 PTS FOR EACH STEP\n\n" +
                    "\t\t50 PTS FOR EVERY SCORPION");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\t\tARRIVED HOME SAFELY\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\t\t1000 PTS FOR SAVING SCOTPIONS");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\t\tINTO FIVE HOMES\n");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\t\tPLUS BONUS");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\t\t10 PTS X REMAINING SECOND\n\n");

                secondChoiseOfMenus();
            }
            #endregion
            else if (inputChoice == 3)
            {
                //Environment.Exit(0);
                return;
            }
            #endregion
        }

        private static void secondChoiseOfMenus()
        {
            while (true)
            {
                for (int i = 0; i < secMenuItems.Length; i++)
                {
                    if (secondChoice == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    Console.SetCursorPosition(25 + (10 * i), 25);
                    Console.WriteLine(secMenuItems[i]);
                    //Console.ResetColor();
                }
                if (Console.KeyAvailable)
                {
                    for (int i = 0; i < secMenuItems.Length; i++)
                    {
                        if (secondChoice == i)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                        }
                        Console.SetCursorPosition(25 + (10 * i), 25);
                        Console.WriteLine(secMenuItems[i]);
                        //Console.ResetColor();
                    }

                    ConsoleKeyInfo pressedKey = Console.ReadKey();
                    if (pressedKey.Key == ConsoleKey.LeftArrow)
                    {
                        secondChoice = (secondChoice - 1 + 2) % 2;
                    }
                    if (pressedKey.Key == ConsoleKey.RightArrow)
                    {
                        secondChoice = (secondChoice + 1) % 2;
                    }
                    if (pressedKey.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            }

            if (secondChoice == 0)
            {
                Console.Clear();
                PrintName();
                MainMenu();
            }
            else if (secondChoice == 1)
            {
                return;
            }
            //TODO: return to main menu, or exit game
        }

        public class AlphanumComparatorFast : IComparer
        {
            public int Compare(object x, object y)
            {
                string s1 = x as string;
                if (s1 == null)
                {
                    return 0;
                }
                string s2 = y as string;
                if (s2 == null)
                {
                    return 0;
                }

                int len1 = s1.Length;
                int len2 = s2.Length;
                int marker1 = 0;
                int marker2 = 0;

                // Walk through two the strings with two markers.
                while (marker1 < len1 && marker2 < len2)
                {
                    char ch1 = s1[marker1];
                    char ch2 = s2[marker2];

                    // Some buffers we can build up characters in for each chunk.
                    char[] space1 = new char[len1];
                    int loc1 = 0;
                    char[] space2 = new char[len2];
                    int loc2 = 0;

                    // Walk through all following characters that are digits or
                    // characters in BOTH strings starting at the appropriate marker.
                    // Collect char arrays.
                    do
                    {
                        space1[loc1++] = ch1;
                        marker1++;

                        if (marker1 < len1)
                        {
                            ch1 = s1[marker1];
                        }
                        else
                        {
                            break;
                        }
                    } while (char.IsDigit(ch1) == char.IsDigit(space1[0]));

                    do
                    {
                        space2[loc2++] = ch2;
                        marker2++;

                        if (marker2 < len2)
                        {
                            ch2 = s2[marker2];
                        }
                        else
                        {
                            break;
                        }
                    } while (char.IsDigit(ch2) == char.IsDigit(space2[0]));

                    // If we have collected numbers, compare them numerically.
                    // Otherwise, if we have strings, compare them alphabetically.
                    string str1 = new string(space1);
                    string str2 = new string(space2);

                    int result;

                    if (char.IsDigit(space1[0]) && char.IsDigit(space2[0]))
                    {
                        int thisNumericChunk = int.Parse(str1);
                        int thatNumericChunk = int.Parse(str2);
                        result = thisNumericChunk.CompareTo(thatNumericChunk);
                    }
                    else
                    {
                        result = str1.CompareTo(str2);
                    }

                    if (result != 0)
                    {
                        return result;
                    }
                }
                return len1 - len2;
            }
        }

        private static void DrawCars(List<Forms> TreesOne, List<Forms> Turtles,List<Forms> trucks, List<Forms> carTwo, List<Forms> carThree, List<Forms> carFour, List<Forms> carFive)
        {

            #region MoveTrees
            for (int i = 0; i < TreesOne.Count; i++)
            {
                PrintVehiclesRight(TreesOne[i].x, TreesOne[i].y, TreesOne[i].color, TreesOne[i].speed, TreesOne[i].ch, true);
                TreesOne[i] = Temp(TreesOne[i], i, '+', TreesOne[i].speed);
                if (i == 0)
                {
                    xOfTreeOne = TreesOne[i].x;
                }
            }
            #endregion

            #region MoveTurtles
            for (int i = 0; i < Turtles.Count; i++)
            {
                char[,] tempCharArr = new char[2, 8];

                if (i == 5 || i == 2)
                {
                    if (showHide == 5 || showHide == 6 || showHide == 7)
                    {
                        showHide = (showHide + 1) % 8;
                        tempCharArr = Turtles[i].ch;
                        //continue;
                    }
                    else if (showHide >= 1 && showHide <= 4)
                    {
                        tempCharArr = turtle2;
                        showHide = (showHide + 1) % 8;
                    }
                    else if (showHide == 0)
                    {
                        showHide = (showHide + 1) % 8;
                    }
                }
                else
                {
                    tempCharArr = Turtles[i].ch;
                }

                PrintVehicles(Turtles[i].x, Turtles[i].y, Turtles[i].color, Turtles[i].speed, tempCharArr, true);
                Turtles[i] = Temp(Turtles[i], i, '-', Turtles[i].speed);
                if (i == 0)
                {
                    xOfTreeOne = Turtles[i].x;
                }
            }
            #endregion

            #region MoveTruck
            for (int i = 0; i < trucks.Count; i++)
            {
                PrintVehicles(trucks[i].x, trucks[i].y, trucks[i].color, trucks[i].speed, trucks[i].ch, false);
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
                PrintVehiclesRight(carTwo[i].x, carTwo[i].y, carTwo[i].color, carTwo[i].speed, carTwo[i].ch, false);
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
                PrintVehicles(carThree[i].x, carThree[i].y, carThree[i].color, carThree[i].speed, carThree[i].ch, false);
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
                PrintVehiclesRight(carFour[i].x, carFour[i].y, carFour[i].color, carFour[i].speed, carFour[i].ch, false);
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
                PrintVehicles(carFive[i].x, carFive[i].y, carFive[i].color, carFive[i].speed, carFive[i].ch, false);
                carFive[i] = Temp(carFive[i], i, '-', carFive[i].speed);
                if (i == 0)
                {
                    xOfcarFive = carFive[i].x;
                }

            }
            #endregion

        }

        private static void PrintVehicles(int x, int y, ConsoleColor color, int speed, char[,] ch, bool isTreeOrTurtle)
        {
            for (int j = 0; j < ch.GetLength(0); j++)
            {
                for (int i = 0; i < ch.GetLength(1); i++)
                {
                    Console.SetCursorPosition((x + i) % width, y + j);
                    Console.ForegroundColor = color;
                    Console.WriteLine(ch[j, i]);
                    int coordX = (x + i) % width;
                    int coordY = y + j;
                    if (!isTreeOrTurtle && (scorpion.y == coordY || scorpion.y + 1 == coordY) && 
                        (scorpion.x == coordX || scorpion.x + 1 == coordX || scorpion.x + 2 == coordX))
                    {
                        scorpion.ch = deadForm;
                        PrintScorpion(scorpion);
                        Thread.Sleep(500);
                        SetInitialPositionOfScorpion();
                        PrintScorpion(scorpion);
                    }
                }
            }
        }

        private static void PrintVehiclesRight(int x, int y, ConsoleColor color, int speed, char[,] ch, bool isTreeOrTurtle)
        {

            for (int j = 0, p = 0; j < ch.GetLength(0); j++, p++)
            {

                for (int i = ch.GetLength(1) - 1, q = 0; i >= 0; i--, q++)
                {
                    Console.SetCursorPosition(((x - q) + width) % width, y + p);
                    Console.ForegroundColor = color;
                    Console.WriteLine(ch[j, i]);

                    int coordX = ((x - q) + width) % width;
                    int coordY = y + p;
                    if (!isTreeOrTurtle && (scorpion.y == coordY || scorpion.y + 1 == coordY) && 
                        (scorpion.x == coordX || scorpion.x + 1 == coordX || scorpion.x + 2 == coordX))
                    {
                        scorpion.ch = deadForm;
                        PrintScorpion(scorpion);
                        Thread.Sleep(500);
                        SetInitialPositionOfScorpion();
                        PrintScorpion(scorpion);
                    }

                }
            }
        }

        private static Forms Temp(Forms item, int i, char ch, int speed)
        {
            Forms temp = item;
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

            //isCrash = CheckCrash(temp);
            //if (isCrash)
            //{
            //    scorpion.ch = deadForm;
            //    PrintScorpion(scorpion);
            //    Thread.Sleep(500);
            //    SetInitialPositionOfScorpion();
            //    PrintScorpion(scorpion);
            //}
            return temp;
        }

        //private static void DrawTrees(List<Forms> TreesOne, List<Forms> Turtles)
        //{
        //    #region MoveTrees
        //    for (int i = 0; i < TreesOne.Count; i++)
        //    {
        //        PrintVehiclesRight(TreesOne[i].x, TreesOne[i].y, TreesOne[i].color, TreesOne[i].speed, TreesOne[i].ch);
        //        TreesOne[i] = Temp(TreesOne[i], i, '+', TreesOne[i].speed);
        //        if (i == 0)
        //        {
        //            xOfTreeOne = TreesOne[i].x;
        //        }
        //    }
        //    #endregion


        //    #region MoveTurtles
        //    for (int i = 0; i < Turtles.Count; i++)
        //    {
        //        char[,] tempCharArr = new char[2, 8];

        //        if (i == 5 || i == 2)
        //        {
        //            if (showHide == 5 || showHide == 6 || showHide == 7)
        //            {
        //                showHide = (showHide + 1) % 8;
        //                tempCharArr = Turtles[i].ch;
        //                //continue;
        //            }
        //            else if (showHide >= 1 && showHide <= 4)
        //            {
        //                tempCharArr = turtle2;
        //                showHide = (showHide + 1) % 8;
        //            }
        //            else if (showHide == 0)
        //            {
        //                showHide = (showHide + 1) % 8;
        //            }
        //        }
        //        else
        //        {
        //            tempCharArr = Turtles[i].ch;
        //        }

        //        PrintVehicles(Turtles[i].x, Turtles[i].y, Turtles[i].color, Turtles[i].speed, tempCharArr);
        //        Turtles[i] = Temp(Turtles[i], i, '-', Turtles[i].speed);
        //        if (i == 0)
        //        {
        //            xOfTreeOne = Turtles[i].x;
        //        }
        //    }
        //    #endregion
        //}

        
        // M   A   I   N

        static void Main(string[] args)
        {
            scorpion = new Forms();

            #region Inicialize Moving Objects(Cars,Forms, etc.)

            #region List of Moving Forms
            List<char[,]> vehicles = new List<char[,]>();
            char[,] cha1 = {
                            {'┌', '█', '█', '█', '█'}, 
                            {'▀', 'O', '▀', 'O', '▀'}
                         };
            vehicles.Add(cha1);

            char[,] cha2 = {
                            {'▒','▒','▒', '▒', '╖'}, 
                            {'▀','#','▀', '#', '▀'}
                          };
            vehicles.Add(cha2);

            char[,] cha3 = {
                        {' ', '╔','╤', '╤','╤', '╗'}, 
                        {'▀', '@','▀', '▀','@', '▀'}
                  };
            vehicles.Add(cha3);

            char[,] cha4 = {
                            {'█','█', '█','█','╗', ' '}, 
                            {'▀','0', '▀','0','▀', ' '}
                          };
            vehicles.Add(cha4);

            char[,] cha5 = {
                        {' ', '╓', '─', '╖'},
                        {'▀', '@', '▀', '@'}
                  };
            vehicles.Add(cha5);

            char[,] charrie = { {'╓','─','┬','─','┬','─','╖'},
                                {'╙','─','┴','─','┴','─','╜'}};

            char[,] charrie3 = { {'╔','#','#','#','#','#','#','#','#','#','#','#','#', '╗'},
                                 {'╚','#','#','#','#','#','#','#','#','#','#','#','#', '╝'}};

            char[,] charrie2 = { {'┌','#','#','#','#','#','#','#','#','#','┐'},
                                 {'└','#','#','#','#','#','#','#','#','#','┘'}};

            char[,] turtle1 = { {' ', '@','@',' ','@','@',' ','@','@'},
                                {' ', '@','@',' ','@','@',' ','@','@'} };

            #endregion

            Forms temp1 = new Forms();
            Forms temp2 = new Forms();
            Forms temp3 = new Forms();

            #region Inicialize Trucks
            List<Forms> trucks = new List<Forms>();

            temp1 = new Forms();
            temp1.x = xOfTruckOne;
            temp1.y = y;
            temp1.ch = vehicles[0];
            temp1.color = ConsoleColor.Yellow;
            temp1.speed = 1;
            trucks.Add(temp1);

            temp2 = new Forms();
            temp2.x = temp1.x + 20;
            temp2.y = temp1.y;
            temp2.ch = temp1.ch;
            temp2.speed = temp1.speed;
            temp2.color = ConsoleColor.Red;
            trucks.Add(temp2);
            #endregion

            #region Inicialize CarTwo
            List<Forms> carTwo = new List<Forms>();

            temp1 = new Forms();
            temp1.x = xOfCarTwo;
            temp1.y = y + 3;
            temp1.ch = vehicles[1];
            temp1.color = ConsoleColor.Green;
            temp1.speed = 2;
            carTwo.Add(temp1);

            temp2 = new Forms();
            temp2.x = temp1.x - 20;
            temp2.y = temp1.y;
            temp2.ch = temp1.ch;
            temp2.color = ConsoleColor.DarkMagenta;
            temp2.speed = temp1.speed;
            carTwo.Add(temp2);

            temp3 = new Forms();
            temp3.x = temp2.x - 20;
            temp3.y = temp1.y;
            temp3.ch = temp1.ch;
            temp3.color = ConsoleColor.DarkCyan;
            temp3.speed = temp1.speed;
            carTwo.Add(temp3);
            #endregion

            #region Inicialize CarThree
            List<Forms> carThree = new List<Forms>();

            temp1 = new Forms();
            temp1.x = xOfcarThree;
            temp1.y = y + 6;
            temp1.ch = vehicles[2];
            temp1.color = ConsoleColor.DarkBlue;
            temp1.speed = 2;
            carThree.Add(temp1);

            temp2 = new Forms();
            temp2.x = temp1.x + 20;
            temp2.y = temp1.y;
            temp2.ch = temp1.ch;
            temp2.color = ConsoleColor.DarkGray;
            temp2.speed = temp1.speed;
            carThree.Add(temp2);

            temp3 = new Forms();
            temp3.x = temp2.x + 20;
            temp3.y = temp1.y;
            temp3.ch = temp1.ch;
            temp3.color = ConsoleColor.DarkYellow;
            temp3.speed = temp1.speed;
            carThree.Add(temp3);
            #endregion

            #region Inicialize CarFour
            List<Forms> carFour = new List<Forms>();

            temp1 = new Forms();
            temp1.x = xOfcarFour;
            temp1.y = y + 9;
            temp1.ch = vehicles[3];
            temp1.color = ConsoleColor.Blue;
            temp1.speed = 2;
            carFour.Add(temp1);

            temp2 = new Forms();
            temp2.x = temp1.x - 20;
            temp2.y = temp1.y;
            temp2.ch = temp1.ch;
            temp2.color = ConsoleColor.DarkRed;
            temp2.speed = temp1.speed;
            carFour.Add(temp2);

            temp3 = new Forms();
            temp3.x = temp2.x - 20;
            temp3.y = temp1.y;
            temp3.ch = temp1.ch;
            temp3.color = ConsoleColor.Gray;
            temp3.speed = temp1.speed;
            carFour.Add(temp3);
            #endregion

            #region Inicialize CarFive
            List<Forms> carFive = new List<Forms>();

            temp1 = new Forms();
            temp1.x = xOfcarFive;
            temp1.y = y + 12;
            temp1.ch = vehicles[4];
            temp1.color = ConsoleColor.Magenta;
            temp1.speed = 1;
            carFive.Add(temp1);

            temp2 = new Forms();
            temp2.x = temp1.x + 20;
            temp2.y = temp1.y;
            temp2.ch = temp1.ch;
            temp2.color = ConsoleColor.Green;
            temp2.speed = temp1.speed;
            carFive.Add(temp2);

            temp3 = new Forms();
            temp3.x = temp2.x + 20;
            temp3.y = temp1.y;
            temp3.ch = temp1.ch;
            temp3.color = ConsoleColor.Cyan;
            temp3.speed = temp1.speed;
            carFive.Add(temp3);
            #endregion

            #region Trees Inicialize
            Forms tempA = new Forms();
            Forms tempB = new Forms();
            Forms tempC = new Forms();

            List<Forms> TreesOne = new List<Forms>();

            #region One
            // O N E
            tempA.x = xOfTreeOne;
            tempA.y = yOfTree;
            tempA.color = ConsoleColor.DarkRed;
            tempA.ch = charrie;
            tempA.speed = 2;
            TreesOne.Add(tempA);

            tempB.x = tempA.x - 10;
            tempB.y = tempA.y;
            tempB.color = tempA.color;
            tempB.ch = tempA.ch;
            tempB.speed = tempA.speed;
            TreesOne.Add(tempB);

            tempC.x = tempA.x - 25;
            tempC.y = tempA.y;
            tempC.color = tempA.color;
            tempC.ch = tempA.ch;
            tempC.speed = tempA.speed;
            TreesOne.Add(tempC);

            tempC = new Forms();
            tempC.x = tempA.x - 35;
            tempC.y = tempA.y;
            tempC.color = tempA.color;
            tempC.ch = tempA.ch;
            tempC.speed = tempA.speed;
            TreesOne.Add(tempC);

            tempC = new Forms();
            tempC.x = tempA.x - 50;
            tempC.y = tempA.y;
            tempC.color = tempA.color;
            tempC.ch = tempA.ch;
            tempC.speed = tempA.speed;
            TreesOne.Add(tempC);
            #endregion

            #region Two
            // T   W   O 
            tempA = new Forms();
            tempB = new Forms();
            tempC = new Forms();

            tempA.x = xOfTreeTwo;
            tempA.y = yOfTree - 2;
            tempA.color = ConsoleColor.DarkYellow;
            tempA.ch = charrie2;
            tempA.speed = 1;
            TreesOne.Add(tempA);

            tempB.x = tempA.x - 15;
            tempB.y = tempA.y;
            tempB.color = tempA.color;
            tempB.ch = tempA.ch;
            tempB.speed = tempA.speed;
            TreesOne.Add(tempB);

            tempC.x = tempA.x - 29;
            tempC.y = tempA.y;
            tempC.color = tempA.color;
            tempC.ch = tempA.ch;
            tempC.speed = tempA.speed;
            TreesOne.Add(tempC);

            tempC = new Forms();
            tempC.x = tempA.x + 14;
            tempC.y = tempA.y;
            tempC.color = tempA.color;
            tempC.ch = tempA.ch;
            tempC.speed = tempA.speed;
            TreesOne.Add(tempC);
            #endregion

            #region Three
            // T H R E E
            tempA = new Forms();
            tempB = new Forms();
            tempC = new Forms();

            tempA.x = xOfTreeTwo;
            tempA.y = yOfTree - 6;
            tempA.color = ConsoleColor.DarkGreen;
            tempA.ch = charrie3;
            tempA.speed = 2;
            TreesOne.Add(tempA);

            tempB.x = tempA.x - 20;
            tempB.y = tempA.y;
            tempB.color = tempA.color;
            tempB.ch = tempA.ch;
            tempB.speed = tempA.speed;
            TreesOne.Add(tempB);

            tempB = new Forms();
            tempB.x = tempA.x - 40;
            tempB.y = tempA.y;
            tempB.color = tempA.color;
            tempB.ch = tempA.ch;
            tempB.speed = tempA.speed;
            TreesOne.Add(tempB);
            #endregion

            #endregion

            #region Turtles
            List<Forms> Turtles = new List<Forms>();

            #region One
            tempA = new Forms();
            tempB = new Forms();
            tempC = new Forms();

            tempA.x = xOfTreeOne + 10;
            tempA.y = yOfTree + 2;
            tempA.color = ConsoleColor.Red;
            tempA.speed = 2;
            tempA.ch = turtle1;
            Turtles.Add(tempA);

            tempB.x = tempA.x + 20;
            tempB.y = tempA.y;
            tempB.color = tempA.color;
            tempB.speed = tempA.speed;
            tempB.ch = tempA.ch;
            Turtles.Add(tempB);

            tempC.x = tempA.x + 40;
            tempC.y = tempA.y;
            tempC.color = tempA.color;
            tempC.speed = tempA.speed;
            tempC.ch = tempA.ch;
            Turtles.Add(tempC);
            #endregion

            #region Two
            tempA = new Forms();
            tempB = new Forms();
            tempC = new Forms();

            tempA.x = xOfTreeOne;
            tempA.y = yOfTree - 4;
            tempA.color = ConsoleColor.Red;
            tempA.speed = 2;
            tempA.ch = turtle1;
            Turtles.Add(tempA);

            tempB.x = tempA.x + 20;
            tempB.y = tempA.y;
            tempB.color = tempA.color;
            tempB.speed = tempA.speed;
            tempB.ch = tempA.ch;
            Turtles.Add(tempB);

            tempC.x = tempA.x + 40;
            tempC.y = tempA.y;
            tempC.color = tempA.color;
            tempC.speed = tempA.speed;
            tempC.ch = tempA.ch;
            Turtles.Add(tempC);
            #endregion

            #endregion

            #endregion

            CustomizeConsole();
            //Front page
            PrintLogo();
            PrintName();
            Thread.Sleep(1000);
            hideLogo();
            MainMenu();
            //Console.WriteLine("╘╦╛");
            //Console.WriteLine(" ╙╜");
            if (inputChoice == 3 || secondChoice == 1)
            {
                return;
            }
            SetInitialPositionOfScorpion();
           //Main game
            while (true)
            {
                Console.Clear();
                #region KeyReading
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyPressed = Console.ReadKey();
                    if (keyPressed.Key == ConsoleKey.UpArrow)
                    {
                        MoveScorpionUp();
                    }
                    else if (keyPressed.Key == ConsoleKey.DownArrow)
                    {
                        MoveScorpionDown();
                    }
                    else if (keyPressed.Key == ConsoleKey.LeftArrow)
                    {
                        MoveScorpionLeft();
                    }
                    else if (keyPressed.Key == ConsoleKey.RightArrow)
                    {
                        MoveScorpionRight();
                    }
                } 
                #endregion
                
                DrawLineNumbers();
                DrawStops();
                //Options (new game, sound, etc.)
                //DrawTrees(TreesOne, Turtles);
                PrintScorpion(scorpion);
                DrawCars(TreesOne, Turtles, trucks, carTwo, carThree, carFour, carFive);
                //Frog movement
                //Points
                //Time
                //Reading name of player
                //Print name and score in txt file
                //~ colors and some bg forms with unicode chars?
                // graphic down with scorpions = numbers of lifes left
                //exception handling
                //~sound effects

                Console.ForegroundColor = ConsoleColor.White;
                
                Thread.Sleep(200);
            }

            //return console color to default
            
        }

        private static void DrawStops()
        {
            for (int i = 22; i < 24; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.SetCursorPosition(j, i);
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.Write(' ');
                }
            }
            Console.ResetColor();
            for (int i = 40; i < 42; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.SetCursorPosition(j, i);
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.Write(' ');
                }
            }
            Console.ResetColor();
        }

        //private static bool CheckCrash(Forms item)
        //{
        //    if ((item.x == scorpion.x || item.x == scorpion.x + 1) && (item.y == scorpion.y || item.y == scorpion.y + 1))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        private static void DrawLineNumbers()
        {
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(2, i);
                Console.WriteLine(i);
            }
        }

        private static void SetInitialPositionOfScorpion()
        {
            scorpion = new Forms();
            scorpion.y = 40;
            scorpion.x = width / 2;
            scorpion.ch = scorpionForm;
            scorpion.speed = 3;
            scorpion.color = ConsoleColor.Yellow;
        }

        private static void PrintScorpion(Forms scorpion)
        {
            char[,] ch = scorpion.ch;
            ConsoleColor color = scorpion.color;
            int x = scorpion.x;
            int y = scorpion.y;

            for (int j = 0; j < ch.GetLength(0); j++)
            {
                for (int i = 0; i < ch.GetLength(1); i++)
                {

                    if (y == 22 || y == 23 || y == 39 || y == 40)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                    }
                    Console.SetCursorPosition(x + i, y + j);
                    Console.ForegroundColor = color;
                    Console.WriteLine(ch[j, i]);
                    Console.ResetColor();
                }
            }
        }

        private static void MoveScorpionUp()
        {
            int step = 3;
            if (scorpion.y <= 23)
            {
                step = 2;
            }
            if (scorpion.y >= 2)
            {
                tempCoord = new Forms();
                tempCoord.x = scorpion.x;
                tempCoord.color = scorpion.color;
                tempCoord.speed = scorpion.speed;
                tempCoord.ch = scorpion.ch;
                tempCoord.y = scorpion.y - step;
                scorpion = tempCoord;
            }
            
            //scorpion.y = scorpion.y - 1;
        }
        private static void MoveScorpionDown()
        {
            int step = 3;
            if (scorpion.y < 23)
            {
                step = 2;
            }
            if (scorpion.y < height - 4)
            {
                tempCoord = new Forms();
                tempCoord.x = scorpion.x;
                tempCoord.color = scorpion.color;
                tempCoord.speed = scorpion.speed;
                tempCoord.ch = scorpion.ch;
                tempCoord.y = scorpion.y + step;
                scorpion = tempCoord;
            }
            
            //scorpion.y = scorpion.y + 1;
        }
        private static void MoveScorpionLeft()
        {
            if (scorpion.x > 2)
            {
                tempCoord = new Forms();
                tempCoord.x = scorpion.x - 3;
                tempCoord.color = scorpion.color;
                tempCoord.speed = scorpion.speed;
                tempCoord.ch = scorpion.ch;
                tempCoord.y = scorpion.y;
                scorpion = tempCoord;
            }
            
            //scorpion.x = scorpion.x - 1;
        }
        private static void MoveScorpionRight()
        {
            if (scorpion.x < width - 4)
            {
                tempCoord = new Forms();
                tempCoord.x = scorpion.x + 3;
                tempCoord.color = scorpion.color;
                tempCoord.speed = scorpion.speed;
                tempCoord.ch = scorpion.ch;
                tempCoord.y = scorpion.y;
                scorpion = tempCoord;
                scorpion.x = scorpion.x + 1;
            }
           
        }

    }
}
