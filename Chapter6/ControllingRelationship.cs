using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive;
using Utilities;

namespace Chapter6
{
    class ControllingRelationship
    {

        public void Delay()
        {
            Console.WriteLine("Creating the observable pipeline at {0}", DateTime.Now);
            Observable.Range(1, 5)
                            .DelaySubscription(TimeSpan.FromSeconds(5))
                            .Timestamp()
                            .SubscribeConsole("Console");
        }
    }
}
