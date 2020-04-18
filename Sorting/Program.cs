using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Searching;

namespace Sorting
{
    // Visit https://www.geeksforgeeks.org/sorting-terminology/ for more info on types of sorting algorithm
    public class Sort
    {
        static void Main(string[] args)
        {
            int[] array;
            string var = null;
            while (var != "0")
            {
                Console.WriteLine("\n========== Un-sorted original array ==========");
                array = ReturnUnsortedArray();//takearrayinput();
                Search.print(array);
                Console.WriteLine("\n====== Select which Sorting u wish to perfrom from below =====\nB=BubbleSort\nS=SelectionSort\nI=InsertionSort\nM=MergeSort\nC=CountSort\nR=RadixSort\nH=HeapSort\nQ=QuickSort\n0=EXIT");
                var = Console.ReadLine();
                switch (var.ToUpper())
                {
                    case "B":
                        Console.WriteLine("Bubble Sort O(n^2)");
                        //Bubblesort(ref array);
                        Bubblesort_Recursive(ref array, array.Length);
                        break;
                    case "S":
                        Console.WriteLine("Selection Sort O(n^2)");
                        array = Selectionsort(array);
                        break;
                    case "I":
                        Console.WriteLine("Insertion Sort O(n^2)");
                        array = Insertionsort(array);
                        break;
                    case "M":
                        Console.WriteLine("Merge Sort O(n log(n))");
                        array = Mergesort(array, 0, array.Length - 1);
                        break;
                    case "Q":
                        Console.WriteLine("Quick Sort O(n^2)");
                        Quicksort(array, 0, array.Length - 1);
                        break;
                    case "C":
                        Console.WriteLine("Count Sort O(n^2)");
                        array = Countsort(array, -3, 9);       // we are passing array to be sorted as 1st paramter & starting element of range as 2nd & last element as 3rd parameter, assuming range consists of consecutive no's.
                        break;
                    case "R":
                        Console.WriteLine("Radix Sort O(nk)");
                        array = Radixsort(array);
                        break;
                    case "H":
                        Console.WriteLine("Heap Sort O(n log(n))");
                        array = Heapsort(array);
                        break;
                    case "0":
                        Console.WriteLine("==================== Exiting the Solution ====================");
                        break;
                    default:
                        Console.WriteLine("Enter a Appropriate Keyword");
                        break;
                }
                Console.WriteLine("---------- Array after sorting ----------");
                Search.print(array);
            }
        }

        private static int[] Heapsort(int[] array)
        {
            throw new NotImplementedException();
        }

        // Quicksort Sort Recursive || Not Stable sort || In Place || TimeComplexity O(n^2) || Divide and Conquer algorithm
        public static void Quicksort(int[] array, int firstIndex, int lastIndex)
        {
            if (firstIndex < lastIndex)
            {
                int pivot = Partition(array, firstIndex, lastIndex);     // Time O(n)
                Quicksort(array, firstIndex, pivot - 1);     // Time T(k)
                Quicksort(array, pivot + 1, lastIndex);     // Time T(n-k-1)
            }
        }

        /// <summary>
        /// Function which return pivot and sorts array such that all element to its left are smaller and all to its rt are bigger than pivot.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        private static int Partition(int[] array, int startIndex, int endIndex)
        {
            // Here picking last element as pivot (can also select first, median or any random no for the same)
            int pivot = array[endIndex], lastBiggestIndex = startIndex;
            while (startIndex < endIndex)
            {
                if (array[startIndex++] < pivot)
                    Swap(ref array[startIndex - 1], ref array[lastBiggestIndex++]);
            }
            Swap(ref array[endIndex], ref array[lastBiggestIndex]);
            return lastBiggestIndex;
        }

        private static int[] Radixsort(int[] array)
        {
            throw new NotImplementedException();
        }

        // Counting Sort || Stable sort || TimeComplexity O(n+k) where n = no of elements & k = range of inputs || Auxiliary Space: O(n+k) || Not comparison based sorting
        public static int[] Countsort(int[] array, int firstElementInRange, int lastElementInRange)
        {
            int i, j, Len = array.Length, rangeOfElements = lastElementInRange - firstElementInRange + 1;
            int[] countArray = new int[rangeOfElements];        // array to store the count of each unique object
            // storing count of each unique element
            for (j = 0; j < Len; j++)
                countArray[array[j] - firstElementInRange] += 1;
            //Console.WriteLine("storing count of each unique element");
            //Search.print(countArray);

            // Modify the count array such that each element at each index stores the sum of previous counts
            for (i = 1; i < rangeOfElements; i++)
                countArray[i] += countArray[i - 1];
            //Console.WriteLine("Modified count array with each element at each index storing the sum of previous counts");
            //Search.print(countArray);

            int[] sortedArray = new int[Len];
            int checkCount = 0, index = 0;
            for (i = 0; i < rangeOfElements; i++)       // Here running loop till Range Of Elements, Not length of input array.
                if(countArray[i]>0 && countArray[i]>checkCount)
                {
                    int noOfTimesElementIsPresent = countArray[i] - checkCount;
                    for (int k = 0; k < noOfTimesElementIsPresent; k++)
                        sortedArray[index++] = firstElementInRange + i;       // Console.Write("\t"+firstElementInRange + i);
                    checkCount = countArray[i];
                }
            return sortedArray;
        }

