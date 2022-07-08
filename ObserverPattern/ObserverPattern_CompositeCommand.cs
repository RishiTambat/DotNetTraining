using System;

namespace ObserverPattern_CompositeCommand
{
    #region Interface

    internal interface ISubject
    {
        event Action Observer;
        object GetUpdates();
    }

    #endregion

    #region Observers

    class ObserverOne
    {
        readonly ISubject _subject;

        public ObserverOne(ISubject subject)
        {
            _subject = subject;
        }

        public void Update()
        {
            Console.WriteLine($"{nameof(ObserverOne)} Notified NewState : {_subject.GetUpdates()}");
        }

        public void Register()
        {
            _subject.Observer += new Action(Update);
        }
    }

    class ObserverTwo
    {
        readonly ISubject _subject;

        public ObserverTwo(ISubject subject)
        {
            _subject = subject;
        }

        public void update()
        {
            Console.WriteLine($"{nameof(ObserverTwo)} Notified , NewState : {_subject.GetUpdates()}");
        }

        public void Register()
        {
            _subject.Observer += new Action(update);
        }
    }

    #endregion

    //Subject as Observable
    internal class Subject : ISubject
    {
        int _state;
        public event Action Observer = null;

        public object GetUpdates()
        {
            return _state;
        }

        public void MutateState()
        {
            _state = new Random().Next();
            Notify();
        }

        void Notify()
        {
            if (Observer != null)
            {
                //Multicast
                Observer.Invoke();
            }
        }
    }
}
