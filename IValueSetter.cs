using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_solver
{
    public interface IValueSetter
    {
        int? Set(int? value);
    }
}
