using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarliestDeadlineFirst
{
    public class CPU
    {
        public const int Quantum = 10;

        public static void Run(Task task, int slice)
        {
            Console.WriteLine($"Running task = [{task.Name}] [{task.Priority}] [{task.Burst}] for {slice} units.");
        }
    }
}
