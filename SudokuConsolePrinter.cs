using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_solver
{
    internal class SudokuConsolePrinter : ISudokuPrinter
    {
        public void Print(Sudoku sudoku)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write($"{sudoku.GetCell(i, j)?.Value ?? 0} ");
                }
                Console.WriteLine();
            }
        }
    }
}
