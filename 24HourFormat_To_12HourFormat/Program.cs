using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _24HourFormat_To_12HourFormat
{
    class Program
    {
        static void Main(string[] args)
        {
            // Sample input - 00:15 || 23:19 || 11:59
            string inputtime = "23:19";
            Console.WriteLine(Convert24to12Hour(inputtime));
            Console.ReadLine();
        }

        private static string Convert24to12Hour(string inputTime)
        {
            string result = string.Empty;
            int amPm;
            Int32.TryParse(inputTime.Substring(0, 2),out amPm);
            if(amPm>12)
                result = String.Format("{0}:{1}:PM", amPm - 12, inputTime.Substring(3, 2));
            else
                result = String.Format("{0}:{1}:AM", amPm, inputTime.Substring(3, 2));

            return result;
        }

    }
}
