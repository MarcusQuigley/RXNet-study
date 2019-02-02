using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Text;
using System.Threading.Tasks;

namespace Rx_Take2
{
    class NumbersObservable : IObservable<int>
    {
        readonly int _amount;

        public NumbersObservable(int amount)
        {
            _amount = amount;
        }
        public IDisposable Subscribe(IObserver<int> observer)
        {
            for (int i = 0; i < _amount; i++)
            {
                observer.OnNext(i);
            }
            observer.OnCompleted();
            return Disposable.Empty;
        }
    }
}
