using System;
using Searching;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Eventing.Reader;

namespace InterviewProblemNSolutions
{
    public class SearchAlgorithms
    {
        public static void DetectDuplicate(int[] input)
        {
            // array is not readonly
            // Algo works only if the Array containing Positive values in the range [0 .. N-1]
            for (int i = 0; i < input.Length; i++)                                                          // Time O(n) || Space O(1)
                if (input[Math.Abs(input[i])] < 0)              // already marked -ve
                    Console.WriteLine($"Duplicate '{Math.Abs(input[i])}' at Index : {i}");
                else
                    input[Math.Abs(input[i])] *= -1;            // mark -ve

            // METHOD 2 : Sort array and search for next value is same or not                               // Time O(nlogn) || Space O(1)
            // METHOD 3 : using Hashtable to store already seen values                                      // Time O(n) || Space O(n)
        }
        public static void MaxRecurrence(int[] input)
        {
            var len = input.Length;
            // array is not readonly
            for (int i = 0; i < len; i++)                                                                   // Time O(n) || Space O(1)
                input[input[i] % len] += len;                 // add array length, instead of marking the node -ve like in above solution to detect duplicates

            // Algo works only if the Array containing Positive values in the range [0 .. N-1]
            int max = -1, index = -1;
            for (int i = 0; i < len; i++)
                if (input[i] / len > max)                       // (input[i] > len*2) can be used to detect duplicates also
                {
                    max = input[i] / len;
                    index = i;
                }

            Console.WriteLine($" Element {input[index] % len} found max time in above array");
            // METHOD 2 : Sort array and search for next value is same and updating counter                 // Time O(nlogn) || Space O(1)
            // METHOD 3 : using Hashtable to store already seen values and their count                      // Time O(n) || Space O(n)
        }

        public static void MissingNo(int[] input)
        {
            // store XOR of input array values (NO DUPLICATE VALUES ALLOWED)
            int XOR = 0;
            for (int i = 0; i < input.Length; i++)
                XOR ^= input[i];

            // XOR all possible values in range [1..N], left value in XOR is missing Number
            for (int i = 1; i < input.Length + 1; i++)
                XOR ^= i;
            Console.WriteLine($"Missing value in above array is {XOR}");
        }

        /// <summary>
        /// Time O(n) || Space O(1)
        /// 
        /// Simple mathematical solution is to compute X & Y from below equations
        /// X + Y = Sum - (n(n+1)/2),  here n = max no in the range
        /// X * Y = P/n!
        /// Hence replacing X in above equvation gives us both X and Y
        /// </summary>
        /// <param name="input"></param>
        /// <param name="range"></param>
        /// <param name="length"></param>
        public static void TwoRepeatingElements(int[] input, int range, int length)
        {
            // 1# find XOR of values in array, so repeating elements would be removed as A ^ A = 0, lets call this XOR_A
            // 2# now XOR all the values from from 1..range with XOR_A, it will remove all values which were present in XOR_A (i.e, all nums which appeared once in input)
            // 3# now we have XOR_A containing XOR of our two repeating values 'X' & 'Y'
            // 4# to find X we need to find first right most bit in XOR_A which is set '1', lets call this no as RightMostSetBitNo
            // 5# Now we calculate XOR of all elements in input array whose above Bit is ON i.e, input[i] & SetBitNo > 0
            // 6# also we repeated same operation with values in range whose above Bit is ON i.e, range[i] & SetBitNo > 0
            // 7# XOR of values obtained from step 5th & 6th leaves us value = 'X' || As repeated elements will be (A ^ A ^ A = A) where as non repeated elements (B ^ B = 0)
            // XOR 'X' with XOR_A gets our second repeated value 'Y'
            int i = 0, XOR_Duplicates = 0, XOR_Input = 0, XOR_Range = 0;

            for (i = 0; i < length; i++)                    // Time O(n)
                XOR_Input ^= input[i];

            for (i = 1; i <= range; i++)                    // Time O(n-2)
                XOR_Range ^= i;

            XOR_Duplicates = XOR_Input ^ XOR_Range;

            var RightMostSetBitNo = Utility.GetRightMostBit(XOR_Duplicates); // O(1)

            XOR_Input = 0;
            for (i = 0; i < length; i++)                    // Time O(n)
                if ((input[i] & RightMostSetBitNo) > 0)
                    XOR_Input ^= input[i];

            XOR_Range = 0;
            for (i = 0; i <= range; i++)                    // Time O(n-2)
                if ((i & RightMostSetBitNo) > 0)
                    XOR_Range ^= i;

            // first repeated nun
            var X = XOR_Input ^ XOR_Range;
            // second repeated nun
            var Y = XOR_Duplicates ^ X;

            Console.WriteLine($"Repeated nums in given input array are : \t'{X}' and '{Y}'");
        }

