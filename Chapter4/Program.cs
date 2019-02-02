using Chapter4.CreatingObservables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4
{
    class Program
    {
        static void Main(string[] args)
        {
            TestCreateObservables();

            Console.Read();
        }
        static void TestCreateObservables()
        {
            var dis= new NumbersObservable(10).SubscribeConsole("numbers..");
            
        }
    }
}
