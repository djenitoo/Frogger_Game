using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection;

namespace Frogger
{
    class Program
    {
        #region Global values
        static int finalPoints = 0;
        static int lives = 5;
        static int width = 69;
        static int height = 50;
        static bool scorpionInSecondHalf = false;
        static bool quitGame = false;
        static bool[] slotsAreEmpty = { true, true, true, true };
        static int points = 0;
        static int time = 500;
        static int y = 25;
        static int lineOfInterest = 0;
        static StringBuilder strOfInterest = new StringBuilder();
        static int showHide = 0;
        static int xOfTruckOne = width - 1;
        static int xOfcarThree = width - 10;
        static int xOfcarFour = 10;
        static int xOfCarTwo = width / 2 + 5;
        static int xOfcarFive = width - 5;
        static int xOfTreeOne = 10;
        static int yOfTree = 18;
        static int xOfTreeTwo = 20;
        static int isPartOfTrees = 0;
        static int[] bridges = { 10, 15, 25, 30, 40, 45, 55, 60 };
        static int[] positions = { 5, 0, 20, 2, 55, 1, 38, 0, 2, 6, 12, 1, 30, 3, 45, 2, 60, 6, 25, 6 };
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

        static string[] finalScorpionForm = {"╘╦╛", " ╙╜"};
        static char[,] scorpionForm = {    { '╘', '╦', '╛' },
                                           { ' ', '╙', '╜' } };
        static char[,] deadForm = {     { '×', ' ', '×' },
                                        { ' ', '_', ' ' } };
        static string[] parentsScorpion = { "╘═╦═╛", "  ╙═╜" };
        public static Forms scorpion;
        public static Forms tempCoord; 
        #endregion

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
                        string line = reader.ReadLine();
                        while (line != null)
                        {
                            playerName.Add(line); //Add name and score to the list
                            line = reader.ReadLine();
                        }
                    }
                    string[] playersArr = playerName.ToArray();
                    Array.Sort(playersArr, new AlphanumComparatorFast());
                    while (playerName.Count > 10)
                    {
                        playerName.RemoveAt(playerName.Count - 1);
                    }

