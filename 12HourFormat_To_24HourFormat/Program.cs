using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12HourFormat_To_24HourFormat
{
    class Program
    {
        static void Main(string[] args)
        {
            // Sample InputS -  11:45:26PM || 00:45:26AM
            string time = "11:45:26PM"; //Console.ReadLine();
            string AmPm = time.Substring(8, 2);//AM or PM
            int hh = Convert.ToInt32(time.Substring(0, 2));

            if (AmPm == "AM")
            {
                if (hh == 12)
                    Console.Write(string.Format("00" + time.Substring(2, 6)));
                else
                    Console.Write(string.Format(time.Substring(0, 8)));
            }
            else
            {
                if (hh == 12)
                    Console.Write(string.Format(time.Substring(0, 8)));
                else
                    Console.Write(string.Format((hh + 12) + time.Substring(2, 6)));
            }

            Console.ReadLine();
        }
    }
}
