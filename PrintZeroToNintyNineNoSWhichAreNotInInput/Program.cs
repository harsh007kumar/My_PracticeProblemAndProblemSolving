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
            int[] input = { 0, 101, 5, 43, 666, 5, 4 };
            PrintZeroToNintyExceptInput_Manually(input);
            Console.ReadLine();
        }
        private static void PrintZeroToNintyExceptInput_UsingInbuiltFunctions(int[] input)
        {
            for(int i=0;i<100;i++)
            {
                if (!input.Contains<int>(i))
                    Console.Write($"{i} ");
            }
        }

        private static void PrintZeroToNintyExceptInput_WithGenericList(int[] input)
        {
            List<int> Li = new List<int>();
            foreach (int var in input)
                Li.Add(var);

            for (int i = 0; i < 100; i++)
            {
                if (!Li.Contains<int>(i))
                    Console.Write($"{i} ");
            }
        }

        private static void PrintZeroToNintyExceptInput_Manually(int[] input)
        {
            int[] myNos = new int[100]; // Create an Array with no of elements you wish to print from Zero(0)

            //Setting Flag = 1 of the no is <100 at that position in array
            for(int i = 0; i<input.Length ; i++)
            {
                if (input[i] < 101)
                    myNos[input[i]] = 1;
            }

            // Loop to check and print no which dont have Flag = 1
            for (int x = 0; x < 100; x++)
            {
                if (myNos[x] != 1)
                    Console.Write($"{x} ");
                if (x % 9 == 0)
                    Console.WriteLine();
            }
        }
    }
}
