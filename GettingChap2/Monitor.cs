using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingChap2
{
    class Monitor
    {
        private readonly StockTicker _ticker;

        public Monitor(StockTicker ticker)
        {
            _ticker = ticker;
            _ticker.Ticked += _ticker_Ticked;
        }

        private void _ticker_Ticked(object sender, StockTick e)
        {
            Console.WriteLine($"Stock: {e.QuoteSymbol}, Price: {e.Price}.");
        }
    }

    class RXMonitor : IDisposable
    {
        private IDisposable subscription;
        private int ctr=0;

        public RXMonitor(StockTicker ticker)
        {
            Console.WriteLine("Starting RX monitoring");
            var setupTicks = Observable.FromEventPattern<EventHandler<StockTick>, StockTick>(
                h => ticker.Ticked += h,
                h => ticker.Ticked -= h)
                .Select(c => c.EventArgs)
                .Synchronize();

            //var ticks = setupTicks
            //     .Buffer(2, 1)
            //     .Select(l =>  
            //         new
            //         {
            //             symbol = l[0].QuoteSymbol,
            //             change = l[1].Price - l[0].Price,
            //             oldPrice = l[0].Price,
            //             newPrice = l[1].Price
            //         })
            //      .Where(a=>a.change > 1.0m)
            //      ;

            var ticks = setupTicks
                .GroupBy(t=>t.QuoteSymbol)
                .SelectMany(t => t)
                .Buffer(2, 1)        
                .Select(l =>
                     new
                     {
                         symbol = l[0].QuoteSymbol,
                         change = l[1].Price - l[0].Price,
                         oldPrice = l[0].Price,
                         newPrice = l[1].Price
                    })
              //.Where(a => a.change > 1.0m)
                    ;
            subscription = ticks.Subscribe(
                //c =>  Console.WriteLine($"Stock: {c.QuoteSymbol} Price: {c.Price}"),
                c => Console.WriteLine($"{ctr+=1} Stock: {c.symbol} Price: {c.newPrice} old price: {c.oldPrice} difference: {c.change}"),
                ex => Console.WriteLine($"Error: {ex.Message}"),
                () => Console.WriteLine("Completed RX"));
        }
 
        public void Dispose()
        {
            subscription.Dispose();
        }

        void TestGroupBy()
        {
            "Dog,Cat,Cat,Dog,Rabbit,Dog,Cat,Dog,Rabbit"
                .Split(',')
                .GroupBy(s => s)
                .SelectMany(f=>f.ToList())
                .Select(g => g.First());
        }
    }
}
