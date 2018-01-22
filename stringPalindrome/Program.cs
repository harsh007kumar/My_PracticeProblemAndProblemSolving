using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stringPalindrome
{
    class Program
    {
        static void Main(string[] args)
        {
            string cont = null;
            while (cont != "0")
            {
                string p;
                Console.WriteLine("-------Enter a string to chek Palindrome--------");
                p = Console.ReadLine();
                Console.WriteLine(check_Palindrome(p));
                Console.WriteLine("\n\tPress Any Key to Continue or Zero To Exit\t");
                cont = Console.ReadLine();
            }
        }
        public static bool check_Palindrome(string str)
        {
            int length = str.Length;
            string reverse = null;
            while (length > 0)
            {
                length--;
                reverse += str.Substring(length, 1);
            }
            return str.Equals(reverse);
        }
    }
}
