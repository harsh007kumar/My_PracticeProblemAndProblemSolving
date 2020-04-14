using System;
using System.Linq;

namespace Searching
{
    // Visit https://www.geeksforgeeks.org/searching-algorithms/ for more info on types of searching algorithm
    public class Search
    {
        public static void Main(string[] args)
        {
            int[] array = ReturnSortedArray();//takearrayinput();
            print(array);   //Show array
            int foundAt,elementToBeSearched = 38;//Convert.ToInt32(Console.ReadLine()); // TestCase : check for -ve values and 1st and last index values along with values not present in array.Ex:  -138, 2 , 3, 38, 91, 138
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
                        foundAt = ExponentialSearch(array, elementToBeSearched);
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
                        foundAt = LinearSearch(array, elementToBeSearched);
                        break;
                    case "F":
                        Console.WriteLine("Fibonacci Search");
                        foundAt = FibonacciSearch(array, elementToBeSearched);
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
        public static int BinarySearch(int[] arr, int elementToBeSearched)
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
            int Mid = (High + Low) / 2, index = -1;
            do
            {
                if (elementToBeSearched == arr[Mid])
                {
                    index = Mid;
                    break;
                }
                else if (elementToBeSearched > arr[Mid])
                    Low = Mid + 1;
                else
                    High = Mid - 1;
                Mid = (High + Low) / 2;
            }
            while (Mid <= High && Mid >= Low);
            return index;
        }

        // Exponential Search using iteration which utilizes binary search, Time Complexity : O(Log n) [ a) Find range where element is present b) Do Binary Search in found range.]
        public static int ExponentialSearch(int[] arr, int elementToBeSearched)
        {
            int i = 1, Len = arr.Length;
            if (elementToBeSearched == arr[0])
                return 0;
            while (i < Len && arr[i] <= elementToBeSearched)
                i *= 2;     // doubling i
            return BinarySearch_Recursive(arr, i / 2, i, elementToBeSearched);      // Searching last subset of array which had highest value bigger than element to be searched
        }

        // Fibonacci Search, Time Complexity : O(Log n)
        private static int FibonacciSearch(int[] arr, int elementToBeSearched)
        {
            throw new NotImplementedException();    // https://www.geeksforgeeks.org/fibonacci-search/
        }

        // Liner Search, Time Complexity : O(n)
        public static int LinearSearch(int[] arr, int elementToBeSearched)
        {
            int Len = arr.Length, i = 0;
            while (i < Len)
                if (arr[i++] == elementToBeSearched)
                    return i-1;
            return -1;
        }

        // Jump Search, Time Complexity : O(√n)
        public static int JumpSearch(int[] arr, int elementToBeSearched)
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
            if(startFrom > i)   //jump_by-1 time loop
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
        public static int InterpolationSearch(int[] arr, int elementToBeSearched)
        {
            //Console.WriteLine("Recursive Interpolation Search");
            //return InterpolationSearch_Recursive(arr, 0, arr.Length - 1, elementToBeSearched);     //recursive way

            Console.WriteLine("Iterative Interpolation Search");
            return InterpolationSearch_Iterative(arr, 0, arr.Length - 1, elementToBeSearched);     //iterative way
        }

        /// <summary>
        /// Interpolation Search using recursion.
        /// The idea of formula is to return higher value of pos when element to be searched(x) is closer to arr[hi]. And smaller value when closer to arr[lo], using
        /// pos = lo + [ (x - arr[lo])*(hi-lo) / (arr[hi]-arr[Lo]) ]
        /// </summary>
        /// <param name="arr">Uniformely sorted array</param>
        /// <param name="Low">Lowest Index of array</param>
        /// <param name="High">Lowest Index of array</param>
        /// <param name="elementToBeSearched"></param>
        /// <returns>index of Element being searched, if present in uniformely sorted array</returns>
        private static int InterpolationSearch_Recursive(int[] arr, int Low, int High, int elementToBeSearched)
        {
            //Interpolation formula : pos = lo + [ (x-arr[lo])*(hi-lo) / (arr[hi]-arr[Lo]) ]
            int pos = Low + (((elementToBeSearched - arr[Low]) * (High - Low)) / (arr[High] - arr[Low])), index = -1;
            
            if (pos > High || pos < Low)
                return index;
            else if (elementToBeSearched == arr[pos])
                return pos;
            else if (elementToBeSearched < arr[pos])
                index = InterpolationSearch_Recursive(arr, Low, pos - 1, elementToBeSearched);
            else if (elementToBeSearched > arr[pos] )
                index = InterpolationSearch_Recursive(arr, pos + 1, High, elementToBeSearched);

            return index;
        }

        // Interpolation Search using iteration.
        private static int InterpolationSearch_Iterative(int[] arr, int Low, int High, int elementToBeSearched)
        {
            //Interpolation formula : pos = lo + [ (x-arr[lo])*(hi-lo) / (arr[hi]-arr[Lo]) ]
            int index = -1, pos = Low + (((elementToBeSearched - arr[Low]) * (High - Low)) / (arr[High] - arr[Low]));

            while (pos <= High && pos >=Low)
            {
                if (elementToBeSearched == arr[pos])
                {
                    index = pos;
                    break;
                }
                else if (elementToBeSearched < arr[pos])
                    High = pos - 1;
                else if (elementToBeSearched > arr[pos])
                    Low = pos + 1;
                pos = Low + (((elementToBeSearched - arr[Low]) * (High - Low)) / (arr[High] - arr[Low]));
            }
            return index;
        }


        public static void swap(ref int v1, ref int v2)
        {
            v1 = v1 ^ v2;
            v2 = v1 ^ v2;
            v1 = v1 ^ v2;
        }

        private static int[] takearrayinput()
        {
            //int[] arr = new int[] { 2, 5, 8, 12, 16, 23, 38, 56, 72, 91 };
            Console.WriteLine("\nEnter the integer(s) you want in ur Sorted Array seperated by comma(,) like 2,3,4");
            return Console.ReadLine().Split(',').Select(str => int.Parse(str)).ToArray();
            //var result = myString.Select(s => s.ToSafeInt()).ToArray()
        }

        public static int[] ReturnSortedArray()
        {   return new int[] { 2, 5, 8, 12, 16, 23, 38, 56, 72, 91 };        }

        public static void print(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
                Console.Write("\t{0}", arr[i]);
            Console.WriteLine();
        }
    }
}
