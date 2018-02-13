using System;
using System.Linq;

namespace Searching
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            string var = null;
            while (var != "0")
            {
                Console.WriteLine("\n\n====== Select which Searching u wish to perfrom from below =====\nB=Binary Search\nE=Exponential Search\n" +
                                  "I=Interpolation Search\nJ=Jump Search\nL=Linear Search\nF=Fibonacci Search\n0=EXIT");
                var = Console.ReadLine();
                switch (var.ToUpper())
                {
                    case "B":
                        Console.WriteLine("Binary Search");
                        int[] B_arr = takearrayinput();
                        BinarySearch(ref B_arr, 25);
                        print(B_arr);
                        break;
                    case "E":
                        Console.WriteLine("Exponential Search");
                        int[] E_arr = ExponentialSearch(takearrayinput(), 25);
                        print(E_arr);
                        break;
                    case "I":
                        Console.WriteLine("Interpolation Search");
                        int[] I_arr = InterpolationSearch(takearrayinput(), 25);
                        print(I_arr);
                        break;
                    case "J":
                        Console.WriteLine("Jump Search");
                        int[] J_arr = JumpSearch(takearrayinput(), 25);
                        print(J_arr);
                        break;
                    case "L":
                        Console.WriteLine("Linear Search");
                        int[] L_arr = LinearSearch(takearrayinput(), 25);
                        print(L_arr);
                        break;
                    case "F":
                        Console.WriteLine("Fibonacci Search");
                        int[] F_arr = FibonacciSearch(takearrayinput(), 25);
                        print(F_arr);
                        break;
                    case "0":
                        Console.WriteLine("==================== Exiting the Solution ====================");
                        break;
                    default:
                        Console.WriteLine("Enter a Appropriate Keyword");
                        break;
                }
            }
            Console.ReadKey();
        }



        private static void BinarySearch(ref int[] b_arr, int v)
        {
            throw new NotImplementedException();
        }

        private static int[] ExponentialSearch(int[] v1, int v2)
        {
            throw new NotImplementedException();
        }

        private static int[] FibonacciSearch(int[] v1, int v2)
        {
            throw new NotImplementedException();
        }

        private static int[] LinearSearch(int[] v1, int v2)
        {
            throw new NotImplementedException();
        }

        private static int[] JumpSearch(int[] v1, int v2)
        {
            throw new NotImplementedException();
        }

        private static int[] InterpolationSearch(int[] v1, int v2)
        {
            throw new NotImplementedException();
        }

        private static void swap(ref int v1, ref int v2)
        {
            v1 = v1 ^ v2;
            v2 = v1 ^ v2;
            v1 = v1 ^ v2;
        }

        private static int[] takearrayinput()
        {
            //int[] arr = new int[] { };
            Console.WriteLine("\nEnter the integer(s) you want in ur Sorted Array seperated by comma(,) like 2,3,4 and Search would be for no 25");
            return Console.ReadLine().Split(',').Select(str => int.Parse(str)).ToArray();
            //var result = myString.Select(s => s.ToSafeInt()).ToArray()
        }

        private static void print(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                Console.Write("\t{0}", arr[i]);
        }
    }
}
