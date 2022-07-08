using System;
using System.Threading.Tasks;

namespace ObserverPattern
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Using observer pattern ------");

            Subject _subject = new Subject();
            CompositeObserver _compositeObserver = new CompositeObserver(_subject);
            _compositeObserver.AddObserver(new ObserverOne(_subject));
            _compositeObserver.AddObserver(new ObserverTwo(_subject));
            _compositeObserver.Register();

            for(int i = 0; i < 10; i++)
            {
                _subject.MutateState();
                Task.Delay(1000).Wait();
            }

            Console.WriteLine("Using composite command pattern ------");

            Subject newSubject = new Subject();
            ObserverOne observerOne = new ObserverOne(newSubject);
            observerOne.Register();
            ObserverOne observerTwo = new ObserverOne(newSubject);
            observerTwo.Register();

            for (int i = 0; i < 10; i++)
            {
                newSubject.MutateState();
                Task.Delay(1000).Wait();
            }
        }
    }
}