                    //string newEntry = points + "\t" + nameOfPlayer;
                    //playerName.Add(newEntry);
                    int counter = 0;
                    for (int i = playersArr.Length - 1, j = 0; i >= playersArr.Length - 13; i--, j++)
                    {
                        Console.SetCursorPosition((width / 2) - 10, 12 + j);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        playerName = playersArr.ToList();
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
                Console.WriteLine("\t\t1000 PTS FOR SAVING ALL SCORPIONS");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\t\tAND RETURN THEM TO THEIR BELOVED HOME\n");
                

                secondChoiseOfMenus();
            }
            #endregion
            else if (inputChoice == 3)
            {
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
                    Console.SetCursorPosition(25 + (10 * i), 27);
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
                        Console.SetCursorPosition(25 + (10 * i), 27);
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

        private static void DrawMovingObjects(List<List<Forms>> TreesOne, List<List<Forms>> Turtles,List<Forms> trucks, List<Forms> carTwo, List<Forms> carThree, List<Forms> carFour, List<Forms> carFive)
        {

            #region MoveTrees
            for (int i = 0; i < TreesOne.Count; i++)
            {
                strOfInterest = new StringBuilder();
                strOfInterest.Append(' ', width);
                for (int j = 0; j < TreesOne[i].Count; j++)
                {
                    
                    PrintVehicles(TreesOne[i][j].x, TreesOne[i][j].y, TreesOne[i][j].color, TreesOne[i][j].speed, TreesOne[i][j].ch, true, true);
                    TreesOne[i][j] = Temp(TreesOne[i][j], i, '+', TreesOne[i][j].speed);
                    if (i == 0)
                    {
                        xOfTreeOne = TreesOne[i][j].x;
                    }
                }
                if (scorpionInSecondHalf && scorpion.y == TreesOne[i][0].y)
                {
                    if (strOfInterest[scorpion.x] == ' ' || strOfInterest[scorpion.x + 1] == ' ' || strOfInterest[scorpion.x + 2] == ' ' ||
                        strOfInterest[scorpion.x] == '*' || strOfInterest[scorpion.x + 1] == '*' || strOfInterest[scorpion.x + 2] == '*')
                    {
                        scorpion.ch = deadForm;
                        PrintScorpion(scorpion, scorpionInSecondHalf);
                        Thread.Sleep(500);
                        SetInitialPositionOfScorpion();
                        PrintScorpion(scorpion, scorpionInSecondHalf);

                        // lives reduction & chech for time > 0
                        lives--;
                        if (lives <= 0)
                        {
                            PrintStringOnPosition(width / 2 - 10, 43, "G A M E  O V E R !!", ConsoleColor.Red);
                            PrintStringOnPosition(10, 45, "Enter your name: ", ConsoleColor.Red);
                            string name = string.Empty;
                            try
                            {
                                name = Console.ReadLine();
                                if (name == string.Empty)
                                {
                                    name = "Alien";
                                    throw new ArgumentNullException();
                                }
                            }
                            catch (ArgumentNullException)
                            {
                                Console.WriteLine("Invalid name");
                            }
                            PrintStringOnPosition(7, 46, "", ConsoleColor.Red);
                            finalPoints = points + ((1000 % (1000 - time)) - 1);
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"..\..\HighScores.txt", true))
                            {
                                file.WriteLine("{0} {1}", finalPoints.ToString().PadRight(8), name);
                            }

                            #region show klasirane
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
                                    string line = reader.ReadLine();
                                    while (line != null)
                                    {
                                        playerName.Add(line); //Add name and score to the list
                                        line = reader.ReadLine();
                                    }
                                }
                                string[] playersArr = playerName.ToArray();
                                Array.Sort(playersArr, new AlphanumComparatorFast());
                                while (playerName.Count > 10)
                                {
                                    playerName.RemoveAt(playerName.Count - 1);
                                }

                                //string newEntry = points + "\t" + nameOfPlayer;
                                //playerName.Add(newEntry);
                                int counter = 0;
                                for (int h = playersArr.Length - 1, g = 0; h >= playersArr.Length - 13; h--, g++)
                                {
                                    Console.SetCursorPosition((width / 2) - 10, 12 + g);
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    playerName = playersArr.ToList();
                                    Console.WriteLine(playersArr[h]);
                                }
                                using (var writer = new StreamWriter(@"..\..\HighScores.txt"))
                                {
                                    for (int h = playersArr.Length - 1; h >= 0; h--)
                                    {
                                        writer.WriteLine(playerName[h]); //Writes name and score supposedly
                                    }
                                }
                            }
                            catch (FileNotFoundException)
                            {
                                Console.Error.WriteLine("\tCannot find 'HighScores.txt'.");
                            }

                            #region menu - return/quit
                            while (true)
                            {
                                for (int n = 0; n < secMenuItems.Length; n++)
                                {
                                    if (secondChoice == n)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                    }
                                    Console.SetCursorPosition(25 + (10 * n), 27);
                                    Console.WriteLine(secMenuItems[n]);
                                    //Console.ResetColor();
                                }
                                if (Console.KeyAvailable)
                                {
                                    for (int r = 0; i < secMenuItems.Length; i++)
                                    {
                                        if (secondChoice == i)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                        }
                                        Console.SetCursorPosition(25 + (10 * i), 27);
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
                                var fileName = Assembly.GetExecutingAssembly().Location;
                                System.Diagnostics.Process.Start(fileName);

                                Environment.Exit(0);
                            }
                            else if (secondChoice == 1)
                            {
                                return;
                            }
                            #endregion
                            #endregion
                        }
                    }
                }
            }
            #endregion

            #region MoveTurtles
            for (int i = 0; i < Turtles.Count; i++)
            {
                strOfInterest = new StringBuilder();
                strOfInterest.Append(' ', width);
                for (int j = 0; j < Turtles[i].Count; j++)
                {
                    char[,] tempCharArr = new char[2, 8];

                    #region check if diving turtle
                    if (j == 2)
                    {
                        if (showHide == 5 || showHide == 6 || showHide == 7)
                        {
                            showHide = (showHide + 1) % 8;
                            tempCharArr = Turtles[i][j].ch;
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
                        tempCharArr = Turtles[i][j].ch;
                    } 
                    #endregion

                    PrintVehicles(Turtles[i][j].x, Turtles[i][j].y, Turtles[i][j].color, Turtles[i][j].speed, tempCharArr, true, false);
                    Turtles[i][j] = Temp(Turtles[i][j], i, '-', Turtles[i][j].speed);
                    if (i == 0)
                    {
                        xOfTreeOne = Turtles[i][j].x;
                    }
                }
                if (scorpionInSecondHalf && scorpion.y == Turtles[i][0].y)
                {
                    if (strOfInterest[scorpion.x] == ' ' || strOfInterest[scorpion.x + 1] == ' ' || strOfInterest[scorpion.x + 2] == ' ' ||
                        strOfInterest[scorpion.x] == '*' || strOfInterest[scorpion.x + 1] == '*' || strOfInterest[scorpion.x + 2] == '*')
                    {
                        scorpion.ch = deadForm;
                        PrintScorpion(scorpion, scorpionInSecondHalf);
                        Thread.Sleep(500);
                        SetInitialPositionOfScorpion();
                        PrintScorpion(scorpion, scorpionInSecondHalf);

                        // lives reduction & chech for time > 0
                        lives--;
                        if (lives <= 0)
                        {
                            PrintStringOnPosition(width / 2 - 10, 43, "G A M E  O V E R !!", ConsoleColor.Red);
                            PrintStringOnPosition(10, 45, "Enter your name: ", ConsoleColor.Red);
                            string name = string.Empty;
                            try
                            {
                                name = Console.ReadLine();
                                if (name == string.Empty)
                                {
                                    name = "Alien";
                                    throw new ArgumentNullException();
                                }
                            }
                            catch (ArgumentNullException)
                            {
                                Console.WriteLine("Invalid name");
                            }
                            PrintStringOnPosition(7, 46, "", ConsoleColor.Red);
                            finalPoints = points + ((1000 % (1000 - time)) - 1);
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"..\..\HighScores.txt", true))
                            {
                                file.WriteLine("{0} {1}", finalPoints.ToString().PadRight(8), name);
                            }

                            #region show klasirane
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
                                    string line = reader.ReadLine();
                                    while (line != null)
                                    {
                                        playerName.Add(line); //Add name and score to the list
                                        line = reader.ReadLine();
                                    }
                                }
                                string[] playersArr = playerName.ToArray();
                                Array.Sort(playersArr, new AlphanumComparatorFast());
                                while (playerName.Count > 10)
                                {
                                    playerName.RemoveAt(playerName.Count - 1);
                                }

                                //string newEntry = points + "\t" + nameOfPlayer;
                                //playerName.Add(newEntry);
                                int counter = 0;
                                for (int h = playersArr.Length - 1, g = 0; h >= playersArr.Length - 13; h--, g++)
                                {
                                    Console.SetCursorPosition((width / 2) - 10, 12 + g);
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    playerName = playersArr.ToList();
                                    Console.WriteLine(playersArr[h]);
                                }
                                using (var writer = new StreamWriter(@"..\..\HighScores.txt"))
                                {
                                    for (int h = playersArr.Length - 1; h >= 0; h--)
                                    {
                                        writer.WriteLine(playerName[h]); //Writes name and score supposedly
                                    }
                                }
                            }
                            catch (FileNotFoundException)
                            {
                                Console.Error.WriteLine("\tCannot find 'HighScores.txt'.");
                            }

                            #region menu - return/quit
                            while (true)
                            {
                                for (int n = 0; n < secMenuItems.Length; n++)
                                {
                                    if (secondChoice == n)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                    }
                                    Console.SetCursorPosition(25 + (10 * n), 27);
                                    Console.WriteLine(secMenuItems[n]);
                                    //Console.ResetColor();
                                }
                                if (Console.KeyAvailable)
                                {
                                    for (int r = 0; i < secMenuItems.Length; i++)
                                    {
                                        if (secondChoice == i)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                        }
                                        Console.SetCursorPosition(25 + (10 * i), 27);
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
                                var fileName = Assembly.GetExecutingAssembly().Location;
                                System.Diagnostics.Process.Start(fileName);

                                Environment.Exit(0);
                            }
                            else if (secondChoice == 1)
                            {
                                return;
                            }
                            #endregion
                            #endregion
                        }
                    }
                    else
                    {
                        scorpion.x = lineOfInterest;
                        //PrintScorpion(scorpion, scorpionInSecondHalf);
                    }
                }
                
            }
            #endregion

            #region MoveTruck
            for (int i = 0; i < trucks.Count; i++)
            {
                PrintVehicles(trucks[i].x, trucks[i].y, trucks[i].color, trucks[i].speed, trucks[i].ch, false, false);
                trucks[i] = Temp(trucks[i], i, '-', trucks[i].speed);
                if (i == 0)
                {
                    xOfTruckOne = trucks[i].x;
                }

            } 
            #endregion

            #region MoveCarTwo
            for (int i = 0; i < carTwo.Count; i++)
            {
                PrintVehicles(carTwo[i].x, carTwo[i].y, carTwo[i].color, carTwo[i].speed, carTwo[i].ch, false, true);
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
                PrintVehicles(carThree[i].x, carThree[i].y, carThree[i].color, carThree[i].speed, carThree[i].ch, false, false);
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
                PrintVehicles(carFour[i].x, carFour[i].y, carFour[i].color, carFour[i].speed, carFour[i].ch, false, true);
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
                PrintVehicles(carFive[i].x, carFive[i].y, carFive[i].color, carFive[i].speed, carFive[i].ch, false, false);
                carFive[i] = Temp(carFive[i], i, '-', carFive[i].speed);
                if (i == 0)
                {
                    xOfcarFive = carFive[i].x;
                }

            }
            #endregion

        }

        private static void PrintVehicles(int x, int y, ConsoleColor color, int speed, char[,] ch, bool isTreeOrTurtle, bool rightOrientation)
        {
            #region rightOrientation true
            if (rightOrientation)
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
                            PrintScorpion(scorpion, scorpionInSecondHalf);
                            Thread.Sleep(500);
                            SetInitialPositionOfScorpion();
                            PrintScorpion(scorpion, scorpionInSecondHalf);

                            // lives reduction & chech for time > 0
                            lives--;
                            if (lives <= 0)
                            {
                                PrintStringOnPosition(width / 2 - 10, 43, "G A M E  O V E R !!", ConsoleColor.Red);
                                PrintStringOnPosition(10, 45, "Enter your name: ", ConsoleColor.Red);
                                string name = string.Empty;
                                try
                                {
                                    name = Console.ReadLine();
                                    if (name == string.Empty)
                                    {
                                        name = "Alien";
                                        throw new ArgumentNullException();
                                    }
                                }
                                catch (ArgumentNullException)
                                {
                                    Console.WriteLine("Invalid name");
                                }
                                PrintStringOnPosition(7, 46, "", ConsoleColor.Red);
                                finalPoints = points + ((1000 % (1000 - time)) - 1);
                                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"..\..\HighScores.txt", true))
                                {
                                    file.WriteLine("{0} {1}", finalPoints.ToString().PadRight(8), name);
                                }

                                #region show klasirane
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
                                        string line = reader.ReadLine();
                                        while (line != null)
                                        {
                                            playerName.Add(line); //Add name and score to the list
                                            line = reader.ReadLine();
                                        }
                                    }
                                    string[] playersArr = playerName.ToArray();
                                    Array.Sort(playersArr, new AlphanumComparatorFast());
                                    while (playerName.Count > 10)
                                    {
                                        playerName.RemoveAt(playerName.Count - 1);
                                    }

                                    //string newEntry = points + "\t" + nameOfPlayer;
                                    //playerName.Add(newEntry);
                                    int counter = 0;
                                    for (int h = playersArr.Length - 1, g = 0; h >= playersArr.Length - 13; h--, g++)
                                    {
                                        Console.SetCursorPosition((width / 2) - 10, 12 + g);
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        playerName = playersArr.ToList();
                                        Console.WriteLine(playersArr[h]);
                                    }
                                    using (var writer = new StreamWriter(@"..\..\HighScores.txt"))
                                    {
                                        for (int h = playersArr.Length - 1; h >= 0; h--)
                                        {
                                            writer.WriteLine(playerName[h]); //Writes name and score supposedly
                                        }
                                    }
                                }
                                catch (FileNotFoundException)
                                {
                                    Console.Error.WriteLine("\tCannot find 'HighScores.txt'.");
                                }

                                #region menu - return/quit
                                while (true)
                                {
                                    for (int n = 0; n < secMenuItems.Length; n++)
                                    {
                                        if (secondChoice == n)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                        }
                                        Console.SetCursorPosition(25 + (10 * n), 27);
                                        Console.WriteLine(secMenuItems[n]);
                                        //Console.ResetColor();
                                    }
                                    if (Console.KeyAvailable)
                                    {
                                        for (int r = 0; i < secMenuItems.Length; i++)
                                        {
                                            if (secondChoice == i)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.Gray;
                                            }
                                            Console.SetCursorPosition(25 + (10 * i), 27);
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
                                    var fileName = Assembly.GetExecutingAssembly().Location;
                                    System.Diagnostics.Process.Start(fileName);

                                    Environment.Exit(0);
                                }
                                else if (secondChoice == 1)
                                {
                                    return;
                                }
                                #endregion
                                #endregion
                            }
                        }
                        else if (isTreeOrTurtle && (scorpion.y == coordY) &&
                                (scorpion.x == coordX && scorpion.x + 2 <= x + ch.GetLength(1)))
                        {
                            scorpion.x = scorpion.x + speed;
                            if (scorpion.x < 0 || scorpion.x + 2 >= width)
                            {
                                //scorpion.ch = deadForm;
                                //PrintScorpion(scorpion, scorpionInSecondHalf);
                                Thread.Sleep(500);
                                SetInitialPositionOfScorpion();
                                PrintScorpion(scorpion, scorpionInSecondHalf);

                                
                            }
                            //else
                            //{
                            //    PrintScorpion(scorpion, scorpionInSecondHalf);
                            //    isPartOfObj = true;
                            //}

                        }
                        if (isTreeOrTurtle && scorpion.y == coordY)
                        {
                            strOfInterest[coordX] = ch[j, i];
                        }
                        //else if (isTreeOrTurtle && (scorpion.y == coordY) &&
                        //    (scorpion.x != coordX || scorpion.x + 1 != coordX || scorpion.x + 2 == coordX))
                        //{
                        //    //Thread.Sleep(500);
                        //    SetInitialPositionOfScorpion();
                        //    PrintScorpion(scorpion, scorpionInSecondHalf);
                        //}

                    }
                }
            } 
            #endregion
            #region rightOrientation false
            else
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
                            PrintScorpion(scorpion, scorpionInSecondHalf);
                            Thread.Sleep(500);
                            SetInitialPositionOfScorpion();
                            PrintScorpion(scorpion, scorpionInSecondHalf);

                            //lives reduction
                            lives--;
                            if (lives <= 0)
                            {
                                PrintStringOnPosition(width / 2 - 10, 43, "G A M E  O V E R !!", ConsoleColor.Red);
                                PrintStringOnPosition(10, 45, "Enter your name: ", ConsoleColor.Red);
                                string name = string.Empty;
                                try
                                {
                                    name = Console.ReadLine();
                                    if (name == string.Empty)
                                    {
                                        name = "Alien";
                                        throw new ArgumentNullException();
                                    }
                                }
                                catch (ArgumentNullException)
                                {
                                    Console.WriteLine("Invalid name");
                                }
                                PrintStringOnPosition(7, 46, "", ConsoleColor.Red);
                                finalPoints = points + ((1000 % (1000 - time)) - 1);
                                using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"..\..\HighScores.txt", true))
                                {
                                    file.WriteLine("{0} {1}", finalPoints.ToString().PadRight(8), name);
                                }

                                #region show klasirane
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
                                        string line = reader.ReadLine();
                                        while (line != null)
                                        {
                                            playerName.Add(line); //Add name and score to the list
                                            line = reader.ReadLine();
                                        }
                                    }
                                    string[] playersArr = playerName.ToArray();
                                    Array.Sort(playersArr, new AlphanumComparatorFast());
                                    while (playerName.Count > 10)
                                    {
                                        playerName.RemoveAt(playerName.Count - 1);
                                    }

                                    //string newEntry = points + "\t" + nameOfPlayer;
                                    //playerName.Add(newEntry);
                                    int counter = 0;
                                    for (int h = playersArr.Length - 1, g = 0; h >= playersArr.Length - 13; h--, g++)
                                    {
                                        Console.SetCursorPosition((width / 2) - 10, 12 + g);
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        playerName = playersArr.ToList();
                                        Console.WriteLine(playersArr[h]);
                                    }
                                    using (var writer = new StreamWriter(@"..\..\HighScores.txt"))
                                    {
                                        for (int h = playersArr.Length - 1; h >= 0; h--)
                                        {
                                            writer.WriteLine(playerName[h]); //Writes name and score supposedly
                                        }
                                    }
                                }
                                catch (FileNotFoundException)
                                {
                                    Console.Error.WriteLine("\tCannot find 'HighScores.txt'.");
                                }

                                #region menu - return/quit
                                while (true)
                                {
                                    for (int n = 0; n < secMenuItems.Length; n++)
                                    {
                                        if (secondChoice == n)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.Gray;
                                        }
                                        Console.SetCursorPosition(25 + (10 * n), 27);
                                        Console.WriteLine(secMenuItems[n]);
                                        //Console.ResetColor();
                                    }
                                    if (Console.KeyAvailable)
                                    {
                                        for (int r = 0; i < secMenuItems.Length; i++)
                                        {
                                            if (secondChoice == i)
                                            {
                                                Console.ForegroundColor = ConsoleColor.Red;
                                            }
                                            else
                                            {
                                                Console.ForegroundColor = ConsoleColor.Gray;
                                            }
                                            Console.SetCursorPosition(25 + (10 * i), 27);
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
                                    var fileName = Assembly.GetExecutingAssembly().Location;
                                    System.Diagnostics.Process.Start(fileName);

                                    Environment.Exit(0);
                                }
                                else if (secondChoice == 1)
                                {
                                    return;
                                }
                                #endregion
                                #endregion
                            }
                        }
                        else if (isTreeOrTurtle && (scorpion.y == coordY || scorpion.y + 1 == coordY) &&
                                (scorpion.x == coordX && scorpion.x + 2 <= x + ch.GetLength(1)))
                        {
                            
                            //scorpion.x = coordX - speed;
                            lineOfInterest = coordX - speed;
                            if (lineOfInterest < 0 || lineOfInterest + 2 >= width)
                            {
                                //scorpion.ch = deadForm;
                                //PrintScorpion(scorpion, scorpionInSecondHalf);
                                Thread.Sleep(500);
                                SetInitialPositionOfScorpion();
                                PrintScorpion(scorpion, scorpionInSecondHalf);

                                
                            }
                            //else
                            //{
                            //    PrintScorpion(scorpion, scorpionInSecondHalf);
                            //    //isPartOfObj = true;
                            //}
                            
                        }

                        if (isTreeOrTurtle && scorpion.y == coordY)
                        {
                            strOfInterest[coordX] = ch[j, i];
                        }

                    }
                }
            } 
            #endregion
            
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
            return temp;
        }

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

            char[,] turtle1 = { {'-', '@','@','-','@','@','-','@','@'},
                                {'-', '@','@','-','@','@','-','@','@'} };

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

            List<List<Forms>> TreesOne = new List<List<Forms>>();
            List<Forms> TreesTemp = new List<Forms>();
            #region One
            // O N E
            tempA.x = xOfTreeOne;
            tempA.y = yOfTree;
            tempA.color = ConsoleColor.DarkRed;
            tempA.ch = charrie;
            tempA.speed = 2;
            TreesTemp.Add(tempA);

            tempB.x = tempA.x - 10;
            tempB.y = tempA.y;
            tempB.color = tempA.color;
            tempB.ch = tempA.ch;
            tempB.speed = tempA.speed;
            TreesTemp.Add(tempB);

            tempC.x = tempA.x - 25;
            tempC.y = tempA.y;
            tempC.color = tempA.color;
            tempC.ch = tempA.ch;
            tempC.speed = tempA.speed;
            TreesTemp.Add(tempC);

            tempC = new Forms();
            tempC.x = tempA.x - 35;
            tempC.y = tempA.y;
            tempC.color = tempA.color;
            tempC.ch = tempA.ch;
            tempC.speed = tempA.speed;
            TreesTemp.Add(tempC);

            tempC = new Forms();
            tempC.x = tempA.x - 50;
            tempC.y = tempA.y;
            tempC.color = tempA.color;
            tempC.ch = tempA.ch;
            tempC.speed = tempA.speed;
            TreesTemp.Add(tempC);
            TreesOne.Add(TreesTemp);
            #endregion

            #region Two

            // T   W   O 
            tempA = new Forms();
            tempB = new Forms();
            tempC = new Forms();
            TreesTemp = new List<Forms>();

            tempA.x = xOfTreeTwo;
            tempA.y = yOfTree - 2;
            tempA.color = ConsoleColor.DarkYellow;
            tempA.ch = charrie2;
            tempA.speed = 1;
            TreesTemp.Add(tempA);

            tempB.x = tempA.x - 15;
            tempB.y = tempA.y;
            tempB.color = tempA.color;
            tempB.ch = tempA.ch;
            tempB.speed = tempA.speed;
            TreesTemp.Add(tempB);

            tempC.x = tempA.x - 29;
            tempC.y = tempA.y;
            tempC.color = tempA.color;
            tempC.ch = tempA.ch;
            tempC.speed = tempA.speed;
            TreesTemp.Add(tempC);

            tempC = new Forms();
            tempC.x = tempA.x + 14;
            tempC.y = tempA.y;
            tempC.color = tempA.color;
            tempC.ch = tempA.ch;
            tempC.speed = tempA.speed;
            TreesTemp.Add(tempC);
            TreesOne.Add(TreesTemp);
            #endregion

            #region Three
            // T H R E E
            tempA = new Forms();
            tempB = new Forms();
            tempC = new Forms();
            TreesTemp = new List<Forms>();

            tempA.x = xOfTreeTwo;
            tempA.y = yOfTree - 6;
            tempA.color = ConsoleColor.DarkGreen;
            tempA.ch = charrie3;
            tempA.speed = 2;
            TreesTemp.Add(tempA);

            tempB.x = tempA.x - 20;
            tempB.y = tempA.y;
            tempB.color = tempA.color;
            tempB.ch = tempA.ch;
            tempB.speed = tempA.speed;
            TreesTemp.Add(tempB);

            tempB = new Forms();
            tempB.x = tempA.x - 40;
            tempB.y = tempA.y;
            tempB.color = tempA.color;
            tempB.ch = tempA.ch;
            tempB.speed = tempA.speed;
            TreesTemp.Add(tempB);

            TreesOne.Add(TreesTemp);
            #endregion

            #endregion

            #region Turtles
            List<List<Forms>> Turtles = new List<List<Forms>>();
            List<Forms> TurtlesTemp = new List<Forms>();
            #region One
            tempA = new Forms();
            tempB = new Forms();
            tempC = new Forms();

            tempA.x = xOfTreeOne + 10;
            tempA.y = yOfTree + 2;
            tempA.color = ConsoleColor.Red;
            tempA.speed = 2;
            tempA.ch = turtle1;
            TurtlesTemp.Add(tempA);

            tempB.x = tempA.x + 20;
            tempB.y = tempA.y;
            tempB.color = tempA.color;
            tempB.speed = tempA.speed;
            tempB.ch = tempA.ch;
            TurtlesTemp.Add(tempB);

            tempC.x = tempA.x + 40;
            tempC.y = tempA.y;
            tempC.color = tempA.color;
            tempC.speed = tempA.speed;
            tempC.ch = tempA.ch;
            TurtlesTemp.Add(tempC);
            Turtles.Add(TurtlesTemp);
            #endregion

            #region Two
            tempA = new Forms();
            tempB = new Forms();
            tempC = new Forms();
            TurtlesTemp = new List<Forms>();

            tempA.x = xOfTreeOne;
            tempA.y = yOfTree - 4;
            tempA.color = ConsoleColor.Red;
            tempA.speed = 2;
            tempA.ch = turtle1;
            TurtlesTemp.Add(tempA);

            tempB.x = tempA.x + 20;
            tempB.y = tempA.y;
            tempB.color = tempA.color;
            tempB.speed = tempA.speed;
            tempB.ch = tempA.ch;
            TurtlesTemp.Add(tempB);

            tempC.x = tempA.x + 40;
            tempC.y = tempA.y;
            tempC.color = tempA.color;
            tempC.speed = tempA.speed;
            tempC.ch = tempA.ch;
            TurtlesTemp.Add(tempC);
            Turtles.Add(TurtlesTemp);
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
                //time reduction & time check for less than 0
                time--;
                
                if (time <= 0)
                {
                    break;
                }
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
                    else if (keyPressed.Key == ConsoleKey.P)
                    {
                        #region pause processing
                        while (true)
                        {
                            PrintStringOnPosition(width / 2 - 4, 22, "P A U S E", ConsoleColor.White);
                            if (Console.KeyAvailable)
                            {
                                ConsoleKeyInfo keyP = Console.ReadKey();
                                if (keyP.Key == ConsoleKey.P)
                                {
                                    break;
                                }
                            }
                        } 
                        #endregion
                    }
                    else if (keyPressed.Key == ConsoleKey.Q)
                    {
                        #region quit processing
                        while (true)
                        {
                            PrintStringOnPosition(width / 2 - 8, 22, "QUIT GAME? Y/N", ConsoleColor.White);
                            if (Console.KeyAvailable)
                            {
                                ConsoleKeyInfo keyQ = Console.ReadKey();
                                if (keyQ.Key == ConsoleKey.Y)
                                {
                                    quitGame = true;
                                    break;
                                }
                                else if (keyQ.Key == ConsoleKey.N)
                                {
                                    quitGame = false;
                                    break;
                                }
                                else
                                {
                                    quitGame = false;
                                    break;
                                }
                            }
                        } 
                        #endregion
                    }
                } 
                #endregion

                if (quitGame)
                {
                    return;
                }
                //DrawLineNumbers();
                Console.Clear();
                PrintStringOnPosition(10, 49, "TIME: " + time, ConsoleColor.White);
                PrintStringOnPosition(50, 49, "POINTS: " + points, ConsoleColor.White);
                PrintStringOnPosition(30, 49, "LIVES: " + lives, ConsoleColor.White);
                DrawStops();
                if (scorpion.y <= 22)
                {
                    scorpionInSecondHalf = true;
                }
                else
                {
                    scorpionInSecondHalf = false;
                }
                DrawMovingObjects(TreesOne, Turtles, trucks, carTwo, carThree, carFour, carFive);
                if ((!slotsAreEmpty[0] && !slotsAreEmpty[1] && !slotsAreEmpty[2] && !slotsAreEmpty[3]))
                {
                    Thread.Sleep(400);
                    break;
                }
                PrintScorpion(scorpion, scorpionInSecondHalf);
                
                //~sound effects

                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(300);
                
                // this break of loop if all 4 scorpions reach its slot  
                
            }

            // HERE succesful end of game change of forrest
            #region change family in the forrest
            if (!(slotsAreEmpty[0] && slotsAreEmpty[1] && slotsAreEmpty[2] && slotsAreEmpty[3]))
            {
                string[] childredScorpi = { "╘╦╛ ╘╦╛ ╘╦╛ ╘╦╛", " ╙╜  ╙╜  ╙╜  ╙╜" };
                int moveRight = 12;
                for (int i = 0, g = 0; g < 4; g++, i = (i + 1) % 2)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(positions[18] + moveRight, positions[19] + i);
                    Console.WriteLine(childredScorpi[i]);
                }

                for (int q = 0; q < slotsAreEmpty.Length; q++)
                {
                    for (int s = 0; s < finalScorpionForm.Length; s++)
                    {
                        Console.SetCursorPosition(bridges[(q * 2)] + 1, 10 + s);
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine(new string(' ', finalScorpionForm[s].Length));
                        Console.ResetColor();
                    }
                }
            } 
            #endregion

            // ASK FOR   NAME AND PRINT POINTS
            PrintStringOnPosition(width / 2 - 10, 43, "G A M E  O V E R !!", ConsoleColor.Red);
            PrintStringOnPosition(10, 45, "Enter your name: ", ConsoleColor.Red);
            string name = string.Empty;
            try
            {
                name = Console.ReadLine();
                if (name == string.Empty)
                {
                    name = "Alien";
                    throw new ArgumentNullException();
                }
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("Invalid name");
            }
            PrintStringOnPosition(7, 46, "", ConsoleColor.Red);
            finalPoints = points + ((1000 % (1000 - time)) - 1);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"..\..\HighScores.txt", true))
            {
                file.WriteLine("{0} {1}", finalPoints.ToString().PadRight(8), name);
            }

            #region show klasirane
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
                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        playerName.Add(line); //Add name and score to the list
                        line = reader.ReadLine();
                    }
                }
                string[] playersArr = playerName.ToArray();
                Array.Sort(playersArr, new AlphanumComparatorFast());
                while (playerName.Count > 10)
                {
                    playerName.RemoveAt(playerName.Count - 1);
                }

                //string newEntry = points + "\t" + nameOfPlayer;
                //playerName.Add(newEntry);
                int counter = 0;
                for (int i = playersArr.Length - 1, j = 0; i >= playersArr.Length - 13; i--, j++)
                {
                    Console.SetCursorPosition((width / 2) - 10, 12 + j);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    playerName = playersArr.ToList();
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

            #region menu - return/quit
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
                    Console.SetCursorPosition(25 + (10 * i), 27);
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
                        Console.SetCursorPosition(25 + (10 * i), 27);
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
                var fileName = Assembly.GetExecutingAssembly().Location;
                System.Diagnostics.Process.Start(fileName);

                Environment.Exit(0);
            }
            else if (secondChoice == 1)
            {
                return;
            }
            #endregion
            #endregion
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
            for (int i = 8; i < 10; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Console.SetCursorPosition(j, i);
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write('▒');
                }
            }
            Console.ResetColor();
            for (int i = 10; i < 12; i++)
            {
                
                for (int j = 0; j < bridges.Length - 1; j+= 2)
                {
                    
                    int count = bridges[j];
                    while (count <= bridges[j + 1])
                    {
                        Console.SetCursorPosition(count, i);
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.Write(' ');
                        count++;
                    }
                }
                Console.ResetColor();
                for (int q = 0; q < slotsAreEmpty.Length; q++)
                {
                    if (!slotsAreEmpty[q])
                    {
                        for (int s = 0; s < finalScorpionForm.Length; s++)
                        {
                            Console.SetCursorPosition(bridges[(q * 2)] + 1, 10 + s);
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.BackgroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine(finalScorpionForm[s]);
                            Console.ResetColor();
                        }
                    }
                }
            }
            DrawForest();
        }

        private static void DrawForest()
        {
            #region Forrest
            string[] tree = { " CCC ", "CCCCC", " CCC ", "  #   ", "  #     " };
            string[] grass = { "v v v v", "| | | |" };
            
            for (int p = 0; p < positions.Length - 1; p += 2)
            {
                #region draw static art
                if (p < 8)
                {
                    for (int i = 0; i < tree.Length; i++)
                    {
                        Console.SetCursorPosition(positions[p], (positions[p + 1]) + i);
                        if (i < 3)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                        }
                        Console.WriteLine(tree[i]);
                        Console.ResetColor();
                    }
                }
                else if (p < 18)
                {
                    for (int i = 0; i < grass.Length; i++)
                    {
                        Console.SetCursorPosition(positions[p], positions[p + 1] + i);
                        if (i < 1)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                        }
                        Console.WriteLine(grass[i]);
                    }
                }
                else if (p >= 18)
                {
                    int moveRight = 0;
                    for (int i = 0, g = 0; g < 4; g++, i = (i + 1) % 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        if (g > 1)
                        {
                            moveRight = 5;
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }

                        Console.SetCursorPosition(positions[p] + moveRight, positions[p + 1] + i);
                        Console.WriteLine(parentsScorpion[i]);
                    }
                }
                #endregion

            }
            #endregion
        }

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

        private static void PrintScorpion(Forms scorpion, bool scInSecHalf)
        {
            bool isDead = false;
            int x = scorpion.x;
            int y = scorpion.y;
            if (scorpion.y == 10)
            {
                if (!((scorpion.x >= 10 && scorpion.x <= 15) || (scorpion.x >= 25 && scorpion.x <= 30) ||
                    (scorpion.x >= 40 && scorpion.x <= 45) || (scorpion.x >= 55 && scorpion.x <= 60)))
                {
                    isDead = true;
                    scorpion.ch = deadForm;
                    scorpion.color = ConsoleColor.Red;

                    lives--;
                    if (lives <= 0)
                    {
                        PrintStringOnPosition(width / 2 - 10, 43, "G A M E  O V E R !!", ConsoleColor.Red);
                        PrintStringOnPosition(10, 45, "Enter your name: ", ConsoleColor.Red);
                        string name = string.Empty;
                        try
                        {
                            name = Console.ReadLine();
                            if (name == string.Empty)
                            {
                                name = "Alien";
                                throw new ArgumentNullException();
                            }
                        }
                        catch (ArgumentNullException)
                        {
                            Console.WriteLine("Invalid name");
                        }
                        PrintStringOnPosition(7, 46, "", ConsoleColor.Red);
                        finalPoints = points + ((1000 % (1000 - time)) - 1);
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"..\..\HighScores.txt", true))
                        {
                            file.WriteLine("{0} {1}", finalPoints.ToString().PadRight(8), name);
                        }

                        #region show klasirane
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
                                string line = reader.ReadLine();
                                while (line != null)
                                {
                                    playerName.Add(line); //Add name and score to the list
                                    line = reader.ReadLine();
                                }
                            }
                            string[] playersArr = playerName.ToArray();
                            Array.Sort(playersArr, new AlphanumComparatorFast());
                            while (playerName.Count > 10)
                            {
                                playerName.RemoveAt(playerName.Count - 1);
                            }

                            //string newEntry = points + "\t" + nameOfPlayer;
                            //playerName.Add(newEntry);
                            int counter = 0;
                            for (int h = playersArr.Length - 1, g = 0; h >= playersArr.Length - 13; h--, g++)
                            {
                                Console.SetCursorPosition((width / 2) - 10, 12 + g);
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                playerName = playersArr.ToList();
                                Console.WriteLine(playersArr[h]);
                            }
                            using (var writer = new StreamWriter(@"..\..\HighScores.txt"))
                            {
                                for (int h = playersArr.Length - 1; h >= 0; h--)
                                {
                                    writer.WriteLine(playerName[h]); //Writes name and score supposedly
                                }
                            }
                        }
                        catch (FileNotFoundException)
                        {
                            Console.Error.WriteLine("\tCannot find 'HighScores.txt'.");
                        }

                        #region menu - return/quit
                        while (true)
                        {
                            for (int n = 0; n < secMenuItems.Length; n++)
                            {
                                if (secondChoice == n)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Gray;
                                }
                                Console.SetCursorPosition(25 + (10 * n), 27);
                                Console.WriteLine(secMenuItems[n]);
                                //Console.ResetColor();
                            }
                            if (Console.KeyAvailable)
                            {
                                for (int r = 0; r < secMenuItems.Length; r++)
                                {
                                    if (secondChoice == r)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                    }
                                    else
                                    {
                                        Console.ForegroundColor = ConsoleColor.Gray;
                                    }
                                    Console.SetCursorPosition(25 + (10 * r), 27);
                                    Console.WriteLine(secMenuItems[r]);
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
                            var fileName = Assembly.GetExecutingAssembly().Location;
                            System.Diagnostics.Process.Start(fileName);

                            Environment.Exit(0);
                        }
                        else if (secondChoice == 1)
                        {
                            return;
                        }
                        #endregion
                        #endregion
                    }
                }
                else
                {
                    if ((scorpion.x > 10 && scorpion.x < 15) && slotsAreEmpty[0] == true)
                    {
                        slotsAreEmpty[0] = false;
                        points += 50;

                    }
                    else if ((scorpion.x > 25 && scorpion.x < 30) && slotsAreEmpty[1] == true)
                    {
                        slotsAreEmpty[1] = false;
                        points += 50;
                    }
                    else if ((scorpion.x > 40 && scorpion.x < 45) && slotsAreEmpty[2] == true)
                    {
                        slotsAreEmpty[2] = false;
                        points += 50;
                    }
                    else if ((scorpion.x > 55 && scorpion.x < 60) && slotsAreEmpty[3] == true)
                    {
                        slotsAreEmpty[3] = false;
                        points += 50;
                    }

                    SetInitialPositionOfScorpion();
                }
                if( slotsAreEmpty[3] == false &&  slotsAreEmpty[2] == false &&  slotsAreEmpty[1] == false  && slotsAreEmpty[0] == false)
                {
                    points += 1000;
                    PrintStringOnPosition(5, 45, "YOU WON!!!", ConsoleColor.Red);
                    PrintStringOnPosition(5, 50, "Enter your name: ", ConsoleColor.Red);
                    string name = string.Empty;
                    try
                    {
                        name = Console.ReadLine();
                        if (name == string.Empty)
                        {
                            name = "Alien";
                            throw new ArgumentNullException();
                        }
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("Invalid name");
                    }
                    PrintStringOnPosition(7, 51, "", ConsoleColor.Red);
                    finalPoints = points + (1000 % (1000 - time) - 1);
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"..\..\HighScores.txt", true))
                    {
                        file.WriteLine("{0}{1} ", finalPoints.ToString().PadRight(8), name);
                    }
                    Environment.Exit(0);
                }
            }
            char[,] ch = scorpion.ch;
            ConsoleColor color = scorpion.color;
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
                if (j + 1 == ch.GetLength(0))
                {
                    if (isDead)
                    {
                        Thread.Sleep(500);
                        SetInitialPositionOfScorpion();
                        isDead = false;
                        //PrintScorpion(scorpion, scorpionInSecondHalf);

                    }
                }
            }
            
        }

        static void PrintStringOnPosition(int x, int y, string str, ConsoleColor color)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = color;
            Console.Write(str);
        }

        private static void MoveScorpionUp()
        {
            int step = 3;
            if (scorpion.y <= 23)
            {
                step = 2;
            }
            if (scorpion.y + step >= 14)
            {
                tempCoord = new Forms();
                tempCoord.x = scorpion.x;
                tempCoord.color = scorpion.color;
                tempCoord.speed = scorpion.speed;
                tempCoord.ch = scorpion.ch;
                tempCoord.y = scorpion.y - step;
                scorpion = tempCoord;
                points += 10;
                Console.Beep();
            }
        }
        private static void MoveScorpionDown()
        {
            int step = 3;
            if (scorpion.y < 23)
            {
                step = 2;
            }
            if (scorpion.y < 42 && scorpion.y + step < 42)
            {
                tempCoord = new Forms();
                tempCoord.x = scorpion.x;
                tempCoord.color = scorpion.color;
                tempCoord.speed = scorpion.speed;
                tempCoord.ch = scorpion.ch;
                tempCoord.y = scorpion.y + step;
                scorpion = tempCoord;
                Console.Beep();
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
                Console.Beep();
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
                Console.Beep();
            }
           
        }
    }
}
