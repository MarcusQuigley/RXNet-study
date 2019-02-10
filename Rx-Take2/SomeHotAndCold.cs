using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Rx_Take2
{
    class SomeHotAndColdObservables
    {
        public async void SimpleTest()
        {
            var coldObservables = Observable.Create<string>(async o => 
            {
                o.OnNext("Hello");
                await Task.Delay(TimeSpan.FromSeconds(1));
                o.OnNext("Rx");
            });

            coldObservables.SubscribeConsole("o1");
            await Task.Delay(TimeSpan.FromSeconds(0.5));
            coldObservables.SubscribeConsole("o2");
        }

    }
}
