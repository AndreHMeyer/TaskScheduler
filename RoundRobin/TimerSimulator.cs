using System;
using System.Threading;

namespace RoundRobin
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
                Thread.Sleep(2);
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