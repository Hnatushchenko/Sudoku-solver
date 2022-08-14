using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_solver
{
    internal class Application : IApplication
    {
        ISudokuPrinter _sudokuPrinter;
        ISudokuSolver _sudokuSolver;
        SudokuFactory _sudokuFactory;

        public Application(ISudokuPrinter sudokuPrinter, ISudokuSolver sudokuSolver, SudokuFactory sudokuFactory)
        {
            _sudokuPrinter = sudokuPrinter;
            _sudokuSolver = sudokuSolver;
            _sudokuFactory = sudokuFactory;
        }

        public void Run()
        {
            Sudoku? sudoku = _sudokuFactory.FromConsole();

            Console.WriteLine();
            if (sudoku is null) return;

            _sudokuSolver.SolveByBacktracking(sudoku);

            if (sudoku.IsSolved)
            {
                _sudokuPrinter.Print(sudoku);
            }
            else
            {
                Console.WriteLine("Cannot solve the sudoku");
            }
        }
    }
}
