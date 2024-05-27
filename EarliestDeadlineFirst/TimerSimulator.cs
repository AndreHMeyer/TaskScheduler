using System;
using System.Threading;

namespace EarliestDeadlineFirst
{
    public class TimerSimulator
    {
        private static readonly object lockObject = new object();
        private static bool running = false;
        private static int currentTime = 0;

        public static void Start()
        {
            running = true;
            Thread timerThread = new Thread(Run);
            timerThread.IsBackground = true;
            timerThread.Start();
        }

        public static void Stop()
        {
            running = false;
        }

        private static void Run()
        {
            while (running)
            {
                // Intervalo de tempo simulado ajustado para 1 unidade de tempo
                Thread.Sleep(1000); // 1 segundo para simular 1 unidade de tempo
                lock (lockObject)
                {
                    currentTime++;
                }
            }
        }

        public static int GetCurrentTime()
        {
            lock (lockObject)
            {
                return currentTime;
            }
        }
    }
}
