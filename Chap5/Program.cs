using Chapter4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Utilities;

namespace Chap5
{
    class Program
    {
        static Timer timer;
        [STAThread]
        public static void Main(string[] args)
        {
            SetupTimer();

            //TestPrime();
            // TestPrimes(10);
            //TestPrimesAsync(10);

            //  TestPrimesObservable(10);
            TestPeriodics();

            Console.WriteLine("Something else");
            Console.ReadKey();
        }

        private static void SetupTimer()
        {
            timer = new Timer();
            timer.Interval = 10000;
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            timer.Dispose();

            if (disposable != null)
            {
                disposable.Dispose();
                Console.WriteLine("Periodic service call ended");
            }
        }

        static void TestPrimes(int number)
        {
            Primes c1 = new Primes();
            var primes = c1.GeneratePrimes(number);
            foreach (var p in primes)
            {
                Console.WriteLine($"{p}");
            }
        }

        static async void TestPrimesAsync(int number)
        {
            Primes c1 = new Primes();
            //var primes = await c1.GeneratePrimesAsync(number);
            foreach (var p in await c1.GeneratePrimesAsync(number))
            {
                Console.WriteLine($"{p}");
            }
        }

        static void TestPrime()
        {
            Primes c1 = new Primes();
            for (int i = 2; i < 14; i++)
            {
                Console.WriteLine($"Is {i} prime? {c1.IsNumberPrime(i)}");
            }
          }

        static void TestPrimesObservable(int number)
        {
            Primes c1 = new Primes();
            var subscription = c1.GeneratePrimesObs2(number)
                                //.GeneratePrimesObs(number)
                                 .Timestamp()
                                 .SubscribeConsole("primes observable");

        }

        static IDisposable disposable;

        static void TestPeriodics()
        {
            IService service = new SomeService();
            PeriodicBehaviour pb = new PeriodicBehaviour(service);
            //disposable =  pb.GetIntervalData();

            pb.GetTimerIntervalData();

        }

    }
}
