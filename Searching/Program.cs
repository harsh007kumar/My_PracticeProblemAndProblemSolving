using System;
using System.Linq;

namespace Searching
{
    // Visit https://www.geeksforgeeks.org/searching-algorithms/ for more info on types of searching algorithm
    class Search
    {
        public static void Main(string[] args)
        {
            int[] array = ReturnStaticArray();//takearrayinput();
            print(array);   //Show array
            int foundAt,elementToBeSearched = 38;//Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("We are searching for element : {0}", elementToBeSearched);
            string var = null;
            while (var != "0")
            {
                Console.WriteLine("\n\n====== Select which Searching u wish to perfrom on above array =====\nB=Binary Search\nE=Exponential Search\n" +
                                  "I=Interpolation Search\nJ=Jump Search\nL=Linear Search\nF=Fibonacci Search\n0=EXIT");
                var = Console.ReadLine();
                foundAt=-1;
                switch (var.ToUpper())
                {
                    case "B":
                        Console.WriteLine("Binary Search");
                        foundAt = BinarySearch(array, elementToBeSearched);
                        break;
                    case "E":
                        Console.WriteLine("Exponential Search");
                        int[] E_arr = ExponentialSearch(array, elementToBeSearched);
                        break;
                    case "I":
                        Console.WriteLine("Interpolation Search");
                        foundAt = InterpolationSearch(array, elementToBeSearched);
                        break;
                    case "J":
                        Console.WriteLine("Jump Search");
                        foundAt = JumpSearch(array, elementToBeSearched);
                        break;
                    case "L":
                        Console.WriteLine("Linear Search");
                        int[] L_arr = LinearSearch(array, elementToBeSearched);
                        break;
                    case "F":
                        Console.WriteLine("Fibonacci Search");
                        int[] F_arr = FibonacciSearch(array, elementToBeSearched);
                        break;
                    case "0":
                        Console.WriteLine("==================== Exiting the Solution ====================");
                        break;
                    default:
                        Console.WriteLine("Enter a Appropriate Keyword");
                        break;
                }
                Console.WriteLine((foundAt == -1) ? "Element Not Present" : "Element found at index : " + foundAt);
            }
        }

        // Binary Search, Time Complexity : O(Log n)
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
                {
                    index = Mid;
                    break;
                }
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

        // Jump Search, Time Complexity : O(√n)
        private static int JumpSearch(int[] arr, int elementToBeSearched)
        {
            int len = arr.Length, jump_by = (int)Math.Sqrt(len), i, index = -1, startFrom=-1;
            for(i=0;i<len;i=i+jump_by)  //(len/jump_by) times loop
            {
                if (arr[i] == elementToBeSearched)
                    return i;
                else if (arr[i] < elementToBeSearched)
                    continue;
                else if (arr[i] > elementToBeSearched)
                {
                    startFrom = i - jump_by;
                    break;
                }
            }
            if(startFrom!=-1)   //jump_by-1 time loop
            {
                int k = startFrom;
                while (k < (jump_by+startFrom))
                {
                    if (arr[k] == elementToBeSearched)
                    { index = k; break; }
                    k++;
                }
            }
            return index;
        }

        //  Interpolation Search, Time Complexity : if elements are uniformly distributed, then O (log log n)). In worst case it can take upto O(n).
        private static int InterpolationSearch(int[] arr, int elementToBeSearched)
        {
            Console.WriteLine("Recursive Interpolation Search");
            return InterpolationSearch_Recursive(arr, 0, arr.Length - 1, elementToBeSearched);     //recursive way
        }

        // Interpolation Search using recursion
        private static int InterpolationSearch_Recursive(int[] arr, int Low, int High, int elementToBeSearched)
        {
            //Interpolation formula : pos = lo + [ (x-arr[lo])*(hi-lo) / (arr[hi]-arr[Lo]) ]
            int pos = Low + (((elementToBeSearched - arr[Low]) * (High - Low)) / (arr[High] - arr[Low])), index = -1;
            if (pos >= High)
                return index;
            else if (elementToBeSearched == arr[pos])
                return pos;
            else if (elementToBeSearched < arr[pos])
                index = InterpolationSearch_Recursive(arr, Low, pos - 1, elementToBeSearched);
            else if (elementToBeSearched > arr[pos] )
                index = InterpolationSearch_Recursive(arr, pos + 1, High, elementToBeSearched);

            return index;
        }
        private static void swap(ref int v1, ref int v2)
        {
            v1 = v1 ^ v2;
            v2 = v1 ^ v2;
            v1 = v1 ^ v2;
        }

        private static int[] takearrayinput()
        {
            //int[] arr = new int[] { 2, 5, 8, 12, 16, 23, 38, 56, 72, 91 };
            Console.WriteLine("\nEnter the integer(s) you want in ur Sorted Array seperated by comma(,) like 2,3,4 and Search would be to check presense of no \"25\"");
            return Console.ReadLine().Split(',').Select(str => int.Parse(str)).ToArray();
            //var result = myString.Select(s => s.ToSafeInt()).ToArray()
        }
        private static int[] ReturnStaticArray()
        {   return new int[] { 2, 5, 8, 12, 16, 23, 38, 56, 72, 91 };        }

        private static void print(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                Console.Write("\t{0}", arr[i]);
            Console.WriteLine();
        }
    }
}
