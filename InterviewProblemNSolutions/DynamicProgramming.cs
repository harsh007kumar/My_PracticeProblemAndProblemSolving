using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace InterviewProblemNSolutions
{
    public class CityPair
    {
        public int left, right;
        public CityPair(int north, int south)
        { left = north; right = south; }
    }

    public class Person
    {
        public int ht, wt;
        public Person(int height, int weight)
        { ht = height; wt = weight; }
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
        // KadaneAlgo()
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
        // Time = Space = O(n*amt), n = no of coins & amt = Change required for Value
        // this problem is very similar to 0-1 knapsack problem just consider value of each coin to be same(ex: -1)
        public static int CoinChangeMinimumNoOfCoins(int[] coins, int amt)
        {
            int n = coins.Length;
            // Create Table to store are intermediate results
            // Step1. Create Table to store are intermediate results min coins req, rows = noOfCoins + 1 & col = totalChange required + 1
            int[,] dp = new int[n + 1, amt + 1];

            // Step2. start filling from i=1 row at each row try to minimize the coins(row no indicates no of items we can choose from start of coins array),
            // (under given constrait of change required for Value indicated by column no, last col indicated max value for which Change is requied i.e, C)
            for (int i = 0; i <= n; i++)
                for (int c = 1; c <= amt; c++)
                {
                    if (i == 0)                 // Extra Row, Set MaxChangeAmt+1 / Int.Max/4 value here so its not picked when we are picking Min value later
                        dp[i, c] = amt + 1;
                    //else if (c == 0)            // As 1st column represents 'Total Amt = 0' hence no coins should be selected to get 0 bucks
                    //    dp[i, c] = 0;
                    // Min of coins either (value from last row & same col) or (picking curr row coin + value from current row & (current col Amt - picked coin value))
                    else if (c >= coins[i - 1])
                        dp[i, c] = Math.Min(dp[i - 1, c], 1 + dp[i, c - coins[i - 1]]);
                    else
                        dp[i, c] = dp[i - 1, c];
                }
            PrintCoinsPicked(dp, coins, n, amt);
            return dp[n, amt] <= amt ? dp[n, amt] : -1;// Min Coins Requierd to make change with all possible coins to choose from
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
                    col -= coins[row - 1];
                }
            }
        }


        // Time = O(n*amt) || Space = O(amt), n = no of coins
        public static int CoinChange_DP_BottomUp(int[] coins, int amt)
        {
            int[] dp = new int[amt + 1];

            for (int i = 0; i < dp.Length; i++)
                dp[i] = amt + 1;

            dp[0] = 0;      // base case as to fetch 0 amt we need 0 coins
            for (int c = 1; c <= amt; c++)
                for (int i = 0; i < coins.Length; i++)
                    if (c >= coins[i])      // if current amt is greater or equal to ith coinType
                        // Min of either current no of coins or taking ith coin and value at current amt - coinValue which is picked
                        dp[c] = Math.Min(dp[c], 1 + dp[c - coins[i]]);
            
            return dp[amt] <= amt ? dp[amt] : -1;
        }


        // Time O(n^2) || Space O(n)
        public static int LongestIncreasingSubsequence(int[] input)
        {
            var len = input.Length;

            // Create a array to store max increasing length till given ith index
            int[] tab = new int[len];
            int maxLen = 1;

            // set default value in table as 1 as min len of sequence can be 1
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

        // This Bridge Problem is variation of LIS i.e, LongestIncreasingSubsequence, other similar ex are 'Stacking Boxes over each other for Max Height' & 'Circus Tower Routine'
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

        // Tushar Roy https://youtu.be/s6FhG--P7z0
        // Time O(N*S), N = no of items/no's & S = desired sum || Space O(N*W)
        // This Problem is a variation of 0-1 KnapSack Problem, instead of profit array we have no's whose total sum should match given Sum 'S'
        // Returns true if subset in array exists who's Summation equals Sum
        public static bool SubsetSum(int[] input, int len, int sum)
        {
            bool[,] tab = new bool[len + 1, sum + 1];   // 1 extra row & col for default values

            for (int i = 0; i <= len; i++)
                for (int s = 0; s <= sum; s++)
                {
                    if (s == 0) tab[i, s] = true;       // default value 'True' for 1st col, which indicates Curr Desired Sum = 0, which is always achievable by not selecting any element
                    else if (i == 0) tab[i, s] = false;  // default value 'False' for 1st row, which indicates no number is avaliable, hencec can't select any no to make given Sum
                    else if (s >= input[i - 1]) tab[i, s] = tab[i - 1, s] || tab[i - 1, s - input[i - 1]];  // set true if either (row above with same col is true) or (row above n col = 'current desired Sum - current no val' is true)
                    else tab[i, s] = tab[i - 1, s];
                }
            // tab.Print();
            PrintSubSetOfNumberForGivenSum(tab, input, len, sum);
            return tab[len, sum];
        }

        public static void PrintSubSetOfNumberForGivenSum(bool[,] tab, int[] input, int row, int col)
        {
            while (row > 0 && col > 0)
            {
                if (tab[row, col] == tab[row - 1, col])//value came row above, hence current item/no must have have been selected
                {
                    Console.WriteLine($" '{input[row - 1]}' \tNot selected");
                    row--;
                }
                else // subset must have come after selcting current row no from input
                {
                    Console.WriteLine($" '{input[row - 1]}' \tSelected");
                    row--;
                    col -= input[row];
                }
            }
        }

        // recursive function return true if subset in array exists who's Summation equals Sum
        // Time O(2^n) we have 2 decisions to make at every index from 0..N-1 || Recursive Space Required!
        public static bool SubSetSumRecursive(int[] input, int len, int sum)
        {
            // we have reduced initial sum that was provided to 0 by selecting some elements in array
            if (sum == 0) return true;
            // reached end of array
            if (len == 0) return false;

            // 2 choice : (Either Not selecting last element) or (select last element n reduce now requied sum by same value)
            return SubSetSumRecursive(input, len - 1, sum) || SubSetSumRecursive(input, len - 1, sum - input[len - 1]);
        }

        // return true if canPartition array into 2 subset who's sum are equal
        public static bool FindPartition(int[] input, int len)
        {
            int totalSum = 0;
            // get total sum of array
            for (int i = 0; i < len; i++) totalSum += input[i];

            // odd total sum, cannot have 2 subset with equal sum
            if (totalSum % 2 == 1) return false;

            // return true if subSet with Half of total sum exists
            //return SubsetSum(input, len, totalSum / 2);                                             // DP Tabulation bottom up O(N*S) [Constraint space is very high if totalSum is large]
            //return SubSetSumRecursive(input, len, totalSum / 2);                                    // Time Consuming O(2^n) recursive approach
            return SubsetSumMemo(input, len, totalSum / 2, 0, new int[len + 1, totalSum / 2]);      // DP Memo Top-Down, FASTEST
            //return SubSetSumMemo(input, len, totalSum / 2, new Dictionary<string, bool>());         // DP Memoization top down approach Old
        }
        // Memoization based || Top Down Approach
        // Time O(2^n) we have 2 decisions to make at every index from 0..N-1 || Recursive Space Required!
        public static bool SubsetSumMemo(int[] nums, int len, int requiredSum, int currSum, int[,] cache)
        {
            if (requiredSum == currSum) return true;
            if (len <= 0 || requiredSum < currSum) return false;
            if (cache[len - 1, currSum] != 0) return cache[len - 1, currSum] == 2;

            bool result = false;
            if (requiredSum >= nums[len - 1]) result = SubsetSumMemo(nums, len - 1, requiredSum, currSum + nums[len - 1], cache);
            if (!result) result = SubsetSumMemo(nums, len - 1, requiredSum, currSum, cache);

            cache[len - 1, currSum] = result ? 2 : 1;
            return cache[len - 1, currSum] == 2;
        }
        // https://youtu.be/3N47yKRDed0
        // Memoization based || Top Down Approach
        // Time O(2^n) we have 2 decisions to make at every index from 0..N-1 || Recursive Space Required!
        public static bool SubSetSumMemo(int[] input, int len, int requiredSum, Dictionary<string, bool> state, int currentSum = 0)
        {
            // currentSum equals requriedSum
            if (currentSum == requiredSum)
                return true;

            // reached end of array
            if (len <= 0 || currentSum > requiredSum) return false;

            var key = len - 1 + "" + currentSum;    // made key or currentIndex + currentSum
            // if we have precomputed this sub-problem before return from map
            if (state.ContainsKey("key"))
                return state[key];

            // 2 choice : (Either Not selecting last element) or (select last element n adding it to currentSum value)
            var result = SubSetSumMemo(input, len - 1, requiredSum, state, currentSum) || SubSetSumMemo(input, len - 1, requiredSum, state, currentSum + input[len - 1]);
            state.Add(key, result);
            return state[key];
        }

        // Abdul Bari https://youtu.be/vLS-zRCHo-Y
        // Time O(n^3) || Space O(n^2)
        // Returns the Optimal/Min cost of searching Keys with given frequencies on creating Optimal Binary Search Tree
        public static int OptimalBinarySearchTree(int[] keys, int[] frequency, int len)
        {
            // Step1. create 2D array with 1 extra (row & col) for ease of calculation
            // Stores 'cost of search' each step
            int[,] C = new int[len + 1, len + 1];

            // Step2, set default value 0 along diagonal, as its represents tree with single node as root, Ex- C[1,1] indicates tree with 1st element as root, C[len,len] is fr last element
            for (int i = 0; i <= len; i++)
                C[i, i] = 0;                            // can skip this step as default value for new int array is 0 by default in C#

            // Step3. Calculate 'Min Cost of Search' starting with Cost of Search with Tree size 1 and keep increasing the size of tree
            for (int l = 1; l <= len; l++)              // 'l' indicates length of current window i.e, first we start with trees with 1 elemenet than with 2 than 3 ...
                for (int i = 0; i <= len - l; i++)      // 'i' indicates starting point of are sliding windows (windows which decides 'no of elements' to choose from as root)
                {
                    int j = i + l;                      // 'j' end point of sliding window

                    // now we loop and try each element as root to find minimum cost of search for this given set of elements

                    // if only 1 item to select as root, set its costOFSearch in table as 'Frequency * 1'
                    if (j - i == 1)
                    {
                        C[i, j] = frequency[i];         // store frequency for given root using index of root in keys/frequency
                        C[j, i] = j;                    // store choosen root element column ID = (1 + actualIndex of root in keys)
                    }
                    else
                    {
                        C[i, j] = int.MaxValue;         // Set default cost before finding minimum
                        var wt = Weight(frequency, i, j);
                        for (int k = i + 1; k <= j; k++)// 'k' indicates current col ID of element choose as root [ColID = 1 + index of element in keys/frequency array]
                        {
                            // Cost of (k-1 as root for elements b/w i..k-1) + (j as root for elements b/w k..j) + sum of individual frequency of each element b/w i..j
                            var currCost = C[i, k - 1] + C[k, j] + wt;
                            if (currCost < C[i, j])
                            {
                                C[i, j] = currCost;     // update min cost of search
                                C[j, i] = k;            // store selected root element col ID(in opposite side of diagonal of 2D array Hint: Its the part that is never used)
                            }
                        }
                    }
                }
            //C.Print();
            PrintOptimalBST(C, keys, 0, len);
            return C[0, len];
        }

        // returns the combined sum of frequency from start to last-1 elements, required during Optiomal BST
        public static int Weight(int[] frequency, int start, int last)
        {
            var combineCostOfIndividualSearchs = 0;
            while (start < last)
                combineCostOfIndividualSearchs += frequency[start++];
            return combineCostOfIndividualSearchs;
        }

        public static void PrintOptimalBST(int[,] C, int[] keys, int row, int col, string spacing = "\t")
        {
            // Optimal Cost is stored at C[i,j]
            // Choosen Root is stored at C[j,i] opp. index
            if (row != col)     // skip diagonal's 
            {
                var indexInKeys = C[col, row] - 1;          // since we stored col ID & not index during building table
                var element = keys[indexInKeys];
                Console.Write($" Root {element}");

                // print left subtree
                Console.Write($"\n {spacing}{element}'s Left >>");
                PrintOptimalBST(C, keys, row, indexInKeys, spacing + spacing);

                // print right subtree
                Console.Write($"\n {spacing}{element}'s Right >>");
                PrintOptimalBST(C, keys, indexInKeys + 1, col, spacing + spacing);
            }
        }

        // Tushar Roy https://youtu.be/We3YDTzNXEk
        /// <summary>
        /// returns Minimum no of operation (Delete,Convert,Add) required to Convert string 'A' into String 'B'
        /// Time O(nm) || Space O(nm) , where n = length of string A and m = len of string B
        /// </summary>
        /// <param name="a">String being Converted</param>
        /// <param name="lenA"></param>
        /// <param name="b">Referrence String</param>
        /// <param name="lenB"></param>
        /// <returns></returns>
        public static int MinimumEditDistance(string a, int lenA, string b, int lenB)
        {
            // Step1 Create 2D array which will hold intermediate minimum cost of convert string A from 1..i to string B from 1..j
            int[,] C = new int[lenB + 1, lenA + 1];     // 1 extra row&col for ease of calculations, row=for stringB(matchWith string) col=for stringA

            // Step2 Set default values for 1st row and 1st col


            for (int i = 0; i <= lenB; i++)             // to iterate over string B, increasing no of characters to be matched one at a time
                for (int j = 0; j <= lenA; j++)         // to iterate over string A, how many operations wud be required to convert increasing length of A to B
                    if (i == 0)                 // indicates we need to convert 'A' to null
                        C[i, j] = j;
                    else if (j == 0)            // indicates we need to convert null to 'B'
                        C[i, j] = i;
                    else if (a[j - 1] == b[i - 1])// char match, no operation required
                        C[i, j] = C[i - 1, j - 1];
                    else                        // either Addition, Conversion or Deletion would be required
                        C[i, j] = 1 + Math.Min(C[i - 1, j], Math.Min(C[i - 1, j - 1], C[i, j - 1]));        // adding 1 indicates same cost of all 3 operations(expand the equation if those can be different)
            //C.Print();
            PrintMinDistanceOperations(C, lenB, lenA, a, b);
            return C[lenB, lenA];
        }

        public static void PrintMinDistanceOperations(int[,] C, int row, int col, string a, string b)
        {
            while (row > 0 && col > 0)
            {
                if (b[row - 1] == a[col - 1])                                   // same characters were found
                { row--; col--; }
                else //(b[row - 1] != a[col - 1])
                {
                    if (C[row, col] == 1 + C[row - 1, col - 1])                 // value came from diagonal cell (convert operation)
                    {
                        row--; col--;
                        Console.WriteLine($" '{a[col]}' Conveted to '{b[row]}'");
                    }
                    else if (C[row, col] == 1 + C[row, col - 1])                // value came from left cell (delete operation)
                    {
                        col--;
                        Console.WriteLine($" '{a[col]}' Deleted");
                    }
                    else // (C[row, col] == 1 + C[row-1, col])                  // value came from above cell (insert operation)
                    {
                        row--;
                        Console.WriteLine($" '{b[row]}' inserted");
                    }
                }
            }
        }

        // Abdul Bari https://youtu.be/oNI0rf2P9gE
        // Time O(n^3) || Space O(n^2)
        // Returns 'All Pair Shortest Path' for Directed A-cyclic Graph
        // Self Loops are replaced by 0 and no Edge is indicated by +infinity i.e, IntMax
        public static int[,] FloydWarshall(int[,] graph, int noOfVertex)
        {
            // Step1 Create a table to store shortest path b/w all pairs of Vertex
            int[,] S = new int[noOfVertex, noOfVertex];

            // Step2. Copy all values from graph matrix to S
            for (int i = 0; i < noOfVertex; i++)
                for (int j = 0; j < noOfVertex; j++)
                    S[i, j] = graph[i, j];

            // Step3. If Self loops are present remove them
            for (int j = 0; j < noOfVertex; j++)
                S[j, j] = 0;

            // Step4. for each vertex A..B try and see if there exists an path via some Vertex V which yields shorter distance,
            // dist(A to B) = Min(dist(A to B) , dist(A to K) + dist(K to B))
            for (int k = 0; k < noOfVertex; k++)                            // intermediate Vertex being tried
                for (int i = 0; i < noOfVertex; i++)                        // Vertex A
                    for (int j = 0; j < noOfVertex; j++)                    // Vertex B
                        if (i != j && i != k && j != k)                             // skip when Vertex A&B are same, also exclude when k is either A or B
                            if (S[i, k] != int.MaxValue && S[k, j] != int.MaxValue) // make sure connecting edge with K exists (IntMax indicates Edge doesn't exists)
                                S[i, j] = Math.Min(S[i, j], S[i, k] + S[k, j]);

            return S;
        }

        // Time O(n^2) || Auxillary Space O(1), as we have n^2 sub-problems to compute each of which takes constant time to compute
        public static int OptimalStrategyGameRecursive(int[] input, int start, int last)
        {
            if (start == last)          // only single coin left we take it
                return input[start];
            else if (start + 1 == last) // if just 2 coins are left we take Max of either
                return Math.Max(input[start], input[last]);

            /* Here we are dealing with an Smart Player2, and as Player1 we have two choice as laid out below:
             * Case1: We(Player1) choose the >> 'start value/coin'
             *      Start+1.....Last        these value are left
             *      Player2 will definatly pick the value which is higher for him/her and has 2 choices below:
             *              CaseA: P2 chooses >> start+1,  now we are left with start+2...Last to choose from
             *              CaseB: P2 chooses >> last,     now we are left with start+1...last-1 to choose from
             *      since we have a smart player2 we will be left with Min (of above two cases) + are orginal value 'Start'
             * Case2: We(Player1) choose the >> 'last value/coin'
             *      Start...Last-1          these values are left on table
             *      Player2 will again make most smart choice and pick the higher and leaves us with below 2 option:
             *              CaseA: P2 choose >> start,     now we are left with start+1...last-1 to choose from
             *              CaseB: P2 choose >> last-1,    now we are left with start...last-2 to choose from
             *      since we have a smart player2 we will be left with Min (of above two cases) + are orginal value 'Last'
             */
            return Math.Max(input[start] + Math.Min(OptimalStrategyGameRecursive(input, start + 2, last),
                                                    OptimalStrategyGameRecursive(input, start + 1, last - 1)),
                            input[last] + Math.Min(OptimalStrategyGameRecursive(input, start + 1, last - 1),
                                                    OptimalStrategyGameRecursive(input, start, last - 2)));
        }

        // Time O(n^2) || Auxillary Space O(n^2), as we have n^2 sub-problems to compute each of which takes constant time to compute
        // DP Memoization approach 'top-down' solution
        public static int OptimalStrategyMemo(int[] input, int start, int last, int[,] cache)
        {
            if (cache[start, last] > 0)         // if sub-Problem is solved before return from cache
                return cache[start, last];
            if (start == last)                  // base case 1
                return cache[start, last] = input[start];
            if (start + 1 == last)              // base case 2
                return cache[start, last] = Math.Max(input[start], input[last]);

            return cache[start, last] = Math.Max(input[start] + Math.Min(OptimalStrategyMemo(input, start + 2, last, cache),
                                                                        OptimalStrategyMemo(input, start + 1, last - 1, cache)),
                                                 input[last] + Math.Min(OptimalStrategyMemo(input, start + 1, last - 1, cache),
                                                                        OptimalStrategyMemo(input, start, last - 2, cache)));
        }

        // Time O(n^2) || Space O(n^2)
        // DP Tabulation approach 'bottom-up' solution
        public static int OptimalStrategy(int[] input, int len)
        {
            int[,] cache = new int[len, len];
            int x, y, z;

            for (int size = 0; size < len; size++)
                for (int i = 0; i < len - size; i++)
                {
                    int j = i + size;
                    if (i == j)                 // base case 1, considering only 1 element in array
                        cache[i, j] = input[i];
                    else
                    {
                        x = i + 2 <= j ? cache[i + 2, j] : 0;
                        y = i + 1 <= j - 1 ? cache[i + 1, j - 1] : 0;
                        z = i <= j - 2 ? cache[i, j - 2] : 0;

                        cache[i, j] = Math.Max(input[i] + Math.Min(x, y),
                                               input[j] + Math.Min(y, z));
                    }
                }
            return cache[0, len - 1];
        }

        // Time O(n^2) || Space O(1)
        // Recursive Brute Force Solution which returns longest Palindromic Subsequence present in input string
        public static int LongestPalindromicSubsequenceRecursive(string input, int start, int last)
        {
            if (start == last) return 1;                                    // single character string had Palindrome of len 1
            if (start + 1 == last && input[start] == input[last]) return 2; // string of len 2 with same character has Palindrome of len 2

            // If first & last character match than we atleast have Palindrome of len 2+ Longest Palindrome in i+1..j-1
            if (input[start] == input[last]) return 2 + LongestPalindromicSubsequenceRecursive(input, start + 1, last - 1);

            // else If first & last character don't match than find longest Palindrome in 'i..j-1' & 'i+1..j'
            return Math.Max(LongestPalindromicSubsequenceRecursive(input, start, last - 1), LongestPalindromicSubsequenceRecursive(input, start + 1, last));
        }

        // Tushar Roy https://youtu.be/_nCsPn7_OgI
        // Time O(n^2) || Space O(n^2)
        // DP Tabulation based bottom-up solution
        public static int LongestPalindromicSubsequence(string input, int len)
        {
            // Step1 Create a 23 table to store lps of intermediate string while finding LPS for entire string
            int[,] lps = new int[len, len];

            // Step2 Start by finding lps for string consider length = 1, n after each iteration keep increasing the len to finally match it to len of string
            for (int l = 0; l < len; l++)
                for (int i = 0; i < len - l; i++)
                {
                    var j = i + l;

                    if (i == j)                                     // base1 case single character string
                        lps[i, j] = 1;
                    else if (i + 1 == j && input[i] == input[j])    // base2 case string of len 2
                        lps[i, j] = 2;
                    else if (input[i] == input[j])                  // first last character match
                        lps[i, j] = 2 + lps[i + 1, j - 1];
                    else                                            // Longest LPS from i..j-1 or i+1..j
                        lps[i, j] = Math.Max(lps[i, j - 1], lps[i + 1, j]);
                }

            return lps[0, len - 1];
        }

        // https://youtu.be/UflHuQj6MVA
        // Time O(n^2) || Space O(n^2)
        // DP Tabulation based bottom-up solution
        public static int LongestPalindromicSubString(string input)
        {
            var len = input.Length;
            bool[,] dp = new bool[len, len];
            string lps = "";
            int maxLength = 0;

            for (int l = 0; l < len; l++)
                for (int start = 0; start < len - l; start++)
                {
                    int last = start + l;
                    bool isPalindrome = false;

                    if (start == last) dp[start, last] = isPalindrome = true;
                    else if (start + 1 == last && input[start] == input[last]) dp[start, last] = isPalindrome = true;
                    else if (input[start] == input[last]) dp[start, last] = isPalindrome = dp[start + 1, last - 1];

                    if (isPalindrome && last - start + 1 > maxLength)
                    {
                        maxLength = l + 1;
                        lps = input.Substring(start, l + 1);
                    }
                }
            Console.WriteLine($" Longest Palindromic Substring in \'{input}\'\tis: \'{lps}\'\tofLength \'{maxLength}\'");
            return maxLength;

            /*
            bool[,] lps = new bool[len, len];
            int maxLen = 0;
            string LPS = "";

            for (int size = 0; size < len; size++)
                for (int i = 0; i < len - size; i++)
                {
                    int j = i + size;
                    var isPalindrome = false;

                    if (i == j)                     // single character is palindrome of length 1
                        isPalindrome = true;
                    else if (i + 1 == j)            // 2 character string is palindrome if both characters match
                        isPalindrome = input[i] == input[j];
                    else                            // for (Len > 3), if input[first] == input[last] than check if input i+1..j-1 is palindrome or not
                        isPalindrome = (input[i] == input[j]) && lps[i + 1, j - 1];

                    lps[i, j] = isPalindrome;
                    if (isPalindrome) maxLen = Math.Max(maxLen, j - i + 1);
                }
            return maxLen;
            */
        }

        // Time O(n^2) || Space O(n^2)
        public static int TimesStringAOccursAsSubsequenceInStringB(string text, int lenA, string pattern, int lenB)
        {
            int[,] count = new int[lenA + 1, lenB + 1];

            // If text string is empty, no subsequence possible
            for (int i = 0; i <= lenB; i++)
                count[0, i] = 0;

            // If pattern string is empty, if empty string is to be match every text string will match for empty scenario atleast once
            for (int i = 0; i <= lenA; i++)
                count[i, 0] = 1;

            for (int i = 1; i <= lenA; i++)          // text length
                for (int j = 1; j <= lenB; j++)      // pattern length
                    // last characters match than we can choose either to include them or we can try to find subsequence in shorter length text
                    if (text[i - 1] == pattern[j - 1])
                        count[i, j] = count[i - 1, j - 1] + count[i - 1, j];
                    // last characters different, try reducing length of text to find an match
                    else
                        count[i, j] = count[i - 1, j];
            //count.Print();
            return count[lenA, lenB];
        }

        // Time O(n^2) || Space O(n^2)
        public static int MaxSizeSquareSubMatrix(int[,] input, int row, int col)
        {
            int[,] m = new int[row, col];
            int max = 0, maxI = 0, maxJ = 0;

            for (int i = 0; i < row; i++)
                for (int j = 0; j < col; j++)
                {
                    if (i == 0)             // for 1st row
                        m[i, j] = input[i, j];
                    else if (j == 0)        // for 1st col
                        m[i, j] = input[i, j];
                    else if (input[i, j] == 1)// right & bottom most corner has 1, check for biggest sub-square possible around it
                        m[i, j] = 1 + Math.Min(Math.Min(m[i - 1, j], m[i, j - 1]), m[i - 1, j - 1]);
                    else                    // right & bottom most corner has 0, subSquare not possible
                        m[i, j] = 0;

                    // keep updating max square found
                    if (m[i, j] > max)
                    {
                        max = m[i, j];      // max size square
                        maxI = i;           // bottom most row index
                        maxJ = j;           // right most col index
                    }
                }
            //m.Print(true);
            return max;
        }

        // Tushar Roy https://youtu.be/yCQN096CwWM
        // Time O(col x col x row) || Space O(row)
        public static int MaximumSumRectangleSubMatrix(int[,] input, int row, int col)
        {
            int L, R, max = int.MinValue;
            int maxLeft = 0, maxRt = 0, maxTop = 0, maxBottom = 0;

            // Step1. Iterate thru each sub-matrix consider size/width(no of col) starting with orignal & keep moving the starting column Index
            // Also keep track of current LeftMost & RtMost point in SubMatrix
            // L Marks start of current sub-Matrix
            for (L = 0; L < col; L++)                                       // Time O(column)
            {
                // Step2. Create 1D array of len = 'row' to store value of one column of Matrix
                int[] tab = new int[row];

                // R Marks the end of current sub-Matrix
                for (R = L; R < col; R++)                                   // Time O(column)
                {
                    // Add all elements of current column to their specific index in 1D array
                    for (int i = 0; i < row; i++)                           // Time O(row)
                        tab[i] += input[i, R];


                    int start = 0, last = 0;
                    // Returns MaxValueContinousSubsequence in O(n)
                    var currentSum = KadaneAlgo(tab, ref start, ref last);  // Time O(row)

                    // If new sum is bigger than global max, update max & maximum sum Sub-Matrix coordinates
                    if (currentSum > max)
                    {
                        max = currentSum;
                        maxLeft = L;
                        maxRt = R;
                        maxTop = start;
                        maxBottom = last;
                    }
                }
            }
            PrintMaxSumRectangleSubMatrix(input, maxLeft, maxRt, maxTop, maxBottom);
            return max;
        }

        // Time O(n) || Space O(1)
        // Returns MaxValueContinousSubsequence, also contains check in case all -ve numbers are present in array
        public static int KadaneAlgo(int[] input, ref int startIndex, ref int lastIndex)
        {
            int maxSumFoFar = int.MinValue;
            int currSum = 0, currentStart = 0;
            bool onePositiveNoPresent = false;

            for (int i = 0; i < input.Length; i++)          // Time O(n)
            {
                currSum += input[i];
                if (currSum < 0)
                {
                    currSum = 0;
                    currentStart = i + 1;
                }
                else if (currSum > maxSumFoFar)
                {
                    maxSumFoFar = currSum;
                    startIndex = currentStart;
                    lastIndex = i;
                    onePositiveNoPresent = true;
                }
            }

            // all numbers are -ve (additional check not required if we know for sure their exists atleast one +ve Integers)
            if (!onePositiveNoPresent)                      // Time O(n)
            {
                maxSumFoFar = int.MinValue;
                for (int i = 0; i < input.Length; i++)
                    if (input[i] > maxSumFoFar)
                    {
                        maxSumFoFar = input[i];
                        startIndex = lastIndex = i;
                    }
            }

            return maxSumFoFar;
        }

        public static void PrintMaxSumRectangleSubMatrix(int[,] input, int startCol, int endCol, int startRow, int endRow)
        {
            for (int r = startRow; r <= endRow; r++)
            {
                for (int c = startCol; c <= endCol; c++)
                    Console.Write($" \t{input[r, c]}");
                Console.WriteLine();
            }
        }

        // Time O(n^2) || Space O(n)
        // Solution is based upon LIS(LongestIncreasingSubsequence) approach
        public static int OptimalJumpsToReachLast(int[] input, int len)
        {
            // base case, if input array is empty or value at 0th index is zero means, no further jump possible
            if (len == 0 || input[0] == 0) return int.MaxValue;

            // array to hold min jumps required till given index
            int[] minJumps = new int[len];

            minJumps[0] = 0;                    // as 0 jumps required to reach 1st index

            // iterate thru each index starting from start and find min no of jumps to reach that index
            for (int i = 1; i < len; i++)
            {
                minJumps[i] = int.MaxValue;     // Set default value
                for (int j = 0; j < i; j++)
                    // If Jump Possible
                    if (input[j] >= i - j)
                        // update jump if shorter path found
                        minJumps[i] = Math.Min(minJumps[i], 1 + minJumps[j]);
            }
            return minJumps[len - 1];
        }

        // Time O(n) || Space O(1)
        public static int OptimalJumpsToReachLastOn(int[] input, int len)
        {
            if (len == 0 || input[0] == 0) return int.MaxValue;

            int jumps = 0, currLadderReach = 0, reserveLadderReach = 0;

            for (int i = 0; i < len; i++)
            {
                var ladderAti = input[i];

                // keep updating reserve ladder at each step (if found one with further reach)
                reserveLadderReach = Math.Max(reserveLadderReach - 1, ladderAti);

                currLadderReach--;

                // break if current ladder long enouf to reach last index
                if (i + currLadderReach >= len - 1)
                    return jumps;

                // if current ladder runs out switch to reserve one and increament the jumps counter
                if (currLadderReach <= 0)
                {
                    currLadderReach = reserveLadderReach;
                    reserveLadderReach = 0;
                    jumps++;
                }

                // if ladder at ith index is shorter than currLadderReach ignore it, if greater than check if it also greater than reserveLadderReach
                if (ladderAti > currLadderReach && ladderAti > reserveLadderReach)
                    reserveLadderReach = ladderAti;
            }
            return jumps;
        }

        // Time O(n^2) || Space O(n)
        // Solution is based upon LIS(LongestIncreasingSubsequence) approach
        public static int CircusTowerRoutine(List<Person> input, int len)
        {
            int maxPplPossible = 0;
            // sort initial list by height
            input = input.OrderBy(p => p.ht).Reverse().ToList();

            int[] tab = new int[len];

            for (int i = 0; i < len; i++)
            {
                tab[i] = 1;         // default value min 1 person is always possible
                for (int j = 0; j < i; j++)
                    if (input[j].wt > input[i].wt && tab[i] < 1 + tab[j])
                        tab[i] = 1 + tab[j];
                maxPplPossible = Math.Max(maxPplPossible, tab[i]);
            }
            return maxPplPossible;
        }

        // recursive solution // Time O(n^3) || Space O(1)
        // Function which returns true if in input there exists a such a set when each substrings which is present in dictionary else false
        // Ex: for string "code" returns true if dictionary contains 'c','od','e' or excat word "code"
        public static bool WordBreakProblemRecursive(string input, HashSet<string> dictionary)
        {
            var len = input.Length;
            if (input == null || len < 1) return false;                 // null input or blank string
            if (dictionary.Contains(input)) return true;                // entire give word is present in dictonary
            // try spliting the string into two part starting with 1 characters only on left if that is present in dictionary
            // check right half recursively if it yields true
            for (int i = 0; i < len; i++)
                if (dictionary.Contains(input.Substring(0, i))          // substring from 0th index of length i present in Dictionary
                    && WordBreakProblemRecursive(input.Substring(i, len - i), dictionary))   // & check recursively remaining substring excluding above part returns true
                    return true;

            return false;
        }

        // Top-Down Memoization based solution // Time O(n^2) || Space O(n^2)
        public static bool WordBreakProblemMemo(string input, HashSet<string> dictionary, Dictionary<string, bool> memo)
        {
            var len = input.Length;
            if (input == null || len < 1) return false;                 // null input or blank string

            if (dictionary.Contains(input) ||                           // entire word is present in dictonary
                (memo.ContainsKey(input) && memo[input]))               // or we have previously calculated this sub-problem/string and result was true
                return true;

            bool result = false;
            // try spliting the string into two part starting with 1 characters only on left if that is present in dictionary
            // check right half recursively if it yields true
            for (int i = 0; i < len; i++)
                // substring from 0th index of length i present in Dictionary && check recursively remaining substring excluding first half part returns true
                if (dictionary.Contains(input.Substring(0, i)) && WordBreakProblemMemo(input.Substring(i), dictionary, memo))
                {
                    result = true;
                    break;
                }
            if (result) memo.Add(input, result);
            return result;
        }

        // Bottom-Up Tabulation based solution // Time O(n^3) || Space O(n^2)
        // Function which returns true if for given input there exists such a set where each substrings is present in dictionary & also prints selected Words
        // Ex: for string "code" returns true if dictionary contains 'c','od','e' or excat word "code"
        public static bool WordBreakProblemTabulation(string input, HashSet<string> dictionary)
        {
            var len = input.Length;
            if (input == null || len < 1) return false;

            int[,] tab = new int[len, len];

            for (int i = 0; i < len; i++)
                for (int j = 0; j < len; j++)
                    tab[i, j] = -1;                                     // set default -1 to indicate word/substring of words not found in Dictionary

            for (int size = 1; size <= len; size++)                     // size of current substring start from 1 to entire length of input
                for (int i = 0; i <= len - size; i++)                   // start index in input from where we consider substring
                {
                    int j = i + size - 1;                               //  last index in input till where we consider substring
                    var substring = input.Substring(i, size);           // extract string for ease of use

                    if (dictionary.Contains(substring))                 // if given substring exists in dictionary mark this True and continue onto next string
                    { tab[i, j] = j; continue; }

                    // looking for an index i.e. 'splitAT' so that both left & right substring return true
                    for (int splitAt = i; splitAt <= j; splitAt++)
                        if (tab[i, splitAt] != -1 && tab[splitAt + 1, j] != -1)
                        { tab[i, j] = splitAt; break; }
                }

            // if subsets of words for input which exists in Dictionary found print them & return True
            var found = tab[0, len - 1] != -1;
            if (found) PrintWordsInWordBreakProblem(tab, input, len);
            return found;
        }

        // Time O(n)
        public static void PrintWordsInWordBreakProblem(int[,] arr, string input, int len)
        {
            Console.Write($"\n Words which were found in dictionary are: \t");
            int i = 0, j = len - 1, k;
            while (i <= j)
            {
                k = arr[i, j];
                if (j == k)     // entire word found, print word & exit
                {
                    Console.Write($" \t{input.Substring(i, j - i + 1)}");
                    break;
                }
                else            // else print left half n continue to search for remaining words in right half
                {
                    var p = Math.Max(k, 1); // needed since we are printing character on 0th index hence add 1 to length
                    Console.Write($" \t{input.Substring(i, p)}");
                }
                i = k + 1;      // keeping interating on right half
            }
        }


        // DP-Tabulation (bottom-Up) approach
        // Time O(N^3) || Space O(N^2)
        public static int BurstBalloons(int[] nums)
        {
            int len = nums.Length;
            int[,] dp = new int[len, len];

            for (int l = 1; l <= len; l++)                      // to change the length of sub-array currently being evaluated
                for (int start = 0; start <= len - l; start++)  // to update starting index of sub-array
                {
                    int last = start + l - 1;
                    // try maximizing sum by trying every balloon b/w start & last (inclusive of boundry) as being last balloon to be bursted
                    for (int k = start; k <= last; k++)
                    {
                        int leftSum = k - 1 < start ? 0 : dp[start, k - 1];
                        int rightSum = k + 1 > last ? 0 : dp[k + 1, last];

                        int leftNum = start - 1 < 0 ? 1 : nums[start - 1];      // Number on left of current sub-array
                        int rtNum = last + 1 >= len ? 1 : nums[last + 1];       // Number on right of current sub-array

                        dp[start, last] = Math.Max(dp[start, last], leftSum + (leftNum * nums[k] * rtNum) + rightSum);
                    }
                }

            return dp[0, len - 1];
        }


        // Recursive Approach
        // Time = O(Row*(Col^2)) || Space = O(1)
        public static int CherryPickupII(int[][] grid, int r1Pos, int r2Pos, int firstCol, int lastCol, int currRow = 0)
        {
            if (currRow >= grid.Length) return 0;
            int sum = 0;
            for (int i = r1Pos - 1; i <= r1Pos + 1; i++)
                if (i >= firstCol && i <= lastCol)
                    for (int j = r2Pos - 1; j <= r2Pos + 1; j++)
                        if (j >= firstCol && j <= lastCol && j != i)
                            sum = Math.Max(sum, grid[currRow][i] + grid[currRow][j] + CherryPickupII(grid, i, j, firstCol, lastCol, currRow + 1));
            return sum;
        }
        // DP Top-Down Approach
        // Time = Space = O(Row*(Col^2))
        public static int CherryPickupII_DP(int[][] grid, int r1Pos, int r2Pos, int firstCol, int lastCol, Dictionary<string, int> cache, int currRow = 0)
        {
            if (currRow >= grid.Length) return 0;
            int sum = 0;
            for (int i = r1Pos - 1; i <= r1Pos + 1; i++)
                if (i >= firstCol && i <= lastCol)
                    for (int j = r2Pos - 1; j <= r2Pos + 1; j++)
                        if (j >= firstCol && j <= lastCol && j != i)
                        {
                            if (!cache.ContainsKey((currRow + 1) + "," + i + "," + j))
                                cache.Add((currRow + 1) + "," + i + "," + j, CherryPickupII_DP(grid, i, j, firstCol, lastCol, cache, currRow + 1));
                            sum = Math.Max(sum, grid[currRow][i] + grid[currRow][j] + cache[(currRow + 1) + "," + i + "," + j]);
                        }
            return sum;
        }


        public static int MinOperationsToReduceToZero(int[] nums, int start, int last, int reqSum, ref bool is0)
        {
            if (reqSum == 0)
            { is0 = true; return 0; }

            if (start > last || reqSum < 0)
                return -1;

            int stepsReqL = int.MaxValue, stepsReqR = int.MaxValue;
            bool got0Left = false, got0Right = false;

            stepsReqL = MinOperationsToReduceToZero(nums, start + 1, last, reqSum - nums[start], ref got0Left);
            stepsReqR = MinOperationsToReduceToZero(nums, start, last - 1, reqSum - nums[last], ref got0Right);

            // if we got zero either by removing from left or right side num
            if (got0Left || got0Right)
            {
                is0 = true;
                // if we got zero from both left or right ends
                if (got0Left && got0Right)
                    return 1 + Math.Min(stepsReqL, stepsReqR);
                else
                    return 1 + (got0Left ? stepsReqL : stepsReqR);
            }
            return -1;
        }
        public static int MinOperationsToReduceToZero_DP(int[] nums, int start, int last, int reqSum, ref bool is0, Dictionary<string, int> cache)
        {
            if (reqSum == 0)
            { is0 = true; return 0; }

            if (start > last || reqSum < 0)
                return -1;

            string key = start + "," + last;
            if (cache.ContainsKey(key))
            {
                if (cache[key] != -1) is0 = true;
                return cache[key];
            }

            int stepsReqL = int.MaxValue, stepsReqR = int.MaxValue, value = -1;
            bool got0Left = false, got0Right = false;

            stepsReqL = MinOperationsToReduceToZero_DP(nums, start + 1, last, reqSum - nums[start], ref got0Left, cache);
            stepsReqR = MinOperationsToReduceToZero_DP(nums, start, last - 1, reqSum - nums[last], ref got0Right, cache);

            if (got0Left || got0Right)                                  // if we got zero either by removing from left or right side num
                if (got0Left && got0Right)
                    value = 1 + Math.Min(stepsReqL, stepsReqR);        // if we got zero from both left or right ends, assign min steps of two
                else
                    value = 1 + (got0Left ? stepsReqL : stepsReqR);     // assign steps from where we got zero

            if (value != -1)
                is0 = true;

            cache.Add(key, value);
            return value;
        }
        // Slightly Clear n better DP Top-Down // TLE
        public static int MinOperationsToReduceXToZero(int[] nums, int x, Dictionary<string, int> cache, int start, int last)
        {
            if (x == 0) return 0;
            if (start > last || x < 0) return -1;     // default val informing X -> 0 not possible

            string key = start + "," + last;
            if (cache.ContainsKey(key)) return cache[key];

            int removeLeft = MinOperationsToReduceXToZero(nums, x - nums[start], cache, start + 1, last);  // remove Start
            int removeRight = MinOperationsToReduceXToZero(nums, x - nums[last], cache, start, last - 1);  // remove Last
            int ans = 0;

            if (removeLeft != -1 && removeRight != -1)  // removing from either left or rt side we can reduce 'X' to 0 reduce min operation from each
                ans = 1 + Math.Min(removeLeft, removeRight);
            else
                ans = Math.Max(removeLeft, removeRight) == -1 ? -1 : 1 + Math.Max(removeLeft, removeRight);

            cache.Add(key, ans);
            return ans;
        }
        // Time O(n) || Space O(1)
        public static int MinOperations_SlidingWindow(int[] nums, int x)
        {
            /* Algorithm
             * Step 1: Calculate the total sum of nums. Mark as total.
             * 
             * Step 2: Initialize two pointers left and right to 0. 
             * Initialize an integer current to represent the sum from nums[left] to nums[right], inclusively.
             * Initialize an integer maxLen to record the maximum length that sums up to total - x.
             * 
             * Step 3: Iterate right form 0 to the end of nums. In each iteration:
             * Update current.
             * If current is greater than total - x, move left to right.
             * If current is equal to total - x, update the maximum length.
             * 
             * Step 4: Return the result.
             */
            int start = 0, last = 0, maxLen = -1, currWindowSum = 0, total = 0;
            for (int i = 0; i < nums.Length; i++) total += nums[i];

            while (last < nums.Length)
            {
                currWindowSum += nums[last];
                while (currWindowSum > total - x && start <= last)
                    currWindowSum -= nums[start++];
                if (currWindowSum == total - x)
                    maxLen = Math.Max(maxLen, last - start + 1);
                last++;
            }
            return maxLen != -1 ? nums.Length - maxLen : -1;
        }


        // Time O(Max(n^2,m^3)) || Space O(n^2), n = length of A & m = length of B
        public static int MaximumLengthOfRepeatedSubarray(int[] A, int[] B)
        {
            HashSet<string> set = new HashSet<string>();
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < B.Length; i++)          // O(n^2)
            {
                for (int j = i; j < B.Length; j++)
                {
                    sb.Append(B[j].ToString()).Append(",");
                    set.Add(sb.ToString());
                }
                sb.Clear();
            }
            int maxLen = 0;
            for (int i = 0; i < A.Length; i++)          // O(m^3) as 'sb.ToString()' is O(m) operation
            {
                for (int j = i; j < A.Length; j++)
                {
                    sb.Append(A[j].ToString()).Append(",");
                    string arr = sb.ToString();
                    if (set.Contains(arr))
                        maxLen = Math.Max(maxLen, j - i + 1);
                }
                sb.Clear();
            }
            return maxLen;
        }
        // Time O(n*m) || Space O(n*m), n = length of A & m = length of B
        public static int MaximumLengthOfRepeatedSubarray_DP(int[] A, int[] B)
        {
            /* subarray of A and B must start at some A[i] and B[j],
             * let dp[i][j] be the longest common prefix of A[i:] and B[j:].
             * Whenever A[i] == B[j], we know dp[i][j] = dp[i+1][j+1] + 1.
             * Also, the answer is max(dp[i][j]) over all i, j
             */
            int maxLen = 0, n = A.Length, m = B.Length;
            int[,] dp = new int[n + 1, m + 1];
            for (int i = n - 1; i >= 0; i--)
                for (int j = m - 1; j >= 0; j--)
                {
                    if (A[i] == B[j])
                        dp[i, j] = dp[i + 1, j + 1] + 1;
                    maxLen = Math.Max(maxLen, dp[i, j]);
                }
            return maxLen;
        }


        // Note: You can only move either down or right at any point in time.
        // Time = Space = O(R*C)
        public static int MinimumPathSum(int[][] grid)
        {
            int n = grid.Length - 1;
            int m = grid[0].Length - 1;

            int[,] dp = new int[n + 1, m + 1];
            for (int r = 0; r <= n; r++) for (int c = 0; c <= m; c++) dp[r, c] = -1;
            dp[n, m] = grid[n][m];

            return MinCostPath(0, 0, dp);

            // LOCAL FUNC
            int MinCostPath(int r, int c, int[,] cache, int cost = 0)
            {
                if (cache[r, c] != -1) return cost + cache[r, c];

                int costFromHere = int.MaxValue;
                if (isValid(r + 1, c))      // move down
                    costFromHere = MinCostPath(r + 1, c, cache, grid[r][c]);
                if (isValid(r, c + 1))      // move right
                    costFromHere = Math.Min(costFromHere, MinCostPath(r, c + 1, cache, grid[r][c]));

                cache[r, c] = costFromHere;
                // return cost to reach till here + cost from this cell
                return cost + costFromHere;
            }
            bool isValid(int r, int c) => r <= n && c <= m;
        }


        /// <summary>
        /// Given an integer n, return the number of strings of length n that consist only of vowels (a, e, i, o, u) and are lexicographically sorted.
        /// A string s is lexicographically sorted if for all valid i, s[i] is the same as or comes before s[i + 1] in the alphabet.
        /// Time O(n*5) ~O(n) || Space O(n)
        /// </summary>
        /// <param name="n"></param>
        /// <param name="lastChar"></param>
        /// <returns></returns>
        public static int CountSortedVowelStrings(int n, Dictionary<string, int> cache, int lastChar = 0)
        {
            /* Since the we need to calculate every string created from combination of 'vowel' that is lexographically sorted
             * We know for string of length 1 there are only 5 valid results =? "a", "e", "i", "o", "u"
             * & for string of length 2 we can have any of the vowel at 1st index
             * followed by all vowel which come after current one including current
             * 
             * We try every combination starting with each vowel at 1st index and make a recursive call passing of current letter n deducting the len by 1
             * 
             * Finally we make use of Cache to decrease our runtime
             */

            // when finding lexographically sorted string of length 1 if we start with first letter of vowel i.e. 'a' we get 5 diff combinations
            // starting with 'e' => 4, 'i' => 3, 'o' => 2 & 'u' => 1
            int[] vowelMap = { 5, 4, 3, 2, 1 };
            if (n == 1) return vowelMap[lastChar];

            string key = n + "," + lastChar;
            if (cache.ContainsKey(key)) return cache[key];

            int ans = 0;
            for (int i = lastChar; i < 5; i++)
                ans += CountSortedVowelStrings(n - 1, cache, i);

            cache.Add(key, ans);
            return ans;
        }


        // Time O(n^2) || Space O(n)
        public static int NumFactoredBinaryTrees(int[] A)
        {
            int mod = 1000000007, l = A.Length;
            Array.Sort(A);              // O(nlogn)

            long[] dp = new long[l];
            Dictionary<int, int> valIdx = new Dictionary<int, int>(l);
            for (int i = 0; i < l; i++)
            {
                dp[i] = 1;  // set default value (atleast one tree can be made by using value as root node)
                valIdx.Add(A[i], i);
            }

            for (int i = 1; i < l; i++)        // O(n^2)
                for (int j = 0; j < i; j++)
                    if (A[i] % A[j] == 0)
                        if (valIdx.ContainsKey(A[i] / A[j]))
                            dp[i] = (dp[i] + (dp[j] * dp[valIdx[A[i] / A[j]]]) % mod) % mod;
            long ans = 0;
            for (int i = 0; i < l; i++)
                ans = (ans + (dp[i] % mod)) % mod;
            return (int)(ans % mod);
        }


        [Obsolete("Doesnt works for all cases+ Algo is not efficient Time = O(n^2)",true)]
        public static int BestTimeToBuyAndSellStockWithTransactionFee_Depricated(int[] prices, int fee)
        {
            int l = prices.Length;
            int[,] dp = new int[l, l];

            for (int tradingPeriod = 1; tradingPeriod <= l; tradingPeriod++)
                for (int startD = 0; startD < l - tradingPeriod; startD++)
                {
                    int lastD = startD + tradingPeriod;
                    if (startD == lastD)
                        dp[startD, lastD] = -fee;
                    else if (startD + 1 == lastD)
                        dp[startD, lastD] = Math.Max(-fee, prices[lastD] - prices[startD] - fee);
                    else
                    {
                        int maxProfit = Math.Max(-fee, prices[lastD] - prices[startD] - fee);
                        for (int k = startD + 1; k < lastD; k++)
                            maxProfit = Math.Max(maxProfit, dp[startD, k] + dp[k + 1, lastD]);

                        dp[startD, lastD] = maxProfit;
                    }
                }

            return Math.Max(dp[0, l - 1], 0);
        }
        // Time O(n) || Space O(1)
        public static int BestTimeToBuyAndSellStockWithTransactionFee(int[] prices, int fee)
        {
            /* If I am holding a share after today,
             * then either I am just continuing holding the share I had yesterday,
             * or that I held no share yesterday, but bought in one share today: hold = max(hold, cash - prices[i])
             * 
             * If I am not holding a share after today,
             * then either I did not hold a share yesterday, 
             * or that I held a share yesterday but I decided to sell it out today: cash = max(cash, hold + prices[i] - fee).
             * 
             * Make sure fee is only incurred once i.e. while selling.
             */
            int cash = 0, boughtPrice = -prices[0];
            for (int i = 1; i < prices.Length; i++)
            {
                cash = Math.Max(cash, boughtPrice + prices[i] - fee);
                boughtPrice = Math.Max(boughtPrice, cash - prices[i]);
            }
            return cash;
        }


        // Recursive Soln
        public static int MaxUncrossedLines_Recursive(int[] A, int[] B, int i = 0, int j = 0)
        {
            if (i >= A.Length || j >= B.Length) return 0;
            if (A[i] == B[j]) return 1 + MaxUncrossedLines_Recursive(A, B, i + 1, j + 1);
            else return Math.Max(MaxUncrossedLines_Recursive(A, B, i, j + 1), MaxUncrossedLines_Recursive(A, B, i + 1, j));
        }
        // DP Top-Down Approach || Time = Space = O(n*m), n = len of A & m = len of B
        public static int MaxUncrossedLines_DP(int[] A, int[] B, int i, int j, int[,] cache)
        {
            if (i >= A.Length || j >= B.Length)
                return 0;
            
            // if sub-problem is pre-computed return stored answer
            if (cache[i, j] != -1)
                return cache[i, j];

            if (A[i] == B[j])
                return cache[i, j] = 1 + MaxUncrossedLines_DP(A, B, i + 1, j + 1, cache);
            else 
                return cache[i, j] = Math.Max(  MaxUncrossedLines_DP(A, B, i, j + 1, cache),
                                                MaxUncrossedLines_DP(A, B, i + 1, j, cache));
        }
        // DP Top-Down Approach || Time = Space = O(n*m), n = len of A & m = len of B
        public static int MaxUncrossedLines_DP_DictionaryCache(int[] A, int[] B)
        {
            return FindMax(0, 0, new Dictionary<string, int>());
            // Local Func
            int FindMax(int i, int j, Dictionary<string, int> cache)
            {
                if (i >= A.Length || j >= B.Length)
                    return 0;

                string key = i + "," + j;
                // if sub-problem is pre-computed return stored answer
                if (cache.ContainsKey(key))
                    return cache[key];

                int ans = 0;
                if (A[i] == B[j])
                    ans = 1 + FindMax(i + 1, j + 1, cache);
                else
                    ans = Math.Max(FindMax(i, j + 1, cache),
                                    FindMax(i + 1, j, cache));
                cache.Add(key, ans);
                return ans;
            }
        }


        // Time = O(n^2) || Space = O(n), n = len of array 'envelopes' || LongestIncreasingSubsequence
        public static int MaxEnvelopes(int[][] envelopes)
        {
            envelopes = (from e in envelopes // sort by width O(nlogn)
                         orderby e[0]
                         select e).ToArray();
            //envelopes = envelopes.OrderBy(x => x[0]).ToArray();
            int[] dp = new int[envelopes.Length];
            int maxRussianDoll = 0;
            for (int i = 0; i < envelopes.Length; i++)
            {
                dp[i] = 1;
                for (int j = 0; j < i; j++)
                    if (envelopes[j][1] < envelopes[i][1] && envelopes[j][0] < envelopes[i][0] && dp[i] < 1 + dp[j])
                        dp[i] = 1 + dp[j];
                maxRussianDoll = Math.Max(maxRussianDoll, dp[i]);
            }
            return maxRussianDoll;
        }


        // Time = Space = O(n^2), n = length of str1
        public static bool IsScramble(string str1, string str2)
        {
            return TryScramble(str1, str2, new Dictionary<string, bool>());
            bool TryScramble(string s1, string s2, Dictionary<string, bool> cache)
            {
                if (s1 == s2) return true;
                string key = s1 + "+" + s2;
                if (cache.ContainsKey(key)) 
                    return cache[key];

                int n = s1.Length;
                int[] map = new int[26];
                // additional step to check the distinct characters & their frequency match, before making recursive calls
                for (int i = 0; i < n; i++)
                {
                    map[s1[i] - 'a']++;
                    map[s2[i] - 'a']--;
                }
                // check if frequency is not zero return false
                for(int i=0;i<map.Length;i++)
                    if(map[i]!=0)
                        return cache[key] = false;

                // Main Step, try breaking equal length s1 & s2 at each possible idx to check if any combination yields true
                for (int i = 1; i < n; i++)
                    // if A+X forms 's1'
                    // &  B+Y forms 's2'
                    // than we return true if either
                    // A,B & X,Y are scramble || A,Y & X,B are scramble
                    if ((TryScramble(s1.Substring(0, i), s2.Substring(0, i), cache) && TryScramble(s1.Substring(i), s2.Substring(i), cache))
                    || (TryScramble(s1.Substring(0, i), s2.Substring(n - i), cache) && TryScramble(s1.Substring(i), s2.Substring(0, n - i), cache)))
                        return cache[key] = true;

                return cache[key] = false;
            }
        }


        // Time O(n*l*l) || Space O(l), n = len of 'strs' & l = length of a word
        public static int MinDeletionSize(string[] strs)
        {
            /* Intution for algo is instead of keeping count of deleted columns we keep count of max columns we can keep
             * we use int array dp which has default value '1' for each columns as surely we can atleast keep 1 column for all rows
             * 
             * Now we check for a given columns C considering we keep it, can we keep the next column 'D',
             *      and to make sure we check all the rows of 'strs' should satisfy condition
             *              strs[rowId][C] <= strs[rowId][D]
             *      if not than we cannot keep column 'D'
             *      if all rows pass the check we can update max columns kept at given column C as Math.Max(dp[C],1+dp[D])
             *      
             * we start with 2nd last columns as C & continue in above fashion till C is >= 0
             * at end we pull the max column kept from dp array
             * and return total columns - max columns kept to get 'min columns deleted'
             */
            int len = strs[0].Length, i;
            int[] dp = new int[len];         // stores no of columns kept i.e. Not Deleted from current index
            for (i = 0; i < len; i++) dp[i] = 1; // as min 1 columns can be kept while still keep each word lexo-sorted in 'strs'

            // start checking for max possible columns count we can keep from each index start from 2nd last column onwards
            for (int startingCol = len - 2; startingCol >= 0; startingCol--)
                for (int currCol = startingCol + 1; currCol < len; currCol++)
                {
                    for (i = 0; i < strs.Length; i++)
                        if (strs[i][startingCol] > strs[i][currCol])    // not lexo-sorted
                            break;

                    if (i == strs.Length)       // all rows checks passed for curr column
                        dp[startingCol] = Math.Max(dp[startingCol], 1 + dp[currCol]);
                }
            int ans = 0;
            for (i = 0; i < len; i++)
                ans = Math.Max(ans, dp[i]);     // get maximum columns kept
            
            return len - ans;                   // returming min columns deleted
        }




    }
}
