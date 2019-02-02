using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GettingChap2.Chap3
{
  static  class Tools
    {
        public static void ForEach<T> (this IEnumerable<T> items,
            Action<T> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }

        public static bool IsEven(this int number)
        {
            return (number % 2 == 0);
        }

        public static List<T> AddItem<T> ( this List<T> list, T item)
        {
            list.Add(item);
            return list;
        }
    }
}
