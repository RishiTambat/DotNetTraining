using System;

namespace GCWorkings
{
    class A
    {
        B obj = new B();
    }

    class B
    {

    }

    internal class Program
    {
        static A _globalRef = null;

        static void _Main(string[] args)
        {
            GcTest();
            GC.Collect();
            _globalRef = null;
            GC.Collect(0);
            GC.Collect(1);
            GC.Collect();
        }

        static void GcTest()
        {
            A obj = new A();// SOH ->Gen 0;

            _globalRef = obj;
            obj = null;
            GC.Collect();
        }
    }
}
