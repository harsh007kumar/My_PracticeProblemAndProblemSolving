using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarandDynamic
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var arr1 = new int[] { 1, 2, 3, 4 };
                dynamic arr2 = new int[] { 1, 2, 3, 4 };

                Console.WriteLine(arr1.Length);
                Console.WriteLine(arr2.VivekAndHarsh);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
