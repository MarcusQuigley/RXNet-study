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
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

            TestControllingRelationship();

            Console.ReadKey();

        }

        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            Console.WriteLine($"unobserved error: {e.Exception.Message}");
            e.SetObserved();
        }

        static void TestControllingRelationship()
        {
            ControllingRelationship cr = new ControllingRelationship();
            //   cr.Delay();

            //cr.CreatingObservers();

            // cr.CreatingObserversWithCancellation();
            //  cr.CreatingObserversInstances();
            cr.ControllingObserver();

        }
    }
}
