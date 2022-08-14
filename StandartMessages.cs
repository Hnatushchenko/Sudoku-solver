using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_solver
{
    public class StandartMessages : IStandartMessages
    {
        public string? WrongValue { get; } = "The value has to be in the range from 1 to 9";
    }
}
