using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enum_Value_NotAvaliable
{
    class Program
    {
        public enum SwitchStateReason
        {
            OffOperationbyCommunication = 0,
            OnOperationByCommunication = 1,
            OffOperationByOverCurrent = 2,
            OnOperationByAutoConnect = 3,
            OffOperationByTimeSwitchFunction = 4,
            OnOperationByTimeSwitchFunction = 5,
            NotApplicable = 6,
            NoSwitch = 7,
            OffOperationByTestEnergization = 8,
            OnOperationByTestEnergization = 9
        }
        static void Main(string[] args)
        {
            int b = -1;
            object obj = (SwitchStateReason)(-1);
            object obj2 = (SwitchStateReason)(9);
            Console.WriteLine("\n\t{0}", obj);
            Console.WriteLine("\n\t{0}", obj2);
            Console.ReadKey();
        }
    }
}
