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
                        array = Mergesort(array);
                        break;
                    case "C":
                        Console.WriteLine("Count/Bucket Sort O(n^2)");
                        array = Countsort(array);
                        break;
                    case "R":
                        Console.WriteLine("Radix Sort O(nk)");
                        array = Radixsort(array);
                        break;
                    case "Q":
                        Console.WriteLine("Quick Sort O(n^2)");
                        array = Quicksort(array);
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

        private static int[] Quicksort(int[] array)
        {
            throw new NotImplementedException();
        }

        private static int[] Radixsort(int[] array)
        {
            throw new NotImplementedException();
        }

        // Stable sort
        public static int[] Countsort(int[] array)
        {
            throw new NotImplementedException();
        }

        // Stable sort
        public static int[] Mergesort(int[] array)
        {
            throw new NotImplementedException();
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
        public static void Bubblesort_Recursive(ref int[] arr,int Len)
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
                if(arr[min_index] < arr[i])
                    Search.swap(ref arr[min_index], ref arr[i]);
            }
            return arr;
        }

        // Insertion Sort || In Place || Stable by default || TimeComplexity O(n^2)
        public static int[] Insertionsort(int[] arr)
        {
            int i,k, CurrentElemenetBeingInspected, Len=arr.Length;
            for(i=1;i<Len;i++)
            {
                CurrentElemenetBeingInspected = arr[i];
                //if (CurrentElemenetBeingInspected < arr[i - 1])
                //{
                    for (k = i-1; k >= 0; k--)
                    {
                        if (CurrentElemenetBeingInspected > arr[k])
                            break;
                        arr[k+1] = arr[k];
                    }
                    arr[k+1] = CurrentElemenetBeingInspected;
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
        { return new int[] { 64, 25, 12, 22, 11 }; }// To check Sort Stablility use this => { 4, 5, 3, 2, 4, 1 }; }
    }
}
