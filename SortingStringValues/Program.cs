using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortingStringValues
{
    class Program
    {
        public static void Sort(string[] str)
        {
            List<string> ans = new List<string>();
            long y,z=0;
            foreach (string x in str)
            {
                if (string.IsNullOrWhiteSpace(x))
                    z += 1;
                else
                {
                    if (long.TryParse(x,out y))
                        Console.WriteLine(x);
                    else
                        ans.Add(x);
                }
            }
            foreach (string s in ans)
                Console.WriteLine(s);
        }
        static void Main(string[] args)
        {
            string[] s = { "100000001", "vdlv12300", null,"abc123","12345",null };
            Sort(s);
            //foreach (string x in s)
            //    Console.WriteLine(x);

            Console.ReadLine();
        }
    }
}
