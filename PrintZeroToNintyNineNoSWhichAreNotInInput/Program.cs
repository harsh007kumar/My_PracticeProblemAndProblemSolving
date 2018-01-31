using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintZeroToNintyNineNoSWhichAreNotInInput
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] input = { 101, 5, 43, 666, 5, 4 };
            PrintZeroToNintyExceptInput(input);
            Console.ReadLine();
        }
        private static void PrintZeroToNintyExceptInput(int[] input)
        {
            for(int i=0;i<100;i++)
            {
                if (!input.Contains<int>(i))
                    Console.Write($"{i} ");
            }
        }
    }
}
