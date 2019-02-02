using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrade.Domain
{
    class StockInfo
    {
        public string Symbol { get;  }
        public decimal PrevPrice { get; set; }

        public StockInfo(string symbol, decimal price)
        {
            Symbol = symbol;
            PrevPrice = price;
        }
    }
}
