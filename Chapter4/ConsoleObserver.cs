using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4
{
    class ConsoleObserver<T> : IObserver<T>
    {
        string _name = string.Empty;
        public ConsoleObserver(string name)
        {
            _name = name;
        }
        public void OnCompleted()
        {
            Console.WriteLine($"{_name} Finished");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine($"{_name} Error {error}");
        }

        public void OnNext(T value)
        {
            Console.WriteLine($"{value} for {_name}");
        }
    }
}
