using System;

namespace InterviewProblemNSolutions
{
    public class SelectionAlgorithms
    {
        // Time Complexity Worst Case O(n^2) || on Avg O(nlogk), where n = no of elements & k = k smallest elements || Space O(1)
        // Time Complexity with Random Pivot Worst Case still O(n^2) || but on Avg O(n)
        public static int KSmallestElements(int[] input, int start, int last, int k)
        {
            if (input == null || input.Length == 0) return -1;
            if (start == last) return start;
            else // if (start < last)
            {
                var pivot = RandomPartition(input, start, last);
                if (pivot == k - 1) return pivot;
                else if (k > pivot) return KSmallestElements(input, pivot + 1, last, k);
                else return KSmallestElements(input, start, pivot - 1, k);
            }
        }

        protected static int Partition(int[] input, int start, int last)
        {
            var pivot = last;
            var pivotElement = input[pivot];
            var currentIndex = start;
            while (currentIndex < last)
            {
                if (input[currentIndex] < pivotElement)
                    Utility.Swap(ref input[currentIndex], ref input[start++]);
                currentIndex++;
            }
            Utility.Swap(ref input[pivot], ref input[start]);
            return start;
        }

        protected static int RandomPartition(int[] input, int start, int last)
        {
            var randomIndex = new Random().Next(start, last);           // find a random index in array
            Utility.Swap(ref input[last], ref input[randomIndex]);      // swap the random index num with last index num in array (since we selecting last index as Pivot
            return Partition(input, start, last);
        }

        // Time O(n)
        // Median of Medians
        // constants are very high for this algorithm. Therefore, this algorithm doesn’t work well in practical situations, randomized quickSelect works better
        public static int KSmallestUsingMedOfMed(int[] input, int start, int last, int k)
        {
            if (input == null || k > last - start + 1) return -1;
            #region Algo
            /* 1) Divide arr[] into ⌈n/5⌉ groups where size of each group is 5 except possibly the last group which may have less than 5 elements.
             * 2) Sort the above created ⌈n/5⌉ groups and find median of all groups. Create an auxiliary array ‘median[]’ and store medians of all ⌈n/5⌉ groups in this median array.
             * // Recursively call this method to find median of median[0..⌈n/5⌉-1]
             * 3) medOfMed = kthSmallest(median[0..⌈n/5⌉-1], ⌈n/10⌉)
             * 
             * 4) Partition arr[] around medOfMed and obtain its position.
             * pos = partition(arr, n, medOfMed)
             * 
             * 5) If pos == k return medOfMed
             * 6) If pos > k return kthSmallest(arr[l..pos-1], k)
             * 7) If pos < k return kthSmallest(arr[pos+1..r], k-pos+l-1)
             */
            #endregion
            int len = last - start + 1;
            var elementsPerGrp = 5;                                 // no of sub-array, typically taken as '5'
            int[] median = new int[(len + (elementsPerGrp - 1)) / 5]; // [(len+4)/5] returns floow value, which is same as Cieling value of len/5
            int i;
            // divide the array into '5' sub array and sort those sub-array and store their median in Median Array
            for (i = 0; i < len / elementsPerGrp; i++)
                median[i] = FindMedian(input, start + (i * elementsPerGrp), elementsPerGrp);

            // for last grp which might not be full
            if (i * elementsPerGrp < len) 
            {
                median[i] = FindMedian(input, start + (i * elementsPerGrp), len % elementsPerGrp);
                i++;
            }
            // recursive call to find median of medians, skip call if we have just 1 element
            var medOfMed = (median.Length == 1) ? 0 : KSmallestUsingMedOfMed(median, 0, median.Length - 1, i / 2);

            // normal steps of QuickSort technique for KSmallestElements
            var pivot = PartitionForMedOfMed(input, start, last, median[medOfMed]);
            if (pivot - start == k - 1) return pivot;
            else if (pivot - start > k - 1) return KSmallestUsingMedOfMed(input, start, pivot - 1, k);
            else return KSmallestUsingMedOfMed(input, pivot + 1, last, k - pivot + start - 1);
        }