        // Time O(n) || Space O(1)
        public static void PairWhoseSumIsClosestToGivenValue(int[] input, int sum = 0)
        {
            int low = 0, high = input.Length - 1;
            int p1 = -1, p2 = -1, diff = int.MaxValue;
            while (low < high)          // Time O(n)
            {
                if (Math.Abs(input[low] + input[high] - sum) < diff)
                {
                    p1 = low; p2 = high;
                    diff = Math.Abs(input[low] + input[high] - sum);
                    if (diff == 0) break;
                }

                if (input[low] + input[high] < sum)
                    low++;              // move to larger values
                else
                    high--;             // move to smaller values
            }
            Console.WriteLine($" Pair '{input[p1]}' & '{input[p2]}' matches/closet to Sum : {sum}");
        }

        // Time O(n^2) || Space O(1)
        public static void TripletWhoseSumIsClosetToGivenValue(int[] input, int sum = 0)
        {
            int p1 = -1, p2 = -1, p3 = -1;
            int diff = int.MaxValue;
            for (int i = 0; i < input.Length; i++)          // Time O(n)
            {
                #region PairWhoseSumIsClosedToGivenValue(input, sum);
                int low = i + 1, high = input.Length - 1;
                while (low < high)                          // Time O(n)
                {
                    var currSum = input[i] + input[low] + input[high];
                    if (Math.Abs(currSum - sum) < diff)
                    {
                        p1 = i; p2 = low; p3 = high;
                        diff = Math.Abs(currSum - sum);
                        if (diff == 0) break;
                    }
                    if (currSum < sum)
                        low++;                              // move to larger values
                    else // (currSum > sum)
                        high--;                             // move to smaller values
                }
                #endregion
                if (diff == 0) break;
            }
            Console.WriteLine($" Pair '{input[p1]} {input[p2]}' & '{input[p3]}' matches/closet to Sum : {sum}");
        }

        // Time O(logn)
        public static int IncreasingSequence(int[] input, int start, int last)
        {
            while (start <= last)
            {
                if (start == last)              // single element
                { Console.WriteLine($"Mid Point of the array is : \t{input[start]}"); return start; }
                else if (start == last - 1)     // only two elements
                { Console.WriteLine($"Mid Point of the array is : \t{input[Math.Max(start, last)]}"); return Math.Max(start, last); }
                else
                {
                    var mid = start + (last - start) / 2;

                    if (input[mid - 1] < input[mid] && input[mid] > input[mid + 1])
                    { Console.WriteLine($"Mid Point of the array is : \t{input[mid]}"); return mid; }
                    else if (input[mid - 1] > input[mid] && input[mid] > input[mid + 1])
                        last = mid - 1;
                    else if (input[mid - 1] < input[mid] && input[mid] < input[mid + 1])
                        start = mid + 1;
                    else
                        return -1;
                }
            }
            return -1;
        }

