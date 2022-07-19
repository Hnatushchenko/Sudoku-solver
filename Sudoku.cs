using System;
using System.Linq;
using System.Collections.Generic;


namespace Sudoku_solver
{
    class Sudoku
    {
        public bool IsValid
        {
            get
            {
                for (int index = 0; index < 9; index++)
                {
                    var row = _cellsTable[index];
                    var column = GetColumnByIndex(index);
                    var square = GetSquareByIndex(index);

                    for (int value = 1; value <= 9; value++)
                    {
                        if (row.Count(cell => cell.Value == value) > 1) return false;
                        if (column.Count(cell => cell.Value == value) > 1) return false;
                        if (square.Count(cell => cell.Value == value) > 1) return false;
                    }
                }
                return true;
            }
        }

        public bool IsSolved
        {
            get
            {
                foreach (List<Cell> row in _cellsTable)
                {
                    if (row.Any(cell => cell.Value == null)) return false;
                }
                return true;
            }
        }

        private List<List<Cell>> _cellsTable;
        public event Action<string>? OutputPrintingEventHandler;      

        public Sudoku()
        {
            _cellsTable = new List<List<Cell>>();
        }

        public static Sudoku? FromConsole()
        {
            Sudoku? sudoku = new Sudoku();

            Console.WriteLine("Enter the values:");
            for (int i = 0; i < 9; i++)
            {
                sudoku._cellsTable.Add(new List<Cell>());

                string?[]? inputValues = Console.ReadLine()?.Split();

                for (int j = 0; j < 9; j++)
                {
                    sudoku._cellsTable[i].Add(new Cell());
                    try
                    {
                        sudoku._cellsTable[i][j].Value = Convert.ToInt32(inputValues[j]);
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

        private List<Cell> GetAllCellsFromSquare(int rowIndex, int columnIndex)
        {
            int leftTopCornerRow;
            if (rowIndex <= 2) leftTopCornerRow = 0;
            else if (rowIndex <= 5) leftTopCornerRow = 3;
            else leftTopCornerRow = 6;

            int leftTopCornerColumn;
            if (columnIndex <= 2) leftTopCornerColumn = 0;
            else if (columnIndex <= 5) leftTopCornerColumn = 3;
            else leftTopCornerColumn = 6;

            List<Cell> cells = new List<Cell>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    cells.Add(_cellsTable[leftTopCornerRow + i][leftTopCornerColumn + j]);
                }
            }
            return cells;
        }
        
        private static List<int> GetMissingValuesForList(List<Cell> listOfCells)
        {
            List<int> result = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9};
            foreach (Cell cell in listOfCells)
            {
                result.Remove(cell.Value ?? 0);
            }
            return result;
        }

        private static List<Cell> GetEmptyCellsFromList(List<Cell> listOfCells)
        {
            List<Cell> listWithEmptyCells = new List<Cell>(listOfCells);
            listWithEmptyCells.RemoveAll(cell => cell.Value is not null);
            return listWithEmptyCells;
        }

        private List<Cell> GetColumnByIndex(int index)
        {
            List<Cell> column = new List<Cell>();
            foreach (List<Cell> row in _cellsTable)
            {
                column.Add(row[index]);
            }
            return column;
        }

        private List<Cell> GetSquareByIndex(int index)
        {
            (int leftTopCornerRow, int leftTopCornerColumn) leftTopCoordinates;

            switch (index)
            {
                case 0:
                    leftTopCoordinates = (0, 0);
                    break;
                case 1:
                    leftTopCoordinates = (0, 3);
                    break;
                case 2:
                    leftTopCoordinates = (0, 6);
                    break;
                case 3:
                    leftTopCoordinates = (3, 0);
                    break;
                case 4:
                    leftTopCoordinates = (3, 3);
                    break;
                case 5:
                    leftTopCoordinates = (3, 6);
                    break;
                case 6:
                    leftTopCoordinates = (6, 0);
                    break;
                case 7:
                    leftTopCoordinates = (6, 3);
                    break;
                case 8:
                    leftTopCoordinates = (6, 6);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            List<Cell> square = new List<Cell>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    square.Add(_cellsTable[leftTopCoordinates.leftTopCornerRow + i][leftTopCoordinates.leftTopCornerColumn + j]);
                }
            }
            return square;
        }

        private List<Cell>? GetRowByCell(Cell cell)
        {
            foreach (List<Cell> row in _cellsTable)
            {
                if (row.Contains(cell))
                {
                    return row;
                }
            }
            return null;
        }

        private List<Cell>? GetColumnByCell(Cell cell)
        {
            for (int columnIndex = 0; columnIndex < 9; columnIndex++)
            {
                if (GetColumnByIndex(columnIndex).Contains(cell))
                {
                    return GetColumnByIndex(columnIndex);
                }
            }
            return null;
        }

        private List<Cell>? GetSquareByCell(Cell cell)
        {
            for (int squareIndex = 0; squareIndex < 9; squareIndex++)
            {
                if (GetSquareByIndex(squareIndex).Contains(cell))
                {
                    return GetSquareByIndex(squareIndex);
                }
            }
            return null;
        }

        private Cell? GetEmptyCell()
        {
            foreach (List<Cell> row in _cellsTable)
            {
                foreach (Cell cell in row)
                {
                    if (cell.Value is null) return cell;
                }
            }
            return null;
        }

