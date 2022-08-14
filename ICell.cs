namespace Sudoku_solver
{
    internal interface ICell
    {
        int? Value { get; set; }

        bool IsValuePossible(int value);
        void RemoveFromPossibleValues(int? value, ref bool exactValueFound);
    }
}