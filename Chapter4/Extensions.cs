using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4
{
 public static   class Extensions
    {
        public static IDisposable SubscribeConsole<T>(this IObservable<T> observable, string name = "")
        {
            ConsoleObserver<T> consoleObserver = new ConsoleObserver<T>(name);
            return observable.Subscribe(consoleObserver);

        }

        public static IObservable<int> ObserveNUmbers(int number)
        {
            return Observable.Create<int>(o =>
            {
                for (int i = 0; i < number; i++)
                {
                    o.OnNext(i);
                }
                o.OnCompleted();
                return Disposable.Empty;
            });
        }
    }
}
