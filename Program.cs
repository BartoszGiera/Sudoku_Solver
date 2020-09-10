using System;
using System.Diagnostics;

namespace SudokuSolver_v1._0
{
    class Program
    {
        // Sample sudokus for testing purposes
        // Sudoku for solving is named "sudoku"
        static int[,] sudokuEasy =
        {
            {1, 0, 0, 9, 0, 4, 0, 8, 2},
            {0, 5, 2, 6, 8, 0, 3, 0, 0},
            {8, 6, 4, 2, 0, 0, 9, 1, 0},
            {0, 1, 0, 0, 4, 9, 8, 0, 6},
            {4, 9, 8, 3, 0, 0, 7, 0, 1},
            {6, 0, 7, 0, 1, 0, 0, 9, 3},
            {0, 8, 6, 0, 3, 5, 2, 0, 9},
            {5, 0, 9, 0, 0, 2, 1, 3, 0},
            {0, 3, 0, 4, 9, 7, 0, 0, 8}
        };

        static int[,] sudokuHard =
        {
            {0, 0, 0, 0, 0, 3, 0, 0, 0},
            {0, 0, 0, 0, 6, 0, 0, 5, 4},
            {9, 0, 1, 7, 5, 0, 0, 6, 0},
            {4, 9, 0, 0, 0, 6, 5, 0, 8},
            {0, 0, 0, 0, 0, 0, 0, 0, 0},
            {2, 0, 7, 5, 0, 0, 0, 4, 3},
            {0, 1, 0, 0, 3, 7, 2, 0, 9},
            {7, 4, 0, 0, 2, 0, 0, 0, 0},
            {0, 0, 0, 6, 0, 0, 0, 0, 0},
        };

        static int[,] sudoku =
        {
            {8, 0, 0, 0, 0, 0, 0, 0, 0},
            {0, 0, 3, 6, 0, 0, 0, 0, 0},
            {0, 7, 0, 0, 9, 0, 2, 0, 0},
            {0, 5, 0, 0, 0, 7, 0, 0, 0},
            {0, 0, 0, 0, 4, 5, 7, 0, 0},
            {0, 0, 0, 1, 0, 0, 0, 3, 0},
            {0, 0, 1, 0, 0, 0, 0, 6, 8},
            {0, 0, 8, 5, 0, 0, 0, 1, 0},
            {0, 9, 0, 0, 0, 0, 4, 0, 0}
        };




        // Prints sudoku grid
        static void PrintArray()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(sudoku[i, j] + " ");
                }

                Console.WriteLine();
            }
        }




        // Checks, if it's possible to put number in the place described as [row, column].
        // If so, returns "True"
        static bool IsPossible(int row, int column, int num)
        {
            //Checks the point's column
            for (int i = 0; i < 9; i++)
            {
                if (sudoku[i, column] == num)
                {
                    return false;
                }
            }

            //Checks the point's row
            for (int i = 0; i < 9; i++)
            {
                if (sudoku[row, i] == num)
                {
                    return false;
                }
            }

            int row0 = (row / 3) * 3;
            int column0 = (column / 3) * 3;

            //Checks the point's 3x3 square
            for (int i = 0; i < 3; i++)
            {
                for ( int j = 0; j < 3; j++)
                {
                    if (sudoku[row0 + i, column0 + j] == num)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        



        // Algorithm solving the sudoku
        static void Solver()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    // Blank point's contain a zero inside. If current point in 
                    // the nested "for" loops contains a zero, rest of the code execute.
                    if (sudoku[i,j] == 0)
                    {
                        // Checks numbers between 1-9 if any fits
                        for (int n = 1; n < 10; n++)
                        {
                            // If fits, puts it in the place. Then we recursively 
                            // execute the algorithm to check every possibility. 
                            // If algorithm gets to a dead end, it backtracks to 
                            // last choice and changes it to zero , than goes again 
                            // until the sudoku is completely solved
                            if (IsPossible(i, j, n))
                            {
                                sudoku[i, j] = n;
                                Solver();
                                sudoku[i, j] = 0;
                            }
                        }
                        return;
                    }
                }
            }
            PrintArray();
        }
        
        static void Main(string[] args)
        {
            // Print loaded sudoku
            Console.WriteLine("Your sudoku: ");
            PrintArray();

            Console.WriteLine("\n\nYour solved sudoku: \n");

            // Stopwatch for execution time count
            var watch = Stopwatch.StartNew();

            // Sudoku solving trigger
            Solver();

            watch.Stop();

            Console.WriteLine("\nExecution time: " + watch.Elapsed);
            Console.ReadKey();
        }
    }
}
