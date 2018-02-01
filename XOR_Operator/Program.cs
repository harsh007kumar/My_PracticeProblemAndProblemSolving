using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XOR_Operator
{   // References https://www.dotnetperls.com/xor
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 9, 45, 0, 4, 8, 0, 0, 0 ,0 };
            arr[2] = arr[0] ^ arr[1];
            arr[5] = arr[3] ^ arr[4];
            arr[6] = arr[2] ^ arr[5];
            arr[7] = arr[6] ^ arr[1] ^ arr[3] ^ arr[4];
            arr[8] = arr[6] ^ arr[1];
            foreach (int numberToTest in arr)
            {
                Print(numberToTest);
            }
            //Console.WriteLine(PrintBinary(c));
            Console.ReadLine();
        }

        /// <summary>
        /// Function to Convert Integer to its equivalent Binary Representation
        /// </summary>
        /// <param name="no"></param>
        /// <returns>Char Array with Binary Representation of Input</returns>
        private static char[] PrintBinary(int no)
        {
            Console.WriteLine($"Number : {no}");
            char[] ch = new char[32];
            int i = 0, pos = 31;
            while(i<32)
            {
                if ((no & (1 << i)) != 0)
                {
                    ch[pos-i] = '1'; i++;
                }
                else
                {
                    ch[pos-i] = '0'; i++;
                }
            }

            return ch;// new string(ch);
        }

        private static void Print(int numberToTest)
        {
            foreach (char ch in PrintBinary(numberToTest))
                Console.Write($"{ch} ");
            Console.WriteLine("\n");
        }
    }
}
