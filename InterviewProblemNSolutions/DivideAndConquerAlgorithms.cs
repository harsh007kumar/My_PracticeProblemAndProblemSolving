using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblemNSolutions
{
    public class DivideAndConquerAlgorithms
    {
        // Time O(nlogn) || Space O(1) (does required recursion stack thou)
        public static int MaxValueContinousSubsequence(int[] input, int start, int last)
        {
            if (start == last) return input[start] > 0 ? input[start] : 0;
            var mid = start + (last - start) / 2;
            var leftHalfMax = MaxValueContinousSubsequence(input, start, mid);
            var rightHalfMax = MaxValueContinousSubsequence(input, mid + 1, last);

            int i = 0, leftMax = 0, rightMax = 0, currentSum = 0;
            // for left half find max sum value starting from mid index streching to its left
            for (i = mid; i >= 0; i--)
            {
                currentSum += input[i];
                if (currentSum > leftMax)
                    leftMax = currentSum;
            }
            currentSum = 0; // reset sum

            // for right half find max sum value starting from mid+1 index streching to its right
            for (i = mid + 1; i <= last; i++)
            {
                currentSum += input[i];
                if (currentSum > rightMax)
                    rightMax = currentSum;
            }
            // return max value from left half or right half or combined max value from left+right
            return Math.Max(leftHalfMax, Math.Max(rightHalfMax, leftMax + rightMax));
        }

        // Time O(logn), Ex- 9^24 can we calculated as (9^12)^2 = ((9^6)^2)^2 = (((9^3)^2)^2)^2 = (((9.9^2)^2)^2)^2 total 5 multiplications required
        // as apposed to Linear way where we multiple no by itself O(n-1) times
        public static long FindPower(long num, long power)
        {
            if (power == 0)
                return 1;
            if (power % 2 == 1)
            {
                var oddPower = FindPower(num, power - 1);
                return num * oddPower;
            }
            else
            {
                var evenPower = FindPower(num, power / 2);
                return evenPower * evenPower;
            }
        }
    }
}
