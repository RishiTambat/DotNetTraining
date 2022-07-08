using System;
using System.Collections.Generic;

namespace ObserverPattern
{
    #region Interfaces

    internal interface IObserver
    {
        void Update();
    }

    internal interface ISubject
    {
        void Subscribe(IObserver observer);
        void UnSubscribe(IObserver observer);
        object GetUpdates();
    }

    #endregion

    #region Observers

    class ObserverOne : IObserver
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
            _subject.Subscribe(this);
        }
    }

    class ObserverTwo : IObserver
    {
        readonly ISubject _subject;

        public ObserverTwo(ISubject subject)
        {
            _subject = subject;
        }

        public void Update()
        {
            Console.WriteLine($"{nameof(ObserverTwo)} Notified , NewState : {_subject.GetUpdates()}");
        }

        public void Register()
        {
            _subject.Subscribe(this);
        }
    }

    class CompositeObserver : IObserver
    {
        readonly ISubject _subject = null;
        List<IObserver> _observers = new List<IObserver>();

        public CompositeObserver(ISubject subject)
        {
            _subject = subject;
        }

        public void Update()
        {
            Console.WriteLine($"{nameof(CompositeObserver)} Notified NewState : {_subject.GetUpdates()}");

            //Multiplex - one -> many
            for (int i = 0; i < _observers.Count; i++)
            {
                _observers[i].Update();
            }
        }

        public void AddObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Register()
        {
            _subject.Subscribe(this);
        }
    }

    #endregion

    //Subject as Observable
    internal class Subject : ISubject
    {
        int _state;
        List<IObserver> _observers = new List<IObserver>();

        public object GetUpdates()
        {
            return _state;
        }

        //Hook
        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        //unHook
        public void UnSubscribe(IObserver observer)
        {
            //Reference Equivalance
            _observers.Remove(observer);
        }

        public void MutateState()
        {
            _state = new Random().Next();
            Notify();
        }

        void Notify()
        {
            //Iterator 
            for (int i = 0; i < _observers.Count; i++)
            {
                _observers[i].Update();
            }
        }
    }
}
