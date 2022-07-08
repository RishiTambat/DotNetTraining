using System;
namespace GCWorkings.ResourceManagement
{
    public enum ResourceState
    {
        FREE,
        BUSY
    }

    public class Resource
    {
        public static readonly Resource Instance = new Resource();
        private Resource() { }
        public ResourceState State = ResourceState.FREE;
    }

    public class A : IDisposable
    {
        public static System.Threading.AutoResetEvent _handle = new System.Threading.AutoResetEvent(false);

        public A()
        {
            lock (A._handle)
            {
                if (Resource.Instance.State == ResourceState.FREE)
                {
                    Console.WriteLine($"Resource Owned By {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                }
                else
                {
                    Console.WriteLine($"Resource Awaited By {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                    //wait
                    _handle.WaitOne();
                    Console.WriteLine($"Resource Owned By {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                }
                Resource.Instance.State = ResourceState.BUSY;
            }
        }

        public void Dispose()
        {
            Resource.Instance.State = ResourceState.FREE;
            Console.WriteLine($"Released Resource By {System.Threading.Thread.CurrentThread.ManagedThreadId}");
            _handle.Set();
        }

        public void UseResource()
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Resource Used By {System.Threading.Thread.CurrentThread.ManagedThreadId}");
                System.Threading.Thread.Sleep(1000);
            }
        }
    }

    class Program
    {
        static void Main()
        {
            new System.Threading.Thread(Client).Start();
            new System.Threading.Thread(Client).Start();
        }
        static void Client()
        {
            A obj = null;
            obj = new A();
            obj.UseResource();
            obj.Dispose();
            obj = null;
            GC.Collect();
        }
    }
}
