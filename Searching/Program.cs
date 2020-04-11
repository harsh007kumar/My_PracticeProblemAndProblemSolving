using System;
using System.Linq;

namespace Searching
{
    // Visit https://www.geeksforgeeks.org/searching-algorithms/ for more info on types of searching algorithm
    class Search
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
                        int[] B_arr = returnStaticArray();//takearrayinput();
                        int elementToBeSearched = 38;//Convert.ToInt32(Console.ReadLine());
                        print(B_arr);   //Show array
                        Console.WriteLine("We are searching for {0}", elementToBeSearched);
                        int foudnAt = BinarySearch(B_arr, elementToBeSearched);
                        Console.WriteLine((foudnAt == -1) ? "Element Not Present" : "Element found at index : "+ foudnAt);
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

        // Binary Search using recursion
        private static int BinarySearch(int[] arr, int elementToBeSearched)
        {
            //Console.WriteLine("Recursive Binary Search");
            //return BinarySearch_Recursive(arr, 0, arr.Length - 1, elementToBeSearched);     //recursive way

            Console.WriteLine("Iterative Binary Search");
            return BinarySearch_Iterative(arr, 0, arr.Length - 1, elementToBeSearched);     //Iterative way
        }
        // Binary Search using recursion
        private static int BinarySearch_Recursive(int[] arr,int Low, int High, int elementToBeSearched)
        {
            int Mid = (High + Low) / 2,index=-1;
            if (elementToBeSearched == arr[Mid])
                index=Mid;
            else if (Mid == High)
                return index;
            else if (elementToBeSearched > arr[Mid])
                index = BinarySearch_Recursive(arr, Mid + 1, High, elementToBeSearched);
            else
                index = BinarySearch_Recursive(arr, Low, Mid -1, elementToBeSearched);

            return index;
        }

        // Binary Search using Iteration
        private static int BinarySearch_Iterative(int[] arr, int Low, int High, int elementToBeSearched)
        {
            int Mid, index = -1;
            do
            {
                Mid = (High + Low) / 2;
                if (elementToBeSearched == arr[Mid])
                    index = Mid;
                else if (elementToBeSearched > arr[Mid])
                    Low = Mid + 1;
                else
                    High = Mid - 1;
            }
            while (Mid != High);
            return index;
        }

        private static int[] ExponentialSearch(int[] arr, int elementToBeSearched)
        {
            throw new NotImplementedException();
        }

        private static int[] FibonacciSearch(int[] arr, int elementToBeSearched)
        {
            throw new NotImplementedException();
        }

        private static int[] LinearSearch(int[] arr, int elementToBeSearched)
        {
            throw new NotImplementedException();
        }

        private static int[] JumpSearch(int[] arr, int elementToBeSearched)
        {
            throw new NotImplementedException();
        }

        private static int[] InterpolationSearch(int[] arr, int elementToBeSearched)
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
            Console.WriteLine("\nEnter the integer(s) you want in ur Sorted Array seperated by comma(,) like 2,3,4 and Search would be to check presense of no \"25\"");
            return Console.ReadLine().Split(',').Select(str => int.Parse(str)).ToArray();
            //var result = myString.Select(s => s.ToSafeInt()).ToArray()
        }
        private static int[] returnStaticArray()
        {   return new int[] { 2, 5, 8, 12, 16, 23, 38, 56, 72, 91 };        }

        private static void print(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                Console.Write("\t{0}", arr[i]);
            Console.WriteLine();
        }
    }
}