        public static int FindMedian(int[] input, int start, int count)
        {
            var last = start + count - 1;
            Sorting.Sort.Quicksort(input, start, last);
            return input[start + (last - start) / 2];
        }

        protected static int PartitionForMedOfMed(int[] input, int start, int last, int value)
        {
            int i;
            for (i = start; i <= last; i++)             // find index of element that matches 'value'
                if (input[i] == value)
                    break;
            // swap last index value with 'i', as we are selecting last index as Pivot,
            // so setting median value at Pivot index insures we divide the array into equal halfs.
            Utility.Swap(ref input[last], ref input[i]);

            // normal Partition algo below
            var pivot = last;
            var pivotElement = input[pivot];
            var currentIndex = start;
            while (currentIndex < last)
            {
                if (input[currentIndex] < pivotElement)
                    Utility.Swap(ref input[currentIndex], ref input[start++]);
                currentIndex++;
            }
            Utility.Swap(ref input[pivot], ref input[start]);
            return start;
        }

        public static double MedianSortedArrayEqualSizeUsingCount(int[] a1, int[] a2, int len)
        {
            int i = 0, j = 0, medPrv = -1, medCurr = -1, count = 0;
            while (count <= len)
            {
                medPrv = medCurr;

                // if we reach end of first array
                if (i == len)
                { medCurr = a2[j]; break; }

                // if we reach end of second array
                else if (j == len)
                { medCurr = a1[i]; break; }

                if (a1[i] <= a2[j])
                    medCurr = a1[i++];
                else
                    medCurr = a2[j++];
                
                count++;
            }
            return (double)(medPrv + medCurr) / 2;
        }

        // DO NOT USE HAS FUNDAMENTAL ISSUE WITH ALGO IF ARRAY ARE OF EVEN LENGTH || MEDIAN IS NOT CALCULATED RIGHT
        // Recusrive Func which find Median of Two Sorted Array || Time O(logn)
        public static double MedianSortedArrayEqualSizeByComparingMedians(int[] a, int aLow, int aHigh, int[] b, int bLow, int bHigh)
        {
            // handling for corner cases
            var lenA = aHigh - aLow + 1;
            var lenB = bHigh - bLow + 1;
            if (lenA == 0 && lenB == 0)
                return -1;
            else if (lenA == 1 && lenB == 1)    // only 1 elements left in both arrays
                return (double)(a[aLow] + b[bLow]) / 2;
            else if (lenA == 2 && lenB == 2)    // only 2 elements left in both arrays
                return (double)(Math.Max(a[aLow], b[bLow]) + Math.Min(a[aHigh], b[bHigh])) / 2;

            var midA = aLow + (aHigh - aLow) / 2;
            var midB = bLow + (bHigh - bLow) / 2;
            // we got a match, return median from either array a or b
            if (a[midA] == b[midB])
                return a[midA];
            else if (a[midA] > b[midB])              // median must be b/w left half of A and right half of B
                return MedianSortedArrayEqualSizeByComparingMedians(a, aLow, midA, b, midB, bHigh);
            else // (a[midA] < b[midB])         // median must be b/w right half of A and left half of B
                return MedianSortedArrayEqualSizeByComparingMedians(a, midA, aHigh, b, aLow, midB);
        }