        private bool ValueIsPossble(Cell cellArg, int value)
        {
            List<int?> reservedValues = new List<int?>();

            List<Cell>? row = GetRowByCell(cellArg);
            List<Cell>? column = GetColumnByCell(cellArg);
            List<Cell>? square = GetSquareByCell(cellArg);

            if (row is null || column is null || square is null)
            {
                throw new ArgumentException("Given cell does not belong to any list of cells");
            }
 
            foreach (Cell cell in row)
            {
                if (!reservedValues.Contains(cell.Value))
                {
                    reservedValues.Add(cell.Value);
                }
            }
              
            foreach (Cell cell in column)
            {
                if (!reservedValues.Contains(cell.Value))
                {
                    reservedValues.Add(cell.Value);
                }
            }
            
            foreach (Cell cell in square)
            {
                if (!reservedValues.Contains(cell.Value))
                {
                    reservedValues.Add(cell.Value);
                }
            }

            if (reservedValues.Contains(value)) return false;
            return true;
        }

        public void Print()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    OutputPrintingEventHandler?.Invoke($"{_cellsTable[i][j].Value ?? 0} ");
                }
                OutputPrintingEventHandler?.Invoke("\n");
            }
        }

        /*
         * This function does not work properly yet. 
         * It is supposed to solve the sudoku using algorithms that people use to solve a sudoku.
        */
        public bool Solve()
        {
            bool newNumberWasAdded = false;
            int iteration = 0;
            do
            {
                newNumberWasAdded = false;
                iteration++;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (_cellsTable[i][j].Value is not null) continue;
                        
                        List<int?> reservedValues = new List<int?>();
                        foreach (Cell cell in _cellsTable[i])
                        {
                            if (!reservedValues.Contains(cell.Value))
                            {
                                reservedValues.Add(cell.Value);
                            }
                        }
                        for (int k = 0; k < 9; k++)
                        {
                            if (!reservedValues.Contains(_cellsTable[k][j].Value))
                            {
                                reservedValues.Add(_cellsTable[k][j].Value);
                            }
                        }
                        foreach (Cell cell in GetAllCellsFromSquare(i, j))
                        {
                            if (!reservedValues.Contains(cell.Value))
                            {
                                reservedValues.Add(cell.Value);
                            }
                        }
                        foreach (int? value in reservedValues)
                        {
                            _cellsTable[i][j].RemoveFromPossibleValues(value, ref newNumberWasAdded);
                        }

                        foreach (List<Cell> row in _cellsTable)
                        {
                            foreach (int value in GetMissingValuesForList(row))
                            {
                                List<Cell> listWithEmptyCells = GetEmptyCellsFromList(row);
                                foreach (Cell cell in listWithEmptyCells.ToList())
                                {
                                    if (cell.IsValuePossible(value) == false)
                                    {
                                        listWithEmptyCells.Remove(cell);
                                    }
                                }
                                if (listWithEmptyCells.Count == 1)
                                {
                                    listWithEmptyCells[0].Value = value;
                                    newNumberWasAdded = true;
                                }
                            }
                        }

                        for (int columnIndex = 0; columnIndex < 9; columnIndex++)
                        {
                            List<Cell> column = GetColumnByIndex(columnIndex);
                            foreach (int value in GetMissingValuesForList(column))
                            {
                                List<Cell> listWithEmptyCells = GetEmptyCellsFromList(column);
                                foreach (Cell cell in listWithEmptyCells.ToList())
                                {
                                    if (cell.IsValuePossible(value) == false)
                                    {
                                        listWithEmptyCells.Remove(cell);
                                    }
                                }
                                if (listWithEmptyCells.Count == 1)
                                {
                                    listWithEmptyCells[0].Value = value;
                                    newNumberWasAdded = true;
                                }
                            }
                        }

                        for (int squareIndex = 0; squareIndex < 9; squareIndex++)
                        {
                            List<Cell> square = GetSquareByIndex(squareIndex);
                            foreach (int value in GetMissingValuesForList(square))
                            {
                                List<Cell> listWithEmptyCells = GetEmptyCellsFromList(square);
                                foreach (Cell cell in listWithEmptyCells.ToList())
                                {
                                    if (cell.IsValuePossible(value) == false)
                                    {
                                        listWithEmptyCells.Remove(cell);
                                    }
                                }
                                if (listWithEmptyCells.Count == 1)
                                {
                                    listWithEmptyCells[0].Value = value;
                                    newNumberWasAdded = true;
                                }
                            }
                        }                 
                    }
                }
                if (IsSolved)
                {
                    OutputPrintingEventHandler?.Invoke("The sudoku was solved\n");
                    OutputPrintingEventHandler?.Invoke($"Number of iterations = {iteration}\n");                   
                    return true;
                }
            } while (newNumberWasAdded);

            OutputPrintingEventHandler?.Invoke("Cannot solve the sudoku\n");
            OutputPrintingEventHandler?.Invoke($"Number of iterations = {iteration}\n");
            return false;
        }

        public bool SolveByBacktracking()
        {
            Cell? emptyCell = GetEmptyCell();
            if (emptyCell is null) return true;

            for (int value = 1; value <= 9; value++)
            {
                if (ValueIsPossble(emptyCell, value))
                {
                    emptyCell.Value = value;                  
                    if (SolveByBacktracking() == true) return true;
                }                
            }
            emptyCell.Value = null;
            return false;
        }
    }
}
