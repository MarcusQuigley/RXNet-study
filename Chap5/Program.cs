using Chapter4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chap5
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args) {

            //TestPrime();
            // TestPrimes(10);
            //TestPrimesAsync(10);

            TestPrimesObservable(10);

            Console.WriteLine("Something else");
            Console.ReadKey();
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

    }
}
