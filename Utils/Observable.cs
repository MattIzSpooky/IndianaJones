using System;
using System.Collections.Generic;

namespace Utils
{
    public class Observable<T>
    {
        private readonly List<IObserver<T>> _observers = new List<IObserver<T>>();
        private T _subject;

        public T Subject
        {
            get => _subject;
            set 
            {
                _subject = value;
                Notify();
            }
        }

        public void Register(IObserver<T> observer)
        {
            _observers.Add(observer);
        }

        public void Unregister(IObserver<T> observer)
        {
            _observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(_subject);
            }
        }
    }
}