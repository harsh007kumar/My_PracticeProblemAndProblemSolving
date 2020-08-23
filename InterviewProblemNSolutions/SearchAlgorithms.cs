using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblemNSolutions
{
    public class SearchAlgorithms
    {
        public static void DetectDuplicate(int[] input)
        {
            // array is not readonly
            // Algo works only if the Array containing Positive values in the range [0 .. N-1]
            for (int i = 0; i < input.Length; i++)                                                          // Time O(n) || Space O(1)
                if (input[i] < 0)                               // already marked -ve
                    Console.WriteLine($"Duplicate '{Math.Abs(input[i])}' at Index : {i}");
                else
                    input[input[i]] *= -1;                      // mark -ve

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
                if (input[i] / len > max)
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

        // Time O(n) || Space O(1)
        public static void TwoRepeatingElements(int[] input, int range, int length)
        {
            // 1# find XOR of values in array, so repeating elements would be removed as A ^ A = 0, lets call this XOR_A
            // 2# now XOR all the values from from 1..range with XOR_A, it will remove all values which were present in XOR_A (i.e, all nums which appeared once in input)
            // 3# now we have XOR_A containing XOR of our two repeating values 'X' & 'Y'
            // 4# to find X we need to find first right most bit in XOR_A which is set '1', lets call this no as RightMostSetBitNo
            // 5# Now we calculate XOR of all elements in input array whose above Bit is ON i.e, input[i] & SetBitNo = 0
            // 6# also we repeated same operation with values in range whose above Bit is ON i.e, range[i] & SetBitNo = 0
            // 7# XOR of values obtained from step 5th & 6th leaves us value = 'X' || As repeated elements will be (A ^ A ^ A = A) where as non repeated elements (B ^ B = 0)
            // XOR 'X' with XOR_A gets our second repeated value 'Y'
            int i = 0, XOR_Duplicates = 0, XOR_Input = 0, XOR_Range = 0;

            for (i = 0; i < length; i++)                    // Time O(n)
                XOR_Input ^= input[i];

            for (i = 1; i <= range; i++)                    // Time O(n-2)
                XOR_Range ^= i;

            XOR_Duplicates = XOR_Input ^ XOR_Range;

            var RightMostSetBitNo = Utility.GetRightMostBit(XOR_Duplicates);

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
    }
}
