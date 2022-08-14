using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_solver
{
    internal class ValueSetter : IValueSetter
    {
        IStandartMessages _standartMessages;
        public ValueSetter(IStandartMessages standartMessages)
        {
            _standartMessages = standartMessages;
        }

        public int? Set(int? value)
        {
            if (value == 0 || value == null)
            {
                return null;
            }
            else if (value > 0 && value < 10)
            {
                return value;
            }
            else
            {
                throw new ArgumentOutOfRangeException(_standartMessages.WrongValue);
            }
        }
    }
}
