using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepeatingNameinString
{
    class Program
    {
        static void Main(string[] args)
        {
            string first, str = "abc xyz pqr pqr xyz abc var pqr pqr";
            var List = str.Split(' ').ToList();
            first = "";
            for (int x = 0; x < List.Count; x++)
            {
                if (x > 0)
                    first = List[x - 1];

                if (first == List[x])
                {
                    List.Remove(first);
                    List.Remove(first);
                    x-=2;
                }
            }
            //Print
            foreach (var item in List)
                Console.WriteLine(item);
            Console.ReadLine();
        }
    }
}
