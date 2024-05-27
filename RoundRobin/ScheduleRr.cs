using System;
using System.Collections.Generic;
using System.Threading;

namespace RoundRobin
{
    public class ScheduleRr
    {
        public const int MinPriority = 1;
        public const int MaxPriority = 5;
        public const int Quantum = 10;
        public const int RealTimeSleep = 20;

        private static Queue<Task>[] readyTaskQueues; //Filas por prioridade
        private static HashSet<Task> realTimeTasks = new HashSet<Task>();
        private static bool globalTimeRunning = true;

        static ScheduleRr()
        {
            readyTaskQueues = new Queue<Task>[MaxPriority];
            for (int i = 0; i < MaxPriority; i++)
            {
                readyTaskQueues[i] = new Queue<Task>();
            }

            //Inicia a segunda thread para simular o contador de tempo global
            Thread globalTimeThread = new Thread(GlobalTimeCounter);
            globalTimeThread.IsBackground = true;
            globalTimeThread.Start();
        }

        public static void Add(string name, int priority, int burst, bool isRealTime = false)
        {
            ValidatePriority(priority);

            Task newTask = new Task
            {
                Name = name,
                Priority = priority,
                Burst = burst,
                IsRealTime = isRealTime
            };

            readyTaskQueues[priority - 1].Enqueue(newTask);

            if (isRealTime)
            {
                realTimeTasks.Add(newTask);
            }
        }

        public static void Schedule()
        {
            Console.WriteLine("Round-Robin {Nome da Tarefa} - {Prioridade} - {Burst} - {Slice}\n");

            TimerSimulator.Start(); // Inicia o timer

            while (TasksRemaining())
            {
                //Percorre as filas de menor prioridade para maior
                for (int i = 0; i < MaxPriority; i++)
                {
                    Queue<Task> queue = readyTaskQueues[i];
                    if (queue.Count > 0)
                    {
                        Task task = queue.Dequeue();
                        int slice = Math.Min(Quantum, task.Burst);

                        CPU.Run(task, slice);

                        task.Burst -= slice;

                        //Remove a tarefa da lista se já terminou
                        if (task.Burst > 0)
                        {
                            queue.Enqueue(task); //Reenfileira a tarefa
                        }

                        //Verifica se a tarefa é real-time e se o tempo de sleep terminou
                        if (task.IsRealTime && TimerSimulator.GetCurrentTime() % RealTimeSleep == 0)
                        {
                            Console.WriteLine($"Real-time task {task.Name} interrupted!");
                            queue.Enqueue(task);
                        }

                        break;
                    }
                }
            }

            TimerSimulator.Stop(); //Para o timer quando todas as tarefas forem executadas
            globalTimeRunning = false; //Sinaliza a thread de tempo global para parar
        }

        private static void GlobalTimeCounter()
        {
            int currentTime = 0;

            while (globalTimeRunning)
            {
                Thread.Sleep(1); //Incrementa o contador de tempo global a cada milissegundo
                currentTime++;

                //Verifica se alguma tarefa real-time precisa ser interrompida
                if (currentTime % RealTimeSleep == 0)
                {
                    foreach (Task realTimeTask in realTimeTasks)
                    {
                        //Se a tarefa real-time ainda estiver na lista, remove para simular a interrupção
                        if (readyTaskQueues[realTimeTask.Priority - 1].Contains(realTimeTask))
                        {
                            readyTaskQueues[realTimeTask.Priority - 1].Enqueue(realTimeTask);
                        }
                    }
                }
            }
        }

        private static bool TasksRemaining()
        {
            //Verifica se ainda há tarefas nas filas
            for (int i = 0; i < MaxPriority; i++)
            {
                if (readyTaskQueues[i].Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        private static void ValidatePriority(int priority)
        {
            if (priority < MinPriority || priority > MaxPriority)
            {
                throw new ArgumentException("Invalid priority");
            }
        }
    }
}