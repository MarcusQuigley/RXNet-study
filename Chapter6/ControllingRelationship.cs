using System;
//using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive;
using Utilities;
using System.Threading.Tasks;
using System.Threading;

namespace Chapter6
{
    class ControllingRelationship
    {

        public void Delay()
        {
            Console.WriteLine("Delay example");
            Console.WriteLine("Creating the observable pipeline at {0}", DateTime.Now);
            Observable.Range(1, 5)
                            .DelaySubscription(TimeSpan.FromSeconds(5))
                            .Timestamp()
                            .SubscribeConsole("Console");
        }

        public void CreatingObservers()
        {
            Console.WriteLine("Create observer without leaving screen");
            Observable.Range(1, 3)
                      .Subscribe((i) => Console.WriteLine($"On next({i})"),
                                (e) => Console.WriteLine("exception " + e.Message),
                                 () => Console.WriteLine("completed")
                         );

            //Console.WriteLine("Create observer with bug");

            //Observable.Range(1, 3)
            //        .Select(x => x / (x - 3))
            //        .Subscribe((i) => Console.WriteLine($"On next({i})")
            //                     ,e=>Console.WriteLine($"Error: {e.Message}")
            //         );

            Console.WriteLine("Add asynchronicity");

            Observable.Range(1, 5)
                    .Select(x => Task.Run(() => x / (x - 3)))
                    .Concat()
                    .Subscribe((i) => Console.WriteLine($"On next({i})")
                                 , e => Console.WriteLine($"Error: {e.Message}")

                                );
        }
        public void CreatingObserversWithCancellation()
        {
            var cts = new CancellationTokenSource();
            cts.Token.Register(()=>Console.WriteLine("Subscription cancelled"));
            Observable.Interval(TimeSpan.FromSeconds(1))
                      .Subscribe((i) => Console.WriteLine($"On next({i})")
                                        , e => Console.WriteLine($"Error: {e.Message}")
                                        , () => Console.WriteLine("completed"), cts.Token

                      );
            cts.CancelAfter(TimeSpan.FromSeconds(5));
        }

        public void CreatingObserversInstances()
        {
            Console.WriteLine($"in CreatingObserversInstances---------"+ '\n');
            IObserver<string> observer = Observer.Create<string>(i => Console.WriteLine($"On next({i})"),
                                                                  () => Console.WriteLine("Completed"));
            Observable.Interval(TimeSpan.FromSeconds(1))
                       .Select(i => $"X{i}")
                       .TakeWhile(i=> i!="X10")
                       .Subscribe(observer);

            Observable.Interval(TimeSpan.FromSeconds(2))
                        .Select(x => $"YY{x}")
                        .TakeUntil(new DateTimeOffset(DateTime.Now.AddSeconds(15)))
                        .Subscribe(observer);
        }
        //This is how to delay the subscription 5      seconds:
        public void ControllingObserver()
        {
            //Observable.Range(1, 5)
            //          .Timestamp()
            //          .DelaySubscription(TimeSpan.FromSeconds(5))
            //          .SubscribeConsole("d");
            //     ;


            Observable.Timer(DateTimeOffset.Now,TimeSpan.FromSeconds(1))//.Interval(TimeSpan.FromSeconds(1))
                      .Timestamp()
                      .TakeUntil(DateTimeOffset.Now.AddSeconds(5))
                      .SubscribeConsole();
            
        }
    }
}
