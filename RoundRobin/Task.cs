using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundRobin
{
    public class Task
    {
        public string Name { get; set; }
        public int Tid { get; set; }
        public int Priority { get; set; }
        public int Burst { get; set; }
        public bool IsRealTime { get; set; }
    }
}