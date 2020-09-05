using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblemNSolutions
{
    public static class DynamicProgramming
    {
        static int[] minSteps = new int[100];

        // Ques. https://youtu.be/f2xi3c1S95M

        /// <summary>
        /// Recursive way to solve the given problem
        /// </summary>
        /// <param name="no"></param>
        /// <returns></returns>
        public static int GetMinSteps_Recursive(int no)
        {
            Console.Write($" {no}");
            if (no == 1) return 0;                      // 0 steps required to minimize 1 to 1
            else if (no == 2) return 1;                 // no '2' divide by 2 once give us 1, hence 1 step
            else if (no == 3) return 1;                 // no '3' divide by 3 once give us 1, hence 1 step
            else if (no % 3 == 0) return 1 + GetMinSteps_Recursive(no / 3);   // if 'no' is divisible by 3 || return 1 step + recursive call : no/3
            else if (no % 2 == 0) return 1 + GetMinSteps_Recursive(no / 2);   // if 'no' is divisible by 2 || return 1 step + recursive call : no/2
            return 1 + GetMinSteps_Recursive(no - 1);     // none of above feasible simply Minimize no - 1 || return 1 step + recursive call : no - 1
        }

        // Memoization (top-down)approach
        public static int GetMinSteps_DP_Memo(int no, ref int[] memo)
        {
            if (no == 1 || memo[no] != 0) return memo[no];  // if no = 1 return 0 or if we found value for no in our memo array no calculation needed return the value

            if (no % 3 == 0)
                memo[no] = 1 + GetMinSteps_DP_Memo(no / 3, ref memo);
            else if (no % 2 == 0)
                memo[no] = 1 + GetMinSteps_DP_Memo(no / 2, ref memo);
            else
                memo[no] = 1 + GetMinSteps_DP_Memo(no - 1, ref memo);
            return memo[no];
        }

        // Tabulation (bottom-up) approach
        public static int[] GetMinSteps_DP_Tab(int no)
        {
            var tab = new int[no];
            for (int i = 0; i < no; i++)                    // Initiliaze array to intMax
                tab[i] = int.MaxValue;
            tab[1] = 0;                                     // update step required for num 1 as 0 (base condition)

            // we start updating value from 1..2..3..4..5....no in tab array so we can utilize it later
            for (int i = 2; i < no; i++)
            {
                if (i % 3 == 0)
                    tab[i] = 1 + tab[i / 3];
                else if (i % 2 == 0)
                    tab[i] = 1 + tab[i / 2];
                else
                    tab[i] = 1 + tab[i - 1];
            }
            return tab;
        }

        public static int FibonnaciRecursive(int num)
        {
            if (num == 0) return 0;
            if (num == 1) return 1;
            return FibonnaciRecursive(num - 1) + FibonnaciRecursive(num - 2);
        }

        public static int FibonnaciIterative(int num)
        {
            if (num < 1) return 0;
            int fib0 = 0, fib1 = 1;
            int result = 1;
            while(num>1)
            {
                result = fib0 + fib1;       // saving results in table while moving bottoms-up will result in subsquently faster response time finding result for nums in array
                fib0 = fib1;
                fib1 = result;
                num--;
            }
            return result;
        }

        public static long FactorialRecursive(long num)
        {
            if (num < 2) return 1;
            return num * FactorialRecursive(num - 1);
        }

        public static long FactorialIterative(long num)
        {
            if (num < 2) return 1;              // base case for num is 0 or 1
            long lastFact = 2, currNum = 2, result = 2;

            while (num > currNum++)             // while currNum is not equal to input num keep on calculating factorial
            {
                result = currNum * lastFact;
                lastFact = result;
                //tabulation[currNum] = result;   // addtionally Storing values while iterating bottom to up and returning from table in end = 'Tabulation'
            }
            return result;
        }

        public static long Factorial(long num, long[] memoizationTable)
        {
            if (num < 2) return 1;              // base case for num is 0 or 1
            if (memoizationTable[num] != 0) return memoizationTable[num];
            return memoizationTable[num] = num * Factorial(num - 1, memoizationTable);  // memorising values on our top down approach to reduce recalculation later = 'Memoization'
        }

        // Time O(n*(2^m)), n = length of 1st string, m = length of 2nd string || Auxilary Space O(1), thou requires recursive space
        public static int LengthLongestCommonSubsequence(string x, string y, int currPosX = 0, int currPosY = 0)
        {
            int lenX = x.Length;
            int lenY = y.Length;

            if (currPosX == lenX || currPosY == lenY)           // we reach end of string
                return 0;
            else if (x[currPosX] == y[currPosY])                // first Characters match, add 1 to length and recursively call on remaining strings
                return 1 + LengthLongestCommonSubsequence(x, y, currPosX + 1, currPosY + 1);
            else // (x[currPosX] not equals y[currPosY])        // either 1st char from X or from Y (or both) will not be part of common subsequence
                return Math.Max(LengthLongestCommonSubsequence(x, y, currPosX + 1, currPosY), LengthLongestCommonSubsequence(x, y, currPosX, currPosY + 1));
        }

        // Time O(n*m), n = length of 1st string, m = length of 2nd string || Auxilary Space O(n*m)
        public static int LongestCommonSubsequence(string x, string y, ref string lcsString)
        {
            int lenX = x.Length;
            int lenY = y.Length;
            int[,] LCS = new int[lenX + 1, lenY + 1];           // we create GRID with 1 more row & column than len of input strings, as we use 1st row & 1st col for default value
            int i = 0, j = 0;

            // to find length of common string
            for (i = 0; i < lenX; i++)                          // start from 1st characters of 'x'
                for (j = 0; j < lenY; j++)                      // start from 1st characters of 'y'
                {
                    if (x[i] == y[j])                           // match found
                        LCS[i + 1, j + 1] = 1 + LCS[i, j];
                    else
                        LCS[i + 1, j + 1] = Math.Max(LCS[i + 1, j], LCS[i, j + 1]);
                }
            
            LCS.Print();

            // to also find & print common string
            i = lenX; j = lenY;
            while (i > 0 && j > 0)
            {
                if (LCS[i, j] == LCS[i, j - 1])                 // value of LCS[i, j] came from LCS[i, j-1]
                    j--;
                else if (LCS[i, j] == LCS[i - 1, j])            // value of LCS[i, j] came from LCS[i-1, j]
                    i--;
                else //if (LCS[i, j] == 1 + LCS[i - 1, j - 1])  // if not from above two if wud surely came from cross LCS[i-1, j-1]
                {
                    i--; j--;
                    lcsString = x[i] + lcsString;
                }
            }
            return LCS[lenX, lenY];
        }

        public static int RecurrenceToCodeRecursive(int num)
        {
            if (num == 0 || num == 1) return 2;
            int i = 1, sum = 0;
            while(i<num)
            {
                sum += 2 * RecurrenceToCodeRecursive(i) * RecurrenceToCodeRecursive(i - 1);
                i++;
            }
            return sum;
        }

        // Bottom-up approach || Time Complexity O(n^2) for 2 loops || Space O(n)
        public static int RecurrenceToCodeUsingDP(int num, int[] tab)
        {
            tab[0] = 2;
            tab[1] = 2;
            if (num < 2) return 2;

            for (int i = 2; i <= num; i++)              // first we calculate answers for values which are smaller than 'num' but required to calculate num
            {
                if (tab[i] != 0) continue;              // useful when we are calling for list of no's (which we might have calculated in prv calls)
                for (int j = 1; j < i; j++)
                    tab[i] += 2 * tab[j] * tab[j - 1];
            }
            return tab[num];
        }

        // Bottom-up approach || Time Complexity O(n) || Space O(n)
        public static int RecurrenceToCodeUsingDP_Iterative(int num, int[] tab)
        {
            tab[0] = 2;
            tab[1] = 2;
            tab[2] = 2 * tab[1] * tab[0];

            for (int i = 3; i <= num; i++)              // first we calculate answers for values which are smaller than 'num' but required to calculate num
                tab[i] = tab[i - 1] + (2 * tab[i - 1] * tab[i - 2]);

            return tab[num];
        }

        // Time O(n) || Space O(n) for table
        public static int MaxValueContinousSubsequence(int[] input)
        {
            var len = input.Length;
            if (len < 1) return -1;

            int[] maxSumTillIndex = new int[len];
            maxSumTillIndex[0] = (input[0] > 0) ? input[0] : 0;

            // calculate max sum till ith index starting from 1th index to last
            for (int i = 1; i < len; i++)
                maxSumTillIndex[i] = maxSumTillIndex[i - 1] + input[i] > 0 ? maxSumTillIndex[i - 1] + input[i] : 0;

            int maxSum = 0;
            // find global max sum in maxSumTillIndex array
            for (int i = 0; i < len; i++)
                maxSum = Math.Max(maxSum, maxSumTillIndex[i]);

            return maxSum;
        }
    }
}