        // Merge Sort Recursive || Stable sort || Not In Place || External Sorting || TimeComplexity O(n log(n)) || Auxiliary Space: O(n) || Divide and Conquer algorithm
        public static int[] Mergesort(int[] array, int firstIndex, int lastIndex)
        {
            if (lastIndex == firstIndex)
                return new int[] { array[firstIndex] };      //Till array is broken down to single element, return array containing single element
            else
            {
                int middle = (firstIndex + lastIndex) / 2;
                int[] LeftSubArray = Mergesort(array, firstIndex, middle);     // Time T(n/2)
                int[] RightSubArray = Mergesort(array, middle + 1, lastIndex);     // Time T(n/2)
                return Merge(LeftSubArray, RightSubArray);     // Time O(n)
            }
        }

        // Merging two sorted array || TimeComplexity O(n)
        private static int[] Merge(int[] ltArr, int[] rtArr)
        {
            int LtLength = ltArr.Length, RtLegth = rtArr.Length;
            int[] NewArray = new int[LtLength + RtLegth];
            int index = 0, ltIndex = 0, rtIndex = 0;
            while (index < NewArray.Length)
            {
                if (ltIndex == LtLength)
                    NewArray[index++] = rtArr[rtIndex++];
                else if (rtIndex == RtLegth)
                    NewArray[index++] = ltArr[ltIndex++];
                else if (ltArr[ltIndex] <= rtArr[rtIndex])
                    NewArray[index++] = ltArr[ltIndex++];
                else if (ltArr[ltIndex] > rtArr[rtIndex])
                    NewArray[index++] = rtArr[rtIndex++];
            }
            return NewArray;
        }

        // Bubble Sort Iterative || In Place || Stable by Default || TimeComplexity O(n^2)
        public static void Bubblesort(ref int[] arr)
        {
            bool swapped;
            int i, j, Len = arr.Length;
            for (i = 0; i < Len - 1; i++)
            {
                swapped = false;
                for (j = 0; j < Len - 1 - i; j++)
                {
                    if (arr[j] > arr[j + 1])
                    {
                        Search.swap(ref arr[j], ref arr[j + 1]);
                        swapped = true;     // If in any one pass swapping not required we know array is sorted now.
                    }
                }
                if (swapped == false)
                    break;
            }
        }

        // Bubble Sort Recursive || In Place || Stable by Default || TimeComplexity O(n^2)
        public static void Bubblesort_Recursive(ref int[] arr, int Len)
        {
            bool swapped = false;
            for (int i = 0; i < Len - 1; i++)
            {
                if (arr[i] > arr[i + 1])
                {
                    Search.swap(ref arr[i], ref arr[i + 1]);
                    swapped = true;     // If in any one pass swapping not required we know array is sorted now.
                }
            }
            if (swapped == true)
                Bubblesort_Recursive(ref arr, Len - 1);     // Recursive call since last element is already in its place
        }

        // Selection Sort || In Place || Not-Stable by Default || (sorting element in ascending order here), TimeComplexity O(n^2)
        public static int[] Selectionsort(int[] arr)
        {
            int i, j, Len = arr.Length, min_index;
            for (i = 0; i < Len - 1; i++)
            {
                min_index = i + 1;
                for (j = i + 1; j < Len; j++)
                    if (arr[j] < arr[min_index])
                        min_index = j;
                if (arr[min_index] < arr[i])
                    Search.swap(ref arr[min_index], ref arr[i]);
            }
            return arr;
        }

        // Insertion Sort || In Place || Stable by default || TimeComplexity O(n^2)
        public static int[] Insertionsort(int[] arr)
        {
            int i, k, CurrentElemenetBeingInspected, Len = arr.Length;
            for (i = 1; i < Len; i++)
            {
                CurrentElemenetBeingInspected = arr[i];
                //if (CurrentElemenetBeingInspected < arr[i - 1])
                //{
                for (k = i - 1; k >= 0; k--)
                {
                    // CurrentElemenetBeingInspected is at correct index, if it's bigger than last element of sorted sub-array
                    if (CurrentElemenetBeingInspected > arr[k])
                        break;
                    arr[k + 1] = arr[k];
                }
                arr[k + 1] = CurrentElemenetBeingInspected;
                //}
            }
            return arr;
        }

        private static int[] Takearrayinput()
        {
            //int[] arr = new int[] { };
            Console.WriteLine("\nEnter the integer(s) you want in ur array seperated by comma(,) like 2,3,4");
            return Console.ReadLine().Split(',').Select(str => int.Parse(str)).ToArray();
            //var result = myString.Select(s => s.ToSafeInt()).ToArray()
        }

        public static int[] ReturnUnsortedArray()
        {
            return new int[] { -3, 1, 4, 1, 2, 7, 5, 2 , -1, -2, -2 };      // use for Counting Sort 
            // return new int[] { 38, 27, 43, 3, 9, 82, 10 };       // used for Merge Sort
            // return new int[] { 64, 25, 12, 22, 11 };     // used with most other Sorting techniques
            // return new int[] { 4, 5, 3, 2, 4, 1 };       // to check Sort Stablility use this
        }

        /// <summary>
        /// Swaping two no using temp variable
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        public static void Swap(ref int v1, ref int v2)
        {
            int temp = v2;
            v2 = v1;
            v1 = temp;
        }
    }
}
