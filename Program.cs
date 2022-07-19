using System;


namespace Sudoku_solver
{   
    class Program
    {
        static void Main()
        {
            Sudoku? sudoku = Sudoku.FromConsole();

            Console.WriteLine();
            if (sudoku is null) return;

            sudoku.OutputPrintingEventHandler += Console.Write;

            sudoku.SolveByBacktracking();

            if (sudoku.IsSolved)
            {
                sudoku.Print();
            }
            else
            {
                Console.WriteLine("Cannot solve the sudoku");
            }
        }
    }
}
