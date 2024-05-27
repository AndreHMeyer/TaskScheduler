using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarliestDeadlineFirst
{
    public class Task
    {
        public string Name { get; set; }
        public int Priority { get; set; }
        public int Burst { get; set; }
        public int Deadline { get; set; }

        public Task(string name, int priority, int burst, int deadline)
        {
            Name = name;
            Priority = priority;
            Burst = burst;
            Deadline = deadline;
        }
    }
}
