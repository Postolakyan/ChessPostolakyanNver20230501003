using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;
using System.Linq;

namespace Chess
{
    internal class Program
    {
        /*The code is written in C# and simulates a basic chess game on the console. It allows the user to choose a figure and its coordinates, and then the program highlights the corresponding cell on the chessboard.


          The code contains several methods, including:

          RunTheProgramm: This method is the main entry point for the program.It displays the chessboard, prompts the user to choose a figure, 
                          its coordinates, and then updates the chessboard accordingly.
          Draw: This method draws the chessboard and calls other methods to highlight cells and receive user input.
          DrawEdges: This method is used to draw vertical borders on the chessboard.
          CreateBoard: This method initializes and returns the chessboard array.
          PrintBoard: This method prints the chessboard to the console, with different colors for each piece and background.
          InputCoordinations: This method prompts the user to choose a figure and its coordinates, validates the input, and returns the selected figure.
          The program uses some C# language features such as loops, conditional statements, dictionaries, and lists.
          It also utilizes some console functions to control the cursor position and color.

          Overall, the program provides a simple and basic chess game simulation, with room for further development and features. */

        /// <summary>
        ///  // The main entry point for the program
        /// </summary>
        public static void RunTheProgramm ()
        {
            bool exit = false;
            while (!exit)
            {
                Draw(); // Draw the chessboard and get user input
                Console.CursorTop = 20;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Do you want to try Again? (Y/N)");
                Console.ForegroundColor = ConsoleColor.White;
                string input = Console.ReadLine();
                if (input.Equals("N", StringComparison.OrdinalIgnoreCase))
                {
                    exit = true;
                    Console.Clear();


                }
                Console.Clear();
            }
        }
        /// <summary>
        /// // Draw the chessboard and get user input
        /// </summary>
        public static void Draw() 
        {
            Console.CursorLeft = 3; 
            string data = "ABCDEFGH";
            List<char> latterslist = new List<char>();
            latterslist.AddRange(data);
            string numbers = "12345678";
            List<char> numberslist = new List<char>();
            numberslist.AddRange(numbers);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            // Draw letters at the top of the board
            foreach (char c in latterslist)
            {
                Console.Write($" {c} ");
            }
            Console.WriteLine("");
            Console.CursorTop = 1; 
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("------------------------------");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            int[] nums = new int[9];
            for (int i = 1; i <= 8; i++)
            {
                nums[i] = i;
                Console.WriteLine(nums[i]);
            }
            DrawEdges(2, 2);
            DrawEdges(2, 28); 
            Console.CursorTop = 10; 
            Console.Write("------------------------------");
            CreateBoard(); // Initialize the chessboard and print it
            var input = InputCoordinations(latterslist, numberslist, out int firstindex, out int secondindex);
            ReplaceFigure(CreateBoard(), firstindex, secondindex, input); // Update the chessboard with the selected figure
        }
        /// <summary>
         /// // Draw vertical borders on the chessboard
         /// </summary>
         /// <param name="top"></param>
         /// <param name="left"></param>
        public static void DrawEdges(int top, int left)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.CursorTop = top;
            for (int i = 0; i < 8; i++)
            {
                Console.CursorLeft = left;
                Console.WriteLine("|");
            }
        }
        /// <summary>
        /// // Initialize and return the chessboard array
        /// </summary>
        /// <returns></returns>
        public static char[,] CreateBoard()
        {
            char[,] board = new char[8, 8];

            // Assign '#' or '@' to each cell to represent the chessboard pattern
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i % 2 == 0 && j % 2 == 1 || j % 2 == 0 && i % 2 == 1)
                    {
                        board[i, j] = '#';
                    }
                    else
                    {
                        board[i, j] = '@';
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
            PrintBoard(board); // Print the chessboard to the console
            return board;
        }
        /// <summary>
        ///  This method prints the chessboard to the console, with different colors for each piece and background.
        /// </summary>
        /// <param name="matrix"></param>
        public static void PrintBoard(char[,] matrix)
        {
            Console.CursorTop = 2;


            for (int i = 0; i < 8; i++)
            {
                Console.CursorLeft = 3;
                for (int j = 0; j < 8; j++)
                {
                    if (matrix[i,j]=='#')
                    {
                        Console.BackgroundColor= ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($" {matrix[i, j]} ");
                        Console.BackgroundColor = ConsoleColor.Black;
                    }

                    else if (matrix[i, j] == '@')
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($" {matrix[i, j]} ");
                        Console.BackgroundColor = ConsoleColor.Black;

                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write($" {matrix[i, j]} ");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
                Console.WriteLine("");
            }
            Console.ForegroundColor= ConsoleColor.White;
        }
        /// <summary>
        /// This method prompts the user to choose a figure and its coordinates, validates the input, and returns the selected figure.
        /// </summary>
        /// <param name="letters"></param>
        /// <param name="numbers"></param>
        /// <param name="firstindex"></param>
        /// <param name="secondindex"></param>
        /// <returns></returns>
        public static char InputCoordinations(List<char> letters, List<char> numbers, out int firstindex, out int secondindex)
        {

            Console.WriteLine("");
            Console.WriteLine("Please Choose The Figure");
            firstindex = 0;
            secondindex = 0;
            Dictionary<char, string> figures = new Dictionary<char, string>
            {
                {'K',"King" },
                {'Q',"Queen" },
                {'N',"KNights" },
                {'R',"Rook" },
                {'B',"Bishop" }
            };
            foreach (KeyValuePair<char,string> c in figures)
            {
                Console.WriteLine($" {c.Key} -> {c.Value}");
            }
            Char.TryParse(Console.ReadLine(), out char figure);
           bool valid =  CheckIfValid(figures, ref figure);
            if (valid)
            {
                Console.WriteLine("Please Insert Coordinations");
                string coordinat = Console.ReadLine();
                if (coordinat.Length != 2)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please Insert Valid Values");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Do you want to try Again? (Y/N)");
                    Console.ForegroundColor = ConsoleColor.White;
                    string input = Console.ReadLine();
                    if (input.Equals("y", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.Clear();
                        RunTheProgramm();
                    }
                    else
                    {
                        Console.Clear();
                    }
                }
                char firstelement = coordinat[0];
                char secondelement = coordinat[1];
                bool isvalid =  CheckIfValid(letters, numbers, ref firstelement, ref secondelement);
                if (isvalid)
                {
                    for (int i = 0; i < letters.Count; i++)
                    {
                        if (firstelement == letters[i])
                        {
                            firstindex = i + 1;
                            break;
                        }
                    }
                    for (int j = 0; j < numbers.Count; j++)
                    {
                        if (secondelement == numbers[j])
                        {
                            secondindex = j + 1;
                            break;
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please Insert Valid Values");
                    Console.WriteLine("Do you want to try Again (Y/N)");
                    Console.ForegroundColor = ConsoleColor.White;
                    string input = Console.ReadLine();
                    if (input.Equals("y", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.Clear();
                        RunTheProgramm();
                    }
                    
                    Console.ForegroundColor = ConsoleColor.White;
                }

            }
            return figure;

        }

        private static bool CheckIfValid(List<char> letters, List<char> numbers,  ref char firstelement, ref char secondelement)
        {
            firstelement = char.ToUpper(firstelement);
            secondelement =  char.ToUpper(secondelement);
            bool firstisvalid = false;
            bool secondisvalid = false;
            foreach (var item in letters)
            {
              
                if (firstelement == item)
                {
                    firstisvalid = true;
                }
            }
            if (!firstisvalid)
            {
                Console.WriteLine("The First Coordinate is not Valid Please insert Valid coordination");
            }
            foreach (var item in numbers)
            {
                if (secondelement == item)
                {
                    secondisvalid = true;
                }
            }
            if (!secondisvalid)
            {
                Console.WriteLine("The Second Coordinate is not Valid Please insert Valid coordination. ");
            }
            if (firstisvalid && secondisvalid)
            {
                return true;
            }else
            {
                return false;
            }
        }
        // Check The Validation Of Figure
        private static bool CheckIfValid(Dictionary<char,string> figures,ref char figure)
        {
              bool isvalid = false;
              figure = char.ToUpper(figure);
              if (figures.ContainsKey(figure))
              {
                isvalid = true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The Input is not Valid ");
                Console.WriteLine("Do you want to try Again? (Y/N)");
                Console.ForegroundColor = ConsoleColor.White;
                string input = Console.ReadLine();
                if (input.Equals("Y", StringComparison.OrdinalIgnoreCase))
                {


                    Console.Clear();
                    RunTheProgramm();
                }
            }
           
                return isvalid;
        }
        public static void ReplaceFigure(char[,] board, int firstindex, int secondindex, char figure)
        {
            board[ secondindex - 1, firstindex - 1] = figure;
            PrintBoard(board);
        }
        static void Main(string[] args)
        {
            RunTheProgramm();
        }
    }
}