using System;

namespace LargestSumContiguousSubarray
{
    // https://www.geeksforgeeks.org/largest-sum-contiguous-subarray/
    // Write an efficient C program to find the sum of contiguous subarray within a one-dimensional array of numbers which has the largest sum.

    class MainClass
    {
        public static void Main(string[] args)
        {
            int[] arr = { -2, -3, 4, -1, -2, 1, 5, -3 };
            int lengthOfContiguousSubArray = 5;
            Console.WriteLine(LargestSumContiguousSubarray(arr, lengthOfContiguousSubArray));
            Console.ReadLine();
        }

        private static int LargestSumContiguousSubarray(int[] arr, int n)
        {
            int i, x, sumLast =Int32.MinValue, sumNow, len = arr.Length;
            for (i = 0; i <= len-n; i++)
            {
                sumNow = 0;
                x = i + n - 1;
                while(x>=i)
                {
                    sumNow += arr[x];
                    x--;
                }
                if (sumNow > sumLast)
                    sumLast = sumNow;
            }
            return sumLast;
        }
    }
}
