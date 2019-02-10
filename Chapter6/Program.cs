using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter6
{
    class Program
    {
        static void Main(string[] args)
        {
            TestControllingRelationship();

            Console.ReadKey();

        }

        static void TestControllingRelationship()
        {
            ControllingRelationship cr = new ControllingRelationship();
            cr.Delay();

        }
    }
}
