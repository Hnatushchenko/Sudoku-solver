using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_solver
{
    internal class CellFactory
    {
        IValueSetter _valueSetter;
        public CellFactory(IValueSetter valueSetter)
        {
            _valueSetter = valueSetter;
        }

        public ICell Create()
        {
            return new Cell(_valueSetter);
        }
    }
}
