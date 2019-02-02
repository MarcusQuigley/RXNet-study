using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingChap2
{
    class StockTicker
    {
        public event EventHandler<StockTick> Ticked;

        public void Notify(StockTick tick)
        {
            if (Ticked != null)
            {
                Ticked(this, tick);
            }
        }
    }
}
