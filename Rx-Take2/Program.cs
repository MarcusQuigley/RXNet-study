using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rx_Take2
{
    class Program
    {
        static void Main(string[] args)
        {
            //AnObservable<int> observable = new AnObservable<int>();
            //observable.Data = Enumerable.Range(1, 5);
            //using (var xx = observable.Subscribe(new AnObserver<int>()))
            //{ }

            //var numbers = new NumbersObservable(10);
            //var subscription = numbers.SubscribeConsole<int>("console");

            new SomeHotAndColdObservables().SimpleTest();

            Console.WriteLine("All done");

            Console.ReadKey();


        }
    }

    class AnObservable<T> : IObservable<T>
    {
        public IEnumerable<T> Data { get; set; }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            using (var enumerator = Data.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    observer.OnNext(enumerator.Current);
                }
                observer.OnCompleted();
            }
            return Disposable.Empty;
        }
    }

    class AnObserver<T> : IObserver<T>
    {
        public void OnCompleted()
        {
            Console.WriteLine("Observerable completed!");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"Observerable error  {error.Message}!");
        }

        public void OnNext(T value)
        {
            Console.WriteLine($"Observerable value: {value.ToString()}");
        }

    }
}
