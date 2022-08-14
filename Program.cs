using System;
using Autofac;

namespace Sudoku_solver
{   
    class Program
    {
        static void Main()
        {
            IContainer container = ContainerConfig.Configure();
            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IApplication>();
                app.Run();
            }
        }
    }
}
