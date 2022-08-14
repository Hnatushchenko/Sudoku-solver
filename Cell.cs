using System;
using System.Collections.Generic;


namespace Sudoku_solver
{
    class Cell : ICell
    {
        IValueSetter _valueSetter;

        private int? _value;
        public int? Value
        {
            get => _value;
            set
            {
                _value = _valueSetter.Set(value);
            }
        }

        private List<int?>? possibleValues;

        public Cell (IValueSetter valueSetter)
        {
            _valueSetter = valueSetter;
            Value = null;
            possibleValues = new List<int?>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
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
