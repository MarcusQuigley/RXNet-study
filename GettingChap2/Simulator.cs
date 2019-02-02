using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Threads = System.Threading;
 
using System.Timers;

namespace GettingChap2
{
    class Simulator :IDisposable
    {
        private readonly StockTicker _ticker;
        private string[] _symbols = new string[] { "MSFT", "IBM", "SUN", "GOOG" };
        private Random _random = new Random();
        private Timer _timer;

        private int _counter = 0;

        public Simulator(StockTicker ticker)
        {
            _ticker = ticker;

            _timer = new Timer(TimeSpan.FromMinutes(1).TotalMilliseconds);
            _timer.Elapsed += _timer_Elapsed;
            _timer.Start();
         }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _timer.Stop();
        }

        public void Simulate()
        {
            while (_timer.Enabled)
            {
                var stock = CreateStock();
                EmitTick(stock);
                Threads.Thread.Sleep(200);
            }
            Console.WriteLine("Finished");
        }

        private StockTick CreateStock()
        {
            var stock = new StockTick()
            {
                QuoteSymbol = _symbols[0],
                //Price = Decimal.Round((decimal)_random.NextDouble() * _random.Next(5), 2)
                Price = _counter += 1
            };
            return stock;
        }

        private void EmitTick(StockTick stock)
        {
            _ticker.Notify(stock);
        }

        public void Dispose()
        {
            if (_timer.Enabled)
                _timer.Stop();
            _timer.Close();
            _timer.Dispose();
        }
    }
}
