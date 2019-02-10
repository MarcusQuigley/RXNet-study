using Chapter4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
namespace Chap5
{
    class PeriodicBehaviour
    {
        public PeriodicBehaviour(IService service)
        {
            MyService = service;
        }
        public IService MyService { get;}
        public IDisposable GetIntervalData()
        {
            var subscription = Observable.Interval(TimeSpan.FromSeconds(.5))
                                         .Select((i) => MyService.GetData())
                                         .SubscribeConsole("IntervalConsole");
                                         ;
            return subscription;
         
        }

        public IDisposable GetTimerIntervalData()
        {
            //IObservable<string> first = Observable.Interval(TimeSpan.FromSeconds(1))
            //                                      .Select(x => $"value: {x}");

            //var immediateObservable = Observable.Return(first);

            //immediateObservable.Merge()
            //                    .Timestamp()
            //                    .SubscribeConsole("timer switch");


            var subsription = Observable.Timer(TimeSpan.FromSeconds(3),TimeSpan.FromSeconds(1))
                                        .Timestamp()
                                        .Select(x => x)
                                        .SubscribeConsole("timer switch");

            return subsription;
        }
    }


    public interface IService
    {
        int GetData();
    }
    class SomeService : IService
    {
        Random r = new Random();

        public int GetData()
        {
            return r.Next(1000);
        }
    }
}
