using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_solver
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<SudokuSolver>().As<ISudokuSolver>();
            builder.RegisterType<Cell>().As<ICell>();
            builder.RegisterType<CellFactory>().SingleInstance();
            builder.RegisterType<SudokuFactory>().SingleInstance();
            builder.RegisterType<SudokuConsolePrinter>().As<ISudokuPrinter>().SingleInstance();
            builder.RegisterType<StandartMessages>().As<IStandartMessages>().SingleInstance();
            builder.RegisterType<ValueSetter>().As<IValueSetter>().SingleInstance();
            return builder.Build();
        }
    }
}
