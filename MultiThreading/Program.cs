using System;
using System.Threading;

namespace MultiThreading
{
    //[System.Runtime.Remoting.Contexts.Synchronization]
    public class Singeton //:System.ContextBoundObject
    {
        object _syncMutateState = new object();
        object _syncReadState = new object();

        int state;
        private Singeton() { }
        public static readonly Singeton Instance = new Singeton();

        // [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
        public void MutateState()
        {
            //Monitor.Enter(_syncMutateState);
            //try
            //{

            //    for (int i = 0; i < 10; i++)
            //    {

            //        state += 1;
            //        Console.WriteLine($"MutateState {state} executing on {Thread.CurrentThread.Name}");
            //        Thread.Sleep(1000);
            //        if (state == 5) { return; }
            //    }
            //}
            //finally
            //{
            //    Monitor.Exit(_syncMutateState);
            //}

            //lock gives same functionality as commented code above
            lock (_syncMutateState)
            {
                for (int i = 0; i < 10; i++)
                {

                    state += 1;
                    Console.WriteLine($"MutateState {state} executing on {Thread.CurrentThread.Name}");
                    Thread.Sleep(1000);
                    if (state == 5) { return; }
                }
            }
        }

        //[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.Synchronized)]
        public void ReadState()
        {
            Monitor.Enter(_syncReadState);
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"ReadState {state} executing on {Thread.CurrentThread.Name}");
                Thread.Sleep(1000);
            }
            Monitor.Exit(_syncReadState);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            new Thread(TaskOne).Start();
            new Thread(TaskOne) { IsBackground = true }.Start();

            new Thread(Singeton.Instance.MutateState) { Name = "T1" }.Start();
            new Thread(Singeton.Instance.ReadState) { Name = "T2" }.Start();
            new Thread(Singeton.Instance.MutateState) { Name = "T3" }.Start();
            new Thread(Singeton.Instance.ReadState) { Name = "T4" }.Start();
        }

        static void TaskOne()
        {
            //Thread _t2 = new Thread(() =>
            //{
            //    for (int i = 0; i < 2; i++)
            //    {
            //        Console.WriteLine($"Task2  and Count:{i}, IsBackground:{Thread.CurrentThread.IsBackground}");
            //        Thread.Sleep(1000);
            //    }
            //});
            //_t2.Start();

            for (int i = 0; i < 10; i++)
            {
                if(i == 4 && !Thread.CurrentThread.IsBackground)
                {
                    Thread.CurrentThread.Suspend();
                }
                Console.WriteLine($"{nameof(TaskOne)} and Count:{i}, IsBackground:{Thread.CurrentThread.IsBackground}");
                Thread.Sleep(1000);
            }
        }
    }
}
