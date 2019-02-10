using Chapter4.CreatingObservables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Utilities;
namespace Chapter4
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestCreateObservables();
          var xx = Create(5);
            xx.Subscribe(new ConsoleObserver<int>("SHITE"));
            TestToEnumerable();
            TestGenerate();
            Console.Read();
        }
        static void TestCreateObservables()
        {
            var dis= new NumbersObservable(10).SubscribeConsole("numbers..");
            
        }

        static IObservable<int> Create(int amount)
        {
           return Observable.Create<int>(observer =>
            {
                for (int i = 0; i < amount; i++)
                {
                    observer.OnNext(i);
                }
                observer.OnCompleted();
                return Disposable.Empty;
            });
        }

        static void TestToEnumerable()
        {
            var observable = Observable.Create<int>(observer => {
                for (int i = 0; i < 5; i++)
                {
                    observer.OnNext(i);
                }
                observer.OnCompleted();
                return Disposable.Empty;              

            });
            var enumerable = observable.ToEnumerable();
            foreach (var item in enumerable)
            {
                Console.WriteLine($"{item} in enumerable");
            }
        }

        static void TestGenerate()
        {
            var obsvable = Observable.Generate(0, i => i < 10, i => i += 1, i => i);
            obsvable.SubscribeConsole("Generator");

        }
    }
}
