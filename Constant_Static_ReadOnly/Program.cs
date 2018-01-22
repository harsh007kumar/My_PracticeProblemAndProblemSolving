using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Constant_Static_ReadOnly
{
    public sealed class Program
    {
        int har = 10;
        public int Har
        { get { return har; } set { har = value; } }

        static int sta = 0;

        public static int Main(string[] args)
        {

            int x = 0, y = 0;
            for (int i = 0; i < 3; i++)
            {
                if (++x > 1 || ++y > 1)
                {
                    x++;
                }
                x ++;//8,3, 

            }
            Console.WriteLine("{0} {1}", x,y);
            
            Method(20);
            
            return 0;
        }
        
        public static void Method(int number)//20 NO loop NO switch//
        {
            PrintTheNumDecreasingOrder(number);
            Console.WriteLine("\n\nDifferent order now");
            PrintTheNumIncreasingOrder(number);

        }
        // 0 1 2 3 ... number

        public static void PrintTheNumDecreasingOrder(int no)
        {
            if (no >= 0)
            {
                Console.Write("{0} ", no);
                PrintTheNumDecreasingOrder(no - 1);
            }

        }
        public static void PrintTheNumIncreasingOrder(int no)
        {
            if (sta <=no)
            {
                Console.Write("{0} ", sta);
                sta += 1;
                PrintTheNumIncreasingOrder(no);
            }

        }

    }

    
}
