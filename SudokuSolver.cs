using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_solver
{
    internal class SudokuSolver : ISudokuSolver
    {
        public bool SolveByBacktracking(Sudoku sudoku)
        {
            ICell? emptyCell = sudoku.GetEmptyCell();
            if (emptyCell is null) return true;

            for (int value = 1; value <= 9; value++)
            {
                if (sudoku.ValueIsPossble(emptyCell, value))
                {
                    emptyCell.Value = value;
                    if (SolveByBacktracking(sudoku) == true) return true;
                }
            }
            emptyCell.Value = null;
            return false;
        }
    }
}
