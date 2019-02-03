using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chap5
{
    public class Primes
    {

        public IEnumerable<int> GeneratePrimesOld(int number)
        {
            List<int> _primes = new List<int>(number);
            bool _isPrime = true;
            int _counter = 0;
            for (int i = 2; i < 100; i++)
            {
                _isPrime = true;
                for (int j = i-1; j > 1; j--)
                {
                    if (i%j == 0)
                    {
                        _isPrime = false;
                        break;
                    }
                }
                if (_isPrime)
                {
                    _counter += 1;
                    if (_counter == number)
                        yield break;
                    else
                        yield return i;
                }
                
            }
        }

        //public bool IsNumberPrime(int number)
        //{
        //    for (int i = number-1; i > 1; i--)
        //    {
        //        if (number% i == 0)
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        public bool IsNumberPrime(int number)
        {
            for (int i = number-1; i >1; i--)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public Task<IEnumerable<int>> GeneratePrimesAsync(int number)
        {
            return Task.Run<IEnumerable<int>>(()=>GeneratePrimes(number));
        }

        public IEnumerable<int> GeneratePrimes(int number)
        {
            int _count = number;
            while (_count > 0)
            {
                for (int i = 2; i < int.MaxValue; i++)
                {
                    if (IsNumberPrime(i))
                    {
                        Thread.Sleep(200);
                        yield return i;
                        _count -= 1;
                    }
                    if (_count == 0)
                        yield break;
                }
            }
        }

        public IObservable<int> GeneratePrimesObs(int amount)
        {
            return Observable.Create<int>(o =>
            {
                foreach (var item in GeneratePrimes(amount))
                {
                    o.OnNext(item);
                }
                o.OnCompleted();
                return Disposable.Empty;
            });

        }

        public IObservable<int> GeneratePrimesObs2(int amount)
        {
            //var cts = new CancellationTokenSource();
            return Observable.Create<int>((o,ct) => {
               return Task.Run(() =>
                {
                    foreach (var item in GeneratePrimes(amount))
                    {
                        //cts.Token.ThrowIfCancellationRequested();
                        ct.ThrowIfCancellationRequested();
                        o.OnNext(item);
                    }
                    o.OnCompleted();
                });
               // return new CancellationDisposable(cts);
            });
        }
    }
}
