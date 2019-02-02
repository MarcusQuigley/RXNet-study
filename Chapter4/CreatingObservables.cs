using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4.CreatingObservables
{
    class NumbersObservable : IObservable<int>
    {
        int _amount;
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
