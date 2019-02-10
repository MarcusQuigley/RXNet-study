using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class Extensions
    {
        public static IDisposable SubscribeConsole<T>(this IObservable<T> observable, string consoleName = "console")
        {
            var observer = new ConsoleObserver<T>(consoleName);
            return observable.Subscribe(observer);
        }

    }
}
