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
                foreach (List<ICell> row in _cellsTable)
                {
                    if (row.Any(cell => cell.Value == null)) return false;
                }
                return true;
            }
        }

        private List<List<ICell>> _cellsTable;   

        public ICell GetCell(int row, int column) => _cellsTable[row][column];

        public Sudoku(CellFactory cellFactory)
        {
            _cellsTable = new List<List<ICell>>();
            for (int i = 0; i < 9; i++)
            {
                _cellsTable.Add(new List<ICell>());
                for (int j = 0; j < 9; j++)
                {
                    _cellsTable[i].Add(cellFactory.Create());
                }
            }
        }  

        private List<ICell> GetAllCellsFromSquare(int rowIndex, int columnIndex)
        {
            int leftTopCornerRow;
            if (rowIndex <= 2) leftTopCornerRow = 0;
            else if (rowIndex <= 5) leftTopCornerRow = 3;
            else leftTopCornerRow = 6;

            int leftTopCornerColumn;
            if (columnIndex <= 2) leftTopCornerColumn = 0;
            else if (columnIndex <= 5) leftTopCornerColumn = 3;
            else leftTopCornerColumn = 6;

            List<ICell> cells = new List<ICell>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    cells.Add(_cellsTable[leftTopCornerRow + i][leftTopCornerColumn + j]);
                }
            }
            return cells;
        }
        
        private static List<int> GetMissingValuesForList(List<ICell> listOfCells)
        {
            List<int> result = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9};
            foreach (ICell cell in listOfCells)
            {
                result.Remove(cell.Value ?? 0);
            }
            return result;
        }

        private static List<ICell> GetEmptyCellsFromList(List<ICell> listOfCells)
        {
            List<ICell> listWithEmptyCells = new List<ICell>(listOfCells);
            listWithEmptyCells.RemoveAll(cell => cell.Value is not null);
            return listWithEmptyCells;
        }

        private List<ICell> GetColumnByIndex(int index)
        {
            List<ICell> column = new List<ICell>();
            foreach (List<ICell> row in _cellsTable)
            {
                column.Add(row[index]);
            }
            return column;
        }

        private List<ICell> GetSquareByIndex(int index)
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
            List<ICell> square = new List<ICell>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    square.Add(_cellsTable[leftTopCoordinates.leftTopCornerRow + i][leftTopCoordinates.leftTopCornerColumn + j]);
                }
            }
            return square;
        }

        private List<ICell>? GetRowByCell(ICell cell)
        {
            foreach (List<ICell> row in _cellsTable)
            {
                if (row.Contains(cell))
                {
                    return row;
                }
            }
            return null;
        }

        private List<ICell>? GetColumnByCell(ICell cell)
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

        private List<ICell>? GetSquareByCell(ICell cell)
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

        public ICell? GetEmptyCell()
        {
            foreach (List<ICell> row in _cellsTable)
            {
                foreach (ICell cell in row)
                {
                    if (cell.Value is null) return cell;
                }
            }
            return null;
        }

        public bool ValueIsPossble(ICell cellArg, int value)
        {
            List<int?> reservedValues = new List<int?>();

            List<ICell>? row = GetRowByCell(cellArg);
            List<ICell>? column = GetColumnByCell(cellArg);
            List<ICell>? square = GetSquareByCell(cellArg);

            if (row is null || column is null || square is null)
            {
                throw new ArgumentException("Given cell does not belong to any list of cells");
            }
 
            foreach (ICell cell in row)
            {
                if (!reservedValues.Contains(cell.Value))
                {
                    reservedValues.Add(cell.Value);
                }
            }
              
            foreach (ICell cell in column)
            {
                if (!reservedValues.Contains(cell.Value))
                {
                    reservedValues.Add(cell.Value);
                }
            }
            
            foreach (ICell cell in square)
            {
                if (!reservedValues.Contains(cell.Value))
                {
                    reservedValues.Add(cell.Value);
                }
            }

            if (reservedValues.Contains(value)) return false;
            return true;
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
                        foreach (ICell cell in _cellsTable[i])
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
                        foreach (ICell cell in GetAllCellsFromSquare(i, j))
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

                        foreach (List<ICell> row in _cellsTable)
                        {
                            foreach (int value in GetMissingValuesForList(row))
                            {
                                List<ICell> listWithEmptyCells = GetEmptyCellsFromList(row);
                                foreach (ICell cell in listWithEmptyCells.ToList())
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
                            List<ICell> column = GetColumnByIndex(columnIndex);
                            foreach (int value in GetMissingValuesForList(column))
                            {
                                List<ICell> listWithEmptyCells = GetEmptyCellsFromList(column);
                                foreach (ICell cell in listWithEmptyCells.ToList())
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
                            List<ICell> square = GetSquareByIndex(squareIndex);
                            foreach (int value in GetMissingValuesForList(square))
                            {
                                List<ICell> listWithEmptyCells = GetEmptyCellsFromList(square);
                                foreach (ICell cell in listWithEmptyCells.ToList())
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
                    //OutputPrintingEventHandler?.Invoke("The sudoku was solved\n");
                    //OutputPrintingEventHandler?.Invoke($"Number of iterations = {iteration}\n");                   
                    return true;
                }
            } while (newNumberWasAdded);

            //OutputPrintingEventHandler?.Invoke("Cannot solve the sudoku\n");
            //OutputPrintingEventHandler?.Invoke($"Number of iterations = {iteration}\n");
            return false;
        }
    }
}
