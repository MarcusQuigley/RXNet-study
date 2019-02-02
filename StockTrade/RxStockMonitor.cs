using StockTrade.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrade
{
    class RxStockMonitor : IDisposable
    {
        readonly decimal maxChangeRatio = 0.1m;
        IDisposable subscription;

        public RxStockMonitor(IStockTicker ticker)
        {
           // var ticks = GetTicksQuery(ticker);
             var ticks = GetTicksMethod(ticker);
            subscription =
                ticks.Subscribe(change =>
                    {
                        Console.WriteLine("Stock:{0} has changed with {1} ratio, Old Price:{2} New Price:{3}", change.Symbol,
                            change.ChangeRatio,
                            change.OldPrice,
                            change.NewPrice);
                    },
                    ex => { Console.WriteLine($"Error {ex.Message}"); }, //#C
                    () => { Console.WriteLine("Finished"); }); //#C
 
        }

        IObservable<DrasticChange> GetTicksQuery(IStockTicker ticker) {
              IObservable<StockTick> ticks =
                    Observable.FromEventPattern<EventHandler<StockTick>, StockTick>(
                        h => ticker.StockTick += h, 
                        h => ticker.StockTick -= h) 
                        .Select(tickEvent => tickEvent.EventArgs)
                        .Synchronize();
            
            var drasticChanges =
                from tick in ticks
                group tick by tick.QuoteSymbol
                into company
                from tickPair in company.Buffer(2, 1)
                let changeRatio = Math.Abs((tickPair[1].Price - tickPair[0].Price) / tickPair[0].Price)
                where changeRatio > maxChangeRatio
                select new DrasticChange()
                {
                    Symbol = company.Key,
                    ChangeRatio = changeRatio,
                    OldPrice = tickPair[0].Price,
                    NewPrice = tickPair[1].Price
                };

            return drasticChanges;
        }

        IObservable<DrasticChange> GetTicksMethod(IStockTicker ticker) {

            return Observable.FromEventPattern<EventHandler<StockTick>, StockTick>(
                         h => ticker.StockTick += h,
                         h => ticker.StockTick -= h)
                         .Select(tickEvent => tickEvent.EventArgs)
                         .Synchronize()
                         .GroupBy(tick => tick.QuoteSymbol)   //groups emits by ticker
                         .SelectMany(t => t)
                         .Buffer(2, 1)
                         .Select(l =>
                             new
                             {
                                 symbol = l[0].QuoteSymbol,
                                 changeRatio = Math.Abs(l[1].Price - l[0].Price),
                                 oldPrice = l[0].Price,
                                 newPrice = l[1].Price
                             })
                           .Where(cr => cr.changeRatio > maxChangeRatio)
                         .Select(cr => new DrasticChange()
                         {
                             Symbol = cr.symbol,
                             ChangeRatio = cr.changeRatio,
                             OldPrice = cr.oldPrice,
                             NewPrice = cr.newPrice
                         });

        }


        public void Dispose()
        {
            subscription.Dispose();
        }
    }

    public class DrasticChange
    {
        public decimal NewPrice { get; set; }
        public string Symbol { get; set; }
        public decimal ChangeRatio { get; set; }
        public decimal OldPrice { get; set; }
    }
}
