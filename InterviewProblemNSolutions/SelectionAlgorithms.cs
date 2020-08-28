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
    }
}