        // GFG https://www.geeksforgeeks.org/search-an-element-in-a-sorted-and-pivoted-array/
        // Time O(Logn) || Space O(1) || 2 pass
        public static int BinarySearchInRotatedArray(int[] input, int element)
        {
            if (input == null || input.Length == 0) return -1;
            // find pivot, element after which following element in smaller (there can be only once such index)
            // divide the array into two sub-array left and right
            // if 'element' is greater than value present at first index search in left sub-array, else search in right

            var pivot = FindPivotInRotatedArray(input);             // Time O(Logn)

            if (pivot == -1)                    // array not rotated
                return Search.BinarySearch_Iterative(input, 0, input.Length - 1, element);  // search in entire array
            else if (input[pivot] == element)   // element found
                return pivot;
            else if (input[0] <= element)        // search in left sub-array
                return Search.BinarySearch_Iterative(input, 0, pivot - 1, element);
            else // (input[0] > element)        // search in right sub-array
                return Search.BinarySearch_Iterative(input, pivot + 1, input.Length - 1, element);
        }

        // Time O(Logn) || Space O(1)
        public static int FindPivotInRotatedArray(int[] input)
        {
            int low = 0, high = input.Length - 1, pivot = -1;
            while (low <= high)
            {
                if (low == high)
                { pivot = low; break; }
                else if (low == high - 1)
                { pivot = input[low] > input[high] ? low : high; break; }

                var mid = low + (high - low) / 2;

                if (mid < high && input[mid] > input[mid + 1]) return mid;
                else if (low < mid && input[mid - 1] > input[mid]) return mid - 1;
                else if (input[low] > input[mid]) low = mid + 1;
                else high = mid - 1;
            }
            return pivot;
        }

        // Time O(Logn) || Space O(1) || 1 pass || Recursive Solution
        // Time O(n) Worst Case when input array can have duplicate values
        public static int BinarySearchInRotatedArraySinglePass(int[] input, int start, int end, int element)
        {
            if(start<=end)
            {
                var mid = start + (end - start) / 2;
                if (input[mid] == element) return mid;
                // [Add this case when duplicates values r possible in inputArr]
                else if (input[start] == input[mid]) start++;
                // if first half is sorted
                else if (input[start] <= input[mid])
                {
                    if (input[start] <= element && element <= input[mid])
                        return BinarySearchInRotatedArraySinglePass(input, start, mid - 1, element);
                    else
                        return BinarySearchInRotatedArraySinglePass(input, mid + 1, end, element);
                }
                // second half must be sorted
                else
                {
                    if (input[mid] <= element && element <= input[end])
                        return BinarySearchInRotatedArraySinglePass(input, mid + 1, end, element);
                    else
                        return BinarySearchInRotatedArraySinglePass(input, start, mid - 1, element);
                }
            }
            return -1;
        }

        // Time O(Logn) || Space O(1) || 1 pass
        // Time O(n) Worst Case when input array can have duplicate values
        public static int BinarySearchInRotatedArraySinglePassIterative(int[] input, int start, int end, int element)
        {
            while (start <= end)
            {
                var mid = start + (end - start) / 2;
                if (input[mid] == element) return mid;
                else if (input[start] == input[mid])    // [Add this case when duplicates values r possible in inputArr]
                    start++;
                else if (input[start] <= input[mid])    // if first half is sorted
                    if (input[start] <= element && element < input[mid]) end = mid - 1;
                    else start = mid + 1;
                else                                    // second half must be sorted
                    if (input[mid] < element && element <= input[end]) start = mid + 1;
                    else end = mid - 1;
            }
            return -1;
        }

        // Time (Logn) || Space O(1)
        public static int FirstOccurenceInSortedArray(int[] input, int data)
        {
            if (input == null || input.Length == 0) return -1;
            int start = 0, last = input.Length - 1;
            while (start <= last)
            {
                var mid = start + (last - start) / 2;
                if (start == mid && input[mid] == data) return mid;
                else if (input[mid] == data && input[mid - 1] < data) return mid;
                else if (input[mid] >= data) last = mid - 1;
                else start = mid + 1;
            }
            return -1;
        }

        // Time (Logn) || Space O(1)
        public static int LastOccurenceInSortedArray(int[] input, int data)
        {
            if (input == null || input.Length == 0) return -1;
            int start = 0, last = input.Length - 1;
            while (start <= last)
            {
                var mid = start + (last - start) / 2;
                if (mid == last && input[mid] == data) return mid;
                else if (input[mid] == data && input[mid + 1] > data) return mid;
                else if (input[mid] <= data) start = mid + 1;
                else last = mid - 1;
            }
            return -1;
        }

