using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrade.Domain
{
    class StockTicker : IStockTicker
    {
        public event EventHandler<StockTick> StockTick;

        public void Notify(StockTick tick)
        {
            StockTick(this, tick);
        }
    }

}
