using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4_Byte_Enum
{
    //
    // Summary:
    //     Enum AMRDeviceStatus
    [Flags]
    public enum AMRDeviceStatus : byte
    {
        RCDC_SWITCH_OPEN = 1,
        RCDC_SWITCH_ARMED = 2,
        RCDC_SWITCH_PRESENT = 4,
        RADIO_DST_ENABLED = 8,
        RADIO_IS_MOBILE = 32,
        RADIO_ON_AC_POWER = 64,
        RADIO_DST_IN_EFFECT = 128
    }
    [Flags]
    public enum DISCONNECTSWITCHSTATUS : byte
    {
        //Closed = 0,
        Open = 1,
        Armed = 2,
        HasSwitch = 4,
        Unknown = 8
    }
    class Program
    {
        static void Main(string[] args)
        {
            byte b = 15;
            AMRDeviceStatus status = (AMRDeviceStatus)b;
            Console.WriteLine("\n\t{0}", status);
            DISCONNECTSWITCHSTATUS ds= (DISCONNECTSWITCHSTATUS)b;
            Console.WriteLine("\n\t{0}", ds);
            Console.ReadKey();
        }
    }
}
