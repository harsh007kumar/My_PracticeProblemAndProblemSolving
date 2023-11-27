using System;
using System.Linq;
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
                array = ReturnUnsortedArray();//takearrayinput();
                Console.WriteLine("\n====== Select which Sorting you wish to perfrom from below =====" +
                    "\nB=BubbleSort\nS=SelectionSort\nI=InsertionSort\nM=MergeSort\nC=CountSort\nR=RadixSort" +
                    "\nH=HeapSort\nQ=QuickSort\nSh=ShellSort\n3Q=3Way Quick Sort/ Dutch National Flag problem" +
                    "\n0 =EXIT");
                var = Console.ReadLine();

                Console.WriteLine("\n========== Un-sorted original array ==========");
                Search.Print(array);

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
                    case "3Q":
                        Console.WriteLine("3Way Quick Sort O(n^2)/ Dutch National Flag problem");
                        QuickSort3Way(array, 0, array.Length - 1);
                        break;
                    case "C":
                        Console.WriteLine("Count Sort O(n+k)");
                        array = Countsort(array, -3, 9);       // we are passing array to be sorted as 1st paramter & starting element of range as 2nd & last element as 3rd parameter, assuming range consists of consecutive no's.
                        break;
                    case "R":
                        Console.WriteLine("Radix Sort O(nk)");
                        Radixsort(ref array);
                        break;
                    case "H":
                        Console.WriteLine("Heap Sort O(n log(n))");
                        Heapsort(ref array);
                        break;
                    case "SH":
                        Console.WriteLine("Shell Sort O(n^2)");
                        ShellSort(ref array);
                        break;
                    case "0":
                        Console.WriteLine("==================== Exiting the Solution ====================");
                        break;
                    default:
                        Console.WriteLine("Enter a Appropriate Keyword");
                        break;
                }
                Console.WriteLine("---------- Array after sorting ----------");
                Search.Print(array);
            }
        }

        // Heap Sort Iterative || Not Stable sort || In Place || TimeComplexity O(n/2*Logn)+O(nlogn) = O(nlogn)
        public static void Heapsort(ref int[] array)
        {
            int Len = array.Length;
            // Create MaxHeap, we start from mid of the array as Heapify itself will take care of element to left & right, remember left = 2*currentIndex+1
            for (int currentRoot = (Len / 2) - 1; currentRoot >= 0; currentRoot--)  // O(n/2)
                MaxHeapify(ref array, currentRoot, Len);                    // Maintin Heap integrity after at each level. O(logn)

            while (Len > 1)                                                 // O(n)
            {
                Swap(ref array[0], ref array[--Len]);                       // Swap Highest element i.e. root with smallest element
                MaxHeapify(ref array, 0, Len);                              // Maintin Heap integrity after swap and reduced length. O(logn)
            }
        }

        /// <summary>
        /// Recursive Heapify function which check and maintains Heap Intergrity from parent index passeed as argument, assumtion both child trees are already heapified.
        /// TimeComplexity of heapify is O(Logn)
        /// </summary>
        /// <param name="array"></param>
        /// <param name="currentRoot">Parent index from where Heap is restructured downwards</param>
        /// <param name="len">Last Index of element in HeapArray</param>
        private static void MaxHeapify(ref int[] array, int currentRoot, int len)
        {
            int left = currentRoot * 2 + 1, right = currentRoot * 2 + 2, largest = currentRoot;
            if (left < len && array[left] > array[currentRoot])
                largest = left;
            if (right < len && array[right] > array[largest])
                largest = right;

            if (currentRoot != largest)
            {
                Swap(ref array[largest], ref array[currentRoot]);
                MaxHeapify(ref array, largest, len);
            }
        }


        // Quicksort Sort Recursive || Not Stable sort || In Place || TimeComplexity O(n^2) || Divide and Conquer algorithm
        public static void Quicksort(int[] array, int firstIndex, int lastIndex)
        {
            if (firstIndex < lastIndex)
            {
                int pivot = Partition(array, firstIndex, lastIndex);    // Time O(n)
                Quicksort(array, firstIndex, pivot - 1);                // Time T(k)
                Quicksort(array, pivot + 1, lastIndex);                 // Time T(n-k-1)
            }
        }

        /// <summary>
        /// Function which return pivot and sorts array such that all element to its left are smaller and all to its rt are bigger than pivot.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        protected static int Partition(int[] array, int startIndex, int endIndex)
        {
            // Here picking last element as pivot (can also select first, median or any random no for the same)
            int pivot = array[endIndex], lastBiggestIndex = startIndex;
            var currentIndex = startIndex;
            while (currentIndex < endIndex)
            {
                if (array[currentIndex] < pivot)
                    Swap(ref array[currentIndex], ref array[lastBiggestIndex++]);
                currentIndex++;
            }
            Swap(ref array[endIndex], ref array[lastBiggestIndex]);
            return lastBiggestIndex;
        }

        // GFG 3-Way QuickSort (Dutch National Flag)
        public static void QuickSort3Way(int[] input, int start, int end)
        {
            if (start < end)
            {
                int i = 0, j = 0;
                Partition(input, start, end, ref i, ref j);
                QuickSort3Way(input, start, i);
                QuickSort3Way(input, j, end);
            }
        }

        /// <summary>
        /// Function which updates i & j such that all element to its left of 'i' are smaller than pivot & all to rt of 'j' are bigger than pivot.
        /// Used with 3Way-QuickSort (scenarios when multiple duplicates entries are present in unsorted array Ex: Dutch National Flag problem)
        /// Time O(n) || Space O(1)
        /// </summary>
        /// <param name="input"></param>
        /// <param name="low"></param>
        /// <param name="high"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        protected static void Partition(int[] input, int low, int high, ref int i, ref int j)
        {
            if (high - low <= 1)
            {
                if (input[low] > input[high])
                    Swap(ref input[low], ref input[high]);
                i = low;
                j = high;
                return;
            }
            else
            {
                var mid = low;
                var pivot = input[high];            // here Pivot is taken as last element(can use low or mid or random index also)
                while (mid <= high)
                {
                    if (input[mid] < pivot) 
                        Swap(ref input[low++], ref input[mid++]);
                    else if (input[mid] == pivot)
                        mid++;
                    else if (input[mid] > pivot)
                        Swap(ref input[mid], ref input[high--]);
                }
                i = low - 1;
                j = mid;    // or high+1
            }
        }

        // Radix Sort || Stable Sort || TimeComplexity O(d*(n+k)) where k is base representing numbers(decimal system b=10 i.e, Range) & d = O(logb(k)) K=Max possible value of base b.
        private static void Radixsort(ref int[] array)
        {
            int significantDigitBeingSorted = 1, Len = array.Length, minSingleDigit = 0, maxSingleDigit = 9;
            while (int.MaxValue / significantDigitBeingSorted > 0)
            {
                Countsort(ref array, minSingleDigit, maxSingleDigit, significantDigitBeingSorted);
                significantDigitBeingSorted *= 10;
            }
        }

        // Counting Sort to be used within Radix Sort
        public static void Countsort(ref int[] array, int firstElementInRange, int lastElementInRange, int n_th_DigitBeingSorted)
        {
            int i, Len = array.Length, rangeOfElements = lastElementInRange - firstElementInRange + 1;
            int[] sortedArray = new int[Len];
            int[] countArray = new int[rangeOfElements];        // array to store the count of each unique object
            // storing count of each unique element
            for (i = 0; i < Len; i++)
                countArray[(array[i] / n_th_DigitBeingSorted) % 10 - firstElementInRange] += 1;

            // Modify the count array such that each element at each index stores the sum of previous counts
            for (i = 1; i < rangeOfElements; i++)
                countArray[i] += countArray[i - 1];

            for (i = Len - 1; i >= 0; i--)       // loop till length of input array O(Len)
                sortedArray[--countArray[(array[i] / n_th_DigitBeingSorted) % 10 - firstElementInRange]] = array[i];
            //bool nextSortIterationRequired = !(countArray[1] == countArray[rangeOfElements-1]);     // this might seem like gr8 idea to break sorting as soon as we get 1st True value indicating elements are already sorted but it applicable only to current i th digit being sorted(Ex- for 2nd digit of array 112,415,218,11119 would return True even when elements are still not sorted.
            array = sortedArray;
        }

        // Counting Sort || Stable sort || TimeComplexity O(n+k) where n = no of elements & k = range of inputs || Auxiliary Space: O(n+k) || Not comparison based sorting
        public static int[] Countsort(int[] array, int firstElementInRange, int lastElementInRange)
        {
            int i, Len = array.Length, rangeOfElements = lastElementInRange - firstElementInRange + 1;
            int[] sortedArray = new int[Len];                   // array to store final sorted output
            int[] countArray = new int[rangeOfElements];        // array to store the count of each unique object

            // storing count of each unique element
            for (i = 0; i < Len; i++)
                countArray[array[i] - firstElementInRange] += 1;

            // Modify the count array such that each element at each index stores the sum of previous counts
            for (i = 1; i < rangeOfElements; i++)
                countArray[i] += countArray[i - 1];

            #region My 1st attempt use either of 1st or 2nd loop below to fill sorted array
            //// 1st
            //int checkCount = 0, index = 0;
            //for (i = 0; i < rangeOfElements; i++)       // loop till length of Range Of Elements, Not length of input array || Loop Time Complexity = O(rangeOfElements+Len)
            //    if (countArray[i] > 0 && countArray[i] > checkCount)
            //    {
            //        int noOfTimesElementIsPresent = countArray[i] - checkCount;
            //        for (int k = 0; k < noOfTimesElementIsPresent; k++)
            //            sortedArray[index++] = firstElementInRange + i;       // Console.Write("\t"+firstElementInRange + i);
            //        checkCount = countArray[i];
            //    }
            //// 2nd below || faster than above esp when range of elements is large || also Stable Sorting
            #endregion

            for (i = Len-1; i >=0; i--)       // loop till length of input array O(Len)
                sortedArray[--countArray[array[i] - firstElementInRange]] = array[i];
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
                for (k = i - 1; k >= 0; k--)
                {
                    // CurrentElemenetBeingInspected is at correct index, if it's bigger than last element of sorted sub-array
                    if (CurrentElemenetBeingInspected > arr[k])
                        break;
                    arr[k + 1] = arr[k];
                }
                arr[k + 1] = CurrentElemenetBeingInspected;
            }
            return arr;
        }

        // Shell Sort || In Place || Stable by default || TimeComplexity O(n^2)
        public static void ShellSort(ref int[] arr)
        {
            int Len = arr.Length, gap;
            for (gap = Len / 2; gap > 0; gap /= 2)      // Log (n)
            {
                for (int j = gap; j < Len; j++)         // (len-gap)
                {
                    int currentElementBeingInspected = arr[j];
                    int k = j;
                    while (k -gap >= 0 && arr[k-gap] > currentElementBeingInspected)    // (n/gap)
                    {
                        arr[k] = arr[k - gap];          // Moving the element which are greater than currentElementBeingInspected to right
                        k -= gap;
                    }
                    arr[k] = currentElementBeingInspected;  // Replace currentElementBeingInspected with last index which is either 0 or index where elements to left are smaller
                }
            }
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
            return new int[] { 4, 9, 4, 4, 1, 9, 4, 4, 9, 4, 4, 1, 4 };     // use for 3-way QuickSort / Dutch National Flag problem
            //return new int[] { 170, 45, 75, 90, 802, 24, 2, 66 };           // use for Radix Sort
            //return new int[] { -3, 1, 4, 1, 2, 7, 5, 2 , -1, -2, -2 };    // use for Counting Sort 
            // return new int[] { 38, 27, 43, 3, 9, 82, 10 };               // use for Merge Sort
            // return new int[] { 64, 25, 12, 22, 11 };                     // use with most other Sorting techniques
            // return new int[] { 4, 5, 3, 2, 4, 1 };                       // to check Sort Stablility use this
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
