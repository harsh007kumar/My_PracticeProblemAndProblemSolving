﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace InterviewProblemNSolutions
{
    public class CityPair
    {
        public int left, right;
        public CityPair(int north, int south)
        { left = north; right = south; }
    }

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
            while (num > 1)
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
            while (i < num)
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
        public static int RecurrenceToCodeUsingDPEfficient(int num, int[] tab)
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

        // Algo doesn't work if all numbers are -ve (add extra check for input array if all are -ve return least -ve value)
        // Time O(n) || Space O(1)
        public static int MaxValueContinousSubsequenceInOn(int[] input)
        {
            int maxSumFoFar = 0, maxSumEndingHere = 0;
            for (int i = 0; i < input.Length; i++)
            {
                maxSumEndingHere = maxSumEndingHere + input[i] > 0 ? maxSumEndingHere + input[i] : 0;
                maxSumFoFar = Math.Max(maxSumFoFar, maxSumEndingHere);
            }
            return maxSumFoFar;
        }

        // Time (4^n) || Space O(1)
        // Returns No Of possible 'Binary Search Trees' with 'N' Nodes
        public static int CatalanNumberRecursion(int noOfNodes)
        {
            if (noOfNodes == 0) return 1;
            int count = 0;
            for (int i = 1; i <= noOfNodes; i++)
                count += CatalanNumberRecursion(i - 1) * CatalanNumberRecursion(noOfNodes - i);
            return count;
        }

        // Time O(n^2) computing CatalanNumber(n), we need to compute all of the CatalanNumber(i) values between 0 & n – 1, each one will be computed exactly once, in linear time
        // Space O(n)
        // Returns No Of possible 'Binary Search Trees' with 'N' Nodes
        public static int CatalanNumber(int noOfNodes, int[] memo)
        {
            if (noOfNodes == 0) return 1;
            if (memo[noOfNodes] != 0) return memo[noOfNodes];       // return value for sub-problem which was solved previously
            for (int i = 1; i <= noOfNodes; i++)
                memo[noOfNodes] += CatalanNumber(i - 1, memo) * CatalanNumber(noOfNodes - i, memo);
            return memo[noOfNodes];
        }

        // Here, p[] is arr of 'matrices'
        // MatrixChainOrderBruteForce(chainOfMatrices, 1, chainOfMatrices.Length - 1)
        public static int MatrixChainOrderBruteForce(int[] p, int start, int last)
        {
            if (start == last) return 0;
            int minOrder = int.MaxValue;
            for (int k = start; k < last; k++)
                minOrder = Math.Min(minOrder, MatrixChainOrderBruteForce(p, start, k) + MatrixChainOrderBruteForce(p, k + 1, last) + (p[start - 1] * p[k] * p[last]));
            return minOrder;
        }

        // Abdul Bari https://youtu.be/prx1psByp7U
        // Returns the 'Minimum No Of Multiplications' required to multiply matrics in 'chain Of Matrices' array
        // array p[] which represents the 'chain of matrices' such that the ith matrix A(i) is of the dimension p[i-1] x p[i].
        // Time O(n^3) as w need to compute matrix multiplication time for n^2 matrixces each operation takes liner time 'n' || Space O(n^2)
        // #DP #tabulation #bottom-up #approach
        public static int MatrixChainOrder(int[] p)
        {
            var len = p.Length - 1;         // No of matrix we can extract from 1-D array of ex size 3 {10,20,30} is 3-1=2 as 10x20, 20x30 these 2 form are input matrix

            /* '2-D table' used for Tabulation as we would be solving the problem in bottoms-up fashion
             * stores multiplications required to multiply matrices,
             * Ex: m[1,2] indicates no of multiplications for multipying Matrix.1 & Matrix.2,
             * similar m[3,3] indicates cost of multiplying Matrix.3 by itself which is 0
             */
            int[,] m = new int[len + 1, len + 1];       // one extra row & column are allocated (0th row & 0th colum are un-used)

            // set m[1,1], m[2,2], m[3,3] and so on.. as 0 as cost of matric multiplied by itself is Zero
            for (int i = 0; i < len + 1; i++)
                m[i, i] = 0;                            // we can skip this step in C# as default value of integer arrays are 0

            // below array stores partition point for the whole multiplication aka 'orderOfParenthesis'
            int[,] s = new int[len + 1, len + 1];

            for (int l = 2; l <= len; l++)              // length of matrix chain
                for (int i = 1; i <= len - l + 1; i++)  // loop to slide the current window of Matrices (shifting starting index of 1st Matrix)
                {
                    int j = i + l - 1;                  // to calculate last Matrix index at each step
                    m[i, j] = int.MaxValue;             // setting default value before finding min multiplications required
                    // trying all possible division b/w i..k & k..j
                    for (int k = i; k < j; k++)         // Parition point of current chain (point where we are placing parenthesis)
                    {
                        var currCost = m[i, k] + m[k + 1, j] + (p[i - 1] * p[k] * p[j]);
                        if (currCost < m[i, j])
                        {
                            m[i, j] = currCost;
                            s[i, j] = k;
                        }
                    }
                }
            
            Console.Write($" Printing Parenthesis for Matrix Multiplication : ");
            PrintParenthesis(p, s, 1, len);

            return m[1, len];                           // last value from the 1st row used
        }

        // Abdul Bari https://www.youtube.com/watch?v=eKkXU3uu2zk&ab_channel=AbdulBari
        public static void PrintParenthesis(int[] chainOfMatrices, int[,] partitionPoint, int firstMatrixId, int lastMatrixId)
        {
            Console.Write(" (");
            if (firstMatrixId == lastMatrixId)
                Console.Write($"{chainOfMatrices[lastMatrixId - 1]}x{chainOfMatrices[lastMatrixId]}");
            else
            {
                var paritionAt = partitionPoint[firstMatrixId, lastMatrixId];           // matrixMultiplications[row,col]

                // print Matrices on left of partition
                PrintParenthesis(chainOfMatrices, partitionPoint, firstMatrixId, paritionAt);
                // print Matrices on Right of partition
                PrintParenthesis(chainOfMatrices, partitionPoint, paritionAt + 1, lastMatrixId);
            }
            Console.Write(") ");
        }

        // Time Complexicity O(2^n) || Auxillary Space O(1) || Brute Force solution
        public static int KnapSackBrute(int W, int[] profit, int[] wt, int n)
        {
            if (n == 0 || W == 0) return 0;         // if no items left to choose from or sack capacity is Zero return 0 value.

            if (wt[n - 1] > W)                      // if weight of last item is larger than sack capacity, skip that item
                return KnapSackBrute(W, profit, wt, n - 1);

            return Math.Max(KnapSackBrute(W, profit, wt, n - 1),                    // not selecting (n-1)th item
                profit[n - 1] + KnapSackBrute(W - wt[n - 1], profit, wt, n - 1));   // selecting (n-1)th item & adding its profit
        }


        // Abdul Bari https://youtu.be/nLmhmB6NzcM
        // Time O(N*W), N = no of items/weights & W = capacity of knapsack || Space O(N*W)
        public static int KnapSack(int W, int[] profit, int[] wt, int n)
        {
            // Step1. create an 2D value array to store max profit, rows = noOfItems+1 & col = maxWeight knapsack can handle + 1
            int[,] val = new int[n + 1, W + 1];

            // Step2. Set 1st row & 1st col to zero, as there is 0 profit with 0 i.e,no wt selected(1st row) & no weight can be selected if given capacity is 0 (1st column)

            // Step3. start filling from i=1 row at each row try to maximize the profit(row no indicates no of items we can choose from start of weight array),
            // (under given constrait of max wt possible indicated by column no, last col indicated max capacity of KnapSack i.e, W)
            for (int i = 0; i <= n; i++)
                for (int w = 0; w <= W; w++)
                // Max of either (value from last row & same col) or (profit of current row item + value from last row & (current col W - wt[i] wt of current item))
                // val[i, w] = Math.Max(val[i - 1, w], profit[i] + val[i - 1, w - wt[i]]);  // use this, if we have we have 0 as first element of both profit & wt array
                {
                    if (i == 0 || w == 0)       // can skip this step as default value is 0 for new int array in C# & instead use continue statement here.
                        val[i, w] = 0;
                    else if (w - wt[i - 1] >= 0)// as wt should not be -ve at any instance (avoids invalid index exception)
                        val[i, w] = Math.Max(val[i - 1, w], profit[i - 1] + val[i - 1, w - wt[i - 1]]);
                    else
                        val[i, w] = val[i - 1, w];
                }
            
            PrintItemsPicked(val, profit, wt, n, W);
            return val[n, W];                   // Max Profit possible with all possible wt to choose from and under Full Capacity
        }

        public static void PrintItemsPicked(int[,] val, int[] profit, int[] wt, int row, int col)
        {
            while (row > 0 && col > 0)
            {
                if (val[row, col] == val[row - 1, col])    // it means current row item was not picked
                {
                    Console.Write($" wt:{wt[row - 1]} with Profit:{profit[row - 1]} \tNot Selected : \t 0");
                    row--;
                }
                else
                {
                    Console.Write($" wt:{wt[row - 1]} with Profit:{profit[row - 1]} \tSelected : \t 1");
                    row--;
                    col -= wt[row];
                }
                Console.WriteLine();
            }
        }

        // Tushar Roy https://youtu.be/Y0ZqKpToTic
        // Time O(N*C), N = no of coins & W = Change required for Value || Space O(N*C)
        // this problem is very similar to 0-1 knapsack problem just consider value of each coin to be same(ex: -1)
        public static int CoinChangeMinimumNoOfCoins(int C, int[] coins, int n)
        {
            // Create Table to store are intermediate results
            // Step1. Create Table to store are intermediate results min coins req, rows = noOfCoins + 1 & col = totalChange required + 1
            int[,] m = new int[n + 1, C + 1];

            // Step2. start filling from i=1 row at each row try to minimize the coins(row no indicates no of items we can choose from start of coins array),
            // (under given constrait of change required for Value indicated by column no, last col indicated max value for which Change is requied i.e, C)
            for (int i = 0; i <= n; i++)
                for (int c = 1; c <= C; c++)
                {
                    if (i == 0)                 // Extra Row, Set Int.Max value here so its not picked when we are picking Min value later
                        m[i, c] = int.MaxValue;
                    else if (i == 1)            // As first row is of 1.Re coin filling same no of coins as the 'Total Amt/Buck' required fr that column (Ex- Amt=5, 1.Re x 5 coins)
                        m[i, c] = c;
                    else if (c == 0)            // As first column represents 'Total Amt = 0' hence no coins should be selected to get 0 bucks
                        m[i, c] = c;
                    // Min of coins either (value from last row & same col) or (1 coin + value from last row & current col Amt - picked coin value)
                    else if (c >= coins[i - 1])
                        m[i, c] = Math.Min(m[i - 1, c], 1 + m[i - 1, c - coins[i - 1]]);
                    else
                        m[i, c] = m[i - 1, c];

                }
            PrintCoinsPicked(m, coins, n, C);
            return m[n, C];                     // Min Coins Requierd to make change with all possible coins to choose from
        }

        public static void PrintCoinsPicked(int[,] val, int[] coins, int row, int col)
        {
            while (row > 0 && col > 0)
            {
                if (val[row, col] == val[row - 1, col])     // current row coin not selected
                {
                    Console.WriteLine($" Coins {coins[row - 1]} \tNot Selected : \t0");
                    row--;
                }
                else
                {
                    Console.WriteLine($" Coins {coins[row - 1]} \tSelected : \t1");
                    row--;
                    col -= coins[row];
                }
            }
        }

        // Time O(n^2) || Space O(n)
        public static int LongestIncreasingSubsequence(int[] input)
        {
            var len = input.Length;

            // Create a array to store max increasing length till given ith index
            int[] tab = new int[len];
            int maxLen = 1;

            // set default value in table as 1 as min len of sequencec can b 1
            for (int i = 0; i < len; i++)
                tab[i] = 1;

            for (int i = 0; i < len; i++)           // index for which we are updating maxLen value
            {
                for (int j = 0; j < i; j++)         // iterate thru each index from 0 to i-1 to find max len for i
                    // if value at i is greater than value at j and maxLen value for i is less than maxLenValue at j + 1 (adding 1 as we r including ith value in array)
                    if (input[i] > input[j] && tab[i] < tab[j] + 1)
                        tab[i] = tab[j] + 1;
                if (tab[i] > maxLen)                // keep updating max length so far
                    maxLen = tab[i];
            }
            PrintIncreasingSequence(tab, maxLen, input, len);
            return maxLen;
        }

        public static void PrintIncreasingSequence(int[] table, int maxLen, int[] input, int len)
        {
            Console.Write($" Numbers selected in sequence (starting from last index): \t");
            for (int i = len - 1; i >= 0; i--)
                if (table[i] == maxLen)
                {
                    Console.Write($" {input[i]}");
                    maxLen--;
                }
            Console.WriteLine();
        }

        // This Bridge Problem is variation of LIS i.e, LongestIncreasingSubsequence, other similar ex are 'Stacking Boxes over each other for Max Height'
        // Time O(n^2) || Space O(n)
        public static int MaxBridges(List<CityPair> cities, int len)
        {
            // sort the cities based on either North or Sourth side cities Number
            cities = cities.OrderBy(c => c.left).ToList<CityPair>();        // LINQ function sorting cities as per left side of river
            

            // create table to store intermittent values, max cities possible till given index
            int[] tab = new int[len];
            

            // set default values in table for each index as 1 (min 1 bridge can surely be constructed over any pari of cities
            for (int i = 0; i < len; i++)
                tab[i] = 1;

            int maxBridges = 0;     // we can have atleast 1 bridge
            
            // check from left for increasing order
            for (int i = 0; i < len; i++)
            {
                for (int j = 0; j < i; j++)
                    if (cities[j].right < cities[i].right && tab[i] < 1 + tab[j])
                        tab[i] = 1 + tab[j];
                if (tab[i] > maxBridges)
                    maxBridges = tab[i];
            }

            #region Not necessary if we get values right with just above loop, for some inputs this works
            //for (int i = 0; i < len; i++)
            //    tab[i] = 1;
            //// check again from right for decreasing order
            //for (int i = len - 1; i >= 0; i--)
            //{
            //    for (int j = len - 1; j > i; j--)
            //        if (cities[j].right < cities[i].right && tab[i] < 1 + tab[j])
            //            tab[i] = 1 + tab[j];
            //    if (tab[i] > maxBridges)
            //        maxBridges = tab[i];
            //}
            #endregion

            return maxBridges;
        }

        public static void PrintBridgesSequence(int[] table, int totalBridges, List<CityPair> cities, int len)
        {
            Console.Write($" Bridges can be built b/w cities: \t");
            for (int i = len - 1; i >= 0; i--)
                if (table[i] == totalBridges)
                {
                    Console.Write($" {cities[i].right}");
                    totalBridges--;
                }
            Console.WriteLine();
        }

        // can be called in List of city as => cities.Sort(new SortCity());
        public class SortCity : IComparer<CityPair>
        {
            public int Compare(CityPair x, CityPair y) => x.right.CompareTo(y.right);
        }
    }
}
