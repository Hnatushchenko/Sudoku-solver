using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_solver
{
    internal class SudokuFactory
    {
        CellFactory _cellFactory;
        public SudokuFactory(CellFactory cellFactory)
        {
            _cellFactory = cellFactory;
        }

        public Sudoku? FromConsole()
        {
            Sudoku? sudoku = new Sudoku(_cellFactory);

            Console.WriteLine("Enter the values:");
            for (int i = 0; i < 9; i++)
            {
                string?[]? inputValues = Console.ReadLine()?.Split();

                for (int j = 0; j < 9; j++)
                {
                    try
                    {
                        sudoku.GetCell(i, j).Value = Convert.ToInt32(inputValues[j]);
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine($"Format exception: {ex.Message}\n");
                        return null;
                    }
                }
            }
            if (sudoku.IsValid == false)
            {
                throw new ArgumentException("Given sudoku cannot exist");
            }
            return sudoku;
        }
    }
}
