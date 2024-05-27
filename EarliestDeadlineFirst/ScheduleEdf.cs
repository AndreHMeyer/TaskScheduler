using System;
using System.Collections.Generic;

namespace EarliestDeadlineFirst
{
    public class ScheduleEdf
    {
        private static List<Task> tasks = new List<Task>();
        private static bool running = true;

        public static void Add(string name, int priority, int burst, int deadline)
        {
            Task newTask = new Task(name, priority, burst, deadline);
            tasks.Add(newTask);
        }

        public static void Schedule()
        {
            Console.WriteLine("Earliest Deadline First");

            while (tasks.Count > 0)
            {
                // Ordena as tarefas pelo deadline
                tasks.Sort((t1, t2) => t1.Deadline.CompareTo(t2.Deadline));

                // Pega a tarefa com o menor deadline
                Task currentTask = tasks[0];
                tasks.RemoveAt(0);

                int remainingBurst = currentTask.Burst;

                // Executa a tarefa pelo tempo de burst ou até a conclusão
                int executionTime = Math.Min(remainingBurst, CPU.Quantum);
                CPU.Run(currentTask, executionTime);
                remainingBurst -= executionTime;

                // Atualiza o tempo restante da tarefa
                currentTask.Burst = remainingBurst;

                // Se a tarefa ainda tiver burst restante, adiciona de volta à lista para ser reavaliada
                if (remainingBurst > 0)
                {
                    tasks.Add(currentTask);
                }
            }

            running = false;
        }
    }
}
