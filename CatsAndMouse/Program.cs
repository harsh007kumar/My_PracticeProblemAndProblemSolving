using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsAndMouse
{
    class Program
    {
        static void Main(string[] args)
        {
            // Taking Input from user
            int q = Convert.ToInt32(Console.ReadLine());
            for (int qItr = 0; qItr < q; qItr++)
            {
                string[] xyz = Console.ReadLine().Split(' ');
                int x = Convert.ToInt32(xyz[0]);
                int y = Convert.ToInt32(xyz[1]);
                int z = Convert.ToInt32(xyz[2]);
                
                string result = catAndMouse(x, y, z);
                Console.WriteLine("\nResult for current query : {0}",result);
            }
            Console.ReadKey();
        }
        /// <summary>
        /// Function which return Cat A if cat A catches the mouse first, Cat B if cat B catches the mouse first, or Mouse C if the mouse escapes.
        /// </summary>
        /// <param name="a">int</param>
        /// <param name="b">int</param>
        /// <param name="c">int</param>
        /// <returns>string</returns>
        private static string catAndMouse(int c1, int c2, int m)
        {
            try
            {
                string result = null;
                if (Math.Abs(m - c1) == Math.Abs(m - c2))
                    result = "Mouse C";
                else
                    result = (Math.Abs(m - c1) < Math.Abs(m - c2)) ? "Cat A" : "Cat B";

                return result;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error encoutnered : {0}", e.Message);
                return "ERROR";
            }
        }
    }
}