        // Time O(n) || Space O(1)
        // Similar problem is seperating O's & 1's which is also similar to 'Dutch National Flag' problem or '3-Way Quick-Sort'
        public static void SeperateEvenOdd(int[] input)
        {
            if (input == null || input.Length == 0) return;
            int low = 0, high = input.Length - 1;
            while (low < high)
            {
                while (input[low] % 2 == 0) low++;
                while (input[high] % 2 == 1) high--;
                if (low < high) Utility.Swap(ref input[low++], ref input[high--]);
            }
        }

        // Time O(n) || Space O(n)
        public static void MaxIndexDiff(int[] input)
        {
            if (input == null || input.Length <= 1) return;
            var len = input.Length;
            int i = 0, j = 0;

            // create leftMin array to store smallest value on left side Arr[i] in input array
            int[] leftMin = new int[len];
            // create rightMax array to store largest value on Right side Arr[j] in input array
            int[] rightMax = new int[len];

            // set initial first value in leftMin array and than populate the remaing array from the start
            // by storing Minimum of values at current index from input array or value on left side of it in leftMin array
            leftMin[0] = input[0];
            for (i = 1; i < len; i++)
                leftMin[i] = Math.Min(leftMin[i - 1], input[i]);

            // set initial first value i rightMax array and than populate the remaing array from the end
            // by storing Maximum of values at current index from input array or value on right side of it in rightMax array
            rightMax[len-1] = input[len - 1];
            for (j = len - 2; j >= 0; j--)
                rightMax[j] = Math.Max(input[j], rightMax[j + 1]);

            int maxDiff = -1;
            i = j = 0;
            while (i < len && j < len)
            {
                if (leftMin[i] < rightMax[j])
                {
                    maxDiff = Math.Max(maxDiff, j - i);
                    j++;
                }
                else
                    i++;
            }
            Console.WriteLine($" The Max Difference 'j-i' which holds true for 'A[j] > A[i]' is : {maxDiff}");
        }

        // Time O(n) || Space O(1)
        // Input array is being used as HashTable itself to store the count
        public static void CountFrequencyNegationMethod(int[] input)
        {
            Console.WriteLine($" Finding frequency using Negation Method : ");
            if (input == null || input.Length < 1) return;
            int pos = 0, prvPos = -1, len = input.Length;
            while (pos < len)               // Time O(n)
            {
                prvPos = input[pos] - 1;    // since array contains element from 1...N but index are in range 0...N-1
                // if current element is encountered for first time
                if (input[pos] > 0)
                {
                    if (input[prvPos] > 0)  // some other element exists at prvPos index
                    {
                        Utility.Swap(ref input[pos], ref input[prvPos]);
                        input[prvPos] = -1; // set -1 to indicate this element is visited once
                    }
                    else                    // prvPos is already -ve means we are encountering this value i.e, input[pos] again
                    {
                        input[prvPos]--;    // increament the counter
                        input[pos++] = 0;   // set 0 counter for current 'pos'
                    }
                }
                else 
                    pos++;
            }

            // Print the frequency for all elements now
            pos = 0;
            while (pos < len)               // Time O(n)
                Console.WriteLine($" Frequency of {pos + 1} : {Math.Abs(input[pos++])}");

            Console.WriteLine();
        }

        // Time O(n) || Space O(1)
        // Issue with this approach is it only prints Frequency of No present in array (that too multiple times if present more than once)
        public static void CountFrequencyAddArrayLengthMethod(int[] input)
        {
            Console.WriteLine($" Finding frequency using Max Recourrence Method : ");
            if (input == null || input.Length < 1) return;
            int len = input.Length;

            // decreament all values so is fits in index range 0...N-1
            for (int i = 0; i < len; i++)
                input[i]--;
            
            // add length of array to each value
            for (int i = 0; i < len; i++)
                input[input[i] % len] += len;

            for (int i = 0; i < len; i++)
                Console.WriteLine($" Frequency of {input[i] % len + 1} : {input[input[i] % len] / len} ");

        }
    }
}
