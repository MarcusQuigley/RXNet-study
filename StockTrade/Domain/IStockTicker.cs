using System;

namespace StockTrade.Domain
{
    interface IStockTicker
    {
        event EventHandler<StockTick> StockTick;
    }
}