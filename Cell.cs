using System;
using System.Collections.Generic;


namespace Sudoku_solver
{
    class Cell
    {
        private int? _value;
        public int? Value
        {
            get => _value;
            set
            {
                if (value == 0 || value == null)
                {
                    _value = null;
                }
                else if (value > 0 && value < 10)
                {
                    _value = value;
                    possibleValues = null;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("The value has to be in the range from 1 to 9");
                }
            }
        }

        private List<int?>? possibleValues;

        public Cell()
        {
            Value = null;
            possibleValues = new List<int?>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }

        public Cell(int? value)
        {
            Value = value;
            possibleValues = null;
        }

        public void RemoveFromPossibleValues(int? value, ref bool exactValueFound)
        {
            possibleValues?.Remove(value);
            if (possibleValues?.Count == 1)
            {
                Value = possibleValues[0];
                possibleValues = null;
                exactValueFound = true;
            }
        }
        public bool IsValuePossible(int value)
        {
            if (possibleValues is not null && possibleValues.Contains(value)) return true;
            return false;
        }
    }
}
