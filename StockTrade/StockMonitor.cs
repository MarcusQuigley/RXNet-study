using StockTrade.Domain;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrade
{
    class StockMonitor : IDisposable
    {
        ConcurrentDictionary<string, StockInfo> concurStockInfos = new ConcurrentDictionary<string, StockInfo>();
        Dictionary<string, StockInfo> stockInfos = new Dictionary<string, StockInfo>();
        readonly decimal maxChangeRatio = 0.1m;
        private readonly StockTicker _ticker;
        private readonly object _locker = new object();

        public StockMonitor(StockTicker ticker)
        {
            _ticker = ticker;
            _ticker.StockTick += OnStockTick;
        }

        private void OnStockTick(object sender, StockTick tick)
        {
            StockInfo stockInfo;
            lock (_locker)
            {
                var quoteSymbol = tick.QuoteSymbol;
                var prevQuote = concurStockInfos.TryGetValue(quoteSymbol, out stockInfo);
                if (stockInfo != null)
                {
                    CheckMargin(tick, stockInfo);
                    concurStockInfos[quoteSymbol].PrevPrice = tick.Price;
                }
                else
                {
                    //stockInfos.Add(quoteSymbol, new StockInfo(quoteSymbol, tick.Price));
                    concurStockInfos.AddOrUpdate(quoteSymbol, new StockInfo(quoteSymbol, tick.Price), (k, v) => { return null; });
                }
            }
        }

        private void CheckMargin(StockTick newTick, StockInfo oldStockInfo)
        {
            var ratio = Math.Abs((newTick.Price - oldStockInfo.PrevPrice) / oldStockInfo.PrevPrice); 
            if (ratio> maxChangeRatio)
            {
                Console.WriteLine($"Stock:{newTick.QuoteSymbol} has changed with {ratio} " +
                    $"ratio,Old Price:{ oldStockInfo.PrevPrice} New Price:{newTick.Price}");
             }
        }

        public void Dispose()
        {
            _ticker.StockTick -= OnStockTick;
            concurStockInfos.Clear();
        }
    }
}
