using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GettingChap2.Chap3;

namespace GettingChap2
{
    class Program
    {
        static void Main(string[] args)
        {
             TestTicker();
         //   TestChap3();
            Console.Read();
        }

        static void TestTicker() {

            var ticker = new StockTicker();
            //  Monitor mon= new Monitor(ticker);
            RXMonitor rxMon = new RXMonitor(ticker);
            Simulator sim = new Simulator(ticker);
            sim.Simulate();
        }

        static void TestChap3() {
            //var numbers = Enumerable.Range(1, 10);
            //numbers.ForEach<int>(i=>Console.WriteLine(i));

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Hello");
            Console.WriteLine(sb.ToString());

            List<string> listy = new List<string>();
            listy.AddItem("this")
                 .AddItem("is")
                 .AddItem("the")
                 .AddItem("shizzle")
                 .ForEach(item => Console.WriteLine(item));

        }
    }
}