        // DO NOT USE HAS FUNDAMENTAL ISSUE WITH ALGO IF ARRAY ARE OF EVEN LENGTH || MEDIAN IS NOT CALCULATED RIGHT
        // Iterative Func which find Median of Two Sorted Array || Time O(logn) || Space O(1)
        public static double MedianSortedArrayEqualSizeByComparingMedians_Iterative(int[] a, int aLow, int aHigh, int[] b, int bLow, int bHigh)
        {
            while (true)
            {
                // handling for corner cases
                var lenA = aHigh - aLow + 1;
                var lenB = bHigh - bLow + 1;
                if (lenA == 0 && lenB == 0)
                    return -1;
                else if (lenA == 1 && lenB == 1)
                    return (double)(a[aLow] + b[bLow]) / 2;
                else if (lenA == 2 && lenB == 2)    // only 2 elements left in both arrays
                    return (double)(Math.Max(a[aLow], b[bLow]) + Math.Min(a[aHigh], b[bHigh])) / 2;


                var midA = aLow + (aHigh - aLow) / 2;
                var midB = bLow + (bHigh - bLow) / 2;

                if (a[midA] == b[midB])             // we got a match, return median from either array a or b
                    return a[midA];
                else if (a[midA] > b[midB])         // median must be b/w left half of A and right half of B
                {
                    aHigh = midA;
                    bLow = midB;
                }
                else // (a[midA] < b[midB])         // median must be b/w right half of A and left half of B
                {
                    aLow = midA;
                    bHigh = midB;
                }
            }
        }

        /// <summary>
        /// Returns Median value from Sorted Array || Time O(1)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="start"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static int Median(int[] a, int start, int len)
        {
            if (len < 1) return -1;

            if (len % 2 == 0)       // even no of elements
                return (a[len / 2] + a[len / 2 - 1]) / 2;
            else
                return a[start + len / 2];
        }

        /// <summary>
        /// Returns Median of two sorted arrays, of different length || Time O(Log(Min(m,n))), m = length of 1st & n = length of 2nd array || Space O(1)
        /// Tushar Roy https://youtu.be/LPFhl65R7ww
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double MedianOfSortedArray(int[] a, int[] b)
        {
            int lenA = a.Length;
            int lenB = b.Length;
            /* We would like to keep first passed argument as smaller than second array */
            if (lenA > lenB)
                return MedianOfSortedArray(b, a);

            int startA = 0, endA = lenA;

            // We keep no of elements on left side of partition(total count from A & B array) 'one more or equal' to no of elements on right side of partition (both array)
            while (startA <= endA)
            {
                /* Parition to decide 'no of elements on left side'
                 * Always keep no of elements on left one more or equal than total no of elements on right [including both arrays]
                 */
                int partitionA = (startA + endA) / 2;                       // elements before this Index are considered on left side of A         
                int partitionB = (lenA + lenB + 1) / 2 - partitionA;        // elements before this Index are considered on left side of B
                // We also add one while paritioning B above as its both 'odd and even friendly'


                // calculate value of elements on left & ride of partition for both arrays
                // [assign -ve infinity value in case we reach to -1 index and +ve infinity when we cross last index of array]
                int leftOfA, rightOfA, leftOfB, rightOfB;
                leftOfA = partitionA == 0 ? int.MinValue : a[partitionA - 1];
                rightOfA = partitionA == lenA ? int.MaxValue : a[partitionA];
                leftOfB = partitionB == 0 ? int.MinValue : b[partitionB - 1];
                rightOfB = partitionB == lenB ? int.MaxValue : b[partitionB];

                if (leftOfA <= rightOfB && leftOfB <= rightOfA)
                    if ((lenA + lenB) % 2 == 1)                             // combined length of arrays is ODD
                        return Math.Max(leftOfA, leftOfB);
                    else                                                    // combined length of arrays is EVEN
                        return (double)(Math.Max(leftOfA, leftOfB) + Math.Min(rightOfA, rightOfB)) / 2;
                else if (leftOfA < rightOfB && leftOfB > rightOfA)          // not quite at partition, we need to move more on right side of A
                    startA = partitionA + 1;
                else // if (leftOfA > rightOfB)                             // came too far on right, move to left
                    endA = partitionA - 1;
            }

            var errorMsg = "one or both provided Input arrays are Not Sorted";
            Console.WriteLine(errorMsg);
            throw new ArgumentException(errorMsg);
        }
    }
}
